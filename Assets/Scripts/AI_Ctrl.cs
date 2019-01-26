using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Ctrl : MonoBehaviour
{
    public Transform someLocation;
    public GameObject agent; 


    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = someLocation.position; 
    }
}
