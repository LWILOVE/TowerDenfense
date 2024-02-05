using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ViewController : MonoBehaviour
{
    public float speed = 60;
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float d = Input.GetAxis("Depth");
        transform.Translate(new Vector3(h,d,v)*Time.deltaTime*speed,Space.World);
        if (speed > 30 && speed < 120)
        {
            speed += Input.mouseScrollDelta.y;
        }
        else
        {
            speed = 60;
        }
    }
}
