Shader "Custom/Lit"
{
    Properties
    {
        [Toggle] _UseLambert ("Use Lambert", Float) = 1
        [Toggle] _UseAmbient ("Use Ambient", Float) = 1
        [Toggle] _UseSpecular ("Use Specular", Float) = 1

        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}

        _SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
        _Shininess ("Shininess", Range(0, 100)) = 16
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "LightMode" = "UniversalForward" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : TEXCOORD1;
                float3 viewDirWS : TEXCOORD2;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                float _UseLambert;
                float _UseAmbient;
                float _UseSpecular;

                half4 _BaseColor;
                float4 _BaseMap_ST;

                half4 _SpecularColor;
                float _Shininess;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.viewDirWS = normalize(GetCameraPositionWS() - TransformObjectToWorld(IN.positionOS.xyz));
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half3 finalColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv) * _BaseColor;

                half3 normalWS = SafeNormalize(IN.normalWS);
                Light mainLight = GetMainLight();
                half3 lightDir = normalize(mainLight.direction);

                if (_UseLambert == 1)
                {
                    float nDotL = saturate(dot(normalWS, lightDir));
                    finalColor += _BaseColor.rgb * mainLight.color.rgb * nDotL;
                }

                if (_UseAmbient == 1)
                {
                    finalColor += SampleSH(normalWS) * _BaseColor;
                }

                if (_UseSpecular)
                {
                    half3 reflectDir = (-lightDir, normalWS);

                    half3 viewDir = normalize(IN.viewDirWS);
                    half specFactor = pow(saturate(dot(reflectDir, viewDir)), _Shininess);

                    finalColor += _SpecularColor.rgb * specFactor;
                }

                return half4(finalColor, 1);
            }
            ENDHLSL
        }
    }
}
