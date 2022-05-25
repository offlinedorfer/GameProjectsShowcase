using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTracker : MonoBehaviour
{

    //Text
    public Text timeTrackerText;
    public Text lapTrackerText;
    public Text finishTrackerText;

    //Variables

    public List<float> lapTimes = new List<float>();
    public float timeOnTrack;

    private bool _canStart;

    private void Awake()
    {
        StartCoroutine(WaitForStart());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Time Tracker")
        {
            //Convert2MinutesAndSeconds();
            //Convert2MinutesAndSeconds(timeOnTrack, timeTrackerText);
            lapTimes.Add(timeOnTrack);
        }
        else if(other.tag == "Finish")
        {
            StartCoroutine(DeletePlayer());
        }
    }

    IEnumerator WaitForStart()
    {
        GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(3);
        GetComponent<PlayerMovement>().enabled = true;
        _canStart = true;
    }

    IEnumerator DeletePlayer()
    {
        //Convert2MinutesAndSeconds(finishTrackerText);
        FindObjectOfType<GameManager>().GetAthleteTotalTime(this);
        GetComponent<FollowWP>().enabled = false;
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if(_canStart)
        {
            timeOnTrack += Time.deltaTime;
            timeTrackerText.text = Convert2MinutesAndSeconds(timeOnTrack);
            
            CanFinish();
        }
    }

    public bool CanFinish()
    {
        if (lapTimes.Count > 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // helper Methods

    public string Convert2MinutesAndSeconds(float seconds)
    {
        int _seconds = (int)seconds;
        int _minutes = 0;

        while (_seconds > 59)
        {
            _minutes += 1;
            _seconds -= 60;
        }

        return ConvertTime2String(_minutes, _seconds);
    }

    string ConvertTime2String(int minutes, int seconds)
    {
        if (minutes > 9 && seconds > 9)
        {
            return minutes.ToString() + ":" + seconds.ToString();
        }
        else if (minutes > 9 && seconds <= 9)
        {
            return minutes.ToString() + ":" + "0" + seconds.ToString();
        }
        else if (minutes <= 9 && seconds > 9)
        {
            return "0" + minutes.ToString() + ":" + seconds.ToString();
        }
        else if (minutes <= 9 && seconds <= 9)
        {
            return "0" + minutes.ToString() + ":" + "0" + seconds.ToString();
        }
        else
            return null;
    }
}
