using UnityEngine;

public class ButtonUtilities : MonoBehaviour
{

    public static ButtonUtilities instance;

    public bool mover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BotonMoverPress()
    {
        mover = true;

        Debug.Log($"Victor: {mover}");
    }
}
