using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private Text _highScoreText;
    

    private int _score;
    private int _highScore;
    
    // Start is called before the first frame update
    private void Start()
    {
        _highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        scoreText.text = "Score: " + 0;

        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _highScoreText.text = "High Score: " + _highScore;
        InvokeRepeating(nameof(UpdateScores), 1f, 1f);
    }

    private void UpdateScores()
    {
        _score += 1;
        if (_score > _highScore)
        {
            PlayerPrefs.SetInt("HighScore", _score);
            _highScoreText.text = "High Score: " + _score;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = "Score: " + _score;
    }
}
