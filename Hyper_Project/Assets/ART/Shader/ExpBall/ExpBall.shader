// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/ExpBall"
{
    Properties
    {
        _MainTexture ("Texture", 2D) = "white" {}
        
        _Lerp ("Lerp STR", Range(0,1))= .1
        _LerpDetail ("Lerp Detail", float) = 2
        _LerpTime ("Lerp Time",float) = 1
        //[HDR] _EmissionColor ("Emission Color",Color) = (0,0,0)
        _EmissionSTR ("Emission STR",Range(0,20)) = 1

        _Color ("Color", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
        _ColorLerp ("Color Lerp STR", Range(0,1))= .2
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
		_FresnelScale ("Fresnel Scale", Float) = 1
		_FresnelPower ("Fresnel Power", Float) = 1
		_StarSize ("Star Size", Float) = 1
		_StarFeather ("Star Feather", Float) = 1
        _StarsHash ("Stars Hash", Vector) = (641, -113, 271, 1117)
        _StarsDensity ("Stars Density", Range(0,1)) = 0.02
       
    }
    SubShader
    {
        Tags { "Queue"="Geometry"
                   "IgnoreProjector"="True"
                   "RenderType"="Opaque" }

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
                float fresnel : TEXCOORD1;
                half3 vertex : TEXCOORD2;
                float3 wpos : TEXCOORD3;
            };

            uniform half _StarsDensity;
            uniform half4 _StarsHash;
            sampler2D _MainTexture;
            float4 _MainTexture_ST,_AnimateXY,_EmissionColor,_FresnelColor,_Color,_Color2;
            float _LerpDetail,_Lerp,_ColorLerp,_LerpTime, _EmissionSTR,
            _FresnelScale,_FresnelPower,_StarSize,_StarFeather;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float3 direction = normalize(ObjSpaceViewDir(v.vertex));
                o.wpos = smoothstep(.4,1.5, dot(direction,v.normal));
                //o.wpos = UnityObjectToViewPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv,_MainTexture);
                o.vertex = -v.vertex;
                
                o.fresnel = _FresnelScale * pow(1 - dot(direction, v.normal), _FresnelPower);
                
                return o;
            }

            float hash21(float2 p)
            {
                p = frac( p * float2 ( 123.34 , 456.21 ));
                p += dot( p , p + 45.32 );
                return frac( p.x * p.y );
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
            
            float Star(float2 p, float size,float feather)
            {
                float2 uv = length(p);  
                float star = 1-smoothstep(size-feather,size+feather,uv);
                return star;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.yy*_LerpTime;
                float2 uv = i.uv*1;
                float2 uv2 = i.uv*55;
                float2 uvw = i.wpos*25;
                float2 gv = frac(uv)-.5;
                float2 gv2 = frac(uv2)-.5;
                float2 gvw = frac(uvw);
                float2 id = floor(uv);
                float2 id2 = floor(uv2);
                float2 idw = floor(uvw);
                float3 col = float3(0,0,0);

                float2 m = float2( 2,2 );
                float perlin = 0;
		        perlin   = 0.5000*noise( uv*_LerpDetail+time ); uv = m*uv; //0.5000
		        perlin += 0.2500*noise( uv*_LerpDetail+time ); uv = m*uv; //0.2500
		        perlin += 0.1250*noise( uv*_LerpDetail+time ); uv = m*uv; //0.1250
		        perlin += 0.0625*noise( uv*_LerpDetail+time ); uv = m*uv; //0.0625
                perlin = 0.5 + 0.5*perlin;
                perlin *= smoothstep( 0, .1, abs(uv));
            
                float2 ww = lerp(uv,perlin,_Lerp);
                fixed4 textureColor = tex2D(_MainTexture,ww/6);
                textureColor = clamp(0,1,textureColor * _EmissionSTR);

                for (int y=-1 ; y <=1 ; y++)
                {
                    for (int x=-1 ; x <=1 ; x++)
                    {
                        float2 offset = float2(x,y);

                        float n = hash21(id2+offset);
                        float size = frac(n*345.32);

                        float starss = Star(gv2-offset-float2(n,frac(n*34)),_StarSize-float2(n*25,frac(n*88)),_StarFeather);
                        col += starss*3;
                    }
                }        

                float3 col2 = i.wpos;

                return  float4(col,1)+ lerp(lerp(textureColor.z*_Color,(1-textureColor.z)*_Color2,_ColorLerp),
                                                    _FresnelColor,i.fresnel)*(1+float4(col2,1)); // one minus !

            }
            ENDCG
        }
    }
        Fallback Off

}
