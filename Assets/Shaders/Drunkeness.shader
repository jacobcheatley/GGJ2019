// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Drunkeness"
{
	Properties
	{
		_MainTex("Texture", 2D) = "black" {}
		_OffsetScale("Offset Scale", Range(0, 0.2)) = 0.005
		_WarpScale("Warp Scale", Range(0, 0.2)) = 0.05
		_DrugScale("Red Scale", Range(0, 2)) = 0
	}
		Subshader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vertex_shader
			#pragma fragment pixel_shader
			#pragma target 2.0

			sampler2D _MainTex;
			float _OffsetScale;
			float _WarpScale;
			float _DrugScale;

			float4 vertex_shader(float4 vertex:POSITION) :SV_POSITION
			{
				return UnityObjectToClipPos(vertex);
			}

			float4 pixel_shader(float4 vertex:SV_POSITION) : COLOR
			{
				vector <float,2> uv = vertex.xy / _ScreenParams.xy;
				uv.x += cos(uv.y*2.0 + _Time.g)*_WarpScale;
				uv.y += sin(uv.x*2.0 + _Time.g)*_WarpScale;

				/*float drugOffset;
				float py = sin(floor(_Time.g * 2.f));
				float ly = 0.125f;//iResolution.y;
				drugOffset = (uv.y > py) ? 0.1f : 0.f;
				drugOffset -= (uv.y > py + ly) ? 0.1f : 0.f;
				py = cos(floor(_Time.g * 3.5f));
				ly = 0.25f;//iResolution.y;
				drugOffset -= (uv.y > py) ? 0.08f : 0.f;
				drugOffset += (uv.y > py + ly) ? 0.08f : 0.f;
				py = cos(floor(_Time.g * 8.f));
				ly = 0.05f;//iResolution.y;
				drugOffset -= (uv.y > py) ? 0.15f : 0.f;
				drugOffset += (uv.y > py + ly) ? 0.15f : 0.f;

				drugOffset *= _DrugScale;
				uv.x += drugOffset;*/

				float offset = sin(_Time.g *0.5) * _OffsetScale;
				float4 a = tex2D(_MainTex,uv);
				float4 b = tex2D(_MainTex,uv - float2(sin(offset),0.0));
				float4 c = tex2D(_MainTex,uv + float2(sin(offset),0.0));
				float4 d = tex2D(_MainTex,uv - float2(0.0,sin(offset)));
				float4 e = tex2D(_MainTex,uv + float2(0.0,sin(offset)));

				float drugOffset;
				float py = sin(floor(_Time.g * 2.f));
				float ly = 0.125f;//iResolution.y;
				drugOffset = (uv.y > py) ? 0.1f : 0.f;
				drugOffset -= (uv.y > py + ly) ? 0.1f : 0.f;
				py = cos(floor(_Time.g * 3.5f));
				ly = 0.25f;//iResolution.y;
				drugOffset -= (uv.y > py) ? 0.08f : 0.f;
				drugOffset += (uv.y > py + ly) ? 0.08f : 0.f;
				py = cos(floor(_Time.g * 8.f));
				ly = 0.05f;//iResolution.y;
				drugOffset -= (uv.y > py) ? 0.15f : 0.f;
				drugOffset += (uv.y > py + ly) ? 0.15f : 0.f;

				drugOffset *= _DrugScale;
				uv.x += drugOffset;

				float4 drugSample = tex2D(_MainTex, uv );
				//return (a + b + c + d + e) / 5.0;
				return (a + b + c + d + e + (drugSample * 5)) * 0.1f;
			}
			ENDCG
		}
	}
}