using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour {

	// OnEnable
	void OnEnable () {
        Invoke("Disable", 0.09f);
    }

    // Disable GameObject
    void Disable() {
        gameObject.SetActive(false);
    }
}
