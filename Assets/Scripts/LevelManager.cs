using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;
    private PlayerController player;

    public GameObject deathParticle;
    public GameObject respawnParticle;

    public int pointPenaltyOnDeath;

    public float respawnDelay;

    private CameraController gameCamera;

    private float playerGravityStore;

    public HealthManager healthManager;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        gameCamera = FindObjectOfType<CameraController>();
        healthManager = FindObjectOfType<HealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void respawnPlayer() {
        StartCoroutine("respawnPlayerCo");
    }

    public IEnumerator respawnPlayerCo() {
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        gameCamera.isFollowing = false;
        ScoreManager.addPoints(-pointPenaltyOnDeath);
        Debug.Log("Player Respawn!");
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        player.gameObject.SetActive(true);
        player.transform.position = currentCheckpoint.transform.position;
        player.knockbackCount = 0; //sem isso ele da respawn sofrendo um knockback do nada
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        healthManager.fullHealth();
        healthManager.isDead = false;
        gameCamera.isFollowing = true;
        Instantiate(respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }

}
