// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1873,x:33229,y:32719,varname:node_1873,prsc:2|normal-4422-RGB,emission-3441-OUT,alpha-603-OUT;n:type:ShaderForge.SFN_Multiply,id:1086,x:32812,y:32818,cmnt:RGB,varname:node_1086,prsc:2|A-5044-RGB,B-5983-RGB,C-5376-RGB;n:type:ShaderForge.SFN_Color,id:5983,x:32531,y:33026,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5376,x:32531,y:33205,varname:node_5376,prsc:2;n:type:ShaderForge.SFN_Multiply,id:603,x:32830,y:33029,cmnt:A,varname:node_603,prsc:2|A-4479-A,B-5983-A,C-5376-A,D-2278-OUT;n:type:ShaderForge.SFN_Slider,id:2755,x:30866,y:32421,ptovrint:False,ptlb:PulsingPeriod,ptin:_PulsingPeriod,varname:_PulsingPeriod,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Time,id:54,x:30996,y:32159,varname:node_54,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7696,x:31237,y:32261,varname:node_7696,prsc:2|A-54-T,B-2755-OUT;n:type:ShaderForge.SFN_Sin,id:1589,x:31392,y:32261,varname:node_1589,prsc:2|IN-7696-OUT;n:type:ShaderForge.SFN_Abs,id:74,x:31556,y:32261,varname:node_74,prsc:2|IN-1589-OUT;n:type:ShaderForge.SFN_Set,id:7537,x:31934,y:32271,varname:pulsing,prsc:2|IN-8492-OUT;n:type:ShaderForge.SFN_Get,id:2278,x:32531,y:33319,varname:node_2278,prsc:2|IN-7537-OUT;n:type:ShaderForge.SFN_If,id:8492,x:31768,y:32271,varname:node_8492,prsc:2|A-2755-OUT,B-5915-OUT,GT-74-OUT,EQ-8292-OUT,LT-74-OUT;n:type:ShaderForge.SFN_Vector1,id:5915,x:31556,y:32435,varname:node_5915,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8292,x:31556,y:32494,varname:node_8292,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:5044,x:32525,y:32575,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ad0472af282bc544ea309a7fd735ac83,ntxv:3,isnm:False|UVIN-6620-OUT;n:type:ShaderForge.SFN_Panner,id:9095,x:31462,y:32786,varname:node_9095,prsc:2,spu:0.1,spv:0|UVIN-5540-UVOUT,DIST-7870-OUT;n:type:ShaderForge.SFN_TexCoord,id:479,x:30958,y:32624,varname:node_479,prsc:2,uv:0;n:type:ShaderForge.SFN_Rotator,id:5540,x:31138,y:32624,varname:node_5540,prsc:2|UVIN-479-UVOUT,SPD-8505-OUT;n:type:ShaderForge.SFN_Slider,id:8505,x:30944,y:32842,ptovrint:False,ptlb:RotatorSpeed,ptin:_RotatorSpeed,varname:_RotatorSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.1714749,max:1;n:type:ShaderForge.SFN_Slider,id:5913,x:30961,y:32990,ptovrint:False,ptlb:PanerSpeed,ptin:_PanerSpeed,varname:_PanerSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.218617,max:1;n:type:ShaderForge.SFN_Multiply,id:7870,x:31298,y:32990,varname:node_7870,prsc:2|A-5913-OUT,B-28-T;n:type:ShaderForge.SFN_Time,id:28,x:31040,y:33118,varname:node_28,prsc:2;n:type:ShaderForge.SFN_Set,id:4946,x:31680,y:32786,varname:textureUV,prsc:2|IN-9095-UVOUT;n:type:ShaderForge.SFN_Get,id:6620,x:32307,y:32575,varname:node_6620,prsc:2|IN-4946-OUT;n:type:ShaderForge.SFN_Tex2d,id:4422,x:32812,y:32552,ptovrint:False,ptlb:NormalMap,ptin:_NormalMap,varname:_NormalMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dd000b3e61365c142bcce6554c447007,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:3490,x:31454,y:33269,ptovrint:False,ptlb:DistoreMap,ptin:_DistoreMap,varname:_DistoreMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a1d16533ef4fabd4ab46ad09985d5a08,ntxv:3,isnm:True|UVIN-7957-OUT;n:type:ShaderForge.SFN_Lerp,id:2540,x:31694,y:33286,varname:node_2540,prsc:2|A-2549-UVOUT,B-3490-R,T-8122-OUT;n:type:ShaderForge.SFN_TexCoord,id:2549,x:31456,y:33463,varname:node_2549,prsc:2,uv:0;n:type:ShaderForge.SFN_Slider,id:8122,x:31299,y:33657,ptovrint:False,ptlb:Distore,ptin:_Distore,varname:_Distore,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.02991458,max:0.1;n:type:ShaderForge.SFN_Get,id:7957,x:31268,y:33283,varname:node_7957,prsc:2|IN-4946-OUT;n:type:ShaderForge.SFN_Set,id:9065,x:31941,y:33285,varname:DistoreUV,prsc:2|IN-2540-OUT;n:type:ShaderForge.SFN_Get,id:4783,x:32334,y:32802,varname:node_4783,prsc:2|IN-9065-OUT;n:type:ShaderForge.SFN_Multiply,id:3441,x:33031,y:32818,varname:node_3441,prsc:2|A-1086-OUT,B-5462-OUT;n:type:ShaderForge.SFN_Slider,id:5462,x:32786,y:33199,ptovrint:False,ptlb:Saturate,ptin:_Saturate,varname:_Saturate,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.051282,max:10;n:type:ShaderForge.SFN_Tex2d,id:4479,x:32525,y:32802,ptovrint:False,ptlb:MaskTex,ptin:_MaskTex,varname:_MaskTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4783-OUT;proporder:4479-5044-5983-4422-3490-2755-8505-5913-8122-5462;pass:END;sub:END;*/

