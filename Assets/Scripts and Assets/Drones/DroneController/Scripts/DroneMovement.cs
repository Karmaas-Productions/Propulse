using DroneController.Physics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneMovement : DroneMovementScript
{
    public override void Update()
    {
        base.Update(); //I would suggest you to put code below this line

        SceneChangeOnClick();
        FlipDroneOnClick();

        CheckSceneName();
    }

    //#################################################################################################################################################################################
    //My scripts...
    //#################################################################################################################################################################################

    public GameObject camera;
    
    private void SceneChangeOnClick()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FlipDroneOnClick()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) FlipDrone();
    }

    private void CheckSceneName()
    {
        // Get the current scene's name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Compare the scene name to "Lobby"
        if (currentSceneName == "LobbyScene")
        {
            // Run your function here (replace with your desired function)
            DisableCameraInLobby();
        }
    }

    private void DisableCameraInLobby()
    {
        camera.SetActive(false);
    }
}
