using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using static Vuforia.TrackableBehaviour;

public class VuforiaAndAssetBundle:MonoBehaviour
  {
    public string AssetName;
    public int Version;
    private GameObject mBundleInstance = null;
    private TrackableBehaviour mTrackableBehaviour;
    private bool mAttached = false;
    public Material dmt;
    AssetBundle assetBundle;
  
    void Start() {
      StartCoroutine(DownloadAndCache());
      mTrackableBehaviour = GetComponent<TrackableBehaviour>();
      if (mTrackableBehaviour) {
        mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStateChanged);
      }
    }

    IEnumerator DownloadAndCache() {
      string path = "/Users/hoshinohiroto/Desktop/ARTest/AssetBundles/StandaloneOSXUniversal/head_small";
      assetBundle = AssetBundle.LoadFromFile(path);
      print("KOKOKndjfnasjfndajf");
      yield return assetBundle;
      print("ファfjdjfなsdjふぁsjfだ");
      if (assetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            //return;
        }
       // AssetBundle bundle = assetBundle;
       print(assetBundle);
        mBundleInstance = Instantiate (assetBundle.LoadAsset<GameObject>("head_small")) as GameObject;
        var childTransforms = mBundleInstance.transform.GetChild(1).gameObject;
        print(childTransforms);
        childTransforms.GetComponent<Renderer>().material = dmt;
        print(dmt);
        print(childTransforms.GetComponent<MeshRenderer>().materials[0]);
        // foreach(var item in childTransforms)
        // {   print(item + "kogjosajfd");
        //     item.GetComponent<MeshRenderer>().materials[0] = dmt;
        // }
    }
  
  
  public void OnTrackableStateChanged(//ここがTrackableBehaviour.Statusなどのメソッドグループだとエラー、オーバーロードを含めたメソッドの集まりをメソッドグループ

    StatusChangeResult obj)

  {

    if (obj.NewStatus == Status.DETECTED ||

        obj.NewStatus == Status.TRACKED ||

        obj.NewStatus == Status.EXTENDED_TRACKED){
        //mBundleInstance = Instantiate (assetBundle.LoadAsset<GameObject>("head_small")) as GameObject;
      if (!mAttached && mBundleInstance) {
        // if bundle has been loaded, let's attach it to this trackable
        mBundleInstance.transform.parent = this.transform;
        mBundleInstance.transform.localScale = new Vector3(0.003128704f, 0.003128704f, 0.003128704f);
        mBundleInstance.transform.localPosition = new Vector3(-0.015542f, 0.023254f, 0.0333f);
        mBundleInstance.transform.localRotation = Quaternion.Euler(-128.156f, -90f, -90f);
        mBundleInstance.transform.gameObject.SetActive(true);
        mAttached = true;
      }
    }
  }

  // Update is called once per frame
//     IEnumerator DownloadAndCache() {
//       while(!Caching.ready)
//       yield return null;
//       // example URL of file on PC filesystem (Windows)
//       // string bundleURL = "file:///D:/Unity/AssetBundles/MyAssetBundle.unity3d";
//       // example URL of file on Android device SD-card
//       string bundleURL = "file:///mnt/sdcard/AndroidCube.unity3d";
//       using (WWW www = WWW .LoadFromCacheOrDownload(bundleURL, Version)) {
//       yield return www;
//       if (www .error != null)
//         throw new UnityException("WWW Download had an error: " + www .error);
//       AssetBundle bundle = www .assetBundle;
//       if (AssetName == "") {
//         mBundleInstance = Instantiate (bundle.mainAsset) as GameObject;
//       }
//       else {
//         mBundleInstance = Instantiate(bundle.LoadAsset(AssetName)) as GameObject;
//       }
//     }
//   }
  }