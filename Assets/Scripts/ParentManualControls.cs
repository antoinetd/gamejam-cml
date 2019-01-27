using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentManualControls : MonoBehaviour
{
    public bool activateManualControls = false;

    public float movementSpeed = 0.2f;
    public float turnSpeed = 60.0f;

    public float exhausted = 0.0f;
    public float stamina = 3.0f;
    public float runningSpeedMultiplier = 3.0f;
    public float exhausedSpeedMultiplier = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateManualControls)
        {
            return;
        }

        
        if (Mathf.Abs(transform.rotation.eulerAngles.x) > 45 || Mathf.Abs(transform.rotation.eulerAngles.z) > 45)
        {
            var rot = transform.rotation;
            rot.x = 0f;
            rot.z = 0f;
            transform.rotation = rot;
        }

            bool isRunning = false;
        if (Input.GetButton("Button1") || Input.GetButton("Button2") || Input.GetButton("Button3") || Input.GetButton("Button4"))
        {
            if (stamina >= -1)
            {
                stamina -= Time.deltaTime;
                if (stamina < -1)
                {
                    exhausted = 2.0f;
                }
            }
            if (stamina >= 0)
            {
                isRunning = true;
            }
        }
        else if (stamina <= 2.0f)
        {
            stamina += Time.deltaTime;
        }

        float moveForward = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;

        if (isRunning)
        {
            moveForward *= runningSpeedMultiplier;
            rotation *= runningSpeedMultiplier;
        }
        if (exhausted > 0)
        {
            exhausted -= Time.deltaTime;
            moveForward *= exhausedSpeedMultiplier;
            rotation *= exhausedSpeedMultiplier;
        }

        this.transform.Translate(new Vector3(0.0f, 0.0f, moveForward));
        this.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), rotation);
    }
}
