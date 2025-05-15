using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using Unity.VisualScripting;

public class CompletarNivelBridge : MonoBehaviour
{
    public bool nivelCompleto = false;
     

    void Start() 
    {
        nivelCompleto = Variables.Object(this.gameObject).Get<bool>("NivelCompleto");
    }

    void Update() 
    {
        if (!nivelCompleto)
        {
            nivelCompleto = Variables.Object(this.gameObject).Get<bool>("NivelCompleto");
        }
    }
}
