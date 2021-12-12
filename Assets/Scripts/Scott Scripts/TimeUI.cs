using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
   
    // Subscribing Actions
    private void OnEnable()
    {
        // Updating the time when time changes
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }
    private void OnDisable()
    {
        // Ubsubscribing the UpdateTime method to the time changing actions
        // If TimeUI gameObject is ever disabled
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        // String interpolation static variables from TimeManager script
        // Set timeText to read timeManger . hour / minute 
        // Using :00 to set the string in this masked format rather than .ToString("00")
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
    }
}
