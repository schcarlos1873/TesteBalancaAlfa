using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace TesteBalancaAlfa
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    SendKeys.Send("{TAB}");
                    break;
                default:
                    break;
            }
        }


        //MyKeyPress(ref e);

        private void MyKeyPress(ref KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    MessageBox.Show("F1 Clicado");
                    break;
                case Keys.F2:
                    MessageBox.Show("F2 Clicado");
                    break;
                case Keys.F3:
                    MessageBox.Show("F3 Clicado");
                    break;
                case Keys.F4:
                    MessageBox.Show("F4 Clicado");
                    break;
                case Keys.F5:
                    MessageBox.Show("F5 Clicado");
                    break;
                default:
                    break;
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            MyKeyPress(ref e);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }


    }
}
