using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareSocialScript : MonoBehaviour
{
    #region Facebook Variables

    private string appID = "1039972606390912";
    private string link = "https://www.facebook.com/feed?";
    private string picture = "https://cdn2.iconfinder.com/data/icons/flat-game-ui-buttons-icons-1/512/19-512.png";
    private string caption = "Check Out My New High Score: ";
    private string description = "Enjoy fun free games. Challenge Yourself & Share With Friends";

    #endregion

    public void ShareScoreFacebook()
    {
        Debug.Log("Sharing High Score: " + PlayerPrefs.GetInt("HighScore", 0) + " to Facebook");
        Application.OpenURL(link + "app_id=" + appID + "&link=" + link + "&picture=" + picture + "&caption=" + caption + PlayerPrefs.GetInt("HighScore", 0) + "&description=" + description);
    }
}
