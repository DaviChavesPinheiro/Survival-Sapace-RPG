using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{   
    public event Action onInteract;
    public void interact(){
        if(onInteract != null) onInteract();
    }
}
