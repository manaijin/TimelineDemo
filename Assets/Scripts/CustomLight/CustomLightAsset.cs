using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Samples;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CustomTimeline
{
    /// <summary>
    /// 自定义灯光资源
    /// </summary>
    [Serializable]
    public class CustomLightAsset : PlayableAsset, ITimelineClipAsset
    {
        [NoFoldOut]
        [NotKeyable] // NotKeyable used to prevent Timeline from making fields available for animation.
        public CustomLightPlayableData template = new CustomLightPlayableData();


        public ClipCaps clipCaps
        {
            get
            {
                return ClipCaps.Blending | ClipCaps.ClipIn | ClipCaps.SpeedMultiplier;
            }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<CustomLightPlayableData>.Create(graph, template);
        }
    }
}
