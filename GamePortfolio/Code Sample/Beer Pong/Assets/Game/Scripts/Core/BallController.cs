using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : PlayerSpawnPositions
{
    public AudioClip[] audioClips;
    public float throwStrengh;
    public CameraController cam;
    public Slider slider;
    public bool ballHasThrown = false;
    public bool redTurn = true;

    private AudioSource audioSource;
    private Rigidbody ballRb;
    private SphereCollider sphereCollider;

    private Vector3 spawnPos;
    private Vector3 defaultCamPos;
    private Quaternion spawnRot;
    private Quaternion defaultCamRot;
    private float turnSpeed;

    private void OnEnable()
    {
        CheckCurrentPlayer();
        ballRb.isKinematic = true;
        ballRb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        cam.Unparent();
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
        ballRb = GetComponent<Rigidbody>();
        spawnPos = transform.position;
        spawnRot = transform.rotation;
    }
    
    void CheckCurrentPlayer()
    {
        if(tag == "Team Red")
        {
            redTurn = true;
        }
        else if(tag == "Team Blue")
        {
            
            redTurn = false;
        }
        Debug.Log("Current Player is: " + name.ToString());
    }

    void Update()
    {
        SetSpeed();
        HandleSlider();
        Rotate();
        HorizontalMovement();
        GetThrowStrengh();
        EndTurn();
    }
    private void FixedUpdate()
    {
        RotateInAir();
    }

    void RotateInAir()
    {
        if(ballHasThrown)
        {
            transform.Rotate(30 * Time.deltaTime, 0, 0);
        }
    }

    void HandleSlider()
    {
        slider.value = throwStrengh;
    }

    void GetThrowStrengh()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !ballHasThrown)
        {
            audioSource.PlayOneShot(audioClips[0]);
        }
        if (Input.GetKey(KeyCode.Space) && !ballHasThrown)
        {
            throwStrengh += 0.25f * Time.deltaTime;
        }
        if (throwStrengh > 0.6f && !ballHasThrown)
        {
            ThrowBall();
        }
        if(Input.GetKeyUp(KeyCode.Space) && !ballHasThrown)
        {
            ThrowBall();
        }
    }

    void SetSpeed()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            turnSpeed = 75;
        }
        else
        {
            turnSpeed = 10;
        }
    }

    void Rotate()
    {
        if (!ballHasThrown)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Rotate(25 * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(-25 * Time.deltaTime, 0, 0);
            }
            Quaternion q = transform.rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            transform.rotation = q;
        }
    }

    void HorizontalMovement()
    {
        if (!ballHasThrown)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            if(tag == "Team Blue")
            {
                horizontalMovement = -horizontalMovement;
            }

            transform.Translate(1 * horizontalMovement / 2 * Time.deltaTime, 0, 0, Space.World);

            // Player bounds
            if (transform.position.x > 0.4f)
            {
                transform.position = new Vector3(0.4f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -0.4f)
            {
                transform.position = new Vector3(-0.4f, transform.position.y, transform.position.z);
            }
        }
    }

    void ThrowBall()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClips[1]);
        sphereCollider.enabled = true;
        ballRb.isKinematic = false;
        ballRb.useGravity = true;
        ballRb.AddForce(transform.forward * throwStrengh);
        ballHasThrown = true;
    }

    public void EndTurn()
    {
        if(Input.GetKeyDown(KeyCode.R) && ballHasThrown)
        {
            throwStrengh = 0;
            ballHasThrown = false;
            ballRb.useGravity = false;
            ballRb.isKinematic = true;

            sphereCollider.enabled = false;
            transform.position = spawnPos;
            transform.rotation = spawnRot;
            cam.ResetCameraTransform();
        }
    }
}
