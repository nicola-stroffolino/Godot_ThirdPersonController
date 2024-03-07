using System;
using Godot;

[GlobalClass]
public partial class MovementComponent : Node {
	[Export]
	public Player Actor { get; private set; }
	[Export]
	public int WalkingSpeed { get; set; } = 2; //km/h
	[Export]
	public int RunningSpeed { get; set; } = 6; //km/h
	[Export]
	public float TimeToJumpPeak { get; set; } = .4f; //second
	[Export]
	public int JumpHeight { get; set; } = 1; //meter

	// Functional Variables
	public int ActualSpeed { get; set; }
	public float Gravity { get; set; }
	public float JumpSpeed { get; set; }
	public Vector3 Direction { get; set; } = Vector3.Zero;
	public Vector3 MoveDirection { get; set; } = Vector3.Zero;
	public Vector3 Velocity { get; set; } = Vector3.Zero;

	public override void _Ready() {
		Gravity = 2 * JumpHeight / (TimeToJumpPeak * TimeToJumpPeak); //m/s^2;
		JumpSpeed = Gravity * TimeToJumpPeak; //m/s
	}

	public override void _Process(double delta) {
		GetNode<Label>("../Control/Label").Text = $@"Input Direction: {Direction}
			Move Direction: {MoveDirection}
			Velocity: {Velocity}

			Target Look: 
		";
	}

	public override void _PhysicsProcess(double delta) {
		// if (Actor.StateMachine.CurrentState is Airborne && Direction == Vector3.Zero) {
		// 	// Velocity = new Vector3(0, Velocity.Y, 0);
		// 	// Velocity = new Vector3(Mathf.Lerp(Velocity.X, 0f, (float)delta * 5), Velocity.Y, Velocity.Z);
		// 	Velocity = new Vector3(Mathf.Lerp(Velocity.X, 0f, 1f), Velocity.Y, Mathf.Lerp(Velocity.Z, 0f, 1f));
		// } else if (Actor.StateMachine.CurrentState is not Airborne) {
		// 	// Velocity = new Vector3(MoveDirection.X * ActualSpeed, Velocity.Y, MoveDirection.Z * ActualSpeed);
		// }

		Velocity = new Vector3(MoveDirection.X * ActualSpeed, Velocity.Y, MoveDirection.Z * ActualSpeed);

		Actor.Velocity = Velocity;
		Actor.MoveAndSlide();
	}

	public void ActuallyJump() {
		// Velocity = new Vector3(Velocity.X, JumpSpeed, Velocity.Z);
	}

	public void CheckIfAirborne() {
		if (Actor.VerticalStateMachine.CurrentState is not Airborne) return;

		Actor.AnimationTree.Set("parameters/jump_shot/request", (int)AnimationNodeOneShot.OneShotRequest.FadeOut);
		// Actor.AnimationTree.Set("parameters/falling_idle/blend_amount", 1);
		Actor.AnimationTree.Set("parameters/falling_idle/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}
}
