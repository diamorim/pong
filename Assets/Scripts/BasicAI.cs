using UnityEngine;

public class BasicAI : MonoBehaviour
{
    [Header("Machine Properties")]
    [SerializeField] private Rigidbody2D rig;
    [Header("Ball Properties")]
    [SerializeField] private GameObject ball;
    [Header("Machine Settings")]
    [SerializeField] private float speed = 5f;
    [Header("AI instructions")]
    [SerializeField] private float distanceToReact = 5f;
    [SerializeField] private float MovingLimitation = 3.2f;
    [SerializeField] private float MovingLimitationSpeed = 1f;
    
    private bool readyToHit = false;

        

    private void FixedUpdate()
    {

        BasicMovement();
        //else
        //{
        //    MoveToCenter();
        //}
    }

    void BasicMovement()
    {
        float ballPosition = ball.transform.position.y;
        float objPosition = transform.position.y;
        float ballDistance = transform.position.x - ball.transform.position.x;
        if (ballDistance <= distanceToReact)
        {
            if (objPosition >= MovingLimitation || objPosition <= -MovingLimitation
                || objPosition == ballPosition)
            {
                rig.linearVelocityY = 0.1f * -Mathf.Sign(objPosition);
            }
            if (ballPosition > objPosition)
            {
                rig.linearVelocityY = speed;
            }
            else if (ballPosition < objPosition)
            {
                rig.linearVelocityY = speed * -1;
            }
        }
        else
        {
            if (objPosition >= MovingLimitation || objPosition <= -MovingLimitation
                || objPosition == ballPosition)
            {
                rig.linearVelocityY = MovingLimitationSpeed * -Mathf.Sign(objPosition);
            }
            else if (ballPosition > objPosition)
            {
                rig.linearVelocityY = speed * -1;
            }
            else if (ballPosition < objPosition)
            {
                rig.linearVelocityY = speed;
            }
        }
    }
}
