using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDestroyAction : MonoBehaviour, IInteractable
{
    // Public Variables
    public bool ActionCalled; // Debug Output
    public GameObject ParticleObject;
    public GameObject HPBar;
    public AudioClip actionSound;
    public float ObjectInitialHealth;

    // Private Variables
    private float ObjectHealth;
    private SpriteRenderer HPBarSpriteRenderer;
    private float HPBarSpriteInitialWidth;
    private float HPBarSpriteInitialHeight;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the object health
        ObjectHealth = (ObjectInitialHealth);


        // Set the particles to be initially off
        ParticleObject.SetActive(false);

        // Prepare the health bar
        HPBarSpriteRenderer = HPBar.GetComponent<SpriteRenderer>();
        HPBarSpriteRenderer.color = Color.green;

        HPBarSpriteInitialWidth = HPBarSpriteRenderer.size.x;
        HPBarSpriteInitialHeight = HPBarSpriteRenderer.size.y;

    }

    public void OnAction()
    {
        // Each time action is called, decrease the health of the object
        if (ObjectHealth > 0)
        {
            // Update the object health
            ObjectHealth--;

            // Update the sprite size
            HPBarSpriteRenderer.size = new Vector2(HPBarSpriteInitialWidth * ObjectHealth / ObjectInitialHealth, HPBarSpriteInitialHeight);

            if (ObjectHealth <= (0.25 * ObjectInitialHealth))
            {
                HPBarSpriteRenderer.color = Color.red;
            }

        }

        if (ObjectHealth == 0)
        {
            // Action to be called by the operator
            ActionCalled = true;

            // Trigger the particle emitter
            ParticleObject.SetActive(false);
            ParticleObject.SetActive(true);

            // Trigger sound file
            SoundManager.instance.RandomizeSfx(actionSound);
        }


    }
}
