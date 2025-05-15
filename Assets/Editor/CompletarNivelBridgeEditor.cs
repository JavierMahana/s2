
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class CompletarNivelBridgeEditor
{
    public static void Completar()
    {
        var window = EditorWindow.GetWindow<ParametrosDeNivelWindow>();
        if (window != null)
        {
            window.MarcarNivelComoCompletado();
        }
        else
        {
            Debug.LogWarning("No se encontró la ventana ParametrosDeNivelWindow.");
        }
    }
}
#endif