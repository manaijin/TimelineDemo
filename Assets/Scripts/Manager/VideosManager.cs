using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideosManager
{
    private static VideosManager instance = null;
    private static object o = new object();
    private static Transform root;
    private static Transform showNode;
    private static Transform hideNode;

    private Camera videoCamera = null;
    private List<VideoPlayer> currentVides = new List<VideoPlayer>();
    private VideosManager()
    {
        videoCamera = Camera.main;
    }

    public static VideosManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (o)
                {
                    if (instance == null)
                    {
                        instance = new VideosManager();
                        root = new GameObject("Videos").transform;
                        showNode = new GameObject("Show").transform;
                        hideNode = new GameObject("Hide").transform;
                        showNode.SetParent(root);
                        hideNode.SetParent(root);
                        hideNode.gameObject.SetActive(false);
                    }
                }
            }
            return instance;
        }
    }

    /// <summary>
    /// 播放多个视频（默认渲染到RT）
    /// </summary>
    /// <param name="clips"></param>
    /// <param name="param"></param>
    public void PlayVideos(VideoClip[] clips, VideoClipParam[] param)
    {
        if (clips == null || param == null) return;
        int length = clips.Length;
        if (length == 0) return;
        if (length != param.Length) return;

        RecoveryCurrentVideos();
        VideoPlayer[] cmops = GetVideoPlayers(length);
        for (int i = 0; i < length; i++)
        {
            VideoPlayer v = cmops[i];
            VideoClip clip = clips[i];
            VideoClipParam p = param[i];
            p.tartget = e_VideoTarget.RenderTexture;
            ApplyVideoParam(v, clip, p);
            v.Play();
            currentVides.Add(v);
        }
    }


    /// <summary>
    /// 播放单个视频
    /// </summary>
    public void PlaySingleVideo(VideoClip clip, VideoClipParam param)
    {
        if (clip == null)
        {
            Debug.LogError("clip is null");
            return;
        }

        RecoveryCurrentVideos();
        VideoPlayer v = GetVideoPlayer();
        ApplyVideoParam(v, clip, param);
        v.Play();

        currentVides.Add(v);
    }

    /// <summary>
    /// 获取当前播放RT
    /// </summary>
    /// <returns></returns>
    public RenderTexture[] GetAllVideoRenderTexture()
    {
        List<RenderTexture> result = new List<RenderTexture>();
        if (!showNode) return default;
        int count = showNode.childCount;
        for (int i = 0; i < count; i++)
        {
            var child = showNode.GetChild(i);
            var v = child.GetComponent<VideoPlayer>();
            if (!v.targetTexture) continue;
            result.Add(v.targetTexture);
        }
        return result.ToArray();
    }

    /// <summary>
    /// 暂停所有当前视频
    /// </summary>
    public void PauseAllVideos()
    {
        foreach (var video in currentVides)
        {
            video.Pause();
        }
    }

    /// <summary>
    /// 设置VideoPlayer参数
    /// </summary>
    /// <param name="v"></param>
    /// <param name="clip"></param>
    /// <param name="param"></param>
    private void ApplyVideoParam(VideoPlayer v, VideoClip clip, VideoClipParam param)
    {
        v.clip = clip;
        if (v.canSetPlaybackSpeed)
        {
            v.playbackSpeed = param.speed;
        }

        switch (param.tartget)
        {
            case e_VideoTarget.Camera:
                v.targetCamera = videoCamera;
                break;
            case e_VideoTarget.RenderTexture:
                v.targetTexture = RenderTexture.GetTemporary((int)clip.width, (int)clip.height);
                break;
            default:
                Debug.LogError($"{param.tartget}类型需要添加处理代码");
                break;
        }

        v.time = param.startTime;
    }

    /// <summary>
    /// 获取多个VideoPlayer组件
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private VideoPlayer[] GetVideoPlayers(int count)
    {
        var result = new VideoPlayer[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = GetVideoPlayer();
        }
        return result;
    }

    /// <summary>
    /// 获取VideoPlayer
    /// </summary>
    /// <returns></returns>
    private VideoPlayer GetVideoPlayer()
    {
        VideoPlayer v;
        if (hideNode && hideNode.childCount > 0)
        {
            var child = hideNode.GetChild(0);
            child.SetParent(showNode);
            v = child.GetComponent<VideoPlayer>();
        }
        else
        {
            v = new GameObject("Video").AddComponent<VideoPlayer>();
            v.transform.SetParent(showNode);
        }
        ResetPlayVideo(v);
        return v;
    }

    /// <summary>
    /// 销毁所有创建组件
    /// </summary>
    public void ClearAll()
    {
        DestoryAllChild(showNode);
        DestoryAllChild(hideNode);
    }

    private void DestoryAllChild(Transform t)
    {
        var count = t.childCount;
        for (int i = count - 1; i <= 0; i--)
        {
            GameObject.Destroy(t.GetChild(i).gameObject);
        }
    }

    public void RecoveryVideoByClip(VideoClip clip)
    {
        if (currentVides.Count > 0)
        {
            foreach (var video in currentVides)
            {
                if (video == null) continue;
                if (video.clip == clip)
                {
                    ResetPlayVideo(video);
                    video.transform.SetParent(hideNode);
                    currentVides.Remove(video);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 回收当前播放组件
    /// </summary>
    public void RecoveryCurrentVideos()
    {
        if (currentVides.Count > 0)
        {
            foreach (var video in currentVides)
            {
                if (video == null) continue;
                ResetPlayVideo(video);
                video.transform.SetParent(hideNode);
            }
            currentVides.Clear();
        }
    }

    /// <summary>
    /// 重置VideoPlayer组件
    /// </summary>
    /// <param name="v"></param>
    private void ResetPlayVideo(VideoPlayer v)
    {
        if (!v) return;
        if (v.isPlaying)
            v.Stop();
        if (v.targetTexture)
        {
            RenderTexture.ReleaseTemporary(v.targetTexture);
            v.targetTexture = null;
        }
        v.targetCamera = null;
        if (currentVides.Contains(v))
            currentVides.Remove(v);
    }
}

/// <summary>
/// Video渲染目标类型
/// </summary>
public enum e_VideoTarget
{
    /// <summary>
    /// 渲染到摄像机
    /// </summary>
    Camera = 0,
    /// <summary>
    /// 渲染到RT
    /// </summary>
    RenderTexture,
}

/// <summary>
/// Video参数
/// </summary>
public class VideoClipParam
{
    /// <summary>
    /// 起始时间
    /// </summary>
    public double startTime = 0;

    /// <summary>
    /// 播放速度
    /// </summary>
    public float speed = 1;

    /// <summary>
    /// Video目标类型
    /// </summary>
    public e_VideoTarget tartget = e_VideoTarget.Camera;
}
