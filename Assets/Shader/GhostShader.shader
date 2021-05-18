shader "Custom/GhostShader" {
    properties{
      _MainTex("main texture", 2d) = "white" {}
      _RainTex("rain texture", 2d) = "white" {}
      _ShiftTex("shift texture", 2d) = "white" {}
      _NoiseTex("noise texture", 2d) = "white" {}
      _RainColour("rain colour", Color) = (0.25, 0.25, 0.25, 1)
      _RainSpeed("rain speed", float) = 1.0
      _ShiftWidth("shift width", float) = 2.0
      _ShiftStrength("shift strength", float) = 0.015
      _NoisePickSpeedX("noise pick speed x", float) = 5.0
      _NoisePickSpeedY("noise pick speed y", float) = 0.5
    }
        subShader{
          tags { "RenderType" = "Transparent" "Queue" = "Geometry" "IgnoreProjector" = "True" }
          pass {
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vs
            #pragma fragment fs
            sampler2D _MainTex;
            sampler2D _RainTex;
            sampler2D _ShiftTex;
            sampler2D _NoiseTex;
            float4 _RainColour;
            float _RainSpeed;
            float _ShiftWidth;
            float _ShiftStrength;
            float _NoisePickSpeedX;
            float _NoisePickSpeedY;
            struct appdata {
              float4 vertex : POSITION;
              float2 texcoord : TEXCOORD0;
            };
            struct v2f {
              float4 vertex : SV_POSITION;
              float2 texcoord : TEXCOORD0;
            };
            v2f vs(appdata i)
            {
              v2f o;
              o.vertex = UnityObjectToClipPos(i.vertex);
              o.texcoord = i.texcoord;
              return o;
            }
            fixed4 fs(v2f i) : SV_TARGET
            {
              float shift = tex2D(_ShiftTex, i.texcoord * _ShiftWidth).r;
              float noise = tex2D(_NoiseTex, float2(shift + _Time.x * _NoisePickSpeedX, _Time.x * _NoisePickSpeedY)).r * _ShiftStrength;
              fixed4 rain = tex2D(_RainTex, float2(i.texcoord.x + sin(_Time.x), i.texcoord.y * 0.5 + _Time.y * _RainSpeed)) * _RainColour;
              fixed4 main = tex2D(_MainTex, float2(i.texcoord.x, i.texcoord.y + noise));
              return main + rain;
            }
            ENDCG
          }
      }
          fallBack "Diffuse"
}