// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1397059,fgcg:0.1397059,fgcb:0.1397059,fgca:1,fgde:1,fgrn:0.59,fgrf:1.35,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:8351,x:32719,y:32712,varname:node_8351,prsc:2|emission-4645-OUT;n:type:ShaderForge.SFN_VertexColor,id:5630,x:32203,y:32784,varname:node_5630,prsc:2;n:type:ShaderForge.SFN_Slider,id:9778,x:32220,y:32700,ptovrint:False,ptlb:emission strength,ptin:_emissionstrength,varname:node_9778,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:4645,x:32541,y:32791,varname:node_4645,prsc:2|A-9778-OUT,B-5630-RGB;proporder:9778;pass:END;sub:END;*/

Shader "LIONELSHADERS/VertexColour" {
    Properties {
        _emissionstrength ("emission strength", Range(0, 1)) = 0
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
            uniform float _emissionstrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float3 emissive = (_emissionstrength*i.vertexColor.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
