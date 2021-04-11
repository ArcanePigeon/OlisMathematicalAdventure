using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelScript : MonoBehaviour {
    public GameObject duck;
    public int currentLane = 0;
    public GameObject[] lane = new GameObject[4];
    public GameObject[] duckLanes = new GameObject[4];
    public GameObject foliage1;
    public GameObject foliage2;
    public GameObject answer;
    public GameObject lilyPad;
    public GameObject healthCrate;
    public GameObject boosterCrate;
    public GameObject rock;
    public TMP_Text questionTMP;
    public TMP_Text scoreTMP;
    public int health = 6;
    public int score = 0;
    public string question = "5X5";
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown("w")){
            if(currentLane >= 1){
                currentLane--;
            }
        }
        if(Input.GetKeyDown("s")){
            if(currentLane <= 2){
                currentLane++;
            }
        }
        if(duck.transform.position.y != duckLanes[currentLane].transform.position.y){
            duck.transform.position = Vector2.Lerp(duck.transform.position,duckLanes[currentLane].transform.position,Time.deltaTime*moveSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "obstacle"){
            health -= 1;
        }
    }
}
