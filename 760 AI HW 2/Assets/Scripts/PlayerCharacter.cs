using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Get the animator instance
    private Animator animator;

    // Declare any animation hashes here
    int speedHash;

    // Public fields to set in editor
    public Camera camera;
    public LayerMask traversalMask;
    public GameObject targetObject;

    // Get the Dynamic Steering component to handle player movement
    private DynamicSteer movementControls;
    private Vector3 currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        // Init controls and target
        movementControls = gameObject.GetComponent<DynamicSteer>();
        currentTarget = transform.position;

        // Init animator reference and hashes
        animator = gameObject.GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse click input
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Set the current target
            currentTarget = GetTargetPoint();
        }

        // Make the player move to the target
        movementControls.Steer(currentTarget);

        // Update speed hash with current speed
        animator.SetFloat(speedHash, movementControls.GetCurrentSpeed() / movementControls.maxSpeed);

        // Continuously rotate the target
        targetObject.transform.rotation *= Quaternion.AngleAxis(0.1f, new Vector3(0.0f, 0.0f, 1.0f));
    }

    // Method called for UI drawing
    private void OnGUI()
    {
        // Draw the target sprite to where the player is going or looking to go
        if(targetObject != null)
        {
            // Create vector for slight offset, making target more visible
            Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f);

            // Moving
            if(movementControls.GetCurrentSpeed() > 0)
            {
                targetObject.transform.position = currentTarget;
            }
            else // Not Moving
            {
                targetObject.transform.position = GetTargetPoint();
            }
        }
    }

    private Vector3 GetTargetPoint()
    {
        // Trace a ray from the camera to the world
        // where the mouse is pointing
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, traversalMask))
        {
            // Return the hit point
            return hit.point;
        }

        // Return out of player view if not a proper target point
        return new Vector3(0, -50, 0);
    }
}
