using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

	public float maxPosX = 735;
	public float maxPosY = 735;
	public GameObject actorPrefab;
	public GameObject spawnPoint;

	private List<GameObject> actors = new List<GameObject>();

	public void SpawnActor(){
		float xOffset = Random.Range(-maxPosX, maxPosX);
		float yOffset = Random.Range(-maxPosY, maxPosY);
		
		GameObject newMonster = NGUITools.AddChild(this.gameObject, actorPrefab);
		newMonster.transform.localPosition = new Vector3(spawnPoint.transform.localPosition.x + xOffset, spawnPoint.transform.localPosition.y + yOffset,0);
		actors.Add(newMonster);
	}
}
