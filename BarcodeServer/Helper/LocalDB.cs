using BarcodeServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeServer.Helper
{
	public class LocalDB
	{
		private static readonly LocalDB instance = new LocalDB();
		public Dictionary<string, string> _sp = new Dictionary<string, string>();

		private string connectionString = string.Empty;

		public static LocalDB Instance
		{
			get
			{
				return instance;
			}
		}

		private LocalDB()
		{
			string currentPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
			string sqlFilePath = Path.Combine(currentPath, "barcodedb.sqlite");

			connectionString = $"Data Source={sqlFilePath};";

			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();
				CreateTableIfNotExists(connection);
			}
		}

		public string InvoiceInsert(InvoiceModel invoiceModel)
		{
			string invoice_id = string.Empty;
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();
				SQLiteCommand insertCommand = new SQLiteCommand();
				insertCommand.Connection = connection;
				insertCommand.CommandText = "INSERT INTO invoices(invoice_date, invoice_title, create_date) VALUES (@invoice_date, @invoice_title, @create_date)";
				insertCommand.Parameters.Add("@invoice_date", DbType.String);
				insertCommand.Parameters.Add("@invoice_title", DbType.String);
				insertCommand.Parameters.Add("@create_date", DbType.String);

				insertCommand.Parameters[0].Value = invoiceModel.InvoiceDate;
				insertCommand.Parameters[1].Value = invoiceModel.InvoiceTitle;
				insertCommand.Parameters[2].Value = invoiceModel.CreateDate;

				int affected = insertCommand.ExecuteNonQuery();

				Console.WriteLine("# of affected row: " + affected);

				SQLiteCommand selectCommand = new SQLiteCommand();
				selectCommand.Connection = connection;
				selectCommand.CommandText = "select seq from sqlite_sequence where name = @table_name";
				selectCommand.Parameters.AddWithValue("@table_name", "invoices");
				
				object result = selectCommand.ExecuteScalar();

				Console.WriteLine("# result seq : " + result.ToString());
				invoice_id = result.ToString();
			}

			return invoice_id;
		}

		public DataTable InvoiceSearch(string fromDate, string toDate)
		{
			DataTable dt = new DataTable();

			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				SQLiteCommand selectCommand = new SQLiteCommand();
				selectCommand.Connection = connection;
				selectCommand.CommandText = @"
												SELECT 
														invoice_id as InvoiceId, 
														invoice_date as InvoiceDate, 
														invoice_title as InvoiceTitle, 
														create_date as CreateDate  
												FROM invoices 
												WHERE invoice_date BETWEEN @fromDate AND @toDate 
												ORDER BY invoice_id;
											";
				selectCommand.Parameters.AddWithValue("@fromDate", fromDate);
				selectCommand.Parameters.AddWithValue("@toDate", toDate);

				SQLiteDataReader reader = selectCommand.ExecuteReader();
				dt.Load(reader);
			}

			return dt;
		}


		public void InvoiceUpdate(InvoiceModel invoiceModel)
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				SQLiteCommand updateCommand = new SQLiteCommand();
				updateCommand.Connection = connection;
				updateCommand.CommandText = @" 
												UPDATE invoices 
												SET invoice_title = @invoice_title 
												WHERE invoice_id = @invoice_id;
											";

				updateCommand.Parameters.Add("@invoice_id", DbType.Int32);
				updateCommand.Parameters.Add("@invoice_title", DbType.String);

				updateCommand.Parameters[0].Value = invoiceModel.InvoiceId;
				updateCommand.Parameters[1].Value = invoiceModel.InvoiceTitle;

				int affected = updateCommand.ExecuteNonQuery();
				Console.WriteLine("# of affected row: " + affected);
			}
		}


		public DataTable InvoiceItemSearch(int invoiceId)
		{
			DataTable dt = new DataTable();

			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				SQLiteCommand selectCommand = new SQLiteCommand();
				selectCommand.Connection = connection;
				selectCommand.CommandText = @"
												SELECT invoice_line_id AS InvoiceLineId,
														invoice_id AS InvoiceId, 
														item_id AS ItemId, 
														item_nm AS ItemNm, 
														order_qty AS OrderQty, 
														scan_qty AS ScanQty, 
														create_date AS CreateDate, 
														modify_date AS ModifyDate,
														scan_date AS ScanDate
												FROM invoice_items
												WHERE invoice_id = @invoice_id											
												ORDER BY invoice_line_id;
											";
				selectCommand.Parameters.AddWithValue("@invoice_id", invoiceId);

				SQLiteDataReader reader = selectCommand.ExecuteReader();
				dt.Load(reader);
			}

			return dt;
		}

		public void InvoiceItemInsert(int invoiceId, List<InvoiceItemModel> itemList)
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();
				using (SQLiteTransaction mytransaction = connection.BeginTransaction())
				{
					SQLiteCommand insertCommand = new SQLiteCommand();
					insertCommand.Transaction = mytransaction;
					insertCommand.Connection = connection;
					insertCommand.CommandText = @"
													INSERT INTO invoice_items(invoice_id, item_id, item_nm, order_qty, scan_qty, create_date, modify_date) VALUES 
													(@invoice_id, @item_id, @item_nm, @order_qty, @scan_qty, @create_date, @modify_date)
												";

					insertCommand.Parameters.Add("@invoice_id", DbType.Int32);
					insertCommand.Parameters.Add("@item_id", DbType.String);
					insertCommand.Parameters.Add("@item_nm", DbType.String);
					insertCommand.Parameters.Add("@order_qty", DbType.Int32);
					insertCommand.Parameters.Add("@scan_qty", DbType.Int32);
					insertCommand.Parameters.Add("@create_date", DbType.String);
					insertCommand.Parameters.Add("@modify_date", DbType.String);

					foreach (var item in itemList)
					{
						insertCommand.Parameters[0].Value = item.InvoiceId;
						insertCommand.Parameters[1].Value = item.ItemId;
						insertCommand.Parameters[2].Value = item.ItemNm;
						insertCommand.Parameters[3].Value = item.OrderQty;
						insertCommand.Parameters[4].Value = item.ScanQty;
						insertCommand.Parameters[5].Value = item.CreateDate;
						insertCommand.Parameters[6].Value = item.ModifyDate;
						insertCommand.Prepare();
						int affected1 = insertCommand.ExecuteNonQuery();
						Console.WriteLine("# of affected row: " + affected1);
					}
					mytransaction.Commit();
				}
			}

			InvoiceItemSearch(invoiceId);
		}

		public void InvoiceItemUpdate(List<InvoiceItemModel> itemList)
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();
				using (SQLiteTransaction mytransaction = connection.BeginTransaction())
				{
					SQLiteCommand insertCommand = new SQLiteCommand();
					insertCommand.Transaction = mytransaction;
					insertCommand.Connection = connection;
					insertCommand.CommandText = @"
													UPDATE invoice_items
													SET 
														item_nm = @item_nm
													,	order_qty = @order_qty
													,	scan_qty = @scan_qty
													,	modify_date = @modify_date
													WHERE invoice_line_id = @invoice_line_id
													AND invoice_id = @invoice_id
													AND item_id = @item_id;
												";

					insertCommand.Parameters.Add("@invoice_line_id", DbType.Int32);
					insertCommand.Parameters.Add("@invoice_id", DbType.Int32);
					insertCommand.Parameters.Add("@item_id", DbType.String);
					insertCommand.Parameters.Add("@item_nm", DbType.String);
					insertCommand.Parameters.Add("@order_qty", DbType.Int32);
					insertCommand.Parameters.Add("@scan_qty", DbType.Int32);
					insertCommand.Parameters.Add("@modify_date", DbType.String);

					foreach (var item in itemList)
					{
						insertCommand.Parameters[0].Value = item.InvoiceLineId;
						insertCommand.Parameters[1].Value = item.InvoiceId;
						insertCommand.Parameters[2].Value = item.ItemId;
						insertCommand.Parameters[3].Value = item.ItemNm;
						insertCommand.Parameters[4].Value = item.OrderQty;
						insertCommand.Parameters[4].Value = item.ScanQty;
						insertCommand.Parameters[6].Value = item.ModifyDate;
						insertCommand.Prepare();
						int affected1 = insertCommand.ExecuteNonQuery();
						Console.WriteLine("# of affected row: " + affected1);
					}
					mytransaction.Commit();
				}
			}
		}


		private void CreateTableIfNotExists(SQLiteConnection conn)
		{
			string sql = string.Empty;
			
			//string sql = "create table if not exists mytable(id int, NAME varchar(50), age int, DESCRIPTION varchar(150))";
			//new SQLiteCommand(sql, conn).ExecuteNonQuery();

			//sql = "create index if not exists idx_NAME on mytable(NAME)";
			//new SQLiteCommand(sql, conn).ExecuteNonQuery();

			sql = @"
					CREATE TABLE if not exists invoices
					(
						[invoice_id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
						[invoice_date] TEXT NOT NULL,
						[invoice_title] TEXT NOT NULL,
						[create_date] TEXT NOT NULL
					)
				   ";
			new SQLiteCommand(sql, conn).ExecuteNonQuery();

			sql = @"
					CREATE INDEX if not exists [IDX_InvoiceInvoiceDate] ON invoices ([invoice_date])
				   ";
			new SQLiteCommand(sql, conn).ExecuteNonQuery();


			sql = @"
					CREATE TABLE if not exists invoice_items
					(
						[invoice_line_id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
						[invoice_id] INTEGER  NOT NULL,
						[item_id] TEXT  NOT NULL,
						[item_nm] TEXT NOT NULL,
						[order_qty] INTEGER  NOT NULL DEFAULT 0,
						[scan_qty] INTEGER  NOT NULL DEFAULT 0,
						[create_date] TEXT NULL,
						[modify_date] TEXT NULL,
						[scan_date] TEXT NULL,
						FOREIGN KEY ([invoice_id]) REFERENCES invoices ([invoice_id]) 
							ON DELETE NO ACTION ON UPDATE NO ACTION
					)
				   ";
			new SQLiteCommand(sql, conn).ExecuteNonQuery();

			sql = @"
					CREATE INDEX if not exists [IDX_invoice_items1] ON invoice_items ([invoice_id], [item_id])
				   ";
			new SQLiteCommand(sql, conn).ExecuteNonQuery();

			//상품마스터 없이 진행
			//sql = @"
			//		CREATE TABLE if not exists items
			//		(
			//			[item_id] TEXT PRIMARY KEY NOT NULL,
			//			[item_nm] TEXT NOT NULL
			//		)
			//	   ";
			//new SQLiteCommand(sql, conn).ExecuteNonQuery();

		}

	}
}
