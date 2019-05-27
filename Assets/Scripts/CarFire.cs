using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarFire : MonoBehaviour {

    public Transform bullet;    // Bullet 프리팹
    public Transform explosion; // 폭파 불꽃

    Transform spPoint;          // SpawnPoint
    GameObject fire;             // 발사 불꽃

    AudioSource[] gunSound;     // AudioSourcce용 배열
    Rigidbody rgBody;           // Rigidbody

    float moveSpeed = 10f;      // 이동 속도
    float rotSpeed = 60f;       // 회전 속도 (초속 60˚)

    bool canFire = true;        // 총을 발사할 수 있는가?
    int hp = 20;
    // float delayTime = 0.1f;  // 발사 지연 시간

    // 게임 초기화
    void Start () {
        // 시작 시 SpownPoint와 AudioSource 읽기
        spPoint = GameObject.Find("SpawnPoint").transform;
        fire = GameObject.Find("FireEffect");
        fire.SetActive(false);

        gunSound = GetComponents<AudioSource>();
        rgBody = GetComponent<Rigidbody>();
    }

    // 게임 루프
    void Update() {
        // 현재 프레임에서 이동할 거리와 각도
        float amount = moveSpeed * Time.deltaTime;
        float amountRot = rotSpeed * Time.deltaTime;

        // 키 입력
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");

        // 이동과 회전
        transform.Translate(Vector3.forward * amount * vert);
        transform.Rotate(Vector3.up * amountRot * horz);

        // 단발 사격
        if (Input.GetButtonDown("Fire1")) {
            SingleShut();
        }

        // 연발 사격(Left Alt or Right Button)
        if (Input.GetButton("Fire2")) {
            // AutoFire();
        }

        // 연발 사격 Coroutine 호출
        // if (Input.GetButton("Fire2") && canFire) {
        if (Input.GetKey(KeyCode.LeftShift) && canFire) {
                StartCoroutine(AutoFire2());
        }

        // 버튼을 놓으면 사운드 중지
        // if (!Input.GetButton("Fire2")) {
        if (!Input.GetKey(KeyCode.LeftShift)) {
            gunSound[1].Stop();
        }
    }

    // Fixed Update
    void FixedUpdate () {
        // 키 입력
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");

        // 현재 프레임에서 이동할 거리와 각도
        float amount = moveSpeed * Time.deltaTime * vert;
        float amountRot = rotSpeed * Time.deltaTime * horz;

        // 이동과 회전
        rgBody.MovePosition(rgBody.position + transform.forward * amount);
        rgBody.MoveRotation(rgBody.rotation * Quaternion.Euler(Vector3.up * amountRot));
    }


    // 단발 사격
    void SingleShut () {
        Instantiate(bullet, spPoint.position, spPoint.rotation);
        gunSound[0].Play();
        fire.SetActive(true);
    }

    // 연발 사격
    //void AutoFire () {
    //    // 발사 지연 시간 계산
    //    delayTime += Time.deltaTime;

    //    if (delayTime >= 0.1f) {
    //        delayTime = 0;
    //        Instantiate(bullet, spPoint.position, spPoint.rotation);
    //    }
    //}

    // 연발 사격 Coroutine
    IEnumerator AutoFire2 () {
        Instantiate(bullet, spPoint.position, spPoint.rotation);
        gunSound[1].Play();
        fire.SetActive(true);
        canFire = false;

        yield return new WaitForSeconds(0.1f);
        canFire = true;
    }

    // 충돌 판정 및 처리
    void OnTriggerEnter (Collider other) {
        if (other.tag == "Bullet") {
            hp--;
            if (hp < 0) {
                StartCoroutine(DestroySelf());
            }
        }
    }

    // Reset Game
    IEnumerator DestroySelf () {
        Instantiate(explosion, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


