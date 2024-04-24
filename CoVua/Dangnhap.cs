﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
namespace CoVua
{
    public partial class Dangnhap : Form
    {
        public Dangnhap()
        {
            InitializeComponent();
        }
        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "NTJq136Kdo8cmFkPh3Fml88nvgEXl2Md6Bw6JbFS",
            BasePath = "https://team14-database-default-rtdb.firebaseio.com/",
        };
        IFirebaseClient client;

        private void Dangnhap_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch
            {
                MessageBox.Show("Connection failed");
            }
        }
        public static string email = "";
        public static string coins;
        private void button2_Click(object sender, EventArgs e)
        {
            new Dangki().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
            else
            {
                FirebaseResponse response = client.Get("Information/");
                Dictionary<string, register> result = response.ResultAs<Dictionary<string, register>>();
                foreach (var get in result)
                {
                    string usesrname = get.Value.Name;
                    string password = get.Value.Password;
                    if (usesrname == txtUser.Text && password == txtPass.Text)
                    {
                        email = get.Value.Email;
                        MessageBox.Show(" Đăng nhập thành công. Chào mừng " + txtUser.Text, "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        usesrname = txtUser.Text;
                        new Trangchu().Show();
                        this .Hide();
                        txtUser.Text = "";
                        txtPass.Text = "";
                        break;
                    }
                    //if (usesrname != txtUser.Text && password != txtPass.Text)
                    //{
                    //    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu"); break;
                    //}
                    if ((usesrname == txtUser.Text && password != txtPass.Text))
                    {
                        MessageBox.Show("Sai mật khẩu.", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    if ((usesrname != txtUser.Text && password == txtPass.Text))
                    {
                        MessageBox.Show("Sai tên đăng nhập", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }


                }



            }
        }
    }
}
