Shader "Custom/ElectronCloud"
{
    Properties
    {
        _MainColor("Cloud Color", Color) = (0.0, 0.5, 1.0, 1.0) //comment: color of the electron cloud
        _Density("Density", Float) = 1.0 //comment: maximum density of the cloud
        _Radius("Radius", Float) = 1.0 //comment: size of the electron cloud
        _Exponent("Falloff Exponent", Float) = 2.0 //comment: controls sharpness of the cloud edges
        _ParentPosition("Parent Position", Vector) = (0.0, 0.0, 0.0, 0.0) //comment: position of the parent object
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" } //comment: transparency settings
        LOD 100 //comment: level of detail

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha //comment: alpha blending for transparency
            ZWrite Off //comment: disable depth writing for transparency

            CGPROGRAM
            #pragma vertex vert //comment: vertex shader
            #pragma fragment frag //comment: fragment shader
            #include "UnityCG.cginc" //comment: include Unity's shader library

            struct appdata
            {
                float4 vertex : POSITION; //comment: vertex position
            };

            struct v2f
            {
                float4 pos : SV_POSITION; //comment: position for rendering
                float3 worldPos : TEXCOORD0; //comment: position in world space
            };

            float4 _MainColor; //comment: color of the cloud
            float _Density; //comment: overall density
            float _Radius; //comment: cloud radius
            float _Exponent; //comment: sharpness of the falloff
            float4 _ParentPosition; //comment: position of the cloud's parent

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); //comment: transforms vertex to clip space
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; //comment: transforms vertex to world space
                return o;
            }

            float ElectronCloudDensity(float3 pos, float3 parentPos, float radius, float exponent)
            {
                float dist = length(pos - parentPos); //comment: distance from the parent object
                float normalizedDist = dist / radius; //comment: normalize distance by the cloud radius
                return exp(-pow(normalizedDist, exponent)); //comment: falloff calculation
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 localPos = i.worldPos; //comment: local position for density calculation
                float density = ElectronCloudDensity(localPos, _ParentPosition.xyz, _Radius, _Exponent); //comment: calculate density
                density *= _Density; //comment: scale by density factor
                return float4(_MainColor.rgb, density); //comment: color with alpha based on density
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse" //comment: fallback shader
}