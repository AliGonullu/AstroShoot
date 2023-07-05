using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneMNG : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI final_score_text;
    private PerkHandler perkHandler;
    private Ball ball;
    private TextMeshProUGUI target_score_text;
    private static int target_score;

    private void Start()
    {
        ball = new();
        perkHandler = new();
        


        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            final_score_text.text = ball.GetScore().ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            target_score_text = GameObject.Find("ScoreTarget").GetComponent<TextMeshProUGUI>();
            int.TryParse(target_score_text.text, out target_score);
            if(target_score <= ball.GetScore())
            {
                target_score += 10;
                target_score_text.text = target_score.ToString();
            }
        }
    }

    public void PlayGame()
    {
        ball.SetScore(0);
        SceneManager.LoadScene("Game");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OpenModifications()
    {
        SceneManager.LoadScene("Modifications");
    }

    public void OpenPerks()
    {
        perkHandler.HandleButtonActivation();
        SceneManager.LoadScene("Perks");
    }

    public void OpenGameOverMenu()
    {
        if (ball.GetScore() > LevelHandler.highscore)
        {
            LevelHandler.highscore = ball.GetScore();
        }


        ball.SetHealth(3);
        perkHandler.DisableFixTheBall();
        perkHandler.DisablePlusTen();
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
