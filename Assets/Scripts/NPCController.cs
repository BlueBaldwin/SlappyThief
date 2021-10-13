using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

[RequireComponent(typeof(NavMeshAgent))]

public class NPCController : MonoBehaviour
{

   public GameObject[] shoppingPoints = null;

   // If timer reaches 0 then messMade becomes true and move on to next point
   private NavMeshAgent agent;
   private Vector3 destination;
   private float messTimer = 3.0f;
   private bool moveTarget = false;
   private GameObject target = null;
   private int randomPoint;


   private void Start()
   {
      int arrayLength = shoppingPoints.Length;
      agent = GetComponent<NavMeshAgent>();
   }

   private void Update()
   {

      if (!moveTarget)
      {
         messTimer -= (1 * Time.deltaTime);
         if (messTimer <= 0)
         {
            ChangePos();
         }
      }
   }

// Random number (point) chosen 
   void ChangePos()
   {
      randomPoint = UnityEngine.Random.Range(1, shoppingPoints.Length);
      target = shoppingPoints[randomPoint];
      print(target);
      print(target.transform.position);
      destination = target.transform.position;
      agent.destination = destination;
      moveTarget = false;
   }
}
// Pass that number to move character
// Once character arrives - count down 3's
// Change sprite
// repeat
