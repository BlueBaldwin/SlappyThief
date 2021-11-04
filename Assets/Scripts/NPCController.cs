using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

// Required components for all NPC Characters
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class NPCController : MonoBehaviour
{
    // TargetLocations
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform[] Waypoints;
    
    private NavMeshAgent navMeshAgent = null;
    private Rigidbody rb = null;
    
    private bool movePosition = false;
    
    void Awake()
    {
        // Attaching required components to the NPC
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (movePosition)
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.destination = currentTarget.position;
            }
        }
        
    }
}