using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollow : MonoBehaviour
{
    public Transform target;  // El objeto que la cámara debe seguir

    private Vector3 offset;         // Distancia relativa inicial
    private Quaternion rotationOffset; // Rotación relativa inicial

    void Start()
    {
        // Calcula la distancia y rotación relativa al inicio
        offset = transform.position - target.position;
        rotationOffset = Quaternion.Inverse(target.rotation) * transform.rotation;
    }

    void LateUpdate()
    {
        if (target != null) 
        {
            // Mantiene la distancia relativa rotada según la rotación actual del target
            transform.position = target.position + target.rotation * offset;

            // Mantiene la rotación relativa inicial
            transform.rotation = target.rotation * rotationOffset;
        }
    }
}
