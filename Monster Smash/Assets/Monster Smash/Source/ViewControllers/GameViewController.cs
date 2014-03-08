using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameViewController : MonoBehaviour {

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
	public UILabel monstersKilledText;
	public UILabel highScoreText;
	
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
				currentMouseClickPosition = hit.transform.localPosition;
				
				if(hit.collider.CompareTag("Monster")){
					ChooseHitPowTexture(currentMouseClickPosition);
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
					ChooseFriendlyFirePowTexture(currentMouseClickPosition);
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
		}
	}
	
	void GameActive(){
		monstersKilledText.text = string.Format("Points: {0}", monstersSmashed);
		
		timer += 1 * Time.deltaTime;
		
		if(timer > monsterSpawnTimer){
			var newMonster = NGUITools.AddChild(this.gameObject, monsterPrefab);
			newMonster.transform.localPosition = new Vector3(spawnPoint.transform.localPosition.x + Random.Range(-maxPosX,maxPosX), spawnPoint.transform.localPosition.y + Random.Range(-maxPosY,maxPosY),0);
			monsterList.Add(newMonster);
			var selectBuff = Random.Range (0,100);
			
			if(selectBuff < 1){
				GameObject.Instantiate(destroyAll, new Vector3(spawnPoint.transform.localPosition.x, spawnPoint.transform.localPosition.y,0), Quaternion.identity);
			}
			else if(selectBuff < 5){
				GameObject.Instantiate(freezeAll, new Vector3(spawnPoint.transform.localPosition.x , spawnPoint.transform.localPosition.y,0), Quaternion.identity);
			}
			else if(selectBuff < 10){
				GameObject.Instantiate(civilianPrefab, new Vector3(spawnPoint.transform.localPosition.x + Random.Range(-maxPosX,maxPosX), spawnPoint.transform.localPosition.y + Random.Range(-maxPosY,maxPosY),0), Quaternion.identity);
			}
			
			timer = 0;
		}
		
		ScoreKeeper();
	}
	
	public void ChooseFriendlyFirePowTexture(Vector3 position){
		GameObject newPow = (GameObject)Instantiate(powPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
		newPow.GetComponentInChildren<UITexture>().mainTexture = friendlyFirePow;
		Destroy(newPow, 0.2F);
	}
	
	public void ChooseHitPowTexture(Vector3 position){
		int random = Random.Range (0,hitPowTextures.Count);
		GameObject newPow = NGUITools.AddChild(this.gameObject, powPrefab);
		newPow.transform.localPosition = position;
		newPow.GetComponentInChildren<UITexture>().mainTexture = hitPowTextures[random];
		Destroy(newPow, 0.2F);
	}
	
	public void BonusPow(Vector3 position){
		float[] random = {100.0f, -100.0f};
		int selectRandom = Random.Range(0, random.Length);

		GameObject newPow = NGUITools.AddChild(this.gameObject, powPrefab);
		newPow.transform.localPosition = new Vector3(position.x + random[selectRandom], position.y + random[selectRandom], 0);
		newPow.GetComponentInChildren<UITexture>().mainTexture = bonusPow;
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
