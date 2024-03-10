using Godot;
using System;

public partial class Model : Node3D {
	[Export]
	public MovementComponent MovementComponent { get; set; }
	public float LookingRotation { get; set; }

	public override void _Ready() {
		LookingRotation = Mathf.Pi;
	}

	public override void _Process(double delta) {
		var mv = MovementComponent;
		if (mv.Direction != Vector3.Zero) {
			var n = Mathf.RadToDeg(Mathf.Atan2(mv.MoveDirection.X, mv.MoveDirection.Z));
			var o = Mathf.RadToDeg(LookingRotation);

			var angleDifference = Mathf.Abs(n - o);
			if (angleDifference > 180) angleDifference = 360 - angleDifference;
			
			// if (n != o) GD.Print($"New: {n} - Old: {o} - Result: {angleDifference}");

			LookingRotation = Mathf.Atan2(mv.MoveDirection.X, mv.MoveDirection.Z);
		}

		Rotation = new() {
			Y = (float) Mathf.Wrap(
				Mathf.LerpAngle(
					Rotation.Y,
					LookingRotation,
					delta * 10 // Actor.StateMachine.CurrentState is Airborne ? 0f : 1f 
				),
				-Math.PI,
				Math.PI
			)
		};
	}
}
