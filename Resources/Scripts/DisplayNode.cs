using Godot;
using System;

public class DisplayNode : TextureButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private bool mouseOver = false;
    public float value = 0.0f;
    private static Label valueLabel;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        valueLabel = GetNode<Label>("Popup/DisplayValue");
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(mouseOver == true) valueLabel.Text = "Value: " + value.ToString();
    }

    public void Mouse_Over() {
        mouseOver = true;
        this.RectScale = new Vector2(1.1f, 1.1f);
        var xSizeDif = (this.RectSize.x * 1.1) - this.RectSize.x;
        var ySizeDif = (this.RectSize.y * 1.1) - this.RectSize.y;
        this.RectPosition = new Vector2((float)(this.RectPosition.x - (xSizeDif / 2)), (float)(this.RectPosition.y - (ySizeDif / 2)));
    }

    public void Mouse_Leave() {
        mouseOver = false;
        this.RectScale = new Vector2(1,1);
        var xSizeDif = (this.RectSize.x * 1.1) - this.RectSize.x;
        var ySizeDif = (this.RectSize.y * 1.1) - this.RectSize.y;
        this.RectPosition = new Vector2((float)(this.RectPosition.x + (xSizeDif / 2)), (float)(this.RectPosition.y + (ySizeDif / 2)));
    }

}
