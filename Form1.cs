using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Minesweeper : Form
    {
        public Minesweeper()
        {
            InitializeComponent();
           // Game();
           // Click();
        }



       public static int m = 100;
        int n = 10, mines = 20, flags = 10;
        Button[,] b = new Button[m, m];
        int[,] a = new int[m, m];
        bool[,] bomb = new bool[m ,m];
        bool[,] flag = new bool[m, m];
        int[] di= new int[] {0, 0, -1, 1, 1, 1, -1, -1};
        int[] dj = new int[] {-1, 1, 0, 0, 1, -1, -1, 1};
        bool[,] check = new bool[m, m];
        int cnt = 0;
         int width = 40;
         int height = 40;
         
        int startw = 10;
        int starth = 50;
        bool start = true;    
        struct pi
        {
            public int first, second;
        }
        

        public void Easy()
        {
            n = 10;
            mines = 20;
            flags = 40;
            cnt = n * n;
            Game();
        }

        public void Normal()
        {
            n = 15;
            mines = 35;
            flags = 70;
            this.Size = new Size(638, 700);
            cnt = n * n;
            Game();
        }
        public void Hard()
        {
            n = 20;
            mines = 70;
            flags = 100;
            this.Size = new Size(840, 900);
            cnt = n * n;
            Game();
        }

       
        public void Game()
        {

           // Easy();
           // cnt = n * n;
            textBox1.Text = cnt.ToString();
            textBox3.Text = flags.ToString();
            
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    Button button= new Button();
                    button.Top = (starth+x * height );
                    button.Left = (startw+y * width );
                    button.Width = width;
                    button.Height = height;
                    button.BackColor = Color.CornflowerBlue;
                    button.TabStop = false;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 1;
                    button.FlatAppearance.BorderColor = Color.DarkOrange;
                    button.MouseUp += new MouseEventHandler(Click);
                    Controls.Add(button);

                    b[x, y] = button; 
                }

            }


        }

        private void Click(object sender, MouseEventArgs e)
        {
            Point coord = ((Button)sender).Location;
            int x = (coord.Y-starth)/width;
            int y = (coord.X-startw)/height;
            if(start == true)
            {
                Random r = new Random();
                int i, j;

                while (mines > 0)
                {
                    i = r.Next(1, n-2);
                    j = r.Next(1, n-2);
                    if (!bomb[i, j] && !(i == x && j == y) && !(i == x && j+1 == y) && !(i == x && j - 1 == y) && !(i + 1 == x && j == y) && !(i - 1 == x && j == y) )
                    {
                        bomb[i, j] = true;
                        a[i + 1, j]++;
                        a[i, j + 1]++;
                        a[i - 1, j]++;
                        a[i, j - 1]++;
                        mines--;

                    }


                }
                start = false;
            }
            if(!flag[x, y])
            if (e.Button == MouseButtons.Left)
            {
                if (check[x, y] == false)
                {


                    if (bomb[x, y])
                    {

                        
                        b[x, y].BackColor = Color.Black;
                        b[x, y].Text = "BOMB";
                        b[x, y].BackgroundImage = Properties.Resources.bomb;
                        b[x, y].ForeColor = Color.White;
                        b[x, y].Font = new Font("Arial", 6, FontStyle.Bold);
                        MessageBox.Show("You lost...");
                        this.Close();

                    }
                    else
                    {
                        b[x, y].BackColor = Color.Red;
                        Queue<pi> q = new Queue<pi>();
                        q.Enqueue(new pi { first = x, second = y });

                        while (q.Count > 0)
                        {
                            pi tile = q.Peek();
                            q.Dequeue();
                            int i, j;
                            i = tile.first;
                            j = tile.second;
                            if (check[i, j] == true) continue;
                            check[i, j] = true;
                            cnt--;
                            textBox1.Text = cnt.ToString();
                            if (i < 0 || j < 0 || i >= n || j >= n)
                                continue;
                            b[i, j].BackColor = Color.LightSlateGray;
                            if (a[i, j] > 0)
                            {
                                b[i, j].Text = a[i, j].ToString();
                                continue;
                            }

                            for (int k = 0; k < 4; k++)
                            {
                                int i_next, j_next;
                                i_next = i + di[k];
                                j_next = j + dj[k];
                                if (i_next < 0 || j_next < 0 || i_next >= n || j_next >= n)
                                    continue;
                                q.Enqueue(new pi { first = i_next, second = j_next });

                            }

                        }

                    }


                    if (cnt == 0)
                    {
                        MessageBox.Show("Congratulations. You WON!!!");
                        this.Close();
                    }
                }
            }
            else
            {
                if(flags > 0)
                {
                    flags--;
                    cnt--;
                    flag[x, y] = true;
                    textBox1.Text = cnt.ToString();
                    textBox3.Text = flags.ToString();
                    b[x, y].BackgroundImage = Properties.Resources.flag;




                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

   
}
