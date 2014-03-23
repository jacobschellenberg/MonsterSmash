using UnityEngine;
using System.Collections;

public class GameOverViewController : MonoBehaviour {

	[SerializeField]
	private UILabel highScore;
	[SerializeField]
	private UILabel currentScore;
	[SerializeField]
	private UILabel newHighScore;

	void Start(){
		highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
		currentScore.text = PlayerPrefs.GetInt("CurrentScore").ToString();
		newHighScore.gameObject.SetActive(false);
		if(PlayerPrefs.GetInt("CurrentScore") >= PlayerPrefs.GetInt("HighScore"))
			newHighScore.gameObject.SetActive(true);
	}

	public void OnReplayButtonClicked(){
		PlayerPrefs.SetInt("CurrentScore", 0);
		Application.LoadLevelAsync("Game");
	}
}
