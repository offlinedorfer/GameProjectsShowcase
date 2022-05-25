using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SpinStars : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.1f * Time.deltaTime, 0.1f * Time.deltaTime, 0);
    }
}
