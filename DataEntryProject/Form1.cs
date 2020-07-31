using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataEntryProject
{
    public partial class frmDataEntry : Form
    {
        private TimeSpan elapsedTime;
        private DateTime lastElapsed;

        public frmDataEntry()
        {
           
            InitializeComponent();
            grbDataEntry.Enabled = false;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();   
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtAdress.Clear();
            txtCity.Clear();
            txtName.Clear();
            txtState.Clear();
            txtZip.Clear();
            
            txtName.Focus();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            grbDataEntry.Enabled = true;
            txtName.Focus();

            timTimer.Enabled = true;
            lastElapsed = DateTime.Now;
            
            
        }

        private void TimTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime += DateTime.Now - lastElapsed;
            lastElapsed = DateTime.Now;

            txtTimer.Text = Convert.ToString(new TimeSpan(elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds));
            
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;

            timTimer.Enabled = false;
            grbDataEntry.Enabled = false;
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            string DataEntry;

            if (txtName.Text.Equals("") || txtAdress.Text.Equals("") || txtCity.Text.Equals("") ||
                txtState.Text.Equals("") || txtZip.Text.Equals(""))
            {
                MessageBox.Show("Each Box Have To Be Filled","Invalid Input",MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtName.Focus();
                return;
            }

            DataEntry = txtName.Text + "\r\n" + txtCity.Text + "\r\n" + txtState.Text + "\r\n" + txtZip.Text + "\r\n" +
                        txtAdress.Text;
            MessageBox.Show(DataEntry);

            btnClear.PerformClick();
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            string txtBoxSender = ((TextBox) sender).Name;

            if (e.KeyChar == 13)
            {
                switch (txtBoxSender)
                {
                    case "txtName":
                        txtAdress.Focus();
                        break;
                    case "txtAdress":
                        txtCity.Focus();
                        break;
                    case "txtCity":
                        txtState.Focus();
                        break;
                    case "txtState":
                        txtZip.Focus();
                        break;
                    case "txtZip":
                        btnAccept.Focus();
                        break;

                }
                e.Handled = true; // for deletng the sound of the Enter press
                
            }

            if (txtBoxSender == "txtZip")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void btnAccept_Hover(object sender, EventArgs e)
        {
            Color cl = Color.OrangeRed;
            Button acceptButton = ((Button) sender);
            acceptButton.BackColor = cl;
            
        }

        private void BtnAccept_MouseLeave(object sender, EventArgs e)
        {
            Button acceptButton = ((Button)sender);
            acceptButton.BackColor = DefaultBackColor;
        }
    }
}
