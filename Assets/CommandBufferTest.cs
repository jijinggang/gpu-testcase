
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CommandBufferTest : BaseScript
{

    private CommandBuffer commandBuffer = null;
    public RenderTexture[] renderTextures = null;

    void Start()
    {
        var quad = Resources.Load<GameObject>("Quad");
        GameObject.Instantiate(quad);
    }
    void OnEnable()
    {
        renderTextures = new RenderTexture[100];
        for (int i = 0; i < 100; i++)
        {
            var renderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1);
            renderTextures[i] = renderTexture;
        }

        commandBuffer = new CommandBuffer();
        Camera.main.AddCommandBuffer(CameraEvent.AfterImageEffects, commandBuffer);
    }

    protected override void _Update()
    {
        base._Update();

    }

    protected override void OnSecondTick()
    {
        // Debug.Log("xxx");
        if (forAutoIncrease)
        {
            forSecond++;
            Shader.SetGlobalFloat("_LoopCount", forSecond);
        }

        if (grabAutoIncrease && grabSecond < renderTextures.Length)
        {
            grabSecond++;
            commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, renderTextures[grabSecond]);
        }
    }


    void OnDisable()
    {
        //移除事件，清理资源
        Camera.main.RemoveCommandBuffer(CameraEvent.AfterImageEffects, commandBuffer);
        commandBuffer.Clear();
        for (int i = 0; i < 100; i++)
        {

            renderTextures[i].Release();
        }
    }

    private Rect r1 = new Rect(0, 200, 200, 80);
    private Rect r2 = new Rect(200, 200, 200, 80);
    private bool forAutoIncrease = false;
    private bool grabAutoIncrease = false;
    private int forSecond = 0;
    private int grabSecond = 0;

    protected override void _OnGUI()
    {
        base._OnGUI();
        GUI.skin.button.fontSize = 20;
        GUI.skin.label.fontSize = 20;
        var writeBand = Screen.width * Screen.height * 4f / (1 << 20);

        if (GUI.Button(r1, "For" + forSecond.ToString()))
        {
            forAutoIncrease = !forAutoIncrease;
        }

        if (GUI.Button(r2, "" + writeBand * grabSecond + "MB"))
        {
            grabAutoIncrease = !grabAutoIncrease;
        }
    }


}
