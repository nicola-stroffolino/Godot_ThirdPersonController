using Godot;
using System;

[GlobalClass]
public partial class CameraState : State {
	[Export]
	public CameraController CameraController { get; set; }
	[Export]
	public Camera3D Camera { get; set; }
}
