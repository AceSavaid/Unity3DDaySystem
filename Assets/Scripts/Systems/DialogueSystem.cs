using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    [System.Serializable]
    public struct DayPreset
    {
        TimePhaseObject timePhase;
        Color colour;
        Image textboxImage;
    }

    [SerializeField] List<DayPreset> dayPresets = new List<DayPreset>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
