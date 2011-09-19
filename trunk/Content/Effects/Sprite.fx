//
// Sprite.fx
//

//
// INPUT
//
uniform extern float4x4 WorldViewProjection : WORLDVIEWPROJECTION;
uniform extern texture UserTexture;

//
// VERTEX OUTPUT
//
struct VertexOut
{
    float4 position : POSITION;
    float4 color : COLOR0;
    float4 texture_coordinate : TEXCOORD0;
};


//
// VERTEX
//
VertexOut VertexShaderFunction(
    float4 Position  : POSITION, 
    float4 Color : COLOR0,
    float4 TextureCoordinate : TEXCOORD0 )
{
    VertexOut result = (VertexOut)0;
    
    result.position = mul(Position, WorldViewProjection);
    result.color = Color;
    result.texture_coordinate = TextureCoordinate;

    return result;
}

//
// SAMPLER
//
sampler TextureSampler = sampler_state
{
    // TODO: learn the texture specification system
    Texture = <UserTexture>;
    mipfilter = LINEAR; 
};

//
// PIXEL
//
float4 PixelShaderFunction( VertexOut vertex ) : COLOR
{
    float4 texture_rgba = tex2D(TextureSampler, vertex.texture_coordinate ).rgba;
    float4 result = vertex.color * texture_rgba;
    return result;
}

//
// TECHNIQUE
//
technique TransformTechnique
{
    pass P0
    {
        vertexShader = compile vs_2_0 VertexShaderFunction();
        pixelShader = compile ps_2_0 PixelShaderFunction();
    }
}