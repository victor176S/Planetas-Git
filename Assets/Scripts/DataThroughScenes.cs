using TMPro;
using UnityEngine;

public class DataThroughScenes : MonoBehaviour
{
    public static DataThroughScenes instance;
    public string textoFinalString;
    public TextMeshProUGUI textoFinal;
    public int puntos = 0;
    public bool haPerdido;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        puntos = GameController.gameController.puntuacion;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}
