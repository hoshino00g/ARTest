using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickevent : MonoBehaviour
{
    public Animator anim_uni_chan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("にゃにゃにゃ");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.transform.gameObject.name == "unitychan"){
                JumpAnimOccur();
                }
            }
        }
        
    }

    public void JumpAnimOccur(){   
         anim_uni_chan.SetLayerWeight(2,1);
         StartCoroutine("AnimStop");
    }

    IEnumerator AnimStop(){
        yield return new WaitForSeconds(2f);
        anim_uni_chan.SetLayerWeight(2,0);
    }
}
