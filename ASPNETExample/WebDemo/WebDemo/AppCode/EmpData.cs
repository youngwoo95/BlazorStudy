using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebDemo.AppCode
{
    public class EmpData
    {
        string strCon = "Server=127.0.0.1,1433; Uid=rladyddn258; Pwd=rladyddn!!95; database = ProjectV13;TrustServerCertificate=true;";

        public DataSet GetEmployee(string name)
        {
            DataSet dataSet = new DataSet(); // 데이터베이스 결과가 담길 데이터셋 필드

            SqlConnection conn = new SqlConnection(strCon);
            conn.Open();

            string sql = $"SELECT * FROM Employee where Name = '{name}'";

            // SqlAdapter를 초기화 하기
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            adapter.Fill(dataSet);

            conn.Close();
            
            return dataSet;
        }

        

    }
}