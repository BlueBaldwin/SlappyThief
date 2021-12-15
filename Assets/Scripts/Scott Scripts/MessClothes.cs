using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessClothes : MonoBehaviour
{
    public bool bTidyUpMess = false;

    [SerializeField] GameObject tidyClothes;
    [SerializeField] GameObject messyClothes;
    [SerializeField] float timerDuration;
    private float timer;
    private bool startTheTimer;
    public bool isMessy; //added for foldminigame integration -- Zac

    private void Awake()
    {
        messyClothes.gameObject.SetActive(false);
        timer = timerDuration;
        isMessy = false;
    }
    private void Update()
    {
        if(startTheTimer && !isMessy)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer <= 0)
            {
                MessClothing();
                startTheTimer = false;
            }
        }
        if(bTidyUpMess)
        {
            TidyMessyClothes();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        startTheTimer = true;
    }

    void OnTriggerExit(Collider other)
    {
        timer = timerDuration;
        Debug.Log("NPC Exited");
    }

    void MessClothing()
    {
        messyClothes.gameObject.SetActive(true);
        tidyClothes.gameObject.SetActive(false);
        isMessy = true;
    }

    void TidyMessyClothes()
    {
        messyClothes.gameObject.SetActive(false);
        tidyClothes.gameObject.SetActive(true);
        isMessy = false;
        bTidyUpMess = false;
    }
}
