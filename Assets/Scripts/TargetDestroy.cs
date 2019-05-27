using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroy : MonoBehaviour {

    public Transform explosion;   // 폭파 파티클

    // 오브젝트 제거 - 외부 호출
    void DestroySelf (Vector3 pos) {
        Instantiate(explosion, transform.position, Quaternion.identity);
        // Destroy(gameObject);
        StartCoroutine(DestroyLazy());
    }

    // 투명하게 사라지기
    IEnumerator DestroyLazy () {
        // 오브젝트의 머티리얼 읽기
        Material mat = GetComponent<Renderer>().material;
        Color color = mat.color;

        // 투명도 설정
        for (float alpha = 1; alpha >= 0; alpha -= 0.02f) {
            color.a = alpha;
            mat.color = color;

            yield return null;
        }

        Destroy(gameObject);
    }
}
