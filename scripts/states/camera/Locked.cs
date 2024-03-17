using Godot;
using System;

public partial class Locked : CameraState {
	public override State StateProcess(float delta) {
		var lookingDirection = CameraController.GlobalPosition.DirectionTo(CameraController.Actor.LockedTarget.GlobalPosition);

		CameraController.HCamRotation = Mathf.RadToDeg(Mathf.Atan2(-lookingDirection.X, -lookingDirection.Z));

		return null;
	}

	public override State StateInput(InputEvent @event) {
		if (@event.IsActionPressed("lock_to_target")) return GetState<Free>();

		return null;
	}
}
