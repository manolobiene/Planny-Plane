using UnityEngine;
using System.Collections;

public class DieTrigger : MonoBehaviour {
	GameObject LSys;
	GeneralAcceleration GA;
	LevelSystem LS;

	void Start () {
		GA = GetComponent<GeneralAcceleration> ();
		LSys = GameObject.FindGameObjectWithTag("LevelSystem");
		LS = LSys.GetComponent<LevelSystem> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (GA.PaperMode == GeneralAcceleration.PaperModes.PaperPlane){
			if (other.tag == "Wall")
				LS.State = LevelSystem.States.Dead;
		}
	}
}
