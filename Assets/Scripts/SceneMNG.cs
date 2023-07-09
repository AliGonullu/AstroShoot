//using TMPro;
//using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMNG : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI target_score_text, start_score_text, mastery_text;
    [SerializeField] private UnityEngine.UI.Slider scoreSlider;
    [SerializeField] private TMPro.TextMeshProUGUI final_score_text;
    
    private static int target_score = first_target_score, mastery_lvl = 100, start_score = 0;
    private const int first_target_score = 10;
    private readonly PerkHandler perkHandler = new();
    private readonly Ball ball = new();
    private readonly Player player = new();


    public int GetMastery()
    {
        return mastery_lvl;
    }

    public void SetMastery(int _new_value)
    {
        mastery_lvl = _new_value;
    }

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameOver")
        {
            final_score_text.text = Ball.score.ToString();
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Menu")
        {
            ChangeMastery(mastery_lvl);
            ChangeTargetScore(target_score);
            ChangeStartScore(Ball.score);
        }
    }

    
    void FixedUpdate()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Menu")
        {
            if (Mathf.Abs(scoreSlider.value - Ball.score) > 0.4f)
            {
                scoreSlider.value = Mathf.Lerp(scoreSlider.value, Ball.score, 0.8f * Time.deltaTime);
                ChangeStartScore(Mathf.RoundToInt(scoreSlider.value));
            }
            
            if(scoreSlider.value == scoreSlider.maxValue)
            {
                scoreSlider.value = 0;
                ChangeMastery(mastery_lvl + 1);
                ChangeTargetScore(target_score + 10);
                ChangeStartScore(0);
            }

            
        }
    }

    public void ResetScoreSlider()
    {
        start_score = 0;
        Ball.score = 0;
        target_score = first_target_score;
    }


    private void ChangeMastery(int _new_value)
    {
        mastery_lvl = _new_value;
        mastery_text.text = "Mastery : " + mastery_lvl.ToString();
    }

    private void ChangeStartScore(int _new_value)
    {
        start_score = _new_value;
        start_score_text.text = start_score.ToString();
    }

    private void ChangeTargetScore(int _new_value)
    {
        target_score = _new_value;
        scoreSlider.maxValue = target_score;
        target_score_text.text = target_score.ToString();
    }


    public void PlayGame()
    {
        Ball.score = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OpenMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void OpenShipMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ShipMenu");
    }

    public void OpenModifications()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Modifications");
    }

    public void OpenPerks()
    {
        perkHandler.HandleButtonActivation();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Perks");
    }

    public void OpenGameOverMenu()
    {
        player.SetHealth(Player.first_health);
        ball.SetHealth(Ball.first_health);
        perkHandler.DisableFixTheBall();
        perkHandler.DisablePlusTen();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
