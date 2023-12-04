using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBase : MonoBehaviour
{
    [SerializeField] bool canInteract = true;

    public virtual void Interact()
    {

    }

}
