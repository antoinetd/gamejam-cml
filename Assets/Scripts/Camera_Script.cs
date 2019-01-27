using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    // Fields
    public GameObject kid;//connect to the kid's body
    public GameObject parent;//connect to the parent's body
    public float maxKidParentDistance;//the distance after which the camera zooms out completely. As it goes up zooming is smoother
    public float minFieldOfView;//minimum field of view used
    public Vector3 ActionOffset;
    public float showKidParDistance;

    // Some stuff
    private Camera cam;
    private Transform lookAt;
    private Transform camTransform;
    private Vector3 initialRelativePosition;
    private Vector3 initialPosition;
    private float initialFOV;

    // Variables for lowpass filters
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
            targetPosition = 0.5f * curKidPos + 0.5f * curParPos + ActionOffset;
            // Just a clamp
            FOVScale = Mathf.Max(Mathf.Min(kidParentDist / maxKidParentDistance, 1.0f), minFieldOfView / initialFOV);
            targetFOV = FOVScale * initialFOV;        
        }
        //some lowpass filters
        filtPosition = 0.9f * filtPosition + 0.1f * targetPosition;
        filtFOV = 0.9f * filtFOV + 0.1f * targetFOV;
        cam.fieldOfView = filtFOV;
        camTransform.position = filtPosition;
        showKidParDistance = kidParentDist;
    }

}

