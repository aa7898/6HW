using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace _6HW
{
    public partial class _6HW : System.Web.UI.Page
    {
        string sb = "Data Source=(localdb)\\ProjectsV13;" +
               "Initial Catalog=Test;" +
               "Integrated Security=True;" +
               "Connect Timeout=30;" +
               "Encrypt=False;" +
               "TrustServerCertificate=False;" +
               "ApplicationIntent=ReadWrite;" +
               "MultiSubnetFailover=False;" +
               "User ID=sa; Password=12345";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            SqlConnection db = new SqlConnection(sb);
            SqlCommand se = new SqlCommand("SELECT * FROM Users", db);

            try
            {
                db.Open();
                Label1.Text = "";
                SqlDataReader reader = se.ExecuteReader();
                for (; reader.Read();)
                {
                    //依序印出資料列的欄位
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Response.Write( reader[i] + " ");
                    }
                    Response.Write( "<br /><br />");
                }

                db.Close();
            }
            catch (Exception exc) {
                Response.Write(exc.ToString());
            }

        }

        protected void btn_Del_Click(object sender, EventArgs e)
        {
            SqlConnection db = new SqlConnection(sb);
            SqlCommand de = new SqlCommand("DELETE FROM Users WHERE Name = @Name", db);
            de.Parameters.AddWithValue("@Name", tb_Name.Text);
            try
            {
                db.Open();
                int del = de.ExecuteNonQuery();
                //名字可能重複
                if (del != 0)
                {
                    Response.Redirect("6HW.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Label1.Text="請輸入名字!!";
                }

                db.Close();
            }
            catch (Exception exc)
            {
                Label1.Text = exc.ToString();
            }
        }
    }
}