using UnityEngine;
using System.Linq.Expressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {
	
	public GameObject GameControllerObject;
	public int HitPoints = 3;
	public int MonsterType;

	float MovementSpeed = 0.5f;
	bool frozen = false;
	float originalSpeed;
	float frozenTimer = 2;
	float currentFrozenTimer = 0;
	float currentRotation = 0;
	float displayDeadTextureTime = 1;
	bool toggleDirection = false; //true = right; false = left;
	float sideMovementSpeed = 0.5f;
	float originalSideMovementSpeed;
	bool dodge = true;
	
	public bool IsDead{get;set;}
	
	void Awake(){
		GameControllerObject = GameObject.FindObjectOfType<GameViewController>().gameObject; //GameObject.FindGameObjectWithTag("GameController");
		originalSpeed = MovementSpeed;
		originalSideMovementSpeed = sideMovementSpeed;
	}
	
	void Start(){
		HitPoints = Random.Range(1, HitPoints + 1);
		Alive ();
		Destroy(gameObject, 20);
	}
	
	void Update(){
		
		if(!IsDead){
			Movement(MonsterType);
		
			if(frozen){
				MovementSpeed = 0;
				sideMovementSpeed = 0;

				currentFrozenTimer += 1 * Time.deltaTime;
				
				currentRotation += currentRotation < 360 ? 10 * Time.deltaTime : 0;
				transform.Rotate(0,0,currentRotation);
				
				if(currentFrozenTimer > frozenTimer){
					if(!IsDead){
						MovementSpeed = originalSpeed;
						sideMovementSpeed = originalSideMovementSpeed;
					}
					currentFrozenTimer = 0;
					transform.rotation = Quaternion.identity;
					frozen = false;
				}
			}
		}
	}

	void SelectAI(int monsterType){
		switch(monsterType){
		case 0: //Red - 3 Hits
			HitPoints = 3;
			break;
		case 1: //Green - Single Hit
			HitPoints = 1;
			break;
		case 2: //Purple - Zig Zag
			HitPoints = 1;
			break;
		case 3: //Yellow - Dodge
			HitPoints = 1;
			break;
		case 4: //DarkGreen - fast
			HitPoints = 1;
			MovementSpeed = 0.7f;
			break;
		case 5: //Hulk Thing - 10 Hits
			GetComponentInChildren<Transform>().localScale = new Vector3(3,3, 1);
			HitPoints = 25;
			MovementSpeed = 0.2f;
			break;
		}
	}
	
	public void Alive(){
		ChooseAliveTexture();
		SelectAI(MonsterType);
		gameObject.collider.enabled = true;
	}
	
	public void Movement(int monsterType){

		switch(monsterType){
			case 0: //Red - 3 Hits
				transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime);
			break;
			case 1: //Green - Single Hit
				transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime);
			break;
			case 2: //Purple - Zig Zag
				ZigZag();
				transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime);
			break;
			case 3: //Yellow - Dodge
				transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime);
			break;
			case 4: //DarkGreen - fast
				transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime);
			break;
			case 5: //Hulk Thing - 10 Hits
				transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime);
			break;
		}
	}

	void ZigZag(){
		var width = 655F;
		var random = Random.Range(0.0F, 1.0F);

		if(random < 0.01F){
			toggleDirection = !toggleDirection;
		}

		if(transform.localPosition.x >= width || transform.localPosition.x <= -width){
			toggleDirection = !toggleDirection;
		}

		if(toggleDirection){
			transform.Translate(Vector3.right * sideMovementSpeed * Time.deltaTime);
		}
		else{
			transform.Translate(Vector3.left * sideMovementSpeed * Time.deltaTime);
		}
	}

	void Dodge(){
		transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
		dodge = false;
	}

	public void Dead(){
		MovementSpeed = 0;
		sideMovementSpeed = 0;
		gameObject.collider.enabled = false;
		ChooseDeadTexture();
		Destroy(gameObject, displayDeadTextureTime);
	}
	
	public void ChooseAliveTexture(){
		var randomAi = Random.Range(0.0F, 1.0F);

		if(randomAi < 0.05F){
			MonsterType = 5;
		}
		else if(randomAi < 0.15F){
			MonsterType = 4;
		}
		else if(randomAi < 0.5F){
			MonsterType = 3;
		}
		else if(randomAi < 0.6F){
			MonsterType = 2;
		}
		else if(randomAi < 0.7F){
			MonsterType = 0;
		}
		else{
			MonsterType = 1;
		}

		GetComponentInChildren<UITexture>().mainTexture = GameControllerObject.GetComponent<GameViewController>().AliveMonsterTextures[MonsterType];
	}
	
	public void ChooseDeadTexture(){
		int random = Random.Range(0, GameControllerObject.GetComponent<GameViewController>().DeadMonsterTextures.Count);
		GetComponentInChildren<UITexture>().mainTexture = GameControllerObject.GetComponent<GameViewController>().DeadMonsterTextures[random];
	}
	
	public void Hit(int damage){

		if(MonsterType == 3 && dodge){
			Dodge();
		}
		else{
			HitPoints -= damage;
		}	

		if(MonsterType == 5){
			transform.Translate(Vector3.up * 2 * Time.deltaTime);
		}
		
		if(HitPoints <= 0){
			Dead ();
		}
	}
	
	public void Freeze(){
		frozen = true;
	}
}