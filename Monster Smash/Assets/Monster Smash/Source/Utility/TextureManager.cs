using UnityEngine;
using System.Collections;

// Named based on actual name of folder in Resources
public enum TextureType{
	Agents,
	BloodSplats,
	Monsters,
	PowerUps,
	HitPows,
	MissPows,
	BonusPows,
	FriendlyHitPows
}

public static class TextureManager {

	public static Texture2D GetRandomTexture(TextureType textureType){
		var textures = Resources.LoadAll(textureType.ToString());
		var texture = (Texture2D)textures[Random.Range(0,textures.Length)];

		return texture;
	}

	public static Texture2D GetTextureAtIndex(TextureType textureType, int index){
		var textures = Resources.LoadAll(textureType.ToString());
		var texture = (Texture2D)textures[index];
		
		return texture;
	}
}
