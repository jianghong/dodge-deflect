Shader "UI/BlendModes/Font/Divide"
{
	Properties
	{
		[PerRendererData] 
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15
	}

	SubShader
	{
		Tags
		{ 
			"Queue" = "Transparent" 
			"IgnoreProjector" = "True" 
			"RenderType" = "Transparent" 
			"PreviewType" = "Plane"		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		GrabPass { }

		Pass
		{
			CGPROGRAM
			
			#include "UnityCG.cginc"
			#include "./IBlendModes.cginc"
			
			#pragma vertex ComputeVertex
			#pragma fragment ComputeFragment
			
			sampler2D _MainTex;
			sampler2D _GrabTexture;
			fixed4 _Color;
			
			struct VertexInput
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct VertexOutput
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
			};
			
			
			VertexOutput ComputeVertex (VertexInput i)
			{
				VertexOutput o;
				
				o.vertex = mul(UNITY_MATRIX_MVP, i.vertex);
				o.screenPos = o.vertex;	
				o.texcoord = i.texcoord;
				#ifdef UNITY_HALF_TEXEL_OFFSET
				o.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1.0, 1.0);
				#endif
				o.color = i.color * _Color;
							
				return o;
			}

			fixed4 ComputeFragment (VertexOutput i) : SV_Target
			{
				fixed4 color = i.color;
				color.a *= tex2D(_MainTex, i.texcoord).a;
				clip(color.a - .01);
				
				float2 grabTexcoord = i.screenPos.xy / i.screenPos.w; 
				grabTexcoord.x = (grabTexcoord.x + 1.0) * .5;
				grabTexcoord.y = (grabTexcoord.y + 1.0) * .5; 
				#if UNITY_UV_STARTS_AT_TOP
				grabTexcoord.y = 1.0 - grabTexcoord.y;
				#endif
				
				half4 grabColor = tex2D(_GrabTexture, grabTexcoord); 
				
				return Divide(grabColor, color);
			}
			
			ENDCG
		}
	}

	Fallback "UI/Default Font"
}
