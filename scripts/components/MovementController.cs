using System;
using Godot;

[GlobalClass]
public partial class MovementController : Node {
	[Export]
	public GameEntity3D Actor { get; private set; }
	[Export]
	public int WalkingSpeed { get; set; } = 2; //km/h
	[Export]
	public int RunningSpeed { get; set; } = 6; //km/h
	[Export]
	public float TimeToJumpPeak { get; set; } = .4f; //second
	[Export]
	public int JumpHeight { get; set; } = 1; //meter

	// Functional Variables
	public float ActualSpeed { get; set; }
	public float Gravity { get; set; }
	public float JumpSpeed { get; set; }
	public Vector3 Direction { get; set; } = Vector3.Zero;
	public Vector3 MoveDirection { get; set; } = Vector3.Zero;
	public Vector3 Velocity { get; set; } = Vector3.Zero;

	public override void _Ready() {
		Gravity = 2 * JumpHeight / (TimeToJumpPeak * TimeToJumpPeak); //m/s^2;
		JumpSpeed = Gravity * TimeToJumpPeak; //m/s
	}

	public override void _PhysicsProcess(double delta) {
		Actor.Velocity = Velocity;
		Actor.MoveAndSlide();
	}

	public void SetVelocity(Vector3 Direction, float value, bool additive = true) {
		Direction *= value;
		if (additive) Velocity += Direction;
		// else not implemented idgaf 
	}

	public void ApplyVelocity() {
		Velocity = new Vector3(MoveDirection.X * ActualSpeed, Velocity.Y, MoveDirection.Z * ActualSpeed);
	}

	public void ApplyGravity(float delta) {
		Velocity = new Vector3(Velocity.X, Velocity.Y - Gravity * delta, Velocity.Z);
	}
}
