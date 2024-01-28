using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public int BPM = 60;
    [SerializeField] public int beatNote = 4;


    public int[] ArrayBPM = {65, 84, 96, 138, 168};
    private int ArrayCount = 0;
    /* beatNotes
    Semibreve = 16
    Minim = 8
    Crotchet = 4
    Quaver = 2
    Semiquaver = 1
    */

    // Notes UI
    private ScreenNotes temp;
    [SerializeField] private int speed = 500; // Speed note is travelling
    [SerializeField] private int MinXOffset = 960; // Edge of the screen
    [SerializeField] private ScreenNotes crotchetPrefab;
    [SerializeField] private Canvas notesUI;


    // Time Interval Management
    public float pause;
    public int count; // Counts every semiquaver in the bpm
    private int beatCount; // Keeps track of how many beats have been performed
    private int pauseCount; // For the notes to instantiate outside the screen

    // Start is called before the first frame update
    private void Start()
    {
        pause = (1.0f* 60 / BPM) / beatNote;
        StartCoroutine(Rhythm());
        newInterval();
    }

    private IEnumerator Rhythm()
    {
        while (true){
            float Timer = 0;
            while (Timer < pause)
            {
                Timer += Time.deltaTime; // Not blocking!
                yield return null;
                
            }
            if (count % beatNote == 0){

                    // Disable pause for first pauseCount timez
                    audioSource.Play();
                            
                    temp = Instantiate(this.crotchetPrefab,  new Vector3(0,0,0), Quaternion.identity);
                    temp.transform.SetParent(notesUI.transform);
                    temp.initialiseVariables(speed, 1, pause, pauseCount);

                    temp = Instantiate(this.crotchetPrefab,  new Vector3(0,0,0), Quaternion.identity);
                    temp.transform.SetParent(notesUI.transform);
                    temp.initialiseVariables(speed, -1, pause, pauseCount);
                }
                count++;
        }
    }

    // Update is called once per frame
    public void newInterval()
    {
        BPM  = ArrayBPM[ArrayCount];
        ArrayCount++;
        pause = (1.0f* 60 / BPM) / beatNote; // Updates pause time
        pauseCount = 0;
        float value = 0;
        while (value < MinXOffset){
            pauseCount += beatNote;
            value = speed * pause * (pauseCount + 1);
        }
        Debug.Log(pauseCount);

    }

}
