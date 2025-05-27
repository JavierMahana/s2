using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorDeTrafico : MonoBehaviour
{
    public float timeToDeactivateDeathZone = 5f;
    public TriggerLose loseTrigger;
    public List<Auto> listaAutos = new List<Auto>();

    //en triger enter con un objeto que tenga receive 
    public IEnumerator TriggerAutos()
    {
        foreach (var auto in listaAutos)
        {
            auto.Activar();
        }


        // Espera el tiempo definido
        yield return new WaitForSeconds(timeToDeactivateDeathZone);

        // Destruye el trigger
        if (loseTrigger != null)
        {
            Destroy(loseTrigger.gameObject); // Destruye el GameObject del trigger
        }
    }
}
