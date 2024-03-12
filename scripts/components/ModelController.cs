using Godot;
using System;

public partial class ModelController : Node3D {
	[Export]
	public GameEntity3D Actor { get; set; }
	[Export]
	public MovementController Movement { get; set; }


	public override void _Ready() {
		
	}

	public override void _Process(double delta) {
		
	}
}
