using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

/*
    // https://jacking75.github.io/NET_lib_ClosedXML/ 참고
    XLWorkbook workbook = YLManager.Excel.ExcelControl.CreateWorkBook(); // 워크북 생성

    IXLWorksheet worksheet = YLManager.Excel.ExcelControl.CreateWorkSheet("테스트시트", workbook);

    // 워크시트 안의 내용 생성
    worksheet = YLManager.Excel.ExcelControl.CreateCell(worksheet, 1, 1, "1_1 내용");
    worksheet = YLManager.Excel.ExcelControl.CreateCell(worksheet, 2, 1, "2_1 내용");
    worksheet = YLManager.Excel.ExcelControl.CreateCell(worksheet, 2, 2, "2_2 내용");


    YLManager.Excel.ExcelControl.CellStyle_Color(worksheet,1, 1, 255, 0, 0);
    YLManager.Excel.ExcelControl.CellStyle_Color(worksheet,2, 1, 255, 255, 0);
    YLManager.Excel.ExcelControl.CellStyle_Color(worksheet, 2, 2, 255, 255, 255);

    YLManager.Excel.ExcelControl.CellStyle_FontFamily(worksheet, 1, 1, "맑은 고딕");

    XLAlignmentHorizontalValues horizon = YLManager.Excel.ExcelControl.SelectHorizontalStyle(1);
    YLManager.Excel.ExcelControl.CellStyle_Horizontal_Alignment(worksheet, 1, 1, horizon);

    XLAlignmentVerticalValues verical = YLManager.Excel.ExcelControl.SelectVerticalStyle(3);
    YLManager.Excel.ExcelControl.CellStyle_Vertical_Alignment(worksheet, 2, 1, verical);


    YLManager.Excel.ExcelControl.SaveWorkbook(workbook,"test");
*/

namespace YLManager.Excel
{
    public class ExcelControl
    {
        /// <summary>
        /// 워크북 생성
        /// </summary>
        /// <returns></returns>
        public static XLWorkbook CreateWorkBook()
        {
            XLWorkbook workbook = new XLWorkbook(); // 워크북 생성
            return workbook;
        }


        /// <summary>
        /// 워크시트 생성
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static IXLWorksheet CreateWorkSheet(string sheetName, XLWorkbook workbook)
        {
            IXLWorksheet worksheet = workbook.Worksheets.Add(sheetName); // 해당 워크북에 받아온 시트네임으로 워크시트 생성
         
            return worksheet;
        }

        /// <summary>
        /// 워크시트에 컬럼생성
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static IXLWorksheet CreateCell(IXLWorksheet worksheet,int col, int row, string content)
        {
            worksheet.Cell(col, row).Value = content;

            return worksheet;
        }

        /// <summary>
        /// 셀 배경색 설정
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public static void CellStyle_Color(IXLWorksheet worksheet, int col, int row, int r, int g, int b)
        {
            IXLCell cell = worksheet.Cell(col, row);
            cell.Style.Fill.BackgroundColor = XLColor.FromArgb(r, g, b);
        }

        /// <summary>
        /// 글꼴 설정
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="family"></param>
        public static void CellStyle_FontFamily(IXLWorksheet worksheet, int col, int row, string family)
        {
            IXLCell cell = worksheet.Cell(col, row);
            cell.Style.Font.FontName = family;
        }

        /// <summary>
        /// Horizontal 정렬 설정
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public static void CellStyle_Horizontal_Alignment(IXLWorksheet worksheet, int col, int row, XLAlignmentHorizontalValues HorizontalStyle)
        {
            IXLCell cell = worksheet.Cell(col, row);
            cell.Style.Alignment.Horizontal = HorizontalStyle;
        }

        /// <summary>
        /// Horizontal Style 리턴
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XLAlignmentHorizontalValues SelectHorizontalStyle(int type)
        {
            switch(type)
            {
                case 0:
                    return XLAlignmentHorizontalValues.Center;
                case 1:
                    return XLAlignmentHorizontalValues.CenterContinuous;
                case 2:
                    return XLAlignmentHorizontalValues.Distributed;
                case 3:
                    return XLAlignmentHorizontalValues.Fill;
                case 4:
                    return XLAlignmentHorizontalValues.General;
                case 5:
                    return XLAlignmentHorizontalValues.Justify;
                case 6:
                    return XLAlignmentHorizontalValues.Left;
                case 7:
                    return XLAlignmentHorizontalValues.Right;
            }

            return XLAlignmentHorizontalValues.Center;
        }

