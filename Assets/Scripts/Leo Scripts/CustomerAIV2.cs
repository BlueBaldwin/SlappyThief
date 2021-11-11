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

    [SerializeField]
    IEnumerator coroutine;

    #endregion

    #region Variables
    int cCounter = 0;
	#endregion

	#region Functions
	// Start is called before the first frame update
	void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        //waypoints will be used to find where the customer will go to next
    }

    // Update is called once per frame
    void Update()
    {
        if (cCounter <= 10) {
            CustomerMovement();
        }
    }
    void CustomerMovement() {
        if (nma.hasPath == false) {
            currentWaypoint = Random.Range(0, waypoint.Length + 1);
            Debug.Log("Customer is waiting");
            StartCoroutine(Timer());
            nma.SetDestination(waypoint[currentWaypoint].transform.position);
            Debug.Log(cCounter);
            cCounter += 1;
        }
    }

    IEnumerator Timer () {
        yield return new WaitForSeconds(15);
	}
    #endregion 
}
//Leo 09/11/2021
//Improved version of the previous waypoint script.
//