using TMPro;
using UnityEngine;

public class InfoCatcher : MonoBehaviour
{

    public static InfoCatcher infoCatcher;
    public string textoFinalString;
    public TextMeshProUGUI textoFinal;

    void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (DataThroughScenes.haPerdido == true)
        {
            //para llamar a un valor estatico desde otro archivo, hay que poner primero
            //la clase y luego directamente el valor, no hace falta poner un 
            //.instance o similar, los estaticos se conservan entre escenas, pero
            //pueden dar problemas si quieres usar nombres similares
            textoFinalString = $"Has perdido con {DataThroughScenes.puntos} puntos";

        }

        else
        {
            textoFinalString = $"Has ganado con {DataThroughScenes.puntos} puntos";
        }
        
        textoFinal.text = textoFinalString; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
