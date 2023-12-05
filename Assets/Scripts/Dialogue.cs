using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    struct dialogueGroup
    {
        string text;
        bool affeectedByTime;
        TimePhaseObject timePhase;
    }

    [SerializeField] List<dialogueGroup> dialogue;
    [SerializeField] int dilogueIndex;


    void DisplayText()
    {
        
    }

    void HideText()
    {

    }
    
    void UpdateIndex()
    {
        dilogueIndex++;
    }
}
