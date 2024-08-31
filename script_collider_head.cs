using UnityEngine;
using System.Diagnostics;
using System;

public class script_collider_head: MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
    }
    void OnTriggerEnter(Collider other){

        
        // 
        if(other.gameObject.tag=="coin_center"){
            Destroy(GameObject.FindWithTag(other.gameObject.tag));
            script_main.num_collect_coin += 1;
            script_main.num_points += 10;
        }
        if(other.gameObject.tag=="coin_start"){
            Destroy(GameObject.FindWithTag(other.gameObject.tag));
            script_main.num_collect_coin += 1;
            script_main.num_points += 10;
        }
        if(other.gameObject.tag=="coin_goal"){
            script_main.num_collect_coin += 1;
            script_main.num_points += 10;
            // if (script_main.cur_trial_type=="tp"){
            //     script_main.num_points += script_main.list_coin3rd_value_potection[Int32.Parse(script_main.cur_list_affect_value[script_main.cur_affect_index])];
            // }
            // if (script_main.cur_trial_type=="pt"){
            //     script_main.num_points += script_main.list_coin3rd_value_threat[Int32.Parse(script_main.cur_list_affect_value[script_main.cur_affect_index])-1];
            // }
            GameObject[] pool_coin = GameObject.FindGameObjectsWithTag("coin_goal");
            foreach(GameObject go in pool_coin) {
                Destroy(go);
            }
        }
        if(other.gameObject.tag=="predator"){
            UnityEngine.Debug.Log("hit bat");
            script_main.bool_show_ima_blood = true;
            script_main.bool_allow_move = true;
            script_main.time_counter_bloodima = new Stopwatch();
            script_main.time_counter_bloodima.Start();
            script_main.num_points=0;
        }

        

    }
}
