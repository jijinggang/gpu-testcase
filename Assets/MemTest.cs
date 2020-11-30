using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;

public class MemTest : BaseScript
{

    private Tuple<string, Action>[] BTNS;


    // Start is called before the first frame update
    void Start()
    {
        BTNS = new Tuple<string, Action>[] {
            new Tuple<string, Action>("读取文件测试", onReadFile),
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
        var rect = new Rect(10, 10, 300, 70);
        foreach(var btn in BTNS)
        {
            if (GUI.Button(rect, btn.Item1))
            {
                btn.Item2();
            }
            rect.y += 70;
        }
        GUI.Label(rect, "imgs:" + imgs.Count);

    }

    private IEnumerator openStreamFile()
    {
        string path = Application.streamingAssetsPath + "/1.txt";
        UnityWebRequest uwr =  UnityWebRequest.Get(path);
        Debug.Log("read start");
        yield return uwr.SendWebRequest();

        if (uwr.isHttpError || uwr.isNetworkError)
            Debug.Log(uwr.error);
        else
        {
            Debug.Log(uwr.downloadHandler.data.Length);
        }
        
    }
    private LinkedList<Texture2D> imgs = new LinkedList<Texture2D>(); 
    private void onReadFile()
    {
        StartCoroutine(openStreamFile());
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
