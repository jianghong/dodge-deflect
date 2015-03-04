// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4606,x:33345,y:32679,varname:node_4606,prsc:2|emission-2136-OUT;n:type:ShaderForge.SFN_Fresnel,id:7480,x:32790,y:32872,varname:node_7480,prsc:2|NRM-5439-OUT,EXP-889-OUT;n:type:ShaderForge.SFN_Blend,id:6321,x:32948,y:32855,varname:node_6321,prsc:2,blmd:1,clmp:True|SRC-7952-RGB,DST-7480-OUT;n:type:ShaderForge.SFN_Color,id:7952,x:32790,y:32678,ptovrint:False,ptlb:Outer Colour,ptin:_OuterColour,varname:node_7952,prsc:2,glob:False,c1:0.08088237,c2:0.6577076,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:889,x:32507,y:32906,varname:node_889,prsc:2,v1:1;n:type:ShaderForge.SFN_Fresnel,id:5592,x:32757,y:33251,varname:node_5592,prsc:2|NRM-2377-OUT,EXP-596-OUT;n:type:ShaderForge.SFN_Blend,id:2136,x:33154,y:32938,varname:node_2136,prsc:2,blmd:19,clmp:True|SRC-6321-OUT,DST-54-OUT;n:type:ShaderForge.SFN_Color,id:1724,x:32543,y:33019,ptovrint:False,ptlb:Inner Colour,ptin:_InnerColour,varname:node_1724,prsc:2,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Subtract,id:54,x:32852,y:33086,varname:node_54,prsc:2|A-1724-RGB,B-5592-OUT;n:type:ShaderForge.SFN_Vector1,id:596,x:32509,y:33389,varname:node_596,prsc:2,v1:1000;n:type:ShaderForge.SFN_NormalVector,id:2377,x:32543,y:33211,prsc:2,pt:True;n:type:ShaderForge.SFN_NormalVector,id:5439,x:32520,y:32734,prsc:2,pt:True;proporder:7952-1724;pass:END;sub:END;*/

Shader "Shader Forge/Star" {
    Properties {
        _OuterColour ("Outer Colour", Color) = (0.08088237,0.6577076,1,1)
        _InnerColour ("Inner Colour", Color) = (1,1,1,1)
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
            uniform float4 _OuterColour;
            uniform float4 _InnerColour;
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
                float node_7480 = pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0);
                float node_5592 = pow(1.0-max(0,dot(normalDirection, viewDirection)),1000.0);
                float3 emissive = saturate(((_InnerColour.rgb-node_5592)-saturate((_OuterColour.rgb*node_7480))));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
