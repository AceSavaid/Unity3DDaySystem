using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimePhase", menuName = "ScriptableObjects/TimePhase", order = 1)]
public class TimePhaseObject : ScriptableObject
{
    [Tooltip("Name of that phase of the day.")]
    [SerializeField] public string phaseName;
    [Tooltip("Colour that the lighting will be during that time of day.")]
    [SerializeField] public Color colour;
}
