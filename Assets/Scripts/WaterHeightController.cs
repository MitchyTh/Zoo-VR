using UnityEngine;
using UnityEngine.UI;

public class WaterHeightController : MonoBehaviour
{
    [SerializeField] private GameObject waterObject;
    [SerializeField] private Slider slider;

    [SerializeField] private float lowHeightOffset = -0.5f;
    [SerializeField] private float highHeightOffset = 0.5f;

    private Vector3 basePosition;

    private void Start()
    {
        if (waterObject == null)
        {
            Debug.LogError("No game object assigned to water object");
        }

        if (slider == null)
        {
            Debug.LogError("No game object assigned to slider");
        }

        basePosition = waterObject.transform.position;
    }

    public void UpdateHeight()
    {
        float newY = basePosition.y + Mathf.Lerp(lowHeightOffset, highHeightOffset, slider.value);
        waterObject.transform.position = new Vector3(basePosition.x, newY, basePosition.z);
    }
}
