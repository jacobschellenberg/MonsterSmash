using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour {

	public float hitPoints = 1;
	public float movementSpeed = 0.5f;
	public float destroyDelay = 1;

	private bool isAlive;
	private int randomDirection;
	private bool absorbedRotateDirection;

	void Start(){
		Initialize ();
	}

	void Update(){
		Movement();
	}

	public void Initialize(){
		isAlive = true;
		SetAliveTexture();
		gameObject.collider.enabled = true;
		randomDirection = Random.Range(0,2);
	}

	public void Movement(){
		if(isAlive)
			transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
		else{
			if(randomDirection == 0){
				transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
			}
			else
				transform.Rotate(Vector3.forward * -360 * Time.deltaTime);
			
			transform.localScale = new Vector3(transform.localScale.x + .5f * Time.deltaTime, transform.localScale.y + .5f * Time.deltaTime, 1);
		}
	}

	public void OnHit(GameObject source){
		GameController.Instance.gameViewController.ShowHitPow(source.transform.localPosition);
		hitPoints -= GameController.Instance.TapDamage;
		if(hitPoints <= 0){
			Dead ();
		}
	}

	public void Dead(){
		isAlive = false;
		movementSpeed = 0;
		gameObject.collider.enabled = false;
		GameController.Instance.scoreController.TotalPoints++;
		Destroy(gameObject, destroyDelay);
	}

	public void SetAliveTexture(){
		GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.Cows);
	}
}
