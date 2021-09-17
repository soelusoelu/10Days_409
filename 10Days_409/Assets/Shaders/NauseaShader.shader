Shader "Unlit/NauseaShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Amp ("Amp", float) = 0.1
		_T ("T", float) = 0.25
    }
    SubShader
    {
		//No culling off ZTest Always
		Cull Off ZWrite Off ZTest Always
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
			float _Amp;
			float _T;

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
				float2 uv = i.uv;
				uv.x += sin((i.uv.y + _Time.y) * 3.14 / _T) * _Amp;
				uv.y += sin((i.uv.x + _Time.y) * 3.14 / _T) * _Amp;
				fixed4 col = tex2D(_MainTex, uv);
				return col;
            }
            ENDCG
        }
    }
}
