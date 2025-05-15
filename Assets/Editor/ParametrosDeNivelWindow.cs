using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;




public class ParametrosDeNivelWindow : EditorWindow
{
    private ParametrosNivelSO parametrosData;
    private bool contando = false;
    private double ultimaActualizacion;

    [MenuItem("Herramientas/Parámetros de Compleción de Nivel")]
    public static void ShowWindow()
    {
        GetWindow<ParametrosDeNivelWindow>("Parámetros de Compleción de Nivel");
    }

    private void OnEnable()
    {
        EditorApplication.update += OnEditorUpdate;
    }

    private void OnDisable()
    {
        EditorApplication.update -= OnEditorUpdate;
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();

        GUI.enabled = !contando;

        if (!contando)
        {
            if (GUILayout.Button("Iniciar Tiempo"))
            {
                CrearYAsignarSO();
                contando = true;
                ultimaActualizacion = EditorApplication.timeSinceStartup;
            }
        }

        GUI.enabled = contando;

        if (contando)
        {
            if (GUILayout.Button("Detener Tiempo"))
            {
                contando = false;
            }
        }

        GUI.enabled = true;
        EditorGUILayout.Space();

        if (parametrosData != null)
        {
            EditorGUILayout.LabelField("Tiempo Acumulado (segundos):", parametrosData.tiempoTotal.ToString("F2"));
            //parametrosData.nivelCompletado = EditorGUILayout.Toggle("¿Nivel Completado?", parametrosData.nivelCompletado);
            EditorUtility.SetDirty(parametrosData);

            CompletarNivelBridge completarNivelBridge = FindObjectOfType<CompletarNivelBridge>();
            if (completarNivelBridge != null && !parametrosData.nivelCompletado)
            {
                if (completarNivelBridge.nivelCompleto)
                {
                    MarcarNivelComoCompletado();
                }
            }
        }
    }

    private void OnEditorUpdate()
    {
        if (contando && parametrosData != null)
        {
            double tiempoActual = EditorApplication.timeSinceStartup;
            double delta = tiempoActual - ultimaActualizacion;
            parametrosData.tiempoTotal += (float)delta;
            ultimaActualizacion = tiempoActual;

            EditorUtility.SetDirty(parametrosData);
        }
    }

    private void CrearYAsignarSO()
    {
        string escenaNombre = SceneManager.GetActiveScene().name;
        string carpetaPath = $"Assets/Datos/{escenaNombre}";

        if (!Directory.Exists(carpetaPath))
        {
            Directory.CreateDirectory(carpetaPath);
            AssetDatabase.Refresh();
        }

        string assetPath = $"{carpetaPath}/ParametrosDeNivel.asset";
        parametrosData = AssetDatabase.LoadAssetAtPath<ParametrosNivelSO>(assetPath);

        if (parametrosData == null)
        {
            parametrosData = ScriptableObject.CreateInstance<ParametrosNivelSO>();
            AssetDatabase.CreateAsset(parametrosData, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        parametrosData.tiempoTotal = 0f;
        parametrosData.nivelCompletado = false;
        EditorUtility.SetDirty(parametrosData);
    }

    public void MarcarNivelComoCompletado()
    {
        if (parametrosData != null)
        {
            parametrosData.nivelCompletado = true;
            contando = false;

            EditorUtility.SetDirty(parametrosData);       // Marca el asset como modificado
            AssetDatabase.SaveAssets();                   // Guarda los cambios
            AssetDatabase.Refresh();                      // Refresca el proyecto por si hay cambios visibles

            Debug.Log("Nivel marcado como completado y datos guardados.");
            ShowNotification(new GUIContent("Nivel completado"));
        }
        else
        {
            Debug.LogWarning("No hay datos cargados para el nivel.");
            ShowNotification(new GUIContent("No hay ScriptableObject cargado"));
        }
    }
}


