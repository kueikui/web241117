using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web_1
{
    public partial class Equipment : System.Web.UI.Page
    {
        private static string connectionString = "server=203.64.84.154;database=care;uid=root;password=Topic@2024;port=33061";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cAccount"] == null && Session["homeAccount"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                BindGridView();
                BindPlaceDropDown();
            }
        }

        // 綁定 GridView
        private void BindGridView()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                SELECT Equipment.EqId, Equipment.EqModel, Equipment.EqRepair, Place.pName 
                FROM Equipment 
                LEFT JOIN Place ON Equipment.pId = Place.pId";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

        // 綁定 DropDownList
        private void BindPlaceDropDown()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT pId, pName FROM Place";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                PlaceDropDown.DataSource = reader;
                PlaceDropDown.DataTextField = "pName";
                PlaceDropDown.DataValueField = "pId";
                PlaceDropDown.DataBind();
            }
        }

        // 顯示新增設備表單
        protected void Button1_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
        }

        // 新增設備
        protected void Button2_Click(object sender, EventArgs e)
        {
            string eqId = EqID_text.Text.Trim();
            string eqModel = EqName_text.Text.Trim();
            string eqRepair = EqYear_text.Text.Trim();
            string placeId = PlaceDropDown.SelectedValue;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Equipment (EqId, EqModel, EqRepair, pId) VALUES (@EqId, @EqModel, @EqRepair, @pId)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EqId", eqId);
                    command.Parameters.AddWithValue("@EqModel", eqModel);
                    command.Parameters.AddWithValue("@EqRepair", eqRepair);
                    command.Parameters.AddWithValue("@pId", placeId);
                    command.ExecuteNonQuery();
                    Response.Write("<script>alert('設備新增成功！');</script>");
                    BindGridView();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('新增失敗：" + ex.Message + "');</script>");
                }
                finally
                {
                    Panel2.Visible = false;
                    ClearFields();
                }
            }
        }

        // 清空輸入欄位
        private void ClearFields()
        {
            EqID_text.Text = "";
            EqName_text.Text = "";
            EqYear_text.Text = "";
            PlaceDropDown.SelectedIndex = 0;
        }

        // 取消新增
        protected void Button3_Click(object sender, EventArgs e)
        {
            Panel2.Visible = false;
            ClearFields();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = GridView1.Rows[index];
                string eqId = selectedRow.Cells[0].Text;
                Response.Redirect($"WebForm1.aspx?EqId={eqId}");
            }
        }
    }
}
