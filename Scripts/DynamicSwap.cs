using UnityEngine;
using Vuforia;
using System.Collections;

public class DynamicSwap : MonoBehaviour {
      public TrackableBehaviour theTrackable;
      private bool mSwapModel = false;
      private bool origin = true;
      GameObject cube;
      // Use this for initialization
      void Start () {
          if (theTrackable == null)
          {
              Debug.Log ("Warning: Trackable not set !!");
          }else{
              
          }
      }
      // Update is called once per frame
      void Update () {
          if (mSwapModel && theTrackable != null) {
              SwapModel();
              mSwapModel = false;
          }
      }
      void OnGUI() {//MonoBehaviourのやつ、OnGUI はレンダリングと GUI イベントのハンドリングのために呼び出されます。
          print("OKO");
          if (GUI.Button (new Rect(200,10,120,40), "Swap Model")) {
              mSwapModel = true;
              print("にゃにゃにゃ");
          }
      }
      private void SwapModel() {
          GameObject trackableGameObject = theTrackable.gameObject;
          //disable any pre-existing augmentation
          if(origin){
          for (int i = 0; i < trackableGameObject.transform.GetChildCount(); i++)
          {
              Transform child = trackableGameObject.transform.GetChild(i);
              child.gameObject.active = false;
          }
          // Create a simple cube object
          cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
          // Re-parent the cube as child of the trackable gameObject
          cube.transform.parent = theTrackable.transform;
          // Adjust the position and scale
          // so that it fits nicely on the target
          cube.transform.localPosition = new Vector3(-0.015542f, 0.023254f, -0.029501f);
          cube.transform.localRotation = Quaternion.identity;
          cube.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
          // Make sure it is active
          cube.name = "ken";
          cube.active = true;
          origin = false;
          }else{
            for (int i = 0; i < trackableGameObject.transform.GetChildCount(); i++)
          {
              Transform child = trackableGameObject.transform.GetChild(i);
              child.gameObject.active = true;
          }
          Destroy(cube);
          origin = true;
          }
      }
  }