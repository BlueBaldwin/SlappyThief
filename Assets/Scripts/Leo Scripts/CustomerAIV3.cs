using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAIV3 : MonoBehaviour
{
    #region SerializeFields

    #endregion

    #region Variables
    public float Range;
    #endregion

    #region Functions
    bool RandomPoint(Vector3 center, float range, out Vector3 result) 
    {

		for (int i = 0; i < 30; i++)                                                    //
        {                                                                               //
            Vector3 randomPoint = center + Random.insideUnitSphere * range;             //
            NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))   //
            {
                result = hit.position;
                return true; 
			}
        }
        result = Vector3.zero;

        return false;
    }

    public Vector3 GetRandomPoint(Transform point = null, float radius = 0) {
        Vector3 _point;

		if (RandomPoint(point == null?transform.position : point.position, radius == 0? Range : radius, out _point)) {
            Debug.DrawRay(_point, Vector3.up, Color.black, 1);
		}
        return Vector3.zero;
	}
	#endregion
}

/* Pseudocode
 * setcustomer destination to a waypoint
 * Once the player reaches that waypoint, set next destination to waypoint within proximity of the customer
 * via collider detection
 */
