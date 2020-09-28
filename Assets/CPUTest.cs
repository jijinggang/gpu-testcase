using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CPUTest : BaseScript
{

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            var start = new ThreadStart(ThreadProc);
            var t = new Thread(start);
            t.Start();
        }
    }

    void ThreadProc()
    {
        while (true)
        {
            cputest();

            Thread.Sleep(16);
        }
    }


    protected override void _Update()
    {
        base._Update();
        //cputest();
    }
    volatile int cpu_count = 0;
    private void cputest()
    {
        float sum = 0f;
        for (int i = 0; i < cpu_count; i++)
        {
            sum += Mathf.Log(i);
        }
        //Debug.Log("1111");
    }



    private Rect rectBtnCreate = new Rect(0, 0, 200, 80);

    private Rect rectBtnClear = new Rect(800, 0, 80, 80);
    private Rect rectStatus = new Rect(0, 80, 300, 50);

    protected override void _OnGUI()
    {
        base._OnGUI();
        GUI.skin.button.fontSize = 20;
        GUI.skin.label.fontSize = 20;

        if (GUI.Button(rectBtnCreate, "Create"))
        {
            cpu_count += 10000;
            return;
        }
        if (GUI.Button(rectBtnClear, "Clear"))
        {
            cpu_count = 0;
            return;
        }


        string status = "objects=4*" + cpu_count;
        GUI.Label(rectStatus, status);

    }

}
