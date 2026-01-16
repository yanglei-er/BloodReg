using BloodReg.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using MiniExcelLibs;
using MiniExcelLibs.OpenXml;
using SqlSugar;
using System.IO;

namespace BloodReg.ViewModels
{
    public partial class DatabaseImportDialogViewModel : ObservableObject
    {
        private readonly ISqlSugarClient db;

        [ObservableProperty]
        private bool _isImporting = false;

        public DatabaseImportDialogViewModel(ISqlSugarClient _db)
        {
            db = _db;
        }

        public async ValueTask<bool> Import(string method)
        {
            if (method == "excel")
            {
                OpenFileDialog openFileDialog = new()
                {
                    Title = "导入",
                    Filter = "Excel 工作簿 (*.xlsx)|*.xlsx"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    IsImporting = true;
                    if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "database.db")))
                    {
                        db.DbMaintenance.CreateDatabase();
                    }
                    if (!db.DbMaintenance.IsAnyTable("Student"))
                    {
                        db.CodeFirst.InitTables<Student>();
                    }
                    if (!db.DbMaintenance.IsAnyTable("Teacher"))
                    {
                        db.CodeFirst.InitTables<Teacher>();
                    }
                    if (!db.DbMaintenance.IsAnyTable("InternationalStudent"))
                    {
                        db.CodeFirst.InitTables<InternationalStudent>();
                    }
                    if (!db.DbMaintenance.IsAnyTable("OutsidePeople"))
                    {
                        db.CodeFirst.InitTables<OutsidePeople>();
                    }

                    using var stream = new FileStream(openFileDialog.FileName, FileMode.Open);
                    var config = new OpenXmlConfiguration { EnableSharedStringCache = false };

                    await Task.Run(async () =>
                    {
                        var studentRows = await MiniExcel.QueryAsync<Student>(stream, sheetName: "学生", configuration: config);
                        await db.Ado.BeginTranAsync();
                        foreach (var row in studentRows)
                        {
                            await db.Storageable<Student>(row).ExecuteCommandAsync();
                        }
                        await db.Ado.CommitTranAsync();
                        stream.Position = 0;
                        var teacherRows = await MiniExcel.QueryAsync<Teacher>(stream, sheetName: "教职工", configuration: config);
                        await db.Ado.BeginTranAsync();
                        foreach (var row in teacherRows)
                        {
                            await db.Storageable<Teacher>(row).ExecuteCommandAsync();
                        }
                        await db.Ado.CommitTranAsync();
                        stream.Position = 0;
                        var internationalStudentRows = await MiniExcel.QueryAsync<InternationalStudent>(stream, sheetName: "留学生", configuration: config);
                        await db.Ado.BeginTranAsync();
                        foreach (var row in internationalStudentRows)
                        {
                            await db.Storageable<InternationalStudent>(row).ExecuteCommandAsync();
                        }
                        await db.Ado.CommitTranAsync();
                        stream.Position = 0;
                        var outsidePeopleRows = await MiniExcel.QueryAsync<OutsidePeople>(stream, sheetName: "校外人员", configuration: config);
                        await db.Ado.BeginTranAsync();
                        foreach (var row in outsidePeopleRows)
                        {
                            await db.Storageable<OutsidePeople>(row).ExecuteCommandAsync(); await db.Ado.CommitTranAsync();
                        }
                        await db.Ado.CommitTranAsync();
                    });
                    IsImporting = false;
                    return true;
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new()
                {
                    Title = "导入",
                    Filter = "SQLite 数据库 (*.db)|*.db"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    IsImporting = true;
                    if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "database.db")))
                    {
                        db.DbMaintenance.CreateDatabase();
                    }
                    if (!db.DbMaintenance.IsAnyTable("Student"))
                    {
                        db.CodeFirst.InitTables<Student>();
                    }
                    if (!db.DbMaintenance.IsAnyTable("Teacher"))
                    {
                        db.CodeFirst.InitTables<Teacher>();
                    }
                    if (!db.DbMaintenance.IsAnyTable("InternationalStudent"))
                    {
                        db.CodeFirst.InitTables<InternationalStudent>();
                    }
                    if (!db.DbMaintenance.IsAnyTable("OutsidePeople"))
                    {
                        db.CodeFirst.InitTables<OutsidePeople>();
                    }

                    var srcDb = new SqlSugarClient(new ConnectionConfig()
                    {
                        ConnectionString = $"DataSource={openFileDialog.FileName}",
                        DbType = DbType.Sqlite,
                        IsAutoCloseConnection = true
                    });
                    await Task.Run(async () =>
                    {
                        if (srcDb.DbMaintenance.IsAnyTable("Student"))
                        {
                            await db.Ado.BeginTranAsync();
                            var srcStudents = await srcDb.Queryable<Student>().ToListAsync();
                            foreach (var row in srcStudents)
                            {
                                await db.Storageable<Student>(row).ExecuteCommandAsync();
                            }
                            await db.Ado.CommitTranAsync();
                        }

                        if (srcDb.DbMaintenance.IsAnyTable("Teacher"))
                        {
                            await db.Ado.BeginTranAsync();
                            var srcTeachers = await srcDb.Queryable<Teacher>().ToListAsync();
                            foreach (var row in srcTeachers)
                            {
                                await db.Storageable<Teacher>(row).ExecuteCommandAsync();
                            }
                            await db.Ado.CommitTranAsync();
                        }

                        if (srcDb.DbMaintenance.IsAnyTable("InternationalStudent"))
                        {
                            await db.Ado.BeginTranAsync();
                            var srcInternational = await srcDb.Queryable<InternationalStudent>().ToListAsync();
                            foreach (var row in srcInternational)
                            {
                                await db.Storageable<InternationalStudent>(row).ExecuteCommandAsync();
                            }
                            await db.Ado.CommitTranAsync();
                        }

                        if (srcDb.DbMaintenance.IsAnyTable("OutsidePeople"))
                        {
                            await db.Ado.BeginTranAsync();
                            var srcOutside = await srcDb.Queryable<OutsidePeople>().ToListAsync();
                            foreach (var row in srcOutside)
                            {
                                await db.Storageable<OutsidePeople>(row).ExecuteCommandAsync();
                            }
                            await db.Ado.CommitTranAsync();
                        }
                    });
                    IsImporting = false;
                    return true;
                }
            }
            return false;
        }
    }
}