using UnityEngine;
using System.Collections.Generic;

public class Castle : MonoBehaviour {
	
	public List<GameObject> LifePoints = new List<GameObject>();
	public Texture2D LifeTexture;
	public Texture2D MissingLifeTexture;
	public GameObject castle;
	public GameObject gameController;
	public GameObject hitFlash;
	public List<GameObject> endExplosions;
	public float hitFlashTimer = 0.1F;
	List<GameObject> MissingLifePoints = new List<GameObject>();
	int health;
	bool isHit = false;
	float timer = 0;
	int initialLife;

	void Start(){
		initialLife = LifePoints.Count;
		health = LifePoints.Count;

		foreach(GameObject explosion in endExplosions)
			explosion.SetActive(false);
	}

	void Update(){
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
	
	public void OnTriggerEnter(Collider trigger){
	
		if(trigger.gameObject.CompareTag("Monster")){
			endExplosions[Random.Range(0,endExplosions.Count)].SetActive(true);

			health--;

			MissingLifePoints.Add(LifePoints[LifePoints.Count - 1]);
			LifePoints.RemoveAt(LifePoints.Count - 1);
			
			isHit = true;
			Destroy (trigger.gameObject);
			
			if(health < 1){
				gameController.GetComponent<GameController>().GameOver();	
			}
		}
		if(trigger.gameObject.CompareTag("Civilian")){
			if(health < initialLife){
				health++;
				LifePoints.Add(MissingLifePoints[MissingLifePoints.Count - 1]);
				MissingLifePoints.RemoveAt(MissingLifePoints.Count - 1);
			}
		}

		foreach(var lifePoint in LifePoints){
			lifePoint.GetComponentInChildren<UITexture>().mainTexture = LifeTexture;
		}

		foreach(var missingLifePoint in MissingLifePoints){
			missingLifePoint.GetComponentInChildren<UITexture>().mainTexture = MissingLifeTexture;
		}
	}
}
