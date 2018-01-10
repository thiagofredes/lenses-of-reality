// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Color Skybox" {
	Properties {
		_SkyColor ("Sky Color", Color) = (1,1,1,1)
		_SkyNoise ("Sky Noise", 2D) = "white" {}
		_NoiseMagnitude ("Noise Magnitude", Range(0, 1)) = 0.01
		_DarknessHeight ("Darkness Height", Range(0, 1)) = 0.5
		_CloudSpeed ("Cloud Speed", Range(0, 1)) = 0.25
	}

	SubShader {
		Tags { "Queue"="Background"  }

		Pass {
			ZWrite Off 
			Cull Off

			CGPROGRAM

				#include "UnityCG.cginc"

				#pragma vertex vert
				#pragma fragment frag

				// User-specified uniforms
				fixed4 _SkyColor;
				sampler2D _SkyNoise;
				float _NoiseMagnitude;
				float _DarknessHeight;
				float _CloudSpeed;

				struct vertexInput {
					float4 vertex : POSITION;
					float3 texcoord : TEXCOORD0;
				};

				struct vertexOutput {
					float4 vertex : SV_POSITION;
					float3 texcoord : TEXCOORD0;
				};

				vertexOutput vert(vertexInput input)
				{
					vertexOutput output;
					output.vertex = UnityObjectToClipPos(input.vertex);
					output.texcoord = input.texcoord;
					return output;
				}

				fixed4 frag (vertexOutput input) : COLOR
				{
					float2 distuv = float2(input.texcoord.x + (_Time.x * _CloudSpeed), input.texcoord.y + (_Time.x * _CloudSpeed));
					float3 disp = tex2D(_SkyNoise, distuv).xyz;
					disp = ((disp * 2) - 1) * _NoiseMagnitude;
					return (_SkyColor + fixed4(disp,0)) * (max(-_DarknessHeight, input.texcoord.y) + _DarknessHeight)/(1 + _DarknessHeight);
				}
			ENDCG 
		}
	} 	
}