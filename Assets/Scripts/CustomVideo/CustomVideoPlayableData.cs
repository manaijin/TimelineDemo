using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace CustomTimeline
{
    public class CustomVideoPlayableData : PlayableBehaviour
    {
        [Tooltip("输出目标类型")]
        public e_VideoOutputType target;

        [Tooltip("Video 资源")]
        public VideoClip[] videoClip;

        [Tooltip("起始帧")]
        public int[] startFrame;

        [Tooltip("播放速度")]
        public float[] playSpeed;



        [Tooltip("遮罩uv范围")]
        public Rect[] uvRects;

        [Tooltip("遮罩纹理")]
        public Texture[] masks;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (!Application.isPlaying) return;
            if (videoClip == null || videoClip.Length == 0) return;
            VideoClipParam[] input = new VideoClipParam[videoClip.Length];
            for (int i = 0; i < videoClip.Length; i++)
            {
                var p = new VideoClipParam();
                p.speed = playSpeed[i];
                p.startFrames = startFrame[i];
                input[i] = p;
            }
            VideosManager.Instance.PlayVideos(videoClip, input);
            var rts = VideosManager.Instance.GetAllVideoRenderTexture();
            if (rts == null)
            {
                Debug.LogError("rts is null");
                return;
            }
            if (target == e_VideoOutputType.DoubleMask)
            {
                var ui = UIManager.Instance.GetUI("UITimeline") as UITimeline;
                ui.BlendMaskVideo(rts[0], rts[1], uvRects, masks);
            }
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

    public enum e_VideoOutputType
    {
        /// <summary>
        /// 单纹理输出
        /// </summary>
        Single,
        /// <summary>
        /// 双纹理混合
        /// </summary>
        DoubleBlend,
        /// <summary>
        /// 双纹理遮罩
        /// </summary>
        DoubleMask,
    }
}

