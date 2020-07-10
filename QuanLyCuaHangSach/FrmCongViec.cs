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
    public partial class FrmCongViec : Form
    {
        DataTable CongViec;
        public FrmCongViec()
        {
            InitializeComponent();
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
                cbomcv.Text = "MCV0" + (chuoi2 + 1).ToString();
            }
        }
        public void LoadDataToGridview()
        {
           
            string sql = "SELECT * FROM CongViec";
            CongViec = DAO.GetDataToTable(sql);
            dataGridView1.DataSource = CongViec;
          
        }
        public void fillDataToCombo()
        {
            string sql = "Select * from CongViec";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, DAO.conn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            cbomcv.DataSource = table;
            cbomcv.ValueMember = "MaCongViec";
            cbomcv.DisplayMember = "MaCongViec";
        }
        private void FrmCongViec_Load(object sender, EventArgs e)
        {
           
            LoadDataToGridview();
            fillDataToCombo();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbomcv.Text = dataGridView1.CurrentRow.Cells["MaCongViec"].Value.ToString();
            txttcv.Text = dataGridView1.CurrentRow.Cells["TenCongViec"].Value.ToString();
        }
        private void ResetValues()
        {
            cbomcv.Text = "";
            txttcv.Text = "";
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (cbomcv.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã công việc!");
                cbomcv.Focus();
                return;
            }
            if (txttcv.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên công việc!");
                txttcv.Focus();
                return;
            }
            string sql = "SELECT MaCongViec FROM CongViec WHERE MaCongViec = '" + cbomcv.Text + "'";
           
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("Mã công việc này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbomcv.Focus();
                cbomcv.Text = "";
                return;
            }
            sql = "INSERT INTO CongViec (MaCongViec, TenCongViec) VALUES ('" + cbomcv.Text + "', N'" + txttcv.Text + "')";
            DAO.RunSql(sql);
           
            LoadDataToGridview();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            
            string sql;
            if (CongViec.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbomcv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE CongViec SET TenCongViec = N'" + txttcv.Text.Trim() + "' WHERE MaCongViec = '" + cbomcv.Text + "'";
            DAO.RunSql(sql);
           
            LoadDataToGridview();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            
            string sql;
            if (CongViec.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbomcv.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE FROM CongViec WHERE MaCongViec = '" + cbomcv.Text + "'";
                DAO.RunSql(sql);
               
                LoadDataToGridview();
            }
        }

        private void btntk_Click(object sender, EventArgs e)
        {
            string sql;
            if (txttcv.Text == "")
            {
                MessageBox.Show("Bạn cần nhập điều kiện tìm kiếm", "Yêu cầu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM CongViec WHERE 1=1";
            if (txttcv.Text != "")
            {
                sql = sql + "AND TenCongViec LIKE N'%" + txttcv.Text + "%'";
            }
            CongViec = DAO.GetDataToTable(sql);
            if (CongViec.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Có " + CongViec.Rows.Count + " bản ghi thỏa mãn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataGridView1.DataSource = CongViec;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
