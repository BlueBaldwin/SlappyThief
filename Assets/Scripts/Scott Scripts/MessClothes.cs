using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessClothes : MonoBehaviour
{
    public static bool bTidyUpMess = false;

    [SerializeField] GameObject tidyClothes;
    [SerializeField] GameObject messyClothes;
    [SerializeField] float timerDuration;
    private float timer;
    private bool startTheTimer;

    private void Awake()
    {
        messyClothes.gameObject.SetActive(false);
        timer = timerDuration;
    }
    private void Update()
    {
        if(startTheTimer)
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
    }

    void TidyMessyClothes()
    {
        messyClothes.gameObject.SetActive(false);
        tidyClothes.gameObject.SetActive(true);
        bTidyUpMess = false;
    }
}
