// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:851,x:34581,y:32479,varname:node_851,prsc:2|diff-3157-OUT,spec-3882-OUT,gloss-4927-OUT,normal-7289-OUT,transm-7727-OUT,lwrap-7727-OUT,alpha-1569-OUT,refract-5097-OUT;n:type:ShaderForge.SFN_Slider,id:5811,x:33708,y:32880,ptovrint:False,ptlb:Refraction Intensity,ptin:_RefractionIntensity,varname:_RefractionIntensity,prsc:2,min:0,cur:0.2279934,max:1;n:type:ShaderForge.SFN_Multiply,id:5097,x:34273,y:32852,varname:node_5097,prsc:2|A-5772-OUT,B-1708-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5772,x:34103,y:32779,varname:node_5772,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-4690-RGB;n:type:ShaderForge.SFN_Vector1,id:1569,x:34273,y:32779,varname:node_1569,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Tex2d,id:4690,x:33865,y:32694,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:_Refraction,prsc:2,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:True|UVIN-7753-OUT;n:type:ShaderForge.SFN_TexCoord,id:3556,x:33507,y:32633,varname:node_3556,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:7753,x:33678,y:32694,varname:node_7753,prsc:2|A-3556-UVOUT,B-5701-OUT;n:type:ShaderForge.SFN_Vector1,id:5701,x:33507,y:32790,varname:node_5701,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Vector1,id:7727,x:34273,y:32705,varname:node_7727,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:3882,x:34273,y:32558,varname:node_3882,prsc:2,v1:6;n:type:ShaderForge.SFN_Vector1,id:4927,x:34273,y:32614,varname:node_4927,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Lerp,id:7289,x:34103,y:32652,varname:node_7289,prsc:2|A-4145-OUT,B-4690-RGB,T-5811-OUT;n:type:ShaderForge.SFN_Vector3,id:4145,x:33865,y:32573,varname:node_4145,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Fresnel,id:3445,x:33871,y:32226,varname:node_3445,prsc:2|EXP-3793-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:3157,x:34273,y:32412,varname:node_3157,prsc:2,a:0.01,b:4|IN-1472-OUT;n:type:ShaderForge.SFN_Multiply,id:1708,x:34103,y:32931,varname:node_1708,prsc:2|A-5811-OUT,B-2580-OUT;n:type:ShaderForge.SFN_Vector1,id:2580,x:33865,y:32959,varname:node_2580,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Color,id:140,x:33871,y:32366,ptovrint:False,ptlb:OuterColour,ptin:_OuterColour,varname:node_1995,prsc:2,glob:False,c1:0.3607843,c2:0,c3:0,c4:0.1019608;n:type:ShaderForge.SFN_Multiply,id:1472,x:34076,y:32342,varname:node_1472,prsc:2|A-3445-OUT,B-140-RGB;n:type:ShaderForge.SFN_Slider,id:3793,x:33735,y:32140,ptovrint:False,ptlb:OuterColourStrength,ptin:_OuterColourStrength,varname:node_824,prsc:2,min:0,cur:1.616566,max:20;proporder:140-3793-5811-4690;pass:END;sub:END;*/

Shader "Shader Forge/RefractionHole" {
    Properties {
        _OuterColour ("OuterColour", Color) = (0.3607843,0,0,0.1019608)
        _OuterColourStrength ("OuterColourStrength", Range(0, 20)) = 1.616566
        _RefractionIntensity ("Refraction Intensity", Range(0, 1)) = 0.2279934
        _Refraction ("Refraction", 2D) = "bump" {}
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
            ZWrite Off
            
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
            uniform float _RefractionIntensity;
            uniform sampler2D _Refraction; uniform float4 _Refraction_ST;
            uniform float4 _OuterColour;
            uniform float _OuterColourStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
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
                float2 node_7753 = (i.uv0*0.2);
                float3 _Refraction_var = UnpackNormal(tex2D(_Refraction,TRANSFORM_TEX(node_7753, _Refraction)));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_Refraction_var.rgb.rg*(_RefractionIntensity*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalLocal = lerp(float3(0,0,1),_Refraction_var.rgb,_RefractionIntensity);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.8;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_3882 = 6.0;
                float3 specularColor = float3(node_3882,node_3882,node_3882);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float node_7727 = 1.0;
                float3 w = float3(node_7727,node_7727,node_7727)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(node_7727,node_7727,node_7727);
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * lerp(0.01,4,(pow(1.0-max(0,dot(normalDirection, viewDirection)),_OuterColourStrength)*_OuterColour.rgb));
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(lerp(sceneColor.rgb, finalColor,0.3),1);
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
            uniform float _RefractionIntensity;
            uniform sampler2D _Refraction; uniform float4 _Refraction_ST;
            uniform float4 _OuterColour;
            uniform float _OuterColourStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
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
                float2 node_7753 = (i.uv0*0.2);
                float3 _Refraction_var = UnpackNormal(tex2D(_Refraction,TRANSFORM_TEX(node_7753, _Refraction)));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_Refraction_var.rgb.rg*(_RefractionIntensity*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalLocal = lerp(float3(0,0,1),_Refraction_var.rgb,_RefractionIntensity);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.8;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_3882 = 6.0;
                float3 specularColor = float3(node_3882,node_3882,node_3882);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float node_7727 = 1.0;
                float3 w = float3(node_7727,node_7727,node_7727)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(node_7727,node_7727,node_7727);
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                float3 diffuse = directDiffuse * lerp(0.01,4,(pow(1.0-max(0,dot(normalDirection, viewDirection)),_OuterColourStrength)*_OuterColour.rgb));
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 0.3,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
