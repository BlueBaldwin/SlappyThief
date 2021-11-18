using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAIV3 : MonoBehaviour
{
    #region SerializeFields
    [SerializeField]
    NavMeshAgent nma = null;
    //holds a pointer to the customers nma(nav mesh agent)

    [SerializeField]
    GameObject[] waypoint;

    [SerializeField]
    int currentWaypoint;

    [SerializeField]
    Transform customerCentre;

    [SerializeField]
    float customerRadius = 5;
    #endregion

    #region Variables

    #endregion

    #region Functions
    //void Start () {
    //    nma = this.GetComponent<NavMeshAgent>();    //waypoints will be used to find where the customer will go to next
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    //void CustomerMovement () {
    //    if (nma.hasPath == false) {
    //        currentWaypoint = Random.Range(0, waypoint.Length + 1);             //this will randomly create a waypoint for the customer to go to from a selection of 
    //                                                                            //current waypoints available
    //        nma.SetDestination(waypoint[currentWaypoint].transform.position);   //set the customers position to a waypoint
    //    }
    //}

    void ProximityCheck () {
        Collider[] proxCheck = Physics.OverlapSphere(customerCentre.position, customerRadius);
    }

	private void OnDrawGizmos () {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(customerCentre.position, customerRadius);
	}
	#endregion
}

/* Pseudocode
 * setcustomer destination to a waypoint
 * Once the player reaches that waypoint, set next destination to waypoint within proximity of the customer
 * via collider detection
 */
