using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public ParticleSystem hardBlockExplosion;
    public ParticleSystem woodBlockExplosion;
    public ParticleSystem redBallExplosion;
    public ParticleSystem greenBallExplosion;
    public ParticleSystem purpleBallExplosion;
    public Animator explosion;
    public Animator dieTrigerBlockExplosion;
    public Movement movement;
    public bool isHelmet;
    public Canvas IngameCanvas;
    public Sprite[] lstNormalPlayerSprite;
    public Sprite[] lstHelmetPlayerSprite;
    public int scoreEarnBall;
    private float remainingHelmetTime;
    public HelmetCount helmetCount;
    
    public bool IsHelmet
    {
        get => isHelmet;
        set
        {
            isHelmet = value;
            SetSpritePlayer(isHelmet);
            if (isHelmet)
            {
                CancelInvoke(nameof(UpdateHelmetTime));
                helmetCount.gameObject.SetActive(true);
                remainingHelmetTime = 10f;
                helmetCount.CurrentHelmetCount = remainingHelmetTime;
                InvokeRepeating(nameof(UpdateHelmetTime), 1f, 1f);
                Invoke(nameof(TurnOffHelmet), 10);
            }
            
            else
            {
                CancelInvoke(nameof(UpdateHelmetTime));
            }
        }
    }
    
    private void UpdateHelmetTime()
    {
        Debug.Log("- time"+remainingHelmetTime);
        remainingHelmetTime -= 1f;
        helmetCount.CurrentHelmetCount = remainingHelmetTime;
        if(remainingHelmetTime <= 0)
        {
            IsHelmet = false;
            helmetCount.gameObject.SetActive(false);
        }
        //Debug.Log("Time remaining to turn off helmet: " + remainingHelmetTime);
    }

    private void Start()
    {
        EventDispatcher.RegisterListener(EventID.ActiveBooster, OnActiveHelmet);
        EventDispatcher.RegisterListener(EventID.ResetGame, OnResetGame);
        SetSpritePlayer(false);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveListener(EventID.ActiveBooster, OnActiveHelmet);
        EventDispatcher.RemoveListener(EventID.ResetGame, OnResetGame);
    }

    private void OnResetGame(object data)
    {
        isHelmet = false;
        this.GetComponent<SpriteRenderer>().sprite = lstNormalPlayerSprite[SessionPref.CurrentItemInUse];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Helmet"))
        {
            if (isHelmet)
            {
                CancelInvoke(nameof(TurnOffHelmet));
                IsHelmet = true;

                other.gameObject.SetActive(false);
                return;
            }
            IsHelmet = true;

            other.gameObject.SetActive(false);
        }

        if (isHelmet)
        {
            if (other.CompareTag("HardBlock"))
            {
                var m_explosion = Instantiate(hardBlockExplosion, transform.position, Quaternion.identity);
                other.gameObject.SetActive(false);
                m_explosion.Play();
                SoundController.Instance.PlaySound(SoundType.BrickBreak);
            }

            //return;
        }
        if (isHelmet == false && other.CompareTag("HardBlock"))
        {
            Debug.Log("trigger obstacle");
            dieTrigerBlockExplosion.Play("Death");
            dieTrigerBlockExplosion.gameObject.SetActive(true);
            Invoke(nameof(DisableBlockDiwExplosionAnim), 1f);
            EndGame();
            SoundController.Instance.PlaySound(SoundType.Crash);
            var m_explosion = Instantiate(hardBlockExplosion, transform.position, Quaternion.identity);
            m_explosion.Play();
        }

        if (other.CompareTag("WoodBlock"))
        {
            Debug.Log("trigger obstacle");
            // var movement = this.GetComponent<Movement>();
            // movement.speed = 0;
            var m_explosion = Instantiate(woodBlockExplosion, transform.position, Quaternion.identity);
            m_explosion.Play();
            SoundController.Instance.PlaySound(SoundType.BrickBreak);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("RedBall"))
        {
            var m_explosion = Instantiate(redBallExplosion, transform.position, Quaternion.identity);
            m_explosion.Play();

            other.gameObject.SetActive(false);
            SoundController.Instance.PlaySound(SoundType.BallBreak);
            EventDispatcher.PostEvent(EventID.AddCoin, 5);
        }

        if (other.CompareTag("GreenBall"))
        {
            var m_explosion = Instantiate(greenBallExplosion, transform.position, Quaternion.identity);
            m_explosion.Play();
            SoundController.Instance.PlaySound(SoundType.BallBreak);
            other.gameObject.SetActive(false);
            EventDispatcher.PostEvent(EventID.AddCoin, 5);
        }

        if (other.CompareTag("PurpleBall"))
        {
            var m_explosion = Instantiate(purpleBallExplosion, transform.position, Quaternion.identity);
            m_explosion.Play();
            SoundController.Instance.PlaySound(SoundType.BallBreak);
            other.gameObject.SetActive(false);
            EventDispatcher.PostEvent(EventID.AddCoin, 5);
        }

        if (other.CompareTag("Bomb"))
        {
            Debug.Log("play explosion");
            explosion.gameObject.SetActive(true);
            SoundController.Instance.PlaySound(SoundType.Bomb);
            explosion.Play("Explosion");
            Invoke(nameof(DisableExplosionAnim), 1f);
            EndGame();
        }

        if (other.CompareTag("Gold"))
        {
            other.gameObject.SetActive(false);
            SoundController.Instance.PlaySound(SoundType.EarnCoin);

            var startPos = WorldToCanvasPosition(transform.position);
            Debug.Log("start pos: " + startPos);
            CoinsManager.Instance.AddCoins(transform.position,AnimFlyType.Star,1,1);
        }

        //other.gameObject.SetActive(false);
    }

    private void SetSpritePlayer(bool isHelmet)
    {
        if (this.isHelmet)
        {
            this.GetComponent<SpriteRenderer>().sprite = lstHelmetPlayerSprite[SessionPref.CurrentItemInUse];
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = lstNormalPlayerSprite[SessionPref.CurrentItemInUse];
        }
    }

    private void DisableExplosionAnim()
    {
        explosion.gameObject.SetActive(false);
    }

    private void DisableBlockDiwExplosionAnim()
    {
        dieTrigerBlockExplosion.gameObject.SetActive(false);
    }

    private void EndGame()
    {
        var movement = this.GetComponent<Movement>();
        movement.speed = 0;
        Debug.Log("remove camera target");
        //CameraController.Instance.RemoveCameraTarget();
        EventDispatcher.PostEvent(EventID.EndGame);
        StartCoroutine(DelayToShowTryAgainPanel());
        //ResetExplosionAnimator();
        CancelInvoke(nameof(TurnOffHelmet));
    }

    public Vector3 WorldToCanvasPosition(Vector3 worldPosition)
    {
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(IngameCanvas.transform as RectTransform, screenPosition, IngameCanvas.worldCamera, out Vector2 localPosition);
        return localPosition;
    }

    private IEnumerator DelayToShowTryAgainPanel()
    {
        yield return new WaitForSeconds(1f);
        UIController.Instance.ShowTryAgainPanel();
        Debug.Log("disable movement");
        EventDispatcher.PostEvent(EventID.EnableMovement, false);
    }

    private void TurnOffHelmet()
    {
        IsHelmet = false;
        helmetCount.CurrentHelmetCount = 0;
    }

    private void OnActiveHelmet(object data)
    {
        IsHelmet = true;
        movement.SetSpeedSlowPlayer();
    }

    private void ResetExplosionAnimator()
    {
        explosion.gameObject.SetActive(false);
        explosion.Rebind();
        explosion.Update(0f);
    }

    public Sprite GetSpritePlayer()
    {
        return lstNormalPlayerSprite[SessionPref.PlayerID];
    }
}
