using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

// Required components for all NPC Characters
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class NPCController : MonoBehaviour
{
    // TargetLocations
    [SerializeField] private Transform currentTarget;
    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    private NavMeshAgent navMeshAgent = null;
    private Rigidbody rb = null;

    private bool movePosition = true;
    private bool startTimer = false;
    private bool changeTarget = false;
    
    private int totalWaypoints;
    private int randomPosition;
    private float timer = 5f;
    
    void Awake()
    {
        // Attaching required components to the NPC
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        totalWaypoints = waypoints.Count;
        SetDestination();
    }

    private void Update()
    {
        if (startTimer == false && timer > 0)           // Need timer to start once arrived at target 
        {
            StartCoroutine(ShoppingTimer());
        }
        if (changeTarget)
        {
            //ChangePosition();                     // Too be created
        }
    }
    // Setting a new Destination initially and once called
    private void SetDestination()
    {
        if (navMeshAgent != null && movePosition)
        {
            randomPosition = Random.Range(1, totalWaypoints);
            currentTarget = waypoints[randomPosition];
            movePosition = false;                   // needs to be set to true once npc reaches target
            return;
        }
    }
    
    IEnumerator ShoppingTimer()
    {
        startTimer = true;      // Set to true to stop multiple co-routines being set off at once
        yield return new WaitForSeconds(1);
        timer -= 1;
        startTimer = false;     // Resets back to false after the 
        if (startTimer == false && timer <= 0)
        {
            Move();
        }
    }
    
    // Initiating the NPC's Movement to the destination
    private void Move()
    {
        navMeshAgent.destination = currentTarget.position;
    }
}