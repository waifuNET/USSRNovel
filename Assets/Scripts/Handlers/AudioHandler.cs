using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;


public class AudioHandler : MonoBehaviour
{
    public AudioSource sourceSFX;
    public AudioSource sourceAmbient;
    public AudioSource sourceMusic;

    public void SetNewSound(string FilePath, string type, bool loop)
    {
        switch (type)
        {
            case "sfx":
                FilePath = Path.Combine(Application.streamingAssetsPath, "Sounds/SFX/" + FilePath).Replace("\\", "/");
                StartCoroutine(ISetNewSound(FilePath, sourceSFX, false));
                break;
            case "ambient":
                FilePath = Path.Combine(Application.streamingAssetsPath, "Sounds/Ambient/" + FilePath).Replace("\\", "/");
                StartCoroutine(ISetNewSound(FilePath, sourceAmbient, true));
                break;
            case "music":
                FilePath = Path.Combine(Application.streamingAssetsPath, "Sounds/Music/" + FilePath).Replace("\\", "/");
                StartCoroutine(ISetNewSound(FilePath, sourceMusic, true));
                break;
        }    
    }

    private IEnumerator ISetNewSound(string FilePath,AudioSource source, bool loop)
    {
        source = source.GetComponent<AudioSource>();
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(FilePath, AudioType.UNKNOWN))
        {
            if (!File.Exists(FilePath))
            {
                Debug.LogWarning("AudiFile doesn't exist.");
                yield return null;
            }
            else 
            { 
                yield return www.SendWebRequest();
                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log(www.uri);
                    source.clip = DownloadHandlerAudioClip.GetContent(www);
                    source.loop = loop;
                    source.Play();
                }
            }
        }
    }
    public void PauseAudio(string type)
    {
        switch (type)
        {
            case "sfx":
                sourceSFX.Pause();
                break;
            case "ambient":
                sourceAmbient.Pause();
                break;
            case "music":
                sourceMusic.Pause();
                break;
        }
    }
    public void PlayAudio(string type)
    {

        switch (type)
        {
            case "sfx":
                sourceSFX.Play();
                break;
            case "ambient":
                sourceAmbient.Play();
                break;
            case "music":
                sourceMusic.Play();
                break;
        }
    }
}

