using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public EveryPlayController everyPlayController;
	public ScoreController scoreController;
	public SpawnController spawnController;
	public GameViewController gameViewController;

	public int lifePoints = 3;
	public int TapDamage = 1;
	public float monsterSpawnTimer = 1.5f;
	public float gameOverDelay = 3;

	private float timer;
	private bool gameOver;

	private static GameController instance;
	public static GameController Instance{
		get{
			if(instance == null)
				instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

			return instance;
		}
	}

	void Update(){
		if(!gameOver){
			timer += 1 * Time.deltaTime;
			if(timer > monsterSpawnTimer){
				spawnController.SpawnActor();
				timer = 0;
			}
		}
	}
	
	public void GameOver(){
		gameOver = true;
		scoreController.UpdateScore();
		StartCoroutine(_DelayedStopRecording());
	}

	IEnumerator _DelayedStopRecording(){
		yield return new WaitForSeconds(gameOverDelay);

		if(everyPlayController.IsRecordingSupported)
			everyPlayController.StopRecording();
		else
			LoadEndScene();
	}

	public void LoadEndScene(){
		Application.LoadLevel("GameOver");
	}

	public void OnActorHitObjective(GameObject source){
		if(source.CompareTag("Actor")){
			lifePoints--;
			gameViewController.UpdateLifePointsDisplay(lifePoints);
			if(lifePoints < 1)
				GameOver();	
			Destroy (source);
		}
	}

	public void OnActorAbducted(){
		this.scoreController.TotalPoints++;
	}

	public float OnActorHit(Actor actor){
		gameViewController.ShowAbductEffect(actor.transform.localPosition);
		return TapDamage;
	}
}
