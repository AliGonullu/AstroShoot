using TMPro;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] LVLTexts, highscoreRequirementsTexts;
    [SerializeField] private TextMeshProUGUI highscoreText;
    public static float highscore = 0;
    private int button_index = 0;
    private readonly float[] score_limits = {20, 40, 80, 160, 320};
    private Ball ball;
    private Texts texts;

    private void Start()
    {
        ball = new();
        texts = new();
        HighscoreChanged(highscore);

        LVLTexts[0].text = Player.playerSpeedLVL.ToString();
        highscoreRequirementsTexts[0].text = texts.HighscoreTextHandling(score_limits[Player.playerSpeedLVL].ToString());

        LVLTexts[1].text = ball.GetThrowForceLVL().ToString();
        highscoreRequirementsTexts[1].text = texts.HighscoreTextHandling(score_limits[ball.GetThrowForceLVL()].ToString());
    }

    public void PlayerSpeedButtonDown()
    {
        Player.playerSpeedLVL = ButtonProcess(0, Player.playerSpeedLVL);
    }

    public void ThrowForceButtonDown()
    {
        ball.SetThrowForceLVL(ButtonProcess(1, ball.GetThrowForceLVL()));
    }

    private int ButtonProcess(int _button_idx, int _lvl)
    {
        button_index = _button_idx;
        if (highscore >= score_limits[_lvl])
        {
            _lvl += 1;
            LVLTexts[button_index].text = _lvl.ToString();
            highscoreRequirementsTexts[button_index].text = texts.HighscoreTextHandling(score_limits[_lvl].ToString());
            HighscoreChanged(0);
        }
        else
        {
            highscoreRequirementsTexts[button_index].text = texts.insufficient_highscore;
        }
        return _lvl;
    }

    private void HighscoreChanged(float _new_value)
    {
        if(highscoreText != null)
        {
            highscore = _new_value;
            highscoreText.text = "(Highscore : " + _new_value.ToString() + ")";
        }
    }

}
