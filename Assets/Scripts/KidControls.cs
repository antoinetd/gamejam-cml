using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidControls : MonoBehaviour
{
    // Put at the beginning
    public AudioClip actionSound1;
    public AudioClip actionSound2;

    public List<GameObject> closestsInteractables;
    public Transform rightLeg;
    public Transform leftLeg;

    public float rotationSpeed = 90f;

    public bool recording = true;
    public bool replaying = false;
    
    public struct SimpleTransform
    {
        public Vector3 position;
        public Quaternion rotation;
        public bool action;
        public bool playSound;
    }
    List<SimpleTransform> history;

    // Start is called before the first frame update
    void Start()
    {
        closestsInteractables = new List<GameObject>();
        history = new List<SimpleTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (replaying)
        {
            if (history.Count > 0)
            {
                this.transform.position = history[0].position;
                this.transform.rotation = history[0].rotation;
                if (history[0].action) DoAction();
                if (history[0].playSound)
                {
                    SoundManager.instance.RandomizeSfx(actionSound1, actionSound2);
                }

                history.RemoveAt(0);
            }
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

            Rigidbody rb = GetComponent<Rigidbody>();
            if (Input.GetButtonUp("Button1") || Input.GetButtonUp("Button2"))
            {
                rb.freezeRotation = true;
                SoundManager.instance.RandomizeSfx(actionSound1, actionSound2);
            }
            else
            {
                rb.freezeRotation = false;
            }

            if (Input.GetButton("Button3"))
            {
                DoAction();
            }

            if (recording)
            {
                history.Add(new SimpleTransform
                            {
                                position = transform.position,
                                rotation = transform.rotation,
                                action = Input.GetButton("Button3"),
                                playSound = Input.GetButtonDown("Button1") || Input.GetButtonDown("Button2")
                }
                );
            }
        }
    }

    private void DoAction()
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<IInteractable>() != null)
        {
            closestsInteractables.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (closestsInteractables.Count > 0)
        {
            closestsInteractables.Remove(collision.gameObject);
        }
    }
}
