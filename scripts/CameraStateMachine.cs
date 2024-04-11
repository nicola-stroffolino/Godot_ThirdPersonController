using Godot;
using System;

public partial class CameraStateMachine : StateMachine {
	public CameraController CameraController { get; set; }
	public GameEntity3D Actor { get; set; }
	public TargetLockController LockController { get; set; }
	public Camera3D Camera { get; set; }

	public override void _Ready() {
		CameraController = (CameraController)GetParent();
		Actor = CameraController.Actor;
		LockController = CameraController.LockController;
		Camera = (Camera3D)GetNode("%Camera3D");
		
		foreach (var child in GetChildren()) {
			if (child is CameraState s) {
				s.CameraController = CameraController;
				s.Actor = Actor;
				s.LockController = LockController;
				s.Camera = Camera;
				s.StateMachine = this;
			}
		}

		base._Ready();
	}
}
