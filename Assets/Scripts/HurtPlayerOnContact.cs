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

            var player = other.GetComponent<PlayerController>(); //tratamento do knockback começa aqui
            player.knockbackCount = player.knockbackDuration;//inicia contador

            //verifica se o player está a direita ou a esquerda
            if (other.transform.position.x < transform.position.x)
                player.knockFromRight = true;
            else
                player.knockFromRight = false;
        }
    }
}
