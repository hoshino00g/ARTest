using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dragging : MonoBehaviour {
   public  Text message = null;
   private Transform pickedObject = null;
   private Vector3 lastPlanePoint;
   // Use this for initialization
   void Start () {
   }
   // Update is called once per frame
   void Update () {
        Plane targetPlane = new Plane(transform.up, transform.position);
        //message.text = "";
        foreach (Touch touch in Input.touches) {
           
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
   
            float dist = 0.0f;
  
            targetPlane.Raycast(ray, out dist);
       
            Vector3 planePoint = ray.GetPoint(dist);
     
            if (touch.phase == TouchPhase.Began) {
         
                 RaycastHit hit = new RaycastHit();
                 if (Physics.Raycast(ray, out hit, 1000)) { 

                     pickedObject = hit.transform;
                     lastPlanePoint = planePoint;
                 } else {
                     pickedObject = null;
                 }
           
            } else if (touch.phase == TouchPhase.Moved) {
                 if (pickedObject != null) {
                     pickedObject.position += planePoint - lastPlanePoint;
                     lastPlanePoint = planePoint;
                 }
            //Set pickedObject to null after touch ends.
            } else if (touch.phase == TouchPhase.Ended) {
                 pickedObject = null;
            }
        }
   }
}
