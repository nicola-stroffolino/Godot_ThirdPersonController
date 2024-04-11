using Godot;
using System;

[GlobalClass]
public partial class CameraState : State {
	public GameEntity3D Actor { get; set; }
	public CameraController CameraController { get; set; }
	public Camera3D Camera { get; set; }
	public TargetLockController LockController { get; set; }
}
