using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public struct dialogueGroup
    {
        [Tooltip("Text that entity should say")]
        public string text;
        //[Tooltip("Whether or not this text should be shown based on the time of day")]
        //public bool affeectedByTime;
        [Tooltip("If Affected by Time is selected, what time of day that text should be shown.")]
        public TimePhaseObject timePhase;

        public string Text { get => text; set => text = value; }
    }
    [Tooltip("Dialoge that should be said. \nNOTE WORK IN PROGRESS \n Can currently only read 1 line of dialogue.")]
    [SerializeField] public List<dialogueGroup> dialogue;
    
    private int dilogueIndex;
    private bool canTrigger;


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && canTrigger) {
            FindObjectOfType<DialogueSystem>().setDialogue(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canTrigger = false;
    }

}
