using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignPlayer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        AlignToSurface();
    }

    private void AlignToSurface()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Quaternion targetRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 6);

        // Ignore Y Rotation
        Quaternion q = transform.localRotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, 0, q.eulerAngles.z);
        transform.localRotation = q;
    }
}

