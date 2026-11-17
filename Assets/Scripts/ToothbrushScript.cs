using UnityEngine;

public class ToothbrushScript : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    private float cameraDepth;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("ToothbrushScript requires a SpriteRenderer on the same GameObject.", this);
            enabled = false;
            return;
        }

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("ToothbrushScript requires a camera tagged MainCamera.", this);
            enabled = false;
            return;
        }

        spriteRenderer.enabled = false;
        cameraDepth = mainCamera.WorldToScreenPoint(transform.position).z;
    }

    private void Update()
    {
        bool isHeld = Input.GetMouseButton(0);
        spriteRenderer.enabled = isHeld;
        if (!isHeld)
        {
            return;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = cameraDepth;
        transform.position = mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
