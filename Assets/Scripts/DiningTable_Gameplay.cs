using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningTable_Gameplay : MonoBehaviour, IInteractable
{
    // Plate Game Objects to make explode when the action is called
    public GameObject Plate;
    public bool ActionCalled; // Debug Output

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

    // Update is called once per frame
    // For debugging purposes
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            OnAction();
        }
    }

    // Action to be called by the operator
    // When the action is called, it will apply a force to the plate 
    public void OnAction()
    {
        ActionCalled = true;

        Vector3 Force = new Vector3(20.0f, 30.0f, 20.0f);
        PlateRigidBody.AddForce(Force);
    }
}
