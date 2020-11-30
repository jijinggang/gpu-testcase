package com.test;
import android.content.Context;
import android.app.ActivityManager;
import java.util.*;
import android.os.Debug;
public class JavaAgent{
    public static Debug.MemoryInfo GetMemInfo(Context ctx){
         String pkgName = ctx.getPackageName();
         ActivityManager mActivityManager =  (ActivityManager) ctx.getSystemService(Context.ACTIVITY_SERVICE);
        int[] pidArray = new int[] { android.os.Process.myPid() };
        Debug.MemoryInfo[] memoryInfo = mActivityManager.getProcessMemoryInfo(pidArray);
        return memoryInfo[0];
        //int temp = memoryInfo[0].getTotalPss() >> 10;  // 除1024
        //return temp;
    }
}