using TMPro;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] LVLTexts, masteryRequirementsTexts;
    [SerializeField] private TextMeshProUGUI masteryText;

    private readonly static int score_limit1 = 1, score_limit2 = 1, score_limit3 = 1;

    private readonly static int[] score_limits = { score_limit1, score_limit2, score_limit3};
    private SceneMNG sMNG;
    private Texts texts;
    private Ball ball;

    private int button_index = 0;
    
    private void Start()
    {
        sMNG = new();
        texts = new();
        ball = new();
        
        ChangeMastery(sMNG.GetMastery());

        LVLTexts[0].text = Player.playerSpeedLVL.ToString();
        masteryRequirementsTexts[0].text = "(" + texts.MasteryTextHandling(score_limits[0].ToString()) + ")";

        LVLTexts[1].text = ball.GetThrowForceLVL().ToString();
        masteryRequirementsTexts[1].text = "(" + texts.MasteryTextHandling(score_limits[1].ToString()) + ")";

        LVLTexts[2].text = ball.GetHealthLVL().ToString();
        masteryRequirementsTexts[2].text = "(" + texts.MasteryTextHandling(score_limits[2].ToString()) + ")";
    }

    public void PlayerSpeedButtonDown()
    {
        Player.playerSpeedLVL = ButtonProcess(0, Player.playerSpeedLVL);
    }

    public void ThrowForceButtonDown()
    {
        ball.SetThrowForceLVL(ButtonProcess(1, ball.GetThrowForceLVL()));
    }

    public void ExtraHealthForBall()
    {
        if(Ball.max_health > ball.GetHealthLVL())
        {
            ball.SetHealthLVL(ButtonProcess(2, ball.GetHealthLVL()));
        }
        else
        {
            masteryRequirementsTexts[2].text = "(Max. Level)";
        }
    }

    private int ButtonProcess(int _button_idx, int _lvl)
    {
        button_index = _button_idx;
        if (sMNG.GetMastery() >= score_limits[button_index])
        {
            
            ChangeMastery(sMNG.GetMastery() - score_limits[button_index]);
            _lvl += 1;
            score_limits[_button_idx] *= 2;
            sMNG.ResetScoreSlider();
            LVLTexts[button_index].text = _lvl.ToString();
            masteryRequirementsTexts[button_index].text = "(" + texts.MasteryTextHandling(score_limits[button_index].ToString()) + ")";
        }
        else
        {
            masteryRequirementsTexts[button_index].text = "(" + texts.insufficient_mastery + ")";
        }
        return _lvl;
    }

    
    private void ChangeMastery(int _new_value)
    {
        if(masteryText != null)
        {
            sMNG.SetMastery(_new_value);
            masteryText.text = "(" + texts.MasteryTextHandling(_new_value.ToString()) + ")";
        }
    }
    
}
