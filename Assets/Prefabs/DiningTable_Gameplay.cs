using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningTable_Gameplay : MonoBehaviour, IInteractable
{
    // Plate Game Objects to make explode when the action is called
    public GameObject Plate;
    public bool ActionCalled; // Debug Output
    public AudioClip actionSound1;  
    public AudioClip actionSound2;

    // Private variable for the plate rigid body
    private Rigidbody PlateRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the debug output
        ActionCalled = false;

        // Find the rigid body component of the plate
        PlateRigidBody = Plate.GetComponent<Rigidbody>();
    }


    // Action to be called by the operator
    // When the action is called, it will apply a force to the plate 
    public void OnAction()
    {
        ActionCalled = true;

        Vector3 Force = new Vector3(20.0f, 30.0f, 20.0f);
        PlateRigidBody.AddForce(Force);

        SoundManager.instance.RandomizeSfx(actionSound1, actionSound2);
    }
}
