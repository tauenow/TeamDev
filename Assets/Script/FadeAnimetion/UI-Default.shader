Shader "UI/Default"
{
    Properties
    {
        // ���C���e�N�X�`���A�X�v���C�g�摜�Ȃǂ�ݒ�
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        // �e�B���g�J���[�i�F������ύX���邽�߂̐F�j
        _Color ("Tint", Color) = (1,1,1,1)

        // �X�e���V���o�b�t�@�̔�r�֐��ݒ�
        _StencilComp ("Stencil Comparison", Float) = 8
        // �X�e���V��ID�i��r�E�������ݎ��̊�ƂȂ�l�j
        _Stencil ("Stencil ID", Float) = 0
        // �X�e���V������
        _StencilOp ("Stencil Operation", Float) = 0
        // �X�e���V���������݃}�X�N
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        // �X�e���V���ǂݎ��}�X�N
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        // �`��̃J���[�o�̓}�X�N
        _ColorMask ("Color Mask", Float) = 15

        // �A���t�@�N���b�v�i���ߗ̈���J�b�g����I�v�V�����AToggle������On/Off�\�j
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    // �V�F�[�_�[�̎��s���e��`
    SubShader
    {
        // �`��ݒ�̃^�O
        Tags
        {
            "Queue"="Transparent"            // �������Ƃ��ĕ`�揇��ݒ�
            "IgnoreProjector"="True"         // �v���W�F�N�^�[�̉e���𖳎�
            "RenderType"="Transparent"       // �`��^�C�v�𓧖��Ƃ��ĕ���
            "PreviewType"="Plane"            // �V�F�[�_�[�v���r���[�ł̕\���`��
            "CanUseSpriteAtlas"="True"       // �X�v���C�g�A�g���X���g�p�\��
        }

        // �X�e���V���ݒ�
        Stencil
        {
            Ref [_Stencil]                   // �X�e���V���Q�ƒl���w��
            Comp [_StencilComp]              // �X�e���V����r�֐�
            Pass [_StencilOp]                // �p�X�����ۂ̃X�e���V������
            ReadMask [_StencilReadMask]      // �ǂݎ�莞�̃}�X�N
            WriteMask [_StencilWriteMask]    // �������ݎ��̃}�X�N
        }

        // ���̑��̃����_�����O�ݒ�
        Cull Off                             // �J�����O�I�t�i���ʂ�`��j
        Lighting Off                         // ���C�e�B���O�Ȃ�
        ZWrite Off                           // �f�v�X�o�b�t�@�ւ̏������݂Ȃ�
        ZTest [unity_GUIZTestMode]          // �f�v�X�e�X�g���[�h
        Blend SrcAlpha OneMinusSrcAlpha      // ���߃u�����f�B���O�i�A���t�@�u�����f�B���O�j
        ColorMask [_ColorMask]              // �J���[�}�X�N�i�ǂ̃J���[�`�����l�����o�͂��邩�j

        // �`��p�X�̒�`
        Pass
        {
            Name "Default"                 
            CGPROGRAM
            #pragma vertex vert               // ���_�V�F�[�_�[�Ƃ���vert�֐����w��
            #pragma fragment frag             // �t���O�����g�V�F�[�_�[�Ƃ���frag�֐����w��
            #pragma target 2.0                // �V�F�[�_�[���f��2.0�ɑΉ�

            // �K�v��Unity��CG�֐����C���N���[�h
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            // �}���`�R���p�C���ŁA2D�N���b�s���O�ƃA���t�@�N���b�v��؂�ւ�����悤�ݒ�
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            // ���_�f�[�^�̍\���̒�`
            struct appdata_t
            {
                float4 vertex   : POSITION;    // ���_�ʒu
                float4 color    : COLOR;       // ���_�J���[
                float2 texcoord : TEXCOORD0;   // �e�N�X�`�����W
                UNITY_VERTEX_INPUT_INSTANCE_ID // �C���X�^���XID
            };

            // ���_�V�F�[�_�[����t���O�����g�V�F�[�_�[�ɓn���f�[�^�\��
            struct v2f
            {
                float4 vertex   : SV_POSITION;     // ��ʋ�Ԃł̒��_�ʒu
                fixed4 color    : COLOR;           // ���_�J���[
                float2 texcoord  : TEXCOORD0;     // �e�N�X�`�����W
                float4 worldPosition : TEXCOORD1; // ���[���h���W
                UNITY_VERTEX_OUTPUT_STEREO       // VR��X�e���I�����_�����O�p��ID
            };

            // �V�F�[�_�[�Ŏg���ϐ�
            sampler2D _MainTex;                // ���C���e�N�X�`��
            fixed4 _Color;                     // �e�B���g�J���[
            fixed4 _TextureSampleAdd;          // �e�N�X�`�����Z�l
            float4 _ClipRect;                  // 2D�N���b�s���O�͈̔�
            float4 _MainTex_ST;                // �e�N�X�`����Tiling/Offset���

            // ���_�V�F�[�_�[�֐�
            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);                                  // �C���X�^���XID�̐ݒ�
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);                  // �X�e���I�����_�����O�̏�����
                OUT.worldPosition = v.vertex;                               // ���[���h���W��ݒ�
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);       // ��ʋ�Ԃɕϊ�

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);        // �e�N�X�`�����W��ϊ�

                OUT.color = v.color * _Color;                              // ���_�J���[�Ƀe�B���g�J���[����Z
                return OUT;
            }

            // �t���O�����g�V�F�[�_�[�֐�
            fixed4 frag(v2f IN) : SV_Target
            {
                // �e�N�X�`���J���[�ɉ��Z�J���[��ǉ����A���_�J���[�ŏ�Z
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                #ifdef UNITY_UI_CLIP_RECT
                // �N���b�s���O��K�p
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                // �A���t�@�N���b�v���L���Ȃ�A�A���t�@��0�ɋ߂��s�N�Z����j��
                clip (color.a - 0.001);
                #endif

                return color; // �J���[��Ԃ�
            }
        ENDCG
        }
    }
}
