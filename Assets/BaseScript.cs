using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    int fps = 0;
    int power = 0;
    // Start is called before the first frame update

    float _lastTime = -1f;
    float _frames = 0;

    // protected int second = 0;

    void Update()
    {
        _Update();
    }
    // Update is called once per frame
    protected virtual void _Update()
    {
        if (_lastTime < 0)
        {
            _lastTime = Time.realtimeSinceStartup;
        }
        var now = Time.realtimeSinceStartup;
        var diff = now - _lastTime;
        if (diff > 1)
        {
            power = Utils.GetElectricity();
            fps = Mathf.RoundToInt(_frames / diff);
            _frames = 0;
            _lastTime = now;
            // Debug.Log("upda");
            OnSecondTick();
        }
        _frames++;
    }

    protected virtual void OnSecondTick()
    {
        // ++second;
    }

    private Rect _rectStatus = new Rect(0, 0, 300, 30);
    void OnGUI()
    {
        _OnGUI();
    }
    protected virtual void _OnGUI()
    {
        _rectStatus.y = Screen.height - _rectStatus.height;
        GUI.Label(_rectStatus, "fps:" + fps + " power:" + power + " mem:"+Utils.GetMemory());
    }
}
