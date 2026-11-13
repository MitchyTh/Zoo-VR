using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InteractBubble : MonoBehaviour
{
    [SerializeField] private GameObject bubbleObject;

    [SerializeField] private GameObject billboardObject;
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject gripIconObject;

    [SerializeField] private ParticleSystem popParticle;

    [SerializeField] private UnityEvent interactEvent;

    [SerializeField] private bool isPopped = false;

    [SerializeField] private AnimationCurve respawnCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [SerializeField] private AnimationCurve hoverAnimCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    const float ICON_OSCILLATE_SPEED = 1.0f;
    const float ICON_OSCILLATE_AMPLITUDE = 0.05f;

    const float RESPAWN_DURATION = 0.5f;

    const float HOVER_ANIM_DURATION = 0.2f;
    const float HOVER_BUBBLE_SCALE = 1.1f;

    private bool isRespawning = false;
    private float respawnTimer = 0f;

    private Vector3 originalBubbleScale;

    private bool isHovered = false;
    private float hoverAnimTimer = 0f;

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
        AnimateBubbleSize();
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
        textObject.SetActive(false);
        gripIconObject.SetActive(true);
        isHovered = true;
    }

    public void OnHoverExit()
    {
        textObject.SetActive(true);
        gripIconObject.SetActive(false);
        isHovered = false;
    }

    private void RespawnUpdate()
    {
        if (isRespawning)
        {
            respawnTimer = Mathf.MoveTowards(respawnTimer, RESPAWN_DURATION, Time.deltaTime);
            float t = respawnTimer / RESPAWN_DURATION;
            float easedT = respawnCurve.Evaluate(t);
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
            Vector3 localOffset = billboardObject.transform.parent.InverseTransformDirection(worldUpOffset);
            billboardObject.transform.localPosition = localOffset;

            // Billboard
            Vector3 iconRotation = Camera.main.transform.rotation.eulerAngles;
            iconRotation.z = 0;
            billboardObject.transform.eulerAngles = iconRotation;
        }
    }

    private void AnimateBubbleSize()
    {
        if (isRespawning) return;

        if (isHovered)
        {
            hoverAnimTimer = Mathf.MoveTowards(hoverAnimTimer, HOVER_ANIM_DURATION, Time.deltaTime);
        }
        else
        {
            hoverAnimTimer = Mathf.MoveTowards(hoverAnimTimer, 0f, Time.deltaTime);
        }

        float t = hoverAnimTimer / HOVER_ANIM_DURATION;
        float easedT = hoverAnimCurve.Evaluate(t);

        // Multiply by 0.95 here because respawnCurve results in an idle scale
        // of 0.95, so we want to match that base scale
        bubbleObject.transform.localScale = Vector3.Lerp(originalBubbleScale*0.95f, originalBubbleScale*HOVER_BUBBLE_SCALE, easedT);
    }
}
