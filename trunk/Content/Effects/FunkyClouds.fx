//
// FunkyClouds.fx
//

uniform extern float4x4 World : World;
uniform extern float4x4 View : View;
uniform extern float4x4 Projection : Projection;
uniform extern float MusicData[8];


struct VertexOut
{
    float4 position : POSITION;
    float4 color : COLOR0;
    float4 position_ : TEXCOORD0;
};


VertexOut VertexShaderFunction(
    float4 Position  : POSITION, 
    float4 Color : COLOR0
    )
{
    VertexOut result = (VertexOut)0;
    
    float4x4 wvp = World * View * Projection;
    
    float4 p = mul(float4(Position.xyz, 1.0f), wvp);

    result.position = p;
    result.color = Color;
    result.position_ = p;

    return result;
}

float4 PixelShaderFunction( VertexOut vertex ) : COLOR
{
    float4 result = vertex.color;

    result = float4(
        vertex.position_.x,
        vertex.position_.y,
        MusicData[0],
        1.0f
    );

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