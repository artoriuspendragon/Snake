
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {   const int map_length=20;
        const int map_height =40;
        _direction direction =_direction.RIGHT;
        _direction predirect;
        public List<PictureBox> board = new List<PictureBox>();
        public List<point> body = new List<point>();
        public point head = new point(6, 5);
        public point apple = new point();
        public point tail = new point();
        public point temp = new point();
        public bool growed = false;
        int test;
        
        public enum _direction
        {
            RIGHT = 4,
            LEFT = 2,
            DOWN = 1,
            UP = 3,
        };
        public struct point
        {
            public int x;
            public int y;
         public point(int x, int y)
            {
                this.x = x;
                this.y = y;

            }
        }
       
       private void start()
        {
            int i;for (i = 0; i < board.Count; i++)
            { board[i].Image = null; }
                growed = false;
            head = new point(6, 5);
            body.Clear();
            label101.Text = "";
            
            body.Add(new point(5,5));
            timer1.Enabled = true;
        }
        public void stop()
        { timer1.Enabled = false; }
     
        public void grow()
        {
            body.Add(new point());
            growed = false;
        }
        public void move() { }
        private _direction checkdirection()
        {
            if (direction == _direction.UP && predirect == _direction.DOWN) return _direction.DOWN;
            if (direction == _direction.DOWN && predirect == _direction.UP) return _direction.UP;
            if (direction == _direction.LEFT && predirect == _direction.RIGHT) return _direction.RIGHT;
            if (direction == _direction.RIGHT && predirect == _direction.LEFT) return _direction.LEFT;
            return direction;

        }
        public void play()
        {
            int i = 0, c = 0,d=0;
            temp = head;

            while (true)//苹果位置
            {

                if (growed)
                { break; }
                Random rand = new Random();
                apple.x = rand.Next(1, map_length);
                apple.y = rand.Next(1, map_height);
                growed = true;
                if (!apple_check())
                {
                    break;
                }
            }
            predirect=checkdirection();
            switch (predirect)
            {
                case _direction.DOWN: if (head.y == map_height) head.y = 1; else head.y += 1; break;
                case _direction.LEFT: if (head.x == 1) head.x = map_length; else head.x -= 1; break;
                case _direction.UP: if (head.y == 1) head.y = map_height; else head.y -= 1; break;
                case _direction.RIGHT: if (head.x == map_length) head.x = 1; else head.x += 1; break;
                default: break;

            }
            //尾巴
            if (head.x == apple.x && head.y == apple.y)
            { grow(); }

            for (i = 0; i < body.Count; i++)
            {
                point trans;
                trans = body[i];
                body[i] = temp;
                temp = trans;
            }
           d = body[body.Count - 1].x - 1 + map_length * ((body[body.Count - 1].y -1)); //
            //尾巴
            //判定
            for (i = 0; i < body.Count; i++)
            {
                if (head.x == body[i].x && head.y == body[i].y)
                {
                    Invoke(new MethodInvoker(delegate() { stop(); label101.Text = "you lose"; }));



                }
            }

            //判定
       

            for (i = 0; i < body.Count; i++)
            {
                c = map_length * (body[i].y - 1) + body[i].x - 1;
                board[c].Image = Image.FromFile(Application.StartupPath + "\\snake\\body.jpg");
            }
            c = map_length * (apple.y - 1) + apple.x - 1;
            board[c].Image = Image.FromFile(Application.StartupPath + "\\snake\\body.jpg");
            c = map_length * (head.y - 1) + head.x - 1;
            board[c].Image = Image.FromFile(Application.StartupPath + "\\snake\\head.jpg");
            board[d].Image=null; 

        }
        public bool apple_check()
        {
            int i;
            for (i = 0; i < body.Count; i++)
            { if (apple.x == body[i].x && apple.y == body[i].y) return true ; }
            return false;
        }
        public Form1()
        {
            
            
            InitializeComponent();
            int i;

            //PictureBox pic1 = new PictureBox();
            //this.Controls.Add(pic1);
            //pic1.Location = new Point(0, 0);
            //pic1.Size = new Size(1000, 560);
            //Bitmap back = new Bitmap(Application.StartupPath + "\\snake\\background.png");
            //pic1.Image = back;
            for (i = 1; i <= map_height * map_length; i++)
            {
               
                
                PictureBox pic = new PictureBox();
                pic.Size = new Size(28, 28);
                float t = (float)(i - 1) / map_length;
                int tz = (int)t;
                pic.Location = new Point(28 * ((i - 1) % map_length),28*tz);
                this.Controls.Add(pic);
               //pic.BackColor = Color.Transparent; pic.Parent = pic1;
                pic.BringToFront();
                board.Add(pic);
                // pic.Image = Image.FromFile(Application.StartupPath+"\\snake\\beijing.jpg");

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            
        }
        public void form1_keydown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                        direction = _direction.LEFT; break;
                case Keys.S:
                        direction =_direction.DOWN ; break;
                case Keys.D:
                        direction =_direction.RIGHT; break;
                case Keys.W:
                        direction = _direction.UP; break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            start();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (head.y != body[0].y - 1)
                direction =_direction.UP;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (head.x != body[0].x - 1)
                direction = _direction.LEFT;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (head.y != body[0].y + 1)
                direction = _direction.DOWN;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (head.x != body[0].x + 1)
                direction = _direction.RIGHT;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(play);
            t.Start();
            
            
               
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            timertest.Enabled = true;
        }

        private void timertest_Tick(object sender, EventArgs e)
        {   board[test].BackColor = System.Drawing.Color.Black;
             test++;
             if (test == 100) { timertest.Enabled = false; test = 0; }
        }

      

    
      
    }
}