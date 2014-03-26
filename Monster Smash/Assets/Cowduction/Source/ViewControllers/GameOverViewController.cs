using UnityEngine;
using System.Collections;

public class GameOverViewController : MonoBehaviour {

	[SerializeField]
	private UILabel highScore;
	[SerializeField]
	private UILabel currentScore;
	[SerializeField]
	private UILabel newHighScore;
	[SerializeField]
	private UILabel totalAbductedAllTime;

	private const string highScoreKey = "HighScore";
	private const string currentScoreKey = "CurrentScore";
	private const string totalAbductedAllTimeKey = "TotalAbductedAllTime";

	void Start(){
		highScore.text = "High Score:\n" + PlayerPrefs.GetInt(highScoreKey).ToString();
		currentScore.text = "Score: " + PlayerPrefs.GetInt(currentScoreKey).ToString();
		totalAbductedAllTime.text = "Total Abducted All Time:\n" + PlayerPrefs.GetInt(totalAbductedAllTimeKey);

		newHighScore.gameObject.SetActive(false);
		if(PlayerPrefs.GetInt("CurrentScore") >= PlayerPrefs.GetInt(highScoreKey))
			newHighScore.gameObject.SetActive(true);
	}

	public void OnReplayButtonClicked(){
		PlayerPrefs.SetInt("CurrentScore", 0);
		Application.LoadLevelAsync("Game");
	}
}
