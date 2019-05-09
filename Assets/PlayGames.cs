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
        PlayGamesPlatform.Activate();
        
        SignIn();
    }

    private static void SignIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    #region  Achievements

    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public void ShowAchievementsUi()
    {
        Social.ShowAchievementsUI();
    }
    
    #endregion
    
    #region Leaderboards

    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public void ShowLeaderboardUi()
    {
        Social.ShowLeaderboardUI();
    }
    
    #endregion
}
