using Godot;
using System;

public partial class PathFollow2d : PathFollow2D
{
	double speed = 0.1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ProgressRatio += (float)(speed * delta);
	}
}
