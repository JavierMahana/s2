using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParametrosDeNivel", menuName = "Herramientas/Par�metros de Nivel")]
public class ParametrosNivelSO : ScriptableObject
{
    public float tiempoTotal;
    public bool nivelCompletado;
}