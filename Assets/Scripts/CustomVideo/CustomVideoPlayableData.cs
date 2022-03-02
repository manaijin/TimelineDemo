using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace CustomTimeline
{
    public class CustomVideoPlayableData : PlayableBehaviour
    {
        [Tooltip("Video 资源")]
        public VideoClip[] videoClip;

        [Tooltip("起始帧")]
        public int startFrame = 0;

        [Tooltip("播放速度")]
        public float playSpeed = 1;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (!Application.isPlaying) return;
            if (videoClip == null || videoClip.Length == 0) return;
            VideoClipParam[] input = new VideoClipParam[videoClip.Length];
            for (int i = 0; i < videoClip.Length; i++)
            {
                var p = new VideoClipParam();
                p.speed = playSpeed;
                p.startFrames = startFrame;
                input[i] = p;
            }
            VideosManager.Instance.PlayVideos(videoClip, input);
            var rts = VideosManager.Instance.GetAllVideoRenderTexture();
            if (rts == null)
            {
                Debug.LogError("rts is null");
                return;
            }
            var ui = UIManager.Instance.GetUI("UITimeline") as UITimeline;
            ui.ShowTwoVideo(rts[0], rts[1]);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (!Application.isPlaying) return;
            VideosManager.Instance.PauseAllVideos();
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (!Application.isPlaying) return;
            foreach (var clip in videoClip)
            {
                VideosManager.Instance.RecoveryVideoByClip(clip);
            }
        }
    }
}

