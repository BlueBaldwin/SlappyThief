using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    public static List<GameObject> inGameNPC = new List<GameObject>();

    [SerializeField] private List<GameObject> NPCList = new List<GameObject>();
    [SerializeField] private float spawnDelay = 5.0f;
    [SerializeField] private Transform spawnOrigin;
    
    //[SerializeField] private int xSpawnBounds;
    //[SerializeField] private int zSpawnBounds;
    
    private bool bStartSpawnTimer = false;
    private GameObject returningCharacter;
    
    // Min / max spawn area = ///  x = -28.167 / 16 ///  z = -6 / 2.5

    private void Start()
    {
        bStartSpawnTimer = true;
    }

    private void Update()
    {
        // Checking the total npc's in the scene
        if (NPCList != null && NPCList.Count > 1)
        {
            if (bStartSpawnTimer)
            {
                // Spawning delay
                Invoke("SpawnNPC", spawnDelay);
                bStartSpawnTimer = false;
            }
        }
    }
    
    // Spawns NPC from the NPCList into the scene within a radius
    // Removing from the spawning pool (NPCList and adding them to the inGameNPC list
    private void SpawnNPC()
    {
        int characterSelection = Random.Range(1, NPCList.Count);
        Vector2 radiusSpawn = Random.insideUnitCircle * 5;
        Vector3 TempPos = new Vector3(radiusSpawn.x, 1f, radiusSpawn.y);
        TempPos += spawnOrigin.transform.position;
        Instantiate(NPCList[characterSelection], TempPos, Quaternion.identity);
        inGameNPC.Add(NPCList[characterSelection]);
        NPCList.Remove(NPCList[characterSelection]);
        bStartSpawnTimer = true;
    }
    
    // Once character has finished shopping return the charcater to the list
    private void OnTriggerEnter(Collider other)
    {
        returningCharacter = other.gameObject;
        ReturnCharacter(returningCharacter);
    }
    private void ReturnCharacter(GameObject returningCharacter)
         {
             // Add character back to the list
             NPCList.Add(returningCharacter);
             inGameNPC.Remove(returningCharacter);
         }
}
