using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
	#region SerializeFields
	[SerializeField]
	GameObject lookAtTarget;									//target to be assigned in the editor
	#endregion

	#region Variables
	#endregion

	#region Functions
	// Start is called before the first frame update
	void Start()
    {
		lookAtTarget = GameObject.Find("LookAtPoint");		//the lookAtTarget is assigned here
															//by it being found when the game starts
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 targetPosition = new Vector3(lookAtTarget.transform.position.x,		//the x value is assigned to be the lookAtTarget
											transform.position.y,					//the y value is assigned to just it's normal value
											lookAtTarget.transform.position.z);		//the z value is assigned to be the lookAtTarget

		transform.LookAt(targetPosition);											//normal lookAt function but will now look at where the Vector3 targetPosition is
																					//with it's assigned parameters so that it is locked at the y axis and won't rotate 
																					//any other way
	}
		
	#endregion
}