Shader "Tribus/Cell" {
    Properties {
        _MaskTex ("MaskTex", 2D) = "white" {}
        _MainTex ("MainTex", 2D) = "bump" {}
        _Color ("Color", Color) = (1,1,1,1)
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _DistoreMap ("DistoreMap", 2D) = "bump" {}
        _PulsingPeriod ("PulsingPeriod", Range(0, 1)) = 0
        _RotatorSpeed ("RotatorSpeed", Range(-1, 1)) = 0.1714749
        _PanerSpeed ("PanerSpeed", Range(-1, 1)) = 0.218617
        _Distore ("Distore", Range(0, 0.1)) = 0.02991458
        _Saturate ("Saturate", Range(0, 10)) = 2.051282
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
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _PulsingPeriod;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _RotatorSpeed;
            uniform float _PanerSpeed;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _DistoreMap; uniform float4 _DistoreMap_ST;
            uniform float _Distore;
            uniform float _Saturate;
            uniform sampler2D _MaskTex; uniform float4 _MaskTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
////// Lighting:
////// Emissive:
                float4 node_28 = _Time + _TimeEditor;
                float4 node_3848 = _Time + _TimeEditor;
                float node_5540_ang = node_3848.g;
                float node_5540_spd = _RotatorSpeed;
                float node_5540_cos = cos(node_5540_spd*node_5540_ang);
                float node_5540_sin = sin(node_5540_spd*node_5540_ang);
                float2 node_5540_piv = float2(0.5,0.5);
                float2 node_5540 = (mul(i.uv0-node_5540_piv,float2x2( node_5540_cos, -node_5540_sin, node_5540_sin, node_5540_cos))+node_5540_piv);
                float2 textureUV = (node_5540+(_PanerSpeed*node_28.g)*float2(0.1,0));
                float2 node_6620 = textureUV;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_6620, _MainTex));
                float3 emissive = ((_MainTex_var.rgb*_Color.rgb*i.vertexColor.rgb)*_Saturate);
                float3 finalColor = emissive;
                float2 node_7957 = textureUV;
                float3 _DistoreMap_var = UnpackNormal(tex2D(_DistoreMap,TRANSFORM_TEX(node_7957, _DistoreMap)));
                float2 DistoreUV = lerp(i.uv0,float2(_DistoreMap_var.r,_DistoreMap_var.r),_Distore);
                float2 node_4783 = DistoreUV;
                float4 _MaskTex_var = tex2D(_MaskTex,TRANSFORM_TEX(node_4783, _MaskTex));
                float node_8492_if_leA = step(_PulsingPeriod,0.0);
                float node_8492_if_leB = step(0.0,_PulsingPeriod);
                float4 node_54 = _Time + _TimeEditor;
                float node_74 = abs(sin((node_54.g*_PulsingPeriod)));
                float pulsing = lerp((node_8492_if_leA*node_74)+(node_8492_if_leB*node_74),1.0,node_8492_if_leA*node_8492_if_leB);
                return fixed4(finalColor,(_MaskTex_var.a*_Color.a*i.vertexColor.a*pulsing));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
