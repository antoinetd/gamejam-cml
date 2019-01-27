using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericWaterAction : MonoBehaviour, IInteractable
{
    // Public Variables
    public bool ActionCalled; // Debug Output
    public GameObject ParticleObject;


    // Start is called before the first frame update
    void Start()
    {

        ActionCalled = false;

        // Set the particles to be initially off
        ParticleObject.SetActive(false);


    }

    public void OnAction()
    {

        // Action to be called by the operator
        ActionCalled = true;

        // Trigger the particle emitter
        ParticleObject.SetActive(true);

    }

}
