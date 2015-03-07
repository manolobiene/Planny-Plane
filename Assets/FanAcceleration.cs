using UnityEngine;
using System.Collections;

public class FanAcceleration : MonoBehaviour {
	public enum FanModes {Player1, Scenario1};
	public FanModes FanMode;
	GameObject plane;
	GeneralAcceleration GA;
	public float TargetIntensity = 0.01f;
	float Intensity = 0f;
	public float MaxRange = 2;

	void Start () {
		plane = GameObject.FindGameObjectWithTag ("Player");
		GA = plane.GetComponent<GeneralAcceleration> ();
	}

	void FixedUpdate () {
		Vector3 direction = transform.TransformDirection (Vector3.up);
		if (FanMode == FanModes.Player1) {
			Vector3 toPlane = plane.transform.position - transform.position;
			float ang = Vector3.Angle (direction, toPlane);
			if (ang < 90){ //evitar elevacion desde abajo
				if (toPlane.magnitude <= MaxRange){
					if (Mathf.Sin (ang * Mathf.Deg2Rad) * toPlane.magnitude <= transform.InverseTransformDirection(GetComponent<SpriteRenderer> ().bounds.size).x / 2){
						GA.Apply (direction, Intensity);
						Vector3 toCenter = new Vector3 (transform.InverseTransformDirection(transform.position).x - transform.InverseTransformDirection(plane.transform.position).x, 0, 0);
						GA.Apply (toCenter, toCenter.magnitude * Mathf.Clamp(Intensity, 0, 1) * Intensity);
					}
				}
			}
		}
		if (FanMode == FanModes.Scenario1) {
			float module = Mathf.Clamp (TargetIntensity * (Mathf.Round(MaxRange / (plane.transform.position - transform.position).magnitude)), 0, TargetIntensity);
			Vector3 toPlane = plane.transform.position - transform.position;
			float ang = Vector3.Angle (direction, toPlane);
			if (ang < 90){ //evitar elevacion desde abajo
				if (Mathf.Sin (ang * Mathf.Deg2Rad) * toPlane.magnitude <= transform.InverseTransformDirection(GetComponent<SpriteRenderer> ().bounds.size).x / 2){
					GA.Apply (direction, module);
					Vector3 toCenter = new Vector3 (transform.InverseTransformDirection(transform.position).x - transform.InverseTransformDirection(plane.transform.position).x, 0, 0);
					if (GA.PaperMode == GeneralAcceleration.PaperModes.Ball)
						GA.Apply (toCenter, toCenter.magnitude * TargetIntensity);
					if (GA.PaperMode == GeneralAcceleration.PaperModes.PaperPlane)
						GA.Apply (toCenter, toCenter.magnitude * TargetIntensity * 0.2f);
				}
			}
		}
	}

	void OnMouseOver () {
		Intensity = TargetIntensity;
	}

	void OnMouseExit () {
		Intensity = 0;
	}
}
