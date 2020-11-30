using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Vuforia;
using static Vuforia.TrackableBehaviour;
  
public class SideLoadIT : MonoBehaviour
{
    public Transform myModelPrefab;
    private TrackableBehaviour mTrackableBehaviour;
    void Start()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(CreateImageTargetFromSideloadedTexture);//実行時に呼ばれる関数
    }
  
    void CreateImageTargetFromSideloadedTexture()
    {
        var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();//Trackerオブジェクト(空)を生成
  
      
        var runtimeImageSource = objectTracker.RuntimeImageSource;//RuntimeImageSourceはDataSet内の画像ファイルから新しいTrackableを作成するためのやつ
        runtimeImageSource.SetFile(VuforiaUnity.StorageType.STORAGE_APPRESOURCE, "Vuforia/takashi.jpg", 0.15f, "takashi");
        var dataset = objectTracker.CreateDataSet();
        var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, "takashi");
        trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();
        objectTracker.ActivateDataSet(dataset);

        mTrackableBehaviour = trackableBehaviour.GetComponent<TrackableBehaviour>();
        if(mTrackableBehaviour){
            mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);//TrackableBehaviourのStatusが変更された時にRegisterOnTrackableStatusChangedが呼ばれる

        }


    }

    public void OnTrackableStatusChanged(
   StatusChangeResult obj)
  { 
     if (obj.NewStatus == Status.DETECTED ||

        obj.NewStatus == Status.TRACKED ||

        obj.NewStatus == Status.EXTENDED_TRACKED)//||はorの意味合い
    {
      OnTrackingFound();
    }
  } 
  private void OnTrackingFound()
  {
    if (myModelPrefab != null)
    {
      Transform myModelTrf = GameObject.Instantiate(myModelPrefab) as Transform;
      myModelTrf.parent = mTrackableBehaviour.transform;
      myModelTrf.localPosition = new Vector3(-0.015542f, 0.023254f, -0.029501f);
      myModelTrf.localRotation = Quaternion.Euler(-90, -180, 0);
      myModelTrf.localScale = new Vector3(0.04185474f, 0.04185474f, 0.04185474f);
      myModelTrf.gameObject.active = true;
    }
  }
}
