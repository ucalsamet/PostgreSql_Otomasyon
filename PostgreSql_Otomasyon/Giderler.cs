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
    public partial class Giderler : Form
    {
        public Giderler()
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
            sql = @"Select * from giderler";
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
            cmbAy.Text = "";
            cmbYıl.Text = "";
            txtDogalgaz.Text = "";
            txtEkstra.Text = "";
            txtElektrik.Text = "";
            txtMaaslar.Text = "";
            txtSu.Text = "";
            txtİnternet.Text = "";
            rchNotlar.Text = "";
        }
        private void Giderler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"insert into giderler(ay,yıl,elektrik,su,dogalgaz,internet,maaslar,ekstra,notlar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", cmbAy.Text);
            cmd.Parameters.AddWithValue("@p2", cmbYıl.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(txtİnternet.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            cmd.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            cmd.Parameters.AddWithValue("@p9", rchNotlar.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["id"].ToString();
            cmbAy.Text = dr["ay"].ToString();
            cmbYıl.Text = dr["yıl"].ToString();
            txtElektrik.Text = dr["elektrik"].ToString();
            txtSu.Text = dr["su"].ToString();
            txtDogalgaz.Text = dr["dogalgaz"].ToString();
            txtİnternet.Text = dr["internet"].ToString();
            txtMaaslar.Text = dr["maaslar"].ToString();
            txtEkstra.Text = dr["ekstra"].ToString();
            rchNotlar.Text = dr["notlar"].ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"update giderler set ay=@p1,yıl=@p2,elektrik=@p3,su=@p4,dogalgaz=@p5,internet=@p6,maaslar=@p7,ekstra=@p8 ,notlar=@p9 where id=@p10";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", cmbAy.Text);
            cmd.Parameters.AddWithValue("@p2", cmbYıl.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(txtİnternet.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            cmd.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            cmd.Parameters.AddWithValue("@p9", rchNotlar.Text);
            cmd.Parameters.AddWithValue("@p10", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"Delete from giderler where id=@p1";
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
