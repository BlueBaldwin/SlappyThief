using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NPCController : MonoBehaviour
{
   [SerializeField] static List<GameObject> shopPos = new List<GameObject>();

   public int arrlengtrh = shopPos.Count;

}
