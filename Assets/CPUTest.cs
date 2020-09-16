using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUTest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }


    private int fps = 0;
    // Update is called once per frame
    float[] deltaTimes = new float[10] ;
    int deltaIndex = 0;
    void Update()
    {
        int len = deltaTimes.Length;
        deltaIndex = (deltaIndex+1) % len;
        deltaTimes[deltaIndex] = Time.deltaTime;

        var sum = 0f;
        for (int i = 0; i < len; i++)
            sum += deltaTimes[i];

        fps = Mathf.RoundToInt(len / sum);

        cputest();
    }
    int cpu_count = 0;
    private void cputest()
    {
            float sum = 0f;
            for(int i=0; i < cpu_count; i++)
            {
                sum += Mathf.Log(i);
            }
    }



    private Rect rectBtnCreate = new Rect(0, 0, 200, 80);

    private Rect rectBtnClear = new Rect(800, 0, 80, 80);
    private Rect rectStatus = new Rect(0, 80, 300, 50);

    void OnGUI()
    {
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


        string status = "objects=" + cpu_count + ",fps=" + fps;
        GUI.Label(rectStatus, status);

    }

}
