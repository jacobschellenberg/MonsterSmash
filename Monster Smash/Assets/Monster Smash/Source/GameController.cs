using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject monsterPrefab;
	public GameObject spawnPoint;
	public GameObject agentPrefab;
	public GameObject powPrefab;
	public GameObject freezeAllPrefab;
	public GameObject destroyAllPrefab;
	public float maxPosX;
	public float maxPosY;
	public int TapDamage = 1;
	public int destroyAllBonus = 100;
	public float monsterSpawnTimer = 3f;
	public GameObject castle;

	public int HighScore{
		get {return PlayerPrefs.GetInt("HighScore");}
		private set{}
	}
	public int MonstersSmashed{get; set;}
	
	private float timer;
	private int previousSmash;
	private int bonusValue;
	private List<GameObject> monsterList = new List<GameObject>();
	private Vector3 currentMouseClickPosition;

	void Update(){
		MouseToScreenDetection();
		GameActive();
	}
	
	public void MouseToScreenDetection(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Input.GetButtonDown("Fire1")){
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
				currentMouseClickPosition = hit.transform.localPosition;
				
				if(hit.collider.CompareTag("Monster")){
					ShowHitPow(currentMouseClickPosition);
					DestroyMonster(hit.collider.gameObject);
				}
				else if(hit.collider.CompareTag("DestroyAll")){
					MonstersSmashed += destroyAllBonus;
					monsterList.Remove(hit.collider.gameObject);
				}		
				else if(hit.collider.CompareTag("Civilian")){
					CivilianHit();
					Destroy (hit.collider.gameObject);
					ShowFriendlyHitPow(currentMouseClickPosition);
				}
			}
		}
	}
	
	//Deactivate and Requeue Monster
	void DestroyMonster(GameObject monster){
		if(!monster.GetComponent<Monster>().isAlive){
			monster.GetComponent<Monster>().Hit(TapDamage);
			
			if(monster.GetComponent<Monster>().MonsterType == previousSmash){
				bonusValue++;
				ShowBonusPow(currentMouseClickPosition);
			}
			else{
				bonusValue = 1;
			}
			
			MonstersSmashed += bonusValue;
			
			previousSmash = monster.GetComponent<Monster>().MonsterType;
		}
	}
	
	void GameActive(){
		timer += 1 * Time.deltaTime;
		
		if(timer > monsterSpawnTimer){
			var newMonster = NGUITools.AddChild(this.gameObject, monsterPrefab);
			newMonster.transform.localPosition = new Vector3(spawnPoint.transform.localPosition.x + Random.Range(-maxPosX,maxPosX), spawnPoint.transform.localPosition.y + Random.Range(-maxPosY,maxPosY),0);
			monsterList.Add(newMonster);

//			var selectBuff = Random.Range (0,10);
			var selectBuff = 4;
			if(selectBuff < 1)
				NGUITools.AddChild(this.gameObject, destroyAllPrefab);
			else if(selectBuff < 5)
				NGUITools.AddChild(this.gameObject, freezeAllPrefab);
			else if(selectBuff < 10)
				GameObject.Instantiate(agentPrefab, new Vector3(spawnPoint.transform.localPosition.x + Random.Range(-maxPosX,maxPosX), spawnPoint.transform.localPosition.y + Random.Range(-maxPosY,maxPosY),0), Quaternion.identity);
			
			timer = 0;
		}
	}
	
	public void ShowFriendlyHitPow(Vector3 position){
		GameObject newPow = (GameObject)Instantiate(powPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
		newPow.GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.FriendlyHitPows);
		Destroy(newPow, 0.2F);
	}
	
	public void ShowHitPow(Vector3 position){
		GameObject newPow = NGUITools.AddChild(this.gameObject, powPrefab);
		newPow.transform.localPosition = position;
		newPow.GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.HitPows);
		Destroy(newPow, 0.2F);
	}
	
	public void ShowBonusPow(Vector3 position){
		float[] random = {100.0f, -100.0f};
		int selectRandom = Random.Range(0, random.Length);
		
		GameObject newPow = NGUITools.AddChild(this.gameObject, powPrefab);
		newPow.transform.localPosition = new Vector3(position.x + random[selectRandom], position.y + random[selectRandom], 0);
		newPow.GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.BonusPows);
		Destroy(newPow, 0.3F);
	}
	
	public void GameOver(){
		if(MonstersSmashed > PlayerPrefs.GetInt("HighScore")){
			PlayerPrefs.SetInt("HighScore", MonstersSmashed);
		}
		PlayerPrefs.SetInt("CurrentScore", MonstersSmashed);
		Application.LoadLevel("GameOver");
	}
	
	void CivilianHit(){
		MonstersSmashed--;
	}
}
