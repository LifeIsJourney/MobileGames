using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    [SerializeField] private Text gemText;
    
    private static int _earnedGems;
    private static int _currentGems;
    private static bool _hasReward;

    private void Start()
    {
        _currentGems = PlayerPrefs.GetInt("Gems", 0);
        gemText.text = "Total Gems: " + _currentGems;
    }

    public static void AddGems(int reward)
    {
        _earnedGems += reward;
        _hasReward = true;
    }
    
    public void Update()
    {
        if (_hasReward)
        {
            var newTotal = _currentGems + _earnedGems;
            PlayerPrefs.SetInt("Gems", newTotal);
            
            gemText.text = "Total Gems: " + PlayerPrefs.GetInt("Gems", 0);
            _hasReward = false;
        }
    }
}