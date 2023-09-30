using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
	// Public fields
	public float maxSpeed;
	public float targetRadius;
	
	// Reference to pathfinding component and rigidbody
	AStar pathFinder;
	public Rigidbody rb;

    // Get the pathfinding component to reference path
    void Start()
    {
		pathFinder = GetComponent<AStar>();
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		// Always follow path
		FollowPath(pathFinder.targetPath);
    }

	// Method for following current path
    void FollowPath(List<Node> path)
    {
		// Null check the path
		if (path == null)
			return;

		// Set the current Node to begin with
		Node curClosestNode = null;
		if (path.Count > 0)
		{
			curClosestNode = path[0];
		}

		// Iterate through the path and reach the locations along it until the end is met
		if (path.Count > 0)
		{
			// Get the direction to the node
			Vector3 direction = curClosestNode.worldPosition - transform.position;

			// Get the distance to the end node
			float distance = (path[path.Count - 1].worldPosition - transform.position).magnitude;

			// Stop moving since you have gotten close
			// enough to your destination
			if (distance <= targetRadius)
			{
				rb.velocity = new Vector3();
				return;
			}

			// Init target values
			Vector3 targetVelocity = new Vector3();
			float targetSpeed = maxSpeed;

			// Calculate the target velocity
			targetVelocity = direction.normalized * targetSpeed;

			// Face the target based on new velocity
			FaceTarget(targetVelocity);

			// Update position based on velocity
			rb.velocity = targetVelocity;
		}
	}

	// Method used for facing the target
	public void FaceTarget(Vector3 velocity)
	{
		// Return if not moving
		if (velocity.magnitude == 0)
			return;

		// Set rotation towards velocity direction
		transform.rotation = Quaternion.LookRotation(
			new Vector3(velocity.normalized.x, 0.0f, velocity.normalized.z),
			transform.up);
	}
}
