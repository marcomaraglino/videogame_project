using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour


{

    public static SoundManager Instance { get; set; }
    // Start is called before the first frame update
    public AudioSource grassWalkSound;
    public AudioSource grassRunSound;
    public void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }
    void Start()
    {
        
    }

    public void PlayMusic(AudioSource audioSource)
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
