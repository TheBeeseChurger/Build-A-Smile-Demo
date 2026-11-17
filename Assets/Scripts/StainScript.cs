using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class StainScript : MonoBehaviour
{
    [SerializeField, Min(1f)] private float scrubDistance = 150f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float scrubbedDistance;
    private Vector2 previousMousePosition;
    private bool isRemoved;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        previousMousePosition = Input.mousePosition;
    }

    private void OnMouseOver()
    {
        if (isRemoved)
        {
            return;
        }

        Vector2 mousePosition = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            scrubbedDistance += Vector2.Distance(previousMousePosition, mousePosition);
            float progress = Mathf.Clamp01(scrubbedDistance / scrubDistance);

            Color color = originalColor;
            color.a = originalColor.a * (1f - progress);
            spriteRenderer.color = color;

            if (progress >= 1f)
            {
                RemoveStain();
            }
        }

        previousMousePosition = mousePosition;
    }

    private void RemoveStain()
    {
        isRemoved = true;

        Transform parent = transform.parent;
        ChildScript childScript = parent != null ? parent.GetComponent<ChildScript>() : null;
        if (childScript == null)
        {
            Debug.LogError("Stains must be direct children of an object with ChildScript.", this);
        }
        else
        {
            Debug.Log("Stain removed!");
            childScript.OnStainRemoved(this);
        }

        Destroy(gameObject);
    }
}
