using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;


public class AdsManager : MonoBehaviour, IUnityAdsListener
{

    private string gameId = "4435281";
    //string placement = "Rewarded_Android";

    public bool testMode = true;

    void Start()
    {
        //This is for use OnunityAdsReady Below 
        Advertisement.AddListener(this);

        Advertisement.Initialize(gameId, testMode);

        //  while (!Advertisement.IsReady(placement))
        //      yield return null;
        //  Advertisement.Show(placement);

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void ShowAds(string placement)
     {
         Advertisement.Show(placement);
     }
   
    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {

        }
    }

    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }
    //public bool AdsHealPlayer;

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //When ads finish
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("REBORN!!!!!!!! KAO REBORN HERE");
           // AdsHealPlayer = true;
            SceneManager.LoadScene("Scenes/A_Test");

        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log("AdsFailYouDie_SHOW MENU SHOW SCORE?");
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("AdsSKIP_ SHOW MENU SHOW SCORE");

        }
    }




}
