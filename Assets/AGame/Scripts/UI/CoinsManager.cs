using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using DucLV;
using Random = UnityEngine.Random;

public class CoinsManager : Singleton<CoinsManager>
{
	//References
	[Header ("UI references")]
	[SerializeField] TMP_Text coinUIText;
	[SerializeField] GameObject animatedCoinPrefab;
	[SerializeField] Transform target;

	[Space]
	[Header ("Available coins : (coins to pool)")]
	[SerializeField] int maxCoins;
	Queue<GameObject> coinsQueue = new Queue<GameObject> ();


	[Space]
	[Header ("Animation settings")]
	[Space]
	[Header ("Animation settings")]
	[SerializeField] [Range (0.2f, 0.5f)] float minAnimDuration;
	[SerializeField] [Range (0.5f, 1f)] float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;

	Vector3 targetPosition;

	private Action callBack;
	private int _c = 0;

	[Space] [SerializeField] private bool isSetUI;

	public int Coins {
		get{ return _c; }
		set {
			_c = value;
			//update UI text whenever "Coins" variable is changed
			//coinUIText.text = Coins.ToString ();
		}
	}

	void Awake ()
	{
		targetPosition = target.localPosition;

		//prepare pool
		PrepareCoins ();
	}
	
	private IEnumerator CountMoneyUp(int startNumber, int endNumber)
	{
		yield return new WaitForSeconds(.15f);
		float currentTime = 0;
		//SoundController.Instance.PlaySound(SoundType.);
		while (currentTime <= 1)
		{
			int currentNumber = (int)Mathf.Lerp(startNumber, endNumber, currentTime / 3);
			coinUIText.text = currentNumber.ToString();
			currentTime += Time.deltaTime;
			yield return null;
		}
		
		coinUIText.text = endNumber.ToString(); // Ensure the final number is correct
		StartCoroutine(DelayedCallback(this.callBack, 0.2f));
	}

	void PrepareCoins ()
	{
		GameObject coin;
		for (int i = 0; i < maxCoins; i++) {
			coin = Instantiate (animatedCoinPrefab);
			coin.transform.parent = transform;
			coin.SetActive (false);
			coin.transform.localPosition = Vector3.zero;
			coin.transform.DOScale(Vector3.one, .1f);
			coinsQueue.Enqueue (coin);
		}
	}

	void Animate(Vector3 collectedCoinPosition, int amount, Action callBack = null)
	{
		if (isSetUI)
		{
			targetPosition = Vector3.zero;
		}

		this.callBack = callBack;
		for (int i = 0; i < amount; i++)
		{
			//check if there's coins in the pool
			if (coinsQueue.Count > 0)
			{
				//extract a coin from the pool
				GameObject coin = coinsQueue.Dequeue();
				coin.SetActive(true);

				Debug.Log("start pos: " + collectedCoinPosition);
				//move coin to the collected coin pos
				coin.transform.position = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);
				Debug.Log("coin pos " + coin.transform.position);
				//animate coin to target position

				float duration = Random.Range(minAnimDuration, maxAnimDuration);
				if (isSetUI)
				{
					coin.transform.DOLocalMove(Vector3.zero, duration)
						.SetEase(easeType)
						.OnComplete(() =>
						{
							//executes whenever coin reach target position
							//coin.SetActive (false);
							Debug.Log("target pos: " + targetPosition);
							Debug.Log("coin pos " + coin.transform.position);
							StartCoroutine(DelayToHideCoin(coin, 1f));

							Coins++;
							if (Coins == amount)
							{
								Debug.Log("call back coin animation complete");
								//StartCoroutine(DelayedCallback(callBack, 0.5f));
							}
						});
				}
				else
				{
					coin.transform.DOLocalMove(targetPosition, duration)
						.SetEase(easeType)
						.OnComplete(() =>
						{
							//executes whenever coin reach target position
							//coin.SetActive (false);
							Debug.Log("complete coin animation "+amount);
							if(i == amount - 1)
							{
								Debug.Log("add coin text");

							}
							Debug.Log("add coin "+ amount);
							EventDispatcher.PostEvent(EventID.AddCoin, amount);
							StartCoroutine(DelayToHideCoin(coin, .2f));
							EventDispatcher.PostEvent(EventID.AddCoin);
							Coins++;
							if (Coins == amount)
							{
								Debug.Log("call back coin animation complete");
								//StartCoroutine(DelayedCallback(callBack, 0.5f));
							}
						});
				}
			}
		}
	}

	IEnumerator DelayToHideCoin (GameObject coin, float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		coin.SetActive (false);
		coinsQueue.Enqueue (coin);
	}
	
	IEnumerator DelayedCallback(Action callback, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		callback?.Invoke();
	}

	public void AddCoins (Vector3 collectedCoinPosition,AnimFlyType type, int amountCoinUI, int amountAdditionalCoin, Action callBack = null)
	{
		SessionPref.AddGoldRemaining(amountCoinUI);
		if (type == AnimFlyType.Star)
		{

			//StartCoroutine(CountMoneyUp(SessionPref.StarRemaining,SessionPref.StarRemaining + amountAdditionalCoin));
			Animate (collectedCoinPosition, amountCoinUI, callBack);
		}
		else
		{
			//Debug.Log("hint remain: "+SessionPref.GetHintRemaining());
			//StartCoroutine(CountMoneyUp(SessionPref.GetHintRemaining(),SessionPref.GetHintRemaining() + amountAdditionalCoin));
			Animate (collectedCoinPosition, amountCoinUI, callBack);
		}
	}
}

public enum AnimFlyType
{
	Star,
	Hint
}
