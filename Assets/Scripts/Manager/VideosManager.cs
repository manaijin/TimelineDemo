using UnityEngine;
using UnityEngine.Video;

public class VideosManager
{
    private static VideosManager instance = null;
    private static object o = new object();

    private Camera videoCamera = null;
    private VideoPlayer currentVides = null;
    private VideosManager()
    {
        videoCamera = Camera.main;
    }


    public static VideosManager Instance()
    {
        if (instance == null)
        {
            lock (o)
            {
                if (instance == null)
                {
                    instance = new VideosManager();
                }
            }
        }
        return instance;
    }


    /// <summary>
    /// ������Ƶ
    /// </summary>
    /// <param name="v"></param>
    /// <param name="frame"></param>
    /// <param name="speed"></param>
    public void PlayVideo(VideoPlayer v, long frame = 0, float speed = 1)
    {
        if (v == null)
        {
            Debug.LogError("video component is null");
            return;
        }

        if (currentVides)
        {
            currentVides.Stop();
            if (v.targetTexture)
                RenderTexture.ReleaseTemporary(v.targetTexture);
        }

        var clip = v.clip;
        if (v.canSetPlaybackSpeed)
        {
            v.playbackSpeed = speed;
        }
        v.targetTexture = RenderTexture.GetTemporary((int)clip.width, (int)clip.height);
        v.frame = frame;
        v.Play();

        currentVides = v;
    }
}
