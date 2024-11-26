Shader "Unlit/FadeIN"
{
	Properties

	{	
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}	// �e�N�X�`�����w��
		_Color("Tint", Color) = (1,1,1,1)								// �e�N�X�`���ɓK�p����F
		_Alpha ("Time", Range(0, 1)) = 0								// �t�F�[�h�𐧌䂷�邽�߂̕ϐ�(0 : ����, 1 : �s����)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				half2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;
			uniform fixed _Alpha;
			sampler2D _MainTex;

			// ���_�V�F�[�_�[
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				// �I�u�W�F�N�g��Ԃ̒��_�ʒu���X�N���[����Ԃɕϊ�
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				
				// �e���_�ɒ�߂�ꂽUV�ݒ�
				OUT.texcoord = IN.texcoord;
#ifdef UNITY_HALF_TEXEL_OFFSET	// �e�N�X�`���̃Y����␳����}�N��
				OUT.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1,1);
#endif
				return OUT;
			}

			// �t���O�����g�V�F�[�_�[
			fixed4 frag(v2f IN) : SV_Target
			{
				// �e�N�X�`���̃��l��alpha�Ɋi�[����
				half alpha = tex2D(_MainTex, IN.texcoord).a;
				
				// saturate�Ōv�Z���ʂ�0, 1�͈̔͂ɐ�������
				alpha = saturate(1 - alpha - (_Alpha * 2 - 1));		

				return fixed4(_Color.r, _Color.g, _Color.b, alpha);
			}
			ENDCG
		}
	}

	FallBack "UI/Default"
}