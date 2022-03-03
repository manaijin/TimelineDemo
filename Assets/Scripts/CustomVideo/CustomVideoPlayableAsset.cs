using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Video;

namespace CustomTimeline
{
    [Serializable]
    public class CustomVideoPlayableAsset : PlayableAsset, ITimelineClipAsset
    {
        [Tooltip("输出目标类型")]
        public e_VideoOutputType target;

        [Tooltip("Video 资源")]
        public VideoClip[] videoClip;

        [Tooltip("开始帧")]
        public int[] startFrame;

        [Tooltip("播放速度")]
        public float[] playSpeed;

        [Tooltip("遮罩纹理")]
        public Texture[] masks;

        [Tooltip("遮罩uv范围")]
        public Rect[] uvRects;

        [Tooltip("混合周期(s)")]
        public float cycle = 5;

#if UNITY_EDITOR
        public bool inited = false;
#endif

        public ClipCaps clipCaps
        {
            get
            {
                return ClipCaps.Blending | ClipCaps.ClipIn | ClipCaps.SpeedMultiplier | ClipCaps.Looping;
            }
        }
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable =  ScriptPlayable<CustomVideoPlayableData>.Create(graph);
            
            var playableBehaviour = playable.GetBehaviour();
            playableBehaviour.target = this.target;
            playableBehaviour.videoClip = this.videoClip;
            playableBehaviour.startFrame = this.startFrame;
            playableBehaviour.playSpeed = this.playSpeed;
            playableBehaviour.uvRects = this.uvRects;
            playableBehaviour.masks = this.masks;
            playableBehaviour.cycle = this.cycle;

            return playable;
        }
    }
}
