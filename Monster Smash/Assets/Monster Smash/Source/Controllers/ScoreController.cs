using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	
	public int HighScore{ get {return PlayerPrefs.GetInt("HighScore");} private set{} }
	public int MonstersSmashed{get; set;}

	private const string highScoreKey = "HighScore";
	private const string currentScoreKey = "CurrentScore";

	void Start(){
		PlayerPrefs.DeleteKey(currentScoreKey);
	}

	public void UpdateScore(){
		if(MonstersSmashed > PlayerPrefs.GetInt(highScoreKey)){
			PlayerPrefs.SetInt(highScoreKey, MonstersSmashed);
		}
		PlayerPrefs.SetInt(currentScoreKey, MonstersSmashed);
	}

	public void ResetHighScore(){
		PlayerPrefs.DeleteKey(highScoreKey);
	}

}
