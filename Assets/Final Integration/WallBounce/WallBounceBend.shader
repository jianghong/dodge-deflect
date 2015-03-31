// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1397059,fgcg:0.1397059,fgcb:0.1397059,fgca:1,fgde:1,fgrn:0.59,fgrf:1.02,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1752,x:32921,y:32617,varname:node_1752,prsc:2|emission-5187-OUT,alpha-6513-OUT,voffset-5301-OUT;n:type:ShaderForge.SFN_Panner,id:6373,x:31445,y:33039,varname:node_6373,prsc:2,spu:0.101,spv:0|UVIN-2735-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:9287,x:31602,y:33039,varname:node_9287,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-6373-UVOUT;n:type:ShaderForge.SFN_Subtract,id:1712,x:31941,y:33039,varname:node_1712,prsc:2|A-5653-OUT,B-6562-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6562,x:31833,y:33328,ptovrint:False,ptlb:Subtract Value,ptin:_SubtractValue,varname:_SubtractValue,prsc:2,glob:False,v1:0.05;n:type:ShaderForge.SFN_Abs,id:5222,x:32101,y:33039,varname:node_5222,prsc:2|IN-1712-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9985,x:32101,y:33180,ptovrint:False,ptlb:Multiply Value,ptin:_MultiplyValue,varname:_MultiplyValue,prsc:0,glob:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:9548,x:32269,y:33131,varname:node_9548,prsc:2|A-5222-OUT,B-9985-OUT;n:type:ShaderForge.SFN_Power,id:1885,x:32438,y:33131,varname:node_1885,prsc:2|VAL-9548-OUT,EXP-2591-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2591,x:32269,y:33278,ptovrint:False,ptlb:Vertex Bulge,ptin:_VertexBulge,varname:_VertexBulge,prsc:0,glob:False,v1:4.5;n:type:ShaderForge.SFN_NormalVector,id:4787,x:32438,y:33426,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:5301,x:32634,y:33234,varname:node_5301,prsc:2|A-1885-OUT,B-2845-OUT,C-4787-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2845,x:32438,y:33322,ptovrint:False,ptlb:Multiply Value2,ptin:_MultiplyValue2,varname:_MultiplyValue2,prsc:0,glob:False,v1:2;n:type:ShaderForge.SFN_Sin,id:5653,x:31771,y:33039,varname:node_5653,prsc:2|IN-9536-OUT;n:type:ShaderForge.SFN_Multiply,id:5187,x:32694,y:32838,varname:node_5187,prsc:2|A-4097-OUT,B-739-OUT;n:type:ShaderForge.SFN_Slider,id:739,x:32353,y:32898,ptovrint:False,ptlb:Emission Strength,ptin:_EmissionStrength,varname:_EmissionStrength,prsc:1,min:0,cur:1,max:1;n:type:ShaderForge.SFN_TexCoord,id:2735,x:31275,y:33039,varname:node_2735,prsc:2,uv:0;n:type:ShaderForge.SFN_Slider,id:6513,x:32895,y:33112,ptovrint:False,ptlb:Alpha (Opacity),ptin:_AlphaOpacity,varname:_AlphaOpacity,prsc:0,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:3528,x:32227,y:32310,ptovrint:False,ptlb:Emission Colour,ptin:_EmissionColour,varname:_EmissionColour,prsc:1,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:6262,x:32227,y:32524,ptovrint:False,ptlb:Emission Colour 2,ptin:_EmissionColour2,varname:_EmissionColour2,prsc:1,glob:False,c1:0.9044118,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:4097,x:32510,y:32726,varname:node_4097,prsc:2|A-3528-RGB,B-6262-RGB,T-357-OUT;n:type:ShaderForge.SFN_Sin,id:357,x:32227,y:32714,varname:node_357,prsc:2|IN-5845-OUT;n:type:ShaderForge.SFN_Time,id:7432,x:31892,y:32742,varname:node_7432,prsc:1;n:type:ShaderForge.SFN_Multiply,id:5845,x:32063,y:32714,varname:node_5845,prsc:2|A-3096-OUT,B-7432-T;n:type:ShaderForge.SFN_Time,id:5069,x:31445,y:32828,varname:node_5069,prsc:1;n:type:ShaderForge.SFN_Multiply,id:9536,x:31606,y:32778,varname:node_9536,prsc:2|A-7775-OUT,B-5069-T,C-9287-OUT;n:type:ShaderForge.SFN_Slider,id:7775,x:31346,y:32681,ptovrint:False,ptlb:Shakey Sine Strength,ptin:_ShakeySineStrength,varname:_ShakeySineStrength,prsc:0,min:-50,cur:0,max:50;n:type:ShaderForge.SFN_Slider,id:3096,x:31735,y:32607,ptovrint:False,ptlb:Colour Sine Strength,ptin:_ColourSineStrength,varname:_ColourSineStrength,prsc:0,min:-50,cur:0,max:50;proporder:6562-9985-2845-2591-3528-6262-739-6513-7775-3096;pass:END;sub:END;*/

