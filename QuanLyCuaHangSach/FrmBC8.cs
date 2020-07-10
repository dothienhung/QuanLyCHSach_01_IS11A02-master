﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangSach
{
    public partial class FrmBC8 : Form
    {
        DataTable BC8;
        public FrmBC8()
        {
            InitializeComponent();
        }

        private void FrmBC8_Load(object sender, EventArgs e)
        {
        }
        private void ResetValues()
        {
            cboQuy.Text = "";
        }
        private void btnBatDau_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void LoadDataGridView()
        {
            dataGridViewC8.AllowUserToAddRows = false;
            dataGridViewC8.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            //Xuất báo cáo
            DAO.OpenConnection();
            string sql = "SELECT SoHDB, TongTien FROM HoaDonBan WHERE 1=1";
            if (cboQuy.Text == "")
                MessageBox.Show("Bạn phải chọn quý!");
            if (cboQuy.Text == "1")
            {
                sql = sql + "AND NgayBan >= '2020-01-01' AND NgayBan <= '2020-03-31'";
            }
            if (cboQuy.Text == "2")
            {
                sql = sql + "AND NgayBan >= '2020-04-01' AND NgayBan <= '2020-06-30'";
            }    
            if (cboQuy.Text == "3")
            {
                sql = sql + "AND NgayBan >= '2020-07-01' AND NgayBan <= '2020-09-30'";
            }    
            if (cboQuy.Text == "4")
            {
                sql = sql + "AND NgayBan >= '2020-10-01' AND NgayBan <= '2020-12-31'";
            }
            BC8 = DAO.GetDataToTable(sql);
            dataGridViewC8.DataSource = BC8;
            dataGridViewC8.Columns[0].HeaderText = "Số hóa đơn bán";
            dataGridViewC8.Columns[0].Width = 160;
            dataGridViewC8.Columns[1].HeaderText = "Tổng tiền";
            dataGridViewC8.Columns[1].Width = 150;
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
            exRange.Range["C2:G2"].Value = "Báo cáo danh sách hóa đơn và tổng tiền theo quý";
            sql = "SELECT SoHDB, TongTien FROM HoaDonBan WHERE 1=1";
            if (cboQuy.Text == "")
                MessageBox.Show("Bạn phải chọn quý!");
            if (cboQuy.Text == "1")
            {
                sql = sql + "AND NgayBan >= '2020-01-01' AND NgayBan <= '2020-03-31'";
            }
            if (cboQuy.Text == "2")
            {
                sql = sql + "AND NgayBan >= '2020-04-01' AND NgayBan <= '2020-06-30'";
            }
            if (cboQuy.Text == "3")
            {
                sql = sql + "AND NgayBan >= '2020-07-01' AND NgayBan <= '2020-09-30'";
            }
            if (cboQuy.Text == "4")
            {
                sql = sql + "AND NgayBan >= '2020-10-01' AND NgayBan <= '2020-12-31'";
            }
            danhsach = DAO.GetDataToTable(sql);
            exRange.Range["B5:G5"].Font.Bold = true;
            exRange.Range["B5:G5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B5:B5"].ColumnWidth = 13;
            exRange.Range["C5:C5"].ColumnWidth = 30;
            exRange.Range["D5:D5"].ColumnWidth = 25;
            exRange.Range["B5:B5"].Value = "STT";
            exRange.Range["C5:C5"].Value = "Số hóa đơn bán";
            exRange.Range["D5:D5"].Value = "Tổng tiền";

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
        private void btnTongQuy_Click(object sender, EventArgs e)
        {
            //Tính tổng tiền bán được trong 1 quý
            DAO.OpenConnection();
            string sql = "SELECT SUM (TongTien) FROM HoaDonBan WHERE 1=1";
            if (cboQuy.Text == "")
                MessageBox.Show("Bạn phải chọn quý!");
            if (cboQuy.Text == "1")
            {
                sql = sql + "AND NgayBan >= '2020-01-01' AND NgayBan <= '2020-03-31'";
            }
            if (cboQuy.Text == "2")
            {
                sql = sql + "AND NgayBan >= '2020-04-01' AND NgayBan <= '2020-06-30'";
            }
            if (cboQuy.Text == "3")
            {
                sql = sql + "AND NgayBan >= '2020-07-01' AND NgayBan <= '2020-09-30'";
            }
            if (cboQuy.Text == "4")
            {
                sql = sql + "AND NgayBan >= '2020-10-01' AND NgayBan <= '2020-12-31'";
            }
            BC8 = DAO.GetDataToTable(sql);
            dataGridView2.DataSource = BC8;
            dataGridView2.Columns[0].HeaderText = "Tổng quý";
            dataGridView2.Columns[0].Width = 120;
            LoadDataGridView();
            DAO.CloseConnetion();
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
