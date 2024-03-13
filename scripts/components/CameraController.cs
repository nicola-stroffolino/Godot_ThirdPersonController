using Godot;
using System;

[GlobalClass]
public partial class CameraController : Node3D {
	[Export]
	public GameEntity3D Actor { get; set; }
	[Export]
	public float HSensitivity { get; set; } = .1f;
	[Export]
	public float VSensitivity { get; set; } = .1f;
	[Export]
	public bool Attached { get; set; } = true;

	public float HCamRotation { get; set; }
	public float VCamRotation { get; set; }
	private const float VCam_Min = -55f;
	private const float VCam_Max = 75f;

	public override void _Ready() {
		Input.MouseMode = Input.MouseModeEnum.Captured;
		HCamRotation = (float)(Actor.RotationDegrees.Y + 180);
		VCamRotation = Actor.RotationDegrees.X;
	}

	public override void _Input(InputEvent @event) {
		if (@event is not InputEventMouseMotion mm || Input.MouseMode != Input.MouseModeEnum.Captured) return;

		if (!Actor.IsLockedOn) HCamRotation -= mm.Relative.X * HSensitivity;
		VCamRotation -= mm.Relative.Y * VSensitivity;
		VCamRotation = Mathf.Clamp(VCamRotation, VCam_Min, VCam_Max);
	}

	public override void _Process(double delta) {
		if (Actor.IsLockedOn) HCamRotation = Mathf.RadToDeg(Mathf.Atan2(GlobalPosition.X - Actor.LockedTarget.GlobalPosition.X, GlobalPosition.Z - Actor.LockedTarget.GlobalPosition.Z));
		GetNode<Node3D>("Horizontal").RotationDegrees = new Vector3 {
			Y = HCamRotation
		};
		GetNode<Node3D>("Horizontal/Vertical").RotationDegrees = new Vector3 {
			X = VCamRotation
		};
		if (Attached) GlobalPosition = Actor.GlobalPosition; 
	}

	public float GetHRot() => Mathf.DegToRad(HCamRotation);
	public float GetVRot() => Mathf.DegToRad(VCamRotation);
}
