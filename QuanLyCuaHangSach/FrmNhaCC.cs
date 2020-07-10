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
    public partial class FrmNhaCC : Form
    {
        DataTable NhaCungCap;
        public FrmNhaCC()
        {
            InitializeComponent();
        }

        private void FrmNhaCC_Load(object sender, EventArgs e)
        {
            txtMNCC.Enabled = false;
            loadDataToGridview();
        }
        private void loadDataToGridview()
        {
            string sql = "Select * From NhaCungCap";
            NhaCungCap = DAO.GetDataToTable(sql);
            dataGridView1.DataSource = NhaCungCap;
        }
        private void Resetvalue()
        {
            txtMNCC.Text = "";
            txtTNCC.Text = "";
            txtDiaChi.Text = "";
            txtsdt.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMNCC.Text = dataGridView1.CurrentRow.Cells["MaNhaCC"].Value.ToString();
            txtTNCC.Text = dataGridView1.CurrentRow.Cells["TenNhaCC"].Value.ToString();
            txtDiaChi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtsdt.Text = dataGridView1.CurrentRow.Cells["DienThoai"].Value.ToString();
            txtMNCC.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMNCC.Enabled = true;
            Resetvalue();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string sql;
            if (NhaCungCap.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu! ");
            }
            if (txtMNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã NCC");
                txtMNCC.Focus();
            }
            if (txtTNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên NCC");
                txtTNCC.Focus();
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ");
                txtDiaChi.Focus();
            }
            if (txtsdt.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điên thoại");
                txtsdt.Focus();
            }
            sql = "UPDATE NhaCungCap SET  TenNhaCC=N'" + txtTNCC.Text.Trim().ToString() +
                    "',DiaChi=N'" + txtDiaChi.Text.Trim().ToString() +
                    "',DienThoai='" + txtsdt.Text.ToString() +
                    "' WHERE MaNhaCC=N'" + txtMNCC.Text + "'";
            DAO.RunSqlDel(sql);
            loadDataToGridview();
            Resetvalue();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (NhaCungCap.Rows.Count == 0)
            {
                MessageBox.Show("khong co du lieu");
            }
            if (txtMNCC.Text == "")
            {
                MessageBox.Show("ban chua chon Nhà cung cấp");
            }
            else
            {
                if (MessageBox.Show("Bạn muốn xóa không", "Thông báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "delete from NhaCungCap where MaNhaCC ='" + txtMNCC.Text + "'";
                    DAO.RunSql(sql);
                    loadDataToGridview();
                    Resetvalue();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (NhaCungCap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }
            if (txtMNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã NSX");
                txtMNCC.Focus();
            }
            if (txtTNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên NSX");
                txtTNCC.Focus();
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ");
                txtDiaChi.Focus();
            }
            if (txtsdt.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại");
                txtsdt.Focus();
            }
            sql = "Select MaNhaCC from NhaCungCap where MaNhaCC ='" + txtMNCC.Text.Trim() + "'";
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("Mã NSX này đã có bạn phải nhập mã khác");
                txtMNCC.Focus();
                return;
            }
            sql = "insert into NhaCungCap values( '" + txtMNCC.Text + "' ,'" + txtTNCC.Text + "','" + txtDiaChi.Text + "' ,'" + txtsdt.Text + "')";
            DAO.RunSqlDel(sql);
            loadDataToGridview();
            Resetvalue();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
