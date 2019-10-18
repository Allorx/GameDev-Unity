Shader "Custom/ScreenSpaceColour_S"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CutoutAlpha ("Cutout Alpha", range(0,1)) = 0.5
        _Colour1 ("Top Colour", Color) = (1,1,1,1)
        _Colour2 ("Bot Colour", Color) = (1,1,1,1)
        _Colour3 ("Left Colour", Color) = (1,1,1,1)
        _Colour4 ("Right Colour", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct appdata members screenPos)
            #pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 scrPos : TEXCOORD1; 
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _CutoutAlpha;
            float4 _Colour1; 
            float4 _Colour2;
            float4 _Colour3;
            float4 _Colour4;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.scrPos = ComputeScreenPos(o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 screenPos = i.scrPos.xy;
                fixed4 col = tex2D(_MainTex, i.uv) * lerp(_Colour2,_Colour1,screenPos.y) * lerp(_Colour3,_Colour4,screenPos.x);
                fixed cutoutVal = tex2D(_MainTex, i.uv).r;

                if(cutoutVal < _CutoutAlpha){
                    discard;
                }

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
