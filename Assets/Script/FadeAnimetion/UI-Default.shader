Shader "UI/Default"
{
    Properties
    {
        // メインテクスチャ、スプライト画像などを設定
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        // ティントカラー（色合いを変更するための色）
        _Color ("Tint", Color) = (1,1,1,1)

        // ステンシルバッファの比較関数設定
        _StencilComp ("Stencil Comparison", Float) = 8
        // ステンシルID（比較・書き込み時の基準となる値）
        _Stencil ("Stencil ID", Float) = 0
        // ステンシル操作
        _StencilOp ("Stencil Operation", Float) = 0
        // ステンシル書き込みマスク
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        // ステンシル読み取りマスク
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        // 描画のカラー出力マスク
        _ColorMask ("Color Mask", Float) = 15

        // アルファクリップ（透過領域をカットするオプション、Toggle属性でOn/Off可能）
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    // シェーダーの実行内容定義
    SubShader
    {
        // 描画設定のタグ
        Tags
        {
            "Queue"="Transparent"            // 透明物として描画順を設定
            "IgnoreProjector"="True"         // プロジェクターの影響を無視
            "RenderType"="Transparent"       // 描画タイプを透明として分類
            "PreviewType"="Plane"            // シェーダープレビューでの表示形式
            "CanUseSpriteAtlas"="True"       // スプライトアトラスを使用可能に
        }

        // ステンシル設定
        Stencil
        {
            Ref [_Stencil]                   // ステンシル参照値を指定
            Comp [_StencilComp]              // ステンシル比較関数
            Pass [_StencilOp]                // パスした際のステンシル操作
            ReadMask [_StencilReadMask]      // 読み取り時のマスク
            WriteMask [_StencilWriteMask]    // 書き込み時のマスク
        }

        // その他のレンダリング設定
        Cull Off                             // カリングオフ（両面を描画）
        Lighting Off                         // ライティングなし
        ZWrite Off                           // デプスバッファへの書き込みなし
        ZTest [unity_GUIZTestMode]          // デプステストモード
        Blend SrcAlpha OneMinusSrcAlpha      // 透過ブレンディング（アルファブレンディング）
        ColorMask [_ColorMask]              // カラーマスク（どのカラーチャンネルを出力するか）

        // 描画パスの定義
        Pass
        {
            Name "Default"                 
            CGPROGRAM
            #pragma vertex vert               // 頂点シェーダーとしてvert関数を指定
            #pragma fragment frag             // フラグメントシェーダーとしてfrag関数を指定
            #pragma target 2.0                // シェーダーモデル2.0に対応

            // 必要なUnityのCG関数をインクルード
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            // マルチコンパイルで、2Dクリッピングとアルファクリップを切り替えられるよう設定
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            // 頂点データの構造体定義
            struct appdata_t
            {
                float4 vertex   : POSITION;    // 頂点位置
                float4 color    : COLOR;       // 頂点カラー
                float2 texcoord : TEXCOORD0;   // テクスチャ座標
                UNITY_VERTEX_INPUT_INSTANCE_ID // インスタンスID
            };

            // 頂点シェーダーからフラグメントシェーダーに渡すデータ構造
            struct v2f
            {
                float4 vertex   : SV_POSITION;     // 画面空間での頂点位置
                fixed4 color    : COLOR;           // 頂点カラー
                float2 texcoord  : TEXCOORD0;     // テクスチャ座標
                float4 worldPosition : TEXCOORD1; // ワールド座標
                UNITY_VERTEX_OUTPUT_STEREO       // VRやステレオレンダリング用のID
            };

            // シェーダーで使う変数
            sampler2D _MainTex;                // メインテクスチャ
            fixed4 _Color;                     // ティントカラー
            fixed4 _TextureSampleAdd;          // テクスチャ加算値
            float4 _ClipRect;                  // 2Dクリッピングの範囲
            float4 _MainTex_ST;                // テクスチャのTiling/Offset情報

            // 頂点シェーダー関数
            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);                                  // インスタンスIDの設定
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);                  // ステレオレンダリングの初期化
                OUT.worldPosition = v.vertex;                               // ワールド座標を設定
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);       // 画面空間に変換

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);        // テクスチャ座標を変換

                OUT.color = v.color * _Color;                              // 頂点カラーにティントカラーを乗算
                return OUT;
            }

            // フラグメントシェーダー関数
            fixed4 frag(v2f IN) : SV_Target
            {
                // テクスチャカラーに加算カラーを追加し、頂点カラーで乗算
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                #ifdef UNITY_UI_CLIP_RECT
                // クリッピングを適用
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                // アルファクリップが有効なら、アルファが0に近いピクセルを破棄
                clip (color.a - 0.001);
                #endif

                return color; // カラーを返す
            }
        ENDCG
        }
    }
}
