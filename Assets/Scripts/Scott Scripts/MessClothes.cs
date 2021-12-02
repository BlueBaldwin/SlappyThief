using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessClothes : MonoBehaviour
{
    [SerializeField] Collider trigger;
    void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("Triggered");
    }
}
