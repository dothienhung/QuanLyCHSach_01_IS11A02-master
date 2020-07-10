using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangSach
{
    public partial class FrnHoaDonBan : Form
    {
        DataTable ChiTietHDB;
        public FrnHoaDonBan()
        {
            InitializeComponent();
        }

        private void FrnHoaDonBan_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
            txtMHB.ReadOnly = true;
            txttnv.ReadOnly = true;
            txttkh.ReadOnly = true;
            txtdc.ReadOnly = true;
            txtdt.ReadOnly = true;
            txtts.ReadOnly = true;
            txtdg.ReadOnly = true;
            txtthanhtien.ReadOnly = true;
            txttongtien.ReadOnly = true;
            txtgg.Text = "0";
            txttongtien.Text = "0";
            DAO.FillDataToCombo("SELECT Makhach, Tenkhach FROM KhachHang", cbomkh,"Makhach", "Makhach");
            cbomkh.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT MaNV, TenNV FROM NhanVien",cbomnv, "MaNV", "MaNV");
            cbomnv.SelectedIndex = -1;
            DAO.FillDataToCombo("SELECT Masach, Tensach FROM KhoSach", cboms,"Masach", "Masach");
            cboms.SelectedIndex = -1;
            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMHB.Text != "")
            {
                Load_ThongtinHD();
                btnXoa.Enabled = true;
                btnIn.Enabled = true;
            }
            Load_DataGridViewChitiet();

        }

        private void Load_ThongtinHD()
        {
            string str;
            str = "SELECT NgayBan FROM HoaDonBan WHERE SoHDB = N'" + txtMHB.Text + "'";
            txtnb.Text = DAO.ConvertDateTime(DAO.GetFieldValues(str));
            str = "SELECT MaNV FROM HoaDonBan WHERE SoHDB = N'" + txtMHB.Text + "'";
            cbomnv.Text = DAO.GetFieldValues(str);
            str = "SELECT MaKhach FROM HoaDonBan WHERE SoHDB = N'" + txtMHB.Text + "'";
            cbomkh.Text = DAO.GetFieldValues(str);
            str = "SELECT TongTien FROM HoaDonBan WHERE SoHDB = N'" + txtMHB.Text + "'";
            txttongtien.Text = DAO.GetFieldValues(str);
            txtchu.Text = "Bằng chữ: " + DAO.ChuyenSoSangChu(txttongtien.Text);

        }

        private void Load_DataGridViewChitiet()
        {
            string sql;
            sql = "SELECT a.Masach, b.Tensach, a.SoLuong, b.DonGiaBan, a.KhuyenMai, a.ThanhTien FROM ChiTietHDB AS a, KhoSach AS b WHERE a.SoHDB = N'" + txtMHB.Text + "' AND a.Masach=b.Masach";
            ChiTietHDB = DAO.GetDataToTable(sql);
            gridviewHDB.DataSource = ChiTietHDB;
            gridviewHDB.Columns[0].HeaderText = "Mã sách";
            gridviewHDB.Columns[1].HeaderText = "Tên sách";
            gridviewHDB.Columns[2].HeaderText = "Số lượng";
            gridviewHDB.Columns[3].HeaderText = "Đơn giá";
            gridviewHDB.Columns[4].HeaderText = "Khuyến mại %";
            gridviewHDB.Columns[5].HeaderText = "Thành tiền";
            gridviewHDB.Columns[0].Width = 80;
            gridviewHDB.Columns[1].Width = 100;
            gridviewHDB.Columns[2].Width = 80;
            gridviewHDB.Columns[3].Width = 90;
            gridviewHDB.Columns[4].Width = 90;
            gridviewHDB.Columns[5].Width = 90;
            gridviewHDB.AllowUserToAddRows = false;
            gridviewHDB.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        private void ResetValues()
        {
            txtMHB.Text = "";
            txtnb.Text = DateTime.Now.ToShortDateString();
            cbomnv.Text = "";
            cbomkh.Text = "";
            txttongtien.Text = "0";
            txtchu.Text = "Bằng chữ: ";
            cboms.Text = "";
            txtsl.Text = "";
            txtgg.Text = "0";
            txtthanhtien.Text = "0";
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnIn.Enabled = false;
            btnThem.Enabled = false;
            ResetValues();
            txtMHB.Text = DAO.CreateKey("HDB");
            Load_DataGridViewChitiet();

        }
       
        private void ResetValuesHang()
        {
            cboms.Text = "";
            txtsl.Text = "";
            txtgg.Text = "0";
            txtthanhtien.Text = "0";
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;
            sql = "SELECT SoHDB FROM HoaDonBan WHERE SoHDB=N'" + txtMHB.Text + "'";
            if (!DAO.checkKeyExit(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                if (txtnb.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnb.Focus();
                    return;
                }
                if (cbomnv.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbomnv.Focus();
                    return;
                }
                if (cbomkh.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbomkh.Focus();
                    return;
                }
                sql = "INSERT INTO HoaDonBan(SoHDB,MaNV,NgayBan,MaKhach,TongTien) VALUES(N'" + txtMHB.Text.Trim() + "',N'" + cbomnv.SelectedValue + "', '" +
                        DAO.ConvertDateTime(txtnb.Text.Trim()) + "',N'" + cbomkh.SelectedValue + "'," + txttongtien.Text + ")";
                DAO.RunSql(sql);
            }
            // Lưu thông tin của các mặt hàng
            if (cboms.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập sách", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                cboms.Focus();
                return;
            }
            if ((txtsl.Text.Trim().Length == 0) || (txtsl.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtsl.Text = "";
                txtsl.Focus();
                return;
            }
            if (txtgg.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtgg.Focus();
                return;
            }
            sql = "SELECT MaSach FROM ChiTietHDB WHERE MaSach=N'" + cboms.SelectedValue + "' AND SoHDB = N'" + txtMHB.Text.Trim() + "'";
            if (DAO.checkKeyExit(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetValuesHang();
                cboms.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(DAO.GetFieldValues("SELECT SoLuong FROM KhoSach WHERE MaSach = N'" + cboms.SelectedValue + "'"));
            if (Convert.ToDouble(txtsl.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsl.Text = "";
                txtsl.Focus();
                return;
            }
            sql = "INSERT INTO ChiTietHDB(SoHDB,MaSach,SoLuong,KhuyenMai, ThanhTien) VALUES(N'" + txtMHB.Text.Trim() + "', N'" + cboms.SelectedValue + 
"'," + txtsl.Text + "," + txtgg.Text + "," + txtthanhtien.Text + ")";
            DAO.RunSql(sql);
            Load_DataGridViewChitiet();
            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            SLcon = sl - Convert.ToDouble(txtsl.Text);
            sql = "UPDATE KhoSach SET SoLuong =" + SLcon + " WHERE MaSach= N'" +cboms.SelectedValue + "'";
            DAO.RunSql(sql);
            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(DAO.GetFieldValues("SELECT TongTien FROM HoaDonBan WHERE SoHDB = N'" + txtMHB.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtthanhtien.Text);
            sql = "UPDATE HoaDonBan SET TongTien =" + Tongmoi + " WHERE SoHDB = N'" +txtMHB.Text + "'";
            DAO.RunSql(sql);
            txttongtien.Text = Tongmoi.ToString();
            txtchu.Text = "Bằng chữ: " + DAO.ChuyenSoSangChu(Tongmoi.ToString());
            ResetValuesHang();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnIn.Enabled = true;
            

        }

        private void cbomnv_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cbomnv.Text == "")
                txttnv.Text = "";
            // Khi kich chon Ma nhan vien thi ten nhan vien se tu dong hien ra
            str = "Select TenNV from NhanVien where MaNV =N'" +cbomnv.SelectedValue + "'";
            txttnv.Text = DAO.GetFieldValues(str);

        }

        private void cbomkh_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cbomkh.Text == "")
            {
                txttkh.Text = "";
                txtdc.Text = "";
                txtdt.Text = "";
            }
            //Khi kich chon Ma khach thi ten khach, dia chi, dien thoai se tu dong hien ra
            str = "Select TenKhach from KhachHang where MaKhach = N'" + cbomkh.SelectedValue+ "'";
            txttkh.Text = DAO.GetFieldValues(str);
            str = "Select DiaChi from KhachHang where MaKhach = N'" + cbomkh.SelectedValue+ "'";
            txtdc.Text = DAO.GetFieldValues(str);
            str = "Select DienThoai from KhachHang where MaKhach= N'" + cbomkh.SelectedValue + "'";
            txtdt.Text = DAO.GetFieldValues(str);

        }

        private void cboms_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboms.Text == "")
            {
                txtts.Text = "";
                txtdg.Text = "";
            }
            // Khi kich chon Ma hang thi ten hang va gia ban se tu dong hien ra
            str = "SELECT TenSach FROM KhoSach WHERE MaSach =N'" + cboms.SelectedValue+ "'";
            txtts.Text = DAO.GetFieldValues(str);
            str = "SELECT DonGiaBan FROM KhoSach WHERE MaSach =N'" + cboms.SelectedValue+ "'";
            txtdg.Text = DAO.GetFieldValues(str);

        }

        private void txtsl_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            if (txtsl.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtsl.Text);
            if (txtgg.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtgg.Text);
            if (txtdg.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtdg.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtthanhtien.Text = tt.ToString();

        }

        private void txtgg_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            if (txtsl.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtsl.Text);
            if (txtgg.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtgg.Text);
            if (txtdg.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtdg.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtthanhtien.Text = tt.ToString();

        }
        private void DelUpdateTongtien(string Mahoadon, double Thanhtien)
        {
            Double Tong, Tongmoi;
            string sql;
            sql = "SELECT TongTien FROM HoaDonBan WHERE SoHDB = N'" + Mahoadon + "'";
            Tong = Convert.ToDouble(DAO.GetFieldValues(sql));
            Tongmoi = Tong - Thanhtien;
            sql = "UPDATE HoaDonBan SET TongTien =" + Tongmoi + " WHERE SoHDB = N'" +Mahoadon + "'";
            DAO.RunSql(sql);
            txttongtien.Text = Tongmoi.ToString();
            txtchu.Text = DAO.ChuyenSoSangChu(Tongmoi.ToString());
        }
       

        private void gridviewHDB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string masach;
            Double Thanhtien;
            if (ChiTietHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                masach = gridviewHDB.CurrentRow.Cells["MaSach"].Value.ToString();
                DelHang(txtMHB.Text, masach);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                Thanhtien = Convert.ToDouble(gridviewHDB.CurrentRow.Cells["ThanhTien"].Value.ToString());
                DelUpdateTongtien(txtMHB.Text, Thanhtien);
                Load_DataGridViewChitiet();
            }

        }
        private void DelHang(string Mahoadon, string MaSach)
        {
            Double s, sl ;
            double SLCon;
            string sql;
            sql = "SELECT SoLuong FROM ChiTietHDB WHERE SoHDB = N'" + Mahoadon + "' AND MaSach = N'" + MaSach + "'";
            s = Convert.ToDouble(DAO.GetFieldValues(sql));
            sql = "DELETE ChiTietHDB WHERE SoHDB=N'" + Mahoadon + "' AND MaSach = N'"+ MaSach + "'";
            DAO.RunSqlDel(sql);
            // Cập nhật lại số lượng cho các mặt hàng
            sql = "SELECT Soluong FROM KhoSach WHERE MaSach = N'" + MaSach + "'";
            sl = Convert.ToDouble(DAO.GetFieldValues(sql));
            SLCon = sl + s;
            sql = "UPDATE KhoSach SET SoLuong =" + SLCon + " WHERE MaSach= N'" + MaSach + "'";
            DAO.RunSql(sql);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string[] Mahang = new string[20];
                string sql;
                int n = 0;
                int i;
                sql = "SELECT MaSach FROM ChiTietHDB WHERE SoHDB = N'" +txtMHB.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, DAO.conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Mahang[n] = reader.GetString(0).ToString();
                    n = n + 1;
                }
                reader.Close();
                //Xóa danh sách các mặt hàng của hóa đơn
                for (i = 0; i <= n - 1; i++)
                    DelHang(txtMHB.Text, Mahang[i]);
                //Xóa hóa đơn
                sql = "DELETE HoaDonBan WHERE SoHDB=N'" + txtMHB.Text + "'";
                DAO.RunSqlDel(sql);
                ResetValues();
                Load_DataGridViewChitiet();
                btnXoa.Enabled = false;
                btnIn.Enabled = false;
            }

        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Name = "Times new roman";
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "CỔ PHONG BOOKS";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Cầu Giấy - Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (04)37562222";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Name = "Times new roman";
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.SoHDB, a.NgayBan, a.TongTien, b.TenKhach, b.DiaChi, b.DienThoai, c.TenNV FROM HoaDonBan AS a, KhachHang AS b, NhanVien AS c WHERE a.SoHDB = N'" + txtMHB.Text + "' AND a.MaKhach = b.MaKhach AND a.MaNV =c.MaNV";
            tblThongtinHD = DAO.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:C9"].Font.Name = "Times new roman";
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TenSach, a.SoLuong, b.DonGiaBan, a.KhuyenMai, a.ThanhTien " +
                  "FROM ChiTietHDB AS a , KhoSach AS b WHERE a.SoHDB = N'" +
                  txtMHB.Text + "' AND a.MaSach = b.MaSach";
            tblThongtinHang = DAO.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang <= tblThongtinHang.Rows.Count - 1; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot <= tblThongtinHang.Columns.Count - 1; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + DAO.ChuyenSoSangChu (tblThongtinHD.Rows[0][2].ToString());
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;

        }

        private void cbomhd1_DropDown(object sender, EventArgs e)
        {
            DAO.FillDataToCombo("SELECT SoHDB FROM HoaDonBan", cbomhd1, "SoHDB","SoHDB");
            cbomhd1.SelectedIndex = -1;

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (cbomhd1.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbomhd1.Focus();
                return;
            }
            txtMHB.Text = cbomhd1.Text;
            Load_ThongtinHD();
            Load_DataGridViewChitiet();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnIn.Enabled = true;
            cbomhd1.SelectedIndex = -1;

        }
        private void FrnHoaDonBan_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetValues();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }


    }
}
