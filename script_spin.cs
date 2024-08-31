using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_spin: MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        
    }


    void FixedUpdate(){
        transform.Rotate(0, 1.5f, 0, Space.Self);
    }


}
