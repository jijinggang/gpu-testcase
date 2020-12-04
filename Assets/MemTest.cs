using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;

public class MemTest : BaseScript
{

    private Tuple<string, Action>[] BTNS;
    private string status;


    // Start is called before the first frame update
    void Start()
    {
        BTNS = new Tuple<string, Action>[] {
            new Tuple<string, Action>("Create内存块4M", onCreateMem),
            new Tuple<string, Action>("创建小内存块4M", onCreateSmallMem),
            new Tuple<string, Action>("读取txt文件", onWebRequest),
            new Tuple<string, Action>("加载bundle&Asset", onLoadBundleAsset),
            new Tuple<string, Action>("初始化GameObject", onGameObjectInst),
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
        GUI.Label(rect, "status:" + status);

    }
    List<byte[]> mems = new List<byte[]>();
    private void onCreateMem()
    {
        int len = 4 * 1024 * 1024;
        var mem = createMem(len);

        Debug.Log(mem[len - 1]);
        mems.Add(mem);
        status = "create mem:" + mems.Count;
    }
    private byte[] createMem(int len)
    {
        var mem = new byte[len];
        for (int i = 0; i < len; i++)
            mem[i] = (byte)(i % 128);
        return mem;
    }
    private void onCreateSmallMem()
    {
        for(int i = 0; i < 1024; i++)
        {
            mems.Add(createMem(4 * 1024));
        }

        status = "create mem:" + mems.Count;
    }

    private int fileIndex = 0;
    private List<System.Object> temps = new List<System.Object>();
    private IEnumerator openStreamFile()
    {
        fileIndex++;
        status = "openfile:"+fileIndex;
        string path = Application.streamingAssetsPath + "/"+fileIndex.ToString()+".txt";
        UnityWebRequest uwr =  UnityWebRequest.Get(path);
        uwr.disposeCertificateHandlerOnDispose = true;
        uwr.disposeDownloadHandlerOnDispose = true;
        uwr.disposeUploadHandlerOnDispose = true;

        Debug.Log("read start");
        yield return uwr.SendWebRequest();

        if (uwr.isHttpError || uwr.isNetworkError)
            Debug.Log(uwr.error);
        else
        {
            var str = uwr.downloadHandler.data;
            temps.Add(str);
            Debug.Log(str.Length);

            //uwr.downloadHandler.Dispose();
        }
        uwr.Dispose();
    }
    private LinkedList<Texture2D> imgs = new LinkedList<Texture2D>(); 
    private void onWebRequest()
    {
        StartCoroutine(openStreamFile());
    }

    private void onLoadBundleAsset()
    {
        fileIndex++;
        status = "load assetbundle:" + fileIndex;
        string path = Application.streamingAssetsPath + "/" + fileIndex.ToString() + ".bundle";
        AssetBundle.SetAssetbundleEncryptionKey("tmskAtlantis20200518", "Atlantis20200518Tmsk");
        var ab = AssetBundle.LoadFromFile(path);
        var names = ab.GetAllAssetNames();

        foreach (var name in names)
        {
            var obj = ab.LoadAsset<Texture2D>(name);
            Debug.Log(name);
        }


 
    }
    private GameObject _img = null;
    private void onGameObjectInst()
    {
        if(_img == null)
            _img = Resources.Load<GameObject>("512");
        for (int i = 0; i < 100; i++)
        {
            GameObject.Instantiate(_img);
        }
        status = "init gameobject 100";
    }

}
