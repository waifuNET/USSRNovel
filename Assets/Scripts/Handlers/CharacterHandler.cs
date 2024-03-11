using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.LowLevelPhysics;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class CharacterHandler : MonoBehaviour
{
    public GameObject character_prefab;
    public SpriteRenderer spriteRenderer;
    Sprite test;
    public List<GameObject> spriteOnScene = new List<GameObject>();
    public void SetNewSprite(string FilePathSprite, string FilePathEmotion)
    {
        FilePathSprite = Path.Combine(Application.streamingAssetsPath, "Sprites/Hero/" + FilePathSprite).Replace("\\", "/");
        FilePathEmotion = Path.Combine(Application.streamingAssetsPath, "Sprites/Hero/emotions/" + FilePathEmotion).Replace("\\", "/");
        StartCoroutine(ISetNewSprite(FilePathSprite, FilePathEmotion));

    }
    private IEnumerator ISetNewSprite(string FilePathSprite, string FilePathEmotion)
    {
        Texture2D texture = new Texture2D(1, 1);
        byte[] img;
        Debug.Log(FilePathSprite + "AAAAAAAAAA");
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(FilePathSprite))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (FilePathSprite.Contains("://") || FilePathSprite.Contains(":///"))
                {
                    yield return www.SendWebRequest();
                    img = www.downloadHandler.data;
                    texture.LoadImage(img);
                }
                else
                {
                    Debug.Log(www.uri);
                    Debug.Log("Download succes");
                    texture = DownloadHandlerTexture.GetContent(www);
                }
                Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                character_prefab = new GameObject("sprite1");
                character_prefab.AddComponent<SpriteRenderer>();
                spriteRenderer = character_prefab.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
            }
        }
    }
}

