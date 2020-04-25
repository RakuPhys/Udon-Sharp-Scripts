Shader "Custom/Board"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_posx("PositionX",Range(0,3))=0.0
		_posy("PositionY",Range(0,3))=0.0
		_result("Result",Range(-1,1))=0.0//1:goal,-1:fail
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float _posx;
		float _posy;
		float _result;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
             // Albedo comes from a texture tinted by color
			float2 uv = IN.uv_MainTex;
			fixed3 c = fixed3(0.05,0.04,0.04) + clamp((-_result),0.0,1.0)*fixed3(0.7,0.07,0.5)+clamp(_result,0.0,1.0)*fixed3(0.8,0.5,0.07);
			fixed3 c1 = fixed3(0.07,0.7,0.5)*1.4;	
			fixed3 c2 = fixed3(0.7,0.07,0.5)*1.4;
			float2 pf = float2(fmod((uv.x-0.005)*4.0,1.0),fmod((uv.y-0.005)*4.0,1.0));
			c += 0.5 * (step(0.1,abs(pf.x)-0.88) + step(0.1,abs(pf.y)-0.88));			
			float2 pc = (uv-0.5)*8.0 + float2(2.0*(1.5-_posx),2.0*(1.5-_posy));
			float r = length(pc);
			c += c1*clamp((r - 0.5)*100.0,0.0,2.0) * clamp((0.6 - r)*100.0,0.0,2.0);
			float2 pg = (uv-0.5)*8.0 + float2(-3.0,-3.0);
			float rg = length(pg);
			c += c2*clamp((rg - 0.5)*100.0,0.0,2.0) * clamp((0.6 - rg)*100.0,0.0,2.0);

			o.Albedo = c;
			o.Emission = c*0.3;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
