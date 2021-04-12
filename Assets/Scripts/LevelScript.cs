using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelScript : MonoBehaviour {
    // public GameObject duck;
    // public int currentLane = 0;
    // public GameObject[] lane = new GameObject[4];
    // public GameObject[] duckLanes = new GameObject[4];
    // public GameObject[] hearts = new GameObject[6];
    // public GameObject foliage1;
    // public GameObject foliage2;
    // public GameObject answer;
    // public GameObject lilyPad;
    // public GameObject healthCrate;
    // public GameObject boosterCrate;
    // public GameObject rocketBooster;
    // public GameObject rock;
    // public TMP_Text questionTMP;
    // public TMP_Text scoreTMP;
    // public int health = 6;
    // public int score = 0;
    // public string question = "5X5";
    // public float moveSpeed = 5f;

    // // Start is called before the first frame update
    // void Start() {
    //     rocketBooster.SetActive(false);
    // }

    // // Update is called once per frame
    // void Update() {
    //     if(Input.GetKeyDown("w")){
    //         if(currentLane >= 1){
    //             currentLane--;
    //         }
    //     }
    //     if(Input.GetKeyDown("s")){
    //         if(currentLane <= 2){
    //             currentLane++;
    //         }
    //     }
    //     if(duck.transform.position.y != duckLanes[currentLane].transform.position.y){
    //         duck.transform.position = Vector2.Lerp(duck.transform.position,duckLanes[currentLane].transform.position,Time.deltaTime*moveSpeed);
    //     }
    // }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.tag == "obstacle"){
    //         health -= 1;
    //     }
        
    //     // only want Oli's collisions

    //     // if Oli collides with a health boost crate, hide crate
    //     //  then increase his health by 2 (full heart value)
    //     //  and call SetHeartView with new health


    //     // if Oli collides with obstacles (rock/lily pad), hide obstacle
    //     //  then decrease his health by 1 (half heart value)
    //     //  and call SetHeartView with new health

    //     // if Oli collides with a jet pack crate, hide crate
    //     //  then start a 5 sec interval, set jet pack to active and increase
    //     //  speed. After 5 sec set jet pack inactive and reduce to normal speed


    // }

    // private void SetHeartView(int health) {
    //     if (health != 0) {
    //         for (int i = 1; i <= health; i++) {
    //             hearts[i].SetActive(true);
    //         }

    //         for (int i = health; i <= 6; i++) {
    //             hearts[i].SetActive(false);
    //         }
    //     }
    // }
}