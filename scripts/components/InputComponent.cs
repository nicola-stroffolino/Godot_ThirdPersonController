using Godot;
using System;

public partial class InputComponent : Node {
	[Signal]
	public delegate void DirectionStatusEventHandler(Vector3 direction);
	[Signal]
	public delegate void StrafeStatusEventHandler(Vector3 direction);
	[Signal]
	public delegate void SprintStatusEventHandler(bool sprinting);
	[Signal]
	public delegate void CameraStatusEventHandler(InputEventMouseMotion motion);
	[Signal]
	public delegate void JumpStatusEventHandler();
	[Signal]
	public delegate void ToggleInventoryEventHandler();
	
	public override void _PhysicsProcess(double delta) {
		var Direction = new Vector3 {
			X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			Y = 0,
			Z = Input.GetActionStrength("move_back") - Input.GetActionStrength("move_forward")
		};
		//EmitSignal(SignalName.DirectionStatus, Direction);
		//EmitSignal(SignalName.StrafeStatus, Direction);
		//EmitSignal(SignalName.SprintStatus, Input.IsActionPressed("sprint"));
		//if (Input.IsActionPressed("jump")) EmitSignal(SignalName.JumpStatus);
		//if (Input.IsActionJustPressed("toggle_inventory")) EmitSignal(SignalName.ToggleInventory);
	}
}
