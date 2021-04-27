using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollingScript : MonoBehaviour {
    public GameObject duck;
    public float speed = 5f;
    public Transform spawner;
    // Start is called before the first frame update
    void Start() {
        duck = GameObject.FindGameObjectsWithTag("Duck")[0];
        speed = duck.GetComponent<DuckLevelScript>().speed;
    }

    // Update is called once per frame
    void Update() {
        speed = duck.GetComponent<DuckLevelScript>().speed;
        transform.Translate(Vector2.left*speed*Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("BackWall")){
            gameObject.transform.position = new Vector3(spawner.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
        }
    }
}
