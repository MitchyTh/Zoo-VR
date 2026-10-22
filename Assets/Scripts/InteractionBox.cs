using UnityEngine;
using UnityEngine.Events;

public class InteractionBox : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onInteract;

    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        onEnter.Invoke();
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
