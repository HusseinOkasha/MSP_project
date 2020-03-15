using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Models

{
	public class DataBaseHandler
	{
		
		public const string connectionString = "Data Source=DESKTOP-4JCKA8A;Initial Catalog=med_pin;Integrated Security=True";

		public static void Insert(string tableName, string[] values)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();

			string cmdS = "Insert into " + tableName + " values (";

			for(int i = 0; i < values.Length; i++)
			{
				cmdS += "'" + values[i] + "'";
				if(i != values.Length - 1)
				{
					cmdS += ", ";
				}
			}
			cmdS += ");";

			SqlCommand cmd = new SqlCommand(cmdS, connection);
			cmd.ExecuteNonQuery();

			connection.Close();
		}

		public static void customInsert(string tableName, string[] columns, string[] values)
		{
			if (columns.Length != values.Length)
				return;

			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			string cmdS = "Insert into " + tableName + " values (";

			string columnsS = "(";
			string valuesS = "values (";
			for (int i = 0; i < values.Length; i++)
			{
				columnsS += columns[i];
				valuesS += values[i];
				if (i != values.Length - 1)
				{
					columnsS += ", ";
					valuesS += ", ";
				}
			}
			columnsS += ") ";
			valuesS += ");";

			SqlCommand cmd = new SqlCommand(cmdS + columnsS + valuesS, connection);
			cmd.ExecuteNonQuery();

			connection.Close();
		}

		public static DataTable Select(string tableName, string clause = "")
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			string cmdS = "SELECT * FROM " + tableName + " " + clause;
			SqlCommand cmd = new SqlCommand(cmdS, connection);

			SqlDataAdapter data = new SqlDataAdapter(cmd);

			DataTable table = new DataTable();
			data.Fill(table);

			connection.Close();
			data.Dispose();

			return table;
		}

		public static DataTable customSelect(string tableName, string[] columns, string clause = "")
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();

			string columnclause = "";
			for(int i = 0; i < columns.Length; i++)
			{
				columnclause += columns[i];
				if (i != columns.Length - 1)
				{
					columnclause += ", ";
				}
			}
			string cmdS = "SELECT " + columnclause + " FROM " + tableName + " " + clause;

			SqlCommand cmd = new SqlCommand(cmdS, connection);

			SqlDataAdapter data = new SqlDataAdapter(cmd);

			DataTable table = new DataTable();
			data.Fill(table);

			connection.Close();
			data.Dispose();

			return table;
		}

		public static void Update(string tableName, string[] columns, string[] values, string clause)
		{
			if (columns.Length != values.Length)
				return;

			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();

			string valueQ = "";
			for(int i = 0; i < values.Length; i++)
			{
				valueQ += columns[i] + "='" + values[i] +"'";
				if (i != values.Length - 1)
				{
					valueQ += ", ";
				}
			}
			string cmdS = "UPDATE " + tableName + " SET " + valueQ + " " + clause;

			SqlCommand cmd = new SqlCommand(cmdS, connection);
			cmd.ExecuteNonQuery();

			connection.Close();
		}

		public static void Delete(string tableName, string clause)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();

			string cmdS = "Delete from " + tableName + clause;

			SqlCommand cmd = new SqlCommand(cmdS, connection);
			cmd.ExecuteNonQuery();

			connection.Close();
		}

		public static void Exec(string command)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			SqlCommand cmd = new SqlCommand(command, connection);
			cmd.ExecuteNonQuery();

			connection.Close();
		}
	}
}
