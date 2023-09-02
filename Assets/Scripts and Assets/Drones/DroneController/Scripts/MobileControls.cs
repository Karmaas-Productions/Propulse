using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonEmulator : MonoBehaviour
{
    public GameObject androidIOSContainer; // Container for buttons on Android and iOS
    public Button wButton;
    public Button aButton;
    public Button sButton;
    public Button dButton;
    public Button iButton;
    public Button jButton;
    public Button kButton;
    public Button lButton;

    private Dictionary<KeyCode, bool> keyStates = new Dictionary<KeyCode, bool>();

    private void Start()
    {
        // Initialize the key states dictionary
        keyStates[KeyCode.W] = false;
        keyStates[KeyCode.A] = false;
        keyStates[KeyCode.S] = false;
        keyStates[KeyCode.D] = false;
        keyStates[KeyCode.I] = false;
        keyStates[KeyCode.J] = false;
        keyStates[KeyCode.K] = false;
        keyStates[KeyCode.L] = false;

        // Show/hide buttons based on platform
#if UNITY_ANDROID || UNITY_IOS
        androidIOSContainer.SetActive(true);
#else
        androidIOSContainer.SetActive(false);
#endif

        // Add onClick listeners to the buttons to simulate keyboard input when buttons are clicked
        AddButtonEvent(wButton, KeyCode.W);
        AddButtonEvent(aButton, KeyCode.A);
        AddButtonEvent(sButton, KeyCode.S);
        AddButtonEvent(dButton, KeyCode.D);
        AddButtonEvent(iButton, KeyCode.I);
        AddButtonEvent(jButton, KeyCode.J);
        AddButtonEvent(kButton, KeyCode.K);
        AddButtonEvent(lButton, KeyCode.L);
    }

    private void Update()
    {
        // Simulate key presses based on button states
        foreach (var kvp in keyStates)
        {
            if (kvp.Value)
            {
                SimulateKeyPress(kvp.Key);
            }
        }
    }

    private void AddButtonEvent(Button button, KeyCode key)
    {
        button.onClick.AddListener(() => ToggleKeyState(key));
        EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();

        // Add event trigger for pointer down (button press)
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((eventData) => ToggleKeyState(key, true));
        eventTrigger.triggers.Add(pointerDownEntry);

        // Add event trigger for pointer up (button release)
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((eventData) => ToggleKeyState(key, false));
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    private void ToggleKeyState(KeyCode key, bool state = true)
    {
        keyStates[key] = state;
    }

    private void SimulateKeyPress(KeyCode key)
    {
        // Simulate key press by setting the key as pressed
        InputSimulator.SimulateKeyDown(key);
        InputSimulator.SimulateKeyUp(key);
    }
}

public static class InputSimulator
{
    // Simulate key down by setting the key as pressed
    public static void SimulateKeyDown(KeyCode key)
    {
        Input.GetKeyDown(key);
        Input.GetKey(key);
    }

    // Simulate key up by setting the key as released
    public static void SimulateKeyUp(KeyCode key)
    {
        Input.GetKeyUp(key);
    }
}