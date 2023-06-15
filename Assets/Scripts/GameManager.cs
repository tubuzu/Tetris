using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameStarted { get; private set; }

    public Board mainBoard;
    public Ghost ghost;

    public GameObject gameStartUI;
    public GameObject gameOverUI;
    public Text scoreText;
    public Text bestScoreText;
    public Text gameOverScoreText;

    public int score { get; private set; }
    private int scorePerLine = 1;

    private void Awake()
    {
        GameManager.instance = this;
        this.isGameStarted = false;
    }

    private void Start()
    {
        gameStartUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    public void Play()
    {
        gameStartUI.SetActive(false);
        this.isGameStarted = true;
        SetScore(0);
        mainBoard.StartGame();
    }

    public void IncreaseScore(int lines)
    {
        SetScore(this.score + scorePerLine * lines);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(4, '0');
    }

    public void GameOver()
    {
        isGameStarted = false;

        if (PlayerPrefs.GetInt("Score") < this.score)
            PlayerPrefs.SetInt("Score", score);

        bestScoreText.text = "Best score: " + PlayerPrefs.GetInt("Score").ToString();
        gameOverScoreText.text = "Score: " + this.score.ToString();

        gameOverUI.SetActive(true);
    }

    public void Retry()
    {
        ghost.Clear();
        mainBoard.SpawnPiece();
        SetScore(0);
        isGameStarted = true;
        gameOverUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
