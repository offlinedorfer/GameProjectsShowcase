using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeHandler : MonoBehaviour
{
    public ShootingLane[] shootingLanes;
    public GameObject firstPersonCam;
    public GameObject thirdPersonCam;
    public GameObject rifle;

    public int penaltyCount;

    private GameObject _penaltyExitBarrier;
    private GameObject _penaltyBarrier;
    private PlayerMovement _playerMovement;
    private CharacterConditions _characterConditions;
    private RangeMovement _rangeMovement;
    private FollowWP _followWP;
    private ShootingLaneContainer _SLC;

    private int _shooterID;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rangeMovement = GetComponent<RangeMovement>();
        _followWP = GetComponent<FollowWP>();
        _characterConditions = GetComponent<CharacterConditions>();
        
        _penaltyExitBarrier = GameObject.Find("Penalty Exit Barrier");
        _penaltyBarrier = GameObject.Find("Penalty Barrier");
        _SLC = FindObjectOfType<ShootingLaneContainer>().GetComponent<ShootingLaneContainer>();
        shootingLanes = _SLC.shootingLanes;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Entrance")
        {
            _shooterID = Random.Range(0,shootingLanes.Length);
            EnterSR();
        }
        else if(other.tag == "Exit")
        {
            ExitSR();
        }
        else if(other.tag == "Midround Trigger")
        {
            OpenTracker();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Penalty Tracker")
        {
            _penaltyBarrier.GetComponent<Collider>().isTrigger = false;
            penaltyCount--;
            if (penaltyCount == 0)
            {
                _penaltyExitBarrier.SetActive(false);
            }
        }
    }

    private void EnterSR()
    {
        _rangeMovement.enabled = true;
        _playerMovement.enabled = false;
        _rangeMovement.AssignLaneID(_shooterID);
    }

    private void ExitSR()
    {
        _rangeMovement.waypoints.Clear();
        if(this.tag != "Player")
        {
            _followWP.currentWP = 0;
            _followWP.hasFinishedShooting = true;
            _followWP.hasFinishedPenalties = false;
        }

        _rangeMovement.anim.SetBool("hasFinishedShooting", false);
        _rangeMovement.anim.SetBool("isLeavingRange", false);
        _playerMovement.enabled = true;
        _rangeMovement.enabled = false;

        penaltyCount = shootingLanes[_shooterID].GetPenaltyCount();
        // Fix Later
        if (penaltyCount == 0)
        {
            _penaltyExitBarrier.SetActive(false);
        }
        else
        {
            _penaltyExitBarrier.SetActive(true);
        }


    }

    private void OpenTracker()
    {
        _penaltyBarrier.GetComponent<Collider>().isTrigger = true;
    }

    public void AIShoting()
    {
        StartCoroutine(ShootTargets());
    }

    private IEnumerator ShootTargets()
    {
        foreach (GameObject target in shootingLanes[_shooterID].targets)
        {
            if(Random.Range(0, 100) <= _characterConditions.a_Shooting)
            target.GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(2);
        }
        _rangeMovement.anim.SetBool("hasFinishedShooting", true);
        _rangeMovement.anim.SetBool("isEnteringRange", false);
        _rangeMovement.anim.SetBool("isReadyToBreak", false);
        _rangeMovement.anim.SetBool("isEnteringLane", false);
        _rangeMovement.hasFinishedShooting = true;
    }

    public void StartShooting()
    {
        firstPersonCam.SetActive(true);
        thirdPersonCam.SetActive(false);
        rifle.SetActive(true);
    }
}
