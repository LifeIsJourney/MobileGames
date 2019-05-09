using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    [SerializeField] private Text gemText;
    
    private static int _gems = 0;
    private static bool _hasReward;

    public static void AddGems(int reward)
    {
        _gems += reward;
        _hasReward = true;
    }

    public void Update()
    {
        if (_hasReward)
        {
            gemText.text = "Total Gems: " + _gems;
            _hasReward = false;
        }
    }
}