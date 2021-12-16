using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAIV2 : MonoBehaviour
{
    #region SerializeFields
    [SerializeField]
    NavMeshAgent nma = null;
    //holds a pointer to the customers nma(nav mesh agent)

    [SerializeField]
    GameObject theft = null;
    [SerializeField]
    GameObject[] waypoint;

    [SerializeField]
    Transform[] startWaypoint;

    [SerializeField]
    Transform[] endWaypoint;

    [SerializeField]
    Transform[] stealingEndWaypoint;

    [SerializeField]
    int currentWaypoint;

    [SerializeField]
    IEnumerator coroutine;
    #endregion

    #region Variables
    private bool waitTimer = false;

    private bool isCustomerInShop = false;

    private bool isCustomerAThief;

    int waypointIndex;

    int endWaypointIndex;

    int inShopWaypointIndex;

    int stealingWaypointIndex;

    int numberAssignment;

    Vector3 target;
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Awake()
    {
        theft.gameObject.SetActive(false);
    }
    
    void Start()
    {
        theft.gameObject.SetActive(false);
        waitTimer = true;                           //wait timer is set to true
        nma = this.GetComponent<NavMeshAgent>();    //waypoints will be used to find where the customer will go to next
        StartCustomerMovement();
        isCustomerInShop = true;
        numberAssignment = Random.Range(0, 4);

		if (numberAssignment == 0 || numberAssignment == 1) {
            isCustomerAThief = false;
            Debug.Log("Customer is not a thief");
		} else if (numberAssignment == 2 || numberAssignment == 3) {
            isCustomerAThief = true;
            Debug.Log("Customer is a thief");
		}
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
            transform.LookAt(waypoint[currentWaypoint].transform.position);
            inShopWaypointIndex++;
            if (inShopWaypointIndex == 5) {                                     //when the inShopWaypointIndex is equal to 5
                isCustomerInShop = false;
			}
        }
    }

    IEnumerator Timer () {                      //so the coroutine timer has been activate
        waitTimer = false;                      //turn the timer to false so the player doesn't move around randomly
        yield return new WaitForSeconds(5);     //have the customer wait for [x] amount of seconds
        Debug.Log("Customer is waiting");       //just to check in the console whether customer is waiting or not
        waitTimer = true;                       //turn it back to true so that in update it will be able to come back into this coroutine

        if (isCustomerInShop == true) {
            CustomerMovement();                 //jump into the customermovement function and move the customer
        } else if (isCustomerInShop == false && isCustomerAThief == false) {
            EndCustomerMovement();
		} else if (isCustomerInShop == false && isCustomerAThief == true) {
            StealingCustomerMovement();
		}

	}

    IEnumerator EndTimer() {
        yield return new WaitForSeconds(10);
        Debug.Log("Customer is waiting to leave the store");
    }

    IEnumerator StealingTimer() {
        yield return new WaitForSeconds(5);
        Debug.Log("This Customer is a thief and is stealing");
	}

    void EndCustomerMovement() {               //function for when the customer has reached all the instoremovement waypoints, they will go to the end waypoint
		if (isCustomerInShop == false && isCustomerAThief == false) {
            target = endWaypoint[endWaypointIndex].position;
            nma.SetDestination(target);
		} else if (isCustomerInShop == false && isCustomerAThief == true) {
            StealingCustomerMovement();
		}
    }

    void EndWaypointIteration() {
        endWaypointIndex++;
        if (endWaypointIndex == 2) {
            //Minigame logic?
            StartCoroutine(EndTimer());
        }
    }

    void StealingCustomerMovement() {
            target = stealingEndWaypoint[stealingWaypointIndex].position;
            nma.SetDestination(target);
            theft.gameObject.SetActive(true);
	}

    void StealingCustomerIteration() {
        stealingWaypointIndex++;
		if (stealingWaypointIndex == stealingEndWaypoint.Length) {
            StealingTimer();
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
 * 
 * at the start of the game, randomly assign the customer a number from 1-2
 * if the number is 1 then they are a normal customer and if it is 2 they are assigned to be a thief
 * if their assigned number is 1 they're a normal customer if their assigned number 2 they are a thief
 * so once the main customer movement loop ends, they will either steal (run out the store) or buy
 * (they will go to the counter)
 */






