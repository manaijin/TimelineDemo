using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace CustomTimeline
{
    /// <summary>
    /// 自定义灯光生命周期逻辑
    /// </summary>
    public class CustomLightSchedulerPlayableBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var light = playerData as Light;
            CustomLightPlayableData blendData = new CustomLightPlayableData() { intensity = 0};
            for (int i = 0; i < playable.GetInputCount(); i++)
            {
                var input = playable.GetInput(i);
                if (input.GetPlayableType() != typeof(CustomLightPlayableData))
                    continue;

                float wight = playable.GetInputWeight(i);
                if (wight <= 0) continue;
                var scriptPlayable = (ScriptPlayable<CustomLightPlayableData>)input;
                CustomLightPlayableData data = scriptPlayable.GetBehaviour();
                BlendData(blendData, data, wight, data.interpolationShortDir);
            }
            SetLight(light, blendData);
        }

        private Vector3 f = new Vector3(360f, 360f, 360f);
        public void BlendData(CustomLightPlayableData result, CustomLightPlayableData input, float wight, bool interpolationShortDir = true)
        {
            result.color += wight * input.color;
            result.position += wight * input.position;
            result.intensity += wight * input.intensity;
            var source = input.rotation.eulerAngles;
            if (interpolationShortDir)
            {                
                if (source.x > 180)
                    source.x -= 360;
                if (source.y > 180)
                    source.y -= 360;
                if (source.z > 180)
                    source.z -= 360;
            }
            Vector3 r = result.rotation.eulerAngles + source * wight;
            result.rotation = Quaternion.Euler(r.x, r.y, r.z);
        }

        public void SetLight(Light light, CustomLightPlayableData data)
        {
            light.transform.position = data.position;
            light.transform.rotation = data.rotation;
            light.color = data.color;
            light.intensity = data.intensity;
        }
    }
}