Shader "LIONELSHADERS/Shakey" {
    Properties {
        _SubtractValue ("Subtract Value", Float ) = 0.05
        _MultiplyValue ("Multiply Value", Float ) = 0.2
        _MultiplyValue2 ("Multiply Value2", Float ) = 2
        _VertexBulge ("Vertex Bulge", Float ) = 4.5
        _EmissionColour ("Emission Colour", Color) = (1,1,1,1)
        _EmissionColour2 ("Emission Colour 2", Color) = (0.9044118,0,0,1)
        _EmissionStrength ("Emission Strength", Range(0, 1)) = 1
        _AlphaOpacity ("Alpha (Opacity)", Range(0, 1)) = 0
        _ShakeySineStrength ("Shakey Sine Strength", Range(-50, 50)) = 0
        _ColourSineStrength ("Colour Sine Strength", Range(-50, 50)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _SubtractValue;
            uniform fixed _MultiplyValue;
            uniform fixed _VertexBulge;
            uniform fixed _MultiplyValue2;
            uniform half _EmissionStrength;
            uniform fixed _AlphaOpacity;
            uniform half4 _EmissionColour;
            uniform half4 _EmissionColour2;
            uniform fixed _ShakeySineStrength;
            uniform fixed _ColourSineStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                half4 node_5069 = _Time + _TimeEditor;
                float4 node_8252 = _Time + _TimeEditor;
                v.vertex.xyz += (pow((abs((sin((_ShakeySineStrength*node_5069.g*(o.uv0+node_8252.g*float2(0.101,0)).r))-_SubtractValue))*_MultiplyValue),_VertexBulge)*_MultiplyValue2*v.normal);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                half4 node_7432 = _Time + _TimeEditor;
                float3 emissive = (lerp(_EmissionColour.rgb,_EmissionColour2.rgb,sin((_ColourSineStrength*node_7432.g)))*_EmissionStrength);
                float3 finalColor = emissive;
                return fixed4(finalColor,_AlphaOpacity);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _SubtractValue;
            uniform fixed _MultiplyValue;
            uniform fixed _VertexBulge;
            uniform fixed _MultiplyValue2;
            uniform fixed _ShakeySineStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
                float3 normalDir : TEXCOORD6;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                half4 node_5069 = _Time + _TimeEditor;
                float4 node_3548 = _Time + _TimeEditor;
                v.vertex.xyz += (pow((abs((sin((_ShakeySineStrength*node_5069.g*(o.uv0+node_3548.g*float2(0.101,0)).r))-_SubtractValue))*_MultiplyValue),_VertexBulge)*_MultiplyValue2*v.normal);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _SubtractValue;
            uniform fixed _MultiplyValue;
            uniform fixed _VertexBulge;
            uniform fixed _MultiplyValue2;
            uniform fixed _ShakeySineStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                half4 node_5069 = _Time + _TimeEditor;
                float4 node_6787 = _Time + _TimeEditor;
                v.vertex.xyz += (pow((abs((sin((_ShakeySineStrength*node_5069.g*(o.uv0+node_6787.g*float2(0.101,0)).r))-_SubtractValue))*_MultiplyValue),_VertexBulge)*_MultiplyValue2*v.normal);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
