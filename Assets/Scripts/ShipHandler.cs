using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] MasteryRequirements;
    [SerializeField] private TextMeshProUGUI mastery_text;
    private readonly int[] mastery_req_for_ships = { 0, 5, 15, 30, 50 };

    private void Awake()
    {
        enabled = (SceneManager.GetActiveScene().name == "ShipMenu");
    }

    private void Start()
    {

        for (int i = 0; i < mastery_req_for_ships.Length; i++)
        {
            MasteryRequirements[i].text = Texts.MasteryTextHandling(mastery_req_for_ships[i].ToString());
        }
        mastery_text.text = Texts.MasteryTextHandling(SceneMNG.mastery_lvl.ToString());
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
        if (mastery_req_for_ships[ship_idx] <= SceneMNG.mastery_lvl)
        {
            Variables.ship_no = ship_idx + 1;
            SceneMNG.mastery_lvl -= mastery_req_for_ships[ship_idx];
            mastery_text.text = "Mastery : " + SceneMNG.mastery_lvl.ToString();
            Variables.ship_engine_effect_idx = Mathf.FloorToInt(ship_idx / 2);
        }
    }

}
