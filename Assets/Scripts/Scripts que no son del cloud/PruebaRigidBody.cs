using TMPro;
using UnityEngine;

public class PruebaRigidBody : MonoBehaviour
{

    public static PruebaRigidBody instance;

    public TextMeshProUGUI debugFixedUpd;

    public TextMeshProUGUI debugIFUpd;

    public bool instanciado;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        debugFixedUpd.text = "fixedupdate";

        if (instanciado)
        {
            debugIFUpd.text = "entra al if";

            SimpleCloudRecoEventHandler.instance.objeto.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //SimpleCloudRecoEventHandler.instance.objeto.transform.Translate(0,0,0); 
        }  
    }
}
