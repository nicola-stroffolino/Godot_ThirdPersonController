using Godot;
using System;

public partial class Skeleton : Skeleton3D {
	[Export]
	public Player Actor { get; set; }
	[Export]
	public CollisionShape3D CollisionBox { get; set; }
	[Export]
	public MeshInstance3D Mesh { get; set; }
	[Export]
	public MeshInstance3D Mesh2 { get; set; }

	public float StartingHeight { get; set; }
	public MeshInstance3D CollisionMesh { get; set; }

	public override void _Ready() {
		StartingHeight = (CollisionBox.Shape as CylinderShape3D).Height;
		
		CollisionMesh = CollisionBox.GetNode<MeshInstance3D>("MeshInstance3D");
	}

	public float OldMinHeight { get; set; }
	public override void _Process(double delta) {
		var lf = GetBoneGlobalPose(59); // 59 - left foot
		var rf = GetBoneGlobalPose(64); // 64 - right foot

		var minHeight = Math.Min(lf.Origin.Y, rf.Origin.Y) / 2;

		// (CollisionBox.Shape as CylinderShape3D).Height = StartingHeight - minHeight;
		// (CollisionMesh.Mesh as CylinderMesh).Height = StartingHeight - minHeight;

		// CollisionBox.Position = new() {
		// 	X = CollisionBox.Position.X,
		// 	Y = CollisionBox.Position.Y + (minHeight - OldMinHeight),
		// 	Z = CollisionBox.Position.Z
		// };

		// OldMinHeight = minHeight;
	}
}
