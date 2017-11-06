using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
//using System.Data;
//using System.Data.SQLite;
//using SQLite;

namespace StarCapturePlan
{

//    class mysqlite
//    {

//        string datasource;

//        System.Data.SQLite.SQLiteConnection conn;//数据库连接

//        System.Data.SQLite.SQLiteCommand cmd;//SQL命令

//        string sql;

//        System.Data.SQLite.SQLiteDataReader reader;


//        public static ObservableCollection<string> numberCollection= new ObservableCollection<string>();

//        public mysqlite()
//        {
            
//            datasource = "h:/test.db";

////创建一个数据库文件
//            System.Data.SQLite.SQLiteConnection.CreateFile(datasource);

////连接数据库
//            conn = new System.Data.SQLite.SQLiteConnection();
//            System.Data.SQLite.SQLiteConnectionStringBuilder connstr = new System.Data.SQLite.SQLiteConnectionStringBuilder();
//            connstr.DataSource = datasource;
//            //connstr.Password = "admin";//设置密码，SQLite ADO.NET实现了数据库密码保护
//            conn.ConnectionString = connstr.ToString();
//            conn.Open();
//        }


//        public void creatTable()
//        {
////创建表
//            cmd = new System.Data.SQLite.SQLiteCommand();//实例化SQL命令  
//            sql = "CREATE TABLE test(id int(10))";//列的数据类型是varchar，最大长度为20字符
//            cmd.CommandText = sql;//设置带参SQL语句  
//            cmd.Connection = conn;
//            cmd.ExecuteNonQuery();//执行查询  
//        }


//        public void insert(int a)
//        {
////插入数据
//            sql = "INSERT INTO test VALUES("+a+")";
//            cmd.CommandText = sql;//设置带参SQL语句  
//            cmd.ExecuteNonQuery();//执行查询  
//        }


//        public void read()
//        {
////取出数据
//            sql = "SELECT * FROM test";
//            cmd.CommandText = sql;//设置带参SQL语句  
//            reader = cmd.ExecuteReader();
//            StringBuilder sb = new StringBuilder();
//            while (reader.Read())
//            {
//                sb.Append("id:").Append(reader.GetString(0)).Append("\n");
//                //.Append("password:").Append(reader.GetString(1));
//                numberCollection.Add(sb.ToString());
//            }
//            // MessageBox.Show(sb.ToString());
//        }
   
//            //string dbPath = Environment.CurrentDirectory + "/test.db";//指定数据库路径  

//            //using (SQLiteConnection conn = new SQLiteConnection("Data Source =" + dbPath))//创建连接  
//            //{
//            //    conn.Open();//打开连接  
//            //    using (SQLiteTransaction tran = conn.BeginTransaction())//实例化一个事务  
//            //    {
//            //        for (int i = 0; i < 100000; i++)
//            //        {
//            //            SQLiteCommand cmd = new SQLiteCommand(conn);//实例化SQL命令  
//            //            cmd.Transaction = tran;
//            //            cmd.CommandText = "insert into student values(@id, @name, @sex)";//设置带参SQL语句  
//            //            cmd.Parameters.AddRange(new[] {//添加参数  
//            //               new SQLiteParameter("@id", i),  
//            //               new SQLiteParameter("@name", "中国人"),  
//            //               new SQLiteParameter("@sex", "男")  
//            //           });
//            //           cmd.ExecuteNonQuery();//执行查询  
//            //        }
//            //        tran.Commit();//提交  
//            //    }
//            //}  

//        //void read()//读取数据
//        //{
//        //    SQLiteConnection conn = null;

//        //    string dbPath = "Data Source =" + Environment.CurrentDirectory + "/test.db";
//        //    conn = new SQLiteConnection(dbPath);//创建数据库实例，指定文件位置  
//        //    conn.Open();//打开数据库，若文件不存在会自动创建  

//        //    string sql = "select * from student";
//        //    SQLiteCommand cmdQ = new SQLiteCommand(sql, conn);

//        //    SQLiteDataReader reader = cmdQ.ExecuteReader();

//        //    while (reader.Read())
//        //    {
//        //        Console.WriteLine(reader.GetInt32(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
//        //    }
//        //    conn.Close();

//        //    Console.ReadKey();  
//        //}

//        //private async Task<bool> CheckDbAsync(string dbName)//检查database file是否存在
//        //{
//        //    bool dbExist = true;

//        //    try
//        //    {
//        //        StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync();
//        //    }
//        //    catch (Exception)
//        //    {
//        //        dbExist = false;
//        //    }
//        //    return dbExist;
//        //}
//    }
}
