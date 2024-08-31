using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine.Rendering.PostProcessing;
// using UnityEditor.PackageManager;

public class script_main: MonoBehaviour
{
    // float int string
    public static int distance_camera_selfbody = 15;
    public static int height_camera_selfbody = 13;
    public static int mountain_distance = 95;
    float distance_border = 70;
    float distance_pathLimit = 7;
    public static float distance_initial_between_selfbody_center = 65f;
    public static float coin_radius = 60;
    float threshold_local_cue_distance = 15f;
    int font_size;
    int num_trial;
    public static int distance_bat_fly=40;
    int trial_timelim = 30000;
    int index_target_mountain_bat;
    int snapshot_counter=1;
    public static float camera_height = 4f;
    public static float selfbody_height = 0.9f;
    public static float height_idle = 4f;
    public static float height_bat = 3.5f;
    public static int num_pass_localcue = 0;
    public static float selfbody_colider_height_crouch = 3f;
    public static float coin_height = 3.5f;
    int mouseSensitivity = 75; 
    float movement_speed_factor = 0.4f; 
    public static int localcue_num = 30;
    public static int half_range_affectiveCue = 6;
    public static int localcue_radius = 30;
    public static int num_collect_coin = 0;
    public static int num_points = 0;
    int cur_pl = 0;
    int cur_tl = 0;
    public static string cur_opt1_pl_color, cur_opt1_tl_color, cur_opt2_pl_color, cur_opt2_tl_color, cur_opt3_pl_color, cur_opt3_tl_color;
    string sub_id="";
    string sess_id;
    string str_warning;
    public static string cur_trial_type, cur_direction_arm1, cur_direction_arm2, cur_direction_arm3;
    public static int cur_affect_index;
    public static float distance_to_localcue1, distance_to_localcue2, distance_to_localcue3, distance_to_curPath;
    public static float rot_LR; 
    public static int trial_counter = 0;
    float cur_predator_timeLag, distance_with_bat_goalloc, distance_to_center;
    string exp_status;

    // list
    public static List<string> cur_list_affect_color, cur_list_arm_value_p, cur_list_arm_value_t;
    List<float> list_bat_speed = new List<float>(){40, 50, 60, 70};
    public static List<int> list_coin3rd_value_potection = new List<int>(){0, 5, 10, 15};
    public static List<int> list_coin3rd_value_threat = new List<int>(){5, 10, 15};
    List<string> list_trial_practice;
    public static List<GameObject> cur_list_object_bat;
    List<int> list_trial_index = new List<int>();
    public static List<int> mountain_identity, mountain_orientation, mountain_loc;
    public static List<String> list_plant_color = new List<String>(){"orange", "pink", "purple"};
    public static List<float> list_distance_to_localcue, list_plant_locs_x1, list_plant_locs_x2, list_plant_locs_x3, list_plant_locs_z1, list_plant_locs_z2, list_plant_locs_z3, list_mountain_locs_x, list_mountain_locs_y, list_mountain_locs_z;

    // locs
    public static Vector3 terrain_size, loc_center_ground, localcue_coor_landmark1, localcue_coor_landmark2, localcue_coor_landmark3, loc_curPath;
    Vector3 temp_bat_goal_loc;

    // bool
    public static bool bool_affect_listening, bool_allow_move, bool_show_ima_blood, bool_confidence_key_pressed, predator_move_toward_camera; 
    bool allow_compute_bat_goalloc, show_statusbar, bool_show_affectiveCue, show_gui_menu, show_gui_txt_slider, show_gui_txt_warning, show_gui_txt_outcome, bool_show_fixation, bool_is_crouch, bool_stand_up, predator_move_away_camera, sb1, sb2, sb3, sb4, sb5, sb6, sb7, sb8, show_gui_inst1, show_gui_inst2, show_gui_inst3;

    // object 
    public Camera MainCamera;
    public static Camera m_MainCamera;
    public static GameObject _group_object, _group_ground_path, m_object_selfbody, m_object_colider_head, m_object_colider_foot, m_object_light, m_object_enermy1, m_object_enermy2, m_object_enermy3, m_ground_path, tem_go;
    public GameObject object_grass, object_selfbody, object_colider_head, object_colider_foot, object_light;
    
    // ima
    Texture2D mouseCursor_off, ima_blood, ima_fixation, ima_statusbar, ima_inst1, ima_inst2, ima_inst3;
    public static Texture2D ima_status_left, ima_status_right, ima_affect_cue, ima_rating, ima_whitescreen;
    // time
    public static Stopwatch time_counter_crouch, time_counter_trialTL, time_counter_bloodima, time_counter_affectCue, time_counter_confidence, time_counter_batLag;
    // others
    public Font myNewFont;
    public static Animator predator_animator;
    public static Animator selfbody_animator;

    void Start(){
        // initilize
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow, new RefreshRate() { numerator = 60, denominator = 1 });
        terrain_size = GameObject.Find("terrain_task").GetComponent<Terrain>().terrainData.size;
        font_size = (int)Screen.width/35;
        Instantiate(object_grass, new Vector3(-5, 0, -5), Quaternion.Euler(0, 0, 0)); 

