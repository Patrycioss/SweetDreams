void ToonShading_float(in float3 Normal, in float ToonRampSmoothness, in float3 ClipSpacePos, in float3 WorldPos, in float4 ToonRampTinting,
in float ToonRampOffset, out float3 ToonRampOutput, out float3 Direction)
{
	#ifdef SHADERGRAPH_PREVIEW
	ToonRampOutput = float3(0.5,0.5,0);
	Direction = float3(0.5,0.5,0);
	#else
	#endif
	#if SHADOWS_SCREEN
	half4 shadowCoord = ComputeScreenPos(ClipSpacePos);
	#else
	#endif
}