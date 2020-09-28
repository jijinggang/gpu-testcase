using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : BaseScript
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }



    // Update is called once per frame

    private string[] buttons =
    {
        "带宽测试",
        "GPU运算测试",
        "CPU测试"
    };
    void DealWith(int sel)
    {
        switch (sel)
        {
            case 0:
                gameObject.AddComponent<BandwidthTest>();
                break;
            case 1:
                gameObject.AddComponent<CommandBufferTest>();
                break;
            case 2:
                gameObject.AddComponent<CPUTest>();
                break;
        }
        Destroy(this);
    }

    protected override void _OnGUI()
    {
        base._OnGUI();
        GUI.skin.button.fontSize = 20;
        GUI.skin.label.fontSize = 20;

        var cx = Screen.width;
        var cy = Screen.height;
        var W = 300;
        var H = 80;

        int len = buttons.Length;
        var spanY = (cy - len * H) / (len + 1);
        var rect = new Rect((cx - W) / 2, spanY, W, H);
        for (int i = 0; i < len; i++)
        {
            rect.y = spanY + i * (spanY + H);
            if (GUI.Button(rect, buttons[i]))
            {
                DealWith(i);
            }
        }

    }
}
