using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	void Start(){
		renderer.enabled = false;
	}

	void Update(){
		ButtonState ();
	}

	public void ButtonState(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Input.GetButtonDown("Fire1")){
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
				if(hit.collider.CompareTag("ContinueButton")){
					renderer.enabled = true;
					Application.LoadLevel("Game");
				}
			}
		}
	}
}
