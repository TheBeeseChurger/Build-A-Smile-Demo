using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButtonScript : ButtonScript
{
    public void OnStartButtonClick()
    {
        // Load the next scene or perform any other action when the start button is clicked
        Debug.Log("Start button clicked!");
        SceneManager.LoadScene("ToothBrushingScene");
    }
}

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float pulseSpeed = 1f;
    [SerializeField] private float pulseAmount = 0.05f;
    [SerializeField] private Vector3 hoverScale = new(1.1f, 1.1f, 1.1f);
    [SerializeField] private Vector3 pressedScale = new(0.9f, 0.9f, 0.9f);

    private Vector3 initialScale;
    private bool isHovered;
    private bool isPressed;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (isPressed)
        {
            transform.localScale = pressedScale;
        }
        else if (isHovered)
        {
            transform.localScale = hoverScale;
        }
        else
        {
            float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = initialScale * pulse;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        isPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
