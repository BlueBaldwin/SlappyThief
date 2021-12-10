using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    // Setting the in Game time Action mapping
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set;}
    public static int Hour { get; private set;}

    private float minuteToRealTime = 0.5f;
    private float timer;

    void Start()
    {
        // Setting the start in game 'Work Time' 
        Hour = 9;
        Minute = 0;
    }

    void Update()
    {

        timer -= Time.deltaTime;
        // Creating the in game clock
        if (timer <= 0)
        {
            Minute++;
            // If not null then invoke it (Action)
            OnMinuteChanged?.Invoke();
            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
            }
            timer = minuteToRealTime;
        }
    }
}
