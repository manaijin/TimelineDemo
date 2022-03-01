using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Timeline UI界面
/// </summary>
public class UITimeline : MonoBehaviour
{
    #region UI节点
    [SerializeField] private RawImage videoBlend;
    [SerializeField] private RawImage video1;
    [SerializeField] private RawImage video2;
    [SerializeField] private UIDialogue nodeDiglogue;
    #endregion

    public void Awake()
    {
        UIManager.Instance.RegistUI("UITimeline", this);
    }

    public void ShowTwoVideo(RenderTexture r1, RenderTexture r2)
    {
        videoBlend.gameObject.SetActive(false);
        video1.transform.parent.gameObject.SetActive(true);
        video2.transform.parent.gameObject.SetActive(true);
        video1.texture = r1;
        video2.texture = r2;
    }
}
