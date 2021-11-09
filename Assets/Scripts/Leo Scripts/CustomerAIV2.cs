using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAIV2 : MonoBehaviour
{
    #region SerializeFields
    [SerializeField]
    NavMeshAgent nma = null;
    //holds a pointer to the customers nma(nav mesh agent)

    [SerializeField]
    GameObject[] waypoint;

    [SerializeField]
    int currentWaypoint;

	#endregion

	#region Variables

	#endregion

	#region Functions
	// Start is called before the first frame update
	void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        waypoint = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    // Update is called once per frame
    void Update()
    {
		if (nma.hasPath == false) {
            currentWaypoint = Random.Range(0, waypoint.Length +1);
            nma.SetDestination(waypoint[currentWaypoint].transform.position);
        }
    }
    #endregion 
}
//Leo 09/11/2021
//Improved version of the previous waypoint script.
//Needs further commenting.