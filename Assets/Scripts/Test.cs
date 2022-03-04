using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var img = GetComponent<RawImage>();
        img.CrossFadeAlpha(0, 1, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
