﻿Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #pragma multi_compile _ _ALU100 _ALU1000 _ALU10000

            #include "UnityCG.cginc"

            float _LoopCount;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float hash12(half2 p)
{
	float3 p3  = frac(float3(p.xyx) * .1031);
    p3 += dot(p3, p3.yzx + 33.33);
    return frac((p3.x + p3.y) * p3.z);
}

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
                float4 c = hash12(i.uv * _ScreenParams.xy + _Time.xy);
                for( int k = 0; k < _LoopCount; ++k)
                {
                    float c2 = hash12(i.uv * _ScreenParams.xy + _Time.xy);
                    float c3 = hash12(i.uv * _ScreenParams.xy + _Time.xx);
                    float c4 = hash12(i.uv * _ScreenParams.xy + _Time.xx + half2(0.1, 2));
                    c += pow(c, c2);
                    c -= sin(c * c3) * 0.8;
                }
                c /= _LoopCount;
                c.a = 0.1;
                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return c;
            }
            ENDCG
        }
    }
}
