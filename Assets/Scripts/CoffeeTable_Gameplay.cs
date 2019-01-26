using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeTable_Gameplay : MonoBehaviour, IInteractable
{

    public bool ActionCalled; // Debug Output
    public AudioClip actionSound1;
    public AudioClip actionSound2;

    // Private variable for the table rigid body
    private Rigidbody TableRigidBody;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the debug output
        ActionCalled = false;

        // Find the rigid body component of the plate
        TableRigidBody = GetComponent<Rigidbody>();
    }

    
    // Action to be called by the operator
    // When the action is called, it will apply a force to the plate 
    public void OnAction()
    {
        ActionCalled = true;

        Vector3 Force = new Vector3(0.0f, 30.0f, 0.0f);
        Vector3 Position = new Vector3(0.75f, 0.0f, 0.75f);

        TableRigidBody.AddForceAtPosition(Force, Position);

        SoundManager.instance.RandomizeSfx(actionSound1, actionSound2);
    }
}
