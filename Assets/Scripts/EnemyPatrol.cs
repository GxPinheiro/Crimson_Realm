using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPatrol : MonoBehaviour {

    public float moveSpeed;
    public bool moveRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;

    private bool notAtEdge;
    public Transform edgeCheck;

    private Transform unturnableContainer;

    // Use this for initialization
    void Start () {

        unturnableContainer = transform.Find("UnturnableContainer");

    }

    // Update is called once per frame
    void Update() {

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall || !notAtEdge) {
            moveRight = !moveRight;
            transform.Rotate(0, 180, 0);
            correctThingsThatCanNotTurn();
        }

        if (moveRight) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        } else {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

    }

    //faz com que elementos filhos (children) dentro do objeto UnturnableContainer fiquem com sua rotação fixa
    private void correctThingsThatCanNotTurn() {
        unturnableContainer.rotation = Quaternion.Euler(0, 0, 0);
    }  

}

