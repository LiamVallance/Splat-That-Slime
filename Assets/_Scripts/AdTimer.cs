using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdTimer : MonoBehaviour
{
    public static AdTimer Instance {get; private set;}
    float adTimer = 120.0f;
    public bool canShowAd = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (adTimer > 0.0f)
        {
            adTimer -= Time.unscaledDeltaTime;
            if (adTimer <= 0.0f)
                timerEnded();

            Debug.Log("Timer = " + adTimer);
        }
    }

    public void timerEnded()
    {
        canShowAd = true;
        Debug.Log("canShowAs = " + canShowAd);
    }

    public void resetTimer()
    {
        canShowAd = false;
        adTimer = 120.0f;
        Debug.Log("canShowAs = " + canShowAd);
    }
}
