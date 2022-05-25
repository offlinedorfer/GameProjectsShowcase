using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonLook : MonoBehaviour
{

    public Transform playerBody;
    public GameObject fpsCam;

    private float _mouseSensitivity = 300.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float xLook = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        playerBody.transform.Rotate(new Vector3(0, xLook, 0));

        if(Input.GetKeyDown(KeyCode.V))
        {
            SwitchToFPS();
        }
    }

    public void SwitchToFPS()
    {
        fpsCam.SetActive(true);
        gameObject.SetActive(false);
    }
}
