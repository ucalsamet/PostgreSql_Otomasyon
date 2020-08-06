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
    public partial class Firmalar : Form
    {
        public Firmalar()
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
            sql = @"Select * from firmalar";
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
                cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtMail.Text = "";
            txtSektör.Text = "";
            txtYetkili.Text = "";
            txtYGorev.Text = "";
            mskFax.Text = "";
            mskTcNo.Text = "";
            mskTel.Text = "";
            rchAdres.Text = "";

        }
        private void Firmalar_Load(object sender, EventArgs e)
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
            txtSektör.Text = dr["sektor"].ToString();
            txtYetkili.Text = dr["yetkili"].ToString();
            txtYGorev.Text = dr["gorev"].ToString();
            mskTcNo.Text = dr["tc"].ToString();
            mskTel.Text = dr["tel"].ToString();
            mskFax.Text = dr["fax"].ToString();
            txtMail.Text = dr["mail"].ToString();
            cmbil.Text = dr["il"].ToString();
            cmbİlce.Text = dr["ilce"].ToString();
            rchAdres.Text = dr["adres"].ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"insert into firmalar(ad,sektor,yetkili,gorev,tc,tel,fax,mail,il,ilce,adres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSektör.Text);
            cmd.Parameters.AddWithValue("@p3", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@p4", txtYGorev.Text);
            cmd.Parameters.AddWithValue("@p5", mskTcNo.Text);
            cmd.Parameters.AddWithValue("@p6", mskTel.Text);
            cmd.Parameters.AddWithValue("@p7", mskFax.Text);
            cmd.Parameters.AddWithValue("@p8", txtMail.Text);
            cmd.Parameters.AddWithValue("@p9", cmbil.Text);
            cmd.Parameters.AddWithValue("@p10", cmbİlce.Text);
            cmd.Parameters.AddWithValue("@p11", rchAdres.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"update firmalar set ad=@p1,sektor=@p2,yetkili=@p3,gorev=@p4,tc=@p5,tel=@p6,fax=@p7,mail=@p8 ,il=@p9 ,ilce=@p10 ,adres=@p11 where id=@p12";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSektör.Text);
            cmd.Parameters.AddWithValue("@p3", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@p4", txtYGorev.Text);
            cmd.Parameters.AddWithValue("@p5", mskTcNo.Text);
            cmd.Parameters.AddWithValue("@p6", mskTel.Text);
            cmd.Parameters.AddWithValue("@p7", mskFax.Text);
            cmd.Parameters.AddWithValue("@p8", txtMail.Text);
            cmd.Parameters.AddWithValue("@p9", cmbil.Text);
            cmd.Parameters.AddWithValue("@p10", cmbİlce.Text);
            cmd.Parameters.AddWithValue("@p11", rchAdres.Text);
            cmd.Parameters.AddWithValue("@p12", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"Delete from firmalar where id=@p1";
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

        private void cmbİlce_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbİlce.Properties.Items.Clear();
            bgl.baglanti();
            sql = @"select countyname from counties where  cityid=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbİlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sql = @"Select * from firmalar where ad like '%" + txtBul.Text + "%'";
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.baglanti().Close();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbİlce.Properties.Items.Clear();
            bgl.baglanti();
            sql = @"select countyname from counties where  cityid=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbİlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
    }
}
