using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : MonoBehaviour
{
    public Vector3 velocity = new Vector3(0, 0, 10);
    public float tiempoDeVida = 3f;

    bool bActivo = false;

    public void Activar()
    {
        bActivo = true;
        gameObject.SetActive(true);
    }

    void Update() 
    {
        if (bActivo) 
        {
            transform.Translate(velocity * Time.deltaTime);
            tiempoDeVida -= Time.deltaTime;
            if (tiempoDeVida <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
