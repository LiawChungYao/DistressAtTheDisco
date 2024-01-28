using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private int speed = 400; // Speed note is travelling
    [SerializeField] private int MinXOffset = 960; // Edge of the screen
    [SerializeField] private ScreenNotes crotchetPrefab;
    [SerializeField] private Canvas notesUI;

    public Metronome time;
    private ScreenNotes temp;
    private int Note;
    private int Bar;
    private int xOffset;
    private int Beat;
    private int currBeat = 0;
    public float pause;

    public void Awake(){
        this.Note = time.beatNote;
        this.Bar = 4; // Change if Notes are different
        updateBeat(time.BPM, this.Note);
    }

    public void Update(){
        currBeat = time.count/Note;
        if(currBeat != Beat){
            Beat = currBeat;
            //OnBeat();
            //Debug.Log(Beat);
            if (Beat%Bar == 0){
                //Debug.Log(Bar);
            }
        }
    }

    public void OnBeat (){
        temp = Instantiate(this.crotchetPrefab,  new Vector3(0,0,0), Quaternion.identity);
        temp.transform.SetParent(notesUI.transform);
        temp.initialiseVariables(speed, 1, pause, xOffset);

        temp = Instantiate(this.crotchetPrefab,  new Vector3(0,0,0), Quaternion.identity);
        temp.transform.SetParent(notesUI.transform);
        temp.initialiseVariables(speed, -1, pause, xOffset);
    }

    public void updateBeat(int BPM, int beatNote){
        this.pause = (1.0f* 60 / BPM) / beatNote;
        xOffset = 0;
        int count = 1;

        // Calculates the offset making sure it is outside the canvas at 600 pixels
        while (xOffset < MinXOffset){
            xOffset = (int)(speed * (beatNote * pause * count));
            count++; 
        }
        
        Debug.Log("Offset is " + xOffset);
    }
}
