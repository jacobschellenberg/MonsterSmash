using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : Actor {
	
	public GameObject GameControllerObject;
	public int MonsterType{ get; set; }

	void Awake(){
		GameControllerObject = GameObject.FindObjectOfType<GameViewController>().gameObject;
		originalSpeed = movementSpeed;
		originalSideMovementSpeed = sideMovementSpeed;
	}
	
	void SelectAI(){
		switch(this.MonsterType){
		case 0: //Red - 3 Hits
			hitPoints = 3;
			break;
		case 1: //Green - Single Hit
			hitPoints = 1;
			break;
		case 2: //Purple - Zig Zag
			hitPoints = 1;
			break;
		case 3: //Yellow - Dodge
			hitPoints = 1;
			break;
		case 4: //DarkGreen - fast
			hitPoints = 1;
			movementSpeed = 0.7f;
			break;
		case 5: //Hulk Thing - 10 Hits
			GetComponentInChildren<Transform>().localScale = new Vector3(3,3, 1);
			hitPoints = 25;
			movementSpeed = 0.2f;
			break;
		}
	}
	
	public override void Alive(){
		base.Alive();
		SelectAI();

		gameObject.collider.enabled = true;
	}
	
	public override void Movement(){
		switch(this.MonsterType){
			case 0: //Red - 3 Hits
				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
			break;
			case 1: //Green - Single Hit
				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
			break;
			case 2: //Purple - Zig Zag
				ZigZag();
				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
			break;
			case 3: //Yellow - Dodge
				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
			break;
			case 4: //DarkGreen - fast
				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
			break;
			case 5: //Hulk Thing - 10 Hits
				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
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
		movementSpeed = 0;
		sideMovementSpeed = 0;
		gameObject.collider.enabled = false;
		SetDeadTexture();
		Destroy(gameObject, displayDeadTextureTime);
	}
	
	public override void SetAliveTexture(){
		//TODO: This probably doesn't belong here....
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
	
		GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetTextureAtIndex(TextureType.Monsters, MonsterType);
	}
	
	public override void SetDeadTexture(){
		GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.BloodSplats);
	}
	
	public void Hit(int damage){
		if(MonsterType == 3 && dodge){
			Dodge();
		}
		else{
			hitPoints -= damage;
		}	

		if(MonsterType == 5){
			transform.Translate(Vector3.up * 2 * Time.deltaTime);
		}
		
		if(hitPoints <= 0){
			Dead ();
		}
	}
	
	public void Freeze(){
		isFrozen = true;
	}
}