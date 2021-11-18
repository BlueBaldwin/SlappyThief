using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MinigameManager : MonoBehaviour
{
    [SerializeField]
    GameObject GameBase; //base gameobject that controls the transform of all minigames and their spawned objects
    //currently MinigamePlane in the editor, used to rotate minigames to be in line with the camera
    //(this is manually set currently, should probably make it rotate to camera on start but as long as we never rotate the camera it will be ok)

    [SerializeField]
    GameObject MinigamesParent; //empty gameobject that has all different minigames as it's children

    private List<Minigame> Minigames; //list of all minigame components 

    private int ActiveGame;

    public int GetActiveMinigame()
    {
        return ActiveGame;
    }

    private void Start()
    {
        GameBase.SetActive(false);
        ActiveGame = -1; //-1 is used for no active game. 0 would index to the first minigame of the list. Don't call Minigames[-1].Load everything will explode, make sure a minigame is active first
        Minigames = new List<Minigame>(GetComponentsInChildren<Minigame>()); //searches children of MinigameParent for minigame components, then adds them to the list of possible minigames
    }

    public void LoadMinigame(int gameID)
    {
        ActiveGame = gameID;
        GameBase.SetActive(true);
        Minigames[ActiveGame].Load(GameBase); //calls custom load function for the active game
    }

    public void UnloadMinigame()
    {
        GameBase.SetActive(false);
        Minigames[ActiveGame].Unload(); //calls the generic unload function for all minigames, with the activegame as the target
        ActiveGame = -1;
    }
}

