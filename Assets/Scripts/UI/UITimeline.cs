using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Timeline UI逻辑组件
/// </summary>
public class UITimeline : MonoBehaviour
{
    #region UI节点
    [SerializeField] private RawImage videoBlend;
    [SerializeField] private RawImage video1;
    [SerializeField] private RawImage video2;
    [SerializeField] private UIDialogue nodeDiglogue;
    #endregion
}
