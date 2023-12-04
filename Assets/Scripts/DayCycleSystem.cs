using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleSystem : MonoBehaviour
{
    [System.Serializable]
    public struct TimeNote
    {
        public float duration;
        public TimePhaseObject timePhase;
        
    }

    [Header("Time Notes")]
    [SerializeField] bool loop = false;
    [SerializeField] public float secondsBetweenTimeTicks = 1;
    [SerializeField] List<TimeNote> timeNotes = new List<TimeNote>();

    [Header("UI")]
    [SerializeField] bool handlesUI = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckTimeNote()
    {

    }

    void UpdateTimeNote()
    {

    }
}
