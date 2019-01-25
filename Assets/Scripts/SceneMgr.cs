using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneMgr : MonoBehaviour
{
    public void doChangeScene(int someScene)
    {
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
