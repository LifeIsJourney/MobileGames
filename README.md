# Mobile Games CA
The objective of the game is to avoid letting the chaser (sphere) catch you while moving the player (cube). Score is added for each second that you remain alive. The score is saved using PlayPrefs and the highscore will be updated when it is acquired.

# Ads
Gems can be earned through watching ads, the application implements AdMob. The AdManager Script file handles the implementation. Below shows the button which can be pressed to get Gem rewards.
![alt text](https://i.imgur.com/qw3hKws.png)

# Achievements
The game features two achievements which were created on Google Play Console. The first achievement is for reaching 10 score, and the second is for reaching 50 score. Below shows the button to view the Achievements UI. The Google Services implementation is handled in the PlayGames Script file.

![alt text](https://i.imgur.com/zO4fLg0.png)

# Leaderboards
When a new high score is reached, it will be automatically added to the games Leaderboards. By pressing the Leaderboards button, the standings will be shown.

![alt text](https://i.imgur.com/jeIZ1ql.png)

# In App Purchases
In app purchases were enabled in the project services and the products were created on the google play console. The product created for this app was a €1.99 purchase for 50 gems. Pressing Purchase Gems will present the google play payments UI to complete purchase. The IAPManager Script shows the implementation.

![alt text](https://i.imgur.com/8pEUXzz.png)

# Social Media Sharing
The application contains a simple method to sharing to Twitter which uses the OpenURL method contained in the ShareSocialScript Script. Sharing to Facebook is implemented using the Facebook SDK which is implemented in the Facebook Manager Script.

![alt text](https://i.imgur.com/h4JLzg2.png)
![alt text](https://i.imgur.com/BIT49V5.png)
