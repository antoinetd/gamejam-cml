using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    public AudioClip[] soundEfx;
    public AudioClip[] music; 
    public GameObject _audioManager;
    AudioSource _audioSource;   

    // Use this for initialization
    void Awake()
    {
        _audioSource = this.GetComponent<AudioSource>();

        // Singleton
        DontDestroyOnLoad(this.gameObject);
    } 
    
}


