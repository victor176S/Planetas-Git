using TMPro;
using UnityEngine;

public class GameControllerPantallaFinal : MonoBehaviour
{

    public Canvas canvasPantallaFinal;

    public TextMeshProUGUI textoFinalPerder;

    public TextMeshProUGUI textoFinalGanar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          if (DataThroughScenes.instance.haPerdido == false)
        {
            canvasPantallaFinal.gameObject.SetActive(true);
            textoFinalPerder.gameObject.SetActive(false);
            textoFinalGanar.gameObject.SetActive(true);
            textoFinalGanar.text += $" con: {DataThroughScenes.instance.puntos} puntos";
        }

        else
        {
            canvasPantallaFinal.gameObject.SetActive(true);
            textoFinalPerder.gameObject.SetActive(true);
            textoFinalGanar.gameObject.SetActive(false);
            textoFinalPerder.text += $" con: {DataThroughScenes.instance.puntos} puntos";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
