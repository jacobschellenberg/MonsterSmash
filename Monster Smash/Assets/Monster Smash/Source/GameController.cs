using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject monsterPrefab;
	public GameObject spawnPoint;
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
	private int previousHit = -1;
	private int bonusValue;
	private List<GameObject> monsterList = new List<GameObject>();
	private Vector3 currentMouseClickPosition;
	bool gameOver;

	#region everyplay
	private bool isRecording = false;
	private bool isPaused = false;
	private bool isRecordingFinished = false;
	#endregion

	void Start(){
		if(Everyplay.SharedInstance != null) {
			Everyplay.SharedInstance.RecordingStarted += RecordingStarted;
			Everyplay.SharedInstance.RecordingStopped += RecordingStopped;
			Everyplay.SharedInstance.WasClosed += LoadEndScene;
		}

		if(!isRecording)
			Everyplay.SharedInstance.StartRecording();
	}

	void Destroy() {
		if(Everyplay.SharedInstance != null) {
			Everyplay.SharedInstance.RecordingStarted -= RecordingStarted;
			Everyplay.SharedInstance.RecordingStopped -= RecordingStopped;
			Everyplay.SharedInstance.WasClosed -= LoadEndScene;
		}
	}

	private void RecordingStarted() {
		isRecording = true;
		isPaused = false;
		isRecordingFinished = false;
	}
	
	private void RecordingStopped() {
		isRecording = false;
		isRecordingFinished = true;

		Everyplay.SharedInstance.PlayLastRecording();
	}

	void Update(){
		if(!gameOver){
			MouseToScreenDetection();
			GameActive();
		}
	}
	
	public void MouseToScreenDetection(){
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
				
				MonstersSmashed += bonusValue;
				previousHit = hit.transform.GetComponent<Monster>().MonsterType;

				if(hit.collider.CompareTag("Monster"))
					ShowHitPow(currentMouseClickPosition);
				else if(hit.collider.CompareTag("DestroyAll"))
					MonstersSmashed += destroyAllBonus;
			}
		}
	}
	
	void GameActive(){
		timer += 1 * Time.deltaTime;
		
		if(timer > monsterSpawnTimer){
			var newMonster = NGUITools.AddChild(this.gameObject, monsterPrefab);
			newMonster.transform.localPosition = new Vector3(spawnPoint.transform.localPosition.x + Random.Range(-maxPosX,maxPosX), spawnPoint.transform.localPosition.y + Random.Range(-maxPosY,maxPosY),0);
			monsterList.Add(newMonster);

			var selectBuff = Random.Range (0.0f,100.0f);
			if(selectBuff < 1)
				NGUITools.AddChild(this.gameObject, destroyAllPrefab);
			else if(selectBuff < 5)
				NGUITools.AddChild(this.gameObject, freezeAllPrefab);
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
		gameOver = true;
		if(MonstersSmashed > PlayerPrefs.GetInt("HighScore")){
			PlayerPrefs.SetInt("HighScore", MonstersSmashed);
		}
		PlayerPrefs.SetInt("CurrentScore", MonstersSmashed);

		if(Everyplay.SharedInstance.IsRecordingSupported() && isRecording)
			Everyplay.SharedInstance.StopRecording();
		else
			LoadEndScene();
	}

	public void LoadEndScene(){
		Application.LoadLevel("GameOver");
	}
}
