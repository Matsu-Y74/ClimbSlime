//http://nn-hokuson.hatenablog.com/entry/2016/10/07/221724
Shader "Custom/SlimeShader" {
	Properties{
		_BaseColor ("Base Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "Queue" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade
		#pragma target 3.0

		fixed4 _BaseColor;

		struct Input {
			float3 worldNormal;
   			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = fixed4(_BaseColor.rgb, 1);
			float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
			o.Alpha = _BaseColor.a * alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}