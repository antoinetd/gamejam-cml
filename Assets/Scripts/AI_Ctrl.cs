using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Ctrl : MonoBehaviour
{
    public Transform someLocation; 


    // Start is called before the first frame update
    void Start()
    {
     NavMeshAgent agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {     
        agent.destination = someLocation.position;
    }
}
