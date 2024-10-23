Shader "Ciconia Studio/Double Sided/Transparent/AWDW" {
    Properties {
        _Diffusecolor ("Diffuse Color", Color) = (1,1,1,1)
        _DiffuseMapCutoutA ("Diffuse Map (Cutout A)", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
        _NormalIntensity ("Normal Intensity", Range(0, 2)) = 1
        _SpecularMap ("Specular Map", 2D) = "white" {}
        _Speccolor ("Spec Color", Color) = (1,1,1,1)
        _SpecIntensity ("Spec Intensity", Range(0, 2)) = 0.5
        _Gloss ("Gloss", Range(0, 1)) = 0.3
    }

    SubShader {
        Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" "RenderPipeline" = "UniversalRenderPipeline" }
        LOD 200

        Pass {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }
            Cull Off
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha
            AlphaTest Greater [_Cutoff]

            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            
            // Используемые текстуры и переменные
            TEXTURE2D(_DiffuseMapCutoutA);
            SAMPLER(sampler_DiffuseMapCutoutA);
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            
            float4 _Diffusecolor;
            float _Cutoff;
            float _NormalIntensity;

            struct Attributes {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float3 normalWS    : TEXCOORD1;
            };

            Varyings vert(Attributes IN) {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target {
                // Получение цвета текстуры
                float4 baseColor = SAMPLE_TEXTURE2D(_DiffuseMapCutoutA, sampler_DiffuseMapCutoutA, IN.uv) * _Diffusecolor;

                // Получение нормалей
                float3 normalMap = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, IN.uv)) * _NormalIntensity;

                // Альфа-клип (отсечение)
                clip(baseColor.a - _Cutoff);

                // Возвращаем результат (на данном этапе просто базовый цвет)
                return baseColor;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
