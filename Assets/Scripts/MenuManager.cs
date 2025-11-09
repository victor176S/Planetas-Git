using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    { //se puede poner el nombre de la escena, o el orden de la escena (File > Build Profiles > Scene List, en la parte de la derecha)
        SceneManager.LoadScene("Principal");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
