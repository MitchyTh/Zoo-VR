using UnityEngine;
using System;
using UnityEngine.UI;

public enum EnrichmentType
{
    None,
    Ball,
    Box,
    ScentedPost,
    Mirror,
    IceBlock,

}

public class HabitatScript : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float idealShelter;
    [Range(0f, 1f)] [SerializeField] private float idealWater;
    [Range(0f, 1f)] [SerializeField] private float idealFoliage;
    [Range(0f, 1f)] [SerializeField] private float idealTemp;
    [SerializeField] private EnrichmentType idealEnrichment;

    [SerializeField] private Slider shelterSlider;
    [SerializeField] private Slider waterSlider;
    [SerializeField] private Slider foliageSlider;
    [SerializeField] private Slider tempSlider;

    // Real Habitat Values
    private float shelterValue;
    private float waterValue;
    private float foliageValue;
    private float tempValue;
    private EnrichmentType enrichmentValue = EnrichmentType.None;

    private float welfareScore;  // Overall welfare score (lower is better right now)

    private void UpdateSliderValues()
    {
        shelterValue = shelterSlider.value;
        waterValue = waterSlider.value;
        foliageValue = foliageSlider.value;
        tempValue = tempSlider.value;
    }

    public void RecalculateScore()
    {
        UpdateSliderValues();

        float shelterScore = Math.Abs(shelterValue - idealShelter);
        float foodScore = Math.Abs(foliageValue - idealFoliage);
        float waterScore = Math.Abs(waterValue - idealWater);
        float tempScore = Math.Abs(tempValue - idealTemp);
        float enrichmentScore = 0;

        if(idealEnrichment == enrichmentValue)
        {
            enrichmentScore = 0;
        }
        else
        {
            enrichmentScore = 1;
        }

        welfareScore = shelterScore + foodScore + waterScore + tempScore + enrichmentScore;
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


    //Wrappers for each enrichment item because Unity doesn't like Enums in the inspector
    public void SelectEnrichmentItem(EnrichmentType enrichment)
    {
        switch (enrichment)
        {
            case EnrichmentType.Ball:
                enrichmentValue = EnrichmentType.Ball;
                break;
            case EnrichmentType.Box:
                enrichmentValue = EnrichmentType.Box;
                break;
            case EnrichmentType.ScentedPost:
                enrichmentValue = EnrichmentType.ScentedPost;
                break;
            case EnrichmentType.Mirror:
                enrichmentValue = EnrichmentType.Mirror;
                break;
            case EnrichmentType.IceBlock:
                enrichmentValue = EnrichmentType.IceBlock;
                break;
            default:
                enrichmentValue = EnrichmentType.None;
                break;
        }
    }

    public void SelectBall()
    {
        SelectEnrichmentItem(EnrichmentType.Ball);
    } 
    public void SelectBox()  
    {
        SelectEnrichmentItem(EnrichmentType.Box);
    }
    public void SelectScentedPost()
    {
        SelectEnrichmentItem(EnrichmentType.ScentedPost);
    }
    public void SelectMirror()
    {
        SelectEnrichmentItem(EnrichmentType.Mirror);
    }
    public void SelectIceBlock()
    {
        SelectEnrichmentItem(EnrichmentType.IceBlock);
    }
    public void SelectNone()
    {
        SelectEnrichmentItem(EnrichmentType.None);
    }
}
