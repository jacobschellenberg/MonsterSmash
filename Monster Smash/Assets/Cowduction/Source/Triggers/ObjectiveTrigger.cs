using UnityEngine;
using System.Collections.Generic;

public class ObjectiveTrigger : MonoBehaviour {

	public void OnTriggerEnter(Collider trigger){
		GameController.Instance.OnActorHitObjective(trigger.gameObject);
	}
}
