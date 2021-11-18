using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedBehaviour : MonoBehaviour
{
    //currently a simple getter/setter for the objects associated minigame. Could be expanded for other selectable objects if needed.

    [SerializeField]
     int gameID;

   public void SetGameID(int id)
    {
        gameID = id;
    }

    public int GetGameID()
    {
        return gameID;
    }
}
