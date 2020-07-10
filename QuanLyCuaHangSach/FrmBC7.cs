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
    public partial class FrmBC7 : Form
    {
        DataTable BC7;
        public FrmBC7()
        {
            InitializeComponent();
        }
        private void FrmBC7_Load(object sender, EventArgs e)
        {
        }
        private void btnBatDau_Click(object sender, EventArgs e)
        {
            txtNam.Text = "";
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadDataGridView()
        {
            dataGridViewBC7.AllowUserToAddRows = false;
            dataGridViewBC7.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void btnHXuat_Click(object sender, EventArgs e)
        {
            DAO.OpenConnection();
            string sql = "SELECT TOP (5)* FROM HoaDonNhap WHERE NgayNhap LIKE '%" + txtNam.Text + "%' ORDER BY TongTien DESC";
            BC7 = DAO.GetDataToTable(sql);
            dataGridViewBC7.DataSource = BC7;
            dataGridViewBC7.Columns[0].HeaderText = "Số HĐN";
            dataGridViewBC7.Columns[0].Width = 145;
            dataGridViewBC7.Columns[1].HeaderText = "Ngày nhập";
            dataGridViewBC7.Columns[1].Width = 100;
            dataGridViewBC7.Columns[2].HeaderText = "Mã NCC";
            dataGridViewBC7.Columns[2].Width = 80;
            dataGridViewBC7.Columns[3].HeaderText = "Mã NV";
            dataGridViewBC7.Columns[3].Width = 75;
            dataGridViewBC7.Columns[4].HeaderText = "Tổng tiền";
            dataGridViewBC7.Columns[4].Width = 80;
            LoadDataGridView();
            DAO.CloseConnetion();
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook;
            COMExcel.Worksheet exSheet;
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable danhsach;

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman";
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5;
            exRange.Range["A1:A1"].ColumnWidth = 10;
            exRange.Range["B1:B1"].ColumnWidth = 20;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "CỔ PHONG BOOKS";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Cầu Giấy - Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (04)37562222";
            exRange.Range["C2:G2"].Font.Size = 16;
            exRange.Range["C2:G2"].Font.Bold = true;
            exRange.Range["C2:G2"].Font.ColorIndex = 3;
            exRange.Range["C2:G2"].MergeCells = true;
            exRange.Range["C2:G2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:G2"].Value = "Báo cáo 5 hóa đơn có tổng tiền nhập lớn nhất";
            sql = "SELECT TOP (5)* FROM HoaDonNhap WHERE NgayNhap LIKE '%" + txtNam.Text + "%' ORDER BY TongTien DESC";
            danhsach = DAO.GetDataToTable(sql);
            exRange.Range["B5:G5"].Font.Bold = true;
            exRange.Range["B5:G5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B5:B5"].ColumnWidth = 13;
            exRange.Range["C5:C5"].ColumnWidth = 30;
            exRange.Range["D5:D5"].ColumnWidth = 15;
            exRange.Range["E5:E5"].ColumnWidth = 15;
            exRange.Range["F5:F5"].ColumnWidth = 15;
            exRange.Range["G5:G5"].ColumnWidth = 15;
            exRange.Range["B5:B5"].Value = "STT";
            exRange.Range["C5:C5"].Value = "Số hóa đơn nhập";
            exRange.Range["D5:D5"].Value = "Ngày nhập";
            exRange.Range["E5:E5"].Value = "Mã NCC";
            exRange.Range["F5:F5"].Value = "Mã nhân viên";
            exRange.Range["G5:G5"].Value = "Tổng tiền";

            for (hang = 0; hang < danhsach.Rows.Count; hang++)
            {
                exSheet.Cells[2][hang + 6] = hang + 1;
                for (cot = 0; cot < danhsach.Columns.Count; cot++)
                {
                    exSheet.Cells[cot + 3][hang + 6] = danhsach.Rows[hang][cot].ToString();
                }
            }
            exRange = exSheet.Cells[2][hang + 8];
            exRange.Range["D1:F1"].MergeCells = true;
            exRange.Range["D1:F1"].Font.Italic = true;
            exRange.Range["D1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D1:F1"].Value = "Hà Nội, " + DateTime.Now.ToShortDateString();
            exSheet.Name = "Báo cáo";
            exApp.Visible = true;
        }
    }
}