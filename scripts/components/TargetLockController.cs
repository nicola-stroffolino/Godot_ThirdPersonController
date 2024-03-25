using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class TargetLockController : Area3D {
	[Export]
	public GameEntity3D Actor { get; set; }
	[Export]
	public LockMarker Target { get; set; }
	[Export]
	public int Radius { get; set; } = 7;

	public CollisionShape3D DetectionArea { get; set; }
	public Array<LockMarker> PotentialTargets { get; set; } = new();
	
	public override void _Ready() {
		DetectionArea = GetChild(0) as CollisionShape3D;
		(DetectionArea.Shape as SphereShape3D).Radius = Radius;

		Connect(SignalName.BodyEntered, new Callable(this, MethodName.OnBodyEntered));
		Connect(SignalName.BodyExited, new Callable(this, MethodName.OnBodyExited));
	}

	public override void _PhysicsProcess(double delta) {
		if (Actor is not null) GlobalPosition = Actor.GlobalPosition;
	}

	public void OnBodyEntered(Node3D body) {
		// good for now, later on the body as GameEntity3D 
		// should be delegated of providing if it has a
		// LockMarker as a child
		if (
			body.GetType() != Actor.GetType() 
			&& body.GetChildren().FirstOrDefault(child => child is LockMarker) is LockMarker lm
			&& lm != null
		) {
			PotentialTargets.Add(lm);
		
			GD.Print(body.Name + " has Entered.");
		}
	}

	public void OnBodyExited(Node3D body) {
		if (
			body.GetType() != Actor.GetType() 
			&& body.GetChildren().FirstOrDefault(child => child is LockMarker) is LockMarker lm
			&& lm != null
			&& PotentialTargets.Contains(lm)
		) {
			PotentialTargets.Remove(lm);

			GD.Print(body.Name + " has Exited.");
		}
	}

	// Check for input "target_lock", calculate shortest distance target, 
	// if locked send signal, player receives signal and changes rotation logic (OnTargetLocked)
	public override void _Input(InputEvent @event) {
		if (@event.IsActionPressed("lock_to_target")) {
			Target = FindClosestTarget();
			LockOnTarget(Target);
		}
	}

	public LockMarker FindClosestTarget() {
		if (PotentialTargets.Count == 0) return null;

		return PotentialTargets.MinBy(
			target => Actor.GlobalPosition.DistanceTo(target.GlobalPosition)
		);
	}

	public void LockOnTarget(LockMarker target) {
		Actor.LockedTarget = target;
	}
}
