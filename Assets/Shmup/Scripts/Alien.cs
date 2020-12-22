using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int health = 2;
    public float speed = 4.0f;

    public GameObject bulletPrefab;

    private GameObject playerReference;

    public float firingInterval = 1.0f;
    private float firingTimer;

    public float detectRadius = 8f;
    private bool playerDetected = false;

    private void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        firingTimer = firingInterval;

    }   
    private void Update()
    {
        firingTimer -= Time.deltaTime;


        if(playerDetected && playerReference != null)
        {
            Vector3 dirToPlayer = (playerReference.transform.position - transform.position).normalized;



            if(firingTimer <= 0.0f)
            {
                GameObject babyBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Bullet bullet = babyBullet.GetComponent<Bullet>();
                bullet.isFriendly = false;
                bullet.transform.forward = (playerReference.transform.position - transform.position).normalized;

                firingTimer = firingInterval;
            }

            transform.forward = dirToPlayer;
            transform.position += dirToPlayer * speed * Time.deltaTime;
        }
        else if(!playerDetected && playerReference != null)
        {  
            float disToPlayer = (transform.position - playerReference.transform.position).magnitude;
            if(disToPlayer <= detectRadius)
            {
                playerDetected = true;
            }
        }

    }

    public void TakeDamage(int damageDealt)
    {
        health -= damageDealt;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShipController ship = collision.gameObject.GetComponent<ShipController>();
        if(ship != null)
        {
            ship.TakeDamage(1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ShipController ship = collision.gameObject.GetComponent<ShipController>();
        if (ship != null)
        {
            ship.TakeDamage(1);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
