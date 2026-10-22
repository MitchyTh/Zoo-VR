using UnityEngine;

/// <summary>
/// Makes this GameObject (e.g. a quad) always face the XR camera.
/// Attach to any object you want to billboard toward the user.
/// </summary>
[DisallowMultipleComponent]
public class Billboard : MonoBehaviour
{
    [Tooltip("Leave empty to auto-find the XR Origin's main camera at runtime.")]
    [SerializeField] private Transform targetCamera;

    [Tooltip("If true, only rotates around the Y axis (good for standing signs/avatars). " +
             "If false, fully faces the camera on all axes.")]
    [SerializeField] private bool yAxisOnly = true;

    private void Start()
    {
        if (targetCamera == null)
        {
            // Works with XR Origin: Camera.main picks up the tagged "MainCamera"
            // under the XR Origin's Camera Offset by default.
            if (Camera.main != null)
                targetCamera = Camera.main.transform;
            else
                Debug.LogWarning($"{name}: Billboard couldn't find a camera to face. " +
                                  "Assign one manually in the Inspector.");
        }
    }

    private void LateUpdate()
    {
        if (targetCamera == null) return;

        if (yAxisOnly)
        {
            Vector3 direction = targetCamera.position - transform.position;
            direction.y = 0f; // ignore height difference
            if (direction.sqrMagnitude < 0.0001f) return;

            transform.rotation = Quaternion.LookRotation(-direction, Vector3.up);
        }
        else
        {
            // Full billboard: quad's forward faces away from camera,
            // so we look at the camera then flip 180°.
            transform.rotation = Quaternion.LookRotation(
                transform.position - targetCamera.position, targetCamera.up);
        }
    }
}