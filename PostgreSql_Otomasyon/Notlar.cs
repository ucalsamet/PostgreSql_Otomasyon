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
    public partial class Notlar : Form
    {
        public Notlar()
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
            sql = @"Select * from notlar";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            bgl.baglanti().Close();
            gridControl1.DataSource = null;
            gridControl1.DataSource = dt;

        }
        void temizle()
        {
            txtId.Text = "";
            txtBaslik.Text = "";
            txtHitap.Text = "";
            txtOlusturan.Text = "";
            mskSaat.Text = "";
            mskTarih.Text = "";
            rchDetay.Text = "";
        }
        private void Notlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"insert into notlar(tarih,saat,baslik,olusturan,hitap,detay) values (@p1,@p2,@p3,@p4,@p5,@p6)";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p2", mskSaat.Text);
            cmd.Parameters.AddWithValue("@p3", txtBaslik.Text);
            cmd.Parameters.AddWithValue("@p4", txtOlusturan.Text);
            cmd.Parameters.AddWithValue("@p5", txtHitap.Text);
            cmd.Parameters.AddWithValue("@p6", rchDetay.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["id"].ToString();
            mskTarih.Text = dr["tarih"].ToString();
            mskSaat.Text = dr["saat"].ToString();
            txtBaslik.Text = dr["baslik"].ToString();
            txtOlusturan.Text = dr["olusturan"].ToString();
            txtHitap.Text = dr["hitap"].ToString();
            rchDetay.Text = dr["detay"].ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"update notlar set tarih=@p1,saat=@p2,baslik=@p3,olusturan=@p4,hitap=@p5,detay=@p6 where id=@p7";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p2", mskSaat.Text);
            cmd.Parameters.AddWithValue("@p3", txtBaslik.Text);
            cmd.Parameters.AddWithValue("@p4", txtOlusturan.Text);
            cmd.Parameters.AddWithValue("@p5", txtHitap.Text);
            cmd.Parameters.AddWithValue("@p6", rchDetay.Text);
            cmd.Parameters.AddWithValue("@p7", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"Delete from notlar where id=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
