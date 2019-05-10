using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGames : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        
        SignIn();
    }

    private static void SignIn()
    {
        Debug.Log("User Signing In");
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Authentication Successful");
                var userInfo = "Username: " + Social.localUser.userName +
                                  "\nUser ID: " + Social.localUser.id +
                                  "\nIsUnderage: " + Social.localUser.underage;
                Debug.Log(userInfo);
            }
            else
                Debug.Log("Authentication Failed");
        });
    }

    #region  Achievements

    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, success =>
        {
            Debug.Log(success ? "Achievement Unlocked" : "Error Unlocking Achievement");
        });
    }

    public void ShowAchievementsUi()
    {
        Debug.Log("Showing Achievements UI");
        Social.ShowAchievementsUI();
    }
    
    #endregion
    
    #region Leaderboards

    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success =>
        {
            Debug.Log(success ? "Achievement Unlocked" : "Error Unlocking Achievement");
        });
    }

    public void ShowLeaderboardUi()
    {
        Debug.Log("Showing Leaderboards UI");
        Social.ShowLeaderboardUI();
    }
    
    #endregion
}
