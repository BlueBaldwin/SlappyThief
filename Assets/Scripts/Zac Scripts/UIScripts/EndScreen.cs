using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class EndScreen : MonoBehaviour
{

    [SerializeField]
    List<Sprite> Sprites;

    List<float> Scores;
    [SerializeField]
    List<Image> Images;

    void Init()
    {
        Scores.Add(ScoreStatics.tidinessScore);
        Scores.Add(ScoreStatics.securityScore);
        Scores.Add(ScoreStatics.serviceScore);
        for(int i = 0; i < Scores.Count; ++i)
        {
            Images[i].sprite = Scores[i] < 20 ? Sprites[0] : Sprites[(int)Scores[i]/20]; // this is either the best or worst code ive ever written im so tired ive slept 3 of the last 48 hrs
        }
    }
}

