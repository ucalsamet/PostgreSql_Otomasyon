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
    public partial class Kasa : Form
    {
        public Kasa()
        {
            InitializeComponent();
        }
        connect bgl = new connect();
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;

        void musteriHareketler()
        {
            sql = @"select * from musteri_hareket";
            dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmaHareketler()
        {
            sql = @"select * from firma_hareket";
            dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        private void Kasa_Load(object sender, EventArgs e)
        {
            musteriHareketler();
            firmaHareketler();
            //Toplam Kasa Geliri
            sql = @"Select toplamkazanc()";
            dt = new DataTable();
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblKasaGelir.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Müşteriler
            sql = @"Select musteritoplam()";
            dt = new DataTable();
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr1 = cmd.ExecuteReader();
            while (dr1.Read())
            {
                lblMusteriSayisi.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //Son Ay Giderler
            sql = @"Select odemelertoplam()";
            dt = new DataTable();
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString();
            }
            bgl.baglanti().Close();

            //Son Ay Personel Maaş Toplamı
            sql = @"Select maaslar from giderler order by id desc limit 1";
            dt = new DataTable();
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr3 = cmd.ExecuteReader();
            while (dr3.Read())
            {
                lblPersonelMaas.Text = dr3[0].ToString();
            }
            bgl.baglanti().Close();

            //Firma Toplam
            sql = @"Select firmatoplam()";
            dt = new DataTable();
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr4 = cmd.ExecuteReader();
            while (dr4.Read())
            {
                lblToplamFirma.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();
            //Personel Sayısı
            sql = @"Select personeltoplam()";
            dt = new DataTable();
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr5 = cmd.ExecuteReader();
            while (dr5.Read())
            {
                lblPersonelSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();
            //Toplam Stok
            sql = @"Select stoktoplam()";
            dt = new DataTable();
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr6 = cmd.ExecuteReader();
            while (dr6.Read())
            {
                lblToplamStok.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
