// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1873,x:33440,y:32727,varname:node_1873,prsc:2|emission-2364-OUT,alpha-603-OUT,clip-9590-OUT;n:type:ShaderForge.SFN_Multiply,id:1086,x:32812,y:32818,cmnt:RGB,varname:node_1086,prsc:2|A-686-RGB,B-5983-RGB,C-5376-RGB;n:type:ShaderForge.SFN_Color,id:5983,x:32551,y:32923,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5376,x:32551,y:33079,varname:node_5376,prsc:2;n:type:ShaderForge.SFN_Multiply,id:603,x:32812,y:32992,cmnt:A,varname:node_603,prsc:2|A-5983-A,B-5376-A,C-686-A;n:type:ShaderForge.SFN_Slider,id:8063,x:30939,y:33869,ptovrint:False,ptlb:disolveValue,ptin:_disolveValue,varname:_disolveValue,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:5564,x:31795,y:33498,ptovrint:False,ptlb:disolveMask,ptin:_disolveMask,varname:_disolveMask,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:2,isnm:False|UVIN-4519-UVOUT;n:type:ShaderForge.SFN_Get,id:9590,x:32823,y:33158,varname:node_9590,prsc:2|IN-6155-OUT;n:type:ShaderForge.SFN_Set,id:6155,x:32458,y:33814,varname:disolveAlpha,prsc:2|IN-5223-OUT;n:type:ShaderForge.SFN_Lerp,id:8796,x:32055,y:33668,varname:node_8796,prsc:2|A-7827-OUT,B-5564-R,T-5550-OUT;n:type:ShaderForge.SFN_Vector1,id:7827,x:31979,y:33560,varname:node_7827,prsc:2,v1:1;n:type:ShaderForge.SFN_Lerp,id:5223,x:32297,y:33804,varname:node_5223,prsc:2|A-8796-OUT,B-8207-OUT,T-5550-OUT;n:type:ShaderForge.SFN_Vector1,id:8207,x:32055,y:33787,varname:node_8207,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:2232,x:33255,y:33290,ptovrint:False,ptlb:DisolveEdge,ptin:_DisolveEdge,varname:_DisolveEdge,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:271f5ee3273dd7f4fae6e204d4f8c4bf,ntxv:0,isnm:False|UVIN-7095-OUT;n:type:ShaderForge.SFN_Get,id:4312,x:32378,y:33510,varname:node_4312,prsc:2|IN-6155-OUT;n:type:ShaderForge.SFN_Get,id:5854,x:32791,y:32712,varname:node_5854,prsc:2|IN-3077-OUT;n:type:ShaderForge.SFN_Set,id:3077,x:33851,y:33470,varname:edgeColor,prsc:2|IN-9451-OUT;n:type:ShaderForge.SFN_Append,id:7095,x:33077,y:33326,varname:node_7095,prsc:2|A-6764-OUT,B-263-OUT;n:type:ShaderForge.SFN_Vector1,id:263,x:32900,y:33400,varname:node_263,prsc:2,v1:0;n:type:ShaderForge.SFN_OneMinus,id:3491,x:32559,y:33510,varname:node_3491,prsc:2|IN-4312-OUT;n:type:ShaderForge.SFN_RemapRange,id:6764,x:32719,y:33510,varname:node_6764,prsc:2,frmn:0,frmx:0.4,tomn:5,tomx:0.9|IN-3491-OUT;n:type:ShaderForge.SFN_Panner,id:4519,x:31629,y:33498,varname:node_4519,prsc:2,spu:0,spv:-1|UVIN-397-UVOUT,DIST-1956-OUT;n:type:ShaderForge.SFN_TexCoord,id:397,x:31475,y:33421,varname:node_397,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:3421,x:31304,y:33658,varname:node_3421,prsc:2,frmn:0,frmx:1,tomn:0,tomx:3|IN-5550-OUT;n:type:ShaderForge.SFN_Multiply,id:2364,x:33048,y:32763,varname:node_2364,prsc:2|A-5854-OUT,B-1086-OUT;n:type:ShaderForge.SFN_Tex2d,id:686,x:32551,y:32689,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1956,x:31475,y:33658,varname:node_1956,prsc:2|A-3421-OUT,B-5550-OUT;n:type:ShaderForge.SFN_RemapRange,id:5550,x:31304,y:33840,varname:node_5550,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.6|IN-8063-OUT;n:type:ShaderForge.SFN_Multiply,id:4907,x:33453,y:33468,varname:node_4907,prsc:2|A-2232-RGB,B-3401-OUT;n:type:ShaderForge.SFN_Slider,id:3401,x:33139,y:33534,ptovrint:False,ptlb:disolveEdgeValue,ptin:_disolveEdgeValue,varname:_disolveEdgeValue,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Clamp01,id:9451,x:33682,y:33408,varname:node_9451,prsc:2|IN-4907-OUT;proporder:5983-8063-5564-2232-686-3401;pass:END;sub:END;*/

Shader "Tribus/CardDisolve" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _disolveValue ("disolveValue", Range(0, 1)) = 0
        _disolveMask ("disolveMask", 2D) = "black" {}
        _DisolveEdge ("DisolveEdge", 2D) = "white" {}
        _MainTex ("MainTex", 2D) = "white" {}
        _disolveEdgeValue ("disolveEdgeValue", Range(0, 10)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _disolveValue;
            uniform sampler2D _disolveMask; uniform float4 _disolveMask_ST;
            uniform sampler2D _DisolveEdge; uniform float4 _DisolveEdge_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _disolveEdgeValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float node_5550 = (_disolveValue*0.6+0.0);
                float2 node_4519 = (i.uv0+((node_5550*3.0+0.0)*node_5550)*float2(0,-1));
                float4 _disolveMask_var = tex2D(_disolveMask,TRANSFORM_TEX(node_4519, _disolveMask));
                float disolveAlpha = lerp(lerp(1.0,_disolveMask_var.r,node_5550),0.0,node_5550);
                clip(disolveAlpha - 0.5);
////// Lighting:
////// Emissive:
                float2 node_7095 = float2(((1.0 - disolveAlpha)*-10.25+5.0),0.0);
                float4 _DisolveEdge_var = tex2D(_DisolveEdge,TRANSFORM_TEX(node_7095, _DisolveEdge));
                float3 edgeColor = saturate((_DisolveEdge_var.rgb*_disolveEdgeValue));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = (edgeColor*(_MainTex_var.rgb*_Color.rgb*i.vertexColor.rgb));
                float3 finalColor = emissive;
                return fixed4(finalColor,(_Color.a*i.vertexColor.a*_MainTex_var.a));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _disolveValue;
            uniform sampler2D _disolveMask; uniform float4 _disolveMask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float node_5550 = (_disolveValue*0.6+0.0);
                float2 node_4519 = (i.uv0+((node_5550*3.0+0.0)*node_5550)*float2(0,-1));
                float4 _disolveMask_var = tex2D(_disolveMask,TRANSFORM_TEX(node_4519, _disolveMask));
                float disolveAlpha = lerp(lerp(1.0,_disolveMask_var.r,node_5550),0.0,node_5550);
                clip(disolveAlpha - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
