using UnityEngine;
using System.Collections;

public class LevelEndPanelScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer panelRenderer;
    [SerializeField, Min(0f)] private float startDelay = 0.5f;
    [SerializeField, Min(0f)] private float animationDuration = 1f;
    [SerializeField] private Vector3 targetScale = Vector3.one;

    private Color targetColor;
    private Coroutine animationCoroutine;

    private void Awake()
    {
        if (panelRenderer == null)
        {
            panelRenderer = GetComponent<SpriteRenderer>();
        }

        if (panelRenderer == null)
        {
            Debug.LogError("LevelEndPanelScript requires a SpriteRenderer on this GameObject.", this);
            enabled = false;
            return;
        }

        targetColor = panelRenderer.color;
        SetAlpha(0f);
        transform.localScale = Vector3.zero;
        panelRenderer.enabled = false;
    }

    public void ShowPanel()
    {
        if (!enabled)
        {
            return;
        }

        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(AnimatePanel());
    }

    private IEnumerator AnimatePanel()
    {
        transform.localScale = Vector3.zero;
        SetAlpha(0f);
        panelRenderer.enabled = true;

        yield return new WaitForSeconds(startDelay);

        if (animationDuration <= 0f)
        {
            transform.localScale = targetScale;
            SetAlpha(targetColor.a);
            animationCoroutine = null;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / animationDuration);
            transform.localScale = Vector3.LerpUnclamped(Vector3.zero, targetScale, progress);

            float fadeProgress = Mathf.Clamp01(progress / 0.3f);
            SetAlpha(Mathf.Lerp(0f, targetColor.a, fadeProgress));
            yield return null;
        }

        transform.localScale = targetScale;
        SetAlpha(targetColor.a);
        animationCoroutine = null;
    }

    private void SetAlpha(float alpha)
    {
        Color color = targetColor;
        color.a = alpha;
        panelRenderer.color = color;
    }
}
