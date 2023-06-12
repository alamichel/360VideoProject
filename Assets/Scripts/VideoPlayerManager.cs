using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject videoPlayerObject;

    // Start is called before the first frame update
    // Ref: <https://docs.unity3d.com/ScriptReference/Video.VideoPlayer.html>
    void Start()
    {
        var videoPlayer = mainCamera.AddComponent<UnityEngine.Video.VideoPlayer>();

        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCameraAlpha = 0.5f;

        videoPlayer.url = videoPlayerObject.GetComponent<UnityEngine.Video.VideoPlayer>().url;
        videoPlayer.isLooping = true;

        videoPlayer.Play();
    }
}
