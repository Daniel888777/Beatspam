using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{   public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; }
    [SerializeField]private TextMeshProUGUI totalScoreText;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CurrentScore = 0;
        totalScoreText.text = "Total Score: " + CurrentScore;
    }

    public void TransmitTotalScore() 
    { 
        //HighScorePoster.Instance.AddScore(new ScoreData { playerName = "Player", score = CurrentScore });
        ScoreData data = new ScoreData
        {
            playerName = PlayerNameManager.PlayerName, score = CurrentScore
        };

        HighScorePoster.Instance.AddScore(data);
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        totalScoreText.text = "Total Score: " + CurrentScore;
    }

    public void ResetScore()
    {
        CurrentScore = 0;
    }
}