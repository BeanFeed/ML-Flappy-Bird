using Godot;
using System;

public class Pipe : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    //public bool hasPast = false;
    //public int speed = 10;
    public int id = 0;

    private int speed = 10;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var windowHeight = GetViewport().Size.y;
        Random rand = new Random();
        Position = new Vector2(Position.x, (float)rand.Next(100,(int)windowHeight - 20));
        id = rand.Next(0,999999);
        //this.LinearVelocity = new Vector2(-speed, 0);
    }
    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }
  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(this.Position.x < -40) QueueFree();
        //this.Position
        var level = GetNode<Level>("/root/Level");
        if (level.simGo) MoveAndSlide(new Vector2(-speed, 0));
    }
}
