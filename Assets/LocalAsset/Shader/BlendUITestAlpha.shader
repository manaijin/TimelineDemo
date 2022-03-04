// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// 将两个纹理混合,但对Alpha进行检测
Shader "UI/BlendUI2"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_MainTex2("Texture2", 2D) = "white" {}
		_BlendParam("Blend Param", range(0,1)) = 1
		_BlendParam2("Blend Param2", range(0,1)) = 0

		_Color("Tint", Color) = (1,1,1,1)

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Blend One OneMinusSrcAlpha
			ColorMask[_ColorMask]

			Pass
			{
				Name "Default"
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				#pragma multi_compile_local _ UNITY_UI_CLIP_RECT
				#pragma multi_compile_local _ UNITY_UI_ALPHACLIP

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
					half4  mask : TEXCOORD2;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				sampler2D _MainTex;
				sampler2D _MainTex2;
				float _BlendParam;
				float _BlendParam2;
				fixed4 _Color;
				fixed4 _TextureSampleAdd;
				float4 _ClipRect;
				float4 _MainTex_ST;
				float4 _MainTex2_ST;
				float _UIMaskSoftnessX;
				float _UIMaskSoftnessY;

				v2f vert(appdata_t v)
				{
					v2f OUT;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

					float4 vPosition = UnityObjectToClipPos(v.vertex);
					OUT.worldPosition = v.vertex;
					OUT.vertex = vPosition;

					float2 pixelSize = vPosition.w;
					pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

					float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
					float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);
					OUT.texcoord = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
					OUT.mask = half4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize.xy)));

					OUT.color = v.color * _Color;
					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					half4 color = IN.color * (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd);
					half4 color2 = IN.color * (tex2D(_MainTex2, IN.texcoord) + _TextureSampleAdd);

					// 剔除
					clip(color.a - 0.001);
					clip(color2.a - 0.001);

					// 扭曲
					float2 dir = IN.texcoord - float2(0.5, 0.5);
					float2 scaleOffest = 0.2 * normalize(dir) * (1 - length(dir));
					scaleOffest.x = cos(_Time.y) * scaleOffest.x - sin(_Time.y) * scaleOffest.y;
					scaleOffest.y = sin(_Time.y) * scaleOffest.x + cos(_Time.y) * scaleOffest.y;
					IN.texcoord = IN.texcoord + scaleOffest;
					color = IN.color * (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd);
					color2 = IN.color * (tex2D(_MainTex2, IN.texcoord) + _TextureSampleAdd);

					color.rgb *= color.a;
					color2.rgb *= color2.a;
					return color * _BlendParam + color2 * _BlendParam2;
				}
				ENDCG
			}
		}
}
