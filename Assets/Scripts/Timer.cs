using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    public static Timer timer;
    public TextMeshProUGUI timerText;
    public float targetTime = 15.0f;

    public bool quitarVida = false;


    void Start()
    {

       
    
    }
    void Update(){

        targetTime -= Time.deltaTime;
        
        if (targetTime <= 0f)
        {

            //aqui no poner lo de las vidas, para el temporizador

            targetTime = 15.0f;
            

        }
        

        timerText.text = "Tiempo: " + Mathf.Round(targetTime).ToString();

    }
}
