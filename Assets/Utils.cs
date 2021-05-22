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
#if !UNITY_ANDROID || UNITY_EDITOR
            return null;
#else


            if (__currActivity == null)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                __currActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return __currActivity;
#endif
        }
    }
    //获取实时电流参数
    static public int GetElectricity()
    {
#if !UNITY_ANDROID || UNITY_EDITOR
        return -1;
#else

        var active = currActivity;
        if (active == null)
            return -1;

        AndroidJavaObject manager = active.Call<AndroidJavaObject>("getSystemService", new object[] { "batterymanager" });
        object[] parm = new object[] { 2 };
        int current = manager.Call<int>("getIntProperty", parm);
        return current;
#endif
    }
/*
    struct MemInfo
    {
        public int dalvikPrivateDirty;
        public int dalvikPss;
        public int dalvikSharedDirty;
        public int nativePrivateDirty;
        public int nativePss;
        public int nativeSharedDirty;
        public int otherPrivateDirty;
        public int otherPss;
        public int otherSharedDirty;


        int TotalPrivateClean;
        int TotalPrivateDirty;
        int TotalPss;
        int TotalSharedClean;
        int TotalSharedDirty;
        int TotalSwappablePss;
    };
    */
    static AndroidJavaObject _jo = null;
    static public int GetMemory()
    {
#if  !UNITY_ANDROID || UNITY_EDITOR
        return -1;
#else

        if(_jo == null)
            _jo = new AndroidJavaObject("com.test.JavaAgent");

        var active = currActivity;
        if (active == null)
            return -1;
        var mem = _jo.CallStatic<AndroidJavaObject>("GetMemInfo", active);
        return mem.Call<int>("getTotalPss");
#endif
    }

}