using System;
using UnityEngine;
using UnityEngine.Playables;

namespace CustomTimeline
{
    /// <summary>
    /// 自定义灯光组件参数
    /// </summary>
    [Serializable]
    public class CustomLightPlayableData : PlayableBehaviour
    {
        [Tooltip("灯光颜色")]
        public Color color = Color.clear;

        [Tooltip("灯光位置")]
        public Vector3 position = Vector3.zero;

        [Tooltip("灯光角度")]
        public Quaternion rotation = Quaternion.identity;

        [Tooltip("灯光强度")]
        public float intensity = 1;

        [Tooltip("旋转角度以最短方向插值处理")]
        public bool interpolationShortDir = true;
    }
}
