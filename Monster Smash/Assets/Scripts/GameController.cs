using UnityEngine;
using System.Linq.Expressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public delegate void GenerateMonster(string monsterName, int hitPoints, int monsterType, int monsterAI);
	
	public GameObject monsterPrefab;
	public GameObject spawnPoint;
	public GameObject civilianPrefab;
	public GameObject freezeAll;
	public GameObject destroyAll;
	public float maxPosX;
	public float maxPosY;
	public int TapDamage = 1;
	public int DestroyAllBonus = 100;
	public float monsterSpawnTimer = 3F;
	public int monstersSmashed = 0;
	public TextMesh monstersKilledText;
	public TextMesh highScoreText;
	
	public GameObject castle;
	
	public GameObject powPrefab;
	public List<Texture2D> powTextures = new List<Texture2D>();
	public List<Texture2D> hitPowTextures = new List<Texture2D>();
	public Texture2D bonusPow;
	public Texture2D friendlyFirePow;
	
	public List<Texture2D> AliveMonsterTextures = new List<Texture2D>();
	public List<Texture2D> DeadMonsterTextures = new List<Texture2D>();  
	
	private float timer;
	private int previousSmash;
	private int bonusValue;
	private List<GameObject> monsterList = new List<GameObject>();
	private Vector3 currentMouseClickPosition;
	
	void Start(){
		if(PlayerPrefs.GetInt("HighScore") != 0){
			highScoreText.text = string.Format("High Score: {0}", PlayerPrefs.GetInt("HighScore"));
		}
	}

	void Update(){
		MouseToScreenDetection();
		GameActive();
	}
	
	public void ScoreKeeper(){
		if(monstersSmashed > PlayerPrefs.GetInt("HighScore")){
			highScoreText.text = string.Format("High Score: {0}", monstersSmashed);
		}
	}
	
	public void MouseToScreenDetection(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Input.GetButtonDown("Fire1")){
	    	if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
				currentMouseClickPosition = ray.origin;
				
				if(hit.collider.CompareTag("Monster")){
					ChooseHitPowTexture(ray.origin);
					DestroyMonster(hit.collider.gameObject);
				}
				else if(hit.collider.CompareTag("FreezeAll")){
					FreezeAll();
					Destroy (hit.collider.gameObject);
				}
				else if(hit.collider.CompareTag("DestroyAll")){
					DestroyAll();
					Destroy (hit.collider.gameObject);
				}		
				else if(hit.collider.CompareTag("Civilian")){
					Civilian();
					Destroy (hit.collider.gameObject);
					ChooseFriendlyFirePowTexture(ray.origin);
				}
				else{
					ChoosePowTexture(currentMouseClickPosition);
				}
			}
		}
	}
	
	//Deactivate and Requeue Monster
	void DestroyMonster(GameObject monster){
		if(!monster.GetComponent<Monster>().IsDead){
			monster.GetComponent<Monster>().Hit(TapDamage);
		
			if(monster.GetComponent<Monster>().MonsterType == previousSmash){
				bonusValue++;
				BonusPow(currentMouseClickPosition);
			}
			else{
				bonusValue = 1;
			}
			
			monstersSmashed += bonusValue;
			
			previousSmash = monster.GetComponent<Monster>().MonsterType;
			//Debug.Log ("Previous Type: " + previousSmash + " | Bonus Value: " + bonusValue);
		}
		else{
			//Debug.Log("Monster is already dead...");
		}
	}
	
	void GameActive(){
		monstersKilledText.text = string.Format("Points: {0}", monstersSmashed);

		timer += 1 * Time.deltaTime;
		
		if(timer > monsterSpawnTimer){
			monsterList.Add(Instantiate(monsterPrefab, new Vector3(spawnPoint.transform.position.x + Random.Range(-maxPosX,maxPosX), spawnPoint.transform.position.y + Random.Range(-maxPosY,maxPosY),0), Quaternion.identity) as GameObject);
			
			var selectBuff = Random.Range (0,100);
			
			if(selectBuff < 1){
				GameObject.Instantiate(destroyAll, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y,0), Quaternion.identity);
			}
			else if(selectBuff < 5){
				GameObject.Instantiate(freezeAll, new Vector3(spawnPoint.transform.position.x , spawnPoint.transform.position.y,0), Quaternion.identity);
			}
			else if(selectBuff < 10){
				GameObject.Instantiate(civilianPrefab, new Vector3(spawnPoint.transform.position.x + Random.Range(-maxPosX,maxPosX), spawnPoint.transform.position.y + Random.Range(-maxPosY,maxPosY),0), Quaternion.identity);
			}
			
			timer = 0;
		}
		
		ScoreKeeper();
	}
	
	public void ChooseFriendlyFirePowTexture(Vector3 position){
		GameObject newPow = (GameObject)Instantiate(powPrefab, new Vector3(position.x, position.y, -1), Quaternion.identity);
		newPow.GetComponentInChildren<Renderer>().material.mainTexture = friendlyFirePow;
		Destroy(newPow, 0.2F);
	}
	
	public void ChoosePowTexture(Vector3 position){
		int random = Random.Range (0,powTextures.Count);
		GameObject newPow = (GameObject)Instantiate(powPrefab, new Vector3(position.x, position.y, -1), Quaternion.identity);
		newPow.GetComponentInChildren<Renderer>().material.mainTexture = powTextures[random];
		Destroy(newPow, 0.2F);
	}
	
	public void ChooseHitPowTexture(Vector3 position){
		int random = Random.Range (0,hitPowTextures.Count);
		GameObject newPow = (GameObject)Instantiate(powPrefab, new Vector3(position.x, position.y, -1), Quaternion.identity);
		newPow.GetComponentInChildren<Renderer>().material.mainTexture = hitPowTextures[random];
		Destroy(newPow, 0.2F);
	}
	
	public void BonusPow(Vector3 position){
		float[] random = {1, -1};
		int selectRandom = Random.Range(0, random.Length);
		
		GameObject newPow = (GameObject)Instantiate(powPrefab, new Vector3(position.x + random[selectRandom], position.y + random[selectRandom], -1), Quaternion.identity);
		newPow.GetComponentInChildren<Renderer>().material.mainTexture = bonusPow;
		Destroy(newPow, 0.3F);
	}
	
	public void GameOver(){
		if(monstersSmashed > PlayerPrefs.GetInt("HighScore")){
			PlayerPrefs.SetInt("HighScore", monstersSmashed);
		}
		PlayerPrefs.SetInt("CurrentScore", monstersSmashed);
		Application.LoadLevel("GameOver");
	}
	
	void DestroyAll(){
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
		
		foreach(var monster in monsters){
			monstersSmashed += DestroyAllBonus;
			monster.GetComponent<Monster>().Dead();
			monsterList.Remove(monster);
		}
	}
	
	void FreezeAll(){
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
		
		foreach(var monster in monsters){
			monster.GetComponent<Monster>().Freeze();
		}
	}
	
	void Civilian(){
		monstersSmashed--;
		Debug.Log ("Hit Civilian");
	}
}