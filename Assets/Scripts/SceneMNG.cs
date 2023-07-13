using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneMNG : MonoBehaviour
{
    private SoundEffect _soundEffect;

    [SerializeField] private TextMeshProUGUI target_score_text, start_score_text, mastery_text;
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private TextMeshProUGUI final_score_text;

    public static int mastery_lvl = 0;

    private static int target_score = first_target_score, start_score = 0;
    private const int first_target_score = 10;

    private void Start()
    {
        _soundEffect = GetComponentInChildren<SoundEffect>();
        
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            if (mastery_text != null && target_score_text != null && start_score_text != null)
            {
                final_score_text.text = Variables.score.ToString();
                ChangeMastery(mastery_lvl);
                ChangeTargetScore(target_score);
                ChangeStartScore(Variables.score);
            }
                
        }
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            if(mastery_text != null && target_score_text != null && start_score_text != null)
            {
                ChangeMastery(mastery_lvl);
                ChangeTargetScore(target_score);
                ChangeStartScore(Variables.score);
            }
            
        }
    }

    
    void FixedUpdate()
    {
        if ((SceneManager.GetActiveScene().name == "GameOver" || SceneManager.GetActiveScene().name == "Menu") && scoreSlider != null && mastery_text != null)
        {
            if (Mathf.Abs(scoreSlider.value - Variables.score) > 0.4f)
            {
                scoreSlider.value = Mathf.Lerp(scoreSlider.value, Variables.score, 0.8f * Time.deltaTime);
                ChangeStartScore(Mathf.RoundToInt(scoreSlider.value));
            }
            
            if(scoreSlider.value == scoreSlider.maxValue)
            {
                _soundEffect.ExtraSoundEffect();
                scoreSlider.value = 0;
                ChangeMastery(mastery_lvl + 1);
                ChangeTargetScore(target_score + 10);
                ChangeStartScore(0);
            }    
        }
    }

    public static void ResetScoreSlider()
    {
        start_score = 0;
        Variables.score = 0;
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
        Variables.score = 0;
        ResetScoreSlider();
        SceneManager.LoadScene("Game");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OpenShipMenu()
    {
        SceneManager.LoadScene("ShipMenu");
    }

    public void OpenModifications()
    {
        SceneManager.LoadScene("Modifications");
    }

    public void OpenPerks()
    {
        SceneManager.LoadScene("Perks");
    }

    public void OpenGameOverMenu()
    {
        Variables.ResetShipHealth();
        Variables.ResetBallHealth();
        PerkHandler.GameOverScene();
        Variables.ResetObstacleSpeed();
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
