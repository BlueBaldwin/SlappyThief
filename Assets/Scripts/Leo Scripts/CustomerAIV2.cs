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
    private bool waitTimer = false;
	#endregion

	#region Functions
	// Start is called before the first frame update
	void Start()
    {
        waitTimer = true;                           //wait timer is set to true
        nma = this.GetComponent<NavMeshAgent>();    //waypoints will be used to find where the customer will go to next

    }

    // Update is called once per frame
    void Update()
    {
		if (waitTimer == true) {                //if the waitTimer is true 
            StartCoroutine(Timer());            //start the coroutine timer
		}
    }
    void CustomerMovement() {
        if (nma.hasPath == false) {
            currentWaypoint = Random.Range(0, waypoint.Length + 1);             //this will randomly create a waypoint for the customer to go to from a selection of 
                                                                                //current waypoints available
            nma.SetDestination(waypoint[currentWaypoint].transform.position);   //set the customers position to a waypoint
        }
    }

    IEnumerator Timer () {                      //so the coroutine timer has been activate
        waitTimer = false;                      //turn the timer to false so the player doesn't move around randomly
        yield return new WaitForSeconds(5);     //have the player wait for [x] amount of seconds
        Debug.Log("Customer is waiting");       //just to check in the console whether customer is waiting or not
        waitTimer = true;                       //turn it back to true so that in update it will be able to come back into this coroutine
        CustomerMovement();                     //jump into the customermovement function and move the customer
	}
    #endregion 
}
//Leo 09/11/2021
//Improved version of the previous waypoint script.
//


//pseudocode
/* have a bool that controls whether a timer is started or not
 * set destination to it's first waypoint
 * if the customer gets to the waypoint turn the timer bool to true
 * this will start the coroutine that will dictate
 * how long the customer will wait at their current destination
 * once the coroutine hits 0, send the player to the next random destination
 * 
 * bool starts off as true
 * update will check whether the bool is true or not
 * if it is it will start the Timer Coroutine
 * the Coroutine will set the bool timer back to false
 * it will wait for 5 seconds
 * throw itself into the player movement function and have the player
 * move to a random location
 */






