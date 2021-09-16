Shader "Unlit/GridShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		[HDR]_GridColor("Grid Colour", Color) = (255, 0, 0, 0)
		_GridNum("Grid num", int) = 10
    }
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
			float4 _GridColor;
			float _GridNum;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			float GridTest(float2 r)
			{
				float result;
				float gridnum = (_GridNum < 1) ? 1 : _GridNum;
				gridnum = 1.0 / gridnum;

				for (float i = 0.0; i < 1.0; i += gridnum)
				{
					for (int j = 0; j < 2; j++)
					{
						result += 1.0 - smoothstep(0.0, 0.004, abs(r[j] - i));
					}
				}

				return result;
			}

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //return col;

				//return float4(GridTest(i.uv), 0.0, 0.0, 0.0);

				fixed4 gridColour = (_GridColor * GridTest(i.uv)) + tex2D(_MainTex, i.uv);
				return float4(gridColour);
            }
            ENDCG
        }
    }
}
