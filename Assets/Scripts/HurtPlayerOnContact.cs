﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour {

    public int damageToGive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D other) {
        if(other.name == "Player") {
            HealthManager.hurtPlayer(damageToGive);
            other.GetComponents<AudioSource>()[0].Play();
        }
    }
}
