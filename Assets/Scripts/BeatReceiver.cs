using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatReceiver : MonoBehaviour
{

    [SerializeField] private int Bar;  // Number of beats in a Bar
    [SerializeField] private int Note; // beatNote
    public delegate void OnBeatAction();
    public static event OnBeatAction OnBeat;
    /* beatNotes
    Semibreve = 16
    Minim = 8
    Crotchet = 4
    Quaver = 2
    Semiquaver = 1
    */

    private int Beat = 0; // Number of beats
    private int currBeat = 0; // Detect when there's a new beat
    public Metronome time; // Getting the count variable from metronome


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currBeat = time.count/Note;
        if(currBeat != Beat){
            Beat = currBeat;
            //Debug.Log(Beat);
            if (Beat%Bar == 0){
                //Debug.Log(Bar);
            }
            if (OnBeat != null) {
                OnBeat();
            }
        }
        
    }
}
