using TMPro;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] LVLTexts, masteryRequirementsTexts;
    [SerializeField] private TextMeshProUGUI masteryText;

    private readonly int[] score_limits = { 1, 2, 4, 8, 16, 32, 64 };
    private readonly SceneMNG sMNG = new();
    private readonly Texts texts = new();
    private readonly Ball ball = new();
    private int button_index = 0;
    
    private void Start()
    {
        ChangeMastery(sMNG.GetMastery());

        LVLTexts[0].text = Player.playerSpeedLVL.ToString();
        masteryRequirementsTexts[0].text = texts.MasteryTextHandling(score_limits[Player.playerSpeedLVL].ToString());

        LVLTexts[1].text = ball.GetThrowForceLVL().ToString();
        masteryRequirementsTexts[1].text = texts.MasteryTextHandling(score_limits[ball.GetThrowForceLVL()].ToString());

        LVLTexts[2].text = ball.GetHealthLVL().ToString();
        masteryRequirementsTexts[2].text = texts.MasteryTextHandling(score_limits[ball.GetHealthLVL()].ToString());
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
        ball.SetHealthLVL(ButtonProcess(2, ball.GetHealthLVL()));
    }

    private int ButtonProcess(int _button_idx, int _lvl)
    {
        button_index = _button_idx;
        if (sMNG.GetMastery() >= score_limits[_lvl])
        {
            ChangeMastery(sMNG.GetMastery() - score_limits[_lvl]);
            sMNG.ResetScoreSlider();
            _lvl += 1;
            LVLTexts[button_index].text = _lvl.ToString();
            masteryRequirementsTexts[button_index].text = texts.MasteryTextHandling(score_limits[_lvl].ToString());
        }
        else
        {
            masteryRequirementsTexts[button_index].text = texts.insufficient_highscore;
        }
        return _lvl;
    }

    
    private void ChangeMastery(int _new_value)
    {
        if(masteryText != null)
        {
            sMNG.SetMastery(_new_value);
            masteryText.text = "(Mastery : " + _new_value.ToString() + ")";
        }
    }
    
}
