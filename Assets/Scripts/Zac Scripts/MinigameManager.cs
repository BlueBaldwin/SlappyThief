using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField]
    GameObject GameBase;
    [SerializeField]
    GameObject FoldPoint;
    [SerializeField]
    GameObject MinigameParent;


    [SerializeField]
    float OffsetX;
    [SerializeField]
    float OffsetZ;
    [SerializeField]
    float OffsetY;

    enum Minigame 
    {   
        GAME_NONE = 0,
        GAME_FOLD = 1,
        GAME_SCAN = 2,
    }

    Minigame ActiveGame = Minigame.GAME_NONE;

    public int GetActiveMinigame()
    {
        return (int)ActiveGame;
    }

    private void Start()
    {
        GameBase.SetActive(false);
    }

    public void LoadMinigame(int gameID)
    {
        ActiveGame = (Minigame)gameID;
        LoadMinigameInternal();
    }

    private void LoadMinigame(Minigame m)
    {
        if (ActiveGame != Minigame.GAME_NONE) UnloadMinigame();
        ActiveGame = m;
        LoadMinigameInternal();
    }

    void LoadMinigameInternal()
    {
        GameBase.SetActive(true);
        if(ActiveGame == Minigame.GAME_FOLD)
        {
            FoldMinigame();
        }
    }

    private void Update()
    {
        bool hasGameEnded = true;
        foreach(Transform t in MinigameParent.transform)
        {
            if (t.gameObject.activeInHierarchy)
            {
                hasGameEnded = false;
                break;
            }
        }
        if (hasGameEnded)
        {
            GameBase.SetActive(false);
        }
    }

    void FoldMinigame()
    {
        //awful bad terrible test code i will delete this
        Color c = Random.ColorHSV();
        GameObject temp;
        GameObject g =  Instantiate(FoldPoint, MinigameParent.transform);
        g.GetComponent<MeshRenderer>().material.color = c;
        g.transform.localPosition = new Vector3(-OffsetX,OffsetY,0);
        temp = g;

        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.GetComponent<MeshRenderer>().material.color = c;
        g.transform.localPosition = new Vector3(-OffsetX / 2, OffsetY, OffsetZ);
        temp.GetComponent<FoldPoint>().LinkedPoint = g.transform;

        c = Random.ColorHSV();

        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.transform.localPosition = new Vector3(OffsetX, OffsetY, 0);
        g.GetComponent<MeshRenderer>().material.color = c;
        temp = g;

        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.transform.localPosition = new Vector3(OffsetX / 2, OffsetY, OffsetZ);
        g.GetComponent<MeshRenderer>().material.color = c;
        temp.GetComponent<FoldPoint>().LinkedPoint = g.transform;

        c = Random.ColorHSV();

        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.transform.localPosition = new Vector3(0, OffsetY, -OffsetZ);
        g.GetComponent<MeshRenderer>().material.color = c;
        temp = g;

        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.transform.localPosition = new Vector3(0, OffsetY, OffsetZ);
        g.GetComponent<MeshRenderer>().material.color = c;
        temp.GetComponent<FoldPoint>().LinkedPoint = g.transform;
    }

    public void UnloadMinigame()
    {
        ActiveGame = Minigame.GAME_NONE;
        GameBase.SetActive(false);
    }
}

