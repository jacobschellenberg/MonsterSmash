using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: Split class into GameController and GameViewController
public class GameViewController : MonoBehaviour {

	public GameController gameController;
	public UILabel monstersSmashedText;
	public UILabel highScoreText;
	
	void Start(){
		highScoreText.text = string.Format("High Score: {0}", gameController.HighScore);
	}

	void Update(){
		if(gameController.MonstersSmashed > gameController.HighScore){
			highScoreText.text = string.Format("High Score: {0}", gameController.MonstersSmashed);
		}

		monstersSmashedText.text = string.Format("Points: {0}", gameController.MonstersSmashed);
	}
}
