using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonaAsamblea : MonoBehaviour
{
    public int edad;
    public string nombre;
    public string barrio;
    public int cantidadDelitos;

    public TextMeshProUGUI textoEdad;
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoBarrio;
    public TextMeshProUGUI textoCantidadDelitos;

    void Start()
    {
        MostrarDatos();
    }

    void OnEnable()
    {
        MostrarDatos();
    }

    void MostrarDatos()
    {
        if (textoEdad != null) textoEdad.text = "Edad: " + edad;
        if (textoNombre != null) textoNombre.text = "Nombre: " + nombre;
        if (textoBarrio != null) textoBarrio.text = "Barrio: " + barrio;
        if (textoCantidadDelitos != null) textoCantidadDelitos.text = "Delitos: " + cantidadDelitos;
    }
}
