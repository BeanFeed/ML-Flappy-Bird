using Godot;
using System;
using System.Collections.Generic;
//using System.Linq;

public class NNBird : Bird
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    //private List<int> pastPipes = new List<int>();
    public NeuralNetwork Brain = new NeuralNetwork(new int[]{1,10,1});
    public bool dead = false;
    private BirdSpawner spawner = null;
    // Called when the node enters the scene tree for the first time.

    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        base._Process(delta);
        var simGoVal = GetNode<Level>("/root/Level").simGo;
        if (!GetNode<Level>("/root/Level").simGo || dead) return;
        GD.Print(GetNode<RayCast2D>("GroundDetector").GetCollider() != null);
        if (GetNode<RayCast2D>("GroundDetector").GetCollider() != null) Flap();
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
    
    private protected override void Kill()
    {
        dead = true;
        spawner.CheckAllDead();
    }
    
    public void ReturnStats()
    {
        if (spawner.bestScore < score)
        {
            spawner.bestScore = score;
            spawner.bestModel = Brain.GeAllWeights();
        }
    }

    public void SetSpawner(BirdSpawner spawner)
    {
        this.spawner = spawner;
    }
}
