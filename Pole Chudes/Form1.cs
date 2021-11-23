using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pole_Chudes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!poleChudes1.onAnimation)
            {
                poleChudes1.setQuestion();
            }  
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            //Screen.PrimaryScreen.WorkingArea.Width
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                if (MessageBox.Show("Вы уверены что хотите закрыть программу?", "Выйти?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }*/
        }
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("Вы ещё об этом пожалеете.", "Страшна", MessageBoxButtons.OK);
            
        }



        private void poleChudes1_Click(object sender, EventArgs e)
        {

        }

        private void poleChudes1_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}
