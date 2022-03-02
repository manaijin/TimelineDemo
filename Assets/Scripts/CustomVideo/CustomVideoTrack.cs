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
            foreach (var clip in GetClips())
            {
                var asset = clip.asset as CustomVideoPlayableAsset;
                if (asset != null && asset.videoClip != null && asset.videoClip.Length > 0)
                {
                    if (asset.inited) continue;
                    clip.duration = asset.videoClip[0].frameCount / 60;
                    asset.inited = true;
                }
            }
            return ScriptPlayable<CustomVideoPlayableData>.Create(graph, inputCount);
        }
    }
}
