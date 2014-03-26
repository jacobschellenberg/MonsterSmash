using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	
	public int HighScore{ get {return PlayerPrefs.GetInt("HighScore");} private set{} }
	public int TotalPoints{ get; set; }

	private int totalAbductedAllTime;

	public static string highScoreKey = "HighScore";
	public static string currentScoreKey = "CurrentScore";
	public static string totalAbductedAllTimeKey = "TotalAbductedAllTime";

	void Start(){
		// Clear the current score for the new round
		PlayerPrefs.DeleteKey(currentScoreKey);
		totalAbductedAllTime = PlayerPrefs.GetInt(totalAbductedAllTimeKey);
	}

	public void UpdateScore(){
		if(TotalPoints > PlayerPrefs.GetInt(highScoreKey)){
			PlayerPrefs.SetInt(highScoreKey, TotalPoints);
		}
		PlayerPrefs.SetInt(currentScoreKey, TotalPoints);
	}

	public void ActorAbducted(){
		TotalPoints++;
		PlayerPrefs.SetInt(totalAbductedAllTimeKey, ++totalAbductedAllTime);
	}
}
