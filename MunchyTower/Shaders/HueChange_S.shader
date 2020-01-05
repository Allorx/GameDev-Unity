Shader "Custom/HueChange_S"
{
    Properties
    {   
        _Hue("Hue", float) = 0
        _Saturation("Saturation", float) = 0.5
        _Value("Value", Range(0, 1)) = 0.5
        _Contrast("Contrast", Range(0, 1)) = 0.5
    }
    
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Tags { "Queue" = "Transparent" }
        GrabPass{ "_GrabTexture" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            sampler2D _GrabTexture;
            float _Hue, _Saturation, _Value, _Contrast;

            // Function
            inline float3 applyHue(float3 aColor, float aHue)
            {
                float angle = radians(aHue);
                float3 k = float3(0.57735, 0.57735, 0.57735);
                float cosAngle = cos(angle);
                //Rodrigues' rotation formula
                return aColor * cosAngle + cross(k, aColor) * sin(angle) + k * dot(k, aColor) * (1 - cosAngle);
            }
 
 
            inline float4 applyHSVEffect(float4 startColor, fixed4 hsvc)
            {
                float hue = 360 * hsvc.r;
                float saturation = hsvc.g * 2;
                float value = hsvc.b * 2 - 1;
                float contrast = hsvc.a * 2;
 
                float4 outputColor = startColor;
                outputColor.rgb = applyHue(outputColor.rgb, hue);
                outputColor.rgb = (outputColor.rgb - 0.5) * contrast + 0.5 + value;
                outputColor.rgb = lerp(Luminance(outputColor.rgb), outputColor.rgb, saturation);
                 
                return outputColor;
            }

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
                o.uv = ComputeGrabScreenPos(o.vertex);
                return o;
            }

 
            fixed4 frag(v2f i) : SV_Target
            {   
                float4 hsvc = float4(_Hue, _Saturation, _Value, _Contrast);
                float4 startColor = tex2D(_GrabTexture, i.uv);
                float4 hsvColor = applyHSVEffect(startColor, hsvc);

                return hsvColor;
            }
            ENDCG
        }
    }
}
