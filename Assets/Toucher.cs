using UnityEngine;
using System.Collections;

public class Toucher : MonoBehaviour {
	public float lastTouch;
	GameObject plane;

	void Start () {
		plane = GameObject.FindGameObjectWithTag("Player");
	}

	void OnMouseDown () {
		lastTouch = 0;
	}

	void FixedUpdate () {
		lastTouch += 1 * Time.deltaTime;
		//Debug.Log (lastTouch);
	}
}
