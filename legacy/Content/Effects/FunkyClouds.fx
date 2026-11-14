//
// FunkyClouds.fx
//

uniform extern float4x4 World : World;
uniform extern float4x4 Projection : Projection;
uniform extern float MusicData[8];
uniform extern float SmoothMusicData[8];
uniform extern texture CloudTexture;
uniform extern texture NoiseTexture;
uniform extern float Time;

sampler CloudSampler = sampler_state
{
    Texture = <CloudTexture>;
    mipfilter = LINEAR; 
    addressu = WRAP;
    addressv = WRAP;
};

sampler NoiseSampler = sampler_state
{
    Texture = <NoiseTexture>;
    mipfilter = LINEAR;
    addressu = WRAP;
    addressv = WRAP; 
};


struct VertexOut
{
    float4 position : POSITION;
    float4 color : COLOR0;
    float2 uv : TEXCOORD0;
    float4 position_ : TEXCOORD1;
};


VertexOut VertexShaderFunction(
    float4 Position  : POSITION, 
    float4 Color : COLOR0,
    float2 TextureCoordinate : TEXCOORD0
    )
{
    VertexOut result = (VertexOut)0;

    float4x4 wp = mul(World, Projection);
    float4 p = mul(float4(Position.xyz, 1.0f), wp);

    result.position = p;
    result.color = Color;
    result.uv = TextureCoordinate;
    result.position_ = p;

    return result;
}

float4 PixelShaderFunction( VertexOut vertex ) : COLOR
{
    float2 uv = vertex.uv;
    float4 t = tex2D(CloudSampler, vertex.uv).rgba;
    float4 result = t;
    return result;
}

technique TransformTechnique
{
    pass P0
    {
        vertexShader = compile vs_2_0 VertexShaderFunction();
        pixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
