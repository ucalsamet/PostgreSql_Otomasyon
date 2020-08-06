using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSql_Otomasyon
{
    public partial class Personeller : Form
    {
        public Personeller()
        {
            InitializeComponent();
        }
        connect bgl = new connect();
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;

        void listele()
        {
            bgl.baglanti();
            sql = @"Select * from personeller";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            bgl.baglanti().Close();
            gridControl1.DataSource = null;
            gridControl1.DataSource = dt;

        }
        void sehirListesi()
        {
            bgl.baglanti();
            sql = @"Select cityname from cities";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmdİl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtMail.Text = "";
            cmbİlce.Text = "";
            cmdİl.Text = "";
            rchAdres.Text = "";
            mskTcNo.Text = "";
            mskTel.Text = "";
        }
        private void Personeller_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            temizle();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["id"].ToString();
            txtAd.Text = dr["ad"].ToString();
            txtSoyad.Text = dr["soyad"].ToString();
            mskTel.Text = dr["tel"].ToString();
            mskTcNo.Text = dr["tc"].ToString();
            txtMail.Text = dr["mail"].ToString();
            cmdİl.Text = dr["il"].ToString();
            cmbİlce.Text = dr["ilce"].ToString();
            rchAdres.Text = dr["adres"].ToString();
            txtGorev.Text = dr["gorev"].ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"insert into personeller(ad,soyad,tel,tc,mail,il,ilce,adres,gorev) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTel.Text);
            cmd.Parameters.AddWithValue("@p4", mskTcNo.Text);
            cmd.Parameters.AddWithValue("@p5", txtMail.Text);
            cmd.Parameters.AddWithValue("@p6", cmdİl.Text);
            cmd.Parameters.AddWithValue("@p7", cmbİlce.Text);
            cmd.Parameters.AddWithValue("@p8", rchAdres.Text);
            cmd.Parameters.AddWithValue("@p9", txtGorev.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"update personeller set ad=@p1,soyad=@p2,tel=@p3,tc=@p4,mail=@p5,il=@p6,ilce=@p7,adres=@p8 ,gorev=@p9 where id=@p10";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTel.Text);
            cmd.Parameters.AddWithValue("@p4", mskTcNo.Text);
            cmd.Parameters.AddWithValue("@p5", txtMail.Text);
            cmd.Parameters.AddWithValue("@p6", cmdİl.Text);
            cmd.Parameters.AddWithValue("@p7", cmbİlce.Text);
            cmd.Parameters.AddWithValue("@p8", rchAdres.Text);
            cmd.Parameters.AddWithValue("@p9", txtGorev.Text);
            cmd.Parameters.AddWithValue("@p10", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"Delete from personeller where id=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Kaydı Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void cmdİl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbİlce.Properties.Items.Clear();
            bgl.baglanti();
            sql = @"select countyname from counties where  cityid=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", cmdİl.SelectedIndex + 1);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbİlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void cmbİlce_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sql = @"Select * from personeller where ad like '%" + txtBul.Text + "%'";
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.baglanti().Close();
        }
    }
}
