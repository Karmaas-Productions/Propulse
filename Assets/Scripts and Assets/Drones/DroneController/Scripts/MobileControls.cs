using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileControls : MonoBehaviour
{
    public GameObject buttonsContainer; // Assign the container for the buttons in the Inspector
    public Button wButton;
    public Button aButton;
    public Button sButton;
    public Button dButton;
    public Button iButton;
    public Button jButton;
    public Button kButton;
    public Button lButton;

    private bool wButtonPressed;
    private bool aButtonPressed;
    private bool sButtonPressed;
    private bool dButtonPressed;
    private bool iButtonPressed;
    private bool jButtonPressed;
    private bool kButtonPressed;
    private bool lButtonPressed;

    private void Start()
    {
        // Enable or disable the button container based on platform
        buttonsContainer.SetActive(IsMobilePlatform());

        // Add onClick listeners to the buttons to simulate keyboard input when buttons are clicked
        AddClickEvent(wButton, () => StartKeyPress(KeyCode.W, () => wButtonPressed = true));
        AddClickEvent(aButton, () => StartKeyPress(KeyCode.A, () => aButtonPressed = true));
        AddClickEvent(sButton, () => StartKeyPress(KeyCode.S, () => sButtonPressed = true));
        AddClickEvent(dButton, () => StartKeyPress(KeyCode.D, () => dButtonPressed = true));
        AddClickEvent(iButton, () => StartKeyPress(KeyCode.I, () => iButtonPressed = true));
        AddClickEvent(jButton, () => StartKeyPress(KeyCode.J, () => jButtonPressed = true));
        AddClickEvent(kButton, () => StartKeyPress(KeyCode.K, () => kButtonPressed = true));
        AddClickEvent(lButton, () => StartKeyPress(KeyCode.L, () => lButtonPressed = true));
    }

    private bool IsMobilePlatform()
    {
        // Check if the current platform is Android or iOS
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }

    private void StartKeyPress(KeyCode key, System.Action keyDownAction)
    {
        keyDownAction.Invoke();
        InvokeRepeating("SimulateKeyPress", 0f, 0.1f); // Call SimulateKeyPress every 0.1 seconds while button is held
    }

    private void StopKeyPress(System.Action keyUpAction)
    {
        keyUpAction.Invoke();
        CancelInvoke("SimulateKeyPress"); // Stop calling SimulateKeyPress
    }

    private void SimulateKeyPress()
    {
        // Simulate key press based on button states
        if (wButtonPressed) InputSimulator.SimulateKeyPress(KeyCode.W);
        if (aButtonPressed) InputSimulator.SimulateKeyPress(KeyCode.A);
        if (sButtonPressed) InputSimulator.SimulateKeyPress(KeyCode.S);
        if (dButtonPressed) InputSimulator.SimulateKeyPress(KeyCode.D);
        if (iButtonPressed) InputSimulator.SimulateKeyPress(KeyCode.I);
        if (jButtonPressed) InputSimulator.SimulateKeyPress(KeyCode.J);
        if (kButtonPressed) InputSimulator.SimulateKeyPress(KeyCode.K);
        if (lButtonPressed) InputSimulator.SimulateKeyPress(KeyCode.L);
    }

    private void AddClickEvent(Button button, System.Action clickAction)
    {
        // Create an event trigger component if it doesn't exist
        EventTrigger eventTrigger = button.gameObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = button.gameObject.AddComponent<EventTrigger>();
        }

        // Create an entry for PointerDown event
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((eventData) => clickAction.Invoke());

        // Add the entry to the event trigger component
        eventTrigger.triggers.Add(pointerDownEntry);
    }
}

public static class InputSimulator
{
    // Simulate a key press by setting the desired key as pressed
    public static void SimulateKeyPress(KeyCode key)
    {
        // Set the key as pressed
        Input.GetKeyDown(key);
        Input.GetKey(key);
        Input.GetKeyUp(key);
    }
}