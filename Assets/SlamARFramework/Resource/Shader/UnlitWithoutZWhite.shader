Shader "Custom/UnlitWithoutZWhite" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader{
			Tags { "Queue" = "Geometry+20" }

			ZWrite Off
			ZTest Off

			LOD 200

			CGPROGRAM
			#pragma surface surf myLightModel

			float4 LightingmyLightModel(SurfaceOutput s, float3 lightDir,half3 viewDir, half atten)
		{
			float4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
					return c;
		}

		sampler2D _MainTex;
		fixed4 _Color;

			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}