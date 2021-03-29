using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClip : MonoBehaviour
{
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio.time = 45.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
