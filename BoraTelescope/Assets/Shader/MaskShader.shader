Shader "Unlit/MaskShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags{"Queue" = "Transparent"}

        ColorMask 0
        ZWrite On

        Pass{}
    }
}
