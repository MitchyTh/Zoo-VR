using UnityEngine;
using UnityEngine.Events;

public class InteractBubble : MonoBehaviour
{
    [SerializeField] private GameObject iconObject;

    [SerializeField] private UnityEvent interactEvent;

    [SerializeField] private float iconOscillateSpeed = 1.0f;
    [SerializeField] private float iconOscillateAmplitude = 0.001f;

    void Start()
    {
        
    }

    void Update()
    {
        float oscillateY = iconOscillateAmplitude * Mathf.Sin(Time.time * iconOscillateSpeed);
        Vector3 worldUpOffset = Vector3.up * oscillateY;
        Vector3 localOffset = iconObject.transform.parent.InverseTransformDirection(worldUpOffset);
        iconObject.transform.localPosition = localOffset;

        // Billboard iconObject
        Vector3 iconRotation = Camera.main.transform.rotation.eulerAngles;
        iconRotation.z = 0;
        iconObject.transform.eulerAngles = iconRotation;
    }

    public void Interact()
    {
        // Pop the bubble
        // TODO: particle effect, respawning after a delay
        gameObject.SetActive(false);

        // Trigger the event
        interactEvent.Invoke();
    }
}
