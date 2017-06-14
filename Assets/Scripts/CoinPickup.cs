using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

    public int pointsToAdd;

    public GameObject coinParticle;

    public AudioSource coinSound;


    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<PlayerController>() != null) {
            ScoreManager.addPoints(pointsToAdd);
            coinSound.Play();
            Destroy(gameObject);
            Instantiate(coinParticle, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
