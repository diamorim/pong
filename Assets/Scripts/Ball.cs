using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ball : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Rigidbody2D rig;
    [Header("Attributes")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedIncreasement = 0.1f;
    [SerializeField] private float platformHitWindow = 0.1f;

    private int xDirection = -1;
    private int yDirection = 1;


    private void FixedUpdate()
    {
        //ball moves at speed
        rig.linearVelocity = new Vector2(speed * xDirection, speed * yDirection);
    }

    //if hit hitboxdown
        //ball goes up
    //if hit hitboxup
        //ball goes down
    //else
        //ball goes forward
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IdWall idWall;

        //else if(collision.gameObject.layer == lowerHitbox &&  collision.gameObject.layer == upperHitbox)
        //{
        //    Debug.Log("touched the Player middle");
        //    xDirection *= -1;
        //    yDirection *= 0;
        //    speed += speedIncreasement;
        //}
        OnHitPlatform(collision);
        if (collision.gameObject.layer == 8)
        {
            yDirection *= -1;
        }
        if (collision.gameObject.layer == 9)
        {
            idWall = collision.gameObject.GetComponent<IdWall>();
            GameManager.instance.GameOver(idWall.id);
            print("game over");
        }
    }

    bool hitUpperPlatform = false;
    bool hitLowerPlatform = false;
    bool checkingHit = false;
    void OnHitPlatform(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ContactPoint2D contact = collision.GetContact(0);

            Collider2D hitbox = contact.collider;

            if (hitbox.gameObject == gameObject)
                hitbox = contact.otherCollider;

            int layer = hitbox.gameObject.layer;

            if(layer == 6)
                hitLowerPlatform = true;
            else if (layer == 7)
                hitUpperPlatform = true;

            if (!checkingHit)
                StartCoroutine(CheckHits());
        }
    }

    IEnumerator CheckHits()
    {

        checkingHit = true;

        if (hitLowerPlatform){
            ChangeDirections(-1, "touched the player down");
        }
        else if (hitUpperPlatform){
            ChangeDirections(1, "touched the player up");
        }

        yield return new WaitForSeconds(platformHitWindow);
        hitLowerPlatform = false;
        hitUpperPlatform = false;
        checkingHit = false;
    }

    void ChangeDirections(int ballDirectionY, string debugMessage)
    {
        Debug.Log(debugMessage);
        xDirection *= -1;
        yDirection = MathF.Sign(ballDirectionY);
        speed += speedIncreasement;
    }
}
