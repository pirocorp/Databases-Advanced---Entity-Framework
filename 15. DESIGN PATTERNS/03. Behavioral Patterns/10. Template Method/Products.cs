namespace _10._Template_Method
{
    using System;
    using System.Data;
    using System.Data.OleDb;

    public class Products : DataAccessObject
    {
        public override void Select()
        {
            var sql = "select ProductName from Products";
            var dataAdapter = new OleDbDataAdapter(sql, this.connectionString);

            this.dataSet = new DataSet();
            dataAdapter.Fill(this.dataSet, "Products");
        }

        public override void Process()
        {
            Console.WriteLine("Products ---- ");
            var dataTable = dataSet.Tables["Products"];
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["ProductName"]);
            }

            Console.WriteLine();
        }
    }
}
