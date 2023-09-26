using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    // Get the player character
    public GameObject playerCharacter;

    // Get values for offset from player
    public float frontOffset = -0.8f;
    public float rightOffset = 0.8f;
    public float height = 1.5f;

    // Get the Dynamic Steering component
    private DynamicSteer movementControls;

    // Start is called before the first frame update
    void Start()
    {
        // Init controls
        movementControls = gameObject.GetComponent<DynamicSteer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the position offset from the player
        Vector3 targetPosition = new Vector3();
        if(playerCharacter != null)
        {
            Vector3 offset = (playerCharacter.transform.forward * frontOffset) + (playerCharacter.transform.right * rightOffset);
            targetPosition = playerCharacter.transform.position + offset + new Vector3(0, height, 0);
        }

        // Steer towards that position
        movementControls.Steer(targetPosition);
    }
}
