using Godot;
using System;

[GlobalClass]
public partial class CameraComponent : Node3D {
	[Export]	
	public float HSensitivity { get; set; } = .1f;
	[Export]
	public float VSensitivity { get; set; } = .1f;

	public float HCamRotation { get; set; } = (float)Math.PI;
	public float VCamRotation { get; set; } = 0;
	private const float VCam_Min = -55f;
	private const float VCam_Max = 75f;

	public override void _Ready() {
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Input(InputEvent @event) {
		if (@event is not InputEventMouseMotion mm || Input.MouseMode != Input.MouseModeEnum.Captured) return;

		HCamRotation -= mm.Relative.X * HSensitivity;
		VCamRotation -= mm.Relative.Y * VSensitivity;
		VCamRotation = Mathf.Clamp(VCamRotation, VCam_Min, VCam_Max);
	}

	public override void _Process(double delta) {
		GetNode<Node3D>("Horizontal").RotationDegrees = new Vector3 {
			Y = HCamRotation
		};
		GetNode<Node3D>("Horizontal/Vertical").RotationDegrees = new Vector3 {
			X = VCamRotation
		};
	}

	public float GetHRot() => Mathf.DegToRad(HCamRotation);
	public float GetVRot() => Mathf.DegToRad(VCamRotation);
}
