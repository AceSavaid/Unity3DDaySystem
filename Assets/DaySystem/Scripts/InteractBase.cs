using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialogue;

public class InteractBase : MonoBehaviour
{
    [SerializeField] bool canInteract = true;

    [System.Serializable]
    struct InteractTimeTypes
    {
        bool affeectedByTime;
        TimePhaseObject timePhase;
        InteractTypes interactTypes;
    }

    enum InteractTypes
    {
        None = 0,
        Delete = 1,
        Hide = 2,
        
    }
    [SerializeField] List<InteractTimeTypes> interactTimes;

    public virtual void Interact()
    {

    }

    public void UpdateInteractCheck()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }
    }

}
