using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderValueText;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        UpdateValue();
    }

    public void UpdateValue()
    {
        float percent = slider.value * 100f;
        sliderValueText.text = percent.ToString("0.00") + "%";
    }
}
