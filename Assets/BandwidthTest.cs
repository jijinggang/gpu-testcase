using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandwidthTest : BaseScript
{

    // Start is called before the first frame update
    GameObject prefab512;
    Color32 COLOR = new Color32(255, 0, 0, 10);

    int image_index = 0;
    int output_index = 0;
    int format_index = 0;
    private Rect rectBtnImage = new Rect(0, 0, 200, 80);
    private Rect rectBtnFormat = new Rect(220, 0, 200, 80);
    private Rect rectBtnOutput = new Rect(440, 0, 200, 80);

    private Rect rectBtnCreate = new Rect(700, 0, 80, 80);
    private Rect rectBtnCreate1 = new Rect(800, 0, 80, 80);
    private Rect rectBtnClear = new Rect(900, 0, 80, 80);
    private Rect rectStatus = new Rect(0, 80, 300, 50);
    void Start()
    {
        Application.targetFrameRate = 60;
        prefab512 = Resources.Load<GameObject>("512");

        //        style.fontSize = 20;

        rectBtnClear.x = Screen.width - rectBtnCreate.width;

    }


    private List<GameObject> objects = new List<GameObject>();
    // Update is called once per frame
    protected override void _Update()
    {
        base._Update();
    }


    int[] IMAGE_SIZES = { 256, 512, 1024, 2048 };
    int[] OUTPUT_SIZES = { 128, 256, 512, 1024 };
    TextureFormat[] IMAGE_FORMATS = { TextureFormat.ARGB4444, TextureFormat.ARGB32, TextureFormat.ASTC_4x4, TextureFormat.ASTC_6x6, TextureFormat.ASTC_8x8, TextureFormat.ASTC_HDR_8x8 };
    string[] IMAGE_FORMATS_STR = { "ARGB4444", "ARGB32", "ASTC_4x4", "ASTC_6x6", "ASTC_8x8", "ASTC_HDR_8x8" };

    private void CreateObjects(int imageSize, int outputSize, TextureFormat format, int count = 100)
    {
        var COLORS = new Color32[imageSize * imageSize];

        for (int i = 0; i < COLORS.Length; i++)
        {
            byte r = (byte)Random.Range(0, 255);
            COLORS[i] = new Color32(r, 0, 0, 10);
        }


        float z = 0f;
        for (int i = 0; i < count; i++)
        {
            COLORS[0].r = (byte)Random.Range(0, 255);
            COLORS[0].g = (byte)Random.Range(0, 255);

            GameObject go = null;
            var texture = new Texture2D(imageSize, imageSize, format, false);
            if (format == TextureFormat.ARGB32)
            {
                texture.SetPixels32(COLORS);
                texture.Apply(false,true);
            }
            //texture.SetPixels32(COLORS);
            go = GameObject.Instantiate(prefab512);
            var sp = go.GetComponent<SpriteRenderer>();
            Sprite spt = Sprite.Create(texture, new Rect(0, 0, imageSize, imageSize), new Vector2(0.5f, 0.5f));
            sp.sprite = spt;
            float scale = 1.0f * outputSize / imageSize;
            sp.transform.localScale = new Vector3(scale, scale, 1);


            go.transform.Translate(0, 0, z);
            z += 0.01f;

            objects.Add(go);
        }
    }
    private void Clear()
    {
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects.Clear();
    }

    protected override void _OnGUI()
    {
        base._OnGUI();
        if (GUI.Button(rectBtnImage, "图片:" + IMAGE_SIZES[image_index]))
        {
            image_index = (image_index + 1) % IMAGE_SIZES.Length;
        }
        if (GUI.Button(rectBtnOutput, "输出:" + OUTPUT_SIZES[output_index]))
        {
            output_index = (output_index + 1) % OUTPUT_SIZES.Length;
        }

        if (GUI.Button(rectBtnFormat, "格式:" + IMAGE_FORMATS_STR[format_index]))
        {
            format_index = (format_index + 1) % IMAGE_FORMATS.Length;
        }

        if (GUI.Button(rectBtnCreate, "Create"))
        {
            CreateObjects(IMAGE_SIZES[image_index], OUTPUT_SIZES[output_index], IMAGE_FORMATS[format_index]);
        }
        if (GUI.Button(rectBtnCreate1, "Create1"))
        {
            CreateObjects(IMAGE_SIZES[image_index], OUTPUT_SIZES[output_index], IMAGE_FORMATS[format_index],1);
        }

        if (GUI.Button(rectBtnClear, "Clear"))
        {
            Clear();
        }


        string status = "objects=" + objects.Count;
        GUI.Label(rectStatus, status);

    }

}
