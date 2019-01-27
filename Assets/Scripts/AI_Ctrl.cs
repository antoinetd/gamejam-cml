using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class AI_Ctrl : MonoBehaviour
{
    // Members   
    public GameObject agent;
    public GameObject kid;
    public float biasToBlocking = 1.0f;

   
    private float closestObjectDistance;
    private Vector3 closestObjectPosition;
    private float kidParentDistance;

    private AIStates stateValue;
    private float midpointParentDistance; // midpoint between kid and closest object
    private Vector3 midpointPosition;

    
    // Types
    public enum AIStates
    {
        chasing,
        blocking,
        idle          
    };                
    
    //Utility functions


    int frameCount = 0; 
    public void doChasing()
    {
        this.GetComponent<NavMeshAgent>().destination = kid.GetComponent<Transform>().position;       
    }

    public void doBlocking()
    {
        this.GetComponent<NavMeshAgent>().destination = midpointPosition;
    }

    public void doIdle()
    {
      
    }

    float getDistance(GameObject Ob1, GameObject Ob2)
    {
        return Vector3.Distance(Ob1.GetComponent<Transform>().position, Ob2.GetComponent<Transform>().position);
    }

    public void calcDistances()
    {
        this.kidParentDistance = getDistance(this.gameObject, kid); 

        closestObjectDistance = -1;
        midpointParentDistance = -1;
        KidControls KC = kid.GetComponent<KidControls>();
        if (KC != null)
        {
            foreach (GameObject currentGO in kid.GetComponent<KidControls>().closestsInteractables)
            {
                float kidCurrentGODistance = getDistance(currentGO, kid);
                if (kidCurrentGODistance > closestObjectDistance)
                {
                    closestObjectDistance = kidCurrentGODistance;
                    closestObjectPosition = currentGO.GetComponent<Transform>().position;
                }
            }
        }

        if (closestObjectDistance != -1)
        {
            midpointPosition = (kid.GetComponent<Transform>().position + closestObjectPosition) / 2;
            midpointParentDistance = (midpointPosition - this.gameObject.GetComponent<Transform>().position).magnitude;
        }

    }


    // Update is called once per frame
    void LateUpdate()
    {
        calcDistances();          
        
        //if (kidParentDistance > maxDistance || midpointParentDistance == -1)
        //{
        //    setState(1);
        //}
        //else
        //{
        //    setState(0); 
        //}

        if (kidParentDistance > midpointParentDistance * biasToBlocking)
        {
            stateValue = AIStates.chasing;
        }
        else
        {
            stateValue = AIStates.blocking;
        }

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
    }
}
