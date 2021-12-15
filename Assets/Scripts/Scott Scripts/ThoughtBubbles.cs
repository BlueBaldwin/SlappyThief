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
   [SerializeField] private Vector3 bubbleOffset;
   [SerializeField] private GameObject[] clothingSelection = new GameObject[3];
   Transform Target;

   private void Awake()
   {
        // Moving the sprites position above the character
        transform.localPosition = bubbleOffset;
        Target = Camera.main.gameObject.transform;
   }

    public void Update()
    {
        //ensure bubble always faces camera 
        gameObject.transform.LookAt(Target,Vector3.up);
    }

    public void Hide()
    {
        //hide all components -- Zac
        foreach(GameObject g in clothingSelection)
        {
            g.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void Show(int i)
    {
        //show required componenents -- Zac
        gameObject.SetActive(true);
        if (i >= 0 && i < clothingSelection.Length) 
          {
            clothingSelection[i].SetActive(true);
          }
        else
        {
            Debug.Log("Invalid Index Passed for Bubble");
            Hide();
        }
    }

   void OnTriggerEnter(Collider other)
   {
        //switch (other.tag)
        //{
        //   case "RedShirt":
        //   {
        //      Destroy(other.gameObject);
        //      break;
        //   }
        //    case "BlueShirt":
        //   {
        //      Destroy(other.gameObject);
        //      break;
        //   }
        //   case "Trousers":
        //   {
        //      Destroy(other.gameObject);
        //      break;
        //   }
        //    default:
        //    break;
        //
        Debug.Log("Triggered, add code here if needed"); //not sure what this method is for -- Zac
   }
}
