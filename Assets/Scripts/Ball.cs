using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] AudioClip bounceSound;
    [SerializeField] float randomFactor = 0.5f;


    Vector2 paddleToBallVector;
    bool hasStarted;

    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockToPaddle();
            LaunchOnClick();
        }
    }

    private void LockToPaddle()
    {
        var paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush,yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hasStarted)
        {
            Vector2 velocityTweak = new Vector2(Random.Range(0, randomFactor), Random.Range(0, randomFactor));
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);

            if(collision.collider.name == "Paddle")
            {
                myAudioSource.PlayOneShot(bounceSound);
            }

            myRigidBody2D.velocity += velocityTweak;
        }
    }

 }
