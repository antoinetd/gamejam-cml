using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Ctrl : MonoBehaviour
{
    // Members
   
    public GameObject agent;
    public GameObject kid;

    private float closestObjectDistance;
    private Vector3 closestObjectPosition;
    private float kidParentDistance; 

    
    private AIStates stateValue = 0;
    
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

    public void doChasing()
    {
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = kid.gameObject.GetComponent<Transform>().position;
    }

    public void doBlocking()
    {
        this.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = kid.gameObject.GetComponent<Transform>().position;
    }

    public void doIdle()
    {
        //
    }

    float getDistance(GameObject Ob1, GameObject Ob2)
    {
        return Vector3.Distance(Ob1.GetComponent<Transform>().position, Ob1.GetComponent<Transform>().position);
    }

    public void doDistances()
    {
        kidParentDistance = getDistance(this.gameObject, kid.gameObject);

        closestObjectDistance = -1;
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
        
    }


    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        doDistances(); 

        if (kidParentDistance > 1.0)
        {
            stateValue = AIStates.chasing; 
        }

        if (closestObjectDistance < 1.0)
        {
            stateValue = AIStates.blocking;
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
        //var something = kidScript.closestsInteractables;
    }
}
