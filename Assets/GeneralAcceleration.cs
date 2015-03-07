using UnityEngine;
using System.Collections;

public class GeneralAcceleration : MonoBehaviour {
	public enum PaperModes {Ball, PaperPlane};
	public PaperModes PaperMode;
	GameObject LSys;
	LevelSystem LS;
	Vector3 VectorAcceleration = Vector3.up * .0001f;
	float MaxVelocity;
	float DropVelocity;
	float Inercia = 1;
	Vector3 DropDirection = Vector3.down;
	public float TIME = 1;

	void Start () {
		LSys = GameObject.FindGameObjectWithTag("LevelSystem");
		LS = LSys.GetComponent<LevelSystem> ();
	}

	public void Apply (Vector3 direction, float module) {
		//module is proporcional to distance
		if (module != 0)
			VectorAcceleration += direction * module;
	}
	
	void FixedUpdate () {
		if (LS.State == LevelSystem.States.Going)
		{
			if (PaperMode == PaperModes.Ball){
				//Values
				MaxVelocity = .15f;
				DropVelocity = 0.03f;
				//Natural physics
				VectorAcceleration += DropDirection * DropVelocity;
				Vector3 NV = new Vector3 (0, VectorAcceleration.y * 0.5f, 0);
				VectorAcceleration = Vector3.Slerp(VectorAcceleration, NV, 0.02f);
			}
			if (PaperMode == PaperModes.PaperPlane){
				//Values
				MaxVelocity = .1f;
				DropVelocity = 0.03f;
				float ang = Vector3.Angle(Vector3.right, transform.TransformDirection(Vector3.right));
				float sin = Mathf.Sin(ang * Mathf.Deg2Rad) * transform.TransformDirection(Vector3.right).y 
					/ Mathf.Abs(transform.TransformDirection(Vector3.right).y);
				if (ang != 0){
					if (sin < 0){
						Inercia -= sin * 0.2f;
						Inercia = Mathf.Clamp(Inercia,0,1);
					}
					if (sin > 0){
						Inercia = Mathf.Lerp(Inercia, 0, 0.0015f);
					}
				}
				//Natural physics
				VectorAcceleration = Vector3.Slerp (VectorAcceleration, Vector3.zero, 0.02f);
				transform.position += DropDirection * DropVelocity * TIME;
				transform.position += transform.TransformDirection(Vector3.right) * 0.08f * TIME;
				VectorAcceleration += transform.TransformDirection(Vector3.left) 
					* Vector3.Cross(Vector3.right, transform.TransformDirection(Vector3.right)).z * 0.1f * (1 - Inercia);
				//Natural motion
				if (Vector3.Angle(Vector3.right, transform.TransformDirection(Vector3.right)) > 90){
					transform.GetChild(0).transform.localEulerAngles = new Vector3(180,0,0);
				}
				else{
					transform.GetChild(0).transform.localEulerAngles = new Vector3(0,0,0);
				}
			}
			
			//Vector to MAX(1,1,1)
			Vector3 v = VectorAcceleration;
			VectorAcceleration = new Vector3 (Mathf.Clamp (v.x, -1, 1), Mathf.Clamp (v.y, -1, 1), Mathf.Clamp (v.z, -1, 1));
			//Vector is unitary, so 1 means MaxVelocity
			transform.position += VectorAcceleration * MaxVelocity * TIME;
		}
	}
}
