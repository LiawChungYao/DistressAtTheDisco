using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    private PlayerMovement _player;
    public GameObject enemyProjectilePrefab; 
    public Transform enemyShootPoint;
    private Vector3 _aimDirection;
    private Rigidbody _rigidbody;
    public Metronome time;
    private int currBeat = 0;
    private float smoothTime = 0.2f;
    private int Note = 4;
    private int Bar = 1;
    private int Beat = 0;
    private float range = 10f;
    private bool isAction = false;
    private bool start = false;
    private float animationSpeedPercent = 0.5f;
    public float speedSmoothTime = 0.1f;
    public float enemyShootForce = 30f;

    private enum state
    {
        Move,
        Charge,
        Attack
    }
    private state _state;

    private float speed = 8;
    private int count = 0;
    Animator animator;
    private Vector3 nextMove;
    private int isAttack = 0; 
    void Start(){
        animator = GetComponent<Animator>();
        
    }
    void Awake(){
        this._player  = FindObjectOfType<PlayerMovement>();
        this._rigidbody = GetComponent<Rigidbody>();
    }
    void Update(){
        Vector3 displacementFromTarget = _player.transform.position - transform.position;
        Vector3 lookDir = _player.transform.position;
        lookDir.y = transform.position.y;
        _aimDirection = displacementFromTarget.normalized;
        if (isAction){
            if (inRange(displacementFromTarget)){
                if (isAttack == 2) {
                    animationSpeedPercent = 1f;
                    transform.LookAt(lookDir);
                    attack(_aimDirection);
                    isAttack = 0;
                } else {
                    animationSpeedPercent = 0f;
                    isAttack++;
                    transform.LookAt(lookDir);
                }
            } else {
                animationSpeedPercent = .5f;
                Vector3 newPos = transform.position + GetDirection(displacementFromTarget);
                transform.LookAt(newPos);
                transform.position = newPos;
            }
        } 
        animator.SetFloat ("speedPercent", animationSpeedPercent);
        isAction = GetBeat();
        
    }

    bool inRange (Vector3 displacementFromTarget){
        if (displacementFromTarget.magnitude < range) {
            return true;
        }
        return false;
    }

    Vector3 GetDirection(Vector3 displacementFromTarget){ 
        if (Mathf.Abs(displacementFromTarget.x) > Mathf.Abs(displacementFromTarget.z)){
            if (displacementFromTarget.x > 0){
                return Vector3.right;
            }
            else
            {
                return Vector3.left;
            }
        }
        else
        {
            if (displacementFromTarget.z > 0)
            {
                return Vector3.forward;
            }
            else
            {
                return Vector3.back;
            }
        }
    }
    public bool GetBeat()
    {
        currBeat = time.count / Note;
        if (currBeat != Beat)
        {
            Beat = currBeat;
            return true;
            if (Beat%Bar == 0){
            }
        }
        return false;
    }
    public void GetMetronome(Metronome metronome)
    {
        this.time = metronome;
    }
    public void attack(Vector3 aimDir){
        Vector3 shootOrigin = enemyShootPoint.transform.position + aimDir;
        GameObject newEnemyProjectile = Instantiate(enemyProjectilePrefab, shootOrigin, Quaternion.identity);
        Rigidbody projectileRigidbody = newEnemyProjectile.GetComponent<Rigidbody>();
        if (projectileRigidbody != null){
            projectileRigidbody.AddForce(aimDir * enemyShootForce, ForceMode.Impulse);
        } else {
            Debug.LogWarning("Enemy projectile doesn't have a rigidbody component");
        }
    }
}
    
    

    