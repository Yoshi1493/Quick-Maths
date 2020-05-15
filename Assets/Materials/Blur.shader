Shader "Custom/Blur"
{
    Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		//_MainTex_TexelSize("TexelSize", float4(1.0, 1.0, 1.0, 1.0)) = (1.0, 1.0, 1.0, 1.0)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
				// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP, *)' with 'UnityObjectToClipPos(*)'
				o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			float4 _MainTex_TexelSize;

			float4 boxBlur(sampler2D tex, float2 uv, float4 size)
			{
				float4 c;

				c = tex2D(tex, uv + float2(-size.x, size.y)) + tex2D(tex, uv + float2(0, size.y)) + tex2D(tex, uv + float2(size.x, size.y)) +
					tex2D(tex, uv + float2(-size.x, 0)) + tex2D(tex, uv + float2(0, 0)) + tex2D(tex, uv + float2(size.x, 0)) +
					tex2D(tex, uv + float2(-size.x, -size.y)) + tex2D(tex, uv + float2(0, -size.y)) + tex2D(tex, uv + float2(size.x, -size.y));

				/*for (int i = -1; i <= 1; i++)
				{
					for (int j = -1; j <= 1; j++)
					{
						c += tex2D(tex, uv + float2(size.x * i, size.y * j));
					}
				}*/

				return c / 9;
			}

            fixed4 frag (v2f i) : SV_Target
            {
				float4 col = boxBlur(_MainTex, i.uv, _MainTex_TexelSize);
                return col;
            }
            ENDCG
        }
    }
}
