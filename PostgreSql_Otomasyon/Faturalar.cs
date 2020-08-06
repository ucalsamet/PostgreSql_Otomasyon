using DevExpress.ClipboardSource.SpreadsheetML;
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
    public partial class Faturalar : Form
    {
        public Faturalar()
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
            sql = @"Select * from faturabilgi";
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
            txtSeri.Text = "";
            txtSiraNo.Text = "";
            mskTarih.Text = "";
            mskSaat.Text = "";
            txtAlici.Text = "";
            txtTeslimEden.Text = "";
            txtTeslimAlan.Text = "";
            txtUrunId.Text = "";
            txtUrunId2.Text = "";
            lkeFirma.Text = "";
            lkeMusteri.Text = "";
            lkePersonel.Text = "";
            lkePersonel2.Text = "";
            lkeUrunAd.Text = "";
            lkeUrunAd2.Text = "";
            txtFaturaId.Text = "";
            txtFaturaId2.Text = "";
            txtFiyat.Text = "";
            txtFiyat2.Text = "";
            txtMiktar.Text = "";
            txtMiktar2.Text = "";
        }

        void urunListesi()
        {
            dt = new DataTable();
            sql = @"Select id,ad from urunler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql,bgl.baglanti());
            da.Fill(dt);
            lkeUrunAd.Properties.ValueMember = "id";
            lkeUrunAd.Properties.DisplayMember = "ad";
            lkeUrunAd.Properties.DataSource = dt;
            lkeUrunAd2.Properties.ValueMember = "id";
            lkeUrunAd2.Properties.DisplayMember = "ad";
            lkeUrunAd2.Properties.DataSource = dt;
        }

        void personelListesi()
        {
            dt = new DataTable();
            sql = @"Select id,(ad||' '||soyad) as AdSoyad from personeller";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            lkePersonel.Properties.ValueMember = "id";
            lkePersonel.Properties.DisplayMember = "AdSoyad";
            lkePersonel.Properties.DataSource = dt;
            lkePersonel2.Properties.ValueMember = "id";
            lkePersonel2.Properties.DisplayMember = "AdSoyad";
            lkePersonel2.Properties.DataSource = dt;
        }
        void musteriListesi()
        {
            dt = new DataTable();
            sql = @"Select id,(ad||' '||soyad) as AdSoyad from musteriler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            lkeMusteri.Properties.ValueMember = "id";
            lkeMusteri.Properties.DisplayMember = "AdSoyad";
            lkeMusteri.Properties.DataSource = dt;
        }

        void firmaListesi()
        {
            dt = new DataTable();
            sql = @"Select id,ad from firmalar";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            lkeFirma.Properties.ValueMember = "id";
            lkeFirma.Properties.DisplayMember = "ad";
            lkeFirma.Properties.DataSource = dt;
        }
        private void Faturalar_Load(object sender, EventArgs e)
        {
            listele();
            urunListesi();
            personelListesi();
            musteriListesi();
            firmaListesi();
        }

        private void lkeUrunAd_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnKaydetMüsteri_Click(object sender, EventArgs e)
        {
            if (txtFaturaId.Text=="")
            {
                bgl.baglanti();
                sql = @"insert into faturabilgi(seri,sirano,tarih,saat,alici,teslimeden,teslimalan) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)";
                cmd = new NpgsqlCommand(sql, bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtSeri.Text);
                cmd.Parameters.AddWithValue("@p2", txtSeri.Text);
                cmd.Parameters.AddWithValue("@p3", mskTarih.Text);
                cmd.Parameters.AddWithValue("@p4", mskSaat.Text);
                cmd.Parameters.AddWithValue("@p5", txtAlici.Text);
                cmd.Parameters.AddWithValue("@p6", txtTeslimEden.Text);
                cmd.Parameters.AddWithValue("@p7", txtTeslimAlan.Text);
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (txtFaturaId.Text != "" && cmbCariTür.Text == "Müşteri")
            {
                if (int.Parse(txtMiktar.Text.ToString()) <= int.Parse(txtUrunAdet.Text.ToString()))
                {
                    double miktar, fiyat, tutar;
                    fiyat = Convert.ToDouble(txtFiyat.Text);
                    miktar = Convert.ToDouble(txtMiktar.Text);
                    tutar = fiyat * miktar;
                    txtTutar.Text = tutar.ToString();
                    sql = @"insert into faturadetay(urunad,miktar,fiyat,tutar,faturaid,urunid) values (@p1,@p2,@p3,@p4,@p5,@p6)";
                    cmd = new NpgsqlCommand(sql, bgl.baglanti());
                    cmd.Parameters.AddWithValue("@p1", lkeUrunAd.Text);
                    cmd.Parameters.AddWithValue("@p2",int.Parse(txtMiktar.Text.ToString()));
                    cmd.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat.Text));
                    cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
                    cmd.Parameters.AddWithValue("@p5",int.Parse(txtFaturaId.Text.ToString()));
                    cmd.Parameters.AddWithValue("@p6", int.Parse(txtUrunId.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ürün Satılması Gerçekleştirildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bgl.baglanti().Close();

                    //Müşteri Hareketler Tablosuna veri girişi

                    sql = @"insert into musterihareketler(urunid,adet,personelid,musteriid,fiyat,toplam,faturaid,tarih) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)";
                    NpgsqlCommand cmd1 = new NpgsqlCommand(sql, bgl.baglanti());
                    cmd1.Parameters.AddWithValue("@h1", int.Parse(txtUrunId.Text.ToString()));
                    cmd1.Parameters.AddWithValue("@h2", int.Parse(txtUrunAdet.Text));
                    cmd1.Parameters.AddWithValue("@h3", int.Parse(lkePersonel.EditValue.ToString()));
                    cmd1.Parameters.AddWithValue("@h4", int.Parse(lkeMusteri.EditValue.ToString()));
                    cmd1.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat.Text));
                    cmd1.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar.Text));
                    cmd1.Parameters.AddWithValue("@h7", int.Parse(txtFaturaId.Text));
                    cmd1.Parameters.AddWithValue("@h8", mskTarih.Text);
                    cmd1.ExecuteNonQuery();
                    bgl.baglanti().Close();
                }
            }
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            sql = @"Select id,satisfiyat,adet from urunler where ad=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", lkeUrunAd.Text);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtUrunId.Text = dr["id"].ToString();
                txtFiyat.Text = dr["satisfiyat"].ToString();
                txtUrunAdet.Text = dr["adet"].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnBul2_Click(object sender, EventArgs e)
        {
            sql = @"Select id,satisfiyat,adet from urunler where ad=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", lkeUrunAd2.Text);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtUrunId2.Text = dr["id"].ToString();
                txtFiyat2.Text = dr["satisfiyat"].ToString();
                txtUrunAdet2.Text = dr["adet"].ToString();
            }
            bgl.baglanti().Close();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["id"].ToString();
            txtSeri.Text = dr["seri"].ToString();
            txtSiraNo.Text = dr["sirano"].ToString();
            mskTarih.Text = dr["tarih"].ToString();
            mskSaat.Text = dr["saat"].ToString();
            txtAlici.Text = dr["alici"].ToString();
            txtTeslimEden.Text = dr["teslimeden"].ToString();
            txtTeslimAlan.Text = dr["teslimalan"].ToString();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FaturaDetay fr = new FaturaDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                fr.id = dr["id"].ToString();
            }
            fr.Show();
        }

        private void btnKaydetFirma_Click(object sender, EventArgs e)
        {
            if (txtFaturaId2.Text == "")
            {
                bgl.baglanti();
                sql = @"insert into faturabilgi(seri,sirano,tarih,saat,alici,teslimeden,teslimalan) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)";
                cmd = new NpgsqlCommand(sql, bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtSeri.Text);
                cmd.Parameters.AddWithValue("@p2", txtSeri.Text);
                cmd.Parameters.AddWithValue("@p3", mskTarih.Text);
                cmd.Parameters.AddWithValue("@p4", mskSaat.Text);
                cmd.Parameters.AddWithValue("@p5", txtAlici.Text);
                cmd.Parameters.AddWithValue("@p6", txtTeslimEden.Text);
                cmd.Parameters.AddWithValue("@p7", txtTeslimAlan.Text);
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (txtFaturaId2.Text != "" && cmbCariTür.Text == "Firma")
            {
                if (int.Parse(txtMiktar2.Text) <= int.Parse(txtUrunAdet2.Text))
                {
                    double miktar, fiyat, tutar;
                    fiyat = Convert.ToDouble(txtFiyat2.Text);
                    miktar = Convert.ToDouble(txtMiktar2.Text);
                    tutar = fiyat * miktar;
                    txtTutar2.Text = tutar.ToString();
                    sql = @"insert into faturadetay(urunad,miktar,fiyat,tutar,faturaid,urunid) values (@p1,@p2,@p3,@p4,@p5,@p6)";
                    cmd = new NpgsqlCommand(sql, bgl.baglanti());
                    cmd.Parameters.AddWithValue("@p1", lkeUrunAd2.Text);
                    cmd.Parameters.AddWithValue("@p2", int.Parse(txtMiktar2.Text.ToString()));
                    cmd.Parameters.AddWithValue("@p3", decimal.Parse(txtFiyat2.Text));
                    cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar2.Text));
                    cmd.Parameters.AddWithValue("@p5", int.Parse(txtFaturaId2.Text.ToString()));
                    cmd.Parameters.AddWithValue("@p6", int.Parse(txtUrunId2.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ürün Satılması Gerçekleştirildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bgl.baglanti().Close(); 

                    //Firma Hareketler Tablosuna veri girişi

                    sql = @"insert into firmahareketler(urunid,adet,personelid,firmaid,fiyat,toplam,faturaid,tarih) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)";
                    NpgsqlCommand cmd1 = new NpgsqlCommand(sql, bgl.baglanti());
                    cmd1.Parameters.AddWithValue("@h1", int.Parse(txtUrunId2.Text.ToString()));
                    cmd1.Parameters.AddWithValue("@h2", int.Parse(txtUrunAdet2.Text));
                    cmd1.Parameters.AddWithValue("@h3", int.Parse(lkePersonel2.EditValue.ToString()));
                    cmd1.Parameters.AddWithValue("@h4", int.Parse(lkeFirma.EditValue.ToString()));
                    cmd1.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat2.Text));
                    cmd1.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar2.Text));
                    cmd1.Parameters.AddWithValue("@h7", int.Parse(txtFaturaId2.Text));
                    cmd1.Parameters.AddWithValue("@h8", mskTarih.Text);
                    cmd1.ExecuteNonQuery();
                    bgl.baglanti().Close();
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"update faturabilgi set seri=@p1,sirano=@p2,tarih=@p3,saat=@p4,alici=@p5,teslimeden=@p6,teslimalan=@p7 where id=@p8";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtSeri.Text);
            cmd.Parameters.AddWithValue("@p2", txtSiraNo.Text);
            cmd.Parameters.AddWithValue("@p3", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p4", mskSaat.Text);
            cmd.Parameters.AddWithValue("@p5", txtAlici.Text);
            cmd.Parameters.AddWithValue("@p6", txtTeslimEden.Text);
            cmd.Parameters.AddWithValue("@p7", txtTeslimAlan.Text);
            cmd.Parameters.AddWithValue("@p8", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"Delete from faturabilgi where id=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
    }
}
