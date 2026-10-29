using UnityEngine;

public class HabitatPanelSwapper : MonoBehaviour
{
    public GameObject sliderPanel, enrichmentPanel;

    public void EnrichmentToSlider()
    {
        enrichmentPanel.SetActive(false);
        sliderPanel.SetActive(true);
    }

    public void SliderToEnrichment()
    {
        enrichmentPanel.SetActive(true);
        sliderPanel.SetActive(false);
    }
}
