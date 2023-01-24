using Godot;
using System;

public class Bird : RigidBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private float startPos = 0;
	[Export]
	private int jumpPower = 10;
	private bool canFlap = true;

	private protected int score = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startPos = this.Position.x;
		var timer =  GetNode<Timer>("FlapTime");
		timer.Connect("timeout", this, "EnableFlap");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(this.Position.x != startPos) {
			this.Position = new Vector2(startPos, this.Position.y);
		}
    }

	public void Flap() {
		if(canFlap) { this.LinearVelocity = new Vector2(this.LinearVelocity.x, -jumpPower); canFlap = false; GetNode<Timer>("FlapTime").Start(); }
	}
	private void EnableFlap() {
		canFlap = true;
	}
}
