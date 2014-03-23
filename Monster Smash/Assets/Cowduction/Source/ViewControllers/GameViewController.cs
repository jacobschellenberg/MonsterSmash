using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameViewController : MonoBehaviour {

	public GameObject gameController;
	public ScoreController scoreController;
	public UILabel monstersSmashedText;
	public UILabel highScoreText;

	public List<GameObject> LifePoints = new List<GameObject>();
	public GameObject hitFlash;
	public float hitFlashTimer = 0.1F;

	private int health;
	private bool isHit = false;
	private float timer = 0;

	void Start(){
		highScoreText.text = string.Format("High Score: {0:00000}", scoreController.HighScore);

		health = LifePoints.Count;
	}

	void Update(){
		if(scoreController.TotalPoints > scoreController.HighScore){
			highScoreText.text = string.Format("High Score: {0:00000}", scoreController.TotalPoints);
		}

		monstersSmashedText.text = string.Format("Current Score: {0:00000}", scoreController.TotalPoints);


		if(isHit){
			timer += Time.deltaTime;
			hitFlash.SetActive(true);
			if(timer > hitFlashTimer){
				hitFlash.SetActive(false);
				timer = 0;
				isHit = false;
			}
		}
	}

	public void OnHit(GameObject source){
		if(source.CompareTag("Monster")){
			health--;

			if(health >= 0)
				LifePoints[health].GetComponent<UISprite>().spriteName = TextureManager.GetLifePointMissingSprite();
			
			isHit = true;
			Destroy (source);
			
			if(health < 1){
				gameController.GetComponent<GameController>().GameOver();	
			}
		}
	}
}
