using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    // Floats
    public float skiingSpeed;
    public float slopeForce;
    public float resSpeed;
    private float helpAngle;

    private float _gravity = -9.81f;
    private Vector3 _gravityVel;
    #endregion

    //References
    private CharacterController _cc;
    private CharacterConditions _characterConditions;
    private Slider speedSlider;

    #region AnimationVars
    public Animator anim;
    public AnimState animState;

    public enum AnimState
    {
        OneOne,
        TwoOne,
        TwoOneUphill,
        Downhill,
        breaking
    }
    #endregion

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _characterConditions = GetComponent<CharacterConditions>();
        speedSlider = GetComponentInChildren<Slider>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        SetSlider();
        LimitSlopeForce();
        AddSlopeForce();
        ApplyFriction(0.7f);
        ApplyGravity();
        SetMovementStyle();
    }

    private void LateUpdate()
    {
        MovePlayer();
    }

    void AddSlopeForce()
    {
        // Raycasting
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down * 5, out hit);
        Vector3 normalVector = hit.normal;

        // Angles
        float a = Vector3.Angle(normalVector, Vector3.down);
        a = 2 * Mathf.PI * a / 360;
        float sinA = Mathf.Sin(a);
        helpAngle = Vector3.Angle(normalVector, transform.forward);

        if (helpAngle > 90f) //moving uphill
        {
            slopeForce -= Mathf.Abs(_gravity * sinA) * Time.deltaTime;
        }
        else //moving downhill
        {
            slopeForce += Mathf.Abs(_gravity * sinA * 1.2f) * Time.deltaTime;
        }
    }

    void LimitSlopeForce()
    {
        if(slopeForce < -3)
        {
            slopeForce = -3;
        }
    }
    


    void ApplyFriction(float frictionForce)
    {
        if (slopeForce > 0)
        {
            slopeForce -= frictionForce * 1.5f * Time.deltaTime;
        }
        else
        {
            slopeForce += frictionForce * Time.deltaTime;
        }
    }

    private void MovePlayer()
    {
        resSpeed = slopeForce + skiingSpeed;
        _cc.SimpleMove(transform.forward * resSpeed);
    }

    private void ApplyGravity()
    {
        _gravityVel.y += _gravity * Time.deltaTime;

        _cc.Move(_gravityVel * Time.deltaTime);
    }

    #region input
    private void SetSlider()
    {
        skiingSpeed = _characterConditions.a_skiing;
        if (Input.GetKey(KeyCode.W))
        {
            speedSlider.value += Time.deltaTime * 0.5f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speedSlider.value -= Time.deltaTime * 0.5f;
        }

        Mathf.Clamp01(speedSlider.value);
        skiingSpeed *= speedSlider.value + 1;
    }

    #endregion

    #region Movement Animation


    private void SetMovementStyle()
    {
        if (slopeForce < 2 && slopeForce > 0)
        {
            //TwoOne
            animState = AnimState.TwoOne;
            anim.SetInteger("MovementID", 2);
        }
        else if (slopeForce < -2)
        {
            //TwoOne-Uphill
            animState = AnimState.TwoOneUphill;
            anim.SetInteger("MovementID", 3);
        }
        else if (slopeForce > 2)
        {
            //Downhill
            animState = AnimState.Downhill;
            anim.SetInteger("MovementID", 4);
        }
    }

    #endregion
}