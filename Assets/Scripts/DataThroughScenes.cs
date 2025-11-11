using TMPro;
using UnityEngine;

public class DataThroughScenes : MonoBehaviour
{
    public static DataThroughScenes instance;
    public static string textoFinal;
    public static int puntos = 0;
    public static bool haPerdido;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        puntos = GameController.gameController.puntuacion;

        
    }

}
