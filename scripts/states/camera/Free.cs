using Godot;
using System;

public partial class Free : CameraState {

	public override void Enter() {
		// CameraController.VCamRotation = 0;
	}

	public override State StateProcess(float delta) {
		CameraController.HCamRotation = CameraController.HCamRotation - Math.Sign(Actor.Movement.Direction.X) * delta; // times some parameter to increase sensitivity

		return null;
	}

	public override State StateInput(InputEvent @event) {
		if (@event is InputEventMouseMotion motion) {
			CameraController.HCamRotation -= motion.Relative.X * CameraController.HSensitivity * Mathf.DegToRad(1);
			CameraController.VCamRotation -= motion.Relative.Y * CameraController.VSensitivity * Mathf.DegToRad(1);
		} 

		if (@event.IsActionPressed("lock_to_target") && Actor.LockedTarget is not null) return GetState<Locked>();
 
		return null;
	}
}
