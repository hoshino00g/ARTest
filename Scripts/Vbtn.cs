using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class Vbtn : MonoBehaviour//, IVirtualButtonEventHandler
{
    public GameObject vbBtnObj;
    public Animator cubeAni;
    // Start is called before the first frame update
    void Start()
    {
        //vbBtnObj = GameObject.Find("Vbtn");
       // vbBtnObj.GetComponent<VirtualButtonBehavior>().RegisterEventHandler(this);
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);//ボタンに挙動を登録している
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        cubeAni.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        cubeAni.SetLayerWeight(2, 1);
        Debug.Log("Button presse");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        cubeAni.SetLayerWeight(2, 0);
        Debug.Log("Button released");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
