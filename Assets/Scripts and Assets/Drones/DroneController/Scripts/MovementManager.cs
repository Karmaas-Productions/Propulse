using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Unity.Netcode;

public class MovementManager : NetworkBehaviour
{
    public GameObject UI;
    public GameObject droneGameObject;

    private void Start()
    {
        if (!IsOwner)
        {
            UI.SetActive(false);

            if (droneGameObject != null)
            {
                DroneMovement droneMovementScript = droneGameObject.GetComponent<DroneMovement>();
                if (droneMovementScript != null)
                {
                    droneMovementScript.enabled = false;
                }
            }
        }
    }
}
