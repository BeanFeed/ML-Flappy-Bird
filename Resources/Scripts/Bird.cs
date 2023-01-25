using Godot;
using System;
using System.Collections.Generic;

public class Bird : RigidBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private bool canFlap = true;
	private protected bool isDead = false;
	private protected List<int> pastPipeIDs = new List<int>();
	private protected int score = 0;
	[Export]
	private protected int FlapPower = 250;

	public float startX = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var timer = GetNode<Timer>("FlapTime");
		timer.Connect("timeout", this, "ResetFlap");
		startX = this.Position.x;
		var deathBox = GetNode<Area2D>("detector");
		deathBox.Connect("body_entered", this, "Body_Entered");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
	    if (!isDead)
	    {
		    var pipeDetector = GetNode<RayCast2D>("PipeDetector");
		    var obj = pipeDetector.GetCollider();
		    if (obj is Pipe p && !pastPipeIDs.Contains(p.id))
		    {
			    pastPipeIDs.Add(p.id);
			    Score();
		    }
	    }

    }

    private protected virtual void Score()
    {
	    score++;
    }
    private protected void Flap()
    {
	    if (canFlap)
	    {
		    this.LinearVelocity = new Vector2(0, -FlapPower);
		    var timer = GetNode<Timer>("FlapTime");
		    timer.Start();
		    canFlap = false;
	    }
    }

    private void ResetFlap()
    {
	    canFlap = true;
	    //GD.Print("Reset");
    }

    public bool IsDead()
    {
	    return isDead;
    }

    public void SetDead(bool isDead)
    {
	    this.isDead = isDead;
    }
    public int GetScore()
    {
	    return score;
    }

    public virtual void Kill()
    {
	    isDead = true;
    }

    private void Body_Entered(Node body)
    {
	    if (!(body is NNBird nb) && (body is Pipe || body is StaticBody2D))
	    {
		    Kill();
	    }
    }
}
