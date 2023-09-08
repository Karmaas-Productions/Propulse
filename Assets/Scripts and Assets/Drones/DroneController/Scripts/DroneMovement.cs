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
    }

    //#################################################################################################################################################################################
    //My scripts...
    //#################################################################################################################################################################################

    private void SceneChangeOnClick()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FlipDroneOnClick()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) FlipDrone();
    }


}
