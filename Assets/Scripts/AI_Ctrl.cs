using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Ctrl : MonoBehaviour
{
    // Members
   
    public GameObject agent;
    public GameObject kid;

    private float closestObject;
    private float kidDistance; 
    
    
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

    public void doDistances()
    {
        kidDistance = Vector3.Distance(this.gameObject.GetComponent<Transform>().position, kid.gameObject.GetComponent<Transform>().position);
        foreach (GameObject g in kid.GetComponent<KidControls>().closestsInteractables)
        {
            //TODO: Determine the cloest object 
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
        // boolean 
        // conditional 1 if parent < kid.distance = 1
        // conditonal 2 if parent.distance > 1 -> chasing
       
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
