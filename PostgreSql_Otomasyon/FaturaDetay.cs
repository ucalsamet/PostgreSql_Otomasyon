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
    public partial class FaturaDetay : Form
    {
        public FaturaDetay()
        {
            InitializeComponent();
        }
        connect bgl = new connect();
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        public string id;
        void listele()
        {
            dt = new DataTable();
            sql = @"Select * from faturadetay where faturaid='" + id + "'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FaturaDetay_Load(object sender, EventArgs e)
        {
            listele();
        }
    }
}
