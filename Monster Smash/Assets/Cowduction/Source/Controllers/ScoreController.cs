using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	
	public int HighScore{ get {return PlayerPrefs.GetInt("HighScore");} private set{} }
	public int TotalPoints{ get; set; }

	private int totalAbductedAllTime;

	private const string highScoreKey = "HighScore";
	private const string currentScoreKey = "CurrentScore";
	private const string totalAbductedAllTimeKey = "TotalAbductedAllTime";

	void Start(){
		// Clear the current score.
		PlayerPrefs.DeleteKey(currentScoreKey);
		totalAbductedAllTime = PlayerPrefs.GetInt(totalAbductedAllTimeKey);
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

	public void ActorAbducted(){
		TotalPoints++;
		PlayerPrefs.SetInt(totalAbductedAllTimeKey, ++totalAbductedAllTime);
	}
}
