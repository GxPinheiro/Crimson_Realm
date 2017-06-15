using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float moveVelocity;
    public float jumpHeight;

    public Transform groundCheck; //verificador de chao
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool doubleJumpPowerUpGot;
    private bool doubleJumped;

    private Animator anim;

    public Transform firePoint;
    public GameObject ninjaStar;
    public GameObject chargedStar;
    public GameObject ninjaSword;

    public float chargeTime;
    private float chargeTimeCounter;

    private bool facingRight;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        chargeTimeCounter = 0;
        facingRight = true;
        doubleJumpPowerUpGot = false;
	}

    void FixedUpdate () {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
	
	// Update is called once per frame
	void Update () {

        if (grounded) {
            doubleJumped = false;
        }

        anim.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.Space) && grounded){
            jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded && doubleJumpPowerUpGot){
            jump();
            doubleJumped = true;
        }

        moveVelocity = 0f;

        if (Input.GetKey(KeyCode.RightArrow)){
            // GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = moveSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow)){
           // GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = -moveSpeed;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

        if (GetComponent<Rigidbody2D>().velocity.x > 0) { 
            transform.localScale = new Vector3(1f, 1f, 1f);
            facingRight = true;
        } else if (GetComponent<Rigidbody2D>().velocity.x < 0) { 
            transform.localScale = new Vector3(-1f, 1f, 1f);
            facingRight = false;
        }

//        if (Input.GetKeyDown(KeyCode.Z)) {
//            GameObject ninjaStarInstance = Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
//            AudioSource.PlayClipAtPoint(ninjaStarInstance.GetComponent<AudioSource>().clip, ninjaStarInstance.GetComponent<Transform>().position);
//        }

        if (Input.GetKey(KeyCode.Z)) {
            chargeTimeCounter += Time.deltaTime;
            if(chargeTimeCounter >= chargeTime) {
                chargeTimeCounter = chargeTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z)) {
            if (chargeTimeCounter >= chargeTime){
                GameObject chargedStarInstance = Instantiate(chargedStar, firePoint.position, firePoint.rotation);
                AudioSource.PlayClipAtPoint(chargedStarInstance.GetComponent<AudioSource>().clip, chargedStarInstance.GetComponent<Transform>().position);
            }
            chargeTimeCounter = 0;               
        }

        if (Input.GetKeyDown(KeyCode.Z)){
            if(GameObject.FindGameObjectsWithTag("Sword").Length <= 0) {
                GameObject ninjaSwordInstance = Instantiate(ninjaSword, firePoint.position, ninjaSword.transform.rotation);
                if (!facingRight)
                    ninjaSwordInstance.transform.localScale = new Vector3(-ninjaSwordInstance.transform.localScale.x, ninjaSwordInstance.transform.localScale.y, ninjaSwordInstance.transform.localScale.z);
                ninjaSwordInstance.transform.parent = gameObject.transform;
                AudioSource.PlayClipAtPoint(ninjaSwordInstance.GetComponent<AudioSource>().clip, ninjaSwordInstance.GetComponent<Transform>().position);
            }
        }
    }

    public void jump() {

        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        GetComponents<AudioSource>()[1].Play();

    }

    public void activateDoubleJumpPowerUP() {
        doubleJumpPowerUpGot = true;
    }

}
