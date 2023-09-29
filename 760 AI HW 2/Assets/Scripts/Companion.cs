using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    // Get the player character
    public GameObject targetObject;

    // Get reference to A* component
    AStar pathFinder;

    // Get the pathfinding component to reference path
    void Start()
    {
        pathFinder = GetComponent<AStar>();
    }

    // Runs once per frame
    void Update()
    {
        pathFinder.target = targetObject.transform.position;
    }
}
