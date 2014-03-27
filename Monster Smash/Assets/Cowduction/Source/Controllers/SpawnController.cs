using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	public float maxPosX = 735;
	public float maxPosY = 735;
	public GameObject actorPrefab;
	public GameObject spawnPoint;

	public void SpawnActor(){
		float xOffset = Random.Range(-maxPosX, maxPosX);
		float yOffset = Random.Range(-maxPosY, maxPosY);

		GameObject newMonster = NGUITools.AddChild(this.gameObject, actorPrefab);
		newMonster.transform.localPosition = new Vector3(spawnPoint.transform.localPosition.x + xOffset, spawnPoint.transform.localPosition.y + yOffset,0);
	}
}
