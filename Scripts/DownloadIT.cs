using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Vuforia;
using static Vuforia.TrackableBehaviour;

public class DownloadIT : MonoBehaviour
{
    public Transform myModelPrefab;

    private TrackableBehaviour mTrackableBehaviour;
    
    void Start()
    {
        StartCoroutine(CreateImageTargetFromDownloadedTexture());
    }

    IEnumerator CreateImageTargetFromDownloadedTexture()
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("https://mizuma-art.co.jp/wp-content/uploads/2018/01/27_yuzuyu-2.jpg"))//外部リソースの開放にはusingを使うことが多い
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

          
                var texture = DownloadHandlerTexture.GetContent(uwr);

             
                var runtimeImageSource = objectTracker.RuntimeImageSource;
                runtimeImageSource.SetImage(texture, 0.15f, "myTargetName");

              
                var dataset = objectTracker.CreateDataSet();
                var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, "myTargetName");

                trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();

         
                objectTracker.ActivateDataSet(dataset);

                mTrackableBehaviour = trackableBehaviour.GetComponent<TrackableBehaviour>();
                if(mTrackableBehaviour){
                    mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);//TrackableBehaviourのStatusが変更された時にRegisterOnTrackableStatusChangedが呼ばれる

                    }
            }
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
