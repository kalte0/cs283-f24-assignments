using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

public class Game
{

    public static int _numObstacles = 20;
    public static Obstacle[] _obstacles;

    public int score = 0; 

    public bool gameOver = false; 
    public Player playerObj = null;

    public void Setup()
    {
        generateObstacles();

        if (playerObj == null)
        {
            playerObj = new Player(); 
        }
    }

    public void Update(float dt)
    {
        if (!gameOver) // while game is running:
        {
            for (int i = 0; i < 10; i++)
            {
                if (_obstacles[i] != null)
                {
                    score += _obstacles[i].Update(dt);
                    // check for player/object collision:
                    if (_obstacles[i].checkCollision( playerObj.getX(), playerObj.getY() ) ) // if there is a collision:
                    {
                        gameOver = true; 
                    }
                }
            }
            playerObj.Update(dt);
        }
    }

    public void Draw(Graphics g)
    {
        //if (!gameOver) // while game is running:
        {
            // run the Draw function for each of the objects and the player. 
            for (int i = 0; i < 10; i++)
            {
                if (_obstacles[i] != null)
                {
                    _obstacles[i].Draw(g);
                }
            }
            playerObj.Draw(g);
        }

   
        Color textBox = ColorTranslator.FromHtml("#232336");
        Brush brush = new SolidBrush(textBox);
        g.FillRectangle(brush, 0, Window.height - 50, 250, 150);

        Font font = new Font("Arial", 12); 
        SolidBrush fontBrush = new SolidBrush(Color.White); 
        StringFormat format = new StringFormat(); 
        if (!gameOver)
        {
            g.DrawString("Acid Rain - RDV - cs283, 2024", font, fontBrush,
                25, Window.height - 25,
                format);

        }
        else if (gameOver)
        {
            g.DrawString("Game Over!", font, fontBrush,
                25, Window.height - 25,
                format);
        }

        g.DrawString("" + score, font, brush,
            Window.width - 50, Window.height - 25, format); 
    }

    public void MouseClick(MouseEventArgs mouse)
    {
        if (mouse.Button == MouseButtons.Left)
        {
            System.Console.WriteLine(mouse.Location.X + ", " + mouse.Location.Y);
        }
    }

    public void KeyDown(KeyEventArgs key)
    {
        if (key.KeyCode == Keys.D || key.KeyCode == Keys.Right)
        {
            playerObj.strafeRight(); 
        }
        else if (key.KeyCode== Keys.S || key.KeyCode == Keys.Down)
        {
        }
        else if (key.KeyCode == Keys.A || key.KeyCode == Keys.Left)
        {
            playerObj.strafeLeft(); 
        }
        else if (key.KeyCode == Keys.W || key.KeyCode == Keys.Up)
        {
        }
    }

    public void generateObstacles()
    {
        _obstacles = new Obstacle[_numObstacles];

        for (int i = 0; i < _numObstacles; i++)
        {
            _obstacles[i] = new Obstacle(Window.RandomRange(0, Window.width), Window.RandomRange(0, Window.height));
        }
    }

}
