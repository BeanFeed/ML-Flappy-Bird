using Godot;
using System;
using System.Collections.Generic;

public class Bird : RigidBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public float startPos = 47;
	[Export]
	private int jumpPower = 10;
	private bool canFlap = true;
	private protected List<int> pastPipes = new List<int>();

	private protected int score = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//startPos = this.Position.x;
		var timer =  GetNode<Timer>("FlapTime");
		timer.Connect("timeout", this, "EnableFlap");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(this.Position.x != startPos) {
			this.Position = new Vector2(startPos, this.Position.y);
		}
        ScorePastPipe();
        if (IsColliding()) Kill();
    }
	
    private protected void ScorePastPipe()
    {
	    Pipe p = GetPastPipe();
	    //GD.Print(p != null);
	    if (p != null && !pastPipes.Contains(p.id))
	    {
		    //GD.Print("Score");
		    Score(p.id);
	    }
    }

    private protected Pipe GetPastPipe()
    {
	    var ray = GetNode<RayCast2D>("PipeDetector");
	    var obj = ray.GetCollider();
	    if (obj is Pipe p)
	    {
		    return p;
	    }

	    return null;
    }
    private protected virtual void Score(int pipeID)
    {
	    //GD.Print("Score");
	    score++;
	    pastPipes.Add(pipeID);
    }
	public void Flap() {
		if(canFlap) { this.LinearVelocity = new Vector2(this.LinearVelocity.x, -jumpPower); canFlap = false; GetNode<Timer>("FlapTime").Start(); }
	}
	private void EnableFlap() {
		canFlap = true;
	}

	private protected virtual void Kill()
	{
		
	}

	private bool IsColliding()
	{
		var detector = GetNode<Area2D>("detector");
		Godot.Collections.Array bodies = detector.GetOverlappingBodies();
		if(bodies.Contains(this))  bodies.Remove(this);
		if (bodies.Count != 0) { GD.Print("Colliding"); return true; }
		return false;
	}
}
