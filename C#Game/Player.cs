using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

public class Player
{
    private float x = 0;
    private float y = 0; 
   
    public Player()
    {
        this.x = (float)(Window.width * 0.5);
        this.y = (float)(Window.height * 0.75); 
    }

    public void Setup()
    {
   
    }

    public void Update(float dt)
    {
       
    }

    public void Draw(Graphics g)
    {
        Color background = ColorTranslator.FromHtml("#0b1545");
        Brush brush = new SolidBrush(background);
        g.FillEllipse(brush, this.x, this.y, 30, 30);
    }

    public void strafeRight()
    {
        if (!(Window.width - this.x <= 45 )) // if not at the right edge of the screen
        {
            this.x += 10;
        }
        
    }
    
    public void strafeLeft()
    {
        if (this.x > 10) // if not at the left edge of the screen:
        {
            this.x -= 10;
        }
        
    }

    public float getX()
    {
        return this.x; 
    }

    public float getY()
    {
        return this.y; 
    }
}