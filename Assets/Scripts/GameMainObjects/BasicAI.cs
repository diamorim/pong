using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    [Header("Machine Properties")]
    [SerializeField] private Rigidbody2D rig;
    [Header("Ball Properties")]
    [SerializeField] private GameObject ball;
    [SerializeField] private Ball ballScript;
    [Header("Machine Settings")]
    [SerializeField] private float speed = 5f;
    [Header("AI instructions")]
    [SerializeField] private float distanceToReact;
    [SerializeField] private float MovingLimitation = 3.2f;
    [SerializeField] private float MovingLimitationSpeed = 1f;


    float ballPosition;
    float positionToFollow;
    float myPosition;
    float ballDistance;
    public bool passedStartState = false;

    private void Start()
    {
        passedStartState = false;
    }

    private void Update()
    {
        ballDistance = transform.position.x - ball.transform.position.x;

        ballPosition = ball.transform.position.y;
        myPosition = transform.position.y;
        positionToFollow = Mathf.Sign(ballPosition - myPosition);

        if (passedStartState == true)
        {
            if (ballDistance >= distanceToReact)
                ChangeAIState(AIStates.farFromBall);
            else
                ChangeAIState(AIStates.closeFromBall);
        }
        else
            ChangeAIState(AIStates.start);
        
    }
    private void FixedUpdate()
    {
        ExecuteState();
    }

    enum AIStates
    {
        start,
        farFromBall,
        closeFromBall
    }

    AIStates currentState;
    void ChangeAIState(AIStates state)
    {
        currentState = state;
    }

    void ExecuteState()
    {
        switch (currentState)
        {
            case AIStates.start:
                print("Started AI");
                FirstHitOnBall();

                break;

            case AIStates.farFromBall:
                print("Mirror ball");
                MirrorBall();
                break;

            case AIStates.closeFromBall:
                print("Follow ball");
                FollowBall();
                break;
            default:
                break;
        }
    }

    void FirstHitOnBall()
    {
        if (ballScript.xDirection > 0 && transform.position.x - ball.transform.position.x >= distanceToReact)
        {
            print("AI not moving");
            rig.linearVelocityY = 0;
        }
        else
        {
            print("AI moving after starting");
            passedStartState = true;
        }


    }
    void MirrorBall()
    {
        if (transform.position.y > MovingLimitation || transform.position.y < -MovingLimitation)
        {
            if (transform.position.y > 0)
            {
                rig.linearVelocityY = -speed;
            }
            else
            {
                rig.linearVelocityY = speed;
            }
            return;
        }
        else if ((transform.position.y == MovingLimitation || transform.transform.position.y == -MovingLimitation) &&
                (ball.transform.position.y > MovingLimitation || ball.transform.position.y < -MovingLimitation))
        {
            rig.linearVelocityY = 0;
        }
        else
            rig.linearVelocityY = -positionToFollow * speed;
        //if (Mathf.Abs(myPosition) >= MovingLimitation && ballPosition >= MovingLimitation * Mathf.Sign(-positionToFollow) || myPosition == ballPosition)
        //{
        //    if (MovingLimitation * Mathf.Sign(myPosition) > myPosition)
        //        rig.linearVelocityY = speed * Mathf.Sign(-myPosition);
        //    else
        //        rig.linearVelocityY = 0f;
        //}
        //else
        //    rig.linearVelocityY = speed * -positionToFollow;
    }
    void FollowBall()
    {
        print(positionToFollow);
        rig.linearVelocityY = speed * positionToFollow;
    }
    void BasicMovement()
    {
        float ballPosition = ball.transform.position.y;
        float ballDistance = transform.position.x - ball.transform.position.x;

        float cpuPosition = transform.position.y;

        if (ballDistance <= distanceToReact)
        {
            if (cpuPosition >= MovingLimitation || cpuPosition <= -MovingLimitation
                || cpuPosition == ballPosition)
            {
                rig.linearVelocityY = 0.1f * -Mathf.Sign(cpuPosition);
            }
            if (ballPosition > cpuPosition)
            {
                rig.linearVelocityY = speed;
            }
            else if (ballPosition < cpuPosition)
            {
                rig.linearVelocityY = speed * -1;
            }
        }
        else
        {
            if (cpuPosition >= MovingLimitation || cpuPosition <= -MovingLimitation
                || cpuPosition == ballPosition)
            {
                rig.linearVelocityY = MovingLimitationSpeed * -Mathf.Sign(cpuPosition);
            }
            else if (ballPosition > cpuPosition)
            {
                rig.linearVelocityY = speed * -1;
            }
            else if (ballPosition < cpuPosition)
            {
                rig.linearVelocityY = speed;
            }
        }
    }
}
