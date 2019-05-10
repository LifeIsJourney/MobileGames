using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareSocialScript : MonoBehaviour
{
    #region Facebook Variables

    private string appID = "1039972606390912";
    private string fb_link = "https://www.facebook.com/feed?";
    private string picture = "https://cdn2.iconfinder.com/data/icons/flat-game-ui-buttons-icons-1/512/19-512.png";
    private string caption = "Check Out My New High Score on Chaser: ";
    private string description = "Enjoy fun free games. Challenge Yourself & Share With Friends";

    #endregion

    #region Twitter Variables

    private string tw_link = "http://twitter.com/intent/tweet";
    private string tw_language = "en";
    private string tw_text = "Check Out My New High Score on Chaser: ";

    #endregion

    public void ShareScoreFacebook()
    {
        Debug.Log("Sharing High Score: " + PlayerPrefs.GetInt("HighScore", 0) + " to Facebook");
        Application.OpenURL(fb_link + "app_id=" + appID + "&link=" + fb_link + "&picture=" + picture + "&caption=" + caption + PlayerPrefs.GetInt("HighScore", 0) + "&description=" + description);
    }

    public void ShareScoreTwitter()
    {
        Debug.Log("Sharing High Score: " + PlayerPrefs.GetInt("HighScore", 0) + " to Twitter");
        Application.OpenURL(tw_link + "?text=" + WWW.EscapeURL(tw_text) + PlayerPrefs.GetInt("HighScore", 0) + "&amp;lang=" + WWW.EscapeURL(tw_language));
    }
}
