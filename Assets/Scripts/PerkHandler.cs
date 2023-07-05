using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] materialsRequirementsTexts;
    [SerializeField] private TextMeshProUGUI materialsText;
    [SerializeField] private GameObject fixTheBall, plusTenScore;
    [SerializeField] private Button fixTheBallButton, plusTenScoreButton;
    private Texts texts;

    private static bool fixTheBallEnabled = false, plusTenScoreEnabled = false;

    public bool GetFixTheBallEnabled() { return fixTheBallEnabled; }
    public bool GetPlusTenScoreEnabled() { return plusTenScoreEnabled; }

    public static float materials = 0;
    private readonly static float[] material_requirements = { 1, 1, 50};

    private void Start()
    {
        texts = new();
        HandleButtonActivation();
        MaterialChanged(materials);
        materialsRequirementsTexts[0].text = texts.MaterialTextHandling(material_requirements[0].ToString());
        materialsRequirementsTexts[1].text = texts.MaterialTextHandling(material_requirements[1].ToString());
        materialsRequirementsTexts[2].text = texts.MaterialTextHandling(material_requirements[2].ToString());
    }

    public void ResetCosts()
    {
        if (material_requirements[2] <= materials)
        {
            material_requirements[0] = 1;
            material_requirements[1] = 1;
            materialsRequirementsTexts[0].text = texts.MaterialTextHandling(material_requirements[0].ToString());
            materialsRequirementsTexts[1].text = texts.MaterialTextHandling(material_requirements[1].ToString());
        }
        else
        {
            materialsRequirementsTexts[2].text = texts.insufficient_materials;
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
            materialsRequirementsTexts[0].text = texts.insufficient_materials;
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
            materialsRequirementsTexts[1].text = texts.insufficient_materials;
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
            fixTheBallButton.enabled = !fixTheBallEnabled;
            if (fixTheBallEnabled)
                materialsRequirementsTexts[0].text = texts.purchased;
        }
        if (plusTenScoreButton != null)
        {
            plusTenScoreButton.enabled = !plusTenScoreEnabled;
            if (plusTenScoreEnabled)
                materialsRequirementsTexts[1].text = texts.purchased;
        }
    }

}
