using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangSach
{
    public partial class FrmBC9 : Form
    {
        DataTable BC9;
        public FrmBC9()
        {
            InitializeComponent();
        }
        private void FrmBC9_Load(object sender, EventArgs e)
        {
        }
        private void ResetValues()
        {
            cmbthang.Text = "";
        }
        private void LoadDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void btnhienthi_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            //xuất báo cáo
            if (cmbthang.Text == "")
            {
                MessageBox.Show("Hãy nhập đủ điều kiện!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "SELECT * FROM KhachHang WHERE NOT EXISTS (SELECT * FROM HoaDonBan WHERE KhachHang.MaKhach = HoaDonBan.MaKhach AND MONTH (NgayBan) = " + Convert.ToInt32(cmbthang.Text) +")";
            BC9 = DAO.GetDataToTable(sql);
            DAO.RunSql(sql);
            dataGridView1.DataSource = BC9;
            dataGridView1.Columns[0].HeaderText = "Mã Khách";
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].HeaderText = "Tên Khách";
            dataGridView1.Columns[1].Width = 170;
            dataGridView1.Columns[2].HeaderText = "Địa Chỉ";
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].HeaderText = "Điện Thoại";
            dataGridView1.Columns[3].Width = 110;
            LoadDataGridView();
            DAO.CloseConnetion();
        }
        private void btnbatdau_Click(object sender, EventArgs e)
        {
            ResetValues();
        }
        private void btnin_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable danhsach;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1: B3"].Font.Size = 10;
            exRange.Range["A1: B3"].Font.Name = "Times new roman";
            exRange.Range["A1: B3"].Font.Bold = true;
            exRange.Range["A1: B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1: A1"].ColumnWidth = 7;
            exRange.Range["B1: B1"].ColumnWidth = 15;
            exRange.Range["A1: B1"].MergeCells = true;
            exRange.Range["A1: B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1: B1"].Value = "CỔ PHONG BOOKS";
            exRange.Range["A2: B2"].MergeCells = true;
            exRange.Range["A2: B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2: B2"].Value = "Cầu Giấy - Hà Nội";
            exRange.Range["A3: B3"].MergeCells = true;
            exRange.Range["A3: B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3: B3"].Value = "Điện thoại: (04)37562222";
            exRange.Range["C4: E4"].Font.Size = 12;
            exRange.Range["C4: F4"].Font.Name = "Times new roman";
            exRange.Range["C4: F4"].Font.Bold = true;
            exRange.Range["C4: F4"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C4: F4"].MergeCells = true;
            exRange.Range["C4: F4"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C4: F4"].Value = "BÁO CÁO DS KHÁCH HÀNG KHÔNG MUA HÀNG THEO THÁNG";

            DAO.OpenConnection();
            sql = "SELECT * FROM KhachHang WHERE NOT EXISTS (SELECT * FROM HoaDonBan WHERE KhachHang.MaKhach = HoaDonBan.MaKhach AND MONTH (NgayBan) = " + Convert.ToInt32(cmbthang.Text) +")";
            BC9 = DAO.GetDataToTable(sql);
            DAO.RunSql(sql);
            danhsach = DAO.GetDataToTable(sql);

            exRange.Range["B6:G6"].Font.Bold = true;//In đậm các chữ 
            exRange.Range["B6:G6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B6:B6"].ColumnWidth = 12;
            exRange.Range["C6:C6"].ColumnWidth = 15;
            exRange.Range["D6:D6"].ColumnWidth = 25;
            exRange.Range["E6:E6"].ColumnWidth = 15;
            exRange.Range["F6:F6"].ColumnWidth = 15;
            exRange.Range["B6:B6"].Value = "STT";
            exRange.Range["C6:C6"].Value = "Mã khách";
            exRange.Range["D6:D6"].Value = "Tên khách";
            exRange.Range["E6:E6"].Value = "Địa chỉ";
            exRange.Range["F6:F6"].Value = "Số điện thoại";

            for (hang = 0; hang < danhsach.Rows.Count; hang++)
            {
                exSheet.Cells[2][hang + 7] = hang + 1;//điền số thứ tự vào cột 2 bắt đầu từ hàng 7 (mở excel ra hình dung)
                for (cot = 0; cot < danhsach.Columns.Count; cot++)
                {
                    exSheet.Cells[cot + 3][hang + 7] = danhsach.Rows[hang][cot].ToString();//điền thông tin các cột còn lại từ dữ liệu đã đc đổ vào từ biến "danhsach" bắt đầu từ cột 3, hàng 6
                }
            }
            exRange = exSheet.Cells[2][hang + 9];//chỗ này là đánh dấu vị trí viết cái dòng "Hà Nội, ngày..."
            exRange.Range["D1:F1"].MergeCells = true;
            exRange.Range["D1:F1"].Font.Italic = true;
            exRange.Range["D1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D1:F1"].Value = "Hà Nội, Ngày " + DateTime.Now.ToShortDateString();
            exSheet.Name = "Báo cáo";
            exApp.Visible = true;
            DAO.CloseConnetion();
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
