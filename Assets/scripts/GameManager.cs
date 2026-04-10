using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text   scoreTxt;

    private static GameManager instance;
    private int score;

    void Awake()
    {
        instance = this;
        gameOverPanel.SetActive(false);
        UpdateScoreUI();
    }

    public static void AddScore(int amount)
    {
        instance.score += amount;
        instance.UpdateScoreUI();
    }

    public static void ShowGameOver() 
    { 
        instance.gameOverPanel.SetActive(true); Time.timeScale = 0f;
    }

    // Assign to Play button OnClick in Inspector
    public void RestartGame()
    { 
        Time.timeScale = 1f; SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateScoreUI() 
    {
        scoreTxt.text = score.ToString();
    }
}
