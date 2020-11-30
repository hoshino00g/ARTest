// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TransparentShadowCollector"
{
    Properties
    {
        _ShadowIntensity ("Shadow Intensity", Range (0, 1)) = 0.6
    }
 
 
    SubShader
    {
 
        Tags {"Queue"="AlphaTest" }
 
        Pass
        {
            Tags {"LightMode" = "ForwardBase" }
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert//こちらも関数変形
            #pragma fragment frag//関数変形
            #pragma multi_compile_fwdbase
 
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            uniform float _ShadowIntensity;
 
            struct v2f
            {
                float4 pos : SV_POSITION;
                LIGHTING_COORDS(0,1)
            };
            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);//ラスタライザ(画像化すること)するときにレンダリングするべき空間のことをクリップ空間
                TRANSFER_VERTEX_TO_FRAGMENT(o);//ライトの情報を適切にフラグメントシェーダに渡す
               
                return o;
            }
            fixed4 frag(v2f i) : COLOR
            {
                float attenuation = LIGHT_ATTENUATION(i);//LIGHT_ATTENUATIONマクロライトの減衰率を計算します。
                return fixed4(0,0,0,(1-attenuation)*_ShadowIntensity);
            }
            ENDCG
        }
 
    }
    Fallback "VertexLit"
}
