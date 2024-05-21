using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private DayCycleSystem dayCycleSystem;
    [System.Serializable]
    public struct DayPreset
    {
        [Tooltip("The custom object that stores the data of the day. You can create your own or use one of the preset ones.")]
        public TimePhaseObject timePhase;
        [Tooltip("The colour that you want to set the textbox to be.")]
        public Color colour;
        [Tooltip("If the background of the textbox should change, place the image here.\nLeave blank if no change.")]
        public Image textboxImage;
    }

    [Tooltip("Lists of all potential times of day that the dialogue box should change")]
    [SerializeField] private List<DayPreset> dayPresets = new List<DayPreset>();
    private DayPreset curDayPreset;
    [Tooltip("Image that is to be used as the background for the text.")]
    [SerializeField] private Image uiTextBox;
    [Tooltip("Text field being used for dialogue.")]
    [SerializeField] private TMP_Text text;

    private Dialogue dialogue;
    private Dialogue.dialogueGroup dialogueGroup;
    
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
            if (group.timePhase == dayCycleSystem.CurTimeNote.timePhase)
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

        uiTextBox.color = curDayPreset.colour;
        
        if(curDayPreset.textboxImage!= null)
        {
            uiTextBox.sprite = curDayPreset.textboxImage.sprite;
        }
        

    }
    void DisplayText()
    {
        uiTextBox.gameObject.SetActive(true);
        text.text= dialogueGroup.Text ;

        dialogueOpen = true;
    }

    void HideText()
    {
        uiTextBox.gameObject.SetActive(false);
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
