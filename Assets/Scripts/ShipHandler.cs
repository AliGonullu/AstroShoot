using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] MasteryRequirements;
    [SerializeField] private TextMeshProUGUI mastery_text;
    private readonly int[] mastery_req_for_ships = { 0, 5, 15, 30, 50 };
    private Player player;
    private SceneMNG sceneMNG;
    private Texts texts;


    private void Awake()
    {
        enabled = (SceneManager.GetActiveScene().name == "ShipMenu");
    }

    private void Start()
    {
        sceneMNG = new();
        texts = new();
        player = new();

        for (int i = 0; i < mastery_req_for_ships.Length; i++)
        {
            MasteryRequirements[i].text = texts.MasteryTextHandling(mastery_req_for_ships[i].ToString());
        }
        mastery_text.text = texts.MasteryTextHandling(sceneMNG.GetMastery().ToString());
    }

    public void ActivateShip1()
    {
        ActivationOfShips(0);
    }

    public void ActivateShip2()
    {
        ActivationOfShips(1);
    }

    public void ActivateShip3()
    {
        ActivationOfShips(2);
    }

    public void ActivateShip4()
    {
        ActivationOfShips(3);
    }

    public void ActivateShip5()
    {
        ActivationOfShips(4);
    }

    private void ActivationOfShips(int ship_idx)
    {
        if (mastery_req_for_ships[ship_idx] <= sceneMNG.GetMastery())
        {
            player.SetShipNo(ship_idx + 1);
            sceneMNG.SetMastery(sceneMNG.GetMastery() - mastery_req_for_ships[ship_idx]);
            Player.engine_effect_idx = Mathf.FloorToInt(ship_idx / 2);
        }
    }

}
