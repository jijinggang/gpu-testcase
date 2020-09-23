
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CommandBufferTest : MonoBehaviour
{

    private CommandBuffer commandBuffer = null;
    public RenderTexture[] renderTextures = null;
    // private Renderer targetRenderer = null;
    // public GameObject targetObject = null;
    // public Material replaceMaterial = null;

    void Start()
    {
        var quad = Resources.Load<GameObject>("Quad");
        GameObject.Instantiate(quad);
    }
    void OnEnable()
    {
        // targetRenderer = targetObject?.GetComponentInChildren<Renderer>();
        //申请RT
        renderTextures = new RenderTexture[100];
        for (int i = 0; i < 100; i++)
        {
            var renderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1);
            renderTextures[i] = renderTexture;
        }

        commandBuffer = new CommandBuffer();
        //设置Command Buffer渲染目标为申请的RT
        // commandBuffer.SetRenderTarget(renderTexture);
        //初始颜色设置为灰色
        // commandBuffer.ClearRenderTarget(true, true, Color.gray);
        //绘制目标对象，如果没有替换材质，就用自己的材质
        // Material mat = replaceMaterial == null ? targetRenderer.sharedMaterial : replaceMaterial;
        // commandBuffer.DrawRenderer(targetRenderer, mat);
        // //然后接受物体的材质使用这张RT作为主纹理
        // this.GetComponent<Renderer>().sharedMaterial.mainTexture = renderTexture;
        for (int j = 0; j < 0; j++)
        {

            commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, renderTextures[j]);
        }
        //直接加入相机的CommandBuffer事件队列中
        Camera.main.AddCommandBuffer(CameraEvent.AfterImageEffects, commandBuffer);
    }

    void Update()
    {

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
    int[] Blit_Count = { 0, 1, 2, 4, 10, 100 };
    int[] For_Count = { 0, 10, 20, 40 };
    int[] For_Count_KEY = { 0, 100, 1000, 10000 };

    private int blit_count_index = 0;
    private int for_count_index = 0;
    void OnGUI()
    {
        GUI.skin.button.fontSize = 20;
        GUI.skin.label.fontSize = 20;
        var writeBand = Screen.width * Screen.height * 4f / (1 << 20);

        if (GUI.Button(r1, "For" + For_Count[for_count_index]))
        {
            for_count_index = (for_count_index + 1) % For_Count.Length;
            for (int j = 0; j < For_Count.Length; j++)
            {
                Shader.DisableKeyword("_ALU" + For_Count_KEY[j]);
            }
            Shader.EnableKeyword("_ALU" + For_Count_KEY[for_count_index]);
        }

        if (GUI.Button(r2, "" + writeBand * Blit_Count[blit_count_index] + "MB"))
        {
            blit_count_index = (blit_count_index + 1) % Blit_Count.Length;

            commandBuffer.Clear();
            for (int i = 0; i < Blit_Count[blit_count_index]; i++)
            {
                commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, renderTextures[i]);
            }
        }
    }

    //也可以在OnPreRender中直接通过Graphics执行Command Buffer，不过OnPreRender和OnPostRender只在挂在相机的脚本上才有作用！！！
    //void OnPreRender()
    //{
    //    //在正式渲染前执行Command Buffer
    //    Graphics.ExecuteCommandBuffer(commandBuffer);
    //}

}
