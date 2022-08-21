using System;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public readonly int width = 1000;
        public readonly int height = 1000;
        public readonly int widthb = 600;
        public readonly int heightb = 600;
        public readonly int sizecube = 50; //10 - 38 : 30 - 12 : 40 - 9 : 50 - 7 : 60 - 6 : 70 - 5 : 80 - 4 : 90 - 4 : 100 - 3
        public int Xcube { get; set; }
        public int Ycube { get; set; }
        public int Xeat { get; set; }
        public int Yeat { get; set; }
        public int point = 0;
        public int error = 7;
        PictureBox eat = new PictureBox();
        PictureBox cube = new PictureBox();
        double[,] vision = new double[8, 3];

        public Form1()
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            Start();
            this.KeyDown += new KeyEventHandler(OKP);
        }

        private void Start()
        {
            cube.Width = sizecube;
            cube.Height = sizecube;
            Generationmap();
            Weight();
            Cube();
            Eat();
        }
        public void Сoordinates_system()
        {
            Xcube = cube.Location.X / sizecube - error;
            Ycube = cube.Location.Y / sizecube;
            Xeat = eat.Location.X / sizecube - error;
            Yeat = eat.Location.Y / sizecube;
        } //система координат
        private void Generationmap()
        {
            for (int i = 0; i < widthb / sizecube + 1; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.BackColor = Color.DarkGray;
                pictureBox.Location = new Point(width - widthb - 17, sizecube * i);
                pictureBox.Size = new Size(widthb, 1);
                this.Controls.Add(pictureBox);
            }
            for (int i = 0; i < heightb / sizecube + 1; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.BackColor = Color.DarkGray;
                pictureBox.Location = new Point(height - heightb - 17 + sizecube * i, 1);
                pictureBox.Size = new Size(1, heightb);
                this.Controls.Add(pictureBox);
            }
        } //генерация карты
        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    if (cube.Location.X < width - (sizecube + 17))
                    {
                        cube.Location = new Point(cube.Location.X + sizecube, cube.Location.Y);
                    }
                    break;
                case "Left":
                    if (cube.Location.X > width - widthb - 17)
                    {
                        cube.Location = new Point(cube.Location.X - sizecube, cube.Location.Y);
                    }
                    break;
                case "Up":
                    if (cube.Location.Y > 1)
                    {
                        cube.Location = new Point(cube.Location.X, cube.Location.Y - sizecube);
                    }
                    break;
                case "Down":
                    if (cube.Location.Y < heightb - sizecube)
                    {
                        cube.Location = new Point(cube.Location.X, cube.Location.Y + sizecube);
                    }
                    break;
            }
            Eat_take();
            Сoordinates_system();
            Vision();
            Perceptron1();
            Text_pointp();
        } //управеление кубом
        private void Text_pointp()
        {
            label1.Text = $"points {point}\ncube X:{Xcube}\ncube Y:{Ycube}\neat X:{Xeat}\neat Y:{Yeat}\n  {vision[7, 2]}      {vision[0, 2]}      {vision[1, 2]}\n     {vision[7, 1]}   {vision[0, 1]}   {vision[1, 1]}\n       {vision[7, 0]} {vision[0, 0]} {vision[1, 0]}\n {vision[6, 2]} {vision[6, 1]} {vision[6, 0]}    {vision[2, 0]} {vision[2, 1]} {vision[2, 2]}\n       {vision[5, 0]} {vision[4, 0]} {vision[3, 0]}\n     {vision[5, 1]}   {vision[4, 1]}   {vision[3, 1]}\n  {vision[5, 2]}      {vision[4, 2]}      {vision[3, 2]}";
        } //сис инфо
        public void Cube()
        {
            cube.BackColor = Color.Red;
            cube.Location = new Point(width - widthb - 17, 0);
            cube.Size = new Size(sizecube, sizecube);
            this.Controls.Add(cube);
        } //код отображения куба
        public void Eat_Location()
        {
            do
            {
                eat.Location = new Point(((int)(Math.Round((double)((new Random().Next(width - widthb - 17 + sizecube, width - 17)) / sizecube))) * sizecube - 17), ((int)(Math.Round((double)((new Random().Next(1, heightb)) / sizecube))) * sizecube));
            } while (eat.Location == cube.Location);
        } //спаун еды
        public void Eat()
        {
            eat.BackColor = Color.Green;
            Eat_Location();
            eat.Size = new Size(sizecube, sizecube);
            this.Controls.Add(eat);
        }  //код отображения еды
        private void Eat_take()
        {
            if (eat.Location == cube.Location)
            {
                Eat_Location();
                point++;
            }
        } //взять еду
        public void Vision()
        {
            bool[,] varition = new bool[8, 2];
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    varition[0, 0] = Yeat == Ycube - (i + 1) && Xcube == Xeat;
                    varition[0, 1] = 0 <= Ycube - (i + 1);
                    varition[1, 0] = Yeat == Ycube - (i + 1) && Xeat == Xcube + (i + 1);
                    varition[1, 1] = 0 <= Ycube - (i + 1) && heightb / sizecube - 1 >= Xcube + (i + 1);
                    varition[2, 0] = Xeat == Xcube + (i + 1) && Ycube == Yeat;
                    varition[2, 1] = widthb / sizecube - 1 >= Xcube + (i + 1);
                    varition[3, 0] = Yeat == Ycube + (i + 1) && Xeat == Xcube + (i + 1);
                    varition[3, 1] = heightb / sizecube - 1 >= Ycube + (i + 1) && widthb / sizecube - 1 >= Xcube + (i + 1);
                    varition[4, 0] = Yeat == Ycube + (i + 1) && Xcube == Xeat;
                    varition[4, 1] = heightb / sizecube - 1 >= Ycube + (i + 1);
                    varition[5, 0] = Yeat == Ycube + (i + 1) && Xeat == Xcube - (i + 1);
                    varition[5, 1] = 0 <= Xcube - (i + 1) && heightb / sizecube - 1 >= Ycube + (i + 1);
                    varition[6, 0] = Xeat == Xcube - (i + 1) && Ycube == Yeat;
                    varition[6, 1] = 0 <= Xcube - (i + 1);
                    varition[7, 0] = Yeat == Ycube - (i + 1) && Xeat == Xcube - (i + 1);
                    varition[7, 1] = 0 <= Ycube - (i + 1) && 0 <= Xcube - (i + 1);

                    if (varition[j, 0])
                    {
                        vision[j, i] = 0.5f;
                    }
                    else if (varition[j, 1])
                    {
                        vision[j, i] = 0f;
                    }
                    else
                    {
                        vision[j, i] = 1f;
                    }
                }
            }

        } // зрение куба
        public void Weight()
        {

        }
        public double ReLU(double x)
        {
            if (x > 0)
            {
                return x;
            }
            return 0;
        }
        public void Perceptron1()
        {

        } //перцептрон
        public void Perceptron2()
        {

        }
        private void timer1_Tick(object state, EventArgs e)
        {

        }
    }
}