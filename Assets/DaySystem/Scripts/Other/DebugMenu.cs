using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    DayCycleSystem cycleSystem;
    DialogueSystem dialogueSystem;

    [SerializeField] Slider timeSpeed;
    [SerializeField] TMP_Text phaseTimeText;
    [SerializeField] TMP_Text dayTimeText;

    private void Awake()
    {
        
    }
}
