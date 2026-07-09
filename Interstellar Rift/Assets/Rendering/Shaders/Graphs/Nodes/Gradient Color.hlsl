void Apply_Gradient_Color_float(float V,
                                float3 Core_Color, float3 Edge_Color,
                                float Sharpness, float Intensity,
                                out float3 Output)
{
    float factor = 1.0 - saturate(abs(V - 0.5) * 2.0);
    factor = pow(factor, Sharpness);

    float3 base = lerp(Edge_Color, Core_Color, factor);

    Output = base * Intensity;
}
