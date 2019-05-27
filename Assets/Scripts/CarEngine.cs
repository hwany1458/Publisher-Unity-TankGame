using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    AudioSource sndEngine;  

	// Use this for initialization
	void Start () {
        sndEngine = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        // 이동 및 회전키의 입력상태 조사 (0~1.0)
        float vert = Mathf.Abs(Input.GetAxis("Vertical"));
        float horz = Mathf.Abs(Input.GetAxis("Horizontal"));

        // 큰 값을 pitch로 설정
        float pitch = Mathf.Max(vert, horz);

        sndEngine.pitch = pitch + 1;    // 1.0~2.0
        sndEngine.volume = sndEngine.pitch * 0.6f;  // 0.6~1.2
    }
}
