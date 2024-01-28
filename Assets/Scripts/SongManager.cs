using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public AudioClip[] audioclips;
    
    public AudioSource audioSource;
    public float[] pitch = {0.6666f,1f,1f,1f,2f};

    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        newInterval();
    }

    // Update is called once per frame
    public void newInterval(){
        audioSource.clip = audioclips[count];
        audioSource.pitch = pitch[count];
        count++;
        audioSource.Play();
    }
}
