using UnityEngine;

public class Texts : MonoBehaviour
{
    public string insufficient_materials = "Not Enough Materials", insufficient_mastery = "Not Enough Mastery", purchased = "Purchased";

    public string MaterialTextHandling(string _value)
    {
        if (LanguageOption.languageSliderValue == 0)
            return "Materials : " + _value;
        else
            return "Materyaller : " + _value;
    }
    public string MasteryTextHandling(string _value)
    {
        if (LanguageOption.languageSliderValue == 0)
            return "Mastery : " + _value;
        else
            return "Ustalýk : " + _value;
    }
}
