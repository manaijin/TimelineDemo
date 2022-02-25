using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace CustomTimeline
{
    public class CustomVideoPlayableData : PlayableBehaviour
    {
        [Tooltip("Video ��Դ")]
        public VideoClip videoClip;

        [Tooltip("��ʼ֡")]
        public int startFrame = 0;

        [Tooltip("�����ٶ�")]
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

