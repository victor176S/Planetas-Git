using UnityEngine;
using Vuforia;



public class ImageTargetHandler : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;

    //aqui sale como hacer una variable que tiene a su disposicion las funciones del archivo GameController
    private GameController gameController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        gameController = FindFirstObjectByType<GameController>();

        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;

        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED)
        {
            //Aqui usa la variable para tener acceso a la función OnTargetFound del otro archivo         
            gameController.OnTargetFound(behaviour.TargetName);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
