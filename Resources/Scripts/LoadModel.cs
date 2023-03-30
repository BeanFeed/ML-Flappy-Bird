using Godot;
using System;
using Newtonsoft.Json;

public class LoadModel : Button
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

    public override void _Pressed()
    {
        var textIn = GetNode<TextEdit>("/root/Level/UI/ModelInput");
        string stringModel = textIn.Text;
        if(textIn.Visible == false) textIn.Visible = true;
        else if(stringModel != "") {
            float[][][] model = JsonConvert.DeserializeObject<float[][][]>(stringModel);
            var level = GetNode<Level>("/root/Level");
            level.RunModel(model);
            textIn.Visible = false;
        }
    }

}
