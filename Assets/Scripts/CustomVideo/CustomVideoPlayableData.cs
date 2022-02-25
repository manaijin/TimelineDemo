using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace CustomTimeline
{
    public class CustomVideoPlayableData : PlayableBehaviour
    {
        [Tooltip("Video 资源")]
        public VideoClip videoClip;

        [Tooltip("开始帧")]
        public int startFrame = 0;

        [Tooltip("播放速度")]
        public float playSpeed = 1;

        public override void PrepareFrame(Playable playable, FrameData info)
        {

        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {

        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {

        }

        public override void OnPlayableDestroy(Playable playable)
        {

        }
    }
}

