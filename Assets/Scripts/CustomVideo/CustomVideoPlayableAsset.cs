using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Video;

namespace CustomTimeline
{
    [Serializable]
    internal class CustomVideoPlayableAsset : PlayableAsset, ITimelineClipAsset
    {
        [Tooltip("Video 资源")]
        public VideoClip videoClip;

        [Tooltip("开始帧")]
        public int startFrame = 0;

        [Tooltip("播放速度")]
        public float playSpeed = 1;

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

            return playable;
        }
    }
}
