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
    public partial class Rehber : Form
    {
        public Rehber()
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
            sql = @"Select ad,soyad,tel,mail from musteriler";
            dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            sql = @"Select ad,tel,fax,mail from firmalar";
            DataTable dt1 = new DataTable();
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da1.Fill(dt1);
            gridControl2.DataSource = dt1;
        }
        private void Rehber_Load(object sender, EventArgs e)
        {
            listele();
        }
    }
}
