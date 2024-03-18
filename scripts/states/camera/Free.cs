using Godot;
using System;

public partial class Free : CameraState {

	public override void Enter() {
		CameraController.VCamRotation = 0;
	}

	public override State StateProcess(float delta) {
		CameraController.HCamRotation = CameraController.HCamRotation - Math.Sign(CameraController.Actor.Movement.Direction.X) * delta * 50;

		return null;
	}

	public override State StateInput(InputEvent @event) {
		if (@event is InputEventMouseMotion motion) {
			CameraController.HCamRotation -= motion.Relative.X * CameraController.HSensitivity;
			CameraController.VCamRotation -= motion.Relative.Y * CameraController.VSensitivity;
		}

		if (@event.IsActionPressed("lock_to_target")) return GetState<Locked>();
 
		return null;
	}
}
