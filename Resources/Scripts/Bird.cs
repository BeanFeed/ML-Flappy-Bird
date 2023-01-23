using Godot;
using System;

public class Bird : RigidBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	private float flapAmount = 10f;

	public int score = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	public void Flap() {
		Vector2 newVel = new Vector2();
		newVel.x = this.LinearVelocity.x;
		newVel.y = flapAmount;
		this.LinearVelocity = newVel;
	}
}
