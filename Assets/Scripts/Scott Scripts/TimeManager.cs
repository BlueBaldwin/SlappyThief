using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    // Setting the in Game time Action mapping
    [SerializeField]
    SceneIndex s;
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set;}
    public static int Hour { get; private set;}

    private float minuteToRealTime = 0.3f;
    private float time;
    public bool shiftOver = false;


    void Awake()
    {
        // Setting the start in game 'Work Time' 
        Hour = 9;
        Minute = 0;
    }

    void Update()
    {
        time -= Time.deltaTime;
        // Creating the in game clock
        if (time <= 0)
        {
            Minute++;
            // If not null then invoke it (Action)
            OnMinuteChanged?.Invoke();
            if (Minute >= 59)
            {
                Hour++;
                Minute = 0;
                if(Hour >= 17){
                    shiftOver = true;
                    s.EndGame();
                }
            }
            time = minuteToRealTime;
        }
    }
}
