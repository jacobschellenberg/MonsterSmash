using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	
	public int HighScore{ get {return PlayerPrefs.GetInt("HighScore");} private set{} }
	public int TotalPoints{ get; set; }

	private const string highScoreKey = "HighScore";
	private const string currentScoreKey = "CurrentScore";

	void Start(){
		// Clear the current score.
		PlayerPrefs.DeleteKey(currentScoreKey);
	}

	public void UpdateScore(){
		if(TotalPoints > PlayerPrefs.GetInt(highScoreKey)){
			PlayerPrefs.SetInt(highScoreKey, TotalPoints);
		}
		PlayerPrefs.SetInt(currentScoreKey, TotalPoints);
	}

	public void ResetHighScore(){
		PlayerPrefs.DeleteKey(highScoreKey);
	}

}