        /// <summary>
        /// Vertical 정렬 설정
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="type"></param>
        public static void CellStyle_Vertical_Alignment(IXLWorksheet worksheet, int col, int row, XLAlignmentVerticalValues VerticalStyle)
        {
            IXLCell cell = worksheet.Cell(col, row);
            cell.Style.Alignment.Vertical = VerticalStyle;
        }

        /// <summary>
        /// Vertial Style 리턴
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XLAlignmentVerticalValues SelectVerticalStyle(int type)
        {
            switch (type)
            {
                case 0: 
                    return XLAlignmentVerticalValues.Bottom; 
                case 1: 
                    return XLAlignmentVerticalValues.Center;
                case 2: 
                    return XLAlignmentVerticalValues.Distributed;
                case 3: 
                    return XLAlignmentVerticalValues.Justify;
                case 4: 
                    return XLAlignmentVerticalValues.Top;
            }

            return XLAlignmentVerticalValues.Center;
        }

        /// <summary>
        /// Border Style 설정
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="boardStyle"></param>
        public static void CellStyle_BorderStyle(IXLWorksheet worksheet, int col, int row, XLBorderStyleValues boardStyle)
        {
            IXLCell cell = worksheet.Cell(col, row);
            cell.Style.Border.OutsideBorder = boardStyle;
        }

        /// <summary>
        /// Border Style 리턴
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XLBorderStyleValues SelectBorderStyle(int type)
        {
            switch(type)
            {
                case 0:
                    return XLBorderStyleValues.DashDot;
                case 1:
                    return XLBorderStyleValues.DashDotDot;
                case 2:
                    return XLBorderStyleValues.Dashed;
                case 3:
                    return XLBorderStyleValues.Dotted;
                case 4:
                    return XLBorderStyleValues.Double;
                case 5:
                    return XLBorderStyleValues.Hair;
                case 6:
                    return XLBorderStyleValues.Medium;
                case 7:
                    return XLBorderStyleValues.MediumDashDot;
                case 8:
                    return XLBorderStyleValues.MediumDashDotDot;
                case 9:
                    return XLBorderStyleValues.MediumDashed;
                case 10:
                    return XLBorderStyleValues.None;
                case 11:
                    return XLBorderStyleValues.SlantDashDot;
                case 12:
                    return XLBorderStyleValues.Thick;
                case 13:
                    return XLBorderStyleValues.Thin;
            }
            return XLBorderStyleValues.MediumDashDotDot;
        }

        /// <summary>
        /// 테두리 색상 설정
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="color"></param>
        public static void CellStyle_BorderColor(IXLWorksheet worksheet, int col, int row, int r,int g,int b)
        {
            IXLCell cell = worksheet.Cell(col, row);
            cell.Style.Border.OutsideBorderColor = XLColor.FromArgb(r, g, b);
        }

        /// <summary>
        /// Column Width 지정
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="col"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static IXLWorksheet SetColWidth(IXLWorksheet worksheet, int col, int width)
        {
            worksheet.Column(col).Width = width;
            return worksheet;
        }

        public static IXLWorksheet SetRowHeight(IXLWorksheet worksheet, int row, int height)
        {
            worksheet.Row(row).Height = height;
            return worksheet;
        }

        /// <summary>
        /// 셀 병합
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="startcol"></param>
        /// <param name="startrow"></param>
        /// <param name="endcol"></param>
        /// <param name="endrow"></param>
        public static void SetRange(IXLWorksheet worksheet, int startcol, int startrow, int endcol, int endrow)
        {
            IXLRange range = worksheet.Range(worksheet.Cell(startcol, startrow), worksheet.Cell(endcol, endrow));
        }

        /// <summary>
        /// 셀 병합 테두리 색상 지정
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="startcol"></param>
        /// <param name="startrow"></param>
        /// <param name="endcol"></param>
        /// <param name="endrow"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public static void SetRangeBackgroundColor(IXLWorksheet worksheet, int startcol, int startrow, int endcol, int endrow, int r, int g, int b)
        {
            IXLRange range = worksheet.Range(worksheet.Cell(startcol, startrow), worksheet.Cell(endcol, endrow));
            range.Style.Fill.BackgroundColor = XLColor.FromArgb(r, g, b);
        }

        /// <summary>
        /// 엑셀 저장
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="fileName"></param>
        public static void SaveWorkbook(XLWorkbook workbook, string fileName)
        {
            workbook.SaveAs($"{fileName}.xlsx", false);
            //workbook.SaveAs("HelloWorld.xlsx");
        }

    }
}