        m_MainCamera = Instantiate(MainCamera, new Vector3(terrain_size.x/2, camera_height, terrain_size.x/2), Quaternion.Euler(0, 0, 0));
        m_MainCamera.GetComponent<script_utility>().camera_to_sky();

        m_object_light = Instantiate(object_light, new Vector3(terrain_size.x/2, 50, terrain_size.z/2), Quaternion.Euler(0, 0, 0));
        m_object_light.transform.localEulerAngles = new Vector3(90, 0, 0);
        
        _group_object = new GameObject("_group_object");
        loc_center_ground = new Vector3(terrain_size.x/2, 0, terrain_size.z/2);
        m_MainCamera.GetComponent<script_utility>().effect_blur("on");
        ima_blood = (Texture2D)Resources.Load("ima_blood");
        ima_fixation = (Texture2D)Resources.Load("ima_fix_on");
        mouseCursor_off = (Texture2D)Resources.Load("ima_null");
        ima_statusbar = (Texture2D)Resources.Load("status_bar");
        ima_inst1 = (Texture2D)Resources.Load("inst1");
        ima_inst2 = (Texture2D)Resources.Load("inst2");
        ima_inst3 = (Texture2D)Resources.Load("inst3");
        show_statusbar = bool_allow_move = bool_show_affectiveCue=show_gui_menu=show_gui_txt_slider=show_gui_txt_warning=show_gui_txt_outcome=bool_show_fixation=bool_is_crouch=bool_stand_up=predator_move_away_camera=sb1=sb2=sb3=sb4=sb5=sb6=sb7=sb8=show_gui_inst1=show_gui_inst2=show_gui_inst3 = false;
        mountain_identity = mountain_orientation = mountain_loc = new List<int>();
        bool_show_ima_blood = bool_confidence_key_pressed = predator_move_toward_camera = false;
        //mountain para
        for (int ith=0; ith<360; ith+=20){
            mountain_loc.Add(ith);
            if(new System.Random().Next(1, 10)>5){
                mountain_identity.Add(1);
            } else {
                mountain_identity.Add(0);
            }
            if(new System.Random().Next(1, 10)>5){
                mountain_orientation.Add(1);
            } else {
                mountain_orientation.Add(0);
            }
        } 
        show_gui_menu = true;
    }

    void show_fixation (){
        m_MainCamera.GetComponent<script_utility>().clear_space();
        m_MainCamera.GetComponent<script_utility>().camera_to_sky();
        Vector2 hotSpot = new Vector2(0, 0);
        Cursor.SetCursor(mouseCursor_off, hotSpot, CursorMode.ForceSoftware);
        m_MainCamera.GetComponent<script_utility>().effect_blur("on");
        bool_show_fixation = true;
        show_gui_menu = false;
        Invoke("trial_start", 1.5f);
    }
    
    void trial_start(){
        // reset
        m_MainCamera.GetComponent<script_utility>().clear_space();
        bool_show_fixation = false;

        // load trials
        if(exp_status=="practice"){
            list_trial_practice = m_MainCamera.GetComponent<script_utility>().TextAssetToList((TextAsset)Resources.Load("table_trial_practice"));
            list_trial_index = Enumerable.Range(0, list_trial_practice.Count-1).ToList();
            list_trial_index = m_MainCamera.GetComponent<script_utility>().ShuffleList(list_trial_index);
            num_trial = 10;
        }
        if(exp_status=="formal"){
            list_trial_practice = m_MainCamera.GetComponent<script_utility>().TextAssetToList((TextAsset)Resources.Load("table_trial_run" + sess_id));
            list_trial_index = Enumerable.Range(0, list_trial_practice.Count-1).ToList();
            list_trial_index = m_MainCamera.GetComponent<script_utility>().ShuffleList(list_trial_index);
            num_trial = list_trial_practice.Count-1;
        }
        string[] trial_info = list_trial_practice[list_trial_index.ElementAt(trial_counter)].Split('\t');

        // cur info
        cur_trial_type = trial_info[0];
        cur_direction_arm1 = trial_info[10];
        cur_direction_arm2 = trial_info[11];
        cur_direction_arm3 = trial_info[12];

        //
        cur_list_arm_value_p = new List<string>(){trial_info[2], trial_info[5], trial_info[8]};
        cur_list_arm_value_t = new List<string>(){trial_info[3], trial_info[6], trial_info[9]};
        cur_list_affect_color = new List<string>(){trial_info[1], trial_info[4], trial_info[7]};
        
        // install_env
        m_MainCamera.GetComponent<script_utility>().install_env();
        // place camera start loc
        Vector3 temp_loc = m_MainCamera.GetComponent<script_utility>().measure_loc(float.Parse(cur_direction_arm1), distance_initial_between_selfbody_center, camera_height, loc_center_ground.x, loc_center_ground.z);
        m_object_selfbody = Instantiate(object_selfbody, new Vector3(terrain_size.x/2, selfbody_height, terrain_size.z/2), Quaternion.Euler(0, 0, 0)); 
        m_object_selfbody.transform.position = new Vector3(temp_loc.x, selfbody_height, temp_loc.z);
        m_object_selfbody.transform.forward = (new Vector3(terrain_size.x/2, selfbody_height, terrain_size.z/2) - m_object_selfbody.transform.position).normalized;
        m_object_colider_head = Instantiate(object_colider_head, new Vector3(terrain_size.x/2, selfbody_height, terrain_size.z/2), Quaternion.Euler(0, 0, 0)); 
        m_object_colider_head.transform.position = new Vector3(m_object_selfbody.transform.position.x, height_idle, m_object_selfbody.transform.position.z);
        m_object_colider_foot = Instantiate(object_colider_foot, new Vector3(terrain_size.x/2, 1f, terrain_size.z/2), Quaternion.Euler(0, 0, 0)); 
        m_object_colider_foot.transform.position = new Vector3(m_object_selfbody.transform.position.x, 1f, m_object_selfbody.transform.position.z);
        
        rot_LR = m_object_selfbody.transform.localEulerAngles.y;
        m_MainCamera.GetComponent<script_utility>().attach_camera_on_selfbody();

        //
        cur_predator_timeLag = new System.Random().Next(50, 150)*1000f/100f;

        // reset
        index_target_mountain_bat = new System.Random().Next(0, list_mountain_locs_x.Count);
        m_MainCamera.GetComponent<script_utility>().effect_blur("off");
        show_statusbar = true;
        bool_show_affectiveCue = false;
        allow_compute_bat_goalloc = true;
        show_gui_txt_slider = false;
        ima_status_left = (Texture2D)Resources.Load("ima_null_protection");
        ima_status_right = (Texture2D)Resources.Load("ima_null_threat");
        ima_whitescreen = (Texture2D)Resources.Load("whitescreen");
        num_points=0;
        num_collect_coin=0;
        m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_idle");
        bool_allow_move=true;     
        bool_affect_listening = true;
        num_pass_localcue = 0;
        cur_pl = 0;
        cur_tl = 0;
        loc_curPath = m_object_selfbody.transform.position;

        // trial time counter
        time_counter_trialTL = new Stopwatch();
        time_counter_trialTL.Start();
    }

    void Update()
    {
        if(bool_allow_move){
            //get cur_affect_index
            m_MainCamera.GetComponent<script_utility>().compute_distance_to_localcues();
            //
            if (bool_affect_listening & list_distance_to_localcue.Min()<=threshold_local_cue_distance & !bool_show_affectiveCue){
                bool_affect_listening = false;
                bool_allow_move = false;
                m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_idle");
                // if threat
                if ((cur_trial_type=="tp" & num_collect_coin==1 & num_pass_localcue==0)  | (cur_trial_type=="pt" & num_collect_coin==2 & num_pass_localcue==1)){
                    cur_tl = int.Parse(cur_list_arm_value_t[cur_affect_index]);
                    ima_affect_cue = (Texture2D)Resources.Load("localcue_"+ cur_list_affect_color[cur_affect_index] + "_t");
                    bool_show_affectiveCue = true;
                    time_counter_affectCue = new Stopwatch();
                    time_counter_affectCue.Start();
                }
                // if protection
                else if ((cur_trial_type=="pt" & num_collect_coin==1 & num_pass_localcue==0)  | (cur_trial_type=="tp" & num_collect_coin==2 & num_pass_localcue==1)){
                    cur_pl = int.Parse(cur_list_arm_value_p[cur_affect_index]);
                    cur_tl = int.Parse(cur_list_arm_value_t[cur_affect_index]);
                    ima_affect_cue = (Texture2D)Resources.Load("localcue_"+ cur_list_affect_color[cur_affect_index] + "_p");
                    bool_show_affectiveCue = true;
                    time_counter_affectCue = new Stopwatch();
                    time_counter_affectCue.Start();
                }
                else{
                    // limits on 2nd coin
                    bool_allow_move=false;
                    show_statusbar=false;
                    m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_idle");
                    m_MainCamera.GetComponent<script_utility>().effect_blur("on");
                    show_gui_txt_warning = true;
                    str_warning = "Game aborted, wrong direction";
                    Invoke("go_next_trial", 2);
                }
            }
            if(num_collect_coin==3){
                m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_idle");
                num_collect_coin = 0;
                show_statusbar=false;
                bool_allow_move=false;
                m_MainCamera.GetComponent<script_utility>().effect_blur("on");
                show_gui_txt_outcome = true;
                Invoke("go_next_trial", 2);
            }

            // limits on time
            if(time_counter_trialTL.ElapsedMilliseconds>trial_timelim){
                bool_allow_move=false;
                show_statusbar=false;
                m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_idle");
                m_MainCamera.GetComponent<script_utility>().effect_blur("on");
                show_gui_txt_warning = true;
                str_warning = "Game aborted, reach time limitation";
                Invoke("go_next_trial", 2);
            }
            // limits on border
            distance_to_center = Vector3.Distance(m_object_selfbody.transform.position, loc_center_ground);
            if(distance_to_center<=10f){
                bool_affect_listening=true;
            }
            if(distance_to_center>=distance_border){
                bool_allow_move=false;
                show_statusbar=false;
                m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_idle");
                m_MainCamera.GetComponent<script_utility>().effect_blur("on");
                show_gui_txt_warning = true;
                str_warning = "Game aborted, wrong direction";
                Invoke("go_next_trial", 2);
            }
            // limits on path
            distance_to_curPath = Vector3.Distance(m_object_selfbody.transform.position, loc_curPath);
            if( distance_to_curPath>distance_pathLimit){
                bool_allow_move=false;
                show_statusbar=false;
                m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_idle");
                m_MainCamera.GetComponent<script_utility>().effect_blur("on");
                show_gui_txt_warning = true;
                str_warning = "Game aborted, wrong direction";
                Invoke("go_next_trial", 2);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_run");
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_idle");
            }
        } 

        if(!bool_is_crouch & !bool_stand_up & Input.GetKeyDown(KeyCode.DownArrow) & !bool_show_fixation)
        {
            m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_crouch");
            m_object_colider_head.transform.position = new Vector3(m_object_selfbody.transform.position.x, selfbody_colider_height_crouch, m_object_selfbody.transform.position.z);
            bool_is_crouch = true;
            num_points = num_points-2;
            time_counter_crouch = new Stopwatch();
            time_counter_crouch.Start();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            back_to_menu();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.T))             
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/snapshot"+snapshot_counter.ToString()+".png");
            snapshot_counter = snapshot_counter + 1;
        }
    } // update

    void FixedUpdate()
    {
        if(bool_show_affectiveCue){
            if(time_counter_affectCue.ElapsedMilliseconds>1000){
                time_counter_affectCue.Stop();
                bool_show_affectiveCue = false;
                // if threat
                if ((cur_trial_type=="pt" & num_collect_coin==2) | (cur_trial_type=="tp" & num_collect_coin==1)){
                    ima_status_right = (Texture2D)Resources.Load("localcue_"+ cur_list_affect_color[cur_affect_index].ToString() + "_t");
                }
                // if protection
                if ((cur_trial_type=="pt" & num_collect_coin==1) | (cur_trial_type=="tp" & num_collect_coin==2)){
                    ima_status_left = (Texture2D)Resources.Load("localcue_"+ cur_list_affect_color[cur_affect_index].ToString() + "_p");
                }
                // 
                if (cur_tl>cur_pl){
                    ima_rating = (Texture2D)Resources.Load("rating_null");
                    show_statusbar = false;
                    m_MainCamera.GetComponent<script_utility>().effect_blur("on");
                    show_gui_txt_slider = true;
                }
                if (cur_tl<=cur_pl){
                    predator_move_away_camera = true;
                    bool_allow_move = true;
                }
            }
        }
        if(bool_confidence_key_pressed){
            if(time_counter_confidence.ElapsedMilliseconds>500){
                time_counter_confidence.Stop();
                show_gui_txt_slider = false;
                show_statusbar = true;
                bool_confidence_key_pressed = false;
                m_MainCamera.GetComponent<script_utility>().effect_blur("off");
                time_counter_batLag = new Stopwatch();
                time_counter_batLag.Start();
                predator_move_toward_camera = true;
            }
        }
        // enermy moves towards
        if(predator_move_toward_camera){
            if(time_counter_batLag.ElapsedMilliseconds>cur_predator_timeLag){
                time_counter_batLag.Stop();
                if(allow_compute_bat_goalloc){
                    allow_compute_bat_goalloc = false;
                    temp_bat_goal_loc = m_MainCamera.GetComponent<script_utility>().compute_bat_goal_loc(cur_list_object_bat[cur_affect_index].transform.position);
                }
                cur_list_object_bat[cur_affect_index].transform.position = Vector3.MoveTowards(cur_list_object_bat[cur_affect_index].transform.position, temp_bat_goal_loc, list_bat_speed[int.Parse(cur_list_arm_value_t[cur_affect_index])-1] * Time.deltaTime);
                distance_with_bat_goalloc = Vector3.Distance(temp_bat_goal_loc, cur_list_object_bat[cur_affect_index].transform.position);
                // success
                if(distance_with_bat_goalloc<0.5f){
                    bool_allow_move = true;
                    predator_move_toward_camera = false;
                    allow_compute_bat_goalloc = true;
                    Destroy(cur_list_object_bat[cur_affect_index]);
                }
            }
        }
        // enermy moves away
        if(predator_move_away_camera){
            Vector3 temp_loc = new Vector3(list_mountain_locs_x[index_target_mountain_bat], list_mountain_locs_y[index_target_mountain_bat], list_mountain_locs_z[index_target_mountain_bat]);
            cur_list_object_bat[cur_affect_index].transform.position = Vector3.MoveTowards(cur_list_object_bat[cur_affect_index].transform.position, temp_loc, 40 * Time.deltaTime);
            distance_with_bat_goalloc = Vector3.Distance(m_object_selfbody.transform.position, cur_list_object_bat[cur_affect_index].transform.position);
            if(distance_with_bat_goalloc>distance_bat_fly){
                predator_move_away_camera = false;
                Destroy(cur_list_object_bat[cur_affect_index]);
            }
        }
        if (bool_is_crouch){
            if(time_counter_crouch.ElapsedMilliseconds>150){
                bool_is_crouch = false;
                bool_stand_up = true;
                time_counter_crouch.Stop();
            }
        }
        if(bool_stand_up){
            bool_stand_up = false;
            m_MainCamera.GetComponent<script_utility>().add_selfbody_animation("animator_selfbody_idle");
            m_object_colider_head.transform.position = new Vector3(m_object_selfbody.transform.position.x, height_idle, m_object_selfbody.transform.position.z);
        }
        if(bool_show_ima_blood){
            if(time_counter_bloodima.ElapsedMilliseconds>500){
                time_counter_bloodima.Stop();
                bool_show_ima_blood = false;
            }
        }
        if(bool_allow_move & !predator_move_toward_camera){
            if(Input.GetKey(KeyCode.UpArrow))
            {
                m_object_selfbody.transform.Translate(Vector3.forward * movement_speed_factor);
                m_object_colider_head.transform.position = new Vector3(m_object_selfbody.transform.position.x, height_idle, m_object_selfbody.transform.position.z);
                m_object_colider_foot.transform.position = new Vector3(m_object_selfbody.transform.position.x, 1f, m_object_selfbody.transform.position.z);
                rot_LR = m_object_selfbody.transform.localEulerAngles.y;
                m_MainCamera.GetComponent<script_utility>().attach_camera_on_selfbody();
            }
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                rot_LR += -mouseSensitivity * Time.deltaTime;
                m_object_selfbody.transform.localEulerAngles = new Vector3(0f, rot_LR, 0.0f);
                m_MainCamera.GetComponent<script_utility>().attach_camera_on_selfbody();
            }
            if(Input.GetKey(KeyCode.RightArrow))
            {
                rot_LR += mouseSensitivity * Time.deltaTime;
                m_object_selfbody.transform.localEulerAngles = new Vector3(0f, rot_LR, 0.0f);
                m_MainCamera.GetComponent<script_utility>().attach_camera_on_selfbody();
            }
        }
    }


    void go_next_trial(){
        show_gui_txt_outcome = false;
        show_gui_txt_warning = false;
        bool_allow_move = false;
        show_statusbar = false;
        bool_show_affectiveCue = false;
        show_gui_menu = false;
        show_gui_txt_slider = false;
        trial_counter = trial_counter+1;
        time_counter_trialTL.Stop();    
        if(trial_counter==num_trial){
            back_to_menu();
        } else {
            show_fixation();
        }
    }
    void back_to_menu(){
        m_MainCamera.GetComponent<script_utility>().clear_space();
        m_MainCamera.GetComponent<script_utility>().camera_to_sky();
        m_MainCamera.GetComponent<script_utility>().effect_blur("on");
        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        show_gui_txt_outcome = false;
        show_gui_txt_warning = false;
        bool_allow_move = false;
        show_statusbar = false;
        bool_show_affectiveCue = false;
        show_gui_menu = true;
        show_gui_txt_slider = false;
        trial_counter = 0;
        exp_status = "null";
    }





    ////////////////
    void OnGUI()
	{
        if(show_gui_menu){
            GUI.Window(0, new Rect( Screen.width/2 - (Screen.width/1.8f)/2, Screen.height/15, Screen.width/1.8f, 9*Screen.height/10f), GUI_mainMenu, "");
        }
        if(show_gui_inst1){
            GUI.Window(1, new Rect( Screen.width/2 - (Screen.width/1.2f)/2, Screen.height/15, Screen.width/1.2f, 9*Screen.height/10f), GUI_inst1, "");
        }
        if(show_gui_inst2){
            GUI.Window(1, new Rect( Screen.width/2 - (Screen.width/1.2f)/2, Screen.height/15, Screen.width/1.2f, 9*Screen.height/10f), GUI_inst2, "");
        }
        if(show_gui_inst3){
            GUI.Window(1, new Rect( Screen.width/2 - (Screen.width/1.2f)/2, Screen.height/15, Screen.width/1.2f, 9*Screen.height/10f), GUI_inst3, "");
        }
        if (bool_show_fixation){            
            var GUIStyle_box = new GUIStyle();
            int box_size = (int)(Screen.width*0.05f);
            GUI.Box(new Rect(Screen.width/2-box_size/2, Screen.height*0.4f, box_size, box_size), ima_fixation, GUIStyle_box);
        }
        if(show_gui_txt_slider){
            m_MainCamera.GetComponent<script_utility>().GetPressedNumber();
            var GUIStyle_text = new GUIStyle();
            GUIStyle_text.fontSize = (int)(font_size*1.2f);
            GUIStyle_text.normal.textColor = Color.black;
            string str_confi = "How confident are you?";
            Vector2 size_str_confi = GUIStyle_text.CalcSize( new GUIContent(str_confi.ToString()));
            GUI.Box(new Rect(0, 0, ima_whitescreen.width, ima_whitescreen.height), ima_whitescreen, new GUIStyle());
            GUI.Label(new Rect(Screen.width/2-size_str_confi[0]/2, Screen.height*0.3f, size_str_confi[0], size_str_confi[1]), str_confi, GUIStyle_text);
            GUI.Box(new Rect(Screen.width/2-ima_rating.width/2, Screen.height*0.4f, (int)(Screen.width), (int)(Screen.width*167/1250)), ima_rating, new GUIStyle());
        }
        if (show_statusbar){
            GUI.Box(new Rect(0, Screen.height-ima_statusbar.height, ima_statusbar.width, ima_statusbar.height), ima_statusbar, new GUIStyle());            
            var GUIStyle_text = new GUIStyle();
            GUIStyle_text.fontSize = (int)(font_size*1.2f);
            GUIStyle_text.normal.textColor = Color.black;
            string str_coin = "Points: " + num_points.ToString();
            Vector2 size_str_coin = GUIStyle_text.CalcSize( new GUIContent(str_coin));
            GUI.skin.textField.fontSize = (int)(font_size*1.2f);
            GUI.skin.button.fontSize = (int)(font_size*1.2f);
            GUI.skin.toggle.fontSize = (int)(font_size*1.2f);
            GUI.Label(new Rect(Screen.width/2-size_str_coin[0]/2, Screen.height-(8*ima_statusbar.height/10), size_str_coin[0], size_str_coin[1]), str_coin, GUIStyle_text);
            GUI.Box(new Rect(Screen.width*0.1f-ima_status_left.width/2, Screen.height-ima_status_left.height, ima_status_left.width, ima_status_left.height), ima_status_left, new GUIStyle());
            GUI.Box(new Rect(Screen.width-Screen.width*0.1f-ima_status_right.width/2, Screen.height-ima_status_right.height, ima_status_right.width, ima_status_right.height), ima_status_right, new GUIStyle());
        }
        if (bool_show_affectiveCue){            
            int box_size = (int)(Screen.width*0.2f);
            GUI.Box(new Rect(Screen.width/2-ima_affect_cue.width/2, Screen.height*0.55f, box_size, box_size), ima_affect_cue, new GUIStyle());
        }
        if(show_gui_txt_warning){
            var GUIStyle_text = new GUIStyle();
            GUIStyle_text.fontSize = (int)(font_size*1.2f);
            GUIStyle_text.normal.textColor = Color.black;
            Vector2 size_str = GUIStyle_text.CalcSize( new GUIContent(str_warning.ToString()));
            GUI.Box(new Rect(0, 0, ima_whitescreen.width, ima_whitescreen.height), ima_whitescreen, new GUIStyle());
            GUI.Label(new Rect(Screen.width/2-size_str[0]/2, Screen.height*0.4f, size_str[0], size_str[1]), str_warning, GUIStyle_text);
        }
        if(show_gui_txt_outcome){
            var GUIStyle_text = new GUIStyle();
            GUIStyle_text.fontSize = (int)(font_size*1.2f);
            GUIStyle_text.normal.textColor = Color.black;
            string str_outcome = "Your earnings: " + num_points.ToString();
            Vector2 size_str = GUIStyle_text.CalcSize( new GUIContent(str_outcome.ToString()));
            GUI.Box(new Rect(0, 0, ima_whitescreen.width, ima_whitescreen.height), ima_whitescreen, new GUIStyle());
            GUI.Label(new Rect(Screen.width/2-size_str[0]/2, Screen.height*0.4f, size_str[0], size_str[1]), str_outcome, GUIStyle_text);
        }
        if(bool_show_ima_blood){
            int box_size = (int)(Screen.width*0.2f);
            GUI.Box(new Rect(Screen.width/2-ima_blood.width/2, Screen.height*0.6f, box_size, box_size), ima_blood, new GUIStyle());
        }
        if(exp_status=="practice"){
            var GUIStyle_text = new GUIStyle();
            GUIStyle_text.fontSize = (int)(font_size*1.2f);
            GUIStyle_text.normal.textColor = Color.black;
            string str_coin = "Press ‘Q’ to quit when you are ready.";
            Vector2 size_str_coin = GUIStyle_text.CalcSize( new GUIContent(str_coin));
            GUI.Label(new Rect(Screen.width/2-size_str_coin[0]/2, Screen.height-(16*ima_statusbar.height/10), size_str_coin[0], size_str_coin[1]), str_coin, GUIStyle_text);
        }
	}

    
    void GUI_mainMenu(int windowID)
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        var style_label = GUI.skin.GetStyle("Label");
        var style_button = GUI.skin.GetStyle("button");
        var style_textfield = GUI.skin.GetStyle("textField");
        var style_toggle = GUI.skin.GetStyle("Toggle");
        style_toggle.fontSize=style_label.fontSize = style_button.fontSize = style_textfield.fontSize = font_size;
        var button_GUIStyle = GUI.skin.GetStyle("button");
        button_GUIStyle.fontSize = (int)(font_size*0.8f);
        
        // left
        GUI.Label(new Rect((Screen.width/4)*0.25f, (Screen.height/2)*0.2f, style_label.CalcSize( new GUIContent("1. Instruction:")).x, style_label.CalcSize( new GUIContent("1. Instruction:")).y), "1. Instruction:", style_label);
        GUI.Label(new Rect((Screen.width/4)*0.25f, (Screen.height/2)*0.4f, style_label.CalcSize( new GUIContent("2. Practice:")).x, style_label.CalcSize( new GUIContent("2. Practice:")).y), "2. Practice:", style_label);
        GUI.Label(new Rect((Screen.width/4)*0.25f, (Screen.height/2)*0.7f, style_label.CalcSize( new GUIContent("3. If ready:")).x, style_label.CalcSize( new GUIContent("3. If ready:")).y), "3. If ready:", style_label);
        //right
        // GUI.enabled = false;

        if (GUI.Button(new Rect((Screen.width/4)*1f, (Screen.height/2)*0.2f, (Screen.width/4), Screen.height/15), "Readme", button_GUIStyle))
        {
            show_gui_menu = false;
            show_gui_inst1 = true;
        }
        if (GUI.Button(new Rect((Screen.width/4)*1f, (Screen.height/2)*0.4f, (Screen.width/4), Screen.height/15), "Start test", button_GUIStyle))
        {
            show_gui_menu=false;
            exp_status = "practice";
            show_fixation();
        }
        // middle
        GUI.Label(new Rect((Screen.width/4)*0.4f, (Screen.height/2)*0.9f, style_label.CalcSize( new GUIContent("Sub ID")).x, style_label.CalcSize( new GUIContent("Sub ID")).y), "Sub ID", style_label);
        sub_id = GUI.TextField(new Rect((Screen.width/4)*0.85f, (Screen.height/2)*0.9f, 100, Screen.height/15), sub_id, 2, style_textfield);
        GUI.Label(new Rect((Screen.width/4)*0.4f, (Screen.height/2)*1.1f, style_label.CalcSize( new GUIContent("Session")).x, style_label.CalcSize( new GUIContent("Session")).y), "Session", style_label);

        sess_id = null;
        GUI.backgroundColor = Color.white;
        sb1 = GUI.Toggle(new Rect((Screen.width/4)*0.85f, (Screen.height/2)*1.1f, 100, Screen.height/15), sb1, "I", style_button);
        if(sb1){
            GUI.backgroundColor = Color.green;
            sb1 = GUI.Toggle(new Rect((Screen.width/4)*0.85f, (Screen.height/2)*1.1f, 100, Screen.height/15), sb1, "I", style_button);
            sb2=sb3=sb4=sb5=sb6=sb7=sb8 = false;
            sess_id = "1";
            GUI.backgroundColor = Color.white;
        }
        sb2 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+150, (Screen.height/2)*1.1f, 100, Screen.height/15), sb2, "II", style_button);
        if(sb2){
            GUI.backgroundColor = Color.green;
            sb2 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+150, (Screen.height/2)*1.1f, 100, Screen.height/15), sb2, "II", style_button);
            sb1=sb3=sb4=sb5=sb6=sb7=sb8 = false;
            sess_id = "2";
            GUI.backgroundColor = Color.white;
        }
        sb3 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+300, (Screen.height/2)*1.1f, 100, Screen.height/15), sb3, "III", style_button);
        if(sb3){
            GUI.backgroundColor = Color.green;
            sb3 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+300, (Screen.height/2)*1.1f, 100, Screen.height/15), sb3, "III", style_button);
            sb1=sb2=sb4=sb5=sb6=sb7=sb8 = false;
            sess_id = "3";
            GUI.backgroundColor = Color.white;
        }
        sb4 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+450, (Screen.height/2)*1.1f, 100, Screen.height/15), sb4, "IV", style_button);
        if(sb4){
            GUI.backgroundColor = Color.green;
            sb4 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+450, (Screen.height/2)*1.1f, 100, Screen.height/15), sb4, "IV", style_button);
            sb1=sb2=sb3=sb5=sb6=sb7=sb8 = false;
            sess_id = "4";
            GUI.backgroundColor = Color.white;
        }
        sb5 = GUI.Toggle(new Rect((Screen.width/4)*0.85f, (Screen.height/2)*1.3f, 100, Screen.height/15), sb5, "V", style_button);
        if(sb5){
            GUI.backgroundColor = Color.green;
            sb5 = GUI.Toggle(new Rect((Screen.width/4)*0.85f, (Screen.height/2)*1.3f, 100, Screen.height/15), sb5, "V", style_button);
            sb1=sb2=sb3=sb4=sb6=sb7=sb8 = false;
            sess_id = "5";
            GUI.backgroundColor = Color.white;
        }
        sb6 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+150, (Screen.height/2)*1.3f, 100, Screen.height/15), sb6, "VI", style_button);
        if(sb6){
            GUI.backgroundColor = Color.green;
            sb6 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+150, (Screen.height/2)*1.3f, 100, Screen.height/15), sb6, "VI", style_button);
            sb1=sb2=sb3=sb4=sb5=sb7=sb8 = false;
            sess_id = "6";
            GUI.backgroundColor = Color.white;
        }
        sb7 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+300, (Screen.height/2)*1.3f, 100, Screen.height/15), sb7, "VII", style_button);
        if(sb7){
            GUI.backgroundColor = Color.green;
            sb7 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+300, (Screen.height/2)*1.3f, 100, Screen.height/15), sb7, "VII", style_button);
            sb1=sb2=sb3=sb4=sb5=sb6=sb8 = false;
            sess_id = "7";
            GUI.backgroundColor = Color.white;
        }
        sb8 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+450, (Screen.height/2)*1.3f, 100, Screen.height/15), sb8, "VIII", style_button);
        if(sb8){
            GUI.backgroundColor = Color.green;
            sb8 = GUI.Toggle(new Rect((Screen.width/4)*0.85f+450, (Screen.height/2)*1.3f, 100, Screen.height/15), sb8, "VIII", style_button);
            sb1=sb2=sb3=sb4=sb5=sb6=sb7 = false;
            sess_id = "8";
            GUI.backgroundColor = Color.white;
        }
        // GUI.enabled = true;
        if (GUI.Button(new Rect((Screen.width/4)*1f, (Screen.height/2)*1.6f, Screen.width/4, Screen.height/15), "Start task", button_GUIStyle))
        {
            if (!string.IsNullOrEmpty(sess_id)){
                show_gui_menu=false;
                exp_status = "formal";
                show_fixation();
            }
        }
    }

    void GUI_inst1(int windowID){
        // Screen.width/2 - (Screen.width/1.2f)/2, Screen.height/15, Screen.width/1.2f, 9*Screen.height/10f
        var cur_GUIStyle = GUI.skin.GetStyle("button");
        cur_GUIStyle.fontSize = (int)(font_size*0.8f);
        GUI.Box(new Rect(Screen.width/1.2f/2-ima_inst1.width/2, Screen.height*0.15f, ima_inst1.width, ima_inst1.height), ima_inst1, new GUIStyle());

        GUI.backgroundColor = Color.green;
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.02f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "1.Collect coin", cur_GUIStyle))
        {
        }
        GUI.backgroundColor = Color.white;
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.27f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "2.Enemy", cur_GUIStyle))
        {
            show_gui_inst1 = show_gui_inst3 = false;
            show_gui_inst2 = true;
        }
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.52f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "3.Protection", cur_GUIStyle))
        {
            show_gui_inst1 = show_gui_inst2 = false;
            show_gui_inst3 = true;
        }
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.77f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "4.Back to menu", cur_GUIStyle))
        {
            show_gui_menu = true;
            show_gui_inst1 = show_gui_inst2 = show_gui_inst3 = false;
        }
    }
    void GUI_inst2(int windowID){
        // Screen.width/2 - (Screen.width/1.2f)/2, Screen.height/15, Screen.width/1.2f, 9*Screen.height/10f
        var cur_GUIStyle = GUI.skin.GetStyle("button");
        cur_GUIStyle.fontSize = (int)(font_size*0.8f);
        GUI.Box(new Rect(Screen.width/1.2f/2-ima_inst2.width/2, Screen.height*0.15f, ima_inst2.width, ima_inst2.height), ima_inst2, new GUIStyle());

        GUI.backgroundColor = Color.white;
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.02f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "1.Collect coin", cur_GUIStyle))
        {
            show_gui_inst2 = show_gui_inst3 = false;
            show_gui_inst1 = true;
        }
        GUI.backgroundColor = Color.green;
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.27f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "2.Enemy", cur_GUIStyle))
        {
        }
        GUI.backgroundColor = Color.white;
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.52f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "3.Protection", cur_GUIStyle))
        {
            show_gui_inst1 = show_gui_inst2 = false;
            show_gui_inst3 = true;
        }
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.77f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "4.Back to menu", cur_GUIStyle))
        {
            show_gui_menu = true;
            show_gui_inst1 = show_gui_inst2 = show_gui_inst3 = false;
        }
    }
    void GUI_inst3(int windowID){
        // Screen.width/2 - (Screen.width/1.2f)/2, Screen.height/15, Screen.width/1.2f, 9*Screen.height/10f
        var cur_GUIStyle = GUI.skin.GetStyle("button");
        cur_GUIStyle.fontSize = (int)(font_size*0.8f);
        GUI.Box(new Rect(Screen.width/1.2f/2-ima_inst3.width/2, Screen.height*0.15f, ima_inst3.width, ima_inst3.height), ima_inst3, new GUIStyle());

        GUI.backgroundColor = Color.white;
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.02f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "1.Collect coin", cur_GUIStyle))
        {
            show_gui_inst2 = show_gui_inst3 = false;
            show_gui_inst1 = true;
        }
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.27f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "2.Enemy", cur_GUIStyle))
        {
            show_gui_inst1 = show_gui_inst3 = false;
            show_gui_inst2 = true;
        }
        GUI.backgroundColor = Color.green;
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.52f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "3.Protection", cur_GUIStyle))
        {
        }
        GUI.backgroundColor = Color.white;
        if (GUI.Button(new Rect((Screen.width/1.2f)*0.77f, 1*Screen.height/30f, Screen.width/5.5f, Screen.height/15), "4.Back to menu", cur_GUIStyle))
        {
            show_gui_menu = true;
            show_gui_inst1 = show_gui_inst2 = show_gui_inst3 = false;
        }
    }
} //class









