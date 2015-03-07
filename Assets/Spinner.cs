using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {
	GameObject plane;
	GeneralAcceleration GA;
	bool spin = false;
	public bool antiClockWise = false;
	
	void Start () {
		plane = GameObject.FindGameObjectWithTag ("Player");
		GA = plane.GetComponent<GeneralAcceleration> ();
	}

	void FixedUpdate () {
		if (spin) {
			if (GA.PaperMode == GeneralAcceleration.PaperModes.PaperPlane){
				if (antiClockWise == false) {
					plane.transform.rotation *= Quaternion.Euler (new Vector3 (0, 0, 1) * 2 * GA.TIME);
				} else {
					plane.transform.rotation *= Quaternion.Euler (new Vector3 (0, 0, -1) * 2 * GA.TIME);
				}
			}
		}
	}

	void OnMouseEnter () {
		spin = true;
	}

	void OnMouseExit () {
		spin = false;
	}
}
