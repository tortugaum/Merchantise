using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Merchantise
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        int startPoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            CircleProgressBar.Value = startPoint;
            startPoint += 5;
            if(CircleProgressBar.Value == 100)
            {
                CircleProgressBar.Value = 0;
                timer1.Stop();
                LoginForm loginform = new LoginForm();
                this.Hide();
                loginform.Show();
            }
        }
    }
}
