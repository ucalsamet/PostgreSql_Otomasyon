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
    public partial class Bankalar : Form
    {
        public Bankalar()
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
            sql = @"Select * from bankalar";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            bgl.baglanti().Close();
            gridControl1.DataSource = null;
            gridControl1.DataSource = dt;

        }
        void firmaListesi()
        {
            bgl.baglanti();
            sql = @"Select id,ad from firmalar";
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            lkpFirma.Properties.ValueMember = "id";
            lkpFirma.Properties.DisplayMember = "ad";
            lkpFirma.Properties.DataSource = dt;

        }
        void sehirListesi()
        {
            bgl.baglanti();
            sql = @"Select cityname from cities";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbİl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void temizle()
        {
            txtId.Text = "";
            txtBankaAd.Text = "";
            txtHesapNo.Text = "";
            txtHesapTür.Text = "";
            txtSube.Text = "";
            txtYetkili.Text = "";
            txtİban.Text = "";
            mskTarih.Text = "";
            mskTel.Text = "";
            lkpFirma.Text = "";
        }
        private void Bankalar_Load(object sender, EventArgs e)
        {
            listele();
            firmaListesi();
            sehirListesi();
            temizle();
        }

        private void cmbİlce_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbİl_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbİlce.Properties.Items.Clear();
            bgl.baglanti();
            sql = @"select countyname from counties where  cityid=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", cmbİl.SelectedIndex + 1);
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
            sql = @"insert into bankalar(ad,il,ilce,sube,iban,hesapno,yetkili,telefon,tarih,hesaptur,firmaid) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtBankaAd.Text);
            cmd.Parameters.AddWithValue("@p2", cmbİl.Text);
            cmd.Parameters.AddWithValue("@p3", cmbİlce.Text);
            cmd.Parameters.AddWithValue("@p4", txtSube.Text);
            cmd.Parameters.AddWithValue("@p5", txtİban.Text);
            cmd.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            cmd.Parameters.AddWithValue("@p7", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@p8", mskTel.Text);
            cmd.Parameters.AddWithValue("@p9", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p10", txtHesapTür.Text);
            cmd.Parameters.AddWithValue("@p11", int.Parse(lkpFirma.EditValue.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["id"].ToString();
            txtBankaAd.Text = dr["ad"].ToString();
            cmbİl.Text = dr["il"].ToString();
            cmbİlce.Text = dr["ilce"].ToString();
            txtSube.Text = dr["sube"].ToString();
            txtİban.Text = dr["iban"].ToString();
            txtHesapNo.Text = dr["hesapno"].ToString();
            txtYetkili.Text = dr["yetkili"].ToString();
            mskTel.Text = dr["telefon"].ToString();
            mskTarih.Text = dr["tarih"].ToString();
            mskTel.Text = dr["telefon"].ToString();
            mskTarih.Text = dr["tarih"].ToString();
            txtHesapTür.Text = dr["hesaptur"].ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"update bankalar set ad=@p1,il=@p2,ilce=@p3,sube=@p4,iban=@p5,hesapno=@p6,yetkili=@p7,telefon=@p8 ,tarih=@p9 ,hesaptur=@p10 ,firmaid=@p11 where id=@p12";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtBankaAd.Text);
            cmd.Parameters.AddWithValue("@p2", cmbİl.Text);
            cmd.Parameters.AddWithValue("@p3", cmbİlce.Text);
            cmd.Parameters.AddWithValue("@p4", txtSube.Text);
            cmd.Parameters.AddWithValue("@p5", txtİban.Text);
            cmd.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            cmd.Parameters.AddWithValue("@p7", txtYetkili.Text);
            cmd.Parameters.AddWithValue("@p8", mskTel.Text);
            cmd.Parameters.AddWithValue("@p9", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p10", txtHesapTür.Text);
            cmd.Parameters.AddWithValue("@p11", lkpFirma.EditValue);
            cmd.Parameters.AddWithValue("@p12", int.Parse(txtId.Text));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Kaydı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"Delete from bankalar where id=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Kaydı Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
