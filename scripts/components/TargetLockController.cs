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

	[Signal]
	public delegate void TargetLockedEventHandler();

	public CollisionShape3D DetectionArea { get; set; }
	public Array<LockMarker> PotentialTargets { get; set; } = new();
	private int _currentTargetIndex = -1;

	
	public override void _Ready() {
		Connect(SignalName.TargetLocked, new Callable(Actor, Player.MethodName.OnTargetLocked));

		DetectionArea = GetChild(0) as CollisionShape3D;
		(DetectionArea.Shape as SphereShape3D).Radius = Radius;

		Connect(SignalName.BodyEntered, new Callable(this, MethodName.OnBodyEntered));
		Connect(SignalName.BodyExited, new Callable(this, MethodName.OnBodyExited));
		
		_currentTargetIndex = PotentialTargets.IndexOf(Target);
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
			if (Target is not null) Target = null;
			else Target = FindClosestTarget();

			LockOnTarget(Target);
		}
		if (Target is not null) {
			if (@event.IsActionPressed("cycle_next_target")) CycleToTarget(1);
			else if (@event.IsActionPressed("cycle_previous_target")) CycleToTarget(-1);
		}
	}

	public LockMarker FindClosestTarget() {
		if (PotentialTargets.Count == 0) return null;

		return PotentialTargets.MinBy(
			target => Actor.GlobalPosition.DistanceTo(target.GlobalPosition)
		);
	}

	public void LockOnTarget(LockMarker target) {
		// Actor.LockedTarget = target;
		Target = target;
		EmitSignal(SignalName.TargetLocked, Target);
	}
	
	private void CycleToTarget(int direction) {
		if (PotentialTargets.Count == 0) return;

		_currentTargetIndex = (_currentTargetIndex + direction + PotentialTargets.Count) % PotentialTargets.Count;
		LockOnTarget(PotentialTargets[_currentTargetIndex]);
	}
}
