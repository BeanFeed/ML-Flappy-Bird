using Godot;
using System;

public class NextPipeDetector : RayCast2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private float startX = 0;
    private float startY = -7;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        startX = this.Position.x;
        //startY = this.Position.y;
        this.Position = new Vector2(startX, startY);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

    public Pipe GetNextPipe()
    {
        var obj = GetCollider();
        if (obj is Pipe p) return p;
        return null;
    }
}
