using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedStarController : MonoBehaviour
{

    public float speed;

    public PlayerController player;

    public GameObject enemyDeathEffect;

    public GameObject chargedImpactEffect;

    public int pointsForKill;

    public float rotationSpeed;

    public int damageToGive;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player.transform.localScale.x < 0)
        { //if player is facing left
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);

        GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy") {
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            if (other.GetComponent<EnemyHealthManager>().enemyHealth > 0) {
                Instantiate(chargedImpactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            
        }
        if (other.tag != "Player" && other.tag != "Enemy") { 
            Instantiate(chargedImpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
