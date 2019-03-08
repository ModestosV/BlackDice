Shader "Custom/Grayscale"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			half4 tex = tex2D(_MainTex, In.uv_MainTex);

			half brightness = dot(tex.rgb, half(0.3, 0.59, 0.11));

			o.Albedo = brightness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
