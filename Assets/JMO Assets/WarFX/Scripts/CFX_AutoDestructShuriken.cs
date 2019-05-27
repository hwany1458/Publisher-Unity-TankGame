using UnityEngine;
using System.Collections;

// ParticleSystem Component가 없으면 자동으로 추가하라
[RequireComponent(typeof(ParticleSystem))]

public class CFX_AutoDestructShuriken : MonoBehaviour {

    public bool OnlyDeactivate;  // true이면 제거하지 않고 비활성화
	
	void OnEnable() {
		StartCoroutine("CheckIfAlive");
	}
	
	IEnumerator CheckIfAlive () {
		while (true) {  // 무한루프의 시작
			yield return new WaitForSeconds(0.5f);  // 0.5초 대기

            // 파티클의 수명이 끝났는가?
			if (!GetComponent<ParticleSystem>().IsAlive(true)) {
				if (OnlyDeactivate) {  // 비활성화시켜야 하는가?
                    // 조건부 컴파일
					#if UNITY_3_5      // 유니티 3.5를 사용중인가?
                        // UNITY 3.5의 비활성화 형식
						this.gameObject.SetActiveRecursively(false);
					#else              // 유니티 3.5가 아님
                        // 그 이후 버전의 비활성화 형식
						this.gameObject.SetActive(false);
					#endif
				}
				else    // 비활성화가 아니면 파티클 삭제
					GameObject.Destroy(this.gameObject);
				yield break;  // 파티클의 수명이 끝났으므로 종료
			}
		}
	}
}
