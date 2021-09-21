Shader "Unlit/BloomShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
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
		float4 _MainTex_TexelSize;
		sampler2D _CameraDepthTexture;
		float4 _CameraDepthTexture_ST;
		sampler2D _SourceTex;
		half4 _FilterParams;
		float _Intensity;

        v2f vert (appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            UNITY_TRANSFER_FOG(o,o.vertex);
            return o;
        }

		half3 sampleMain(float2 uv)
		{
			return tex2D(_MainTex, uv).rgb;
		}

		half3 sampleBox(float2 uv, float delta)
		{
			float4 offset = _MainTex_TexelSize.xyxy * float2(-delta, delta).xxyy;
			half3 sum = sampleMain(uv + offset.xy) + sampleMain(uv + offset.zy) + sampleMain(uv + offset.xw) + sampleMain(uv + offset.zw);
			return sum * 0.25;
		}

		//明度を返す
		half getBrightness(half3 color)
		{
			return max(color.r, max(color.g, color.b));
		}

		ENDCG

		Cull Off
		ZTest Always
		ZWrite Off

		Tags{ "RenderType" = "Opaque" }

		//0: 適応するピクセル抽出用のパス
		Pass
		{
			CGPROGRAM
			
			fixed4 frag(v2f i) : SV_Target
			{
				//光度の高い色を抽出
				half4 col = 1;
				col.rgb = sampleBox(i.uv, 1.0);
				half brightness = getBrightness(col.rgb);

				half soft = brightness - _FilterParams.y;
				soft = clamp(soft, 0, _FilterParams.z);
				soft = soft * soft * _FilterParams.w;
				half contribution = max(soft, brightness - _FilterParams.x);
				contribution /= max(brightness, 0.00001);
				return col * contribution;
			}
			ENDCG
		}

		//1: ダウンサンプリング用のパス
		Pass
		{
			CGPROGRAM

			fixed4 frag(v2f i) : SV_Target
			{
				half4 col = 1;
				col.rgb = sampleBox(i.uv, 1.0);
				return col;
			}
			ENDCG
		}

		//2: アップサンプリング用のパス
		Pass
		{
			Blend One One
			CGPROGRAM

			fixed4 frag(v2f i) : SV_Target
			{
				half4 col = 1;
				col.rgb = sampleBox(i.uv, 0.5);
				return col;
			}
			ENDCG
		}

		//3: 最後の一回のアップサンプリング用のパス
		Pass
		{
			CGPROGRAM

			fixed4 frag(v2f i) : SV_Target
			{
				half4 col = tex2D(_SourceTex, i.uv);
				col.rgb += sampleBox(i.uv, 0.5) * _Intensity;
				return col;
			}
			ENDCG
		}

		//4: デバッグ用
		Pass
		{
			CGPROGRAM

			fixed4 frag(v2f i) : SV_Target
			{
				half4 col = 1;
				col.rgb = sampleBox(i.uv, 0.5) * _Intensity;
				return col;
			}

			ENDCG
		}
    }
}
