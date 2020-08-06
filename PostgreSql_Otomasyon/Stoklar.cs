using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraCharts;
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
    public partial class Stoklar : Form
    {
        public Stoklar()
        {
            InitializeComponent();
        }
        connect bgl = new connect();
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        private void Stoklar_Load(object sender, EventArgs e)
        {
            bgl.baglanti();
            DataTable dt = new DataTable();
            sql = @"Select ad,sum(adet) as miktar from urunler group by ad";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

           
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]),int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();


            sql = @"select il,count(*) from musteriler group by il";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            NpgsqlDataReader dr1 = cmd.ExecuteReader();
            while (dr1.Read())
            {
                chartControl2.Series["Series 2"].Points.AddPoint(Convert.ToString(dr1[0]), int.Parse(dr1[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            StokDetay fr = new StokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                fr.ad = dr["ad"].ToString();
            }
            fr.Show();
        }
    }
}
