using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
	// Start with 100 points (try and retain as many points as possible
	public static float tidinessScore = 100.0f;
	private static float totalScore;
	private float serviceScore = 100.0f;
	private float securityScore = 100.0f;

	[SerializeField] private float queuePenaltyTimer;
	[SerializeField] private float penaltyTimerReset = 8.0f;
	[SerializeField] private TimeManager shopTime;

	public ShopInfo myShopInfo;

	void Awake()
	{
		queuePenaltyTimer = penaltyTimerReset = 8.0f;
	}

	void Update()
	{
		for (int i = 0; i < myShopInfo.QueueLength(); i++)
		{
			CustomerWaitingPenalty();
		}
		queuePenaltyTimer = penaltyTimerReset;

		if (shopTime.shiftOver)
		{
			GetTotalScore();
		}
	}

	void CustomerWaitingPenalty()
	{
		if (queuePenaltyTimer <= 0.0f)
		{
			serviceScore -= 10;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Thief"))
		{
			serviceScore -= 100;
			//Debug.Log("thieeeeeeeef");
		}
	}

	public void GetTotalScore()
	{
		ScoreStatics.securityScore = securityScore;
		ScoreStatics.tidinessScore = tidinessScore;
		ScoreStatics.serviceScore = serviceScore;
		ScoreStatics.totalScore = securityScore + tidinessScore + serviceScore;
	}
}
    
