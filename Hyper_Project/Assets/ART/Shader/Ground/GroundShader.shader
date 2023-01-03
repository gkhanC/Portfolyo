// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/ExpBall"
{
    Properties
    {
        _SandColor ("Sand Color", Color) = (1,1,1,1)
        _MainTexture ("First Texture", 2D) = "white" {}
        _MainTextureNormal ("First Texture Normal", 2D) = "white" {}
        _MainTexture2 ("Second Texture", 2D) = "white" {}
        _MainTexture3 ("Third Texture", 2D) = "white" {}
        
        _Lerp1 ("1. Lerp STR", Range(0,2))= .1
		_LerpFeather ("Lerp Feather", range(0,2)) = 1
        _LerpDetail ("Lerp Detail", float) = 2
        _Lerp2 ("2. Lerp STR", Range(0,2))= .1

       
    }
    SubShader
    {
        Tags { "Queue"="Geometry"
                   "IgnoreProjector"="True"
                   "RenderType"="Opaque"
                   "LightMode" = "ForwardBase" }

        LOD 100
        //Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 tbn[3] : TEXCOORD1;
                half3 vertex : TEXCOORD2;
            };

            sampler2D _MainTexture,_MainTexture2,_MainTexture3,_MainTextureNormal;
            float4 _MainTexture_ST,_MainTexture2_ST,_MainTexture3_ST,_MainTextureNormal_ST,_SandColor;
            float _LerpDetail,_Lerp1,_Lerp2,_LerpFeather;
            

            v2f vert (appdata v)
            {
                v2f o;
                
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv,_MainTexture);
                o.normal = TRANSFORM_TEX(v.normal,_MainTextureNormal);
                o.vertex = v.vertex;
                
                return o;
            }

            float hash(float2 p) 
            {
                p  = 50*frac( p*.3183099 + float2(.71,.113));
                return 1-2*frac( p.x*p.y*(p.x*p.y) );
            }

                float noise(float2 p )
            {
                float2 i = floor( p );
                float2 f = frac( p );
	            float2 u = f*f*(3-2*f);

            return lerp( lerp( hash( i + float2(0,0) ), 
                                            hash( i + float2(1,0) ), u.x),
                                  lerp( hash( i + float2(0,1) ), 
                                            hash( i + float2(1,1) ), u.x), u.y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv*1;
               
                float2 gv = frac(uv)-.5;
               
                float2 id = floor(uv);
               
               float3 col = float3(0,0,0);

                float2 m = float2( 2,2 );
                float perlin = 0;
		        perlin   = 0.5000*noise( uv*_LerpDetail ); uv = m*uv; //0.5000
		        perlin += 0.2500*noise( uv*_LerpDetail); uv = m*uv; //0.2500
		        perlin += 0.1250*noise( uv*_LerpDetail); uv = m*uv; //0.1250
		        perlin += 0.0625*noise( uv*_LerpDetail); uv = m*uv; //0.0625
                perlin = 0.5 + 0.5*perlin;
                float perlin2 = 0.2 + 0.85*perlin;
                perlin *= smoothstep( 0, .1, abs(uv));
                perlin2 *= smoothstep( 0, .1, abs(uv+.5));
            

                half4 tex1 = tex2D(_MainTexture,uv/6);
                half4 tex2 = tex2D(_MainTexture2,uv/5);
                half4 tex3 = tex2D(_MainTexture3,uv/3);
               
               float3 normal = normalize(i.normal);
                return normalize(tex2D(_MainTextureNormal,i.normal));
                return lerp(lerp(tex1*_SandColor,tex2,smoothstep(_Lerp1 - _LerpFeather,_Lerp1 + _LerpFeather,perlin)),
                                                      tex3,smoothstep(_Lerp2 - _LerpFeather,_Lerp2 + _LerpFeather,perlin2));


            }
            ENDCG
        }
    }
        Fallback Off

}
