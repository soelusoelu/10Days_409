Shader "Unlit/PostEffectShader_Test"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_CameraDepthTexture("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags 
		{ 
			"RenderType"="Opaque" 
			"LightMode" = "ShadowCraster"
		}
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

        sampler2D _MainTex;//���C���̃e�N�X�`��
        float4 _MainTex_ST;
		sampler2D _NormalTex;//���̃e�N�X�`��
		sampler2D _CameraDepthTexture;
		float4 _CameraDepthTexture_ST;
		//sampler2D _BTex;//�u���[������������̉摜
		float _Depth;//�[�x���C��
		float _Near;//near�Ƃ̋���
		float _Middle;//Middle�Ƃ̋���
		float _SmoothOffset;//�X���[�X�X�e�b�v�̋��E

        v2f vert (appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            UNITY_TRANSFER_FOG(o,o.vertex);
            return o;
        }

		//�K�E�V�A���u���[�p
		fixed Gaussian(float2 uv, float2 pick, float sigma)
		{
			float r = distance(uv, pick + uv);
			float weight = exp(-(r*r) / (2 * sigma * sigma));
			return weight;
		}

		
		ENDCG

		//0�p�X�� �[�x�o�b�t�@
		Pass
		{
			CGPROGRAM

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_CameraDepthTexture, i.uv);
				//��ʑ̐[�x
				
				//���o����
				float depthRate = Linear01Depth(col.x);
				float dis = distance(_Depth, depthRate);
				float near = 1 - smoothstep(_Near - _SmoothOffset, _Near + _SmoothOffset, dis);
				float middle = mul(1 - near, 1 - smoothstep(_Middle - _SmoothOffset, _Middle + _SmoothOffset, dis));
				float far = mul(1 - near, 1 - middle);

				fixed totalWheight = 0;
				float color = fixed4(0, 0, 0, 0);
				float4 bloom = fixed4(0, 0, 0, 0);
				float2 pick = float2(0, 0);
				float pickRange = 0.15f;

				float rate = (far*0.5) + (middle*0.25);

				for (pick.y = -pickRange; pick.y <= pickRange; pick.y += 0.01)
				{
					for (pick.x = -pickRange; pick.x <= pickRange; pick.x += 0.01)
					{
						float wheight = Gaussian(i.uv, pick, pickRange);
						fixed4 tex = (far > 0.5 || middle > 0.5) ? (tex2D(_MainTex, i.uv + pick * rate)) : (tex2D(_NormalTex, i.uv + pick * rate));
						fixed4 bloomtex = tex2D(_MainTex, i.uv + pick * 0.1f);
						float aaa = step(0.9, (bloomtex.x + bloomtex.y + bloomtex.z) / 3.0);
						bloom += (far > 0.5 || middle > 0.5) ? (tex * aaa * wheight) : (0);
						color += tex * wheight;
						totalWheight += wheight;
					}
				}

				fixed4 output = color / totalWheight;
				bloom /= totalWheight;
				//�K�E�V�A���u���[
				return output + bloom;
			}

			ENDCG
		}

		//1�p�X�ځ@���̉摜�ɔ�ʑ̐[�x�Ƀu���[�������������̂��o��
		Pass
		{
			CGPROGRAM

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col2 = tex2D(_NormalTex, i.uv);
				return col + col2;
				//return col;
			}

			ENDCG
		}
    }
}
