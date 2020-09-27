using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{


    private static AndroidJavaObject __currActivity = null;
    private static AndroidJavaObject currActivity
    {
        get
        {

            if (__currActivity == null)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                __currActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return __currActivity;
        }
    }
    //获取实时电流参数
    static public int GetElectricity()
    {
        var active = currActivity;
        if (currActivity == null)
            return -1;
       
        AndroidJavaObject manager = active.Call<AndroidJavaObject>("getSystemService", new object[] { "batterymanager" });
        object[] parm = new object[] { 2 };
        int current = manager.Call<int>("getIntProperty", parm);
        return current;
    }

}