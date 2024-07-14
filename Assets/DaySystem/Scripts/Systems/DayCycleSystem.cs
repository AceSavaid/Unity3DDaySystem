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
        [Tooltip("How long that phase should take.")]
        public float duration;
        [Tooltip("Should the lighting change slowly from the previous lighting to this phase's?")]
        public bool slowTransition;
        [Tooltip("The custom object that stores the data of the day. You can create your own or use one of the preset ones.")]
        public TimePhaseObject timePhase;
        
    }
    
    public enum LoopState
    {
        NoLoop,
        LoopAtEndOfTime,
        LoopAtEndOfPhases,
    }
    

    [Header("Time Notes")]
    [Tooltip("No Loop stops the day processes from looping after it reaches the end. \n" +
        "Loop At End of Time will cause the daytime processes to loop of Total Time. \n" +
        "Loop At End of Phases will cause the day time processes to loop after the last timephase on the list concludes.")]
    [SerializeField] LoopState loop = LoopState.NoLoop;

    //time calculation
    [Tooltip("Total duration that the day should take before it loops in seconds")]
    [SerializeField] float totalTime;

    [Tooltip("Default is 1.\nAny multipliers to affect how quickly or slowly time passes in seconds.")]
    [SerializeField] private float secondsBetweenTimeTicks = 1;
    [Tooltip("List of all of the phases of the day you want to have.")]
    [SerializeField] List<TimeNote> timeNotes = new List<TimeNote>();
    private TimeNote curTimeNote;

    private bool progressTime = true; //used when you wish to pause the timer
    private int currentTimeNoteIndex = 0;
    private float dayTimer = 0;
    private float currentPhaseTimer = 0;

    [Header("Lighting")]
    [SerializeField] bool handlesLights = false;
    Color currentColour= Color.white;
    [SerializeField] List<Light> sceneLights = new List<Light>();

    [Header("UI")]
    [Tooltip("Should this system handle any UI features?")]
    [SerializeField] bool handlesUI = false;
    [Tooltip("A slider/guage that you wish to display the time of day on (like a clock or timer).")]
    [SerializeField] Slider timeSlider;
    [Tooltip("A text feild that you wish to display the name of the current time of day.")]
    [SerializeField] TMP_Text timeOfDayText;

    [Header("Interactions")]
    [Tooltip("Should this system handle objects that changes based on the time of day?")]
    [SerializeField] bool handlesInterations = false;
    [Tooltip("List of objects that change based on the day. \n Needs the Interact Base Script Attached or a variation of it.")]
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
        if(progressTime) UpdateTimer();

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

        }

        if (handlesUI && timeSlider)
        {
            timeSlider.value = dayTimer;
        }

        if (timeNotes[currentTimeNoteIndex].slowTransition)
        {
            foreach (Light light in sceneLights)
            {
                light.color = Color.LerpUnclamped(timeNotes[currentTimeNoteIndex].timePhase.colour, 
                    timeNotes[currentTimeNoteIndex + 1].timePhase.colour, 
                    currentPhaseTimer / timeNotes[currentTimeNoteIndex].duration);

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
    public TimeNote CurTimeNote { get => curTimeNote; set => curTimeNote = value; }
    public bool ProgressTime { get => progressTime; set => progressTime = value; }
}
