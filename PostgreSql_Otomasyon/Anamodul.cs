using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace PostgreSql_Otomasyon
{
    public partial class Anamodul : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {
        public Anamodul()
        {
            InitializeComponent();
        }
        urunler fr1;
        private void ÜRÜNLER_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr1==null || fr1.IsDisposed)
            {
                fr1 = new urunler();
                fr1.MdiParent = this;
                fr1.Show();
            }
           
        }
        Musteriler fr2;
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (fr2 == null || fr2.IsDisposed)
            {
                fr2 = new Musteriler();
                fr2.MdiParent = this;
                fr2.Show();
            }
        }
        Personeller fr3;
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr3 == null || fr3.IsDisposed)
            {
                fr3 = new Personeller();
                fr3.MdiParent = this;
                fr3.Show();
            }

        }
        Giderler fr4;
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr4 == null || fr4.IsDisposed)
            {
                fr4 = new Giderler();
                fr4.MdiParent = this;
                fr4.Show();
            }
        }
        Firmalar fr5;
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr5 == null || fr5.IsDisposed)
            {
                fr5 = new Firmalar();
                fr5.MdiParent = this;
                fr5.Show();
            }
        }
        Notlar fr6;
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr6 == null || fr6.IsDisposed)
            {
                fr6 = new Notlar();
                fr6.MdiParent = this;
                fr6.Show();
            }
        }
        Stoklar fr7;
        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (fr7 == null || fr7.IsDisposed)
            {
                fr7 = new Stoklar();
                fr7.MdiParent = this;
                fr7.Show();
            }
        }
        Bankalar fr8;
        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr8 == null || fr8.IsDisposed)
            {
                fr8 = new Bankalar();
                fr8.MdiParent = this;
                fr8.Show();
            }
        }
        Rehber fr9;
        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr9 == null || fr9.IsDisposed)
            {
                fr9 = new Rehber();
                fr9.MdiParent = this;
                fr9.Show();
            }
        }
        Faturalar fr10;
        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr10 == null || fr10.IsDisposed)
            {
                fr10 = new Faturalar();
                fr10.MdiParent = this;
                fr10.Show();
            }
        }
        Hareketler fr11;
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr11 == null || fr11.IsDisposed)
            {
                fr11 = new Hareketler();
                fr11.MdiParent = this;
                fr11.Show();
            }
        }
        Kasa fr12;
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr12 == null || fr12.IsDisposed)
            {
                fr12 = new Kasa();
                fr12.MdiParent = this;
                fr12.Show();
            }
        }
        [DefaultValue(true)]
        [DXCategory("Appearance")]
        public virtual bool UseDefaultLookAndFeel { get; set; }
        private void Anamodul_Load(object sender, EventArgs e)
        {

        }
    }
}