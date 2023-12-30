using Godot;
using System;

public partial class Main : Node3D {
	public override void _UnhandledInput(InputEvent @event) {
		if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Keycode == Key.Escape)
			GetTree().Quit();
	}
}
