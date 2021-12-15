using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
	// Start with 100 points (try and retain as many points as possible
	public static float totalScore = 100.0f;
	
	[SerializeField] private float queuePenaltyTimer;
	[SerializeField] private float penaltyTimerReset = 8.0f;

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
	}

	void CustomerWaitingPenalty()
	{
		if (queuePenaltyTimer <= 0.0f)
		{
			totalScore -= 10;
		}
	}
}
    
