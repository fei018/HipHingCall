using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HHCSPHelp;
using System.IO;

namespace HHCallTools
{
    public partial class FormCall : Form
    {
        public FormCall()
        {
            InitializeComponent();

            txtDayCalls.Text = "3";
            txtStartDate.Text = "01/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            txtEndDate.Text = DateTime.Today.ToShortDateString();

            this.Activate();
            this.Focus();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                CSPLoginSet.DayCalls = txtDayCalls.Text.Trim();
                CSPLoginSet.EndDate = txtEndDate.Text.Trim();
                CSPLoginSet.Password = txtPwd.Text.Trim();
                CSPLoginSet.StartDate = txtStartDate.Text.Trim();
                CSPLoginSet.LoginId = txtUserId.Text.Trim();
                CSPLoginSet.Assignto = txtAssignto.Text.Trim();
                //CheckCSPLoginSet();

                CSPLoginSet.AppExit = false;
                this.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private void CheckCSPLoginSet()
        //{
        //    try
        //    {
        //        if (DateTime.Today.CompareTo(DateTime.Parse(CSPLoginSet.EndDate)) < 0)
        //        {
        //            throw new Exception("EndDate Error.");
        //        }
        //        if (string.IsNullOrWhiteSpace(CSPLoginSet.UserId))
        //        {
        //            throw new Exception("LoginName Empty.");
        //        }
        //        if (string.IsNullOrWhiteSpace(CSPLoginSet.Password))
        //        {
        //            throw new Exception("Password Empty.");
        //        }
        //        if (string.IsNullOrWhiteSpace(CSPLoginSet.DayCalls))
        //        {
        //            throw new Exception("DayCalls Empty.");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtAssignto.AppendText(e.KeyChar.ToString());
        }
    }
}
