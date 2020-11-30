using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using static Vuforia.TrackableBehaviour;


public class PrefabInstantiater : MonoBehaviour 
{   private TrackableBehaviour mTrackableBehaviour;
    public Transform myModelPrefab;
    public GameObject vbBtnObj;
    // Start is called before the first frame update
    void Start()
    {   
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if(mTrackableBehaviour){
            mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);//TrackableBehaviourのStatusが変更された時にRegisterOnTrackableStatusChangedが呼ばれる

        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
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
