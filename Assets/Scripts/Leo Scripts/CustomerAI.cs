using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
	#region SerializeFields

	#endregion

	#region Public Variables
	NavMeshAgent agent;

	public Transform[] waypoints;

	int waypointIndex;

	Vector3 target;
	#endregion

	#region Functions

	private void Start() {
		agent = GetComponent<NavMeshAgent>();
		UpdateDestination();
	}

	private void Update() {
		//if the distance is less that a certain amount
		//if it is iterate the waypoint and update the destination
		if (Vector3.Distance(transform.position, target) < 1) {
			IterateWaypointIndex();
			UpdateDestination();
		}
	}

	void UpdateDestination() {
		target = waypoints[waypointIndex].position;
		agent.SetDestination(target);
	}

	void IterateWaypointIndex() {
		
		waypointIndex++;

		if (waypointIndex == waypoints.Length) {
			//when the last waypoint is reached, go back to the last waypoint
			//this will basically just have the customer go around in loop of waypoints.
			waypointIndex = 0;
		}
	}

	#endregion
}

//Leo 07/11/2021
//Test & Improved version of the Customer nav mesh
//The Customer will navigate around the store via a set of waypoints
//The customer will go towards the 1st, 2nd, 3rd and so forth in waypoints
//Once the customer gets to the end of the waypoints (the array), it will reset and start a loop again
//Where it will navigate towards the 1st waypoint
