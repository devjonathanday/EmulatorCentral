﻿Shader "Unlit/ScrollingShader"
{
    Properties
    {
        [PerRendererData]
        _MainTex ("Texture", 2D) = "white" {}
        _Tint ("Tint", Color) = (0,0,0,0)
        _ScrollX ("Scroll Speed X", Float) = 0
        _ScrollY ("Scroll Speed Y", Float) = 0
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Tint;
            float _ScrollX;
            float _ScrollY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.x -= (_ScrollX * _Time);
                o.uv.y -= (_ScrollY * _Time);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Tint;
                return col;
            }
            ENDCG
        }
    }
}
