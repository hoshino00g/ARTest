using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Vuforia;
using System.Linq;
using static Vuforia.TrackableBehaviour;
using System.Collections.Generic;

public class DataSetLoading : MonoBehaviour//動的にTagetを追加しなければならない時にデータセットから追加する場合(データセットは静的にTargetobjがある場合は行けるが、、)
{//また、データセット全体のマーカーを一気に動的に出すことができます。
    // public GameObject augmentationObject = null;  // you can use teapot or other object
	// public string dataSetName = "";
    public Transform myModelPrefab;
    private TrackableBehaviour[] mTrackableBehaviour;

    void Start()
    {   
        mTrackableBehaviour = new TrackableBehaviour[4];
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(LoadDataSet);       
    }

    void Update()
    {
        
    }

    void LoadDataSet()
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        DataSet dataSet = objectTracker.CreateDataSet();
        dataSet.Load("Sample1");
        objectTracker.ActivateDataSet(dataSet);
        var tbs = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
        int k = 0;
        foreach (TrackableBehaviour tb in tbs) {
            mTrackableBehaviour[k] = tb;
            mTrackableBehaviour[k].gameObject.name = k.ToString();
            mTrackableBehaviour[k].RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);
            k++;
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
      for(int k = 2; k <= 3; ++k){
        if (mTrackableBehaviour[k] != null)
        {
            if(mTrackableBehaviour[k].gameObject.transform.childCount == 0){
            print(mTrackableBehaviour[k]);
            Transform myModelTrf = GameObject.Instantiate(myModelPrefab) as Transform;
            myModelTrf.parent = mTrackableBehaviour[k].transform;
            myModelTrf.localPosition = new Vector3(-0.015542f - k * -0.01f, 0.023254f, -0.029501f);
            myModelTrf.localRotation = Quaternion.Euler(-90, -180, 0);
            myModelTrf.localScale = new Vector3(0.04185474f, 0.04185474f, 0.04185474f);
            myModelTrf.gameObject.active = true;
            }
        }
        }
     }
}


