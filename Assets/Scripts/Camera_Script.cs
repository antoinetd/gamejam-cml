using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    //public GameObject parent;
    public GameObject kid;
    public GameObject parent;
    public float maxKidParentDistance;
    public float minFieldOfView;
   // public Transform lookAt;

    //private Transform initialTransform;
    //private Quaternion initialOrientation;
    //private Transform lookAt;
    //private Vector3 previousPosition;
    private Camera cam;
    private Transform lookAt;
    private Transform camTransform;
    private Vector3 initialRelativePosition;
    private Vector3 initialPosition;
    private float initialFOV;

    //
    private Vector3 filtPosition;
    private float filtFOV;

    // Start is called before the first frame update
    void Start()
    {
        //camTransform = GetComponent<Transform>();
        cam = GetComponent<Camera>();
        camTransform = transform;
        Vector3 camPosition = camTransform.position;
        initialPosition = camPosition;
        filtPosition = initialPosition;
        initialRelativePosition = camPosition - kid.transform.position;
        initialFOV = cam.fieldOfView;
        filtFOV = initialFOV;
        //cam.fieldOfView = 62.0f;

        //lookAt = kid.transform;
        //camTransform.in
        //Debug.Log(initialRelativePosition);
        //Debug.Log(kid.transform.position);
        //Debug.Log(transform.position);

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    // Late update called after player positions updated
    void LateUpdate()
    {
        Vector3 curKidPos = kid.transform.position;
        Vector3 curParPos = parent.transform.position;
        float kidParentDist = (curKidPos - curParPos).magnitude;
        Vector3 targetPosition;
        float targetFOV;
        float FOVScale;
       if (kidParentDist > maxKidParentDistance)
        {
            targetFOV = initialFOV;
            targetPosition = initialPosition;
        }
       else
        {
            targetPosition = 0.5f * curKidPos + 0.5f * curParPos + initialRelativePosition;
            // Just a clamp
            FOVScale = Mathf.Max(Mathf.Min(kidParentDist / maxKidParentDistance, 1.0f), minFieldOfView / initialFOV);
            targetFOV = FOVScale * initialFOV;        
        }
        filtPosition = 0.9f * filtPosition + 0.1f * targetPosition;
        filtFOV = 0.9f * filtFOV + 0.1f * targetFOV;
        cam.fieldOfView = filtFOV;
        camTransform.position = filtPosition;
    }

}

