using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanLooking : MonoBehaviour
{
    Animator animator;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    { 
        this.animator = GetComponent<Animator>();
       // this.targetPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButton(0)){
        //     Vector3 touchPos = Camera.main.ScreenPointToRay (Input.mousePosition).origin;
        //     touchPos.z = 0.859f;
        //     targetPos = touchPos;
        // }
    }

    private void OnAnimatorIK(int layerIndex){
        this.animator.SetLookAtWeight(1.0f, 0.8f, 1.0f, 0.0f,0f);
        this.animator.SetLookAtPosition(camera.transform.position);
    }
}
