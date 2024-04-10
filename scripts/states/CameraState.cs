using Godot;
using System;

[GlobalClass]
public partial class CameraState : State {
	// [Export]
	// public GameEntity3D Actor { get; set; }
	[Export]
	public CameraController CameraController { get; set; }
	[Export]
	public Camera3D Camera { get; set; }
	[Export]
	public TargetLockController LockController { get; set; }
}
