using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Scene3Manager : MonoBehaviour
{

    

    public ScriptMachine accesoScriptMachine;

    public TextMeshProUGUI textoDescripcion;
    public GameObject panelAccesoEntregado;
    public GameObject panelAccesoDenegado;

    public string descripcion;
    public string textoErrorAccesoDenegadoAVecino;
    public string textoErrorEdad;
    public string textoErrorBarrio;
    public string textoErrorDelitos;
    public string textoVictoria;



    public PersonaAsamblea persona1;
    public PersonaAsamblea persona2;
    public PersonaAsamblea persona3;
    public PersonaAsamblea persona4;
    public PersonaAsamblea persona5;



    void Start()
    {
        persona1.gameObject.SetActive(false);
        persona2.gameObject.SetActive(false);
        persona3.gameObject.SetActive(false);
        persona4.gameObject.SetActive(false);
        persona5.gameObject.SetActive(false);

        StartCoroutine(EvaluarPersonas());
    }

    IEnumerator EvaluarPersonas()
    {
        textoDescripcion.gameObject.SetActive(true);
        textoDescripcion.text = descripcion;
        yield return new WaitForSeconds(8f);
        textoDescripcion.gameObject.SetActive(false);

        PersonaAsamblea[] personas = { persona1, persona2, persona3, persona4 , persona5};
        bool todasCorrectas = true;

        foreach (var persona in personas)
        {
            persona.gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);

            ModificarVariablePermitirAcceso(persona);

            yield return null;


            bool accesoVisualScripting = LeerVariablePermitirAcceso();

            if (accesoVisualScripting)
            {
                panelAccesoEntregado.SetActive(true);
            }
            else 
            { 
                panelAccesoDenegado.SetActive(true);
            }
            yield return new WaitForSeconds(1.5f);
            panelAccesoEntregado.SetActive(false);
            panelAccesoDenegado.SetActive(false);   

            bool accesoCorrecto = CheckPersona(persona);

            if (accesoVisualScripting != accesoCorrecto)
            {
                if (accesoCorrecto == true)
                {
                    textoDescripcion.text = textoErrorAccesoDenegadoAVecino;
                }
                textoDescripcion.gameObject.SetActive(true);
                todasCorrectas = false;
                persona.gameObject.SetActive(false);
                yield break;
            }

            persona.gameObject.SetActive(false);
        }

        if (todasCorrectas)
        {
            textoDescripcion.text = textoVictoria;
            textoDescripcion.gameObject.SetActive(true);
        }
    }


    public void ModificarVariablePermitirAcceso(PersonaAsamblea persona)
    {
        var args = new object[] { persona.gameObject };
        CustomEvent.Trigger(accesoScriptMachine.gameObject, "PermitirAcceso", args);
    }

    public bool LeerVariablePermitirAcceso()
    {
        return Variables.Object(accesoScriptMachine.gameObject).Get<bool>("AccesoConcedido");
    }


    public bool CheckPersona(PersonaAsamblea persona) 
    {
        //verifica si la edad es mayor a 15
        if (persona.edad < 16)
        {
            textoDescripcion.text = textoErrorEdad;
            return false;
        }
        //verifica si el barrio es correcto
        if (persona.barrio != "Las Americas")
        {
            textoDescripcion.text = textoErrorBarrio;
            return false;
        }
        //verifica si la cantidad de delitos es correcta
        if (persona.cantidadDelitos > 0)
        {
            textoDescripcion.text = textoErrorDelitos;
            return false;
        }
        //si todo es correcto
        return true;
    }





}
