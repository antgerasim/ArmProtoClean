using System;
using System.Windows.Forms;
using ArmProtoClean.Sql;

namespace ArmProtoClean.Design
{

    //public partial class Loginform : Form
    //{
    //    public Loginform()
    //    {
    //        InitializeComponent();
    //    }
    //}
    public partial class Loginform : Form
    {

        public Loginform() { InitializeComponent(); }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlManager.FillCredentials(tbServerName.Text, tbServerAddress.Text, tbLogin.Text, tbPassword.Text);
            //SqlManager.CreateConnectionString(tbServerName.Text, tbServerAddress.Text, tbLogin.Text, tbPassword.Text);
            //SqlManager.CreateCountQuery(tbServerAddress.Text);

            Close();
            //Application.Exit();

        }

        //Loginform_FormClosing(this,new EventArgs);
    }

}