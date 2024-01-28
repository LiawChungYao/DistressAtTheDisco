using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShaderLight : MonoBehaviour
{
    [SerializeField] public GameObject Player;
    [SerializeField] private Shader shader;
    [SerializeField] private Texture texture;

    private ProjectileShooter bullet;
    private MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        bullet = Player.GetComponent<ProjectileShooter>();
        // Store renderer reference
        this._renderer = gameObject.GetComponent<MeshRenderer>();

        // Assign custom shader
        this._renderer.material.shader = this.shader;
        this._renderer.material.mainTexture = this.texture;
    }

    // Update is called once per frame
    /*
    void Update()
    {
        PointLight pointLight = bullet.getBullet().GetComponent<PointLight>();
        if (pointLight != null){
            // Pass updated light colour to shader
            this._renderer.material.SetColor("_PointLightColor",
                pointLight.GetColor());

            // Pass updated light position to shader
            this._renderer.material.SetVector("_PointLightPosition",
                pointLight.GetWorldPosition());
        }
    }
*/
    public void GetPlayer(GameObject player){
        Player = player;
        
    }
}
