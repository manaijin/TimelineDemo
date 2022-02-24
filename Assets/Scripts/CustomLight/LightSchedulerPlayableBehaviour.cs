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
    public class LightSchedulerPlayableBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var light = playerData as Light;
            CustomLightPlayableData blendData = new CustomLightPlayableData();
            for (int i = 0; i < playable.GetInputCount(); i++)
            {
                var input = playable.GetInput(i);
                if (input.GetPlayableType() != typeof(CustomLightPlayableData))
                    continue;

                float wight = playable.GetInputWeight(i);
                var scriptPlayable = (ScriptPlayable<CustomLightPlayableData>)input;
                CustomLightPlayableData data = scriptPlayable.GetBehaviour();
                BlendData(blendData, data, wight);
            }
            SetLight(light, blendData);
        }

        public void BlendData(CustomLightPlayableData result, CustomLightPlayableData input, float wight)
        {
            result.color += wight * input.color;
            result.position += wight * input.position;
            result.intensity += wight * input.intensity;
            Vector3 r = result.rotation.eulerAngles + input.rotation.eulerAngles * wight;
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
