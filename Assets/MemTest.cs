using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MemTest : BaseScript
{

    private Tuple<string, Action>[] BTNS;


    // Start is called before the first frame update
    void Start()
    {
        BTNS = new Tuple<string, Action>[] {
            new Tuple<string, Action>("ResourcesLoad100", onResourceLoad),
            new Tuple<string, Action>("LoadAndInst100", onLoadAndInstantiate),
        };
    }

    // Update is called once per frame
    protected override void _Update()
    {
        base._Update();
    }

    protected override void _OnGUI()
    {
        base._OnGUI();
        var rect = new Rect(10, 10, 200, 50);
        foreach(var btn in BTNS)
        {
            if (GUI.Button(rect, btn.Item1))
            {
                btn.Item2();
            }
            rect.y += 50;
        }
        GUI.Label(rect, "imgs:" + imgs.Count);

    }

    private LinkedList<Texture2D> imgs = new LinkedList<Texture2D>(); 
    private void onResourceLoad()
    {
        for (int i = 0; i < 100; i++)
        {
            var texture = new Texture2D(512, 512, TextureFormat.ARGB32, false);
            texture.Apply(false, true);
            imgs.AddLast(texture);
        }
    }
    private void onLoadAndInstantiate()
    {
        for (int i = 0; i < 100; i++)
        {
            var img = Resources.Load<GameObject>("512");
            GameObject.Instantiate(img);
        }
    }
}
