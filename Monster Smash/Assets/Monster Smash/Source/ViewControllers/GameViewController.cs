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
	public List<GameObject> endExplosions;
	public float hitFlashTimer = 0.1F;

	private int health;
	private bool isHit = false;
	private float timer = 0;

	void Start(){
		highScoreText.text = string.Format("High Score: {0}", scoreController.HighScore);

		health = LifePoints.Count;
		foreach(GameObject explosion in endExplosions)
			explosion.SetActive(false);
	}

	void Update(){
		if(scoreController.MonstersSmashed > scoreController.HighScore){
			highScoreText.text = string.Format("High Score: {0}", scoreController.MonstersSmashed);
		}

		monstersSmashedText.text = string.Format("Points: {0}", scoreController.MonstersSmashed);


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
			endExplosions[Random.Range(0,endExplosions.Count)].SetActive(true);
			
			health--;

			if(health >= 0)
				LifePoints[health].GetComponent<UITexture>().mainTexture = TextureManager.GetLifePointMissingTexture();
			
			isHit = true;
			Destroy (source);
			
			if(health < 1){
				gameController.GetComponent<GameController>().GameOver();	
			}
		}
	}
}
