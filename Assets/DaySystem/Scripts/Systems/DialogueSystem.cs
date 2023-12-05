using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] DayCycleSystem dayCycleSystem;
    [System.Serializable]
    public struct DayPreset
    {
        public TimePhaseObject timePhase;
        public Color colour;
        public Image textboxImage;
    }

    [SerializeField] List<DayPreset> dayPresets = new List<DayPreset>();
    DayPreset curDayPreset;
    [SerializeField] Image textBox;
    [SerializeField] TMP_Text text;

    Dialogue dialogue;
    Dialogue.dialogueGroup dialogueGroup;
    
    bool dialogueOpen = false;

    public void setDialogue(Dialogue d)
    {
        dialogue= d;
        UpdateTextBox();

        DisplayText();
    }
    
    void UpdateTextBox()
    {
        foreach (Dialogue.dialogueGroup group in dialogue.dialogue)
        {
            if (group.timePhase == dayCycleSystem.curTimeNote.timePhase)
            {
                dialogueGroup = group;
                foreach(DayPreset dayPreset in dayPresets)
                {
                    if (dayPreset.timePhase == group.timePhase)
                    {
                        curDayPreset = dayPreset;
                    }
                }
            }
        }

        textBox.color = curDayPreset.colour;
        /*
        if(curDayPreset.textboxImage!= null)
        {
            textBox.sprite = curDayPreset.textboxImage.sprite;
        }*/
        

    }
    void DisplayText()
    {
        textBox.gameObject.SetActive(true);
        text.text= dialogueGroup.Text ;

        dialogueOpen = true;
    }

    void HideText()
    {
        textBox.gameObject.SetActive(false);
        text.text = string.Empty;
        dialogueOpen = false;
    }

    void Start()
    {
        if(dayCycleSystem == null)
        {
            dayCycleSystem = FindObjectOfType<DayCycleSystem>();
        }
    }
    void Update()
    {
        if(dialogueOpen && Input.GetMouseButtonDown(0))
        {
            HideText();
        }
    }
}
