using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{

    [SerializeField]
    ShopInfo s; //need this to make dyanmic spawning work with my script. -- zac

    // public Dictionary<string, Queue<GameObject>> npcDictionary;
    [SerializeField] private List<GameObject> NPCList = new List<GameObject>();
    [SerializeField] private List<GameObject> inGameNPC = new List<GameObject>();
    [SerializeField] private float spawnGap = 5.0f;
    [SerializeField] private Transform spawnOrigin;
    //[SerializeField] private int xSpawnBounds;
    //[SerializeField] private int zSpawnBounds;



    private bool bSpawnNewChar = false;
    private bool bStartSpawnTimer = false;
    

    private GameObject returningCharacter;
    
    // Min / max spawn area = ///  x = -28.167 / 16 ///  z = -6 / 2.5

    private void Start()
    {
        bSpawnNewChar = true;
        bStartSpawnTimer = true;
    }

    private void Update()
    {
        if (NPCList != null && NPCList.Count >= 1)
        {
            if (bStartSpawnTimer)
            {
                Invoke("SpawnNPC", 5);
                bStartSpawnTimer = false;
                //StartCoroutine(SpawnTimer());
            }
        }
    }

    IEnumerator SpawnTimer()
    {
        if (bSpawnNewChar)
        {
            if (bStartSpawnTimer)
            { 
                yield return new WaitForSeconds(spawnGap);
                SpawnNPC();
            }
        }
    }

    private void SpawnNPC()
    {
        int characterSelection = Random.Range(1, NPCList.Count);
        // Vector2 radiusSpawn = Random.insideUnitCircle * 5;
        // spawnOrigin.transform.position += new Vector3(radiusSpawn.x, 0.0f, radiusSpawn.y);
        GameObject g = Instantiate(NPCList[characterSelection], spawnOrigin.position, Quaternion.identity);
        inGameNPC.Add(NPCList[characterSelection]);
        Debug.Log(g.name);
        Debug.Log("has sb: " +  g.GetComponent<ShopperBehaviour>() != null);
        s.ActiveShoppers.Add(g.GetComponent<ShopperBehaviour>()); // need this --zac 
        NPCList.Remove(NPCList[characterSelection]);
        bStartSpawnTimer = true;
    }
    private void ReturnCharacter()
    {
        // Add character back to the list
        NPCList.Add(returningCharacter);
        s.ActiveShoppers.Remove(returningCharacter.GetComponent<ShopperBehaviour>()); //need this -- zac
    }

    private void OnTriggerEnter(Collider other)
    {
        returningCharacter = other.gameObject;
    }
}
