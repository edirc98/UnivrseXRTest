using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private struct TimeSection {
        public string sectionName;
        public float startTime;
        public float endTime;
        public float sectionDuration;
        public void NewSection(string name,float start, float end)
        {
            sectionName = name; 
            startTime = start;
            endTime = end;  
        }
        public void SectionDuration()
        {
            sectionDuration = endTime - startTime;
        }
        
    }

    private float timer; 
    public float timerTime
    {
        get { return timer; }
        set
        {
            timer = value;
        }
    }
    private List<TimeSection> sections = new List<TimeSection>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddTimeSection(string name, float start, float end)
    {
        TimeSection timeSection = new TimeSection();
        timeSection.NewSection(name,start, end);
        timeSection.SectionDuration();
        sections.Add(timeSection); 
    }

    public void AddToTimer(float newTime)
    {
        timerTime += newTime; 
    }

    public void PrintLastSection()
    {
        Debug.Log("Section Name: " + sections[sections.Count - 1].sectionName + "\n" +
              "Section Duration: " + sections[sections.Count - 1].sectionDuration );
    }
}
