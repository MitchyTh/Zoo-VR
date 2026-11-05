using UnityEngine;

public class VisualController : MonoBehaviour
{
    [SerializeField] private GameObject lowState;
    [SerializeField] private GameObject mediumState;
    [SerializeField] private GameObject highState;

    public void SetVisualState(float value)
    {
        value = Mathf.Clamp01(value);

        if(value < 0.33f)
        {
            lowState.SetActive(true);
            mediumState.SetActive(false);
            highState.SetActive(false);
        }
        else if (value < 0.66f)
        {
            lowState.SetActive(false);
            mediumState.SetActive(true);
            highState.SetActive(false);
        }
        else
        {
            lowState.SetActive(false);
            mediumState.SetActive(false);
            highState.SetActive(true);
        }
    }
}
