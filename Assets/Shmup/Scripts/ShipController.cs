using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float speed = 5;

    public GameObject bulletPrefab;

    public float firingInterval = .5f;
    public float firingTimer = 0.0f;

    public int health = 3;

    public bool isVulnerable = true;

    public float invulnerableVal = 0.0f;
    private float invulnTimer = 0.0f;

    public void TakeDamage(int DamageDealt)
    {

        if (isVulnerable)
        {
            health -= DamageDealt;

            isVulnerable = false;
            invulnTimer = invulnerableVal;

            if (health <= 0)
            {
                Destroy(gameObject);
            }

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        firingTimer -= Time.deltaTime;
        invulnTimer -= Time.deltaTime;

        if(invulnTimer < 0.0f)
        {
            isVulnerable = true;
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        //deltaTime makes ship move by 5 seconds instead of 5 frames
        transform.Translate(input * speed * Time.deltaTime);

        if (firingTimer <= 0.0f && Input.GetButton("shmupFire"))
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);

            firingTimer = firingInterval;

        }
    }
}
