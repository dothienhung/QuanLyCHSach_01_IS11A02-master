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
    public partial class FrmTimKiemHoaDon : Form
    {
        DataTable TimHDN;
        public FrmTimKiemHoaDon()
        {
            InitializeComponent();
        }

        private void FrmTimKiemHoaDon_Load(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            dataGridView1.DataSource = null;
        }
        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtNgayNhap.Text = "";
            txtMaHang.Focus();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHang.Text == "") && (txtNgayNhap.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "select a.SoHDN,a.NgayNhap,a.MaNV,a.MaNhaCC,a.TongTien from HoaDonNhap a join ChiTietHDN b on a.SoHDN=b.SoHDN where 1=1";
            if (txtMaHang.Text != "")
            {
                sql = sql + " AND b.MaSach Like '%" + txtMaHang.Text + "%'";
            }
            if (txtNgayNhap.Text != "")
            {
                sql = sql + " AND a.NgayNhap Like '%" + txtNgayNhap.Text + "%'";
            }
            TimHDN = DAO.GetDataToTable(sql);
            if (TimHDN.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + TimHDN.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = TimHDN;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridView1.DataSource = null;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
