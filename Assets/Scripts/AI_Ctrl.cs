using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Ctrl : MonoBehaviour
{
    // Public members   
    public GameObject kid;
    public AIStates DebugAIState = AIStates.idle;
    public Transform midPointToKid;

    // Private members
    private float closestObjectDistance;
    private Vector3 closestObjectPosition;
    private float kidParentDistance;
    private AIStates stateValue;
    private float midpointParentDistance; // midpoint between kid and closest object
    private Vector3 midpointPosition;
    private float timerMax = 3f;
    private float timerMin = 0f;
    public bool isWaiting = false;


    // Types
    public enum AIStates
    {
        chasing,
        blocking,
        idle
    };


    // Methods
    public void doChasing()
    {
        if (isWaiting == false)
        {
            this.GetComponent<NavMeshAgent>().speed = 1.5f;
            this.GetComponent<NavMeshAgent>().destination = kid.GetComponent<Transform>().position;
        }
    }

    public void doBlocking()
    {
        if (isWaiting == false)
        {
            this.GetComponent<NavMeshAgent>().speed = 2.5f;
            this.GetComponent<NavMeshAgent>().destination = midPointToKid.transform.position;

            timerMax -= Time.deltaTime;
            if (timerMax < timerMin)
            {
                isWaiting = true;
                stateValue = AIStates.idle;
                timerMax = 3f; // reset timer
            }
        }
    }

    public void doIdle()
    {
        isWaiting = true;
        this.GetComponent<NavMeshAgent>().speed = 0f;

        timerMax -= Time.deltaTime;
        if (timerMax < timerMin)
        {
            timerMax = 5f; // reset timer
            isWaiting = false;
            stateValue = AIStates.chasing;
        }
    }

    float getDistance(GameObject Ob1, GameObject Ob2)
    {
        return Vector3.Distance(Ob1.GetComponent<Transform>().position, Ob2.GetComponent<Transform>().position);
    }

    public void calcDistances()
    {
        // Parent pointers
        GameObject parent = this.gameObject;
        Vector3 parentPosition = parent.GetComponent<Transform>().position;

        // Distance between kid and parent
        kidParentDistance = getDistance(parent, kid);

        // Initialize distance metrics
        closestObjectDistance = -1;
        midpointParentDistance = -1;

        // Find closest object to kid, and its distance
        KidControls KC = kid.GetComponent<KidControls>();
        if (KC != null)
        {
            var goList = KC.closestsInteractables;
            if (goList.Count > 0) closestObjectDistance = getDistance(goList[0], kid);
            foreach (GameObject currentObject in goList)
            {
                float kidCurrentObjectDistance = getDistance(currentObject, kid);
                if (kidCurrentObjectDistance <= closestObjectDistance)
                {
                    closestObjectDistance = kidCurrentObjectDistance;
                    closestObjectPosition = currentObject.GetComponent<Transform>().position;
                }
            }
        }

        // If there is at least one object, compute the midpoint data
        if (closestObjectDistance != -1)
        {
            midpointPosition = (kid.GetComponent<Transform>().position + closestObjectPosition) / 2;
            midpointParentDistance = (midpointPosition - parentPosition).magnitude;
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        calcDistances();

        // Note: if midpointParentDistance == -1, the kid is not near any objects
        if (isWaiting == false)
        {
            if (kidParentDistance > 2.0f)
            {
                stateValue = AIStates.chasing;
            }
            else
            {
                stateValue = AIStates.blocking;
            }
        }

        // State Machine - AI Behaviours 
        switch (stateValue)
        {
            case AIStates.blocking:
                doBlocking();
                break;
            case AIStates.chasing:
                doChasing();
                break;
            case AIStates.idle:
                doIdle();
                break;
        }

        Debug.Log(stateValue);

    }
}
