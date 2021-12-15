using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
	// Start with 100 points (try and retain as many points as possible
	[SerializeField] public static float totalScore = 100.0f;
	[SerializeField] private float queuePenaltyTimer;
	[SerializeField] private float penaltyTimerReset = 8.0f;

	[SerializeField] List<GameObject> MessyLocations;

	public ShopInfo myShopInfo;

	void Awake()
	{
		queuePenaltyTimer = penaltyTimerReset = 8.0f;
	}

	void Update()
	{
		//for (int i = 0; i < myShopInfo.; i++)
		//{
			
		//}
		if (ShopperBehaviour.isInQueue)
		{
			CustomerWaitingPenalty();
		}
	}

	void CustomerWaitingPenalty()
	{
		if (queuePenaltyTimer <= 0.0f)
		{
			totalScore -= 10;
			queuePenaltyTimer = penaltyTimerReset;
		}
	}
}
    
