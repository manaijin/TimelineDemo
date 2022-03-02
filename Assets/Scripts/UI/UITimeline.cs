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
    [SerializeField] private RawImage mask1;
    [SerializeField] private RawImage mask2;
    [SerializeField] private UIDialogue nodeDiglogue;
    #endregion

    public void Awake()
    {
        UIManager.Instance.RegistUI("UITimeline", this);
    }

    public void BlendMaskVideo(RenderTexture r1, RenderTexture r2, Rect[] uvs = null, Texture[] masks = null)
    {
        videoBlend.gameObject.SetActive(false);
        mask1.gameObject.SetActive(true);
        mask2.gameObject.SetActive(true);        
        video1.texture = r1;
        video2.texture = r2;
        if (uvs != null && uvs.Length >= 2)
        {
            video1.uvRect = uvs[0];
            video2.uvRect = uvs[1];
        }

        if (masks != null && masks.Length >= 2)
        {
            mask1.texture = masks[0];
            mask2.texture = masks[1];
        }
    }
}
