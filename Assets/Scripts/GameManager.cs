using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameDuration;
    [SerializeField] private TextMeshProUGUI player1ScoreDisplay;
    [SerializeField] private TextMeshProUGUI player2ScoreDisplay;
    [SerializeField] private TextMeshProUGUI timerDisplay;

    private static int _player1Score;
    public static int Player1Score
    {
        get { return _player1Score; } 
        set { _player1Score = value; }    
    }

    private static int _player2Score;
    public static int Player2Score
    {
        get { return _player2Score; }
        set { _player2Score = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        player1ScoreDisplay.text = Player1Score.ToString();
        player2ScoreDisplay.text = Player2Score.ToString();
        DisplayTimer();
        
    }

    public void DisplayTimer()
    {
        gameDuration -= Time.deltaTime;
        int minutesDisplay = (int)gameDuration / 60;
        int secondsDisplay = (int)gameDuration % 60;
        timerDisplay.text = minutesDisplay.ToString("00") + ":" + secondsDisplay.ToString("00");
    }
}
