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
    Transform[] startWaypoint;

    [SerializeField]
    Transform[] endWaypoint;

    [SerializeField]
    int currentWaypoint;

    [SerializeField]
    IEnumerator coroutine;

    #endregion

    #region Variables
    private bool waitTimer = false;

    private bool isCustomerInShop = false;

    int waypointIndex;

    int endWaypointIndex;

    int inShopWaypointIndex;

    Vector3 target;
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        waitTimer = true;                           //wait timer is set to true
        nma = this.GetComponent<NavMeshAgent>();    //waypoints will be used to find where the customer will go to next
        StartCustomerMovement();
        isCustomerInShop = true;
    }

    // Update is called once per frame
    void Update()
    {
		if (waitTimer == true) {                //if the waitTimer is true 
            StartCoroutine(Timer());            //start the coroutine timer
		}

		if (Vector3.Distance(transform.position, target) < 1) {
            WaypointIteration();
            StartCustomerMovement();
        }
    }

    void StartCustomerMovement() {              //function for when the game begins, customers will move to the start waypoints first
        if (waypointIndex >= 0 && waypointIndex < startWaypoint.Length)
        {
            target = startWaypoint[waypointIndex].position;
            nma.SetDestination(target);
        }
        //added this check to suppress OutOfBounds errors. This is not the proper way to fix it I just need to be able to see my console output. --Zac
	}

    void WaypointIteration() {
        waypointIndex++;
		if (waypointIndex == 2) {               //once the waypointindex = 2
            isCustomerInShop = true;            //set the isCustomerInShop bool to true
		}                                       //so that it will stop looping between the StartCustomerMovement
	}                                           //and will then proceed to move to the normal CustomerMovement waypoints

    void CustomerMovement() {
        if (nma.hasPath == false) {
            currentWaypoint = Random.Range(0, waypoint.Length); //removed +1 as it was causing out of bound errors - Zac              //this will randomly create a waypoint for the customer to go to from a selection of 
                                                                                //current waypoints available
            Debug.Log(inShopWaypointIndex);
            nma.SetDestination(waypoint[currentWaypoint].transform.position);   //set the customers position to a waypoint
            inShopWaypointIndex++;
            if (inShopWaypointIndex == 5) {                                     //when the inShopWaypointIndex is equal to 5
                isCustomerInShop = false;                                       //then set the isCustomerInShop bool to false to get the customer to use the EndCustomerMovement function
			}
        }
    }

    IEnumerator Timer () {                      //so the coroutine timer has been activate
        waitTimer = false;                      //turn the timer to false so the player doesn't move around randomly
        yield return new WaitForSeconds(2);     //have the customer wait for [x] amount of seconds
        Debug.Log("Customer is waiting");       //just to check in the console whether customer is waiting or not
        waitTimer = true;                       //turn it back to true so that in update it will be able to come back into this coroutine

        if (isCustomerInShop == true) {
            CustomerMovement();                 //jump into the customermovement function and move the customer
        } else if (isCustomerInShop == false) {
            EndCustomerMovement();
		}
	}

    IEnumerator EndTimer() {
        yield return new WaitForSeconds(10);
        Debug.Log("Customer is waiting to leave the store");
    }

    void EndCustomerMovement() {               //function for when the customer has reached all the instoremovement waypoints, they will go to the end waypoint
		if (isCustomerInShop == false) {
            target = endWaypoint[endWaypointIndex].position;
            nma.SetDestination(target);
        }
    }

    void EndWaypointIteration() {
        endWaypointIndex++;
        if (endWaypointIndex == 1) {
            //Minigame logic?
            StartCoroutine(EndTimer());
            return;
        }
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






