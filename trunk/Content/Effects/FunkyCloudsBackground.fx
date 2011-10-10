//
// FunkyCloudsBackground.fx
//

uniform extern float4x4 World;
uniform extern float4x4 View;
uniform extern float4x4 Projection;
uniform extern float MusicData[8];
uniform extern float SmoothMusicData[8];
uniform extern texture NoiseTexture;
uniform extern float Time;

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

    float4x4 world = World;
    float4x4 projection = Projection;
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
    float4 sky_color = float4(0.3f, 0.4f, 0.7f, 0.0f);
    float4 result = sky_color;

    float m = MusicData[1] + MusicData[2] + MusicData[3];
    float sm = SmoothMusicData[0] + SmoothMusicData[1] + SmoothMusicData[3] + SmoothMusicData[4];
    float2 uv = vertex.uv;
    uv.x += (sm * sm * sm * sm * sm) * 4.0f;
    uv.y -= Time * 0.005f;

    float4 n = tex2D(NoiseSampler, uv).rgba;

    result += n * 0.5f;

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
