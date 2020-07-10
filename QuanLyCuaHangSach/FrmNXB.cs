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
    public partial class FrmNXB : Form
    {
        DataTable NXB;
        public FrmNXB()
        {
            InitializeComponent();
        }
        private void FrmNXB_Load(object sender, EventArgs e)
        {        
            LoadDataToGrivew();         
        }
        private void LoadDataToGrivew()
        {
           
            string sql = "SELECT * FROM NhaXuatBan";
            NXB = DAO.GetDataToTable(sql);
            dataGridView1.DataSource = NXB;
          
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtma.Text = dataGridView1.CurrentRow.Cells["MaNSX"].Value.ToString();
            txtNXB.Text = dataGridView1.CurrentRow.Cells["TenNXB"].Value.ToString();
            txtdiachi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtsdt.Text = dataGridView1.CurrentRow.Cells["DienThoai"].Value.ToString();
        }
        private void btnthem_Click_1(object sender, EventArgs e)
        {
            txtma.SelectedIndex = -1;
            txtNXB.Text = "";
            txtdiachi.Text = "";
            txtsdt.Text = "";
        }
        private void btnluu_Click_1(object sender, EventArgs e)
        {
            if (txtma.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã NXB!");
                return;
            }
            if (txtNXB.Text == "")
            {
                MessageBox.Show("Bạn không được để trống tên nhà xuất bản!");
                txtNXB.Focus();
                return;
            }
            if (txtdiachi.Text == "")
            {
                MessageBox.Show("Bạn không được để trống địa chỉ!");
                txtdiachi.Focus();
                return;
            }
            if (txtsdt.Text == "")
            {
                MessageBox.Show("Bạn không được để trống số điện thoại!");
                txtsdt.Focus();
                return;
            }
            string sql = "SELECT MaNXB, TenNXB, DiaChi, DienThoai FROM NhaXuatBan WHERE MaNXB = '" + txtma.Text + "'";
          
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("Mã NXB này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtma.Focus();
                txtma.Text = "";
                return;
            }
            sql = "INSERT INTO NhaXuatBan (MaNXB, TenNXB, DiaChi, DienThoai) VALUES ('" + txtma.Text + "', N'" + txtNXB.Text + "', N'" + txtdiachi.Text + "', '" + txtsdt.Text + "')";
            DAO.RunSql(sql);
          
            LoadDataToGrivew();
        }
        private void btnsua_Click_1(object sender, EventArgs e)
        {
          
            string sql;
            if (NXB.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtma.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE NhaXuatBan SET TenNXB = N'" + txtNXB.Text.Trim() + "', DiaChi = N'" + txtdiachi.Text + "', DienThoai = '" + txtsdt.Text + "' WHERE MaNXB = '" + txtma.Text + "'";
            DAO.RunSql(sql);
            
            LoadDataToGrivew();
        }
        private void btnxoa_Click_1(object sender, EventArgs e)
        {
            
            string sql;
            if (NXB.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtma.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE FROM NhaXuatBan WHERE MaNXB = '" + txtma.Text + "'";
                DAO.RunSql(sql);
                
                LoadDataToGrivew();
            }
        }
        private void btntimkiem_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (txtNXB.Text == "")
            {
                MessageBox.Show("Bạn cần nhập điều kiện tìm kiếm", "Yêu cầu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM NhaXuatBan WHERE 1=1";
            if (txtNXB.Text != "")
            {
                sql = sql + "AND TenNXB LIKE N'%" + txtNXB.Text + "%'";
            }
            NXB = DAO.GetDataToTable(sql);
            if (NXB.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Có " + NXB.Rows.Count + " bản ghi thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataGridView1.DataSource = NXB;
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmNXB_Load_1(object sender, EventArgs e)
        {
            LoadDataToGrivew();
        }
    }
}
