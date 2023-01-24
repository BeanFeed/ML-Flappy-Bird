using Godot;
using System;
using System.Collections.Generic;
//using System.Linq;

public class NNBird : Bird
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private List<int> pastPipes = new List<int>();
    public NeuralNetwork Brain = new NeuralNetwork(new int[]{1,10,1});
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }
    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(PipeInFront())
        {
            var nextPipe = GetNextPipe();
            var pipeY = nextPipe.Position.y;
            Brain.SetInput(new float[]{pipeY - Position.y});
        }
        
        Brain.Process();
        float[] results = Brain.getOutput();
        if(results[0]>0.5f) Flap();
    }
    private void searchPastPipe()
    {
        var ray = GetNode<RayCast2D>("PipeDetector");
        var obj = ray.GetCollider();
        if (obj is Pipe p && !pastPipes.Contains(p.id))
        {
            score++;
            pastPipes.Add(p.id);
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
        var ray = GetNode<RayCast2D>("NextPipeDetector");
        var obj = ray.GetCollider();
        if(obj != null && obj is Pipe) return true;
        return false;
    }
}
