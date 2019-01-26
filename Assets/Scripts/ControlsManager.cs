using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public float hAxis = 0.0f;
    public float vAxis = 0.0f;
    public bool button1 = false;
    public bool button2 = false;
    public bool button3 = false;

    public bool replay = false;

    public bool record = true;
    private bool recordStarted = false;

    private List<Controls> moveHistory;
    public struct Controls
    {
        public float hAxis;
        public float vAxis;
        public bool button1;
        public bool button2;
        public bool button3;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveHistory = new List<Controls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!recordStarted && record)
        {
            recordStarted = true;
        }

        Controls ctrl = new Controls();

        if (replay && moveHistory.Count > 0)
        {
            hAxis = ctrl.hAxis;
            vAxis = ctrl.vAxis;
            button1 = ctrl.button1;
            button2 = ctrl.button2;
            button3 = ctrl.button3;

            moveHistory.RemoveAt(0);
        }
        else
        {
            hAxis = Input.GetAxis("Horizontal");
            vAxis = Input.GetAxis("Vertical");
            button1 = Input.GetButton("Button1");
            button2 = Input.GetButton("Button2");
            button3 = Input.GetButton("Button3");
        }

        if (record)
        {
            moveHistory.Add(ctrl);
        }

        hAxis = ctrl.hAxis;
        vAxis = ctrl.vAxis;
        button1 = ctrl.button1;
        button2 = ctrl.button2;
        button3 = ctrl.button3;
    }

}
