using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public EveryPlayController everyPlayController;
	public ScoreController scoreController;
	public GameObject monsterPrefab;
	public GameObject spawnPoint;
	public GameObject powPrefab;
	public UILabel gameOverLabel;
	public float maxPosX = 735;
	public float maxPosY = 735;
	public int TapDamage = 1;
	public float monsterSpawnTimer = 1.5f;

	private float timer;
	private List<GameObject> monsterList = new List<GameObject>();
	private Vector3 currentMouseClickPosition;
	private bool gameOver;

	void Start(){
		gameOverLabel.gameObject.SetActive(false);
	}

	void Update(){
		if(!gameOver){
			timer += 1 * Time.deltaTime;
			if(timer > monsterSpawnTimer){
				SpawnMonster();

				timer = 0;
			}
		}
	}
	
	void SpawnMonster(){
		float xOffset = Random.Range(-maxPosX, maxPosX);
		float yOffset = Random.Range(-maxPosY, maxPosY);

		GameObject newMonster = NGUITools.AddChild(this.gameObject, monsterPrefab);
		newMonster.transform.localPosition = new Vector3(spawnPoint.transform.localPosition.x + xOffset, spawnPoint.transform.localPosition.y + yOffset,0);
		monsterList.Add(newMonster);
	}
	
	public void ShowHitPow(Vector3 position){
		GameObject newPow = NGUITools.AddChild(this.gameObject, powPrefab);
		newPow.transform.localPosition = position;
		Destroy(newPow, 1);
	}
	
	public void GameOver(){
		gameOver = true;

		this.scoreController.UpdateScore();

		StartCoroutine(DelayedStop());
	}

	IEnumerator DelayedStop(){
		gameOverLabel.gameObject.SetActive(true);

		yield return new WaitForSeconds(3);

		if(everyPlayController.IsRecordingSupported)
			everyPlayController.StopRecording();
		else
			LoadEndScene();
	}

	public void LoadEndScene(){
		Application.LoadLevel("GameOver");
	}
}
