using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollow : MonoBehaviour
{
    public Transform target;  // El objeto que la c�mara debe seguir

    private Vector3 offset;         // Distancia relativa inicial
    private Quaternion rotationOffset; // Rotaci�n relativa inicial

    void Start()
    {
        // Calcula la distancia y rotaci�n relativa al inicio
        offset = transform.position - target.position;
        rotationOffset = Quaternion.Inverse(target.rotation) * transform.rotation;
    }

    void LateUpdate()
    {
        if (target != null) 
        {
            // Mantiene la distancia relativa rotada seg�n la rotaci�n actual del target
            transform.position = target.position + target.rotation * offset;

            // Mantiene la rotaci�n relativa inicial
            transform.rotation = target.rotation * rotationOffset;
        }
    }
}
