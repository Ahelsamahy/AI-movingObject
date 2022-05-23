using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MOT;
using MOT.StateRepresentation;

namespace MovingObjectWF
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        MovingObjectNode node = new MovingObjectNode(new MovingObjectState());
        public Form1()
        {
            InitializeComponent();
        }

        Graphics g;
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            DrawNode(g, node);

            DrawLinePointF(e);
        }
        public void DrawLinePointF(PaintEventArgs e)
        {

            // Create pen.
            Pen boundryLine = new Pen(Color.Black, 4);
            Pen noramlLine = new Pen(Color.Black, 2);

            // Create points that define line.
            PointF Upper1 = new PointF(2F, 1F); //Ceiling upper
            PointF Upper2 = new PointF(312F, 1F);

            PointF Upper3 = new PointF(2F, 105F); //Ceiling bottom
            PointF Upper4 = new PointF(312F, 105F);

            PointF Upper5 = new PointF(1F, 0F); //First short upper
            PointF Upper6 = new PointF(1F, 106F);

            PointF Upper7 = new PointF(105F, 0F); //first long upper
            PointF Upper8 = new PointF(105F, 506F);

            PointF Upper9 = new PointF(209F, 0F); //second long upper
            PointF Upper10 = new PointF(209F, 506F);

            PointF Upper11 = new PointF(313F, 0F); //Second short upper
            PointF Upper12 = new PointF(313F, 106F);


            PointF Lower1 = new PointF(0F, 401F); //Floor upper
            PointF Lower2 = new PointF(312F, 401F);

            PointF Lower3 = new PointF(2F, 505F); //Floor bottom
            PointF Lower4 = new PointF(312F, 505F);

            PointF Lower5 = new PointF(1F, 400F); //First short bottom
            PointF Lower6 = new PointF(1F, 506F);

            PointF Lower7 = new PointF(313F, 400F); //Second short bottom
            PointF Lower8 = new PointF(313F, 506F);


            PointF Middle1 = new PointF(0F, 201F); //Middle upper
            PointF Middle2 = new PointF(210F, 201F);

            PointF Middle3 = new PointF(1F, 200F); //Middle short First
            PointF Middle4 = new PointF(1F, 303F);

            PointF Middle5 = new PointF(0F, 301F); //Middle bottom
            PointF Middle6 = new PointF(210F, 301F);

            // Draw line to screen.
            e.Graphics.DrawLine(boundryLine, Upper1, Upper2);
            e.Graphics.DrawLine(boundryLine, Upper3, Upper4);
            e.Graphics.DrawLine(boundryLine, Upper5, Upper6);
            e.Graphics.DrawLine(boundryLine, Upper7, Upper8);
            e.Graphics.DrawLine(boundryLine, Upper9, Upper10);
            e.Graphics.DrawLine(boundryLine, Upper11, Upper12);

            e.Graphics.DrawLine(boundryLine, Lower1, Lower2);
            e.Graphics.DrawLine(boundryLine, Lower3, Lower4);
            e.Graphics.DrawLine(boundryLine, Lower5, Lower6);
            e.Graphics.DrawLine(boundryLine, Lower7, Lower8);

            e.Graphics.DrawLine(boundryLine, Middle1, Middle2);
            e.Graphics.DrawLine(boundryLine, Middle3, Middle4);
            e.Graphics.DrawLine(boundryLine, Middle5, Middle6);
        }

        private void DrawNode(Graphics g, MovingObjectNode node)
        {
            Font drawFont = new Font("Arial", 16);
            int R_counter = 0;
            int G_counter = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((node.State as MovingObjectState).Table[i, j] == 'R')
                    {
                        g.FillEllipse(Brushes.Red, 3 + j * 104, 3 + i * 100, 100, 100);
                        g.DrawString("R" + R_counter, drawFont, Brushes.AliceBlue, (3 + j * 104) + 30, (3 + i * 100) + 30);
                        R_counter++;
                    }

                    else if ((node.State as MovingObjectState).Table[i, j] == 'G')
                    {
                        g.FillEllipse(Brushes.Green, 3 + j * 104, 3 + i * 100, 100, 100);
                        g.DrawString("G" + G_counter, drawFont, Brushes.Black, (3 + j * 104) +30, (3 + i * 100) +30);
                        G_counter++;
                    }
                }
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            List<AbstractNode> possibleChoices = node.Expand();
            if (possibleChoices.Count == 0)
            {
                timer.Stop();
                MessageBox.Show("This is a dead end.");
                return;
            }
            node = new MovingObjectNode((MovingObjectNode)possibleChoices[rnd.Next(possibleChoices.Count)]);
            pictureBox.Invalidate();

            if (node.IsTerminal)
            {
                MessageBox.Show("Problem solved.");
                timer.Stop();
                return;
            }
        }



    }
}
