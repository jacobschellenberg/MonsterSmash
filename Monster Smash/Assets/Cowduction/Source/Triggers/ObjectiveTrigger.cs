using UnityEngine;
using System.Collections.Generic;

public class ObjectiveTrigger : MonoBehaviour {

	public GameViewController gameViewController;

	public void OnTriggerEnter(Collider trigger){
		gameViewController.OnHit(trigger.gameObject);
	}
}
