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
    public partial class FrmMatSach : Form
    {
        DataTable MatSach;
        public FrmMatSach()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        public void LoadDataToGridview()
        {
            DAO.OpenConnection();
            string sql = "SELECT MaLanMat, MaSach, NgayMat, SoLuongMat FROM MatSach";
            MatSach = DAO.GetDataToTable(sql);
            dataGridView1.DataSource = MatSach;
            DAO.CloseConnetion();
        }
        private void FrmMatSach_Load(object sender, EventArgs e)
        {
            LoadDataToGridview();
            DAO.FillDataToCombo("SELECT MaSach, TenSach FROM KhoSach", cmbmasach, "MaSach", "MaSach");
            cmbmasach.SelectedIndex = -1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmalanmat.Text = dataGridView1.CurrentRow.Cells["MaLanMat"].Value.ToString();
            cmbmasach.Text = dataGridView1.CurrentRow.Cells["MaSach"].Value.ToString();
            txtngaymat.Text = dataGridView1.CurrentRow.Cells["NgayMat"].Value.ToString();
            txtsoluongmat.Text = dataGridView1.CurrentRow.Cells["SoLuongMat"].Value.ToString();
        }
        private void ResetValue()
        {
            txtmalanmat.Text = "";
            cmbmasach.Text = "";
            txtsoluongmat.Text = "";
            txtngaymat.Text = DateTime.Now.ToShortDateString();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            ResetValue();
            int count = 0;
            count = dataGridView1.Rows.Count;
            string chuoi = "";
            int chuoi2 = 0;
            chuoi = Convert.ToString(dataGridView1.Rows[count - 2].Cells[0].Value);
            chuoi2 = Convert.ToInt32((chuoi.Remove(0, 2)));
            if (chuoi2 + 1 < 100)
            {
                txtmalanmat.Text = "LM0" + (chuoi2 + 1).ToString();
            }
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (txtmalanmat.Text == "")
            {
                MessageBox.Show("Bạn không được để trống mã lần mất");
                txtmalanmat.Focus();
                return;
            }

            if (cmbmasach.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn mã sách");
                return;
            }
            if (txtngaymat.Text == "")
            {
                MessageBox.Show("Bạn không được để trống ngày mất");
                txtngaymat.Focus();
                return;
            }
            if (txtsoluongmat.Text == "")
            {
                MessageBox.Show("Bạn không được để trống số lượng mất");
                txtsoluongmat.Focus();
                return;
            }

            // - Mã lần mất ko được trùng
            string sql = "select * from MatSach where MaLanMat = '" +
            txtmalanmat.Text.Trim() + "'";
            DAO.OpenConnection();
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("mã lần mất đã tồn tại");
                txtmalanmat.Focus();
                DAO.CloseConnetion();
                return;
            }
            else
            {
                sql = "INSERT INTO  MatSach (MaLanMat, MaSach, NgayMat, SoLuongMat) values ('" + txtmalanmat.Text.Trim() + "',N'" + cmbmasach.SelectedValue.ToString() + "','" + DAO.ConvertDateTime(txtngaymat.Text.Trim()) + "'," + txtsoluongmat.Text.Trim() + ")";

                DAO.RunSql(sql);
                LoadDataToGridview();
                DAO.CloseConnetion();
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string sql;
            if (MatSach.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu! ");
            }
            if (txtmalanmat.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã lần mất!");
                txtmalanmat.Focus();
            }
            if (cmbmasach.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn mã sách!");
                cmbmasach.Focus();
            }
            if (txtngaymat.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày mất!");
                txtngaymat.Focus();
            }
            if (txtsoluongmat.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng mất!");
                txtsoluongmat.Focus();
            }
            sql = "Update MatSach set MaLanMat = '" + txtmalanmat.Text.Trim() + "', MaSach = N'" + cmbmasach.Text.Trim() + "'," +
                "NgayMat ='" + DAO.ConvertDateTime(txtngaymat.Text) + "' , SoLuongMat =" + txtsoluongmat.Text + " where MaLanMat = '" + txtmalanmat.Text + "'";
            DAO.RunSqlDel(sql);
            LoadDataToGridview();
            ResetValue();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            string sql;
            if (MatSach.Rows.Count == 0)
            {
                MessageBox.Show("khong co du lieu");
            }
            if (txtmalanmat.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn mã lần mất");
            }
            else
            {
                if (MessageBox.Show("Bạn muốn xóa không", "Thông báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "delete from MatSach where MaLanMat ='" + txtmalanmat.Text + "'";
                    DAO.RunSql(sql);
                    LoadDataToGridview();
                    ResetValue();
                }
            }
            DAO.CloseConnetion();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
