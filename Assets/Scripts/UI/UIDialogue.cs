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

    private List<string> disloghueList = new List<string>() { "可以逃跑,可以哭泣,但不可放弃", "别害怕，抬起头来，因为你并没有做错什么" };
    private float duration = 1f;
    private float dt = 0f;

    private void Start()
    {
        txtDialogue.text = string.Empty;
    }

    private void Update()
    {
        var length = txtDialogue.text.Length;
        if (disloghueList.Count == 0) return;
        var content = disloghueList[0];
        if (content.Length <= length)
        {
            disloghueList.RemoveAt(0);
            txtDialogue.text = string.Empty;
            return;
        }
        dt += Time.deltaTime;
        if (dt < duration) return;
        dt = 0f;
        txtDialogue.text = content.Substring(0, length + 1);
    }

    public void AddDialogue(string str)
    {
        disloghueList.Add(str);
    }
}
