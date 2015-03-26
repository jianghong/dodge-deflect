// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6604,x:33549,y:32554,varname:node_6604,prsc:2|diff-6759-OUT,spec-9065-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:1525,x:32549,y:32717,varname:node_1525,prsc:2;n:type:ShaderForge.SFN_LightColor,id:2045,x:32549,y:32847,varname:node_2045,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9065,x:33238,y:32477,varname:node_9065,prsc:2|A-1525-OUT,B-2045-RGB,C-2651-OUT;n:type:ShaderForge.SFN_NormalVector,id:9942,x:32320,y:32414,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:6541,x:32320,y:32288,varname:node_6541,prsc:2;n:type:ShaderForge.SFN_Dot,id:4916,x:32487,y:32316,varname:node_4916,prsc:2,dt:2|A-6541-OUT,B-9942-OUT;n:type:ShaderForge.SFN_HalfVector,id:7832,x:32320,y:32138,varname:node_7832,prsc:2;n:type:ShaderForge.SFN_Dot,id:39,x:32487,y:32151,varname:node_39,prsc:2,dt:0|A-7832-OUT,B-9942-OUT;n:type:ShaderForge.SFN_Power,id:6486,x:32749,y:32159,varname:node_6486,prsc:2|VAL-39-OUT,EXP-8259-OUT;n:type:ShaderForge.SFN_Slider,id:8259,x:32715,y:32002,ptovrint:False,ptlb:glossiness,ptin:_glossiness,varname:node_8259,prsc:2,min:1,cur:46.99495,max:100;n:type:ShaderForge.SFN_Multiply,id:2800,x:32950,y:32159,cmnt:Specular Contribution,varname:node_2800,prsc:2|A-6486-OUT,B-1315-OUT,C-6198-RGB;n:type:ShaderForge.SFN_Slider,id:1315,x:33049,y:32000,ptovrint:False,ptlb:specularity,ptin:_specularity,varname:node_1315,prsc:2,min:1,cur:6.377187,max:100;n:type:ShaderForge.SFN_Add,id:2651,x:33022,y:32343,varname:node_2651,prsc:2|A-2800-OUT,B-4916-OUT;n:type:ShaderForge.SFN_Color,id:2261,x:33357,y:32339,ptovrint:False,ptlb:colour,ptin:_colour,varname:node_2261,prsc:2,glob:False,c1:0.1,c2:0.4,c3:0.6,c4:1;n:type:ShaderForge.SFN_Color,id:6198,x:33297,y:32141,ptovrint:False,ptlb:Specularity Colour,ptin:_SpecularityColour,varname:node_6198,prsc:2,glob:False,c1:0.8,c2:0.4,c3:0.2,c4:0.2;n:type:ShaderForge.SFN_Multiply,id:6759,x:33595,y:32324,varname:node_6759,prsc:2|A-1182-OUT,B-2261-RGB;n:type:ShaderForge.SFN_Slider,id:1182,x:33464,y:32207,ptovrint:False,ptlb:Diffusion power,ptin:_Diffusionpower,varname:node_1182,prsc:2,min:0,cur:0.2478632,max:2;proporder:8259-1315-2261-6198-1182;pass:END;sub:END;*/

Shader "Shader Forge/Rock Specular" {
    Properties {
        _glossiness ("glossiness", Range(1, 100)) = 46.99495
        _specularity ("specularity", Range(1, 100)) = 6.377187
        _colour ("colour", Color) = (0.1,0.4,0.6,1)
        _SpecularityColour ("Specularity Colour", Color) = (0.8,0.4,0.2,0.2)
        _Diffusionpower ("Diffusion power", Range(0, 2)) = 0.2478632
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
            uniform float4 _colour;
            uniform float4 _SpecularityColour;
            uniform float _Diffusionpower;
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
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = (attenuation*_LightColor0.rgb*((pow(dot(halfDirection,i.normalDir),_glossiness)*_specularity*_SpecularityColour.rgb)+min(0,dot(lightDirection,i.normalDir))));
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * (_Diffusionpower*_colour.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular;
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
            uniform float4 _colour;
            uniform float4 _SpecularityColour;
            uniform float _Diffusionpower;
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
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = (attenuation*_LightColor0.rgb*((pow(dot(halfDirection,i.normalDir),_glossiness)*_specularity*_SpecularityColour.rgb)+min(0,dot(lightDirection,i.normalDir))));
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuse = directDiffuse * (_Diffusionpower*_colour.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
