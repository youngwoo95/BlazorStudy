using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebDemo.AppCode;

namespace WebDemo
{
    public partial class subPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            EmpData empData = new EmpData();
            
            DataSet dataSet = empData.GetEmployee(txtName.Text);

            GridView1.DataSource = dataSet.Tables[0];
            
            GridView1.DataBind(); // 이걸 한번 더 호출해줘야함.

            txtName.Text = null;
            
        }
    }
}