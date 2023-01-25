using Godot;
using System;

public class Level : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public bool simGo = true;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public Pipe GetNextPipe()
    {
        var nextPipe = GetNode<NextPipeDetector>("NextPipeDetector").GetCollider();
        if (nextPipe is Pipe p) return p;
        return null;
    }
    public void Restart()
    {
        var Pipes = GetNode<Node>("Pipes");
        var Spawner = GetNode<BirdSpawner>("BirdSpawner");
        var pipeChildren = Pipes.GetChildren();
        foreach (Node pipe in pipeChildren)
        {
            pipe.QueueFree();
        }
        Spawner.Respawn();
        simGo = true;
    }
}
