using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

//\=================================================================================
//\   When the thought bubble prefab is instantiated into the scene, it will randomly 
//\   choose an item to display and be shadowed to the NPC's transform whilst locking rotation
//\==================================================================================
public class ThoughtBubbles : MonoBehaviour
{
   [SerializeField] private Transform characeter;
   [SerializeField] private Vector3 bubbleOffset;
   [SerializeField] private GameObject[] clothingSelection = new GameObject[3];

   private int chosenIndex;
   private void Awake()
   {
      // Randomly selecting an item to request
      chosenIndex = Random.Range(0, clothingSelection.Length);
   }

   private void Start()
   {
      clothingSelection[chosenIndex].SetActive(true);
      // ==== Set the Chosen Characters index here ====
      // characeter = NPCManager.inGameNPC[].transform;
   }

   private void LateUpdate()
   {
      // Moving the sprites position above the character
      transform.position = (characeter.transform.position + bubbleOffset) ;
   }
}
