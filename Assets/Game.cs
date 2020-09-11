using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    string[] TEST_NAME =
    {
        "1. 1000个512透明图片",
        "2. 1000个128透明图片",
        "3. 1000个128透明图片并放大4倍",
        "4. 1000个用2张512透明图片交叉",
    };
    // Start is called before the first frame update
    GameObject prefab512, prefab512_2, prefab128;
    int index = 0;
    int count = 0;
    void Start()
    {
        prefab512 = Resources.Load<GameObject>("512");
        prefab512_2 = Resources.Load<GameObject>("512-2");
        prefab128 = Resources.Load<GameObject>("128");
    }

    private Rect rectBtnSwitch = new Rect(0, 0, 300, 80);
    private Rect rectBtnCreate = new Rect(350, 0, 80, 80);
    private Rect rectStatus = new Rect(450, 0, 300, 20);
    private int fps = 0;
    // Update is called once per frame
    void Update()
    {
        fps = Mathf.RoundToInt(1 / Time.deltaTime);
    }
    private void CreateObjs(int index)
    {
        float z = 0f;
        for (int i = 0; i < 100; i++)
        {
            GameObject go = null;
            if (index == 0)
                go = Instantiate(prefab512);
            else if (index == 1)
                go = Instantiate(prefab128);
            else if (index == 2)
            {
                go = Instantiate(prefab128);
                go.transform.localScale = new Vector3(4, 4, 1);
            }
            else if (index == 3)
            {
                if (i % 2 == 0)
                    go = Instantiate(prefab512);
                else
                    go = Instantiate(prefab512_2);
            }
            go.transform.Translate(0, 0, z);
            z += 0.01f;

            count++;
        }
    }
    void OnGUI()
    {

        if (GUI.Button(rectBtnSwitch, TEST_NAME[index] + ":"))
        {
            SelectNext();
        }
        if (GUI.Button(rectBtnCreate, "Create"))
        {
            CreateObjs(index);
        }
        string status = "objects=" + count + ",fps=" + fps;
        GUI.Label(rectStatus, status);

    }
    private void Reset()
    {
    }
    private void SelectNext()
    {
        var len = TEST_NAME.Length;
        index++;
        if (index >= len)
        {
            index = 0;
        }
    }
}
