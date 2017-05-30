using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

namespace ClassLib
{
    public static class Classlib
    {
        public static string User = "None";
        public static string DomainUser = "None";
        public static int ScreenWidth = 1024;
        public static int ScreenHight = 768;
        public static Form CreateForm(string form)
        {
            try
            {
                
                Type t = Type.GetType("StockControl." + form);
                return (Form)Activator.CreateInstance(t);
            }
            // catch (Exception ex) { ErrorAdd("Open CreateForm" + "FMS." + form, ex.ToString(), "BaseClass.cs"); return null; }
            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + "ไม่มีไฟล์ link"); return null; }

        }
        // ฟังก์ชั่น Update DatagridView
        public static void DGVCOMMIT(object sender, EventArgs e) //Commit
        {
            DataGridView obj = null;
            obj = (DataGridView)sender;
            if (obj.CurrentCell is DataGridViewCheckBoxCell || obj.CurrentCell is DataGridViewComboBoxCell)
            {
                obj.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others         will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
    public class Memory
    {

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize",
        ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int SetProcessWorkingSetSize(
          IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public static void Heap()
        {
            IntPtr ptr = Marshal.AllocHGlobal(1024);
            if (ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ptr);
                ptr = IntPtr.Zero;
                GC.RemoveMemoryPressure(1024);
            }
        }
    }
}
