using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CustomTimeline
{
    [Serializable]
    [TrackClipType(typeof(CustomVideoPlayableAsset))]
    [TrackColor(0.008f, 0.698f, 0.655f)]
    public class CustomVideoTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<CustomVideoSchedulerPlayableBehaviour>.Create(graph, inputCount);
        }
    }
}
