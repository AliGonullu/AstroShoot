using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LanguageOption : MonoBehaviour
{
    private Slider language_option_slider;
    public static int languageSliderValue = 0;

    public Slider GetLanguageSlider() 
    {
        if(language_option_slider != null)
            return language_option_slider;
        return null;
    }

    void Start()
    {
        language_option_slider = GetComponentInChildren<Slider>();
        language_option_slider.onValueChanged.AddListener(delegate { SliderValueChanged(); });
    }

    private void SliderValueChanged()
    {
        languageSliderValue = (int)language_option_slider.value;
        switch (language_option_slider.value)
        {
            case 0:
                gameObject.GetComponent<TextMeshProUGUI>().text = "English";
                break;
            case 1:
                gameObject.GetComponent<TextMeshProUGUI>().text = "Turkish";
                break;
        }
    }
}
