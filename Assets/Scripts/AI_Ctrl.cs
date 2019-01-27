using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class AI_Ctrl : MonoBehaviour
{
    // Members   
    public GameObject agent;
    public GameObject kid;
   
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
    public void setState(int someState)
    {        
        switch(someState)
        {
            case 0:
                stateValue = AIStates.blocking; 
                break;
            case 1:
                stateValue = AIStates.chasing;
                break;
            case 2:
                stateValue = AIStates.chasing;
                break;
            default:
                break;
        }
    }
    int frameCount = 0; 
    public void doChasing()
    {
        this.GetComponent<NavMeshAgent>().destination = kid.GetComponent<Transform>().position;
        // Debug.Log(this.GetComponent<NavMeshAgent>().destination);  
    }

    public void doBlocking()
    {
       // this.GetComponent<NavMeshAgent>().destination = kid.gameObject.GetComponent<Transform>().position;
    }

    public void doIdle()
    {
      
    }

    float getDistance(GameObject Ob1, GameObject Ob2)
    {
        return Vector3.Distance(Ob1.GetComponent<Transform>().position, Ob2.GetComponent<Transform>().position);
    }

    public void doDistances()
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


    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {       
        doDistances();
      
        Debug.Log(kidParentDistance);

        if (kidParentDistance > 0.01f)
        {           
            setState(1); 
        }       
             
      switch(stateValue)
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
