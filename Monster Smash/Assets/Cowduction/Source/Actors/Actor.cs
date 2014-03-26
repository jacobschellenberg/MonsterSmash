﻿using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	public float hitPoints = 1;
	public float movementSpeed = 0.5f;
	public float destroyDelay = 1;

	private bool isAlive = true;
	private int randomDirection;

	// Initialize
	void Start(){
		// Set Alive Texture
		GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.Cows);
		randomDirection = Random.Range(0,2);
	}

	void Update(){
		// Movement
		if(isAlive)
			transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
		else{
			if(randomDirection == 0)
				transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
			else
				transform.Rotate(Vector3.forward * -360 * Time.deltaTime);
		}
	}

	public void OnHit(GameObject source){
		hitPoints -= GameController.Instance.OnActorHit(this);
		if(hitPoints <= 0){
			// Abducted
			isAlive = false;
			movementSpeed = 0;
			gameObject.collider.enabled = false;
			GameController.Instance.OnActorAbducted(this);
			Destroy(gameObject, destroyDelay);
		}
	}
}
