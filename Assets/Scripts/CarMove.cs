using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour {

    float moveSpeed = 10f;      // 이동 속도
    float rotSpeed = 60f;      // 회전 속도 (초속 60˚)

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // 현재 프레임에서 이동할 거리
        float amount = moveSpeed * Time.deltaTime;

        // 현재 프레임에서 회전할 각도
        float amountRot = rotSpeed * Time.deltaTime;

        // 전후진 키 (W or S)
        float vert = Input.GetAxis("Vertical");

        // 좌우 회전 키 (A or D)
        float horz = Input.GetAxis("Horizontal");

        // 전후진
        transform.Translate(Vector3.forward * amount * vert);

        // 좌우로 회전
        transform.Rotate(Vector3.up * amountRot * horz);
    }
}
