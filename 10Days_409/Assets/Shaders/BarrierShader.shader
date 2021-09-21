Shader "Unlit/BarrierShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_MaskMap("MaskMap", 2D) = "white"{}
		_DistortionMap("DistortionMap", 2D) = "white"{}
		_Color("Color", Color) = (1, 1, 1, 1)
		_AlphaCut("AlphaCut", float) = 0.8
    }
    SubShader
    {
        Tags 
		{ 
			//"RenderType"="Opaque"
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		Blend SrcAlpha OneMinusSrcAlpha //重なったオブジェクトの画素の色とのブレンド方法の指定

        LOD 100

        CGINCLUDE
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
		sampler2D _MaskMap;
		float4 _MaskMap_ST;
		sampler2D _DistortionMap;
		float4 _DistortionMap_ST;
		float4 _Color;
		float _AlphaCut;

        v2f vert (appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            UNITY_TRANSFER_FOG(o,o.vertex);
            return o;
        }

		fixed4 f(v2f i)
		{
			//fixed4 maskMap = tex2D(_MaskMap, i.uv);
			//fixed4 distortionMap = tex2D(_DistortionMap, i.uv);
			//fixed4 col = lerp(_Color, distortionMap, maskMap.b);
			//return col;
			float t = _Time.x;
			float t2 = _SinTime.x;
			fixed4 maskMap = tex2D(_MaskMap, i.uv + float2(t, -t));
			fixed4 maskMap2 = tex2D(_MaskMap, i.uv + float2(-t2, t2));
			clip(maskMap.a - _AlphaCut);
			clip(maskMap2.a - _AlphaCut);
			fixed4 col = _Color * maskMap * maskMap2;
			col.a = 0.7;
			return col;
		}

		
		ENDCG//ここまで共有
		Pass
		{
			//裏面のみ描画
			Cull front
			CGPROGRAM
			fixed4 frag(v2f i) : SV_Target
			{
				return f(i);
			}
			ENDCG
		}

		Pass
		{
			//表面のみ描画
			Cull back
			CGPROGRAM
			fixed4 frag(v2f i) : SV_Target
			{
				return f(i);
			}
			ENDCG
		}
    }
}
