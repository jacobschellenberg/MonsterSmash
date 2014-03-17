using UnityEngine;
using System.Collections;

public class DestroyAll : PowerUpBase {

	public override IEnumerator Effect(){
		foreach(var monster in monsters)
			monster.GetComponent<Monster>().Dead();

		Destroy(this.gameObject);

		yield return null;
	}
}
