using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public float currTime;
    private int minute;
    private int seconds;
    private bool running;

    // New Interval Changes (Add if object needs to be updated for every interval)
    private int currMin;
    public Metronome metronome;
    public SongManager song;
    public GameManager gameManager;
    public OnDeathManager onDeathManager;
    public MouseLook mouseLook;
    public RecoilScript gun1Recoil;
    public RecoilScript gun2Recoil;
    public PlayerMovement playerMovement;
    public ProjectileShooter projectileShooter;
    public HealthBar healthBar;
    public BossMovementScript bossMovementScript;
    public BossHealthManager bossHealthManager;

    // Start is called before the first frame update
    void Start()
    {
        running = true;
        minute = Mathf.FloorToInt(currTime/60);
        currMin = minute;
        seconds = Mathf.FloorToInt(currTime%60);
        Text.text = (minute.ToString() + ":" + seconds.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (running){
            if (currTime <= 1){
                running = false;
            }
            currTime -= Time.deltaTime;
            minute = Mathf.FloorToInt(currTime/60);
            seconds = Mathf.FloorToInt(currTime%60);
            if ((seconds == 0) && (currMin != minute)){
                
                Debug.Log(currMin);
                currMin = minute;
                Interval();
            }
            Text.text = string.Format("{0:0}:{1:00}", minute, seconds);
        }

        // Game ended
        else{
            Debug.Log("You waited till the end");
            onDeathManager.ShowWinningUI();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLook.canLook = false;
            gun1Recoil.enabled = false;
            gun2Recoil.enabled = false;
            playerMovement.enabled = false;
            projectileShooter.enabled = false;
            healthBar.enabled = false;
            gameManager.DisplayWinningScore();
           
        }
    }

    void Interval(){
        metronome.newInterval();
        song.newInterval();
        bossMovementScript.newInterval();
        bossHealthManager.newInterval();
    }
}
