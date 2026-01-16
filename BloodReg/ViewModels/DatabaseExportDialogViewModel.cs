using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using MiniExcelLibs;
using MiniExcelLibs.OpenXml;
using SqlSugar;
using System.IO;

namespace BloodReg.ViewModels
{
    public partial class DatabaseExportDialogViewModel : ObservableObject
    {
        private readonly ISqlSugarClient db;

        [ObservableProperty]
        private bool _isExporting = false;

        public DatabaseExportDialogViewModel(ISqlSugarClient _db)
        {
            db = _db;
        }

        public async ValueTask<bool> Export(string method)
        {
            if (method == "excel")
            {
                SaveFileDialog saveFileDialog = new()
                {
                    Title = "导出数据",
                    FileName = "献血数据" + DateTime.Now.ToString("yyyyMMdd"),
                    Filter = "Excel 工作簿 (*.xlsx)|*.xlsx",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    IsExporting = true;
                    var sheets = new Dictionary<string, object?>();
                    if (db.DbMaintenance.IsAnyTable("Student"))
                    {
                        sheets.Add("学生", await db.Ado.GetDataReaderAsync("SELECT * FROM Student"));
                    }
                    else
                    {
                        sheets.Add("学生", null);
                    }

                    if (db.DbMaintenance.IsAnyTable("Teacher"))
                    {
                        sheets.Add("教职工", await db.Ado.GetDataReaderAsync("SELECT * FROM Teacher"));
                    }
                    else
                    {
                        sheets.Add("教职工", null);
                    }

                    if (db.DbMaintenance.IsAnyTable("InternationalStudent"))
                    {
                        sheets.Add("留学生", await db.Ado.GetDataReaderAsync("SELECT * FROM InternationalStudent"));
                    }
                    else
                    {
                        sheets.Add("留学生", null);
                    }

                    if (db.DbMaintenance.IsAnyTable("OutsidePeople"))
                    {
                        sheets.Add("校外人员", await db.Ado.GetDataReaderAsync("SELECT * FROM OutsidePeople"));
                    }
                    else
                    {
                        sheets.Add("校外人员", null);
                    }
                    OpenXmlConfiguration config = new() { TableStyles = TableStyles.None };
                    await MiniExcel.SaveAsAsync(saveFileDialog.FileName, sheets, overwriteFile: true, configuration: config);
                    return true;
                }
            }
            else
            {
                SaveFileDialog saveFileDialog = new()
                {
                    Title = "导出数据",
                    FileName = "献血数据" + DateTime.Now.ToString("yyyyMMdd"),
                    Filter = "SQLite 数据库 (*.db)|*.db",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    IsExporting = true;
                    File.Copy(@".\database.db", saveFileDialog.FileName, true);
                    return true;
                }
            }
            IsExporting = false;
            return false;
        }
    }
}
