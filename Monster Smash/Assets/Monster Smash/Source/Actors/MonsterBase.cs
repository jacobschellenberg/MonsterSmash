using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MonsterBase : MonoBehaviour {

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

	void Start(){
		Alive ();
		Destroy(gameObject, 15);
		gameController = GameObject.FindObjectOfType<GameController>();
	}

	void Awake(){
		originalSpeed = movementSpeed;
		originalSideMovementSpeed = sideMovementSpeed;
	}

	void Update(){
		if(!isAlive)
			Movement();
	}

	public virtual void Alive(){
		SetAliveTexture();
	}

	public virtual void Movement(){
		transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
	}

	void OnHit(GameObject monster){
		if(!monster.GetComponent<Monster>().isAlive)
			monster.GetComponent<Monster>().Hit(gameController.TapDamage);
	}

	public abstract void SetAliveTexture();
	public abstract void SetDeadTexture();
}
