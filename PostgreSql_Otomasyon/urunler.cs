using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Internal;
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
    public partial class urunler : Form
    {
        public urunler()
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
            sql = @"Select * from urunler";
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
            txtUrunad.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            txtSatisFiyat.Text = "";
            txtAlisFiyat.Text = "";
            rchDetay.Text = "";
            mskYıl.Text = "";
        }
        private void urunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                bgl.baglanti();
                sql = @"insert into urunler(ad,marka,model,yıl,adet,alisfiyat,satisfiyat,detay) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";
                cmd = new NpgsqlCommand(sql, bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtUrunad.Text);
                cmd.Parameters.AddWithValue("@p2", txtMarka.Text);
                cmd.Parameters.AddWithValue("@p3", txtModel.Text);
                cmd.Parameters.AddWithValue("@p4", mskYıl.Text);
                cmd.Parameters.AddWithValue("@p5", int.Parse(txtAdet.Text.ToString()));
                cmd.Parameters.AddWithValue("@p6", int.Parse(txtAlisFiyat.Text.ToString()));
                cmd.Parameters.AddWithValue("@p7", int.Parse(txtSatisFiyat.Text.ToString()));
                cmd.Parameters.AddWithValue("@p8", rchDetay.Text);
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                bgl.baglanti();

                sql = @"insert into stoklar(stoktur,stokadet,urunid) values (@p1,@p2,@p3)";
                cmd = new NpgsqlCommand(sql, bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", txtUrunad.Text);
                cmd.Parameters.AddWithValue("@p2", int.Parse(txtAdet.Text.ToString()));
                cmd.Parameters.AddWithValue("@p3", int.Parse(txtId.Text));
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"update urunler set ad=@p1,marka=@p2,model=@p3,yıl=@p4,adet=@p5,alisfiyat=@p6,satisfiyat=@p7,detay=@p8 where id=@p9";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", txtUrunad.Text);
            cmd.Parameters.AddWithValue("@p2", txtMarka.Text);
            cmd.Parameters.AddWithValue("@p3", txtModel.Text);
            cmd.Parameters.AddWithValue("@p4", mskYıl.Text);
            cmd.Parameters.AddWithValue("@p5", int.Parse((txtAdet.Value).ToString()));
            cmd.Parameters.AddWithValue("@p6", int.Parse(txtAlisFiyat.Text));
            cmd.Parameters.AddWithValue("@p7", int.Parse(txtSatisFiyat.Text));
            cmd.Parameters.AddWithValue("@p8", rchDetay.Text);
            cmd.Parameters.AddWithValue("@p9",int.Parse(txtId.Text.ToString()) );
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bgl.baglanti();
            sql = @"Delete from stoklar where urunid=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            bgl.baglanti();
            sql = @"Delete from urunler where id=@p1";
            cmd = new NpgsqlCommand(sql, bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", int.Parse(txtId.Text.ToString()));
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Kaydı Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

            
        

        }
        private void gridView1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr["id"].ToString();
            txtUrunad.Text = dr["ad"].ToString();
            txtMarka.Text = dr["marka"].ToString();
            txtModel.Text = dr["model"].ToString();
            mskYıl.Text = dr["yıl"].ToString();
            txtAdet.Value = int.Parse(dr["adet"].ToString());
            txtAlisFiyat.Text = dr["alisfiyat"].ToString();
            txtSatisFiyat.Text = dr["satisfiyat"].ToString();
            rchDetay.Text = dr["detay"].ToString();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            sql = @"Select * from urunler where ad like '%" + txtBul.Text + "%'";
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.baglanti().Close();
        }
    }
}
