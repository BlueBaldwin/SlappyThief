using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class EndScreen : MonoBehaviour
{

    [SerializeField]
    List<Sprite> StampSprites;

    [SerializeField]
    List<Sprite> BoxSprites;
    [SerializeField]
    List<Image> Images;

    [SerializeField]
    Image BigStamp;
    void Awake()
    {
        List<float> Scores = new List<float>();
        Scores.Add(ScoreStatics.tidinessScore);
        Scores.Add(ScoreStatics.securityScore);
        Scores.Add(ScoreStatics.serviceScore);

        for(int i = 0; i < Scores.Count; ++i)
        {      
            Images[i].enabled = true;     
            Images[i].sprite = Scores[i] < 40 ? BoxSprites[0] : BoxSprites[1];
            Images[i].preserveAspect = true;
        }

         BigStamp.sprite = ScoreStatics.totalScore < 60 ? StampSprites[0] : StampSprites[(int)ScoreStatics.totalScore/60]; // this is either the best or worst code ive ever written im so tired ive slept 3 of the last 48 hrs
        BigStamp.preserveAspect = true; 
    }
}

