using UnityEngine;
using System;
using UnityEngine.UI;

public class HabitatScript : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float idealShelter;
    [Range(0f, 1f)] [SerializeField] private float idealWater;
    [Range(0f, 1f)] [SerializeField] private float idealFoliage;
    [Range(0f, 1f)] [SerializeField] private float idealTemp;
    [Range(0f, 1f)] [SerializeField] private float idealEnrichment;

    [SerializeField] private Slider shelterSlider;
    [SerializeField] private Slider waterSlider;
    [SerializeField] private Slider foliageSlider;
    [SerializeField] private Slider tempSlider;
    [SerializeField] private Slider enrichmentSlider;

    // Real Habitat Values
    private float shelterValue;
    private float waterValue;
    private float foliageValue;
    private float tempValue;
    private float enrichmentValue;

    private float welfareScore;  // Overall welfare score (lower is better right now)

    private void UpdateSliderValues()
    {
        shelterValue = shelterSlider.value;
        waterValue = waterSlider.value;
        foliageValue = foliageSlider.value;
        tempValue = tempSlider.value;
        enrichmentValue = enrichmentSlider.value;
    }

    public void RecalculateScore()
    {
        UpdateSliderValues();

        float shelterScore = Math.Abs(shelterValue - idealShelter);
        float foodScore = Math.Abs(foliageValue - idealFoliage);
        float waterScore = Math.Abs(waterValue - idealWater);
        float tempScore = Math.Abs(tempValue - idealTemp);
        float cleanScore = Math.Abs(enrichmentValue - idealEnrichment);

        welfareScore = shelterScore + foodScore + waterScore + tempScore + cleanScore;
    }

    private void FixedUpdate()
    {
        // This is unnecessary to do every tick, but probably fine
        RecalculateScore();
    }

    public float getWelfareScore()
    {
        return welfareScore;
    }
}
