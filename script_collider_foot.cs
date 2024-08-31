using UnityEngine;
using System.Diagnostics;
using System;

public class script_collider_foot: MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="object_path"){
            script_main.loc_curPath = other.gameObject.transform.position;
        }

        if(other.gameObject.name=="plant1" & script_main.num_collect_coin==1 & script_main.num_pass_localcue==0){
            script_main.num_pass_localcue += 1;
        }
        if(other.gameObject.name=="plant2" & script_main.num_collect_coin==2 & script_main.num_pass_localcue==1){
            script_main.num_pass_localcue += 1;
        }

    }
}
