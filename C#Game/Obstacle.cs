using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


public class Obstacle
{
	public float x = 0; 
	public float y = 0;

	public static float _rainSpeed
	{
		get { return 50.0f; }
	}

	public Obstacle(int x, int y)
	{
		this.x = x;
		this.y = y;

		while (checkCollision((float)(Window.width * 0.5), (float)(Window.height * 0.75)))
		{
			this.x = Window.RandomRange(0, Window.width);
			this.y = Window.RandomRange(0, Window.height);
		}


	}

	public void Setup()
	{
		
	}

	public int Update(float dt)
	{
		y = y + (_rainSpeed * dt);

		if ( y >= Window.height - 10  ) // If past the screen on the bottom
		{
			x = Window.RandomRange(0, Window.width);
			y = -100;
			return 1; 
		}

		return 0; 
	}

	public void Draw(Graphics g)
	{
		Color background = ColorTranslator.FromHtml("#a5cad9");
        Brush brush = new SolidBrush(background);
        g.FillEllipse(brush, this.x, this.y, 30, 30);

		
	}

	public bool checkCollision(float playerX, float playerY)
	{
		if ( Math.Sqrt( Math.Pow(playerX - this.x, 2) + Math.Pow(playerY - this.y, 2) ) <= 30) {
			return true; 
		}
		return false;
	}
}