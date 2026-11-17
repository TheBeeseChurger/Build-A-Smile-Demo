using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ChildScript : MonoBehaviour
{
    private static readonly Vector3 InitialPosition = new(0.159999996f, 0.389999986f, 0f);
    private static readonly Vector3 InitialScale = new(0.209999993f, 0.209999993f, 0f);
    private static readonly Vector3 FinalPosition = new(0.159999996f, 5.48000002f, 0f);
    private static readonly Vector3 FinalScale = new(0.519999981f, 0.519999981f, 0f);

    [SerializeField] private float startDelay = 0.5f;
    [SerializeField] private float animationDuration = 2f;
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] private UnityEvent onAllStainsRemoved = new();

    private bool allStainsRemoved;

    public void OnStainRemoved(StainScript removedStain)
    {
        if (allStainsRemoved)
        {
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            StainScript stain = transform.GetChild(i).GetComponent<StainScript>();
            if (stain != null && stain != removedStain)
            {
                return;
            }
        }

        allStainsRemoved = true;
        Debug.Log("All stains removed!");
        OnAllStainsRemoved();
    }

    public void OnAllStainsRemoved()
    {
        onAllStainsRemoved.Invoke();
    }

    private IEnumerator Start()
    {
        transform.localPosition = InitialPosition;
        transform.localScale = InitialScale;

        yield return new WaitForSeconds(startDelay);

        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / animationDuration);
            float curveProgress = animationCurve.Evaluate(progress);
            transform.localPosition = Vector3.LerpUnclamped(InitialPosition, FinalPosition, curveProgress);
            transform.localScale = Vector3.LerpUnclamped(InitialScale, FinalScale, curveProgress);
            yield return null;
        }

        transform.localPosition = FinalPosition;
        transform.localScale = FinalScale;
    }
}
