using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Could Not Initialise App");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }
    
    #region Login/Logout

    private void FacebookLogin()
    {
        //var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(callback:OnLogIn);
    }

    private void OnLogIn(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var token = AccessToken.CurrentAccessToken;
            Debug.Log("Facebook UserID: " + token.UserId);
        }
        else
        {
            Debug.Log("Cancelled Login");
        }
    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }
    
    #endregion

    public void FacebookShare()
    {
        FacebookLogin();
        FB.ShareLink(contentTitle:"Check Out My New High Score in Chaser:",
            contentURL:new System.Uri("https://google.com"),
            contentDescription:"Check Out Chaser and Challenge Yourself to Score Better",
            callback:OnShare);
    }

    private void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Share Score Error" + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
            Debug.Log("Share Score Success");
    }
}
