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
    public partial class Musteriler : Form
    {
        public Musteriler()
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
            sql = @"Select * from musteriler";
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
        private void Musteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            temizle();
        }

        private void cmdİl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbİlce.Properties.Items.Clear();
            bgl.baglanti();
            sql = @"select countyname from counties where  cityid=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",cmdİl.SelectedIndex + 1);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbİlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"insert into musteriler(ad,soyad,tel,tc,mail,il,ilce,adres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTel.Text);
            cmd.Parameters.AddWithValue("@p4", mskTcNo.Text);
            cmd.Parameters.AddWithValue("@p5",txtMail.Text);
            cmd.Parameters.AddWithValue("@p6", cmdİl.Text);
            cmd.Parameters.AddWithValue("@p7", cmbİlce.Text);
            cmd.Parameters.AddWithValue("@p8",rchAdres.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
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

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"update musteriler set ad=@p1,soyad=@p2,tel=@p3,tc=@p4,mail=@p5,il=@p6,ilce=@p7,adres=@p8 where id=@p9";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTel.Text);
            cmd.Parameters.AddWithValue("@p4", mskTcNo.Text);
            cmd.Parameters.AddWithValue("@p5", txtMail.Text);
            cmd.Parameters.AddWithValue("@p6", cmdİl.Text);
            cmd.Parameters.AddWithValue("@p7", cmbİlce.Text);
            cmd.Parameters.AddWithValue("@p8", rchAdres.Text);
            cmd.Parameters.AddWithValue("@p9", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"Delete from musteriler where id=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void rchAdres_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void mskTcNo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtSoyad_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void txtAd_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void txtMail_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbİlce_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mskTel_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sql = @"Select * from musteriler where ad like '%" + txtBul.Text + "%'";
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.baglanti().Close();
        }
    }
}
