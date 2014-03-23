using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ActorBase : MonoBehaviour {

	public GameController gameController;
	public float hitPoints;
	public float movementSpeed = 0.5f;
	public float originalSpeed;
	public float sideMovementSpeed = 0.5f;
	public float originalSideMovementSpeed;
	public bool isAlive;
	public float displayDeadTextureTime = 1;
	public bool toggleDirection = false; //true = right; false = left;
	public bool dodge = true;

	private int randomDirection;

	void Start(){
		Alive ();
		Destroy(gameObject, 15);
		gameController = GameObject.FindObjectOfType<GameController>();
	}

	void Awake(){
		originalSpeed = movementSpeed;
		originalSideMovementSpeed = sideMovementSpeed;
	}
	
	bool chosenDirection;
	void Update(){
		if(isAlive)
			Movement();
		else{
			if(!chosenDirection){
				randomDirection = Random.Range(0,2);
				chosenDirection = true;
			}

			if(randomDirection == 0){
				transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
			}
			else
				transform.Rotate(Vector3.forward * -360 * Time.deltaTime);

			transform.localScale = new Vector3(transform.localScale.x + .5f * Time.deltaTime, transform.localScale.y + .5f * Time.deltaTime, 1);
		}
	}

	public virtual void Alive(){
		isAlive = true;
		SetAliveTexture();
	}

	public virtual void Movement(){
		transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
	}

	public virtual void OnHit(GameObject monster){
		gameController.ShowHitPow(this.transform.localPosition);

		hitPoints -= gameController.TapDamage;

		if(hitPoints <= 0){
			Dead ();
		}
	}

	public virtual void Dead(){
		isAlive = false;
		movementSpeed = 0;
		sideMovementSpeed = 0;
		gameObject.collider.enabled = false;
		SetDeadTexture();
		gameController.scoreController.TotalPoints++;
		Destroy(gameObject, displayDeadTextureTime);
	}

	public abstract void SetAliveTexture();
	public abstract void SetDeadTexture();
}
