// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6604,x:33549,y:32554,varname:node_6604,prsc:2|emission-8552-OUT,custl-9065-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:1525,x:32498,y:32604,varname:node_1525,prsc:2;n:type:ShaderForge.SFN_LightColor,id:2045,x:32498,y:32734,varname:node_2045,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9065,x:33238,y:32477,varname:node_9065,prsc:2|A-1525-OUT,B-2045-RGB,C-2651-OUT;n:type:ShaderForge.SFN_NormalVector,id:9942,x:32320,y:32414,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:6541,x:32320,y:32288,varname:node_6541,prsc:2;n:type:ShaderForge.SFN_Dot,id:4916,x:32487,y:32316,varname:node_4916,prsc:2,dt:2|A-6541-OUT,B-9942-OUT;n:type:ShaderForge.SFN_HalfVector,id:7832,x:32320,y:32138,varname:node_7832,prsc:2;n:type:ShaderForge.SFN_Dot,id:39,x:32487,y:32151,varname:node_39,prsc:2,dt:1|A-7832-OUT,B-9942-OUT;n:type:ShaderForge.SFN_Power,id:6486,x:32749,y:32159,varname:node_6486,prsc:2|VAL-39-OUT,EXP-8259-OUT;n:type:ShaderForge.SFN_Slider,id:8259,x:32715,y:32002,ptovrint:False,ptlb:glossiness,ptin:_glossiness,varname:node_8259,prsc:2,min:1,cur:76.60124,max:100;n:type:ShaderForge.SFN_Multiply,id:2800,x:32950,y:32159,cmnt:Specular Contribution,varname:node_2800,prsc:2|A-6486-OUT,B-1315-OUT;n:type:ShaderForge.SFN_Slider,id:1315,x:33049,y:32000,ptovrint:False,ptlb:specularity,ptin:_specularity,varname:node_1315,prsc:2,min:0.1,cur:0.7461141,max:4;n:type:ShaderForge.SFN_Add,id:2651,x:32970,y:32345,varname:node_2651,prsc:2|A-2800-OUT,B-4916-OUT;n:type:ShaderForge.SFN_AmbientLight,id:1187,x:32820,y:32762,varname:node_1187,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8552,x:33020,y:32834,varname:node_8552,prsc:2|A-1187-RGB,B-5836-RGB;n:type:ShaderForge.SFN_Color,id:5836,x:32820,y:32920,ptovrint:False,ptlb:node_5836,ptin:_node_5836,varname:node_5836,prsc:2,glob:False,c1:0.1,c2:0.1,c3:0.3,c4:1;proporder:8259-1315-5836;pass:END;sub:END;*/

Shader "Shader Forge/Rock Specular" {
    Properties {
        _glossiness ("glossiness", Range(1, 100)) = 76.60124
        _specularity ("specularity", Range(0.1, 4)) = 0.7461141
        _node_5836 ("node_5836", Color) = (0.1,0.1,0.3,1)
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
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float _glossiness;
            uniform float _specularity;
            uniform float4 _node_5836;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float3 emissive = (UNITY_LIGHTMODEL_AMBIENT.rgb*_node_5836.rgb);
                float3 finalColor = emissive + (attenuation*_LightColor0.rgb*((pow(max(0,dot(halfDirection,i.normalDir)),_glossiness)*_specularity)+min(0,dot(lightDirection,i.normalDir))));
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float _glossiness;
            uniform float _specularity;
            uniform float4 _node_5836;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 finalColor = (attenuation*_LightColor0.rgb*((pow(max(0,dot(halfDirection,i.normalDir)),_glossiness)*_specularity)+min(0,dot(lightDirection,i.normalDir))));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
