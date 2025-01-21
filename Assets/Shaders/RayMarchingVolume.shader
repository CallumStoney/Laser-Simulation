Shader "Custom/ElectronCloud"
{
    Properties
    {
        _MainColor("Cloud Color", Color) = (0.0, 0.5, 1.0, 1.0) // Color of the electron cloud
        _Density("Density", Float) = 1.0                         // Maximum density
        _Radius("Radius", Float) = 1.0                           // Size of the electron cloud
        _Exponent("Falloff Exponent", Float) = 2.0               // Sharpness of the cloud's edges
        _ParentPosition("Parent Position", Vector) = (0.0, 0.0, 0.0, 0.0) // Position of the parent object
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _MainColor;
            float _Density;
            float _Radius;
            float _Exponent;
            float4 _ParentPosition;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; // Transform vertex to world space
                return o;
            }

            float ElectronCloudDensity(float3 pos, float3 parentPos, float radius, float exponent)
            {
                float dist = length(pos - parentPos);              // Distance from the parent position
                float normalizedDist = dist / radius;              // Normalize distance by radius
                return exp(-pow(normalizedDist, exponent));        // Exponential falloff
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 localPos = i.worldPos;                      // Use local position for density calculation
                float density = ElectronCloudDensity(localPos, _ParentPosition.xyz, _Radius, _Exponent);
                density *= _Density;                               // Scale by the density factor
                return float4(_MainColor.rgb, density);            // Color weighted by density
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}