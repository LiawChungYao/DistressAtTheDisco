using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{


    public GameObject primary_projectilePrefab; // Regular bullet
    public GameObject secondary_projectilePrefab; // Music note to be shot when beat is hit
    public Camera playerCamera;
    public Transform shootPoint; // Invisible game component, where gun 'barrel' will be. Shots come from here
    public float shootForce = 50f;
    public float fireRate = 0.5f;
    public float projectileLifetime = 3f; // Bullets destroyed after this amount of time
    public float maxSpreadAngle = 0.5f; // Allows for some bullet spread

    /*
        maxAdjustmentDistance is a bit confusing. Without this, shooting an an object up close will never be accurate
        due to the shootPoint being slightly off centre from the middle of the camera. Without this, shots will just go
        towards the centre of the screen at the horizon. 
        Shots still up close will be slightly right of the cross hair by design.
    */
    public float maxAdjustmentDistance = 500f;
    public float beatToleranceWindow = 0.5f; // Amount of time around a beat that the player has to click shoot
    private float nextFireTime = 0.1f;

    private float beatWindowStart = 0f;
    private float beatWindowEnd = 0f;
    public bool isAlive = true;

    private GameObject bullet;

    void Start()
    {
        BeatReceiver.OnBeat += OnBeatHandler;
    }
    void OnDestroy()
    {
        BeatReceiver.OnBeat -= OnBeatHandler;
    }

    void OnBeatHandler()
    {
        // Player must shoot between these times to shoot on beat
        beatWindowStart = Time.time - beatToleranceWindow;
        beatWindowEnd = Time.time + beatToleranceWindow;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
            {
                if (Time.time >= beatWindowStart && Time.time <= beatWindowEnd)
                { // Is the click in beat window
                    ShootProjectile(secondary_projectilePrefab); // If player is on beat, they will shoot secondary bullet
                    
                    GameManager.instance.AddScore(20);
                }
                else
                {
                    
                    ShootProjectile(primary_projectilePrefab); // If they are not on beat, they will shoot primary bullet   
                    GameManager.instance.AddScore(-5); 
                }
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void ShootProjectile(GameObject bulletPrefab)
    {

        // Calculate the direction the shot should go towards
        Vector3 shootDirection = (playerCamera.transform.forward + playerCamera.transform.rotation * Vector3.forward).normalized;

        // Calculate and add the random spread of bullets to the direction
        Vector3 dispersion = Random.insideUnitCircle * Mathf.Tan(maxSpreadAngle * Mathf.Deg2Rad);
        shootDirection += dispersion;

        // Shoot a ray to see how far away surface is. This is used to negate the impact of shooting up close
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, shootDirection, out hit, maxAdjustmentDistance))
        {
            float distanceAdjustment = hit.distance / maxAdjustmentDistance;
            shootDirection = Vector3.Lerp(shootDirection, hit.point - shootPoint.position, distanceAdjustment);
        }

        // Create bullet and shoot it
        GameObject newProjectile = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Debug.Log(newProjectile);
        bullet = newProjectile;
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
        if (projectileRigidbody != null)
        {
            projectileRigidbody.AddForce(shootDirection.normalized * shootForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Projectile doesn't have a rigidbody component");
        }

        // After duration, delete the bullet
        Destroy(newProjectile, projectileLifetime);
    }

    public GameObject getBullet(){
        return bullet;
    }

}
