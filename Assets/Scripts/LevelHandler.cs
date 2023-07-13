using TMPro;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] LVLTexts, masteryRequirementsTexts;
    [SerializeField] private TextMeshProUGUI masteryText;

    private readonly static int score_limit1 = 1, score_limit2 = 1, score_limit3 = 1;

    public static int[] score_limits = { score_limit1, score_limit2, score_limit3};

    private int button_index = 0;
    
    private void Start()
    {
        ChangeMastery(SceneMNG.mastery_lvl);
        
        LVLTexts[0].text = Variables.ship_speed_lvl.ToString();
        masteryRequirementsTexts[0].text = "(" + Texts.MasteryTextHandling(score_limits[0].ToString()) + ")";

        LVLTexts[1].text = Variables.throw_force_lvl.ToString();
        masteryRequirementsTexts[1].text = "(" + Texts.MasteryTextHandling(score_limits[1].ToString()) + ")";

        LVLTexts[2].text = Variables.ball_health_level.ToString();
        masteryRequirementsTexts[2].text = "(" + Texts.MasteryTextHandling(score_limits[2].ToString()) + ")";
    }

    public void PlayerSpeedButtonDown()
    {
        Variables.ship_speed_lvl = ButtonProcess(0, Variables.ship_speed_lvl);
    }

    public void ThrowForceButtonDown()
    {
        Variables.throw_force_lvl = ButtonProcess(1, Variables.throw_force_lvl);
    }

    public void ExtraHealthForBall()
    {
        
        if (Variables.ball_max_health > Variables.ball_health_level)
        {
            Variables.ball_health_level = ButtonProcess(2, Variables.ball_health_level);
        }
        else
        {
            masteryRequirementsTexts[2].text = "(Max. Level)";
        }
    }

    private int ButtonProcess(int _button_idx, int _lvl)
    {
        button_index = _button_idx;
        if (SceneMNG.mastery_lvl >= score_limits[button_index])
        {
            
            ChangeMastery(SceneMNG.mastery_lvl - score_limits[button_index]);
            _lvl += 1;
            score_limits[_button_idx] *= 2;
            SceneMNG.ResetScoreSlider();
            LVLTexts[button_index].text = _lvl.ToString();
            masteryRequirementsTexts[button_index].text = "(" + Texts.MasteryTextHandling(score_limits[button_index].ToString()) + ")";
        }
        else
        {
            masteryRequirementsTexts[button_index].text = "(" + Texts.insufficient_mastery + ")";
        }
        return _lvl;
    }

    
    private void ChangeMastery(int _new_value)
    {
        if(masteryText != null)
        {
            SceneMNG.mastery_lvl = _new_value;
            masteryText.text = "(" + Texts.MasteryTextHandling(_new_value.ToString()) + ")";
        }
    }
    
}
