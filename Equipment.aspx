<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Equipment.aspx.cs" Inherits="web_1.Equipment" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-image: url(../Images/b-4.jpg);
            background-size: cover;
        }
        .gridview tr:hover {
            background-color: #ccffff; /* 懸停時的背景顏色 */
            color: white; /* 懸停時的文字顏色 */
        }
        .panel-common {
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            padding: 10px;
            min-height:300px;
        }
        .gridview-container {
            align-items: center;    /* 水平置中 */
            justify-content: center;
            padding:30px;
            /*height: 100vh;  可選，設定容器高度來讓GridView完全置中 */
        }
        .gridview{
              margin: 0 auto; /* 水平置中 */
        }
         /* 按钮样式 */
        .button {
            padding: 5px 10px;
            margin: 5px;
            border-width: 1px;
            color: white;
            border-radius: 3px;
            background-color: #006699;
        }
    </style>
    <main aria-labelledby="title">
            <h1>設備</h1>
        <asp:Panel ID="Panel1" CssClass="panel-common" runat="server" BackColor="PowderBlue">
            <div class="gridview-container">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="button" Text="新增設備" />
                <br />
                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="gridview" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="View" Text="查看畫面" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                </div>
        </asp:Panel>
        <br />
    <asp:Panel ID="Panel2"  style="height: 150px" CssClass="panel-common" runat="server" BackColor="PowderBlue" Visible="False">
        設 備 ID:<asp:TextBox ID="EqID_text" runat="server"></asp:TextBox>
        <br />
        設備名稱:<asp:TextBox ID="EqName_text" runat="server"></asp:TextBox>
        <br />
        設備產年:<asp:TextBox ID="EqYear_text" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        裝設地點:<asp:DropDownList ID="PlaceDropDown" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="新增" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="取消" />
    </asp:Panel>
    </main>
</asp:Content>
