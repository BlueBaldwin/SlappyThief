using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CustomerNavMesh : MonoBehaviour
{
	#region SerializeField variables
	[SerializeField]
	private Transform movePositionTransform;
	#endregion

	#region Other Variables
	private NavMeshAgent navMeshAgent;
	#endregion

	#region Functions
	private void Awake() {
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		navMeshAgent.destination = movePositionTransform.position;
	}
	#endregion
}

//Leo 07/11/2021
//Test customer nav mesh script
//This was created just to test out how a customer may walk around the store
//It works via using the "MoveTarget" object in the scene.
//Place this object where ever inside the example building (so within where the nav mesh is baked)
//Attach the "MoveTarget" object to "MovePositionTransform" in the "CustomerNavMesh" script.
//The Customer will then hopefully move towards the point on the map.
//
//
//You can move the point around when project is in play and the Customer will move themselves
//towards the point