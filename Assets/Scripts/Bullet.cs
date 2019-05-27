using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float speed = 30f;

	// Use this for initialization
	void Start () {
        // Destroy(gameObject);  // 즉시 제거
        Destroy(gameObject, 2);  // 2초 후에 제거
    }

    // Update is called once per frame
    void Update () {
        float amount = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * amount);
	}

    // 충돌의 판정과 처리
    void OnTriggerEnter (Collider other) {
        print(other.name + " " + other.tag);

        // Target
        if (other.tag == "Target") {
            // Destroy(other.gameObject);
            other.SendMessage("DestroySelf", transform.position);
        }

        // Enemy
        if (other.tag == "Enemy") {
            other.transform.root.SendMessage("DestroySelf", transform.position);
        }

        Destroy(gameObject);
    }
}
