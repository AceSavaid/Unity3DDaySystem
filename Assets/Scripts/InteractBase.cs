using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBase : MonoBehaviour
{
    [SerializeField] bool canInteract = true;

    [System.Serializable]
    struct InteractTypes
    {
        bool affeectedByTime;
        TimePhaseObject timePhase;
        
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
