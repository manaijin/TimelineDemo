using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteAlways]
/// <summary>
/// UI节点不随父节点变化
/// </summary>
public class DonotChangeChild : MonoBehaviour
{
    public List<Transform> orT = new List<Transform>();
    public List<Vector3> orPos = new List<Vector3>();
    public List<Vector3> orScale = new List<Vector3>();// 需要注意scale为0的处理
    public List<Quaternion> orRotation = new List<Quaternion>();

    protected void OnEnable()
    {
        var result = transform.GetComponentsInChildren<Transform>();
        foreach (var tr in result)
        {
            if (tr == transform)
                continue;
            orT.Add(tr);
            orPos.Add(tr.position);
            orRotation.Add(tr.rotation);
            var scale1 = transform.localScale;
            var scale2 = tr.localScale;
            orScale.Add(new Vector3(scale1.x * scale2.x, scale1.y * scale2.y, scale1.z * scale2.z));
        }
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            ResetChildTransform();
        }
    }

    public void ResetChildTransform()
    {
        for (int i = 0; i < orT.Count; i++)
        {
            var tr = orT[i];
            tr.position = orPos[i];
            tr.rotation = orRotation[i];
            float localX = transform.localScale.x;
            float localY = transform.localScale.y;
            float localZ = transform.localScale.z;
            if (localX == 0 || localY == 0 || localZ == 0)
                return;

            float x = orScale[i].x / localX;
            float y = orScale[i].y / localY;
            float z = orScale[i].z / localZ;
            tr.localScale = new Vector3(x, y, z);
        }
    }
}
