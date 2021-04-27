using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
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
    public GameObject[] problemAnswers = new GameObject[4];
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
    public TMP_Text questionShadowTMP;
    public TMP_Text scoreTMP;
    public TMP_Text scoreShadowTMP;
    public TMP_Text highscoreTMP;
    public TMP_Text highscoreShadowTMP;
    public int health = 6;
    public int score = 0;
    public int highScore = 0;
    public float moveSpeed = 5f;
    public float speed = 5f;
    public float timeBtwSpawn = 0f;
    public float startTimeBtwSpawn = 3f;
    public float timeBtwSpawnCoins = 0f;
    public float startTimeBtwSpawnCoins = 2f;
    public float timeBtwSpawnQuestion = 0f;
    public float startTimeBtwSpawnQuestion = 6f;
    public float questionWaitTime = 5f;
    public string questionString = "";
    public GameObject loseScreen;
    public bool paused = false;
    public Sound[] sounds;

    private Dictionary<string, string[]> multiplicationTable = new Dictionary<string,string[]>{
        {"1x0", new string[]{"0", "10", "1", "2"}},
        {"1x1", new string[]{"1", "11", "2", "0"}},
        {"1x2", new string[]{"2", "12", "1", "3"}},
        {"1x3", new string[]{"3", "13", "1", "4"}},
        {"1x4", new string[]{"4", "14", "1", "5"}},
        {"1x5", new string[]{"5", "15", "1", "6"}},
        {"1x6", new string[]{"6", "16", "1", "7"}},
        {"1x7", new string[]{"7", "17", "1", "8"}},
        {"1x8", new string[]{"8", "18", "1", "9"}},
        {"1x9", new string[]{"9", "19", "1", "10"}},
        {"1x10", new string[]{"10", "110", "1", "11"}},
        {"1x11", new string[]{"11", "111", "1", "12"}},
        {"1x12", new string[]{"12", "112", "1", "13"}},
        {"2x0", new string[]{"0", "2", "20", "1"}},
        {"2x1", new string[]{"2", "1", "0", "3"}},
        {"2x2", new string[]{"4", "2", "8", "22"}},
        {"2x3", new string[]{"6", "9", "23", "5"}},
        {"2x4", new string[]{"8", "4", "6", "12"}},
        {"2x5", new string[]{"10", "20", "8", "11"}},
        {"2x6", new string[]{"12", "18", "10", "13"}},
        {"2x7", new string[]{"14", "21", "7", "27"}},
        {"2x8", new string[]{"16", "10", "24", "18"}},
        {"2x9", new string[]{"18", "27", "9", "15"}},
        {"2x10", new string[]{"20", "12", "22", "18"}},
        {"2x11", new string[]{"22", "12", "14", "20"}},
        {"2x12", new string[]{"24", "22", "18", "42"}},
        {"3x0", new string[]{"0", "1", "3", "30"}},
        {"3x1", new string[]{"3", "1", "6", "4"}},
        {"3x2", new string[]{"6", "9", "3", "5"}},
        {"3x3", new string[]{"9", "6", "18", "3"}},
        {"3x4", new string[]{"12", "24", "11", "15"}},
        {"3x5", new string[]{"15", "20", "18", "13"}},
        {"3x6", new string[]{"18", "9", "21", "15"}},
        {"3x7", new string[]{"21", "24", "18", "17"}},
        {"3x8", new string[]{"24", "21", "27", "30"}},
        {"3x9", new string[]{"27", "18", "34", "33"}},
        {"3x10", new string[]{"30", "27", "33", "23"}},
        {"3x11", new string[]{"33", "34", "30", "36"}},
        {"3x12", new string[]{"36", "33", "30", "40"}},
        {"4x0", new string[]{"0", "1", "4", "5"}},
        {"4x1", new string[]{"4", "1", "5", "0"}},
        {"4x2", new string[]{"8", "16", "12", "6"}},
        {"4x3", new string[]{"12", "16", "12", "34"}},
        {"4x4", new string[]{"16", "12", "20", "24"}},
        {"4x5", new string[]{"20", "22", "24", "16"}},
        {"4x6", new string[]{"24", "28", "20", "32"}},
        {"4x7", new string[]{"28", "20", "24", "32"}},
        {"4x8", new string[]{"32", "36", "28", "40"}},
        {"4x9", new string[]{"36", "32", "40", "34"}},
        {"4x10", new string[]{"40", "36", "44", "48"}},
        {"4x11", new string[]{"44", "36", "40", "42"}},
        {"4x12", new string[]{"48", "52", "40", "44"}},
        {"5x0", new string[]{"0", "1", "5", "4"}},
        {"5x1", new string[]{"5", "0", "1", "4"}},
        {"5x2", new string[]{"10", "5", "15", "25"}},
        {"5x3", new string[]{"15", "20", "10", "35"}},
        {"5x4", new string[]{"20", "16", "25", "30"}},
        {"5x5", new string[]{"25", "30", "35", "40"}},
        {"5x6", new string[]{"30", "40", "20", "35"}},
        {"5x7", new string[]{"35", "40", "44", "32"}},
        {"5x8", new string[]{"40", "45", "50", "54"}},
        {"5x9", new string[]{"45", "40", "30", "35"}},
        {"5x10", new string[]{"50", "55", "60", "40"}},
        {"5x11", new string[]{"55", "50", "45", "65"}},
        {"5x12", new string[]{"60", "72", "65", "70"}},
        {"6x0", new string[]{"0", "1", "6", "3"}},
        {"6x1", new string[]{"6", "1", "12", "3"}},
        {"6x2", new string[]{"12", "24", "18", "6"}},
        {"6x3", new string[]{"18", "24", "23", "12"}},
        {"6x4", new string[]{"24", "30", "25", "36"}},
        {"6x5", new string[]{"30", "36", "24", "35"}},
        {"6x6", new string[]{"36", "30", "42", "24"}},
        {"6x7", new string[]{"42", "47", "48", "36"}},
        {"6x8", new string[]{"48", "54", "36", "42"}},
        {"6x9", new string[]{"54", "60", "47", "48"}},
        {"6x10", new string[]{"60", "52", "66", "54"}},
        {"6x11", new string[]{"66", "60", "72", "76"}},
        {"6x12", new string[]{"72", "76", "68", "60"}},
        {"7x0", new string[]{"0", "7", "1", "5"}},
        {"7x1", new string[]{"7", "1", "17", "6"}},
        {"7x2", new string[]{"14", "21", "17", "16"}},
        {"7x3", new string[]{"21", "27", "23", "28"}},
        {"7x4", new string[]{"28", "21", "22", "24"}},
        {"7x5", new string[]{"35", "42", "28", "24"}},
        {"7x6", new string[]{"42", "35", "40", "47"}},
        {"7x7", new string[]{"49", "42", "47", "49"}},
        {"7x8", new string[]{"56", "50", "49", "63"}},
        {"7x9", new string[]{"63", "56", "70", "77"}},
        {"7x10", new string[]{"70", "64", "63", "56"}},
        {"7x11", new string[]{"77", "70", "78", "84"}},
        {"7x12", new string[]{"84", "92", "80", "77"}},
        {"8x0", new string[]{"0", "1", "8", "6"}},
        {"8x1", new string[]{"8", "1", "7", "0"}},
        {"8x2", new string[]{"16", "24", "23", "18"}},
        {"8x3", new string[]{"24", "18", "32", "16"}},
        {"8x4", new string[]{"32", "40", "38", "24"}},
        {"8x5", new string[]{"40", "48", "44", "42"}},
        {"8x6", new string[]{"48", "56", "54", "44"}},
        {"8x7", new string[]{"56", "64", "60", "54"}},
        {"8x8", new string[]{"64", "56", "62", "52"}},
        {"8x9", new string[]{"72", "80", "64", "62"}},
        {"8x10", new string[]{"80", "88", "78", "70"}},
        {"8x11", new string[]{"88", "96", "80", "78"}},
        {"8x12", new string[]{"96", "88", "80", "98"}},
        {"9x0", new string[]{"0", "9", "1", "8"}},
        {"9x1", new string[]{"9", "19", "1", "10"}},
        {"9x2", new string[]{"18", "24", "20", "27"}},
        {"9x3", new string[]{"27", "34", "36", "32"}},
        {"9x4", new string[]{"36", "34", "44", "33"}},
        {"9x5", new string[]{"45", "50", "55", "40"}},
        {"9x6", new string[]{"54", "63", "45", "42"}},
        {"9x7", new string[]{"63", "64", "56", "72"}},
        {"9x8", new string[]{"72", "78", "68", "63"}},
        {"9x9", new string[]{"81", "90", "88", "83"}},
        {"9x10", new string[]{"90", "99", "81", "92"}},
        {"9x11", new string[]{"99", "90", "89", "72"}},
        {"9x12", new string[]{"108", "112", "98", "104"}},
        {"10x0", new string[]{"0", "10", "1", "9"}},
        {"10x1", new string[]{"10", "1", "0", "11"}},
        {"10x2", new string[]{"20", "22", "25", "30"}},
        {"10x3", new string[]{"30", "32", "31", "39"}},
        {"10x4", new string[]{"40", "38", "42", "44"}},
        {"10x5", new string[]{"50", "54", "51", "49"}},
        {"10x6", new string[]{"60", "66", "63", "59"}},
        {"10x7", new string[]{"70", "77", "74", "71"}},
        {"10x8", new string[]{"80", "89", "90", "81"}},
        {"10x9", new string[]{"90", "99", "91", "95"}},
        {"10x10", new string[]{"100", "110", "112", "101"}},
        {"10x11", new string[]{"110", "112", "121", "120"}},
        {"10x12", new string[]{"120", "110", "111", "121"}},
        {"11x0", new string[]{"0", "11", "1", "12"}},
        {"11x1", new string[]{"11", "12", "1", "13"}},
        {"11x2", new string[]{"22", "29", "21", "20"}},
        {"11x3", new string[]{"33", "30", "35", "31"}},
        {"11x4", new string[]{"44", "41", "40", "47"}},
        {"11x5", new string[]{"55", "51", "57", "59"}},
        {"11x6", new string[]{"66", "60", "61", "65"}},
        {"11x7", new string[]{"77", "70", "71", "79"}},
        {"11x8", new string[]{"88", "80", "81", "82"}},
        {"11x9", new string[]{"99", "91", "90", "93"}},
        {"11x10", new string[]{"110", "121", "111", "91"}},
        {"11x11", new string[]{"121", "131", "123", "111"}},
        {"11x12", new string[]{"132", "121", "131", "135"}},
        {"12x0", new string[]{"0", "12", "1", "11"}},
        {"12x1", new string[]{"12", "13", "1", "11"}},
        {"12x2", new string[]{"24", "22", "21", "28"}},
        {"12x3", new string[]{"36", "37", "24", "22"}},
        {"12x4", new string[]{"48", "36", "54", "28"}},
        {"12x5", new string[]{"60", "65", "57", "70"}},
        {"12x6", new string[]{"72", "74", "68", "81"}},
        {"12x7", new string[]{"84", "81", "78", "64"}},
        {"12x8", new string[]{"96", "78", "81", "108"}},
        {"12x9", new string[]{"108", "120", "104", "98"}},
        {"12x10", new string[]{"120", "121", "102", "114"}},
        {"12x11", new string[]{"132", "144", "128", "131"}},
        {"12x12", new string[]{"144", "132", "121", "156"}}
    };

    // Start is called before the first frame update
    void Start() {
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
        rocketBooster.SetActive(false);
        GetNewQuestion();
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
    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        if(s == null){
            return;
        }
        s.source.Play();
    }
    public IEnumerator SpawnObstacles(int i){
        if(i == 0 || i == 1){
            SpawnLane(Items.LilyPad, 0);
            SpawnLane(Items.LilyPad, 1);
            SpawnLane(Items.LilyPad, 2);
            yield return new WaitForSeconds(1.5f);
            SpawnLane(Items.LilyPad, 3);
            SpawnLane(Items.LilyPad, 2);
            SpawnLane(Items.LilyPad, 1);
        }else if(i == 2 || i == 3){
            SpawnLane(Items.Rock, 0);
            SpawnLane(Items.Rock, 3);
            yield return new WaitForSeconds(0.75f);
            SpawnLane(Items.Rock, 1);
            SpawnLane(Items.Rock, 2);
        }else if(i == 4 || i == 5){
            SpawnLane(Items.Rock, 0);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Rock, 1);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Rock, 2);
            yield return new WaitForSeconds(0.25f);
        }else if( i == 6 || i == 7){
            SpawnLane(Items.Rock, 3);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Rock, 2);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Rock, 1);
            yield return new WaitForSeconds(0.25f);
        }else if(i == 8 || i == 9){
            SpawnLane(Items.LilyPad, 0);
            SpawnLane(Items.LilyPad, 1);
            yield return new WaitForSeconds(0.75f);
            SpawnLane(Items.LilyPad, 2);
            SpawnLane(Items.LilyPad, 3);
            yield return new WaitForSeconds(0.75f);
            SpawnLane(Items.LilyPad, 0);
            SpawnLane(Items.LilyPad, 1);
        }else if(i == 10 || i == 11){
            SpawnLane(Items.LilyPad, 0);
            SpawnLane(Items.LilyPad, 1);
            SpawnLane(Items.LilyPad, 2);
        }else if(i == 12){
            SpawnLane(Items.HealthCrate,UnityEngine.Random.Range(0,4));
        }else if(i == 13){
            SpawnLane(Items.BoosterCrate,UnityEngine.Random.Range(0,4));
        }else if(i == 14){
            SpawnLane(Items.Bread, 0);
            SpawnLane(Items.Bread, 1);
            SpawnLane(Items.Bread, 2);
            SpawnLane(Items.Bread, 3);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Bread, 0);
            SpawnLane(Items.Bread, 1);
            SpawnLane(Items.Bread, 2);
            SpawnLane(Items.Bread, 3);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Bread, 0);
            SpawnLane(Items.Bread, 1);
            SpawnLane(Items.Bread, 2);
            SpawnLane(Items.Bread, 3);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Bread, 0);
            SpawnLane(Items.Bread, 1);
            SpawnLane(Items.Bread, 2);
            SpawnLane(Items.Bread, 3);
        }else if(i == 16){
            SpawnLane(Items.Bread, 3);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Bread, 2);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Bread, 1);
            yield return new WaitForSeconds(0.25f);
        }else if(i == 16){
            int x = UnityEngine.Random.Range(0,4);
            SpawnLane(Items.Bread, x);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Bread, x);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Bread, x);
            yield return new WaitForSeconds(0.25f);
            SpawnLane(Items.Bread, x);
        }
    }
    // Update is called once per frame
    void Update() {
        if(!paused){
            if(timeBtwSpawn <= 0){
                if(timeBtwSpawnQuestion <= 0f){
                    timeBtwSpawn += 2f;
                }else{
                    int i = (UnityEngine.Random.Range(0,17));
                    StartCoroutine(SpawnObstacles(i));
                    timeBtwSpawn = startTimeBtwSpawn;
                }
            }else{
                timeBtwSpawn -= Time.deltaTime;
            }
            if(timeBtwSpawnQuestion <= 0){
                ResetProblemAnswerStates();
                SetNewQuestion();
                timeBtwSpawnQuestion = startTimeBtwSpawnQuestion;
            }else{
                timeBtwSpawnQuestion -= Time.deltaTime;
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
            Play("RockHit");
            Play("DuckHit");
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
            Play("HeartBoxHit");
        }

        if (other.CompareTag("Money")) {
            other.gameObject.SetActive(false);
            scoreIncrease(100);
            Play("BreadHit");
        }

        // if Oli collides with a jet pack crate, hide crate
        //  then start a 5 sec interval, set jet pack to active and increase
        //  speed. After 5 sec set jet pack inactive and reduce to normal speed
        if (other.CompareTag("BoosterCrate") && !rocketBooster.activeInHierarchy) {
            other.gameObject.SetActive(false);
            rocketBooster.SetActive(true);
            moveSpeed += 5f;
            speed += 5f;
            Play("RocketBoxHit");
            Play("RocketActivate");
            yield return new WaitForSeconds(5f);
            rocketBooster.SetActive(false);
            moveSpeed -= 5f;
            speed -= 5f;
        }

        if (other.CompareTag("ProblemAnswer")) {
            // if hit correct answer
            if (other.gameObject.GetComponent<ProblemAnswerScript>().isCorrectAnswer) {
                // increase score from correct answer
                    if (rocketBooster.activeInHierarchy){
                        scoreIncrease(500);
                    } else {
                        scoreIncrease(300);
                    }
                    Play("RightAnswer");
            }else{
                health -= 1;
                if(health < 0){
                    health = 0;
                }
                SetHeartView(health);
                Play("WrongAnswer");
            }
            foreach (GameObject answer in problemAnswers) {
                if (answer.GetComponent<ProblemAnswerScript>().isCorrectAnswer) {
                    // selected correct answer
                    // remove the question from the dictionary because the user knows it, so it shouldn't repeat
                    // multiplicationTable.Remove(questionTMP.text);

                    // highlight the correct answer
                    answer.transform.GetChild(1).GetComponent<TMP_Text>().color = new Color32(50,168,82,255);
                } else{
                    // selected incorrect answer
                    // highlight incorrect answer with red
                    answer.transform.GetChild(1).GetComponent<TMP_Text>().color = new Color32(168,50,50,255);
                }
            }
            GetNewQuestion();
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
            hearts[0].SetActive(false);
            loseScreen.SetActive(true);
            paused = true;
            speed = 0f;
            Play("LostGame");
        }
    }
    public void PlayAgain(){
        loseScreen.SetActive(false);
        foreach(var x in rockPool){
            x.SetActive(false);
        }
        foreach(var x in breadPool){
            x.SetActive(false);
        }
        foreach(var x in lilyPadPool){
            x.SetActive(false);
        }
        foreach(var x in healthCratePool){
            x.SetActive(false);
        }
        foreach(var x in boosterCratePool){
            x.SetActive(false);
        }
        foreach(var x in problemAnswers){
            x.transform.position = new Vector3(-999,-999,-999);
        }
        if(score > highScore){
            highScore = score;
        }
        score = 0;
        scoreTMP.text = "Score: " + score;
        scoreShadowTMP.text = "Score: " + score;
        highscoreTMP.text = "Score: " + highScore;
        highscoreShadowTMP.text = "Score: " + highScore;
        speed = 5f;
        paused = false;
        health = 6;
        SetHeartView(health);
    }

    private void ResetProblemAnswerStates() {
        for (int i = 0; i < 4; i++) {
            problemAnswers[i].GetComponent<ProblemAnswerScript>().isCorrectAnswer = false;
            problemAnswers[i].transform.position = lane[i].transform.position;
        }
    }
    public void GetNewQuestion(){
        List<string> keyList = new List<string>(this.multiplicationTable.Keys);
        questionString = keyList[(int)UnityEngine.Random.Range(0, keyList.Count - 1)];
        questionTMP.text = questionString;
        questionShadowTMP.text = questionString;
    }

    private void SetNewQuestion() {
        /*if (multiplicationTable.Count == 0) {
            Debug.Log("stop here, game over. all questions have been answered correctly. congrats u win");
        }*/

        var possibleAnswers = multiplicationTable[questionString];
        var correctAnswer = possibleAnswers[0];

        // shuffle answers list
        for (int i = 0; i < possibleAnswers.Length - 1; i++) {
            int rnd = UnityEngine.Random.Range(i, possibleAnswers.Length);
            var temp = possibleAnswers[rnd];
            possibleAnswers[rnd] = possibleAnswers[i];
            possibleAnswers[i] = temp;
        }

        for (int i = 0; i < 4; i++) {
            problemAnswers[i].transform.GetChild(0).GetComponent<TMP_Text>().text = possibleAnswers[i];
            problemAnswers[i].transform.GetChild(1).GetComponent<TMP_Text>().text = possibleAnswers[i];
            problemAnswers[i].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color32(255,255,255,255);
            if (possibleAnswers[i] == correctAnswer) {
                problemAnswers[i].GetComponent<ProblemAnswerScript>().isCorrectAnswer = true;
            }
        }
    }

    private void scoreIncrease(int points){
        score += points;
        scoreTMP.text = "Score: " + score;
        scoreShadowTMP.text = "Score: " + score;
    }
}