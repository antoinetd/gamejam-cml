using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidControls : MonoBehaviour
{
    public Transform rightLeg;
    public Transform leftLeg;
    public SphereCollider actionSphere;

    public float rotationSpeed = 90f;

    public bool recording = true;
    public bool replaying = false;
    
    public struct SimpleTransform
    {
        public Vector3 position;
        public Quaternion rotation;
        public bool action;
    }
    List<SimpleTransform> history;

    // Start is called before the first frame update
    void Start()
    {
        history = new List<SimpleTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (replaying)
        {

        }
        else
        {
            Transform transform = GetComponent<Transform>();
            if (Input.GetButton("Button1"))
            {
                transform.RotateAround(leftLeg.position, new Vector3(0.0f, 1.0f, 0.0f), -rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetButton("Button2"))
            {
                transform.RotateAround(rightLeg.position, new Vector3(0.0f, 1.0f, 0.0f), rotationSpeed * Time.deltaTime);
            }

            if (Input.GetButton("Button3"))
            {
                Vector3 transformPos = transform.position;
                transformPos.z = transformPos.z + 0.3f;
                Collider[] hitColliders = Physics.OverlapSphere(transformPos, 0.3f);
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    IInteractable obj = hitColliders[i].GetComponent<IInteractable>();
                    if (obj != null)
                    {
                        obj.OnAction();
                    }
                }
            }

            if (recording)
            {
                history.Add(new SimpleTransform
                            {
                                position = transform.position,
                                rotation = transform.rotation,
                                action = Input.GetButton("Button3")
                            }
                );
            }
        }
    }
}
