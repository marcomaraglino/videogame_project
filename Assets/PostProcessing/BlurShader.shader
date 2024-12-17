Shader "Custom/UIBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0,10)) = 0
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
            float _BlurSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 texelSize = float2(1.0 / _ScreenParams.x, 1.0 / _ScreenParams.y);
                fixed4 col = fixed4(0,0,0,0);
                
                for(int x = -4; x <= 4; x++)
                {
                    for(int y = -4; y <= 4; y++)
                    {
                        col += tex2D(_MainTex, i.uv + float2(x,y) * texelSize * _BlurSize);
                    }
                }
                
                col /= 81; // 9x9 kernel
                return col;
            }
            ENDCG
        }
    }
}
