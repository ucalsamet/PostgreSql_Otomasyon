using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Npgsql;

namespace PostgreSql_Otomasyon
{
    public partial class AdminGiris : DevExpress.XtraEditors.XtraForm
    {
        public AdminGiris()
        {
            InitializeComponent();
        }
        connect bgl = new connect();
        private string sql;
        private NpgsqlCommand cmd;
        private void AdminGiris_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sql = @"Select * from admin where kullaniciad=@p1 and sifre=@p2";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtkulad.Text);
            cmd.Parameters.AddWithValue("@p2", txtsifre.Text);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Anamodul fr = new Anamodul();
                fr.Show();
                this.Hide();
            }
            else
            {
                labelControl3.Visible = true;
                labelControl3.Text = "Kullanıcı Adı veya Şifre Yanlış!";
            }
            bgl.baglanti().Close();
        }
    }
}