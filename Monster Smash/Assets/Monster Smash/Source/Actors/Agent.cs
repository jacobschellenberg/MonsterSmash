using UnityEngine;
using System.Collections.Generic;

public class Agent : MonsterBase {

	public override void SetAliveTexture(){
		GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.Agents);
	}

	public override void SetDeadTexture(){
		GetComponentInChildren<UITexture>().mainTexture = TextureManager.GetRandomTexture(TextureType.BloodSplats);
	}
}
