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

	void Start(){
		highScore.text = "High Score:\n" + PlayerPrefs.GetInt(ScoreController.highScoreKey).ToString();
		currentScore.text = "Score: " + PlayerPrefs.GetInt(ScoreController.currentScoreKey).ToString();
		totalAbductedAllTime.text = "Total Abducted All Time:\n" + PlayerPrefs.GetInt(ScoreController.totalAbductedAllTimeKey);

		newHighScore.gameObject.SetActive(false);
		if(PlayerPrefs.GetInt("CurrentScore") >= PlayerPrefs.GetInt(ScoreController.highScoreKey))
			newHighScore.gameObject.SetActive(true);
	}

	public void OnReplayButtonClicked(){
		PlayerPrefs.SetInt("CurrentScore", 0);
		Application.LoadLevelAsync("Game");
	}
}
