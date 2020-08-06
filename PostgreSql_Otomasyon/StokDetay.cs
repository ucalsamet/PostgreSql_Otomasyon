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
    public partial class StokDetay : Form
    {
        public StokDetay()
        {
            InitializeComponent();
        }
        connect bgl = new connect();
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        public string ad;
        private void StokDetay_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            sql = @"Select * from urunler where ad='"+ ad +"'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql,bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
    }
}
