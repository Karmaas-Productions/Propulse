using DroneController.Physics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneMovement : DroneMovementScript
{
    private bool[] isKeysPressed = new bool[8];


    public override void Awake()
    {
        base.Awake(); //I would suggest you to put code below this line
    }

    public override void Start()
    {
        base.Start(); //I would suggest you to put code below this line
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate(); //I would suggest you to put code below this line
    }

    public override void Update()
    {
        base.Update(); //I would suggest you to put code below this line

        SceneChangeOnClick();
        FlipDroneOnClick();

        CheckKey(KeyCode.W, 0, OnWKeyPress, OnWKeyRelease);
        CheckKey(KeyCode.A, 1, OnAKeyPress, OnAKeyRelease);
        CheckKey(KeyCode.S, 2, OnSKeyPress, OnSKeyRelease);
        CheckKey(KeyCode.D, 3, OnDKeyPress, OnDKeyRelease);
        CheckKey(KeyCode.I, 4, OnIKeyPress, OnIKeyRelease);
        CheckKey(KeyCode.J, 5, OnJKeyPress, OnJKeyRelease);
        CheckKey(KeyCode.K, 6, OnKKeyPress, OnKKeyRelease);
        CheckKey(KeyCode.L, 7, OnLKeyPress, OnLKeyRelease);
    }

    //#################################################################################################################################################################################
    //My scripts...
    //#################################################################################################################################################################################

    void CheckKey(KeyCode key, int index, System.Action onPress, System.Action onRelease)
    {
        // Check if the key is being held down
        if (Input.GetKey(key))
        {
            // Call the press function when the key is pressed
            if (!isKeysPressed[index])
            {
                onPress?.Invoke();
                isKeysPressed[index] = true;
            }
        }
        else
        {
            if (isKeysPressed[index])
            {
                onRelease?.Invoke();
                isKeysPressed[index] = false;
            }
        }
    }

    void OnWKeyPress()
    {
        Debug.Log("Works");
        CustomFeed_pitch = 0.65f;
    }

    void OnAKeyPress()
    {
        Debug.Log("Works");
        CustomFeed_yaw = 0.65f;
    }

    void OnSKeyPress()
    {
        Debug.Log("Works");
        CustomFeed_pitch = -0.65f;
    }

    void OnDKeyPress()
    {
        Debug.Log("Works");
        CustomFeed_yaw = -0.65f;
    }

    void OnIKeyPress()
    {
        Debug.Log("Works");
        CustomFeed_throttle = 0.65f;
    }

    void OnJKeyPress()
    {
        Debug.Log("Works");
        CustomFeed_roll = -0.65f;
    }

    void OnKKeyPress()
    {
        
    }

    void OnLKeyPress()
    {
        Debug.Log("Works");
        CustomFeed_roll = 0.65f;
    }

    void OnWKeyRelease()
    {
        Debug.Log("Works");
        ResetPitch();
    }

    void OnAKeyRelease()
    {
        Debug.Log("Works");
        ResetYaw();
    }

    void OnSKeyRelease()
    {
        Debug.Log("Works");
        ResetPitch();
    }

    void OnDKeyRelease()
    {
        Debug.Log("Works");
        ResetYaw();
    }

    void OnIKeyRelease()
    {
        Debug.Log("Works");
        ResetThrottle();
    }

    void OnJKeyRelease()
    {
        Debug.Log("Works");
        ResetRoll();
    }

    void OnKKeyRelease()
    {
        
    }

    void OnLKeyRelease()
    {
        Debug.Log("Works");
        ResetRoll();
    }

    private void SceneChangeOnClick()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FlipDroneOnClick()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) FlipDrone();
    }


}
