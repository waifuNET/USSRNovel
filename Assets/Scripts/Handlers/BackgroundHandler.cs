using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BackgroundHandler : MonoBehaviour
{
	[SerializeField] private Image background;

	public void SetNewBackground(string FilePath)
	{
		FilePath = Path.Combine(Application.streamingAssetsPath, "Backgrounds/" + FilePath).Replace('\\', '/');
		StartCoroutine(ISetNewBackground(FilePath));
	}

	private IEnumerator ISetNewBackground(string FilePath)
	{
		byte[] imgData;
		Texture2D tex = new Texture2D(2, 2);

		if (!File.Exists(FilePath))
		{
			Debug.LogWarning(FilePath + ": not found!");
			yield return null;
		}
		else
		{
			if (FilePath.Contains("://") || FilePath.Contains(":///"))
			{
				UnityWebRequest www = UnityWebRequest.Get(FilePath);
				yield return www.SendWebRequest();
				imgData = www.downloadHandler.data;
			}
			else
			{
				imgData = File.ReadAllBytes(FilePath);
			}
			Debug.Log(imgData.Length);

			tex.LoadImage(imgData);

			Vector2 pivot = new Vector2(0.5f, 0.5f);
			Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);

			background.sprite = sprite;
		}
	}
}
