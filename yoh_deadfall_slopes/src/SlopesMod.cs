using UnityEngine;
using VoxelTycoon;
using VoxelTycoon.Modding;
using VoxelTycoon.Tools.TrackBuilder;

namespace Yoh.VoxelTycoon.Slopes;

public sealed class SlopesMod : Mod
{
    protected override void Deinitialize()
    {
		base.Deinitialize();

		var multiplier = WorldSettings.Current.GetInt<SlopesModSettings>(SlopesModSettings.RailSlopeLengthMultiplier);

		UpdateSlope(RailPathfinder.Slope);
		UpdateSlope(RailPathfinder.SlopeRotated);
		
		UpdateSlope(RailPathfinder.SlopeIn);
		UpdateSlope(RailPathfinder.SlopeInRotated);
		
		UpdateSlope(RailPathfinder.SlopeInOut);
		UpdateSlope(RailPathfinder.SlopeInOutRotated);

		UpdateSlope(RailPathfinder.SlopeOut);
		UpdateSlope(RailPathfinder.SlopeOutRotated);

		void UpdateSlope(NodeType node)
		{
			node.Width /= multiplier;
			node.Offset /= multiplier;
		}

		UpdateCurve(R.Curves.Rails.DiagonalSlopeIn, static (ref Vector3 point) => ref point.x);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeIn, static (ref Vector3 point) => ref point.z);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeInOut, static (ref Vector3 point) => ref point.x);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeInOut, static (ref Vector3 point) => ref point.x);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeOut, static (ref Vector3 point) => ref point.z);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeOut, static (ref Vector3 point) => ref point.z);

		UpdateCurve(R.Curves.Rails.StraightSlopeIn, static (ref Vector3 point) => ref point.z);
		UpdateCurve(R.Curves.Rails.StraightSlopeInOut, static (ref Vector3 point) => ref point.z);
		UpdateCurve(R.Curves.Rails.StraightSlopeOut, static (ref Vector3 point) => ref point.z);

		void UpdateCurve(Curve curve, GetCoordinateReference getCoordinate) =>
			UpdateCurveBy(1.0f / multiplier, curve, getCoordinate);
    }

    protected override void Initialize()
    {
        base.Initialize();

		var multiplier = WorldSettings.Current.GetInt<SlopesModSettings>(SlopesModSettings.RailSlopeLengthMultiplier);

		UpdateSlope(RailPathfinder.Slope);
		UpdateSlope(RailPathfinder.SlopeRotated);
		
		UpdateSlope(RailPathfinder.SlopeIn);
		UpdateSlope(RailPathfinder.SlopeInRotated);
		
		UpdateSlope(RailPathfinder.SlopeInOut);
		UpdateSlope(RailPathfinder.SlopeInOutRotated);

		UpdateSlope(RailPathfinder.SlopeOut);
		UpdateSlope(RailPathfinder.SlopeOutRotated);

		void UpdateSlope(NodeType node)
		{
			node.Width *= multiplier;
			node.Offset *= multiplier;
		}

		UpdateCurve(R.Curves.Rails.DiagonalSlopeIn, static (ref Vector3 point) => ref point.x);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeIn, static (ref Vector3 point) => ref point.z);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeInOut, static (ref Vector3 point) => ref point.x);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeInOut, static (ref Vector3 point) => ref point.z);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeOut, static (ref Vector3 point) => ref point.x);
		UpdateCurve(R.Curves.Rails.DiagonalSlopeOut, static (ref Vector3 point) => ref point.z);

		UpdateCurve(R.Curves.Rails.StraightSlopeIn, static (ref Vector3 point) => ref point.z);
		UpdateCurve(R.Curves.Rails.StraightSlopeInOut, static (ref Vector3 point) => ref point.z);
		UpdateCurve(R.Curves.Rails.StraightSlopeOut, static (ref Vector3 point) => ref point.z);

		void UpdateCurve(Curve curve, GetCoordinateReference getCoordinate) =>
			UpdateCurveBy(multiplier, curve, getCoordinate);
	}

	private delegate ref float GetCoordinateReference(ref Vector3 point);

	private static void UpdateCurveBy(float multiplier, Curve curve, GetCoordinateReference getCoordinate)
	{
		var points = curve.Points;
		
		for (var index = 0; index < points.Length; index += 1)
			getCoordinate(ref points[index]) *= multiplier;

		curve.RecalculateLength();
	}
}
