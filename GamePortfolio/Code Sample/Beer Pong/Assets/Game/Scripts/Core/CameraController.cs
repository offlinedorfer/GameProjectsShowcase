using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BallController ballController;
    public GameObject parentBall;

    private Transform _focalPoint;
    private BallController _ballController;

    private Vector3 camPos;
    private Quaternion camRot;
    private Vector3 camPosOffsetInAir;
    private Quaternion camRotOffsetInAir;
    private Vector3 unparentPosOffset;
    private Quaternion unparentRotOffset;
    private bool isUnparented = false;
    private bool hasCollided;

    // Start is called before the first frame update
    void Awake()
    {
        _ballController = GetComponentInParent<BallController>();
        camPos = new Vector3(0, 0, -0.43f);
        camRot = Quaternion.Euler(0, 0, 0);
        camPosOffsetInAir = new Vector3(0, 0.5f, -0.5f);
        camRotOffsetInAir = Quaternion.Euler(40, 0, 0);

        if(_ballController.tag == "Team Red")
        unparentPosOffset = new Vector3(0, 1, -0.5f);
        unparentRotOffset = Quaternion.Euler(60, 0, 0);

        if(_ballController.tag == "Team Blue")
        {
            print("Player Blue");
            unparentPosOffset = new Vector3(0, 1, 0.5f);
            unparentRotOffset = Quaternion.Euler(120, 0, 180);
        }
    }
    // Update is called once per frame
    void Update()
    {
        FollowBall();
    }

    public void Unparent()
    {
        if(!hasCollided)
        {
            transform.parent = null;
            isUnparented = true;
            hasCollided = true;
        }

    }

    public void ResetCameraTransform()
    {
        transform.parent = parentBall.transform;
        transform.localPosition = camPos;
        transform.localRotation = camRot;
        isUnparented = false;
        hasCollided = false;

    }
    private void FollowBall()
    {
        if (ballController.ballHasThrown && !isUnparented)
        {
            transform.localPosition = camPosOffsetInAir;
            transform.localRotation = camRotOffsetInAir;
        }
    }

}
