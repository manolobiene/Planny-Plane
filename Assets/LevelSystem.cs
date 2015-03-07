using UnityEngine;
using System.Collections;

public class LevelSystem : MonoBehaviour {
	public enum States {Start, Going, Pause, Resume, Dead};
	public States State;
	int globalCounter;
	GameObject Plane;
	GeneralAcceleration GA;

	void Start () {
		Plane = GameObject.FindGameObjectWithTag ("Player");
		GA = Plane.GetComponent<GeneralAcceleration> ();
	}

	void OnGUI () {
		switch (State) 
		{
		case States.Start:
			GA.TIME = 0;
			if (GUILayout.Button("GO!"))
				State = States.Going;
			break;
		case States.Going:
			GA.TIME = Mathf.Lerp(GA.TIME, 1, 0.01f);
			if (GUILayout.Button("Pause"))
				State = States.Pause;
			//extra
			if (Plane.GetComponent<GeneralAcceleration>().PaperMode == GeneralAcceleration.PaperModes.PaperPlane){
				if (Plane.GetComponent<Rigidbody2D>().gravityScale > 0){
					Plane.GetComponent<Rigidbody2D>().gravityScale = 0;
				}
			}
			else{
				if (Plane.GetComponent<Rigidbody2D>().gravityScale < 0f){
					Plane.GetComponent<Rigidbody2D>().gravityScale = 0f;
				}
			}
			break;
		case States.Pause:
			GA.TIME = 0;
			if (GUILayout.Button("Resume")){
				State = States.Resume;
				StartCoroutine("Resume");
			}
			break;
		case States.Resume:
			GUILayout.Label(globalCounter.ToString());
			break;
		case States.Dead:
			GUILayout.Label("You lose");
			if (GUILayout.Button("Retry?"))
				Application.LoadLevel(Application.loadedLevelName);
			//extra
			if (Plane.GetComponent<Rigidbody2D>().gravityScale == 0){
				Plane.GetComponent<Rigidbody2D>().gravityScale = 1;
			}
			if (Plane.GetComponent<Collider2D>().isTrigger == true){
				Plane.GetComponent<Collider2D>().isTrigger = false;
			}
			break;
		}
	}

	IEnumerator Resume () {
		globalCounter = 3;
		yield return new WaitForSeconds(1);
		globalCounter = 2;
		yield return new WaitForSeconds(1);
		globalCounter = 1;
		yield return new WaitForSeconds(1);
		globalCounter = 0;
		yield return new WaitForSeconds(1);
		State = States.Going;
		yield break;
	}
}
