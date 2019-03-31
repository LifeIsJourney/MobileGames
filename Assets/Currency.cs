using UnityEngine;

public class Currency : MonoBehaviour
{
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
            var gemText = GetComponent<UnityEngine.UI.Text>();
            gemText.text = "Total Gems: " + _gems.ToString();
            _hasReward = false;
        }
    }
}