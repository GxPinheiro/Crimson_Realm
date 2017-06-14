using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

    public float degreesPerFrame;
    public float startRotation;
    public float endRotation;
    private float currentAngle;

    public int damageToGive;
    private bool damageGiven;

	// Use this for initialization
	void Start () {
        currentAngle = startRotation;
        transform.Rotate(0, 0, startRotation);
        damageGiven = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(currentAngle >= endRotation ) { 
            transform.Rotate(0, 0, degreesPerFrame);
            currentAngle += degreesPerFrame;
        } else { 
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy" && !damageGiven) {
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            damageGiven = true;
        }
    }

}
