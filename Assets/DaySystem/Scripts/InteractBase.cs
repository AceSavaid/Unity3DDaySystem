using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBase : MonoBehaviour
{
    [SerializeField] bool canInteract = true;

    [System.Serializable]
    struct InteractTimeTypes
    {
        bool affeectedByTime;
        TimePhaseObject timePhase;
        
    }

    enum InteractTypes
    {
        None = 0,
        Delete = 1,
        Follow = 2,
        
    }

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
