Shader "Unlit/CameraRegionMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Region ("Region (X, Y, Width, Height)", Vector) = (0.25, 0.25, 0.5, 0.5)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Region;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                if (i.uv.x < _Region.x || i.uv.x > (_Region.x + _Region.z) ||
                    i.uv.y < _Region.y || i.uv.y > (_Region.y + _Region.w))
                {
                    return float4(0, 0, 0, 1); 
                }

                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
