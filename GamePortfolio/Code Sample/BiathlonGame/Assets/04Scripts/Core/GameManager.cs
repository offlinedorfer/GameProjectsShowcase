using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject baseAI;

    public float timeScale;
    public Text[] totalNames;
    public Text[] totalResults;

    public GameObject[] Athletes;

    public List<FinTime> totalTime;
    

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Wait());
    }
    /*
    IEnumerator Wait()
    {
        foreach (GameObject athlete in Athletes)
        {
            Instantiate(athlete);
            yield return new WaitForSeconds(30);
        }  
    }*/
    
    public void GetAthleteTotalTime(TimeTracker Athlete)
    {
        int index = 0;

        FinTime athleteFinTime = new FinTime(Athlete.name, Athlete.timeOnTrack);
        athleteFinTime.athleteName = Athlete.name;
        athleteFinTime.finTime = Athlete.timeOnTrack;

        totalTime.Add(athleteFinTime);

        totalTime.Sort((x, y) => x.finTime.CompareTo(y.finTime));

        foreach(FinTime time in totalTime)
        {
            totalNames[index].text = time.athleteName;
            totalResults[index].text = Athlete.Convert2MinutesAndSeconds(time.finTime);
            index++;
        }

    }


    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
    }
}
