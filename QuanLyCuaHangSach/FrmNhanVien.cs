using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyCuaHangSach
{
    public partial class FrmNhanVien : Form
    {
        DataTable NhanVien;
        public FrmNhanVien()
        {
            InitializeComponent();
        }
        public void LoadDataToGridview()
        {
            
            string sql = "SELECT * FROM NhanVien";
            NhanVien = DAO.GetDataToTable(sql);
            dataGridView1.DataSource = NhanVien;
           
        }
        public void fillDataToCombo()
        {
            string sql = "SELECT * FROM NhanVien";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, DAO.conn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            cbomnv.DataSource = table;
            cbomnv.ValueMember = "MaNV";
            cbomnv.DisplayMember = "MaNVn";
        }
        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            
            LoadDataToGridview();
            fillDataToCombo();
            DAO.FillDataToCombo("SELECT MaCongViec FROM CongViec", cbomcv, "MaCongViec", "MaCongViec");
            cbomcv.SelectedIndex = -1;
           
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbomnv.Text = dataGridView1.CurrentRow.Cells["MaNV"].Value.ToString();
            txttnv.Text = dataGridView1.CurrentRow.Cells["TenNV"].Value.ToString();
            txtsdt.Text = dataGridView1.CurrentRow.Cells["DienThoai"].Value.ToString();
            txtdc.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
            cbomcv.Text = dataGridView1.CurrentRow.Cells["MaCongViec"].Value.ToString();
        }
        private void ResetValues()
        {
            cbomnv.Text = "";
            txttnv.Text = "";
            txtsdt.Text = "";
            txtdc.Text = "";
            cbomcv.Text = "";
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            {
                ResetValues();
                int count = 0;
                count = dataGridView1.Rows.Count;
                string chuoi = "";
                int chuoi2 = 0;
                chuoi = Convert.ToString(dataGridView1.Rows[count - 2].Cells[0].Value);
                chuoi2 = Convert.ToInt32((chuoi.Remove(0, 2)));
                if (chuoi2 + 1 < 100)
                {
                    cbomnv.Text = "NV0" + (chuoi2 + 1).ToString();
                }
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (cbomnv.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã nhân viên!");
                cbomnv.Focus();
                return;
            }
            if (txttnv.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên nhân viên!");
                txttnv.Focus();
                return;
            }
            if (txtsdt.Text == "")
            {
                MessageBox.Show("Bạn cần nhập số điện thoai!");
                txtsdt.Focus();
                return;
            }
            if (txtdc.Text == "")
            {
                MessageBox.Show("Bạn cần nhập địa chỉ!");
                txtdc.Focus();
                return;
            }
            if (cbomcv.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã công việc!");
                cbomcv.Focus();
                return;
            }
            string sql = "SELECT MaNV FROM NhanVien WHERE MaNV = '" + cbomnv.Text + "'";
           
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbomnv.Focus();
                cbomnv.Text = "";
                return;
            }
            sql = "INSERT INTO NhanVien (MaNV, TenNV, DienThoai, DiaChi, MaCongViec) VALUES ('" + cbomnv.Text + "', N'" + txttnv.Text + "', '"+txtsdt.Text+"', '"+txtdc.Text+"', '"+cbomcv.Text+"')";
            DAO.RunSql(sql);
            
            LoadDataToGridview();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
           
            string sql;
            if (NhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbomnv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE NhanVien SET TenNV = N'" + txttnv.Text.Trim() + "' WHERE MaNV = '" + cbomnv.Text + "'";
            DAO.RunSql(sql);
            
            LoadDataToGridview();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
           
            string sql;
            if (NhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbomnv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE FROM NhanVien WHERE MaNV = '" + cbomnv.Text + "'";
                DAO.RunSql(sql);
               
                LoadDataToGridview();
            }
        }

        private void btntk_Click(object sender, EventArgs e)
        {
            string sql;
            if (txttnv.Text == "")
            {
                MessageBox.Show("Bạn cần nhập điều kiện tìm kiếm", "Yêu cầu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM NhanVien WHERE 1=1";
            if (txttnv.Text != "")
            {
                sql = sql + "AND TenNV LIKE N'%" + txttnv.Text + "%'";
            }
            NhanVien = DAO.GetDataToTable(sql);
            if (NhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Có " + NhanVien.Rows.Count + " bản ghi thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataGridView1.DataSource = NhanVien;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
