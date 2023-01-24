using Godot;
using System;

public class Pipe : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    //public bool hasPast = false;
    public int id = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var windowHeight = GetViewport().Size.y;
        Random rand = new Random();
        Position = new Vector2(Position.x, (float)rand.Next(100,(int)windowHeight - 20));
        id = rand.Next(0,999999);
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Position.x < -40) QueueFree();
    }
}
