//
// FunkyClouds.fx
//

uniform extern float4x4 World : World;
uniform extern float4x4 View : View;
uniform extern float4x4 Projection : Projection;
uniform extern float MusicData[8];
uniform extern texture CloudTexture;

sampler CloudSampler = sampler_state
{
    Texture = <CloudTexture>;
    mipfilter = LINEAR; 
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
    
    float4x4 wvp = World * View * Projection;
    
    float4 p = mul(float4(Position.xyz, 1.0f), wvp);

    result.position = p;
    result.color = Color;
    result.uv = TextureCoordinate;
    result.position_ = p;

    return result;
}

float4 PixelShaderFunction( VertexOut vertex ) : COLOR
{
    float4 t = tex2D(CloudSampler, vertex.uv).rgba;
    float4 result = t * vertex.color;
    return float4(1,0,0,1);

    result.r += MusicData[0] * 0.01f;
    result.g += MusicData[1] * 0.01f;
    result.b += MusicData[2] * 0.01f;


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
