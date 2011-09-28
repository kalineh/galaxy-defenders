//
// Sprite.fx
//

uniform extern float4x4 World : World;
uniform extern float4x4 View : View;
uniform extern float4x4 Projection : Projection;


//float noise(int x)
//{
    //x = (x << 13) ^ x;
    //return 1.0f - ((x * (x * x * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0f;
//}

//If you don't mind crappy randomness, a classic method is
//
// x[n+1] = (x[n] * x[n] + C) mod N
//
//where C and N are constants, C != 0 and C != -2, and N is prime. This is a typical pseudorandom generator for Pollard Rho factoring. Try C = 1 and N = 8051, those work ok.

float noise(float x)
{
    return x * x;
    return sin((x + x * x * (x * x - x)) % 8051);
}

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
        1.0f,
        1.0f
    );

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