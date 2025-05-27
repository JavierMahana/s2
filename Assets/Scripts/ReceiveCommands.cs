using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Comando 
{
    CAMINAR_ANTES_INTERSECCION  = 0,
    CAMINAR_MEDIO_INTERSECCION = 1,
    GIRAR_IZQUIERDA =3,
    GIRAR_DERECHA = 4,
    ESPERAR_LUZ_VERDE = 5,
}

public class ReceiveCommands : MonoBehaviour
{

    Animator animator;
    WalkForward wf;

    List<Comando> ListaDeComandos;

    bool caminandoHastaAntesInterseccion = false;
    bool caminandoHastaMedioInterseccion = false;
    bool esperandoLuzVerde = false;


    public void AgregarComando(Comando nuevoComando) 
    {
        
        ListaDeComandos.Add(nuevoComando);
    }

    public void IniciarEjecucionComandos()
    {
        StartCoroutine(EjecutarTodosLosComandos());
    }

    private IEnumerator EjecutarTodosLosComandos()
    {
        foreach (Comando comando in ListaDeComandos)
        {
            EjecutarComando(comando);
            // Espera hasta que el comando se haya terminado de ejecutar (no esté ejecutando)
            yield return new WaitUntil(() => !IsExecutingCommand());
        }

        ListaDeComandos.Clear(); // Limpiar la lista después de ejecutar todos los comandos
    }

    public void EjecutarComando(Comando comando)
    {
        switch (comando)
        {
            case Comando.CAMINAR_ANTES_INTERSECCION:
                CaminarHastaAntesInterseccion();
                break;
            case Comando.CAMINAR_MEDIO_INTERSECCION:
                CaminarHastaMedioInterseccion();
                break;
            case Comando.GIRAR_IZQUIERDA:
                GirarIzquierda();
                break;
            case Comando.GIRAR_DERECHA:
                GirarDerecha();
                break;
            case Comando.ESPERAR_LUZ_VERDE:
                EsperarLuzVerde();
                break;
        }
    }

    public bool IsExecutingCommand() 
    {
        return caminandoHastaMedioInterseccion || caminandoHastaAntesInterseccion || esperandoLuzVerde; 
    }

    public void CaminarHastaAntesInterseccion()
    {
        wf.activateMovement = true;
        caminandoHastaAntesInterseccion = true;
        animator.SetBool("Walking", true);
    }
    public void CaminarHastaMedioInterseccion()
    {
        wf.activateMovement = true;
        caminandoHastaMedioInterseccion = true;
        animator.SetBool("Walking", true);
    }
    public void GirarIzquierda() 
    {
        transform.Rotate(0, -90, 0); // Gira 90 grados a la izquierda
    }
    public void GirarDerecha() 
    {
        transform.Rotate(0, 90, 0); // Gira 90 grados a la derecha
    }
    public void EsperarLuzVerde()
    {
        esperandoLuzVerde = true;
        StartCoroutine(FinEsperarLuzVerde());
    }

    private IEnumerator FinEsperarLuzVerde()
    {
        yield return new WaitForSeconds(5f); // Por ejemplo, esperar 2 segundos
        esperandoLuzVerde = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        ListaDeComandos = new List<Comando>();
        animator = GetComponent<Animator>();
        wf = FindObjectOfType<WalkForward>();
        //CaminarHastaMedioInterseccion();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IntersectionStart>() != null)
        {
            if(caminandoHastaAntesInterseccion)
            {
                Destroy(other);
                caminandoHastaAntesInterseccion = false;
                wf.activateMovement = false;
                animator.SetBool("Walking", false);
            }
        }

        if (other.gameObject.GetComponent<IntersectionMiddle>() != null)
        {
            if (caminandoHastaMedioInterseccion)
            {
                Destroy(other);
                caminandoHastaMedioInterseccion = false;
                wf.activateMovement = false;
                animator.SetBool("Walking", false);
            }
        }

        var manejadorTrafico = other.gameObject.GetComponent<ManejadorDeTrafico>();
        if (manejadorTrafico != null)
        {
            StartCoroutine(manejadorTrafico.TriggerAutos());
        }


        var triggerWin = other.gameObject.GetComponent<TriggerWin>();
        if (triggerWin != null)
        {
            triggerWin.TriggerWinPanel();
            Destroy(gameObject);
        }


        var triggerLose = other.gameObject.GetComponent<TriggerLose>();
        if (triggerLose != null)
        {
            triggerLose.TriggerLosePanel();
            Destroy(gameObject);
        }
    }

}
