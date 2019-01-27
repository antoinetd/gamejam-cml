using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAction : MonoBehaviour, IInteractable


{
    public bool ActionCalled; // Debug Output
    public AudioClip actionSound1;
    public AudioClip actionSound2;

    public void OnAction()
    {
        // Action to be called by the operator
        // When the action is called, it will apply a force to the plate 
        ActionCalled = true;

        Vector3 Force = new Vector3(Random.Range(-40f, 40f), 30.0f, Random.Range(-40, 40));
        GetComponent<Rigidbody>().AddForce(Force);

        SoundManager.instance.RandomizeSfx(actionSound1, actionSound2);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
