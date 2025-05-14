using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextGameOver;
    public Button restartButton;
    public GameObject gameOverCanvas;
    private int score;
    private float timer;
    private PlayerMovement playerMovementScript;

    void Start()
    {
        gameOverCanvas.SetActive(false);
        playerMovementScript = GameObject.Find("Gordo").GetComponent<PlayerMovement>();
        score = 0;
        UpdateScoreText();

        restartButton.onClick.AddListener(RestartGame);
    }

    void FixedUpdate()
    {
        if (!playerMovementScript.gameOver)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= 1f)
            {
                score++;
                timer = 0;
                UpdateScoreText();
            }
        }
        else if (!gameOverCanvas.activeSelf)
        {
            ShowGameOverScreen();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
        scoreTextGameOver.text = $"Your Score: {score}";
    }

    private void ShowGameOverScreen()
    {
        gameOverCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }
}
