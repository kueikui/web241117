using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Web.UI.HtmlControls;


namespace web_1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cAccount"] == null && Session["homeAccount"] == null)
            {
                Response.Redirect("Login.aspx");
            }


            if (Session["ShowPanel"] != null && (bool)Session["ShowPanel"] == true)
            {
                Panel1.Visible = true;
                PlaceText.Visible = true;
                PlaceText.Text= Session["AlertLocation"].ToString();
            }
            if (!IsPostBack)
            {
                if (Session["LoginType"] != null)
                {
                    if (Session["LoginType"].ToString() == "System")
                    {
                        Image1.ImageUrl = "~/Images/螢幕擷取畫面 2024-10-11 132451.png";
                        Button1.Visible = false;
                        Button7.Visible = false;
                    }
                    else if (Session["LoginType"].ToString() == "Home")
                    {
                        Image1.ImageUrl = "~/Images/螢幕擷取畫面 2024-10-11 132451.png";
                        Button14.Visible = false;
                        Button15.Visible = false;
                    }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "玄關";
            ClientScript.RegisterStartupScript(this.GetType(), "SetIframeSrc", "document.querySelector('iframe').src='https://tcumicarewatchsafeguard.me/0';", true);
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Label1.Text = "客廳";
            ClientScript.RegisterStartupScript(this.GetType(), "SetIframeSrc", "document.querySelector('iframe').src='https://tcumicarewatchsafeguard.me/1';", true);
        }
        protected void Button14_Click(object sender, EventArgs e)
        {
            Label1.Text = "大廳 ";
            ClientScript.RegisterStartupScript(this.GetType(), "SetIframeSrc", "document.querySelector('iframe').src='https://tcumicarewatchsafeguard.me/0';", true);
        }
        protected void Button15_Click(object sender, EventArgs e)
        {
            Label1.Text = "交誼廳 ";
            ClientScript.RegisterStartupScript(this.GetType(), "SetIframeSrc", "document.querySelector('iframe').src='https://tcumicarewatchsafeguard.me/1';", true);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Label1.Text = "廁所 ";
            //ClientScript.RegisterStartupScript(this.GetType(), "SetIframeSrc", "document.querySelector('iframe').src='https://tcumicarewatchsafeguard.me/1';", true);
        }
        protected async void Button11_Click(object sender, EventArgs e) // 玄關
        {
            Button1.CssClass += " btn-twinke";
            Button14.CssClass += " btn-twinke";

            if (Session["LoginType"].ToString() == "System")
            {
                Panel1.Visible = true;
                Session["ShowPanel"] = true;
                PlaceText.Text = "大廳";
                string message = "警報：大廳發生跌倒事件，請立即處理。";
                FileStream file = File.OpenRead(@"C:\Users\user\Desktop\web240703\web-1\Images\help.png");
                await SendLineNotify(message, file);

                string location = "大廳";
                Session["AlertLocation"] = location;
                Session["IsFlashing"] = true;

                // 使用 ScriptManager 來觸發 Button14 的閃爍效果
                ScriptManager.RegisterStartupScript(this, GetType(), "startTwinklingButton14",
                    "document.getElementById('" + Button14.ClientID + "').classList.add('btn-twinke');", true);
            }
            else if (Session["LoginType"].ToString() == "Home")
            {
                Panel1.Visible = true;
                PlaceText.Text = "玄關";
                string message = "警報：玄關發生跌倒事件，請立即處理。";
                FileStream file = File.OpenRead(@"C:\Users\user\Desktop\web240703\web-1\Images\help.png");
                await SendLineNotify(message, file);

                string location = "玄關";
                Session["AlertLocation"] = location;
                Session["IsFlashing"] = true;

                ScriptManager.RegisterStartupScript(this, GetType(), "startTwinklingButton1",
                    "document.getElementById('" + Button1.ClientID + "').classList.add('btn-twinke');", true);
            }
        }

        protected async void Button13_Click(object sender, EventArgs e)//客廳
        {
            
            Button7.CssClass += " btn-twinke";
            Button15.CssClass += " btn-twinke";
            if (Session["LoginType"].ToString() == "System")
            {
                Panel1.Visible = true;
                Session["ShowPanel"] = true;
                PlaceText.Text = "交誼廳";
                string message = "警報：交誼廳發生跌倒事件，請立即處理。";
                FileStream file = File.OpenRead(@"C:\Users\user\Desktop\web240703\web-1\Images\help.png");
                await SendLineNotify(message, file);
                string location = "交誼廳";
                Session["AlertLocation"] = location;
                Session["IsFlashing"] = true; // 假設你有閃爍效果
                string script = $"triggerAlert('{location}');";
                ScriptManager.RegisterStartupScript(this, GetType(), " startTwinkling", script, true);

            }
            else if (Session["LoginType"].ToString() == "Home")
            {
                Panel1.Visible = true;
                PlaceText.Text = "客廳";
                string message = "警報：客廳發生跌倒事件，請立即處理。";
                FileStream file = File.OpenRead(@"C:\Users\user\Desktop\web240703\web-1\Images\help.png");//未命名的影片_ 使用 Clipchamp 製作 (1).mp4
                await SendLineNotify(message, file);
                string location = "客廳";
                Session["AlertLocation"] = location;

                //Panel1.Controls.Clear();
                //Panel1.Controls.Add(new LiteralControl($"在{PlaceText.Text} 發生跌倒事件<br/>請立即派人前往救助"));

            }
        }

        protected async void Button5_Click(object sender, EventArgs e)
        {
            Button3.CssClass += " btn-twinke";
            //Button15.CssClass += " btn-twinke";

            Panel1.Visible = true;
            Session["ShowPanel"] = true;
            PlaceText.Text = "廁所";
            string message = "警報：廁所發生跌倒事件，請立即處理。";
            FileStream file = File.OpenRead(@"C:\Users\user\Desktop\web240703\web-1\Images\help.png");
            await SendLineNotify(message, file);
            string location = "廁所";
            Session["AlertLocation"] = location;
            Session["IsFlashing"] = true; // 假設你有閃爍效果


            ScriptManager.RegisterStartupScript(this, GetType(), "startTwinklingButton1",
                "document.getElementById('" + Button3.ClientID + "').classList.add('btn-twinke');", true);

        }
        private async Task SendLineNotify(string message, FileStream file)
        {
            string token = "6Oxe8CZCWSWkMWsnrXgmNP2Mxed5WbyN9amXlzniM5m"; // 您的 LINE Notify 權杖
            string url = "https://notify-api.line.me/api/notify";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                using (var form = new MultipartFormDataContent())
                {
                    // 添加訊息
                    form.Add(new StringContent(message), "message");

                    // 添加圖片 (如果有)
                    if (file != Stream.Null)
                    {
                        var streamContent = new StreamContent(file);
                        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                        form.Add(streamContent, "imageFile", "help.png");
                    }

                    // 發送請求
                    HttpResponseMessage response = await client.PostAsync(url, form);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("LINE Notify 發送失敗：" + response.StatusCode);
                    }
                }
            }
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Session["ShowPanel"] = false;
            Button1.CssClass = Button1.CssClass.Replace("btn-twinke", "").Trim();
            Button7.CssClass = Button7.CssClass.Replace("btn-twinke", "").Trim();
            Button14.CssClass = Button14.CssClass.Replace("btn-twinke", "").Trim();
            Button15.CssClass = Button15.CssClass.Replace("btn-twinke", "").Trim();
            // 執行 JavaScript 移除所有 'btn-twinke' class
            ScriptManager.RegisterStartupScript(this, GetType(), "removeTwinkle",
                "document.querySelectorAll('.btn-twinke').forEach(el => el.classList.remove('btn-twinke'));", true);
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
        }
        protected void Button10_Click(object sender, EventArgs e)
        {
        }
        protected void Button16_Click(object sender, EventArgs e)
        {
        }
        protected void Button17_Click(object sender, EventArgs e)
        {
        }
        protected void Button18_Click(object sender, EventArgs e)
        {
        }
        protected void Button19_Click(object sender, EventArgs e)
        {
        }
    }
}