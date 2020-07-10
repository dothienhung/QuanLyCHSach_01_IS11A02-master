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
    public partial class FrmTacGia : Form
    {
        DataTable TacGia;
        public FrmTacGia()
        {
            InitializeComponent();
        }

        private void FrmTacGia_Load(object sender, EventArgs e)
        {
            
            LoadDataToGridview();
            fillDataToCombo();
          
        }
        public void LoadDataToGridview()
        {
           
            string sql = "SELECT * FROM TacGia";
            TacGia = DAO.GetDataToTable(sql);
            dataGridView1.DataSource = TacGia;
            DAO.FillDataToCombo("SELECT GioiTinh FROM TacGia", cboGT, "GioiTinh", "GioiTinh");
            cboGT.SelectedIndex = -1;
           
        }
        public void fillDataToCombo()
        {
            string sql = "SELECT * FROM TacGia";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, DAO.conn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            cboMTG.DataSource = table;
            cboMTG.ValueMember = "MaTG";
            cboMTG.DisplayMember = "MaTG";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cboMTG.Text = dataGridView1.CurrentRow.Cells["MaTG"].Value.ToString();
            txtTTG.Text = dataGridView1.CurrentRow.Cells["TenTG"].Value.ToString();
            txtNS.Text = dataGridView1.CurrentRow.Cells["NgaySinh"].Value.ToString();
            cboGT.Text = dataGridView1.CurrentRow.Cells["GioiTinh"].Value.ToString();
            txtDC.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
        }
        private void ResetValues()
        {
            cboMTG.Text = "";
            txtTTG.Text = "";
            txtNS.Text = "";
            cboGT.Text = "";
            txtDC.Text = "";
        }

        private void btbThem_Click(object sender, EventArgs e)
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
                cboMTG.Text = "TG0" + (chuoi2 + 1).ToString();
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboMTG.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã tác giả!");
                cboMTG.Focus();
                return;
            }
            if (txtTTG.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên tác giả!");
                txtTTG.Focus();
                return;
            }
            if (txtNS.Text == "")
            {
                MessageBox.Show("Bạn cần nhập ngày sinh!");
                txtNS.Focus();
                return;
            }
            if (cboGT.Text == "")
            {
                MessageBox.Show("Bạn cần nhập giới tính!");
                txtTTG.Focus();
                return;
            }
            if (txtDC.Text == "")
            {
                MessageBox.Show("Bạn cần nhập địa chỉ!");
                txtTTG.Focus();
                return;
            }
            string sql = "SELECT MaTG FROM TacGia WHERE MaTG = '" + cboMTG.Text + "'";
           
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("Mã tác giả này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMTG.Focus();
                cboMTG.Text = "";
                return;
            }
            sql = "INSERT INTO TacGia (MaTG, TenTG, NgaySinh, GioiTinh, DiaChi) VALUES ('" + cboMTG.Text + "', N'" + txtTTG.Text + "', '" + DAO.ConvertDateTime(txtNS.Text) + "', N'" + cboGT.Text + "', '" + txtDC.Text + "')";
            DAO.RunSql(sql);
            
            LoadDataToGridview();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
           
            string sql;
            if (TacGia.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMTG.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE TacGia SET TenTG = N'" + txtTTG.Text.Trim() + "', DiaChi = N'"+txtDC.Text+"' WHERE MaTG = '" + cboMTG.Text + "'";
            DAO.RunSql(sql);
           
            LoadDataToGridview();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
           
            string sql;
            if (TacGia.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMTG.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE FROM TacGia WHERE MaTG = '" + cboMTG.Text + "'";
                DAO.RunSql(sql);
               
                LoadDataToGridview();
            }
        }
        private void btnTK_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtTTG.Text == "")
            {
                MessageBox.Show("Bạn cần nhập điều kiện tìm kiếm", "Yêu cầu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM TacGia WHERE 1=1";
            if (txtTTG.Text != "")
            {
                sql = sql + "AND TenTG LIKE N'%" + txtTTG.Text + "%'";
            }
            TacGia = DAO.GetDataToTable(sql);
            if (TacGia.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Có " + TacGia.Rows.Count + " bản ghi thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataGridView1.DataSource = TacGia;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
