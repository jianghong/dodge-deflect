// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:0,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:851,x:34581,y:32479,varname:node_851,prsc:2|spec-5483-OUT,gloss-7062-OUT,normal-7289-OUT,emission-1472-OUT,alpha-1569-OUT,refract-5097-OUT;n:type:ShaderForge.SFN_Slider,id:5811,x:33599,y:32925,ptovrint:False,ptlb:Refraction Intensity,ptin:_RefractionIntensity,varname:_RefractionIntensity,prsc:0,min:0,cur:4.813501,max:5;n:type:ShaderForge.SFN_Multiply,id:5097,x:34315,y:32946,varname:node_5097,prsc:2|A-5772-OUT,B-1708-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5772,x:34103,y:32796,varname:node_5772,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-4690-RGB;n:type:ShaderForge.SFN_Vector1,id:1569,x:34291,y:32874,varname:node_1569,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Tex2d,id:4690,x:33865,y:32694,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:_Refraction,prsc:2,tex:bca146bbc5499414c9cd51c7b9b671d8,ntxv:2,isnm:False|UVIN-7753-OUT;n:type:ShaderForge.SFN_TexCoord,id:3556,x:33479,y:32530,varname:node_3556,prsc:2,uv:1;n:type:ShaderForge.SFN_Multiply,id:7753,x:33678,y:32694,varname:node_7753,prsc:2|A-3556-UVOUT,B-5701-OUT;n:type:ShaderForge.SFN_Vector1,id:5701,x:33507,y:32790,varname:node_5701,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Lerp,id:7289,x:34103,y:32619,varname:node_7289,prsc:2|A-5679-OUT,B-4690-RGB,T-5811-OUT;n:type:ShaderForge.SFN_Fresnel,id:3445,x:33780,y:32164,varname:node_3445,prsc:2|NRM-6898-OUT,EXP-1792-OUT;n:type:ShaderForge.SFN_Multiply,id:1708,x:34079,y:32991,varname:node_1708,prsc:2|A-5811-OUT,B-2580-OUT;n:type:ShaderForge.SFN_Vector1,id:2580,x:33882,y:33035,varname:node_2580,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Color,id:140,x:33750,y:32304,ptovrint:False,ptlb:OuterColour,ptin:_OuterColour,varname:_OuterColour,prsc:2,glob:False,c1:1,c2:0,c3:0,c4:0.1019608;n:type:ShaderForge.SFN_Multiply,id:1472,x:34103,y:32240,varname:node_1472,prsc:2|A-3445-OUT,B-140-RGB,C-140-A;n:type:ShaderForge.SFN_ValueProperty,id:54,x:34103,y:32463,ptovrint:False,ptlb:Specularity,ptin:_Specularity,varname:_Specularity,prsc:0,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:7062,x:34268,y:32686,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Gloss,prsc:0,glob:False,v1:1;n:type:ShaderForge.SFN_NormalVector,id:6898,x:33524,y:32106,prsc:2,pt:False;n:type:ShaderForge.SFN_Slider,id:1792,x:33450,y:32273,ptovrint:False,ptlb:Outer Glow Strength,ptin:_OuterGlowStrength,varname:_OuterGlowStrength,prsc:0,min:0,cur:0,max:20;n:type:ShaderForge.SFN_Multiply,id:5483,x:34316,y:32479,varname:node_5483,prsc:2|A-54-OUT,B-7289-OUT;n:type:ShaderForge.SFN_Vector3,id:5679,x:33872,y:32496,varname:node_5679,prsc:2,v1:0,v2:0,v3:0;proporder:140-5811-4690-54-7062-1792;pass:END;sub:END;*/

Shader "Shader Forge/RefractionHole" {
    Properties {
        _OuterColour ("OuterColour", Color) = (1,0,0,0.1019608)
        _RefractionIntensity ("Refraction Intensity", Range(0, 5)) = 4.813501
        _Refraction ("Refraction", 2D) = "black" {}
        _Specularity ("Specularity", Float ) = 1
        _Gloss ("Gloss", Float ) = 1
        _OuterGlowStrength ("Outer Glow Strength", Range(0, 20)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform fixed _RefractionIntensity;
            uniform sampler2D _Refraction; uniform float4 _Refraction_ST;
            uniform float4 _OuterColour;
            uniform fixed _Specularity;
            uniform fixed _Gloss;
            uniform fixed _OuterGlowStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 node_7753 = (i.uv1*0.2);
                float4 _Refraction_var = tex2D(_Refraction,TRANSFORM_TEX(node_7753, _Refraction));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_Refraction_var.rgb.rg*(_RefractionIntensity*0.1));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 node_7289 = lerp(float3(0,0,0),_Refraction_var.rgb,_RefractionIntensity);
                float3 normalLocal = node_7289;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = (_Specularity*node_7289);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
////// Emissive:
                float3 node_1472 = (pow(1.0-max(0,dot(i.normalDir, viewDirection)),_OuterGlowStrength)*_OuterColour.rgb*_OuterColour.a);
                float3 emissive = node_1472;
/// Final Color:
                float3 finalColor = specular + emissive;
                return fixed4(lerp(sceneColor.rgb, finalColor,0.2),1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform fixed _RefractionIntensity;
            uniform sampler2D _Refraction; uniform float4 _Refraction_ST;
            uniform float4 _OuterColour;
            uniform fixed _Specularity;
            uniform fixed _Gloss;
            uniform fixed _OuterGlowStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 node_7753 = (i.uv1*0.2);
                float4 _Refraction_var = tex2D(_Refraction,TRANSFORM_TEX(node_7753, _Refraction));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_Refraction_var.rgb.rg*(_RefractionIntensity*0.1));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 node_7289 = lerp(float3(0,0,0),_Refraction_var.rgb,_RefractionIntensity);
                float3 normalLocal = node_7289;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = (_Specularity*node_7289);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/// Final Color:
                float3 finalColor = specular;
                return fixed4(finalColor * 0.2,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
