using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayCycleSystem : MonoBehaviour
{
    [System.Serializable]
    public struct TimeNote
    {
        public float duration;
        public bool slowTransition;
        public TimePhaseObject timePhase;
        
    }
    
    public enum LoopState
    {
        NoLoop,
        LoopAtEndOfTime,
        LoopAtEndOfPhases,
    }
    

    [Header("Time Notes")]
    [SerializeField] LoopState loop = LoopState.NoLoop;

    [SerializeField] float totalTime;
    [SerializeField] private float secondsBetweenTimeTicks = 1;
    [SerializeField] List<TimeNote> timeNotes = new List<TimeNote>();
    public TimeNote curTimeNote;

    private bool progressTime = true;
    private int currentTimeNoteIndex = 0;
    private float dayTimer = 0;
    private float currentPhaseTimer = 0;

    [Header("Lighting")]
    [SerializeField] bool handlesLights = false;
    Color currentColour= Color.white;
    [SerializeField] List<Light> sceneLights = new List<Light>();

    [Header("UI")]
    [SerializeField] bool handlesUI = false;
    [SerializeField] Slider timeSlider;
    [SerializeField] TMP_Text timeOfDayText;

    [Header("Interactions")]
    [SerializeField] bool handlesInterations = false;
    [SerializeField] List<InteractBase> interacts= new List<InteractBase>();

    


    // Start is called before the first frame update
    void Start()
    {
        if (timeSlider)
        {
            timeSlider.maxValue = totalTime;
            timeSlider.value = dayTimer;
        }
        curTimeNote = timeNotes[0];
        UpdateFeatures();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();

        switch (loop)
        {
            case LoopState.NoLoop:

                break;
            case LoopState.LoopAtEndOfTime: 
                if (dayTimer >= totalTime)
                {
                    currentTimeNoteIndex = 0;
                    dayTimer= 0;
                    UpdateFeatures();
                }
                
                break;
            case LoopState.LoopAtEndOfPhases:
                if (currentTimeNoteIndex >= timeNotes.Count-1 && currentPhaseTimer >= timeNotes[currentTimeNoteIndex].duration)
                {
                    currentPhaseTimer = 0;
                    UpdateFeatures();
                }

                break;
        }


        
        if (currentPhaseTimer >= timeNotes[currentTimeNoteIndex].duration && currentTimeNoteIndex < timeNotes.Count-1)
        {
            currentTimeNoteIndex++;
            currentPhaseTimer = 0;
            UpdateFeatures();

            Debug.Log("Next Phase");
        }

        if (handlesUI && timeSlider)
        {
            timeSlider.value = dayTimer;
        }

        if (timeNotes[currentTimeNoteIndex].slowTransition)
        {
            foreach (Light light in sceneLights)
            {
                light.color = Color.LerpUnclamped(timeNotes[currentTimeNoteIndex].timePhase.colour, timeNotes[currentTimeNoteIndex + 1].timePhase.colour, currentPhaseTimer / timeNotes[currentTimeNoteIndex].duration);

            }
            
        }

    }

    void UpdateTimer()
    {
        dayTimer += Time.deltaTime * secondsBetweenTimeTicks;
        currentPhaseTimer += Time.deltaTime * secondsBetweenTimeTicks;
    }

    void UpdateFeatures()
    {
        curTimeNote = timeNotes[currentTimeNoteIndex];
        if (handlesUI)
        {
            UpdateUI();
        }

        if(handlesLights)
        {
            UpdateLights();
        }

        if(handlesInterations)
        {
            UpdateInteractions();
        }
    }
    
    void UpdateUI()
    {
        if (timeOfDayText)
        {
            timeOfDayText.text = timeNotes[currentTimeNoteIndex].timePhase.phaseName;
        }

        if (timeSlider)
        {
            timeSlider.value = dayTimer;
        }
    }

    void UpdateLights()
    {
        foreach (Light light in sceneLights)
        {
            light.color = timeNotes[currentTimeNoteIndex].timePhase.colour;
            
        }
    }


    void UpdateInteractions()
    {
        foreach (InteractBase i in interacts)
        {
            i.UpdateInteractCheck();
        }
    }
    
    public int CurrentTimeNote { get => currentTimeNoteIndex; set => currentTimeNoteIndex = value; }
    public float DayTimer { get => dayTimer; set => dayTimer = value; }
    public float CurrentPhaseTimer { get => currentPhaseTimer; set => currentPhaseTimer = value; }
    public float TotalTime { get => totalTime; set => totalTime = value; }
    public float SecondsBetweenTimeTicks { get => secondsBetweenTimeTicks; set => secondsBetweenTimeTicks = value; }

}
