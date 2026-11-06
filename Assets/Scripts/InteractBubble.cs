using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InteractBubble : MonoBehaviour
{
    [SerializeField] private GameObject bubbleObject;
    [SerializeField] private GameObject iconObject;
    [SerializeField] private TextMeshPro iconTextMeshPro;
    [SerializeField] private ParticleSystem popParticle;

    [SerializeField] private UnityEvent interactEvent;

    [SerializeField] private bool isPopped = false;

    const float ICON_OSCILLATE_SPEED = 1.0f;
    const float ICON_OSCILLATE_AMPLITUDE = 0.001f;

    const float RESPAWN_DURATION = 0.5f;

    const float ICON_ANIM_DURATION = 0.25f;

    private static Color BASE_ICON_COLOR = Color.white;
    private static Color HOVERED_ICON_COLOR = new Color(0.7f, 0.7f, 1.0f);

    private bool isRespawning = false;
    private float respawnTimer = 0f;

    private Vector3 originalBubbleScale;

    private bool isHovered = false;
    private float iconAnimTimer = 0f;

    void Start()
    {
        originalBubbleScale = bubbleObject.transform.localScale;

        if (isPopped)
        {
            // Hide the bubble
            bubbleObject.SetActive(false);
        }
    }

    void Update()
    {
        RespawnUpdate();

        AnimateIconPosition();
        AnimateIconColor();
    }

    public void Interact()
    {
        if (!isPopped)
        {
            // Mark as popped
            isPopped = true;

            // Hide the bubble
            bubbleObject.SetActive(false);

            // Play particle effect
            popParticle.Play();

            // Trigger the event
            interactEvent.Invoke();
        }
    }
    
    // Makes the bubble visible and interactable again
    public void Respawn()
    {
        isPopped = false;
        isRespawning = true;
        respawnTimer = 0;
        bubbleObject.SetActive(true);
        bubbleObject.transform.localScale = Vector3.zero;
    }

    public void OnHoverEnter()
    {
        isHovered = true;
    }

    public void OnHoverExit()
    {
        isHovered = false;
    }

    private float EaseInCubic(float x)
    {
        return x * x * x;
    }

    private void RespawnUpdate()
    {
        if (isRespawning)
        {
            respawnTimer = Mathf.MoveTowards(respawnTimer, RESPAWN_DURATION, Time.deltaTime);
            float t = respawnTimer / RESPAWN_DURATION;
            float easedT = EaseInCubic(t);
            bubbleObject.transform.localScale = Vector3.Lerp(Vector3.zero, originalBubbleScale, easedT);

            if (t >= 1.0)
            {
                isRespawning = false;
            }
        }
    }

    private void AnimateIconPosition()
    {
        if (!isPopped)
        {
            float oscillateY = ICON_OSCILLATE_AMPLITUDE * Mathf.Sin(Time.time * ICON_OSCILLATE_SPEED);
            Vector3 worldUpOffset = Vector3.up * oscillateY;
            Vector3 localOffset = iconObject.transform.parent.InverseTransformDirection(worldUpOffset);
            iconObject.transform.localPosition = localOffset;

            // Billboard iconObject
            Vector3 iconRotation = Camera.main.transform.rotation.eulerAngles;
            iconRotation.z = 0;
            iconObject.transform.eulerAngles = iconRotation;
        }
    }

    private void AnimateIconColor()
    {
        float target = isHovered ? ICON_ANIM_DURATION : 0;
        iconAnimTimer = Mathf.MoveTowards(iconAnimTimer, target, Time.deltaTime);
        float t = iconAnimTimer / ICON_ANIM_DURATION;
        Color newColor = Color.Lerp(BASE_ICON_COLOR, HOVERED_ICON_COLOR, t);
        iconTextMeshPro.color = newColor;
    }
}
