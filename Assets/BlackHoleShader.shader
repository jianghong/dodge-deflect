// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:664,x:33273,y:32870,varname:node_664,prsc:2|emission-7968-OUT;n:type:ShaderForge.SFN_Fresnel,id:1895,x:32661,y:32964,varname:node_1895,prsc:2|NRM-6195-OUT,EXP-2811-OUT;n:type:ShaderForge.SFN_Slider,id:2561,x:32119,y:32769,ptovrint:False,ptlb:OutsideEdge,ptin:_OutsideEdge,varname:_OutsideEdge,prsc:2,min:0,cur:0,max:25;n:type:ShaderForge.SFN_Color,id:3105,x:32661,y:33102,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:_Colour,prsc:2,glob:False,c1:1,c2:0.02352941,c3:0.4641651,c4:1;n:type:ShaderForge.SFN_Multiply,id:7968,x:32865,y:33012,varname:node_7968,prsc:2|A-1895-OUT,B-3105-RGB;n:type:ShaderForge.SFN_NormalVector,id:6195,x:32702,y:32771,prsc:2,pt:False;n:type:ShaderForge.SFN_Log,id:2811,x:32479,y:32760,varname:node_2811,prsc:2,lt:2|IN-2561-OUT;proporder:2561-3105;pass:END;sub:END;*/

Shader "Shader Forge/BlackHoleShader" {
    Properties {
        _OutsideEdge ("OutsideEdge", Range(0, 25)) = 0
        _Colour ("Colour", Color) = (1,0.02352941,0.4641651,1)
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
            uniform float _OutsideEdge;
            uniform float4 _Colour;
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
                float3 emissive = (pow(1.0-max(0,dot(i.normalDir, viewDirection)),log10(_OutsideEdge))*_Colour.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
