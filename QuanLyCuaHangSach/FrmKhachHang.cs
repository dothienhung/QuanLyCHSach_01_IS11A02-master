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
    public partial class FrmKhachHang : Form
    {
        DataTable KhachHang;
        public FrmKhachHang()
        {
            InitializeComponent();
        }
        public void LoadDataToGridview()
        {
           
            string sql = "SELECT * FROM KhachHang";
            KhachHang = DAO.GetDataToTable(sql);
            dataGridView1.DataSource = KhachHang;
           
        }
        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
           
            LoadDataToGridview();
            
        }
        private void ResetValues()
        {
            txtMKH.Text = "";
            txtTKH.Text = "";
            txtDC.Text = "";
            txtDT.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
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
                txtMKH.Text = "MK0" + (chuoi2 + 1).ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMKH.Text = dataGridView1.CurrentRow.Cells["MaKhach"].Value.ToString();
            txtTKH.Text = dataGridView1.CurrentRow.Cells["TenKhach"].Value.ToString();
            txtDC.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtDT.Text = dataGridView1.CurrentRow.Cells["DienThoai"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMKH.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã khách!");
                txtMKH.Focus();
                return;
            }
            if (txtTKH.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên khách!");
                txtTKH.Focus();
                return;
            }
            if (txtDC.Text == "")
            {
                MessageBox.Show("Bạn cần nhập địa chỉ!");
                txtDC.Focus();
                return;
            }
            if (txtDT.Text == "")
            {
                MessageBox.Show("Bạn cần nhập số điện thoại!");
                txtDT.Focus();
                return;
            }
            string sql = "SELECT MaKhach FROM KhachHang WHERE MaKhach = '" + txtMKH.Text + "'";
          
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKH.Focus();
                txtMKH.Text = "";
                return;
            }
            sql = "INSERT INTO KhachHang (MaKhach, TenKhach, DiaChi, DienThoai) VALUES ('" + txtMKH.Text + "', N'" + txtTKH.Text + "', N'" + txtDC.Text + "', '" + txtDT.Text + "')";
            DAO.RunSql(sql);
           
            LoadDataToGridview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string sql;
            if (KhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE KhachHang SET TenKhach = N'" + txtTKH.Text.Trim() + "', DiaChi = N'" + txtDC.Text + "', DienThoai = N'" + txtDT.Text + "' WHERE MaKhach = '" + txtMKH.Text + "'";
            DAO.RunSql(sql);
            
            LoadDataToGridview();
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            string sql;
            if (KhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE FROM KhachHang WHERE MaKhach = '" + txtMKH.Text + "'";
                DAO.RunSql(sql);
               
                LoadDataToGridview();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtTKH.Text == "")
            {
                MessageBox.Show("Bạn cần nhập điều kiện tìm kiếm", "Yêu cầu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM KhachHang WHERE 1=1";
            if (txtTKH.Text != "")
            {
                sql = sql + "AND TenKhach LIKE N'%" + txtTKH.Text + "%'";
            }
            KhachHang = DAO.GetDataToTable(sql);
            if (KhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Có " + KhachHang.Rows.Count + " bản ghi thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataGridView1.DataSource = KhachHang;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
