using Godot;
using System;
using System.Collections.Generic;
//using System.Linq;

public class NNBird : Bird
{
    public NeuralNetwork Brain = new NeuralNetwork(new int[] { 1, 5, 1 });
    private BirdSpawner spawner = null;
    private float startY = 0;
    public override void _Ready()
    {
        base._Ready();
        var nextPipeDetector = GetNode<NextPipeDetector>("NextPipeDetector");
        nextPipeDetector.Position = new Vector2(Position.x, -7);
        startY = this.Position.y;
    }
    public override void _Process(float delta)
    {
        base._Process(delta);
        if (!isDead)
        {
            var nextPipe = GetNode<NextPipeDetector>("NextPipeDetector").GetCollider();
            if (nextPipe is Pipe pipe)
            {
                float dist = pipe.Position.y - Position.y;
                Brain.SetInput(new float[]{dist});
                Brain.Process();
                var outS = Brain.getOutput();
                if(outS[0] > 0.5) { Flap(); GD.Print("Ai Flap: " + new Random().Next(1,10)); }
            }
            if(GetNode<RayCast2D>("GroundDetector").GetCollider()!= null && ((Node)GetNode<RayCast2D>("GroundDetector").GetCollider()).Name == "BottomBorder") Flap();
        }

        
    }

    public void SetSpawner(BirdSpawner spawner)
    {
        this.spawner = spawner;
    }

    public override void Kill()
    {
        base.Kill();
        this.GravityScale = 0;
        spawner.CheckAllDead();
        this.Position = new Vector2(startX, -20);
    }

    public void Respawn()
    {
        this.GravityScale = 1;
        this.isDead = false;
        this.Position = new Vector2(startX, startY);
    }
    
}
