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
    public partial class FrmLinhVuc : Form
    {
        DataTable LinhVuc;
        public FrmLinhVuc()
        {
            InitializeComponent();
        }
        private void loadDataToGridview()
        {
            string sql = "Select * From LinhVuc";
            LinhVuc = DAO.GetDataToTable(sql);
            dataGridView_LinhVuc.DataSource = LinhVuc;
        }
        private void Resetvalue()
        {
            txtMaLinhVuc.Text = "";
            txtTenLinhVuc.Text = "";
        }
        private void FrmLinhVuc_Load(object sender, EventArgs e)
        {
            txtMaLinhVuc.Enabled = false;
            loadDataToGridview();
        }

        private void dataGridView_LinhVuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLinhVuc.Text = dataGridView_LinhVuc.CurrentRow.Cells["MaLinhVuc"].Value.ToString();
            txtTenLinhVuc.Text = dataGridView_LinhVuc.CurrentRow.Cells["TenLinhVuc"].Value.ToString();
            txtMaLinhVuc.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaLinhVuc.Enabled= true;
            Resetvalue();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (LinhVuc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu! ");
            }
            if (txtMaLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã lĩnh vực");
                txtMaLinhVuc.Focus();
            }
            if (txtTenLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên Lĩnh vực");
                txtTenLinhVuc.Focus();
            }
            sql = "update LinhVuc set TenLinhVuc='" + txtTenLinhVuc.Text + "' where MaLinhVuc ='" + txtMaLinhVuc.Text + "'";
            DAO.RunSqlDel(sql);
            loadDataToGridview();
            Resetvalue();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (LinhVuc.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
            }
            if (txtMaLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào");
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "delete from LinhVuc where MaLinhVuc= '" + txtMaLinhVuc.Text + "'";
                    DAO.RunSqlDel(sql);
                    loadDataToGridview();
                    Resetvalue();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (LinhVuc.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }
            if (txtMaLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã Lĩnh vực");
                txtMaLinhVuc.Focus();
            }
            if (txtTenLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên Lĩnh Vực");
                txtMaLinhVuc.Focus();
            }
            sql = "Select MaLinhVuc from LinhVuc where MaLinhVuc ='" + txtMaLinhVuc.Text.Trim() + "'";
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("Mã Lĩnh Vực này đã có bạn phải nhập mã khác");
                txtMaLinhVuc.Focus();
                return;
            }
            sql = "insert into LinhVuc values( '" + txtMaLinhVuc.Text + "' ,'" + txtTenLinhVuc.Text + "')";
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
