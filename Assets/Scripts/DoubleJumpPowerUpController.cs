using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPowerUpController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, GetComponent<Transform>().position);
            Destroy(gameObject); //Destroy Square
            other.GetComponent<PlayerController>().activateDoubleJumpPowerUP();
        }
    }

}
