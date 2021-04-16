using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DuckLevelScript : MonoBehaviour {
    public enum Items{
        LilyPad,
        Rock,
        Bread,
        HealthCrate,
        BoosterCrate,
        Tree,
        Bush
    }
    public GameObject duck;
    public int currentLane = 0;
    public GameObject[] lane = new GameObject[4];
    public GameObject[] duckLanes = new GameObject[4];
    public GameObject[] hearts = new GameObject[6];
    public GameObject foliage1;
    public GameObject foliage2;
    public GameObject answer;
    public GameObject rocketBooster;
    public List<GameObject> lilyPadPool = new List<GameObject>();
    public GameObject lilyPad;
    public int numLilyPads = 5;
    public List<GameObject> healthCratePool = new List<GameObject>();
    public GameObject healthCrate;
    public int numHealthCrates = 2;
    public List<GameObject> boosterCratePool = new List<GameObject>();
    public GameObject boosterCrate;
    public int numRocketBoosters = 1;
    public List<GameObject> rockPool = new List<GameObject>();
    public GameObject rock;
    public int numRocks = 5;
    public List<GameObject> breadPool = new List<GameObject>();
    public GameObject bread;
    public int numBread = 5;
    public TMP_Text questionTMP;
    public TMP_Text scoreTMP;
    public int health = 6;
    public int score = 0;
    public string question = "5X5";
    public float moveSpeed = 5f;
    public float speed = 5f;
    public float timeBtwSpawn = 0f;
    public float startTimeBtwSpawn = 1f;

    // Start is called before the first frame update
    void Start() {
        rocketBooster.SetActive(false);
        for(int i = 0; i < 6; i++) {
            hearts[i].SetActive(false);
        }
        hearts[5].SetActive(true);
        for(int i = 0; i < numLilyPads; i++){
            GameObject obj = (GameObject)Instantiate(lilyPad);
            obj.SetActive(false);
            lilyPadPool.Add(obj);
        }
        for(int i = 0; i < numHealthCrates; i++){
            GameObject obj = (GameObject)Instantiate(healthCrate);
            obj.SetActive(false);
            healthCratePool.Add(obj);
        }
        for(int i = 0; i < numRocketBoosters; i++){
            GameObject obj = (GameObject)Instantiate(boosterCrate);
            obj.SetActive(false);
            boosterCratePool.Add(obj);
        }
        for(int i = 0; i < numRocks; i++){
            GameObject obj = (GameObject)Instantiate(rock);
            obj.SetActive(false);
            rockPool.Add(obj);
        }
        for(int i = 0; i < numBread; i++){
            GameObject obj = (GameObject)Instantiate(bread);
            obj.SetActive(false);
            breadPool.Add(obj);
        }
    }

    // Update is called once per frame
    void Update() {
        if(timeBtwSpawn <= 0){
            Items i = Random.Range(0,101) < 95 ? (Items)(Random.Range(0,3)) : (Items)Random.Range(3,5);
            SpawnLane(i,Random.Range(0,4));
            timeBtwSpawn = startTimeBtwSpawn;
        }else{
            timeBtwSpawn -= Time.deltaTime;
        }
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
        if(Input.GetKeyDown("1")){
            SpawnLane(Items.LilyPad, 0);
        }
        if(Input.GetKeyDown("2")){
            SpawnLane(Items.HealthCrate, 1);
        }
        if(Input.GetKeyDown("3")){
            SpawnLane(Items.BoosterCrate, 2);
        }
        if(Input.GetKeyDown("4")){
            SpawnLane(Items.Rock, 3);
        }
        if(duck.transform.position.y != duckLanes[currentLane].transform.position.y){
            duck.transform.position = Vector2.Lerp(duck.transform.position,duckLanes[currentLane].transform.position,Time.deltaTime*moveSpeed);
        }
    }
    public void SpawnLane(Items item, int l){
        if(item == Items.LilyPad){
            for(int i = 0; i < numLilyPads; i++){
                if(!lilyPadPool[i].activeInHierarchy){
                    GameObject obj = lilyPadPool[i];
                    obj.transform.position = lane[l].transform.position;
                    obj.SetActive(true);
                    break;
                }else{
                    GameObject obj = (GameObject)Instantiate(lilyPad);
                    obj.SetActive(false);
                    lilyPadPool.Add(obj);
                    numLilyPads++;
                }
            }
        }else if(item == Items.HealthCrate){
            for(int i = 0; i < numHealthCrates; i++){
                if(!healthCratePool[i].activeInHierarchy){
                    GameObject obj = healthCratePool[i];
                    obj.transform.position = lane[l].transform.position;
                    obj.SetActive(true);
                    break;
                }else{
                    GameObject obj = (GameObject)Instantiate(healthCrate);
                    obj.SetActive(false);
                    healthCratePool.Add(obj);
                    numHealthCrates++;
                }
            }
        }else if(item == Items.BoosterCrate){
            for(int i = 0; i < numRocketBoosters; i++){
                if(!boosterCratePool[i].activeInHierarchy){
                    GameObject obj = boosterCratePool[i];
                    obj.transform.position = lane[l].transform.position;
                    obj.SetActive(true);
                    break;
                }else{
                    GameObject obj = (GameObject)Instantiate(boosterCrate);
                    obj.SetActive(false);
                    boosterCratePool.Add(obj);
                    numRocketBoosters++;
                }
            }
        }else if(item == Items.Rock){
            for(int i = 0; i < numRocks; i++){
                if(!rockPool[i].activeInHierarchy){
                    GameObject obj = rockPool[i];
                    obj.transform.position = lane[l].transform.position;
                    obj.SetActive(true);
                    break;
                }else{
                    GameObject obj = (GameObject)Instantiate(rock);
                    obj.SetActive(false);
                    rockPool.Add(obj);
                    numRocks++;
                }
            }
        }else if(item == Items.Bread){
            for(int i = 0; i < numBread; i++){
                if(!breadPool[i].activeInHierarchy){
                    GameObject obj = breadPool[i];
                    obj.transform.position = lane[l].transform.position;
                    obj.SetActive(true);
                    break;
                }else{
                    GameObject obj = (GameObject)Instantiate(bread);
                    obj.SetActive(false);
                    breadPool.Add(obj);
                    numBread++;
                }
            }
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other) {

        // if Oli collides with obstacles (rock/lily pad), hide obstacle
        //  then decrease his health by 1 (half heart value)
        //  and call SetHeartView with new health
        if (other.CompareTag("Obstacle")) {
            other.gameObject.SetActive(false);
            health -= 1;
            if(health < 0){
                health = 0;
            }
            SetHeartView(health);
        }

        // if Oli collides with a health boost crate, hide crate
        //  then increase his health by 2 (full heart value)
        //  and call SetHeartView with new health
        if (other.CompareTag("HeartCrate")) {
            other.gameObject.SetActive(false);
            health += 2;
            if(health > 6){
                health = 6;
            }
            SetHeartView(health);
        }

        if (other.CompareTag("Money")) {
            other.gameObject.SetActive(false);
            score += 1000;
        }

        // if Oli collides with a jet pack crate, hide crate
        //  then start a 5 sec interval, set jet pack to active and increase
        //  speed. After 5 sec set jet pack inactive and reduce to normal speed
        if (other.CompareTag("BoosterCrate") && !rocketBooster.activeInHierarchy) {
            other.gameObject.SetActive(false);
            rocketBooster.SetActive(true);
            moveSpeed += 5f;
            speed += 5f;
            yield return new WaitForSeconds(5f);
            rocketBooster.SetActive(false);
            moveSpeed -= 5f;
            speed -= 5f;
        }
    }

    private void SetHeartView(int health) {
        if (health != 0) {
            for(int i = 0; i < 6; i++) {
                hearts[i].SetActive(false);
            }
            // health can never exceed 6
            health = health > 6 ? 6 : health;
            hearts[health - 1].SetActive(true);
        } else {
            Debug.Log("out of health, level ends");
        }
    }
}