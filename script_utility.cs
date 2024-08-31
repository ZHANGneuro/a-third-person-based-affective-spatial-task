using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine.Rendering.PostProcessing;

public class script_utility: MonoBehaviour
{
    public GameObject mountain1;
    public GameObject mountain2;
    public GameObject object_enermy_l1;
    public GameObject object_enermy_l2;
    public GameObject object_enermy_l3;
    public GameObject object_enermy_l4;
    public GameObject plant;
    public GameObject object_coin;
    GameObject m_object_coin;
    public GameObject ground_path;

    public void GetPressedNumber() {
        if(!script_main.bool_confidence_key_pressed){
            if(Input.GetKeyDown(KeyCode.Alpha1)) {
                script_main.ima_rating = (Texture2D)Resources.Load("rating_1");
                script_main.time_counter_confidence = new Stopwatch();
                script_main.time_counter_confidence.Start();
                script_main.bool_confidence_key_pressed = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha2)) {
                script_main.ima_rating = (Texture2D)Resources.Load("rating_2");
                script_main.time_counter_confidence = new Stopwatch();
                script_main.time_counter_confidence.Start();
                script_main.bool_confidence_key_pressed = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha3)) {
                script_main.ima_rating = (Texture2D)Resources.Load("rating_3");
                script_main.time_counter_confidence = new Stopwatch();
                script_main.time_counter_confidence.Start();
                script_main.bool_confidence_key_pressed = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha4)) {
                script_main.ima_rating = (Texture2D)Resources.Load("rating_4");
                script_main.time_counter_confidence = new Stopwatch();
                script_main.time_counter_confidence.Start();
                script_main.bool_confidence_key_pressed = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha5)) {
                script_main.ima_rating = (Texture2D)Resources.Load("rating_5");
                script_main.time_counter_confidence = new Stopwatch();
                script_main.time_counter_confidence.Start();
                script_main.bool_confidence_key_pressed = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha6)) {
                script_main.ima_rating = (Texture2D)Resources.Load("rating_6");
                script_main.time_counter_confidence = new Stopwatch();
                script_main.time_counter_confidence.Start();
                script_main.bool_confidence_key_pressed = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha7)) {
                script_main.ima_rating = (Texture2D)Resources.Load("rating_7");
                script_main.time_counter_confidence = new Stopwatch();
                script_main.time_counter_confidence.Start();
                script_main.bool_confidence_key_pressed = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha8)) {
                script_main.ima_rating = (Texture2D)Resources.Load("rating_8");
                script_main.time_counter_confidence = new Stopwatch();
                script_main.time_counter_confidence.Start();
                script_main.bool_confidence_key_pressed = true;
            }
            if(Input.GetKeyDown(KeyCode.Alpha9)) {
                script_main.ima_rating = (Texture2D)Resources.Load("rating_9");
                script_main.time_counter_confidence = new Stopwatch();
                script_main.time_counter_confidence.Start();
                script_main.bool_confidence_key_pressed = true;
            }
        }
    }

    public Vector3 measure_loc (float angle_in_degree, float distance, float loc_y, float shift_x, float shift_z){
        float loc_x = distance * Mathf.Cos((Mathf.PI / 180.0f) * angle_in_degree) + shift_x;
        float loc_z = distance * Mathf.Sin((Mathf.PI / 180.0f) * angle_in_degree) + shift_z;
        return new Vector3(loc_x, loc_y, loc_z);
    }

    public List<string> TextAssetToList(TextAsset ta) {
		return new List<string>(ta.text.Split('\n'));
	}

	public List<E> ShuffleList<E>(List<E> inputList){
		List<E> randomList = new List<E>();
		System.Random r = new System.Random();
		int randomIndex = 0;
		while (inputList.Count > 0){
			randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
			randomList.Add(inputList[randomIndex]); //add it to the new, random list
			inputList.RemoveAt(randomIndex); //remove to avoid duplicates
		}
		return randomList; //return the new random list
	}

    public float mod(float x, float m) {
        return (x%m + m)%m;
    }

    public void effect_blur(string on_or_off){
        DepthOfField pr;
        PostProcessVolume volume;
        if(on_or_off=="on"){
            volume = script_main.m_MainCamera.GetComponent<PostProcessVolume>();
            if (volume.sharedProfile.TryGetSettings<DepthOfField>(out pr))
            {
                pr.enabled.value=true;
            }
        }
        if(on_or_off=="off"){
            volume = script_main.m_MainCamera.GetComponent<PostProcessVolume>();
            if (volume.sharedProfile.TryGetSettings<DepthOfField>(out pr))
            {
                pr.enabled.value=false;
            }
        }
    }

    public void clear_space(){
        //
        GameObject[] pool_predator = GameObject.FindGameObjectsWithTag("predator");
        foreach(GameObject go in pool_predator) {
            Destroy(go);
        }
        //
        if (GameObject.Find("_group_object")!=null){
            Destroy(script_main._group_object);
            script_main._group_object = new GameObject("_group_object");
        } else {
            script_main._group_object = new GameObject("_group_object");
        }
        //
        if (GameObject.Find("_group_ground_path")!=null){
            Destroy(script_main._group_ground_path);
            script_main._group_ground_path = new GameObject("_group_ground_path");
        } else {
            script_main._group_ground_path = new GameObject("_group_ground_path");
        }
        //
        if (GameObject.FindGameObjectWithTag("selfbody")!=null){
            Destroy(GameObject.FindWithTag("selfbody"));
        }
        GameObject[] pool_selfbody = GameObject.FindGameObjectsWithTag("selfbody");
        foreach(GameObject go in pool_selfbody) {
            Destroy(go);
        }
    }
    public void camera_to_sky(){
        script_main.m_MainCamera.transform.position = new Vector3(script_main.terrain_size.x/2, script_main.camera_height, script_main.terrain_size.z/2);
        script_main.m_MainCamera.transform.localEulerAngles = new Vector3(-45, 0, 0);
    }
    public void add_temp_selfbody_camera(){
        Vector3 temp_loc = measure_loc(0, script_main.distance_initial_between_selfbody_center, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z);
        script_main.m_object_selfbody.transform.position = new Vector3(temp_loc.x, script_main.selfbody_height, temp_loc.z);
        script_main.m_object_selfbody.transform.forward = (new Vector3(script_main.terrain_size.x/2, script_main.selfbody_height, script_main.terrain_size.z/2) - script_main.m_object_selfbody.transform.position).normalized;
        script_main.rot_LR = script_main.m_object_selfbody.transform.localEulerAngles.y;
        attach_camera_on_selfbody();
    }

    public void attach_camera_on_selfbody(){
        float angle_rad = (Mathf.PI / 180.0f) * (270-script_main.rot_LR);
        float loc_x = script_main.distance_camera_selfbody * Mathf.Cos(angle_rad);
        float loc_z = script_main.distance_camera_selfbody * Mathf.Sin(angle_rad);
        script_main.m_MainCamera.transform.position = new Vector3(loc_x, script_main.height_camera_selfbody, loc_z)+script_main.m_object_selfbody.transform.position;
        script_main.m_MainCamera.transform.forward = (script_main.m_object_selfbody.transform.position - script_main.m_MainCamera.transform.position).normalized;  
    }

    public void add_selfbody_animation(string ani_name){
        script_main.selfbody_animator = script_main.m_object_selfbody.GetComponent<Animator> ();
        script_main.selfbody_animator.runtimeAnimatorController = Instantiate(Resources.Load(ani_name)) as RuntimeAnimatorController;
    }

    public Vector3 compute_bat_goal_loc (Vector3 bat_loc){
        float cur_ori = mod((float)(Mathf.Atan2(script_main.m_object_selfbody.transform.position.z-bat_loc.z, script_main.m_object_selfbody.transform.position.x-bat_loc.x) * 180f/Math.PI), 360f) ;
        float angle_rad = (Mathf.PI / 180.0f) * cur_ori;
        Vector3 temp_vector = new Vector3(script_main.distance_bat_fly * Mathf.Cos(angle_rad), 0f, script_main.distance_bat_fly * Mathf.Sin(angle_rad)) + bat_loc;
        return temp_vector;
    }

    //
    public void install_env (){
        script_main.list_plant_locs_x1 = new List<float>();
        script_main.list_plant_locs_x2 = new List<float>();
        script_main.list_plant_locs_x3 = new List<float>();
        script_main.list_plant_locs_z1 = new List<float>();
        script_main.list_plant_locs_z2 = new List<float>();
        script_main.list_plant_locs_z3 = new List<float>();
        // place mountain
        add_mountain();
        // place local cues
        add_localcues();
        // place bats
        place_predators();
        // place paths
        place_path();
        // place coins 
        place_coin();
    }
    public void add_mountain(){
        script_main.list_mountain_locs_x = new List<float>(){};
        script_main.list_mountain_locs_y = new List<float>(){};
        script_main.list_mountain_locs_z = new List<float>(){};
        for (int ith=0; ith<script_main.mountain_identity.Count; ith++){
            float xx = Mathf.Cos(script_main.mountain_loc[ith]*Mathf.PI/180) * script_main.mountain_distance;
            float yy = Mathf.Sin(script_main.mountain_loc[ith]*Mathf.PI/180) * script_main.mountain_distance;
            if(script_main.mountain_identity[ith]==0){
                script_main.tem_go = Instantiate(mountain1, new Vector3(xx, -1f, yy) + script_main.loc_center_ground, Quaternion.Euler(0, 0, 0)); 
            } else {
                script_main.tem_go = Instantiate(mountain2, new Vector3(xx, -1f, yy) + script_main.loc_center_ground, Quaternion.Euler(0, 0, 0)); 
            }
            script_main.list_mountain_locs_x.Add(script_main.tem_go.transform.position.x);
            script_main.list_mountain_locs_y.Add(15);
            script_main.list_mountain_locs_z.Add(script_main.tem_go.transform.position.z);
            script_main.tem_go.transform.parent = script_main._group_object.transform;
            script_main.tem_go.transform.localScale = new Vector3(new System.Random().Next(13,15)/10f, new System.Random().Next(10,15)/10f, 1f);
            if(script_main.mountain_orientation[ith]==0){
                script_main.tem_go.transform.forward = (script_main.loc_center_ground - script_main.tem_go.transform.position).normalized;
            } else {
                script_main.tem_go.transform.forward = (script_main.tem_go.transform.position - script_main.loc_center_ground).normalized;
            }
            if(script_main.mountain_loc[ith]>=0 & script_main.mountain_loc[ith]<120){
                script_main.tem_go.GetComponent<Renderer>().material = Resources.Load("material_mountain_grey", typeof(Material)) as Material;
            }
            if(script_main.mountain_loc[ith]>=120 & script_main.mountain_loc[ith]<240){
                script_main.tem_go.GetComponent<Renderer>().material = Resources.Load("material_mountain_grey", typeof(Material)) as Material;
            }
            if(script_main.mountain_loc[ith]>=240 & script_main.mountain_loc[ith]<360){
                script_main.tem_go.GetComponent<Renderer>().material = Resources.Load("material_mountain_grey", typeof(Material)) as Material;
            }
        } 
    }
    public void add_localcues(){
        // local cues for start location
        for(int ith_obj = 0; ith_obj <script_main.localcue_num; ith_obj++){
            float random_angle = new System.Random().Next(-script_main.half_range_affectiveCue, script_main.half_range_affectiveCue);
            float random_dis = (new System.Random().Next(-13, 14)/10f);
            Vector3 temp_loc = measure_loc((float.Parse(script_main.cur_direction_arm1)+random_angle), script_main.localcue_radius+random_dis, 0f, 0f, 0f);
            var cur_plant = Instantiate(plant, new Vector3(temp_loc.x, 0f, temp_loc.z) + script_main.loc_center_ground, Quaternion.Euler(0, new System.Random().Next(1,360), 0)); 
            cur_plant.GetComponent<Renderer>().material = Resources.Load("material_plant_"+script_main.cur_list_affect_color[0], typeof(Material)) as Material;
            cur_plant.transform.parent = script_main._group_object.transform;
            cur_plant.name = "plant1";
            script_main.list_plant_locs_x1.Add(temp_loc.x);
            script_main.list_plant_locs_z1.Add(temp_loc.z);
        }
        // local cues for end location
        for(int ith_obj = 0; ith_obj <script_main.localcue_num; ith_obj++){
            float random_angle = new System.Random().Next(-script_main.half_range_affectiveCue, script_main.half_range_affectiveCue);
            float random_dis = (new System.Random().Next(-13, 14)/10f);
            Vector3 temp_loc = measure_loc((float.Parse(script_main.cur_direction_arm2)+random_angle), script_main.localcue_radius+random_dis, 0f, 0f, 0f);
            var cur_plant = Instantiate(plant, new Vector3(temp_loc.x, 0f, temp_loc.z) + script_main.loc_center_ground, Quaternion.Euler(0, new System.Random().Next(1,360), 0)); 
            cur_plant.GetComponent<Renderer>().material = Resources.Load("material_plant_"+script_main.cur_list_affect_color[1], typeof(Material)) as Material;
            cur_plant.transform.parent = script_main._group_object.transform;
            cur_plant.name = "plant2";
            script_main.list_plant_locs_x2.Add(temp_loc.x);
            script_main.list_plant_locs_z2.Add(temp_loc.z);
        }
        // local cues for the third
        for(int ith_obj = 0; ith_obj <script_main.localcue_num; ith_obj++){
            float random_angle = new System.Random().Next(-script_main.half_range_affectiveCue, script_main.half_range_affectiveCue);
            float random_dis = (new System.Random().Next(-13, 14)/10f);
            Vector3 temp_loc = measure_loc((float.Parse(script_main.cur_direction_arm3)+random_angle), script_main.localcue_radius+random_dis, 0f, 0f, 0f);
            var cur_plant = Instantiate(plant, new Vector3(temp_loc.x, 0f, temp_loc.z) + script_main.loc_center_ground, Quaternion.Euler(0, new System.Random().Next(1,360), 0)); 
            cur_plant.GetComponent<Renderer>().material = Resources.Load("material_plant_"+script_main.cur_list_affect_color[2], typeof(Material)) as Material;
            cur_plant.transform.parent = script_main._group_object.transform;
            cur_plant.name = "plant2";
            script_main.list_plant_locs_x3.Add(temp_loc.x);
            script_main.list_plant_locs_z3.Add(temp_loc.z);
        }
    } 
    public void compute_distance_to_localcues(){
        if(GameObject.Find("object_enermy1")!=null){
            script_main.m_object_enermy1.transform.forward = (script_main.m_object_selfbody.transform.position - script_main.m_object_enermy1.transform.position).normalized;
        }
        if(GameObject.Find("object_enermy2")!=null){
            script_main.m_object_enermy2.transform.forward = (script_main.m_object_selfbody.transform.position - script_main.m_object_enermy2.transform.position).normalized;
        }
        if(GameObject.Find("object_enermy3")!=null){
            script_main.m_object_enermy3.transform.forward = (script_main.m_object_selfbody.transform.position - script_main.m_object_enermy3.transform.position).normalized;
        }
        
        // Instantiate(object_coin, measure_loc(float.Parse(script_main.cur_direction_arm1), script_main.localcue_radius, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z), Quaternion.Euler(0, 0, 0));
        // Instantiate(object_coin, measure_loc(float.Parse(script_main.cur_direction_arm2), script_main.localcue_radius, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z), Quaternion.Euler(0, 0, 0));
        // Instantiate(object_coin, measure_loc(float.Parse(script_main.cur_direction_arm3), script_main.localcue_radius, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z), Quaternion.Euler(0, 0, 0));

        script_main.distance_to_localcue1 = Vector3.Distance(script_main.m_object_selfbody.transform.position, measure_loc(float.Parse(script_main.cur_direction_arm1), script_main.localcue_radius, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z));
        script_main.distance_to_localcue2 = Vector3.Distance(script_main.m_object_selfbody.transform.position, measure_loc(float.Parse(script_main.cur_direction_arm2), script_main.localcue_radius, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z));
        script_main.distance_to_localcue3 = Vector3.Distance(script_main.m_object_selfbody.transform.position, measure_loc(float.Parse(script_main.cur_direction_arm3), script_main.localcue_radius, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z));
        script_main.list_distance_to_localcue = new List<float>(){script_main.distance_to_localcue1, script_main.distance_to_localcue2, script_main.distance_to_localcue3};
        script_main.cur_affect_index = script_main.list_distance_to_localcue.IndexOf(script_main.list_distance_to_localcue.Min());
    }

    public void place_predators(){
        int random_pos_bat1 = new System.Random().Next(0, script_main.localcue_num);
        int random_pos_bat2 = new System.Random().Next(0, script_main.localcue_num);
        int random_pos_bat3 = new System.Random().Next(0, script_main.localcue_num);
        Vector3 bat1_pos = new Vector3(script_main.list_plant_locs_x1[random_pos_bat1], script_main.height_bat, script_main.list_plant_locs_z1[random_pos_bat1]) + script_main.loc_center_ground;
        Vector3 bat2_pos = new Vector3(script_main.list_plant_locs_x2[random_pos_bat2], script_main.height_bat, script_main.list_plant_locs_z2[random_pos_bat2]) + script_main.loc_center_ground;
        Vector3 bat3_pos = new Vector3(script_main.list_plant_locs_x3[random_pos_bat3], script_main.height_bat, script_main.list_plant_locs_z3[random_pos_bat3]) + script_main.loc_center_ground;

        if(script_main.cur_list_arm_value_t[0]=="1"){
            script_main.m_object_enermy1 = Instantiate(object_enermy_l1, bat1_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy1.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l1")) as RuntimeAnimatorController;
        }
        if(script_main.cur_list_arm_value_t[0]=="2"){
            script_main.m_object_enermy1 = Instantiate(object_enermy_l2, bat1_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy1.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l2")) as RuntimeAnimatorController;
        }
        if(script_main.cur_list_arm_value_t[0]=="3"){
            script_main.m_object_enermy1 = Instantiate(object_enermy_l3, bat1_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy1.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l3")) as RuntimeAnimatorController;
        }
        if(script_main.cur_list_arm_value_t[0]=="4"){
            script_main.m_object_enermy1 = Instantiate(object_enermy_l4, bat1_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy1.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l4")) as RuntimeAnimatorController;
        }
        script_main.m_object_enermy1.name = "object_enermy1";

        if(script_main.cur_list_arm_value_t[1]=="1"){
            script_main.m_object_enermy2 = Instantiate(object_enermy_l1, bat2_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy2.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l1")) as RuntimeAnimatorController;
        }
        if(script_main.cur_list_arm_value_t[1]=="2"){
            script_main.m_object_enermy2 = Instantiate(object_enermy_l2, bat2_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy2.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l2")) as RuntimeAnimatorController;
        }
        if(script_main.cur_list_arm_value_t[1]=="3"){
            script_main.m_object_enermy2 = Instantiate(object_enermy_l3, bat2_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy2.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l3")) as RuntimeAnimatorController;
        }
        if(script_main.cur_list_arm_value_t[1]=="4"){
            script_main.m_object_enermy2 = Instantiate(object_enermy_l4, bat2_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy2.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l4")) as RuntimeAnimatorController;
        }
        script_main.m_object_enermy2.name = "object_enermy2";

        if(script_main.cur_list_arm_value_t[2]=="1"){
            script_main.m_object_enermy3 = Instantiate(object_enermy_l1, bat3_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy3.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l1")) as RuntimeAnimatorController;
        }
        if(script_main.cur_list_arm_value_t[2]=="2"){
            script_main.m_object_enermy3 = Instantiate(object_enermy_l2, bat3_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy3.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l2")) as RuntimeAnimatorController;
        }
        if(script_main.cur_list_arm_value_t[2]=="3"){
            script_main.m_object_enermy3 = Instantiate(object_enermy_l3, bat3_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy3.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l3")) as RuntimeAnimatorController;
        }
        if(script_main.cur_list_arm_value_t[2]=="4"){
            script_main.m_object_enermy3 = Instantiate(object_enermy_l4, bat3_pos, Quaternion.Euler(0, 0, 0)); 
            script_main.predator_animator = script_main.m_object_enermy3.GetComponent<Animator> ();
            script_main.predator_animator.runtimeAnimatorController = Instantiate(Resources.Load("animator_fly_enermy_l4")) as RuntimeAnimatorController;
        }
        script_main.m_object_enermy3.name = "object_enermy3";

        script_main.cur_list_object_bat = new List<GameObject>(){script_main.m_object_enermy1, script_main.m_object_enermy2, script_main.m_object_enermy3};
        // GameObject child1 = script_main.m_object_enermy1.transform.GetChild(0).gameObject;
        // child1.GetComponent<Renderer>().material = Resources.Load("material_bat", typeof(Material)) as Material;
    }


    public void place_path(){
        int path_counter = 0;
        for (int i = 0; i<80; i+=5){
            Vector3 temp_path_loc = script_main.m_MainCamera.GetComponent<script_utility>().measure_loc(float.Parse(script_main.cur_direction_arm1), i, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z);
            script_main.m_ground_path = Instantiate(ground_path, new Vector3(temp_path_loc.x, temp_path_loc.y, temp_path_loc.z), Quaternion.Euler(0, 180-float.Parse(script_main.cur_direction_arm1), 0)); 
            script_main.m_ground_path.transform.parent = script_main._group_ground_path.transform;
            script_main.m_ground_path.name= "path"+path_counter.ToString();
            path_counter = path_counter + 1;
        }
        for (int i = 0; i<80; i+=5){
            Vector3 temp_path_loc = script_main.m_MainCamera.GetComponent<script_utility>().measure_loc(float.Parse(script_main.cur_direction_arm2), i, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z);
            script_main.m_ground_path = Instantiate(ground_path, new Vector3(temp_path_loc.x, temp_path_loc.y, temp_path_loc.z), Quaternion.Euler(0, 180-float.Parse(script_main.cur_direction_arm2), 0)); 
            script_main.m_ground_path.transform.parent = script_main._group_ground_path.transform;
            script_main.m_ground_path.name="path"+path_counter.ToString();
            path_counter = path_counter + 1;
        }
        for (int i = 0; i<80; i+=5){
            Vector3 temp_path_loc = script_main.m_MainCamera.GetComponent<script_utility>().measure_loc(float.Parse(script_main.cur_direction_arm3), i, 0f, script_main.loc_center_ground.x, script_main.loc_center_ground.z);
            script_main.m_ground_path = Instantiate(ground_path, new Vector3(temp_path_loc.x, temp_path_loc.y, temp_path_loc.z), Quaternion.Euler(0, 180-float.Parse(script_main.cur_direction_arm3), 0)); 
            script_main.m_ground_path.transform.parent = script_main._group_ground_path.transform;
            script_main.m_ground_path.name="path"+path_counter.ToString();
            path_counter = path_counter + 1;
        }
    }
    public void place_coin(){
        // place coin center
        m_object_coin = Instantiate(object_coin, new Vector3(script_main.terrain_size.x/2, script_main.coin_height, script_main.terrain_size.z/2), Quaternion.Euler(0, 0, 0)); 
        m_object_coin.transform.parent = script_main._group_object.transform;
        m_object_coin.tag = "coin_center";
        // place coin start
        Vector3 temp_c_loc = script_main.m_MainCamera.GetComponent<script_utility>().measure_loc(float.Parse(script_main.cur_direction_arm1), script_main.coin_radius, script_main.coin_height, script_main.loc_center_ground.x, script_main.loc_center_ground.z);
        m_object_coin = Instantiate(object_coin, new Vector3(temp_c_loc.x, temp_c_loc.y, temp_c_loc.z), Quaternion.Euler(0, 0, 0)); 
        m_object_coin.transform.parent = script_main._group_object.transform;
        m_object_coin.tag = "coin_start";
        // place coin goal
        Vector3 temp_g_loc = script_main.m_MainCamera.GetComponent<script_utility>().measure_loc(float.Parse(script_main.cur_direction_arm2), script_main.coin_radius, script_main.coin_height, script_main.loc_center_ground.x, script_main.loc_center_ground.z);
        m_object_coin = Instantiate(object_coin, temp_g_loc, Quaternion.Euler(0, 0, 0)); 
        m_object_coin.transform.parent = script_main._group_object.transform;
        m_object_coin.tag = "coin_goal";
        // place coin random
        Vector3 temp_r_loc = script_main.m_MainCamera.GetComponent<script_utility>().measure_loc(float.Parse(script_main.cur_direction_arm3), script_main.coin_radius, script_main.coin_height, script_main.loc_center_ground.x, script_main.loc_center_ground.z);
        m_object_coin = Instantiate(object_coin, temp_r_loc, Quaternion.Euler(0, 0, 0)); 
        m_object_coin.transform.parent = script_main._group_object.transform;
        m_object_coin.tag = "coin_goal";
    }



}
