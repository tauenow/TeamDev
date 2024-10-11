Shader "Unlit/Transition"
{
	Properties
	{
		// ���C���̃e�N�X�`�����`(�f�t�H���g�͐F)
		_MainTex("Texture", 2D) = "white" {}
		// 臒l��ݒ肷�鏊
		_Val("Val", Range(-1.0, 1.0)) = 1.0
	}
		
	SubShader
	{
		// �`��ݒ�
		Tags { "RenderType" = "Opaque" }
		LOD 100

		// �A���t�@�u�����h�ݒ�
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			// ���_�V�F�[�_�[�ƃt���O�����g�V�F�[�_�[���`
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// �t�H�O��L����
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			// ���_�V�F�[�_�[�ɓn�����f�[�^�\����
			struct appdata
			{
				float4 vertex : POSITION;		// ���_�̍��W
				float2 uv : TEXCOORD0;			// �e�N�X�`�����W
			};

			// ���_�V�F�[�_�[����t���O�����g�V�F�[�_�[�ɓn�����f�[�^�\����
			struct v2f
			{
				float2 uv : TEXCOORD0;			// �e�N�X�`�����W
				UNITY_FOG_COORDS(1)				// �t�H�O�p�̃f�[�^
				float4 vertex : SV_POSITION;	// �N���b�v��Ԃł̒��_�ʒu
			};

			sampler2D _MainTex;		// ���C���e�N�X�`��
			float4 _MainTex_ST;		// �e�N�X�`���̕ύX�s��(�X�P�[���ƃI�t�Z�b�g)

			// 臒l�p�̕ϐ�(�X���C�_�[�Őݒ�\)
			float _Val;

			// ���_�f�[�^���N���b�v��Ԃɕϊ�
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);		// ���f����Ԃ̍��W���N���b�v��Ԃɕϊ�
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);			// �e�N�X�`�����W�̕ϊ�
				UNITY_TRANSFER_FOG(o,o.vertex);					// �t�H�O�̌v�Z�𒸓_�f�[�^�ɒǉ�
				return o;
			}

			// �s�N�Z�����Ƃ̐F���v�Z
			fixed4 frag(v2f i) : SV_Target
			{
				// �e�N�X�`������T���v�����O���ĐF���擾
				fixed4 col = tex2D(_MainTex, i.uv);

				// �e�N�X�`���̐ԐF������臒l���r���A�o�͂��v�Z
				float output = saturate(col.x - _Val);

				/*
				// �t�H�O�̓K�p
				UNITY_APPLY_FOG(i.fogCoord, col);
				*/

				// 臒l�����ύX(�o�͐F��Ԃ�)
				return fixed4(col.xyz, output);
			}
			ENDCG
		}
	}
}