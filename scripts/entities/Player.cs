using Godot;
using System;

public partial class Player : CharacterBody3D {
	[ExportGroup("Node References")]
	[Export]
	public Node3D Model { get; private set; }
	[Export]
	public CollisionShape3D CollisionShape { get; private set; }
	[Export]
	public MovementComponent MovementComponent { get; private set; }
	[Export]
	public CameraComponent CameraComponent { get; private set; }
	[Export]
	public StateMachine StateMachine { get; private set; }
	[Export]
	public AnimationTree AnimationTree { get; private set; }
	[Export]
	public AnimationPlayer AnimationPlayer { get; private set; }

	public override void _Process(double delta) {
		var InputDirection = Vector3.Zero;
		InputDirection.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		InputDirection.Z = Input.GetActionStrength("move_backward") - Input.GetActionStrength("move_forward");

		MovementComponent.Direction = InputDirection;
		MovementComponent.MoveDirection = InputDirection.Rotated(Vector3.Up, CameraComponent.GetHRot()).Normalized();
		if (InputDirection != Vector3.Zero) {
			if (InputDirection != MovementComponent.ContiguousDirection) {
				var angle = MovementComponent.ContiguousDirection.AngleTo(InputDirection);
				GD.Print(Mathf.RadToDeg(angle));
			}
			MovementComponent.ContiguousDirection = InputDirection;
		}
	}
}
