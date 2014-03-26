using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameViewController : MonoBehaviour {

	public List<GameObject> lifePoints = new List<GameObject>();
	public ScoreController scoreController;
	public UILabel monstersSmashedText;
	public UILabel highScoreText;
	public UILabel gameOverLabel;
	public GameObject hitFlash;
	public GameObject absorbedPrefab;
	public float hitFlashTimer = 0.1F;
	public float abductDisplayTime = 1;

	private float timeSinceHit;
	private bool isHit = false;

	void Start(){
		gameOverLabel.gameObject.SetActive(false);
		highScoreText.text = string.Format("High Score: {0:00000}", scoreController.HighScore);
	}

	void Update(){
		if(scoreController.TotalPoints > scoreController.HighScore){
			highScoreText.text = string.Format("High Score: {0:00000}", scoreController.TotalPoints);
		}
		monstersSmashedText.text = string.Format("Current Score: {0:00000}", scoreController.TotalPoints);

		if(isHit){
			timeSinceHit += Time.deltaTime;
			hitFlash.SetActive(true);
			if(timeSinceHit > hitFlashTimer){
				hitFlash.SetActive(false);
				timeSinceHit = 0;
				isHit = false;
			}
		}
	}

	public void UpdateLifePointsDisplay(int currentLifePoints){
		if(currentLifePoints >= 0)
			lifePoints[currentLifePoints].GetComponent<UISprite>().spriteName = TextureManager.GetLifePointMissingSprite();

		if(currentLifePoints == 0)
			gameOverLabel.gameObject.SetActive(true);

		isHit = true;
	}

	public void ShowAbductEffect(Vector3 position){
		GameObject prefab = NGUITools.AddChild(this.gameObject, absorbedPrefab);
		prefab.transform.localPosition = position;
		Destroy(prefab, abductDisplayTime);
	}
}
