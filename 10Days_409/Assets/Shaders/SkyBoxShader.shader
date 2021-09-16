Shader "CustomSkybox/SkyBoxShader"
{
	SubShader
	{
		Tags
		{
			"RenderType" = "Background"
			"Queue" = "Background"
			"PreviewType" = "SkyBox"
		}

		Pass
		{
			ZWrite Off
			Cull Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
				float3 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 texcoord : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}

			float GridTest(float3 r)
			{
				float result;

				for (float i = -1.0; i < 1.0; i += 0.075)
				{
					for (int j = 0; j < 3; j++)
					{
						result += 1.0 - smoothstep(0.0, 0.004, abs(r[j] - i));
					}
				}

				return result;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return float4(0.0, 0.0, GridTest(i.texcoord.xyz) / 2, 0.0);
				//return fixed4(lerp(fixed3(1, 0, 0), fixed3(0, 0, 1), i.texcoord.y * 0.5 + 0.5), 1.0);
			}
			ENDCG
		}
	}
	/*
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
	*/
}
