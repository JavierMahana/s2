using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParametrosDeNivel", menuName = "Herramientas/Parámetros de Nivel")]
public class ParametrosNivelSO : ScriptableObject
{
    public float tiempoTotal;
    public bool nivelCompletado;
}