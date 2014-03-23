using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cow : ActorBase {

	public int Type{ get; set; }

	void Awake(){
		originalSpeed = movementSpeed;
		originalSideMovementSpeed = sideMovementSpeed;
	}
	
	void SelectAI(){
//		switch(this.MonsterType){
//		case 0: //Red - 3 Hits
//			hitPoints = 3;
//			break;
//		case 1: //Green - Single Hit
//			hitPoints = 1;
//			break;
//		case 2: //Purple - Zig Zag
//			hitPoints = 1;
//			break;
//		case 3: //Yellow - Dodge
//			hitPoints = 1;
//			break;
//		case 4: //DarkGreen - fast
//			hitPoints = 1;
//			movementSpeed = 0.7f;
//			break;
//		case 5: //Hulk Thing - 10 Hits
//			GetComponentInChildren<Transform>().localScale = new Vector3(3,3, 1);
//			hitPoints = 25;
//			movementSpeed = 0.2f;
//			break;
//		}

		hitPoints = 1;
	}
	
	public override void Alive(){
		base.Alive();
		SelectAI();

		gameObject.collider.enabled = true;
	}
	
//	public override void Movement(){
//		switch(this.MonsterType){
//			case 0: //Red - 3 Hits
//				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
//			break;
//			case 1: //Green - Single Hit
//				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
//			break;
//			case 2: //Purple - Zig Zag
//				ZigZag();
//				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
//			break;
//			case 3: //Yellow - Dodge
//				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
//			break;
//			case 4: //DarkGreen - fast
//				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
//			break;
//			case 5: //Hulk Thing - 10 Hits
//				transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
//			break;
//		}
//		if(isAlive)
//			transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
//	}

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
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);
		dodge = false;
	}

	public override void SetAliveTexture(){
		//TODO: This probably doesn't belong here....
		var randomAi = Random.Range(0.0F, 1.0F);

		if(randomAi < 0.05F){
			Type = 5;
		}
		else if(randomAi < 0.15F){
			Type = 4;
		}
		else if(randomAi < 0.5F){
			Type = 3;
		}
		else if(randomAi < 0.6F){
			Type = 2;
		}
		else if(randomAi < 0.7F){
			Type = 0;
		}
		else{
			Type = 1;
		}
	
		GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.Cows);
	}
	
	public override void SetDeadTexture(){
		//GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.BloodSplats);
	}
	
//	public void Hit(int damage){
////		if(MonsterType == 3 && dodge){
////			Dodge();
////		}
////		else{
//		hitPoints -= damage;
////		}	
//
////		if(MonsterType == 5){
////			transform.Translate(Vector3.up * 2 * Time.deltaTime);
////		}
//		
//		if(hitPoints <= 0){
//			Dead ();
//		}
//	}
}