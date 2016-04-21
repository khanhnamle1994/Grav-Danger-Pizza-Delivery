using UnityEngine;

public static class RendererExtensions {
	// checks if visible by Camera
	public static bool IsVisibileFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (camera);
		return GeometryUtility.TestPlanesAABB (planes, renderer.bounds);
	}
}
