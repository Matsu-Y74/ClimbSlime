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
			o.Albedo = _BaseColor;
			float alpha = (1 - (abs(dot(IN.viewDir, IN.worldNormal)))) * 0.5f;
			o.Alpha = _BaseColor.a * alpha + 0.5f;
		}
		ENDCG
	}
	FallBack "Diffuse"
}