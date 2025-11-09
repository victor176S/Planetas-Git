using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Canvas failCanvas;

    public AudioSource failSound;

    public Canvas infoCanvas;

    public static GameController gameController;

    public TextMeshProUGUI targetNameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI targetFoundText;

    public TextMeshProUGUI failText;

    private List<string> opcionesBuscar = new List<string>()
    {
        "Tierra", "Jupiter", "Marte", "Mercurio", "Neptuno", "Saturno", "Sol", "Urano", "Venus"
    };
    private string targetABuscar;
    //ya no bajan aun acertando (fallo de los nombres ingles-español)
    public int vidas = 3;
    //ahora suben correctamente (fallo de los nombres ingles-español)
    public int puntuacion = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        failCanvas.gameObject.SetActive(false);
    }

    void Start()
    {
        Debug.Log("Victor: start");
        generaSiguienteTarget();
        Debug.Log("Victor: antes UI");
        ActualizaUI();

    }

    public void ActualizaUI()
    {
        Debug.Log("Victor: Actualiza UI");
        targetNameText.text = "Busca " + targetABuscar;
        livesText.text = "Tienes " + vidas + " vidas.";
        scoreText.text = "Puntos: " + puntuacion;
       
        if (puntuacion >= 15)
        {

            WinScreen();

        }

        if (vidas <= 0)
        {

            GameOver();

        }
    }

    public void OnTargetFound(String targetReconocido)
    {
        Debug.Log("Victor: " + targetReconocido + " " + targetABuscar);

        if (targetReconocido == targetABuscar)
        {
            failCanvas.gameObject.SetActive(false);
            //El jugador acierta
            targetFoundText.text = targetReconocido;
            puntuacion++;
            generaSiguienteTarget();


        }

        else
        {
            //No ha acertado. Ha escaneado otra imagen
            vidas--;
            failCanvas.gameObject.SetActive(true);
            failText.text = $"Has escaneado {targetReconocido}, necesitas encontrar: {targetABuscar}";
            targetFoundText.text = targetReconocido;
            Debug.Log("Victor: Decremento vidas= " + vidas);
            //SIEMPRE QUE SE QUIERA REPRODUCIR UN SONIDO HAY QUE INICIAR CON UNA CORUTINA
            StartCoroutine(ReproducirSonidoFallo());
            StartCoroutine(CambioDeVidasTimer());
            
         
        }
        
        ActualizaUI();
    }

    public void generaSiguienteTarget()
    {
        Debug.Log("Victor: siguiente target");
        int posAleatoria = Random.Range(0, opcionesBuscar.Count);
        targetABuscar = opcionesBuscar[posAleatoria];
    }

    void GameOver()
    {
        if (vidas <= 0)
        {
            Debug.Log("Victor: Game Over");
            DataThroughScenes.instance.haPerdido = true;
            HideInfo();
        }
    }

    private IEnumerator ReproducirSonidoFallo()
    {
        failSound.Play();
        yield return new WaitForSeconds(3);

    }
    
    private IEnumerator CambioDeVidasTimer()
    {
        if(Timer.timer.targetTime <= 0f)
        {
            vidas -= 1;
            ActualizaUI();
        }


        yield return new WaitForSeconds(1);
    }
        

    void WinScreen()
    {
        DataThroughScenes.instance.haPerdido = false;
        Debug.Log("Victor: Has ganado");
        HideInfo();
    }
    public void HideInfo()
    {
        Debug.Log("Victor: Entrada a la funcion HideInfo");
        if (vidas <= 0)
        {   
            
            Debug.Log("Victor: Se detecto que no hay vidas");
            infoCanvas.gameObject.SetActive(false);
            failCanvas.gameObject.SetActive(false);
            SceneManager.LoadScene("Pantalla Final");
        }
    }
}
