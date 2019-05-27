using UnityEngine;
using System.Collections;

// ParticleSystem Component�� ������ �ڵ����� �߰��϶�
[RequireComponent(typeof(ParticleSystem))]

public class CFX_AutoDestructShuriken : MonoBehaviour {

    public bool OnlyDeactivate;  // true�̸� �������� �ʰ� ��Ȱ��ȭ
	
	void OnEnable() {
		StartCoroutine("CheckIfAlive");
	}
	
	IEnumerator CheckIfAlive () {
		while (true) {  // ���ѷ����� ����
			yield return new WaitForSeconds(0.5f);  // 0.5�� ���

            // ��ƼŬ�� ������ �����°�?
			if (!GetComponent<ParticleSystem>().IsAlive(true)) {
				if (OnlyDeactivate) {  // ��Ȱ��ȭ���Ѿ� �ϴ°�?
                    // ���Ǻ� ������
					#if UNITY_3_5      // ����Ƽ 3.5�� ������ΰ�?
                        // UNITY 3.5�� ��Ȱ��ȭ ����
						this.gameObject.SetActiveRecursively(false);
					#else              // ����Ƽ 3.5�� �ƴ�
                        // �� ���� ������ ��Ȱ��ȭ ����
						this.gameObject.SetActive(false);
					#endif
				}
				else    // ��Ȱ��ȭ�� �ƴϸ� ��ƼŬ ����
					GameObject.Destroy(this.gameObject);
				yield break;  // ��ƼŬ�� ������ �������Ƿ� ����
			}
		}
	}
}
