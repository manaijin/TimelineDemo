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

        [Tooltip("混合周期(s)")]
        public float cycle = 5;


        public override void PrepareFrame(Playable playable, FrameData info)
        {
            if (!Application.isPlaying) return;
            if (target == e_VideoOutputType.DoubleBlend)
            {
                var ui = UIManager.Instance.GetUI("UITimeline") as UITimeline;
                if (!ui) return;
                double time = playable.GetTime();
                float weight = Mathf.Sin((float)time / cycle * Mathf.PI * 2) * 0.5f + 0.5f;
                ui.SetBlendWeight(weight, 1 - weight);
            }
        }

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
            PlayVideo(input);

        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (!Application.isPlaying) return;
            RecoveryPlayer();
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (!Application.isPlaying) return;
            RecoveryPlayer();
        }

        private void RecoveryPlayer()
        {
            if (videoClip == null) return;
            foreach (var clip in videoClip)
            {
                VideosManager.Instance.RecoveryVideoByClip(clip);
            }
        }

        private void PlayVideo(VideoClipParam[] input)
        {
            if (target == e_VideoOutputType.DoubleMask)
            {
                VideosManager.Instance.PlayVideos(videoClip, input);
                var rts = VideosManager.Instance.GetAllVideoRenderTexture();
                var ui = UIManager.Instance.GetUI("UITimeline") as UITimeline;
                ui.MaskVideo(rts[0], rts[1], uvRects, masks);
            }
            else if (target == e_VideoOutputType.DoubleBlend)
            {
                VideosManager.Instance.PlayVideos(videoClip, input);
                var rts = VideosManager.Instance.GetAllVideoRenderTexture();
                var ui = UIManager.Instance.GetUI("UITimeline") as UITimeline;
                ui.BlendMask(rts[0], rts[1], 1, 1);
            }
            else if (target == e_VideoOutputType.Single)
            {

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

