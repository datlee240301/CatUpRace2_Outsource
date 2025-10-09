using System.Collections;
using System.Collections.Generic;
using DucLV;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public void LoadSceneAgain()
    {
        EventDispatcher.PostEvent(EventID.ResetGame);
        Debug.Log("enable movement");
        EventDispatcher.PostEvent(EventID.EnableMovement,true);
    }
}