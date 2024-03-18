using Godot;
using System;

public partial class Locked : CameraState {
	public override State StateProcess(float delta) {
		var lookingDirection = CameraController.GlobalPosition.DirectionTo(CameraController.Actor.LockedTarget.GlobalPosition);

		CameraController.HCamRotation = Mathf.RadToDeg(Mathf.Atan2(-lookingDirection.X, -lookingDirection.Z));

		var deltaX = Math.Abs(CameraController.Actor.GlobalPosition.X - CameraController.Actor.LockedTarget.GlobalPosition.X);
		var deltaZ = Math.Abs(CameraController.Actor.GlobalPosition.Z - CameraController.Actor.LockedTarget.GlobalPosition.Z);
		var hypotenuse = Math.Sqrt(deltaX * deltaX + deltaZ * deltaZ);
		var alpha = Mathf.Atan2(CameraController.Actor.GlobalPosition.Y - CameraController.Actor.LockedTarget.GlobalPosition.Y, hypotenuse);
		CameraController.VCamRotation = (float)Mathf.RadToDeg(-alpha);

		return null;
	}

	public override State StateInput(InputEvent @event) {
		if (@event.IsActionPressed("lock_to_target")) return GetState<Free>();

		return null;
	}
}
