// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:Diffuse,lico:1,lgpr:1,nrmq:1,limd:1,uamb:False,mssp:True,lmpd:False,lprd:False,rprd:False,enco:True,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:True,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4606,x:33908,y:32759,varname:node_4606,prsc:2|diff-5846-OUT,emission-1825-OUT,transm-7385-OUT;n:type:ShaderForge.SFN_Fresnel,id:4805,x:32596,y:32619,varname:node_4805,prsc:2|NRM-3450-OUT,EXP-1101-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1101,x:32362,y:32784,ptovrint:True,ptlb:Exp,ptin:_Exp,varname:_Exp,prsc:2,glob:False,v1:0;n:type:ShaderForge.SFN_NormalVector,id:8586,x:31884,y:32485,prsc:2,pt:False;n:type:ShaderForge.SFN_Fresnel,id:2524,x:32639,y:33117,varname:node_2524,prsc:2|NRM-7291-OUT,EXP-9143-OUT;n:type:ShaderForge.SFN_OneMinus,id:7385,x:32817,y:33117,varname:node_7385,prsc:2|IN-2524-OUT;n:type:ShaderForge.SFN_NormalVector,id:7291,x:32415,y:33107,prsc:2,pt:False;n:type:ShaderForge.SFN_ValueProperty,id:9143,x:32411,y:33311,ptovrint:False,ptlb:Rim_in_Shadow,ptin:_Rim_in_Shadow,varname:_Rim_in_Shadow,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Color,id:1008,x:32780,y:32222,ptovrint:False,ptlb:OverallColour,ptin:_OverallColour,varname:_OverallColour,prsc:2,glob:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_OneMinus,id:3461,x:32780,y:32619,varname:node_3461,prsc:2|IN-4805-OUT;n:type:ShaderForge.SFN_Multiply,id:2883,x:32985,y:32854,varname:node_2883,prsc:2|A-3461-OUT,B-1786-OUT;n:type:ShaderForge.SFN_Slider,id:1786,x:32612,y:32776,ptovrint:True,ptlb:Brightness,ptin:_Brightness,varname:_Brightness,prsc:2,min:0,cur:0.4909381,max:1;n:type:ShaderForge.SFN_LightVector,id:2350,x:33165,y:31937,varname:node_2350,prsc:2;n:type:ShaderForge.SFN_HalfVector,id:3450,x:32362,y:32559,varname:node_3450,prsc:2;n:type:ShaderForge.SFN_Fresnel,id:7837,x:33417,y:32262,varname:node_7837,prsc:2|NRM-5339-OUT,EXP-5697-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5697,x:33210,y:32283,ptovrint:False,ptlb:Atmosphere_thickness,ptin:_Atmosphere_thickness,varname:_Atmosphere_thickness,prsc:2,glob:False,v1:2.5;n:type:ShaderForge.SFN_NormalVector,id:5339,x:33210,y:32116,prsc:2,pt:False;n:type:ShaderForge.SFN_Color,id:1436,x:33417,y:32063,ptovrint:False,ptlb:Atmosphere_colour,ptin:_Atmosphere_colour,varname:_Atmosphere_colour,prsc:2,glob:False,c1:1,c2:0.8068966,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:1825,x:33598,y:32262,varname:node_1825,prsc:2|A-1436-RGB,B-7837-OUT;n:type:ShaderForge.SFN_HalfVector,id:5446,x:33184,y:31780,varname:node_5446,prsc:2;n:type:ShaderForge.SFN_Lerp,id:5846,x:33495,y:32756,varname:node_5846,prsc:2|A-1008-RGB,B-2883-OUT,T-4364-OUT;n:type:ShaderForge.SFN_Slider,id:4364,x:32540,y:32446,ptovrint:False,ptlb:node_4364,ptin:_node_4364,varname:_node_4364,prsc:2,min:0,cur:0.7698093,max:1;proporder:1101-9143-1008-1786-5697-1436-4364;pass:END;sub:END;*/

Shader "LIONEL_GRANT_ASSETS\SHADERS\Planet" {
    Properties {
        _Exp ("Exp", Float ) = 0
        _Rim_in_Shadow ("Rim_in_Shadow", Float ) = 1
        _OverallColour ("OverallColour", Color) = (1,0,0,1)
        _Brightness ("Brightness", Range(0, 1)) = 0.4909381
        _Atmosphere_thickness ("Atmosphere_thickness", Float ) = 2.5
        _Atmosphere_colour ("Atmosphere_colour", Color) = (1,0.8068966,0,1)
        _node_4364 ("node_4364", Range(0, 1)) = 0.7698093
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
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _Exp;
            uniform float _Rim_in_Shadow;
            uniform float4 _OverallColour;
            uniform float _Brightness;
            uniform float _Atmosphere_thickness;
            uniform float4 _Atmosphere_colour;
            uniform float _node_4364;
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
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 forwardLight = max(0.0, NdotL );
                float node_7385 = (1.0 - pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Rim_in_Shadow));
                float3 backLight = max(0.0, -NdotL ) * float3(node_7385,node_7385,node_7385);
                float3 directDiffuse = (forwardLight+backLight)*InvPi * attenColor;
                float node_2883 = ((1.0 - pow(1.0-max(0,dot(halfDirection, viewDirection)),_Exp))*_Brightness);
                float3 diffuse = directDiffuse * lerp(_OverallColour.rgb,float3(node_2883,node_2883,node_2883),_node_4364);
////// Emissive:
                float3 emissive = (_Atmosphere_colour.rgb*pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Atmosphere_thickness));
/// Final Color:
                float3 finalColor = diffuse + emissive;
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
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _Exp;
            uniform float _Rim_in_Shadow;
            uniform float4 _OverallColour;
            uniform float _Brightness;
            uniform float _Atmosphere_thickness;
            uniform float4 _Atmosphere_colour;
            uniform float _node_4364;
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
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 forwardLight = max(0.0, NdotL );
                float node_7385 = (1.0 - pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Rim_in_Shadow));
                float3 backLight = max(0.0, -NdotL ) * float3(node_7385,node_7385,node_7385);
                float3 directDiffuse = (forwardLight+backLight)*InvPi * attenColor;
                float node_2883 = ((1.0 - pow(1.0-max(0,dot(halfDirection, viewDirection)),_Exp))*_Brightness);
                float3 diffuse = directDiffuse * lerp(_OverallColour.rgb,float3(node_2883,node_2883,node_2883),_node_4364);
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
