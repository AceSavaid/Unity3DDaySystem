using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public struct dialogueGroup
    {
        public string text;
        bool affeectedByTime;
        public TimePhaseObject timePhase;

        public string Text { get => text; set => text = value; }
    }

    [SerializeField] public List<dialogueGroup> dialogue;
    [SerializeField] int dilogueIndex;
    bool canTrigger;


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && canTrigger) {
            FindObjectOfType<DialogueSystem>().setDialogue(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canTrigger = true;
        Debug.Log("In range");
    }

    private void OnTriggerExit(Collider other)
    {
        canTrigger = false;
    }

}
