using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public string Escena;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void  CargarEscena(string Escena)
    {
        
        SceneManager.LoadScene(Escena);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
