using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private LayerMask playerLayer;
    [Header("Attributes")]
    [SerializeField] private float speed;

    //ball moves at speed
    private void FixedUpdate()
    {
        rig.linearVelocity = new Vector2(speed, speed);
    }

    //if hit player
    //reflects it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == playerLayer)
        {

        }

    }
    //gain speed
    //if hit ground
    //reflects 
    //if hit wall
    //end game

}
