float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
    return float4(1, 0, 0, 1);
}

technique Technique1
{
    pass TestShader
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}