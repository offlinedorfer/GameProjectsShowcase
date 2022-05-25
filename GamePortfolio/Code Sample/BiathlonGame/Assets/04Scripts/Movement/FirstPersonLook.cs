using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{

    public Transform playerBody;
    public GameObject rifle;
    public GameObject thirdPersonCam;

    private float _mouseSensitivity = 300.0f;
    private float _xRotation;
    private float _timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();

        if (rifle.activeSelf == true)
            AddPerlinNoise();

        if(Input.GetKeyDown(KeyCode.V))
            SwitchToThirdPerson();
    }

    public void SwitchToThirdPerson()
    {
        thirdPersonCam.SetActive(true);
        gameObject.SetActive(false);
    }

    private void RotatePlayer()
    {
        float xLook = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float yLook = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= yLook;
        _xRotation = Mathf.Clamp(_xRotation, -90.0f, 90.0f);

        playerBody.transform.Rotate(new Vector3(0, xLook, 0));
        transform.localRotation = Quaternion.Euler(new Vector3(_xRotation, 0, 0));
    }

    #region Perlin Noise

    private void AddPerlinNoise()
    {
        _timeCounter += Time.deltaTime;
        transform.Rotate(GetVerctor3());
    }

    Vector3 GetVerctor3()
    {
        return new Vector3(GetFloat(1), GetFloat(10), 0);
    }

    private float GetFloat(float seed)
    {
        return (Mathf.PerlinNoise(seed, _timeCounter) - 0.5f) * 2;
    }

    #endregion
}
