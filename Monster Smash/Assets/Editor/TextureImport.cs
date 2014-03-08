using UnityEditor;
using UnityEngine;
using System.Collections;

public class TextureImport : MonoBehaviour
{
	[MenuItem("UnityExtensionsAndTools/TextureImport/GUI + Point + 512 + Compressed")]
	static void ChangeTextureSize_512_Compressed (){
		SelectedChangeMaxTextureSize (512, true);
	}

	[MenuItem("UnityExtensionsAndTools/TextureImport/GUI + Point + 512 + Uncompressed")]
	static void ChangeTextureSize_512_Uncompressed (){
		SelectedChangeMaxTextureSize (512, false);
	}

	[MenuItem("UnityExtensionsAndTools/TextureImport/GUI + Point + 4096 + Uncompressed")]
	static void ChangeTextureSize_4096_Uncompressed (){
		SelectedChangeMaxTextureSize (4096, false);
	}

	static void SelectedChangeMaxTextureSize (int size, bool compressed){
		Object[] textures = Selection.GetFiltered (typeof(Texture2D), SelectionMode.DeepAssets);
		Selection.objects = new Object[0];
		foreach (Texture2D texture in textures) {
			string path = AssetDatabase.GetAssetPath (texture);
			TextureImporter textureImporter = AssetImporter.GetAtPath (path) as TextureImporter;

			textureImporter.textureType = TextureImporterType.GUI;
			textureImporter.filterMode = FilterMode.Point;
			textureImporter.maxTextureSize = size;
			textureImporter.textureFormat = compressed ? TextureImporterFormat.AutomaticCompressed : TextureImporterFormat.AutomaticTruecolor;
			AssetDatabase.ImportAsset (path);
		}

		Debug.Log ("Modified: " + textures.Length + " textures.");
	}
}
