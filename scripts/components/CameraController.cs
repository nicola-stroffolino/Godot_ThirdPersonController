using Godot;
using System;

[GlobalClass]
public partial class CameraController : Node3D {
	[Export]
	public GameEntity3D Actor { get; set; }
	[Export]
	public TargetLockController LockController { get; set; }
	[Export]
	public float HSensitivity { get; set; } = .1f;
	[Export]
	public float VSensitivity { get; set; } = .1f;
	[Export]
	public bool Attached { get; set; } = true;

	public float HCamRotation { get; set; }
	public float VCamRotation { get; set; }
	public readonly float VCam_Min = Mathf.DegToRad(-55f);
	public readonly float VCam_Max = Mathf.DegToRad(75f);

	public override void _Ready() {
		if (Actor is not null && Actor is Player p) p.CameraController = this;
		
		Input.MouseMode = Input.MouseModeEnum.Captured;
		HCamRotation = Actor.Rotation.Y + Mathf.Pi;
		VCamRotation = Actor.Rotation.X;
	}

	public override void _Process(double delta) {
		GetNode<Node3D>("Horizontal").Rotation = new Vector3 {
			Y = HCamRotation	
		};
		VCamRotation = Mathf.Clamp(VCamRotation, VCam_Min, VCam_Max);
		GetNode<Node3D>("Horizontal/Vertical").Rotation = new Vector3 {
			X = VCamRotation
		};
		if (Attached) GlobalPosition = Actor.GlobalPosition; 
	}
}
