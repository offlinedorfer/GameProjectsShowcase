using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeMovement : MonoBehaviour
{
    public Animator anim;

    private ShootingRangeHandler SRHandler;


    public bool hasFinishedShooting;
    public List<Vector3> waypoints;

    private float _speed;
    private int _laneID;
    private int _currentWP;
    private bool _laneIsReached;

    private void Awake()
    {
        SRHandler = GetComponent<ShootingRangeHandler>();
    }

    private void OnEnable()
    {
        _laneIsReached = false;
        hasFinishedShooting = false;
        _speed = 8;
    }

    public void AssignLaneID(int ShooterID)
    {
        _laneID = ShooterID;
        GetWaypoints();
        _currentWP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(!_laneIsReached)
        FollowWaypoint(5, _laneID, waypoints);
        */

        if(!_laneIsReached)
            GoToShootingLane();

        if (hasFinishedShooting)
            GoToRangeExit();
    }


    // Code execution
    // Get all waypoint on activation
    private void GetWaypoints()
    {
        Transform shootingLane = SRHandler.shootingLanes[_laneID].transform;
        Vector3 target1 = new Vector3(shootingLane.transform.position.x + 1, this.transform.position.y, shootingLane.position.z - 10);
        Vector3 target2 = new Vector3(shootingLane.position.x, this.transform.position.y, shootingLane.position.z);
        Vector3 target3 = new Vector3(shootingLane.transform.position.x - 6, this.transform.position.y, shootingLane.position.z - 8);
        Vector3 target4 = new Vector3(-745.99f, transform.position.z, 761.76f);
        waypoints.Add(target1);
        waypoints.Add(target2);
        waypoints.Add(target3);
        waypoints.Add(target4);
    }

    // Move and Check
    private void GoToShootingLane()
    {
        FollowWP();
        // first WP
        if(Vector3.Distance(this.transform.position, waypoints[0]) < 0.5f)
        {
            anim.SetBool("isEnteringRange", true);
            anim.SetBool("isReadyToBreak", true);
        }

        // 2nd WP

        if(Vector3.Distance(this.transform.position, waypoints[1]) < 0.5f)
        {

            _speed = 0;
            anim.SetBool("isEnteringLane", true);
            _laneIsReached = true;
            if(gameObject.tag == "Player")
            {
                // Start Shooting
                StartCoroutine(ActivateRifle());
            }
            else
            {
                SRHandler.AIShoting();
            }
        }
    }

    private void GoToRangeExit()
    {
        anim.SetBool("hasFinishedShooting", true); 
        FollowWP();
        _speed = 8;
        anim.SetBool("isLeavingRange", true);
    }


    IEnumerator ActivateRifle()
    {
        // Start Shooting
        float rotAmount = Vector3.SignedAngle(this.transform.forward, -SRHandler.shootingLanes[_laneID].transform.up, Vector3.up);
        transform.Rotate(0, rotAmount, 0);
        yield return new WaitForSeconds(2f);
        SRHandler.StartShooting();
    }

    private void FollowWP()
    {
        if (Vector3.Distance(this.transform.position, waypoints[_currentWP]) < 0.5f)
        {
            _currentWP++;
        }

        Vector3 currentTarget = new Vector3(waypoints[_currentWP].x, this.transform.position.y, waypoints[_currentWP].z);
        Quaternion lookAtWP = Quaternion.LookRotation(currentTarget - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtWP, 5f * Time.deltaTime);
        this.transform.Translate(0, 0, _speed * Time.deltaTime);
    }

}
