using Godot;
using System.IO;
using Newtonsoft.Json;
using File = System.IO.File;
using Path = System.IO.Path;


public class SaveModel : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
    private void _pressed()
    {
        var spawner = GetNode<BirdSpawner>("/root/Level/BirdSpawner");
        spawner.SaveModel();
        string vals = JsonConvert.SerializeObject(spawner.bestModel);
        string path = "/home/austind";
        var file = File.Create(Path.Combine(path, "model.txt"));
        file.Close();
        File.WriteAllText(Path.Combine(path, "model.txt"), vals);
        GD.Print("Saved");
    }
    
}
