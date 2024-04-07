using Godot;
using System;

public partial class Locked : CameraState {
	public override State StateProcess(float delta) {
		var lookingDirection = CameraController.GlobalPosition.DirectionTo(Actor.LockedTarget.GlobalPosition);

		float newHCamRotation = Mathf.Atan2(-lookingDirection.X, -lookingDirection.Z);
		CameraController.HCamRotation = Mathf.Wrap(
			Mathf.LerpAngle(CameraController.HCamRotation, newHCamRotation, delta * 7), 
			-Mathf.Pi, 
			Mathf.Pi
		);
		
		// var deltaX = Math.Abs(Actor.GlobalPosition.X - Actor.LockedTarget.GlobalPosition.X);
		// var deltaZ = Math.Abs(Actor.GlobalPosition.Z - Actor.LockedTarget.GlobalPosition.Z);
		// var hypotenuse = Math.Sqrt(deltaX * deltaX + deltaZ * deltaZ);
		// var alpha = Mathf.Atan2(Actor.GlobalPosition.Y - Actor.LockedTarget.GlobalPosition.Y, hypotenuse);
		// CameraController.VCamRotation = (float)Mathf.RadToDeg(-alpha);

		return null;
	}

	public override State StateInput(InputEvent @event) {
		if (@event.IsActionPressed("lock_to_target")) return GetState<Free>();

		return null;
	}
}
