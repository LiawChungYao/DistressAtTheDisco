using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ScreenNotes : MonoBehaviour
{
    [SerializeField] private int speed = 500; // Speed note is travelling
    public float pause;
    private int xOffset;
    private int beatNote;
    private Rigidbody _rigidbody;
    private float timeElapsed;
    private float lerpDuration;
    private int startValue;
    private int endValue;
    private Vector3 velocity;
    private float startTime;

    private void Awake()
    { 
        startTime = Time.time;
        this._rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision col)
    {
        // Destroy self
        Destroy(gameObject);
    }


    public void initialiseVariables(int speed, int dir, float pause, int count){
        transform.localPosition = new Vector3(speed*pause*count*(dir - dir*2),0.0f, 0.0f);
        Vector3 direction = new Vector3(0,0,0) - this.transform.localPosition;
        velocity = direction.normalized * speed;
    }

    public void Update(){
        transform.position += velocity * Time.deltaTime;
    }

}
