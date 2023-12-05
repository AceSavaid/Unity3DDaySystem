using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimePhase", menuName = "ScriptableObjects/TimePhase", order = 1)]
public class TimePhaseObject : ScriptableObject
{
    [SerializeField] public string phaseName;
    [SerializeField] public Color colour;
}
