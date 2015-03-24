// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9026,x:33003,y:32660,varname:node_9026,prsc:2|diffpow-5670-OUT,emission-5670-OUT;n:type:ShaderForge.SFN_Lerp,id:5670,x:32691,y:32678,varname:node_5670,prsc:2|A-7332-RGB,B-6762-OUT,T-1464-OUT;n:type:ShaderForge.SFN_Color,id:7332,x:32429,y:32596,ptovrint:True,ptlb:BaseColour,ptin:_BaseColour,varname:_BaseColour,prsc:1,glob:False,c1:1,c2:0.8690808,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:6762,x:32429,y:32782,varname:node_6762,prsc:2|A-421-RGB,B-9790-OUT;n:type:ShaderForge.SFN_Multiply,id:1464,x:32573,y:32947,varname:node_1464,prsc:2|A-7410-OUT,B-6849-OUT;n:type:ShaderForge.SFN_Color,id:421,x:32240,y:32596,ptovrint:True,ptlb:GlowColour,ptin:_GlowColour,varname:_GlowColour,prsc:1,glob:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_OneMinus,id:7410,x:32409,y:32947,varname:node_7410,prsc:2|IN-8508-OUT;n:type:ShaderForge.SFN_Fresnel,id:8508,x:32252,y:32947,varname:node_8508,prsc:2|NRM-9816-OUT,EXP-1933-OUT;n:type:ShaderForge.SFN_NormalVector,id:9816,x:32072,y:32828,prsc:2,pt:False;n:type:ShaderForge.SFN_Slider,id:6849,x:32252,y:33118,ptovrint:True,ptlb:Slider,ptin:_Slider,varname:_Slider,prsc:1,min:0,cur:1.196581,max:10;n:type:ShaderForge.SFN_ValueProperty,id:9790,x:32240,y:32811,ptovrint:True,ptlb:GlowIntensity,ptin:_GlowIntensity,varname:_GlowIntensity,prsc:1,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:1933,x:32072,y:33013,ptovrint:False,ptlb:Exp,ptin:_Exp,varname:_Exp,prsc:1,glob:False,v1:0.35;proporder:7332-421-6849-9790-1933;pass:END;sub:END;*/

Shader "Shader Forge/Glow_Base" {
    Properties {
        _BaseColour ("BaseColour", Color) = (1,0.8690808,0,1)
        _GlowColour ("GlowColour", Color) = (1,0,0,1)
        _Slider ("Slider", Range(0, 10)) = 1.196581
        _GlowIntensity ("GlowIntensity", Float ) = 1
        _Exp ("Exp", Float ) = 0.35
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform half4 _BaseColour;
            uniform half4 _GlowColour;
            uniform half _Slider;
            uniform half _GlowIntensity;
            uniform half _Exp;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 node_5670 = lerp(_BaseColour.rgb,(_GlowColour.rgb*_GlowIntensity),((1.0 - pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Exp))*_Slider));
                float3 emissive = node_5670;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
