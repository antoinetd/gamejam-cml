using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAction : MonoBehaviour, IInteractable


{
    public bool ActionCalled; // Debug Output
    public AudioClip actionSound1;
    public AudioClip actionSound2;
    public Vector3 ForceVector;

    public void OnAction()
    {
        // Action to be called by the operator
        // When the action is called, it will apply a force to the plate 
        ActionCalled = true;

        GetComponent<Rigidbody>().AddRelativeForce(ForceVector);

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
