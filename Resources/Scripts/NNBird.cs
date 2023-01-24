using Godot;
using System;

public class NNBird : Bird
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public NeuralNetwork Brain = new NeuralNetwork(new int[]{1,10,1});
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var nextPipe = GetNextPipe();
        var pipeY = nextPipe.Position.y;
        
        Brain.Process();
        float[] results = Brain.getOutput();
    }
    private void searchPastPipe()
    {
        var ray = GetNode<RayCast2D>("PipeDetector");
        var obj = ray.GetCollider();
        if (obj is Pipe p && !p.hasPast)
        {
            score++;
            p.hasPast = true;
        }
    }

    private Pipe GetNextPipe()
    {
        var ray = GetNode<RayCast2D>("NextPipeDetector");
        var obj = ray.GetCollider();
        if (obj is Pipe p) return p;
        else return null;
    }

    private bool PipeInFront()
    {
        
    }
}
