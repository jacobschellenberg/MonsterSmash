using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameViewController : MonoBehaviour {

	public ScoreController scoreController;
	public UILabel monstersSmashedText;
	public UILabel highScoreText;
	
	void Start(){
		highScoreText.text = string.Format("High Score: {0}", scoreController.HighScore);
	}

	void Update(){
		if(scoreController.MonstersSmashed > scoreController.HighScore){
			highScoreText.text = string.Format("High Score: {0}", scoreController.MonstersSmashed);
		}

		monstersSmashedText.text = string.Format("Points: {0}", scoreController.MonstersSmashed);
	}
}
