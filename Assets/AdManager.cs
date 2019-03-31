using System;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    [SerializeField] private const string GameId = "3099945";

    private void Awake()
    {
        Advertisement.Initialize(GameId, true);
    }

    public void ShowAd()
    {
        #if UNITY_EDITOR
        StartCoroutine(WaitForAd());
        #endif
        
        if (Advertisement.IsReady())
        {
            var options = new ShowOptions();
            options.resultCallback = HandleAdResult;
            
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                Debug.Log("Added 5 Gems to Account");
                break;
            case ShowResult.Skipped:
                Debug.Log("Player Skipped Ad");
                break;
            case ShowResult.Failed:
                Debug.Log("Player Failed to Launch Ad - Internet?");
                break;
        }
    }
    
    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }
}