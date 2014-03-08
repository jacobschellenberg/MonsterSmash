using UnityEngine;
using System.Collections;

public class PowerUpController : MonoBehaviour {
	
	public float MaxPosX = 6;
	public float MaxPosY = 6;
	public float RotationSpeed = 50;
	
	void Start(){
		transform.position = new Vector3(Random.Range (-MaxPosX, MaxPosX), Random.Range(-MaxPosY, MaxPosY), 0);
	}
	
	void Update(){
		GetComponentInChildren<Transform>().Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
	}
}
