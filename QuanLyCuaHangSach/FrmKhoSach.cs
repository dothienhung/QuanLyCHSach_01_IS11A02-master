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
    public partial class FrmKhoSach : Form
    {
        DataTable KhoSach;
        public FrmKhoSach()
        {
            InitializeComponent();
        }
        public void LoadDataToGridview()
        {
           
            string sql = "SELECT * FROM Khosach";
            KhoSach = DAO.GetDataToTable(sql);
            dataGridView1.DataSource = KhoSach;
           
        }
        private void FrmKhoSach_Load(object sender, EventArgs e)
        {
            txtDGB.ReadOnly = true;
            LoadDataToGridview();
            DAO.FillDataToCombo("SELECT MaSach, TenSach FROM KhoSach", cboMS, "MaSach", "MaSach");
            cboMS.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaLoaiSach, TenLoaiSach FROM LoaiSach", cboMLS, "MaLoaiSach", "MaLoaiSach");
            cboMLS.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaTG, TenTG FROM TacGia", cboMTG, "MaTG", "MaTG");
            cboMTG.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaNXB, TenNXB FROM NhaXuatBan", cboMNXB, "MaNXB", "MaNXB");
            cboMNXB.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaLinhVuc, TenLinhVuc FROM LinhVuc", cboMLV, "MaLinhVuc", "MaLinhVuc");
            cboMLV.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaNgonNgu, TenNgonNgu FROM NgonNgu", cboMNN, "MaNgonNgu", "MaNgonNgu");
            cboMNN.SelectedIndex = -1;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cboMS.Text = dataGridView1.CurrentRow.Cells["MaSach"].Value.ToString();
            txtTS.Text = dataGridView1.CurrentRow.Cells["TenSach"].Value.ToString();
            txtSL.Text = dataGridView1.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDGN.Text = dataGridView1.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
            txtDGB.Text = dataGridView1.CurrentRow.Cells["DonGiaBan"].Value.ToString();
            cboMLS.Text = dataGridView1.CurrentRow.Cells["MaLoaiSach"].Value.ToString();
            cboMTG.Text = dataGridView1.CurrentRow.Cells["MaTG"].Value.ToString();
            cboMNXB.Text = dataGridView1.CurrentRow.Cells["MaNXB"].Value.ToString();
            cboMLV.Text = dataGridView1.CurrentRow.Cells["MaLinhVuc"].Value.ToString();
            cboMNN.Text = dataGridView1.CurrentRow.Cells["MaNgonNgu"].Value.ToString();
            txtAnh.Text = dataGridView1.CurrentRow.Cells["Anh"].Value.ToString();
            txtSoTrang.Text = dataGridView1.CurrentRow.Cells["SoTrang"].Value.ToString();
        }
        private void ResetValues()
        {
            cboMS.Text = "";
            txtTS.Text = "";
            txtSL.Text = "";
            txtDGN.Text = "";
            txtDGB.Text = "0";
            cboMLS.Text = "";
            cboMTG.Text = "";
            cboMNXB.Text = "";
            cboMLV.Text = "";
            txtAnh.Text = "";
            txtSoTrang.Text = "";
            cboMNN.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
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
                cboMS.Text = "MS0" + (chuoi2 + 1).ToString();
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
           
            string sql;
            if (cboMS.Text == "")
            {
                MessageBox.Show("Bạn cần nhập mã sách!");
                cboMS.Focus();
                return;
            }
            if (txtTS.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên sách!");
                txtTS.Focus();
                return;
            }
            if (txtSL.Text == "")
            {
                MessageBox.Show("Bạn cần nhập số lượng!");
                txtSL.Focus();
                return;
            }
            if (txtDGN.Text == "")
            {
                MessageBox.Show("Bạn cần nhập đơn giá nhập!");
                txtDGN.Focus();
                return;
            }
            if (txtDGB.Text == "")
            {
                MessageBox.Show("Bạn cần nhập đơn giá bán!");
                txtDGB.Focus();
                return;
            }
            if (cboMLS.Text == "")
            {
                MessageBox.Show("Bạn cần chọn mã loại sách!");
                cboMLS.Focus();
                return;
            }
            if (cboMTG.Text == "")
            {
                MessageBox.Show("Bạn cần chọn mã tác giả!");
                cboMTG.Focus();
                return;
            }
            if (cboMNXB.Text == "")
            {
                MessageBox.Show("Bạn cần chọn mã NXB!");
                cboMNXB.Focus();
                return;
            }
            if (cboMLV.Text == "")
            {
                MessageBox.Show("Bạn cần chọn mã lĩnh vực!");
                cboMLV.Focus();
                return;
            }
            if (cboMNN.Text == "")
            {
                MessageBox.Show("Bạn cần chọn mã ngôn ngữ!");
                cboMNN.Focus();
                return;
            }
            if (txtAnh.Text == "")
            {
                MessageBox.Show("Bạn cần nhập ảnh!");
                txtAnh.Focus();
                return;
            }
            if (txtSoTrang.Text == "")
            {
                MessageBox.Show("Bạn cần nhập số trang!");
                txtSoTrang.Focus();
                return;
            }
            sql = "SELECT MaSach FROM KhoSach WHERE MaSach = '" + cboMS.Text + "'";
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("Mã sách này đã tồn tại, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMS.Focus();
                cboMS.Text = "";
                return;
            }
            //Câu 3: Giá bán = 110% giá nhập
            double tn, tx;
            if (txtDGN.Text == "")
                tn = 0;
            else
                tn = Convert.ToDouble(txtDGN.Text);
            tx = tn * 110 / 100;
            txtDGB.Text = tx.ToString();
            sql = "UPDATE KhoSach SET DonGiaBan = '" + tx + "' WHERE MaSach = '" + cboMS.Text + "'";
            DAO.RunSql(sql);
            sql = "INSERT INTO KhoSach (MaSach, TenSach, SoLuong, DonGiaNhap, DonGiaBan, MaLoaiSach, MaTG, MaNXB, MaLinhVuc, MaNgonNgu, Anh, SoTrang)" + "VALUES ('" + cboMS.Text + "', N'" + txtTS.Text + "', '" + txtSL.Text + "', '" + txtDGN.Text + "', '" + txtDGB.Text + "', '" + cboMLS.Text + "', '" + cboMTG.Text + "', '" + cboMNXB.Text + "', '" + cboMLV.Text + "', '" + cboMNN.Text + "' ,'" + txtAnh.Text + "', '" + txtSoTrang.Text + "')";
            DAO.RunSql(sql);
           
            LoadDataToGridview();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            
            string sql;
            if (KhoSach.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMS.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            double tn, tx;
            tn = Convert.ToDouble(txtDGN.Text);
            tx = tn * 110 / 100;
            sql = "UPDATE KhoSach SET TenSach = N'" + txtTS.Text.Trim().ToString() + "', SoLuong = '" + txtSL.Text +
                "', DonGiaNhap = '" + txtDGN.Text + "', DonGiaBan = '" + tx + "', MaLoaiSach = '" + cboMLS.SelectedValue.ToString() + 
                "', MaTG = '" + cboMTG.SelectedValue.ToString() + "', MaNXB = '" + cboMNXB.SelectedValue.ToString() + "', MaLinhVuc = '" + cboMLV.SelectedValue.ToString() + "' , Anh = '" + txtAnh.Text + "', SoTrang = '" + txtSoTrang.Text + "' WHERE MaSach = '"+cboMS.SelectedValue.ToString()+"'";
            DAO.RunSql(sql);
          
            LoadDataToGridview();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
          
            string sql;
            if (KhoSach.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMS.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE FROM KhoSach WHERE KhoSach.MaSach = '" + cboMS.Text + "'";
                DAO.RunSql(sql);
                
                LoadDataToGridview();
                ResetValues();
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtDGB_TextChanged(object sender, EventArgs e)
        { 
        }
    }
}

