using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();

        }

        private void mnuthoat_Click(object sender, EventArgs e)
        {
            DAO.CloseConnetion();
            Application.Exit();
        }

        private void mnunhanvien_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmNhanVien f = new QuanLyCuaHangSach.FrmNhanVien();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnukhosach_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmKhoSach f = new QuanLyCuaHangSach.FrmKhoSach();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnukhachhang_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmKhachHang f = new QuanLyCuaHangSach.FrmKhachHang();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnuNCC_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmNhaCC f = new QuanLyCuaHangSach.FrmNhaCC();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnusm_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmMatSach f = new QuanLyCuaHangSach.FrmMatSach();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnucongviec_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmCongViec f = new QuanLyCuaHangSach.FrmCongViec();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnutg_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmTacGia f = new QuanLyCuaHangSach.FrmTacGia();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnunxb_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmNXB f = new QuanLyCuaHangSach.FrmNXB();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnulinhvuc_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmLinhVuc f = new QuanLyCuaHangSach.FrmLinhVuc();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnuhdn_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmHoaDonNhap f = new QuanLyCuaHangSach.FrmHoaDonNhap();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnuhdban_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrnHoaDonBan f = new QuanLyCuaHangSach.FrnHoaDonBan();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void ngônNgữToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmNN f = new QuanLyCuaHangSach.FrmNN();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void mnutimkiem_Click(object sender, EventArgs e)
        {

        }

        private void tìmKiếmSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmTimKiemSach f = new QuanLyCuaHangSach.FrmTimKiemSach();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }

        private void tìmKiếmHĐNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmTimKiemHoaDon f = new QuanLyCuaHangSach.FrmTimKiemHoaDon();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }

        private void bC8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmBC8 f = new QuanLyCuaHangSach.FrmBC8();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }
        private void bC9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmBC9 f = new QuanLyCuaHangSach.FrmBC9();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }

        private void bC7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmBC7 f = new QuanLyCuaHangSach.FrmBC7();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }

        private void bC6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyCuaHangSach.FrmBC6 f = new QuanLyCuaHangSach.FrmBC6();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }
    }
}
