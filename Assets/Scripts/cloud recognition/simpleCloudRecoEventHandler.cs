using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    CloudRecoBehaviour mCloudRecoBehaviour;
    bool mIsScanning = false;
    string mTargetMetadata = "";

    string URL;

    string nombreBundle;

    public ImageTargetBehaviour ImageTargetTemplate;

    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
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
            // Clear all known targets
        }
    }

     // Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult )
    {

        if (ImageTargetTemplate)
        {
        /* Enable the new result with the same ImageTargetBehaviour: */
        mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, ImageTargetTemplate.gameObject);
        }
        // Store the target metadata
        mTargetMetadata = cloudRecoSearchResult.MetaData;

        // Stop the scanning by disabling the behaviour
        mCloudRecoBehaviour.enabled = false;
    }

    void OnGUI() {
    // Display current 'scanning' status
    GUI.Box (new Rect(100,100,200,50), mIsScanning ? "Scanning" : "Not scanning");
    // Display metadata of latest detected cloud-target
    GUI.Box (new Rect(100,200,200,50), "Metadata: " + mTargetMetadata);
    // If not scanning, show button
    // so that user can restart cloud scanning
    if (!mIsScanning) {
        if (GUI.Button(new Rect(100,300,200,50), "Restart Scanning")) {
        // Reset Behaviour
        mCloudRecoBehaviour.enabled = true;
        mTargetMetadata="";
            }
        }
    }

        IEnumerator FetchGameObjectFromServer(string url,string manifestFileName,uint crcR,Hash128 hashR)
        {
         
            //Get from generated manifest file of assetbundle.
            uint crcNumber = crcR;
            //Get from generated manifest file of assetbundle.
            Hash128 hashCode = hashR;
             UnityWebRequest webrequest =
                UnityWebRequestAssetBundle.GetAssetBundle(url, new CachedAssetBundle(manifestFileName, hashCode), crcNumber);
    
           
            webrequest.SendWebRequest();
    
            while (!webrequest.isDone)
            {
              Debug.Log(webrequest.downloadProgress);  
              
            }
        
            AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(webrequest);
           yield return assetBundle;
           if (assetBundle == null)
                yield break;
       
      
            //Gets name of all the assets in that assetBundle.
            string[] allAssetNames = assetBundle.GetAllAssetNames();
             Debug.Log(allAssetNames.Length +"objects inside prefab bundle");
            foreach (string gameObjectsName in allAssetNames)
            {
                string gameObjectName = Path.GetFileNameWithoutExtension(gameObjectsName).ToString();
                GameObject objectFound = assetBundle.LoadAsset(gameObjectName) as GameObject;
                Instantiate(objectFound);
            }
            assetBundle.Unload(false);
            yield return null;
        }

        void Start()
        {
            StartCoroutine(FetchGameObjectFromServer(URL,nombreBundle,0,new Hash128()));
        }
}