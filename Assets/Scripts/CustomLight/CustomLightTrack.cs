using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace CustomTimeline
{
    [Serializable]
    [TrackClipType(typeof(CustomLightAsset))]
    [TrackBindingType(typeof(Light))]
    [TrackColor(0.008f, 0.698f, 0.655f)]
    public class CustomLightTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var clip in GetClips())
            {
                var asset = clip.asset as CustomLightAsset;
                if (!asset) continue;
            }
            return ScriptPlayable<CustomLightSchedulerPlayableBehaviour>.Create(graph, inputCount);
        }

        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
            base.GatherProperties(director, driver);
        }
    }
}

