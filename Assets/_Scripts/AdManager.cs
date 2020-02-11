using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string playStorID = "3464973";
    private string appStorID = "3464972";

    private string interstitialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";

    public bool isTargetPlayStore;
    public bool isTestAd;


    private void Start()
    {
        Advertisement.AddListener(this);
        InitializeAd();
    }

    private void InitializeAd()
    {
        if(isTargetPlayStore) { Advertisement.Initialize(playStorID, isTestAd); return; }
        Advertisement.Initialize(appStorID, isTestAd);
    }

    public void PlayInterstitialAd()
    {
        if(!Advertisement.IsReady(interstitialAd)) { return; }
        if (AdTimer.Instance == null) { return;  }
        if (!AdTimer.Instance.canShowAd) { return; }
        Advertisement.Show(interstitialAd);
    }

    public void PlayRewardedVideoAd()
    {
        if(!Advertisement.IsReady(rewardedVideoAd)) { return; }
        Advertisement.Show(rewardedVideoAd);
        
    }

    void IUnityAdsListener.OnUnityAdsReady(string placementId){}
    void IUnityAdsListener.OnUnityAdsDidError(string message){}
    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    {
        //mute all game audio
    }
    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                AdTimer.Instance.resetTimer();
                FindObjectOfType<MenuController>().Play();
                break;
            case ShowResult.Finished:
                if(placementId ==  rewardedVideoAd)
                    FindObjectOfType<GameController>().AdPlayedContinue();
                else
                {
                    AdTimer.Instance.resetTimer();
                    FindObjectOfType<MenuController>().Play();
                }
                break;
        }
    }



}
