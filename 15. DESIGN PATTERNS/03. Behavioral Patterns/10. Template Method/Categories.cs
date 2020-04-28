namespace _10._Template_Method
{
    using System;
    using System.Data;
    using System.Data.OleDb;

    /// <summary>
    /// A 'ConcreteClass' class
    /// </summary>
    public class Categories : DataAccessObject
    {
        public override void Select()
        {
            var sql = "select CategoryName from Categories";
            var dataAdapter = new OleDbDataAdapter(sql, this.connectionString);

            this.dataSet = new DataSet();
            dataAdapter.Fill(this.dataSet, "Categories");
        }

        public override void Process()
        {
            Console.WriteLine("Categories ---- ");

            var dataTable = this.dataSet.Tables["Categories"];
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["CategoryName"]);
            }
            Console.WriteLine();
        }
    }
}
