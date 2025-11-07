using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public Canvas loseCanvas;

    public Canvas infoCanvas;

    public static GameController gameController;

    public TextMeshProUGUI targetNameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private List<string> opcionesBuscar = new List<string>()
    {
        "Tierra", "Jupiter", "Marte", "Mercurio", "Neptuno", "Saturno", "Sol", "Urano", "Venus"
    };
    private string targetABuscar;
    //ya no bajan aun acertando (fallo de los nombres ingles-español)
    public int vidas = 3;
    //ahora suben correctamente (fallo de los nombres ingles-español)
    private int puntuacion = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Victor: start");
        generaSiguienteTarget();
        Debug.Log("Victor: antes UI");
        ActualizaUI();
    }

    void ActualizaUI()
    {
        Debug.Log("Victor: Actualiza UI");
        targetNameText.text = "Busca " + targetABuscar;
        livesText.text = "Tienes " + vidas + " vidas.";
        scoreText.text = "Puntos: " + puntuacion;
    }

    public void OnTargetFound(String targetReconocido)
    {

        Debug.Log("Victor: " + targetReconocido + " " + targetABuscar);
 
        if (targetReconocido == targetABuscar)
        {
            //El jugador acierta
            
            puntuacion++;
            generaSiguienteTarget();
        }
        else
        {
            //No ha acertado. Ha escaneado otra imagen
            vidas--;
            Debug.Log("Victor: Decremento vidas= " + vidas);
            if (vidas == 0)
            {
                //GameOver
                GameOver();
            }
        }

        if (vidas >= 15)
        {

            WinScreen();

        }

        ActualizaUI();

    }

    void generaSiguienteTarget()
    {
        Debug.Log("Victor: siguiente target");
        int posAleatoria = Random.Range(0, opcionesBuscar.Count);
        targetABuscar = opcionesBuscar[posAleatoria];
    }

    void GameOver()
    {
        HideInfo();
        Lose();
        targetNameText.text = "Gameover";

        if (targetNameText.text == "Gameover")
        {
            Debug.Log("se deberia cambiar el Target por Gameover");
        }
    }

    void WinScreen()
    {
        HideInfo();
        targetNameText.text = "Has ganado";
    }

    public void Lose()
    {
        Debug.Log("Victor: entrada a la funcion Lose");
        if (vidas <= 0)
        {
            Debug.Log("Victor: Se detectó que no hay vidas");
            loseCanvas.gameObject.SetActive(true);
        }
    }
    public void HideInfo()
    {
        Debug.Log("Victor: Entrada a la funcion HideInfo");
        if (vidas <= 0)
        {   
            Debug.Log("Victor: Se detectó que no hay vidas");
            infoCanvas.gameObject.SetActive(true);
        }
    }
}
