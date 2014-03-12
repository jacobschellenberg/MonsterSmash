using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Actor : MonoBehaviour {

	public float hitPoints;
	public float movementSpeed = 0.5f;
	public float originalSpeed;
	public float sideMovementSpeed = 0.5f;
	public float originalSideMovementSpeed;
	public bool isAlive;
	public bool isFrozen;

	public float frozenTimer = 2;
	public float currentFrozenTimer = 0;
	public float currentRotation = 0;
	public float displayDeadTextureTime = 1;
	public bool toggleDirection = false; //true = right; false = left;
	public bool dodge = true;

	void Start(){
		Alive ();
		Destroy(gameObject, 15);
	}

	void Awake(){
		originalSpeed = movementSpeed;
		originalSideMovementSpeed = sideMovementSpeed;
	}

	void Update(){
		if(!isAlive){
			Movement();
			
			if(isFrozen){
				movementSpeed = 0;
				sideMovementSpeed = 0;
				
				currentFrozenTimer += 1 * Time.deltaTime;
				
				currentRotation += currentRotation < 360 ? 10 * Time.deltaTime : 0;
				transform.Rotate(0,0,currentRotation);
				
				if(currentFrozenTimer > frozenTimer){
					if(!isAlive){
						movementSpeed = originalSpeed;
						sideMovementSpeed = originalSideMovementSpeed;
					}
					currentFrozenTimer = 0;
					transform.rotation = Quaternion.identity;
					isFrozen = false;
				}
			}
		}
	}

	public virtual void Alive(){
		SetAliveTexture();
	}

	public virtual void Movement(){
		transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
	}

	public abstract void SetAliveTexture();
	public abstract void SetDeadTexture();
}
