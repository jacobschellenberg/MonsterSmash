using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public TextMesh HighScore;
	public TextMesh CurrentScore;
	public TextMesh NewHighScore;
	public GameObject Replay;
	public GameObject ReplayClicked;

	float x;
	float y;
	
	
	void Start(){
		HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
		CurrentScore.text = PlayerPrefs.GetInt("CurrentScore").ToString();
		if(PlayerPrefs.GetInt("CurrentScore") >= PlayerPrefs.GetInt("HighScore")){
			NewHighScore.renderer.enabled = true;
		}
	}
	
	void Update(){
		MouseToScreenDetection();
	}
	
	public void MouseToScreenDetection(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Input.GetButtonDown("Fire1")){
	    	if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
				if(hit.collider.CompareTag("Replay")){
					ReplayClicked.renderer.enabled = true;
					PlayerPrefs.SetInt("CurrentScore", 0);
					Application.LoadLevel("Game");
				}
			}
		}
	}
}
