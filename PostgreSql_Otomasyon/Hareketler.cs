using DevExpress.XtraReports.Design;
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
    public partial class Hareketler : Form
    {
        public Hareketler()
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
        private void Hareketler_Load(object sender, EventArgs e)
        {
            musteriHareketler();
            firmaHareketler();
        }
    }
}
