using UnityEngine;

public class PruebaRigidBody : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (SimpleCloudRecoEventHandler.instance.objeto != null)
        {
            SimpleCloudRecoEventHandler.instance.objeto.transform.position += new Vector3 (0.02f, 0,0);
        }  
    }
}
