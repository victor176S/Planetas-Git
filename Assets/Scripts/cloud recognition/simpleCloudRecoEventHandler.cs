using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;
using UnityEngine.Networking;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UI;

//clase para leer el Json, con sus datos, cambiar las variables dependiendo de lo que haya en el Json
public class metaDatos
{

    public string nombre;
    public string puntuacion;
    public string url;

    public static metaDatos CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<metaDatos>(jsonString);
    }

}
/*

    Script que:
        - Genera una opción aleatoriamente (planetas) y lo muestra en pantalla
        - Reconoce imágenes del cloud y obtiene el nombre (TargetName)
        - Comprueba si la imagen coincide con la mostrada en el texto



*/
public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    public float speed;

    public Button boton;

    public static SimpleCloudRecoEventHandler instance;

    CloudRecoBehaviour mCloudRecoBehaviour;
    bool mIsScanning = true;
    string mTargetMetadata = "";

    //variable del tipo "metaDatos" de la clase de arriba del todo para que funcione en OnNewSearchResult
    metaDatos metaDatosVuforia;
   

    public ImageTargetBehaviour ImageTargetTemplate;

    public GameObject objeto;
    public bool instanciado;

    public GameObject objectFound;


    // Register cloud reco callbacks
    void Awake()
    {

        instance = this;

        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);

        speed = 10f;
    }

    IEnumerator GetAssetBundle() {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(metaDatosVuforia.url);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            string[] allAssetNames = bundle.GetAllAssetNames();
            string gameObjectName = Path.GetFileNameWithoutExtension(allAssetNames[0]).ToString();
            objectFound = bundle.LoadAsset(gameObjectName) as GameObject;
            objeto = Instantiate(objectFound,ImageTargetTemplate.gameObject.transform.position, ImageTargetTemplate.gameObject.transform.rotation);
            if (boton.GetComponent<ButtonUtilities>().mover == true)
            {
                objeto.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }  
        }
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
     public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());

    }
     public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;

        if (scanning)
        {
            Destroy(objeto);
        }
    }
      // Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult )
    {



        // Store the target metadata
        mTargetMetadata = cloudRecoSearchResult.TargetName;

        //viene de la clase "metaDatos" de arriba del todo, solo seria cambiar "metaDatos" para adaptarlo a como lo quieras poner en
        //la funcion de arriba del todo

        metaDatosVuforia = metaDatos.CreateFromJSON(cloudRecoSearchResult.MetaData);

        StartCoroutine(GetAssetBundle());

        //ESTO ESTO FALTABA ESTE IF ESTA COSA

        if (ImageTargetTemplate)
        {
            mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, ImageTargetTemplate.gameObject);
        }

        // Stop the scanning by disabling the behaviour
        mCloudRecoBehaviour.enabled = false;
    }
   void OnGUI() {
        //caja de texto 1 donde sale si está escaneando

      // Display current 'scanning' status
      GUI.Box (new Rect(100,100,200,50), mIsScanning ? "Scanning" : "Not scanning");
      //caja de texto dos con el valor de url

      // Display metadata of latest detected cloud-target
      GUI.Box (new Rect(100,200,200,50), "Metadata: " + metaDatosVuforia.url);
        //caja de texto donde sale para reiniciar el scan

      // If not scanning, show button
      // so that user can restart cloud scanning
      if (!mIsScanning) {
          if (GUI.Button(new Rect(100,300,200,50), "Restart Scanning")) {
            //Destroy(ImageTargetTemplate.gameObject);
          // Reset Behaviour
          mCloudRecoBehaviour.enabled = true;
          mTargetMetadata="";
            
          }
      }
  }
}
