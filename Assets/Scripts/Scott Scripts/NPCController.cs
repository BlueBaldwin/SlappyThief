using System.Collections;
using System.Collections.Generic;
using System;
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

    private bool movePosition = false;
    private int totalWaypoints;
    private int randomPosition;
    
    void Awake()
    {
        // Attaching required components to the NPC
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        totalWaypoints = waypoints.Count;
    }

    private void Update()
    {
        if (movePosition)
        {
           SetDestination();
        }
        
    }

    private void SetDestination()
    {
        if (navMeshAgent != null)
        {
            randomPosition = Random.Range(1, totalWaypoints);
            navMeshAgent.destination = currentTarget.position;
        }
    }
}