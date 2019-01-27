using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SceneMgr : MonoBehaviour
{
    public float gtimer = 120f;
    private int frameCounter = 0;
    private bool doSwitch = false;
    public float currentTimer = 120f;
    List<KidControls.SimpleTransform> history_backup;

    private void Start()
    {
        currentTimer = gtimer;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;

        if ((Input.GetButtonDown("ChangeToParent") || currentTimer < 0f) && doSwitch == false)
        {
            KidControls kidControls = (KidControls)FindObjectOfType(typeof(KidControls));
            // If we are already in parent mode do nothing
            if (kidControls.recording == false && kidControls.replaying == true)
            {
                return;
            }

            history_backup = new List<KidControls.SimpleTransform>();
            foreach (KidControls.SimpleTransform item in kidControls.history)
            {
                history_backup.Add(item);
            }
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

            doSwitch = true;
        }

        if (doSwitch)
        {
            frameCounter++;
        }

        if (frameCounter == 10)
        {
            KidControls kidControls = (KidControls)FindObjectOfType(typeof(KidControls));

            KidControls kidControlsNewScene = (KidControls)FindObjectOfType(typeof(KidControls));
            ParentManualControls parentControls = (ParentManualControls)FindObjectOfType(typeof(ParentManualControls));
            AI_Ctrl ai_ctrl = (AI_Ctrl)FindObjectOfType(typeof(AI_Ctrl));

            if (kidControlsNewScene != null)
            {
                kidControls.history = history_backup;
                kidControlsNewScene.recording = false;
                kidControlsNewScene.replaying = true;
            }
            if (parentControls != null)
            {
                parentControls.activateManualControls = true;
                var navMesh = parentControls.gameObject.GetComponent<NavMeshAgent>();
                if (navMesh != null) navMesh.enabled = false;
            }
            if (ai_ctrl != null)
            {
                ai_ctrl.enableParent = false;
                var bc = ai_ctrl.GetComponent<BoxCollider>();
                if (bc != null) bc.enabled = true;
                var cc = ai_ctrl.GetComponent<CapsuleCollider>();
                if (cc != null) cc.enabled = true;
            }
            GameManager_Scoring.GetInstance().isAdult = true;
        }
    }

    public void doChangeScene(int someScene)
    {
        currentTimer = gtimer;
        switch (someScene)
        {
            case 0:
                SceneManager.LoadScene(0);
                break;

            case 1:
                SceneManager.LoadScene(1);
                break;

            case 2:
                SceneManager.LoadScene(2);
                break;
        }

    }
}
