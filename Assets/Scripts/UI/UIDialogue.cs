using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 剧情对话UI
/// </summary>
public class UIDialogue : MonoBehaviour
{
    #region UI节点
    [SerializeField] private RawImage imgBg;
    [SerializeField] private Button btnSkip;
    [SerializeField] private Text txtDialogue;
    #endregion

    private List<string> disloghueList = new List<string>();
    private float duration = 0.5f;
    private float dt = 0f;
    private bool isStopping = false;

    private void Start()
    {
        txtDialogue.text = string.Empty;
    }

    private void Update()
    {        
        if (disloghueList.Count == 0) return;
        var content = disloghueList[0];
        var length = txtDialogue.text.Length;
        if (content.Length <= length && !isStopping)
        {
            disloghueList.RemoveAt(0);
            txtDialogue.CrossFadeAlpha(0, 2, false);
            isStopping = true;
            return;
        }
        dt += Time.deltaTime;
        if (dt < duration) return;
        if (isStopping)
        {
            isStopping = false;
            txtDialogue.text = string.Empty;
            txtDialogue.CrossFadeAlpha(1, 0, false);
            dt = 0f;
            return;
        }
        dt = 0f;
        txtDialogue.text = content.Substring(0, length + 1);
    }

    public void AddDialogue(string str)
    {
        disloghueList.Add(str);
    }
}
