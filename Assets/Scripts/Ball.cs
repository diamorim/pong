using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Rigidbody2D rig;
    [Header("Attributes")]
    [SerializeField] private float speed = 5f;

    private int xDirection = -1;
    private int yDirection = 1;


    private void FixedUpdate()
    {
        //ball moves at speed
        rig.linearVelocity = new Vector2(speed * xDirection, speed * yDirection);
    }

    //if hit player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IdWall idWall;
        if (collision.gameObject.layer == 7)
        {
            print("touched the Player");
            //reflects the ball
            xDirection *= -1;
            //gain speed
            speed += 0.5f;
        }
        if (collision.gameObject.layer == 8)
        {
            print("touched the roof");
            yDirection *= -1;
        }
        if (collision.gameObject.layer == 9)
        {
            idWall = collision.gameObject.GetComponent<IdWall>();
            GameManager.instance.GameOver(idWall.id);
            print("game over");
        }
    }


}
