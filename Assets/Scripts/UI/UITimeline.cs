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

    public void BlendMask(RenderTexture r1, RenderTexture r2, float blendWeight1, float blendWeight2)
    {
        videoBlend.gameObject.SetActive(true);
        mask1.gameObject.SetActive(false);
        mask2.gameObject.SetActive(false);
        var mat = videoBlend.material;
        mat.SetTexture("_MainTex", r1);
        mat.SetTexture("_MainTex2", r2);
        mat.SetFloat("_BlendParam", blendWeight1);
        mat.SetFloat("_BlendParam2", blendWeight2);
    }

    public void SetBlendWeight(float blendWeight1, float blendWeight2)
    {
        var mat = videoBlend.material;
        mat.SetFloat("_BlendParam", blendWeight1);
        mat.SetFloat("_BlendParam2", blendWeight2);
    }

    public void MaskVideo(RenderTexture r1, RenderTexture r2, Texture[] masks = null)
    {
        videoBlend.gameObject.SetActive(false);
        mask1.gameObject.SetActive(true);
        mask2.gameObject.SetActive(true);
        video1.texture = r1;
        video2.texture = r2;

        if (masks != null && masks.Length >= 2)
        {
            mask1.texture = masks[0];
            mask2.texture = masks[1];
        }
    }
}
