using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeUI : MonoBehaviour
{
    public TMP_Text highScoreTxt;
    public BaseButton playBtn;
    public GameObject Guide;

    private void Awake()
    {
        playBtn.button.onClick.AddListener(OnClickPlay);
    }

    private void OnChangeHighScore(object data)
    {
        highScoreTxt.text = SessionPref.HighScore.ToString();
    }

    private void OnClickPlay()
    {
        GameController.Instance.PlayGame(true);
        EventDispatcher.PostEvent(EventID.OnChangeMusicVolumeInGame);

        //Guide.SetActive(true);
        UIController.Instance.ActiveHomeUI(false);
        SceneController.Instance.LoadSceneAgain();
    }

    private IEnumerator DelayToShowGuide()
    {
        yield return new WaitForSeconds(0.2f);
        //Guide.SetActive(true);
    }

    private void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.RegisterListener(EventID.ChangeHighScore, OnChangeHighScore);
        highScoreTxt.text = SessionPref.HighScore.ToString();
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveListener(EventID.ChangeHighScore, OnChangeHighScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
