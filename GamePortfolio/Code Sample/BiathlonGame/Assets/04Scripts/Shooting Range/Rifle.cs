using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    // Reference
    public Animation anim;
    public Animator animator;
    public RangeMovement rangeMovement;

    public int ammoCount;

    private float _recoveryRate = 0.5f;
    private float _nextReload;
    public bool _hasReloaded = true;



    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = true;
        _hasReloaded = true;
        ammoCount = 5;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammoCount > 0 && _hasReloaded)
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.R) && !_hasReloaded && Time.time > _nextReload && ammoCount > 0)
        {
            ReloadShot();
        }
    }

    void Shoot()
    {
        ammoCount--;
        RaycastHit hitInfo;
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(rayOrigin, out hitInfo);
        
        CheckHit(hitInfo);
        _hasReloaded = false;
        anim.Play("Rifle_Kick");
        _nextReload = Time.time + _recoveryRate;
        if(ammoCount < 1)
        {
            //ads.isAimDownSights = false;
            animator.SetBool("hasFinishedShooting", true);
            animator.SetBool("isEnteringLane", false);
            animator.SetBool("isReadyToBreak", false);
            animator.SetBool("isEnteringRange", false);
            GetComponent<MeshRenderer>().enabled = false;
            Invoke("LeaveRange", 2f);
        }
    }

    void LeaveRange()
    {
        animator.SetBool("hasFinishedShooting", false);
        rangeMovement.hasFinishedShooting = true;
        //StartCoroutine(rangeMovement.MoveToRangeExit());
        
        gameObject.SetActive(false);
    }

    void ReloadShot()
    {
        anim.Play("Rifle_Reload");
        StartCoroutine(ReloadAnimation());
    }

    IEnumerator ReloadAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        _hasReloaded = true;
    }

    void CheckHit(RaycastHit hitInfo)
    {
        if (hitInfo.collider == null)
        {
            return;
        }
        else if (hitInfo.collider.tag == "Target")
        {
            hitInfo.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }


}
