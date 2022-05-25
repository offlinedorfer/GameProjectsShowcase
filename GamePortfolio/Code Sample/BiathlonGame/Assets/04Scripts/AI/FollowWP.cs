using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowWP : MonoBehaviour
{
    public bool hasFinishedShooting;
    public bool hasFinishedPenalties;

    private AIWaypoints _aIWaypoints;
    private RangeMovement _rangeMovement;
    private ShootingRangeHandler _SRHandler;

    public int currentWP = 0;

    [SerializeField] private float _rotSpeed = 5;

    void Awake()
    {
        _aIWaypoints = GameObject.FindObjectOfType<AIWaypoints>().GetComponent<AIWaypoints>();
        _rangeMovement = GetComponent<RangeMovement>();
        _SRHandler = GetComponent<ShootingRangeHandler>();

        hasFinishedPenalties = true;
        hasFinishedShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetermineWaypoints();
    }

    private void DetermineWaypoints()
    {
        if(hasFinishedPenalties)
        {
            FollowTrack();
        }
        else if(hasFinishedShooting)
        {
            FollowPenalty();
        }
    }

    private void FollowTrack()
    {
        if (!_rangeMovement.isActiveAndEnabled)
        {

            if (Vector3.Distance(transform.position, _aIWaypoints.trackWaypoints[currentWP].transform.position) < 3)
            {
                currentWP++;
            }

            if(currentWP == 400 && GetComponent<TimeTracker>().CanFinish())
            {
                currentWP = 403;
            }

            if (currentWP >= _aIWaypoints.trackWaypoints.Length)
            {
                currentWP = 0;
            }

            FollowWaypoint(_aIWaypoints.trackWaypoints);
        }
    }

    private void FollowPenalty()
    {
        if (!_rangeMovement.isActiveAndEnabled)
        {

            if (Vector3.Distance(transform.position, _aIWaypoints.penaltyWaypoints[currentWP].transform.position) < 3)
            {
                currentWP++;
            }

            if (currentWP >= _aIWaypoints.penaltyWaypoints.Length)
            {
                currentWP = 2;
            }

            if (_SRHandler.penaltyCount < 1 && currentWP == 2)
            {
                currentWP = 17;
                hasFinishedPenalties = true;
                hasFinishedShooting = false;
                return;
            }

            FollowWaypoint(_aIWaypoints.penaltyWaypoints);
        }
    }

    private void FollowWaypoint(GameObject[] WPGroup)
    {
        Vector3 currentTarget = new Vector3(WPGroup[currentWP].transform.position.x, transform.position.y, WPGroup[currentWP].transform.position.z);
        Quaternion lookAtWP = Quaternion.LookRotation(currentTarget - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtWP, _rotSpeed * Time.deltaTime);
    }
}

