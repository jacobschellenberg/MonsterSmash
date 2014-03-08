using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public string buttonName;
	public Texture2D idle;
	public GameObject pressed;
	
	void Start(){
		pressed.SetActive (false);
	}
	
	void Update(){
		ButtonState ();
	}
	
	public void ButtonState(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Input.GetButtonDown("Fire1")){
	    	if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
				if(hit.collider.CompareTag("Play")){
					pressed.SetActive(true);
					Application.LoadLevel("NewPlayer");
				}
				if (hit.collider.CompareTag ("Reset")) {
					PlayerPrefs.SetInt ("HighScore", 0);
										Debug.Log ("High Score Reset");
				}
			}
		}
	}
}