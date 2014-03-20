using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public EveryPlayController everyPlayController;
	public ScoreController scoreController;
	public GameObject monsterPrefab;
	public GameObject spawnPoint;
	public GameObject powPrefab;
	public float maxPosX = 735;
	public float maxPosY = 735;
	public int TapDamage = 1;
	public float monsterSpawnTimer = 1.5f;
	public GameObject castle;

	private float timer;
	private int previousHit = -1;
	private int bonusValue;
	private List<GameObject> monsterList = new List<GameObject>();
	private Vector3 currentMouseClickPosition;
	private bool gameOver;

	void Update(){
		if(!gameOver){
			MouseToScreenDetection();

			timer += 1 * Time.deltaTime;
			if(timer > monsterSpawnTimer){
				SpawnMonster();

				timer = 0;
			}
		}
	}
	
	void MouseToScreenDetection(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
	
		if(Input.GetButtonDown("Fire1")){
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
				currentMouseClickPosition = hit.transform.localPosition;

				// TODO: is currently giving bonus if hit same monster.
				// Should be giving bonus if same monster type is hit.
				if(hit.transform.GetComponent<Monster>() != null &&  previousHit == hit.transform.GetComponent<Monster>().MonsterType){
					bonusValue++;
					ShowBonusPow(currentMouseClickPosition);
				}
				else
					bonusValue = 1;
				
				this.scoreController.MonstersSmashed += bonusValue;
				previousHit = hit.transform.GetComponent<Monster>().MonsterType;

				if(hit.collider.CompareTag("Monster"))
					ShowHitPow(currentMouseClickPosition);
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
	
	void ShowFriendlyHitPow(Vector3 position){
		GameObject newPow = (GameObject)Instantiate(powPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
		newPow.GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.FriendlyHitPows);
		Destroy(newPow, 0.2F);
	}
	
	void ShowHitPow(Vector3 position){
		GameObject newPow = NGUITools.AddChild(this.gameObject, powPrefab);
		newPow.transform.localPosition = position;
		newPow.GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.HitPows);
		Destroy(newPow, 0.2F);
	}
	
	void ShowBonusPow(Vector3 position){
		float[] random = {100.0f, -100.0f};
		int selectRandom = Random.Range(0, random.Length);
		
		GameObject newPow = NGUITools.AddChild(this.gameObject, powPrefab);
		newPow.transform.localPosition = new Vector3(position.x + random[selectRandom], position.y + random[selectRandom], 0);
		newPow.GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.BonusPows);
		Destroy(newPow, 0.3F);
	}
	
	public void GameOver(){
		gameOver = true;

		this.scoreController.UpdateScore();

		if(everyPlayController.IsRecordingSupported)
			everyPlayController.StartRecording();
		else
			LoadEndScene();
	}

	public void LoadEndScene(){
		Application.LoadLevel("GameOver");
	}
}
