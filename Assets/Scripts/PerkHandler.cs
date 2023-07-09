//using TMPro;
//using UnityEngine.UI;
using UnityEngine;


public class PerkHandler : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI[] materialsRequirementsTexts, perk_buttons_texts;
    [SerializeField] private UnityEngine.UI.Button fixTheBallButton, plusTenScoreButton;
    [SerializeField] private GameObject fixTheBall, plusTenScore;
    [SerializeField] private TMPro.TextMeshProUGUI materialsText;
    

    private static bool fixTheBallEnabled = false, plusTenScoreEnabled = false;
    private readonly static float[] material_requirements = { 1, 1, 50 };
    public static float materials = 2;
    private Texts texts;


    public bool GetFixTheBallEnabled() { return fixTheBallEnabled; }
    public bool GetPlusTenScoreEnabled() { return plusTenScoreEnabled; }

    private void Start()
    {
        texts = new();
        HandleButtonActivation();
        MaterialChanged(materials);
        materialsRequirementsTexts[0].text = "(" + texts.MaterialTextHandling(material_requirements[0].ToString()) + ")";
        materialsRequirementsTexts[1].text = "(" + texts.MaterialTextHandling(material_requirements[1].ToString()) + ")";
        materialsRequirementsTexts[2].text = "(" + texts.MaterialTextHandling(material_requirements[2].ToString()) + ")";

        if (fixTheBallEnabled)
            materialsRequirementsTexts[0].text = texts.purchased;
        if(plusTenScoreEnabled)
            materialsRequirementsTexts[1].text = texts.purchased;

    }

    public void ResetCosts()
    {
        if (material_requirements[2] <= materials)
        {
            material_requirements[0] = 1;
            material_requirements[1] = 1;
            materialsRequirementsTexts[0].text = "(" + texts.MaterialTextHandling(material_requirements[0].ToString()) + ")";
            materialsRequirementsTexts[1].text = "(" + texts.MaterialTextHandling(material_requirements[1].ToString()) + ")";
        }
        else
        {
            materialsRequirementsTexts[2].text = "(" + texts.insufficient_materials + ")";
        }
    }

    private void MaterialChanged(float _new_value)
    {
        materials = _new_value;
        materialsText.text = texts.MaterialTextHandling(materials.ToString());
    }

    public void EnableFixTheBall()
    {
        if (fixTheBall != null && material_requirements[0] <= materials)
        {
            fixTheBallEnabled = true;
            EnablePerk(8, 0);
            material_requirements[0]++;
        }
        else
        {
            materialsRequirementsTexts[0].text = "(" + texts.insufficient_materials + ")";
        }
    }

    public void EnablePlusTenScore()
    {
        if (plusTenScore != null && material_requirements[1] <= materials)
        {
            plusTenScoreEnabled = true;
            EnablePerk(9, 1);
            material_requirements[1]++;
        }
        else
        {
            materialsRequirementsTexts[1].text = "(" + texts.insufficient_materials + ")";
        }
    }

    public void DisableFixTheBall()
    {
        fixTheBallEnabled = false;
        DisablePerk(8);
    }

    public void DisablePlusTen()
    {
        plusTenScoreEnabled = false;
        DisablePerk(9);
    }

    private void EnablePerk(int _perk_idx, int _requirement_idx)
    {
        SpawnManager.enabled_indexes.Add(_perk_idx);
        MaterialChanged(materials - material_requirements[_requirement_idx]);
        HandleButtonActivation();
    }

    private void DisablePerk(int _perk_idx)
    {
        SpawnManager.enabled_indexes.Remove(_perk_idx);
        HandleButtonActivation();
    }

    public void HandleButtonActivation()
    {
        if (fixTheBallButton != null)
        {
            fixTheBallButton.interactable = !fixTheBallEnabled;
            if (fixTheBallEnabled)
                materialsRequirementsTexts[0].text = "(" + texts.purchased  + ")";
        }
        if (plusTenScoreButton != null)
        {
            plusTenScoreButton.interactable = !plusTenScoreEnabled;
            if (plusTenScoreEnabled)
                materialsRequirementsTexts[1].text = "(" + texts.purchased + ")";
        }
    }

}
