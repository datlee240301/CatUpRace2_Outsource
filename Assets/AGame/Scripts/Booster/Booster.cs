using System;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public BoosterTypes boosterType;

    private void Start()
    {
        EventDispatcher.RegisterListener(EventID.ActiveBooster, OnActiveBooster);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveListener(EventID.ActiveBooster, OnActiveBooster);
    }

    public virtual void ActivateBooster()
    {
        Debug.Log("Booster activated");
    }

    public void OnActiveBooster(object data)
    {
        ActivateBooster();
    }
}

public enum BoosterTypes
{
    Helmet,
    Speed
}