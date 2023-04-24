using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EmployeeRegForm1.Models;

namespace EmployeeRegForm1
{
    public partial class frm_Employee : System.Web.UI.Page
    {
        ClsClass cmncls = new ClsClass();
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlConnection con;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDllRole();
            }
            FillUser();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int roleid = Convert.ToInt32(ddlDsgnID.Text.Trim());
                //int rd = int.Parse(ddlDsgnID.SelectedValue.Trim());
                string FName = TxtFrstName.Text.Trim();
                string LName = TxtlstName.Text.Trim();
                string contact = TxtContact.Text.Trim();
                string EmailId = TxtEmail.Text.Trim();

                if (string.IsNullOrEmpty(FName))
                {
                    lblMSG.Text = "Enter First Name.";
                    TxtFrstName.Focus();
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                }
                else if (string.IsNullOrEmpty(LName))
                {
                    lblMSG.Text = "Enter Last Name.";
                    TxtlstName.Focus();
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                }
                else if (string.IsNullOrEmpty(contact))
                {
                    lblMSG.Text = "Enter Contact number.";
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                    TxtContact.Focus();
                }
                else if (string.IsNullOrEmpty(EmailId))
                {
                    lblMSG.Text = "Enter Email Id.";
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                    TxtEmail.Focus();
                }
                else
                {
                    con = cmncls.ClsConn();
                    cmd = new SqlCommand(" insert into EmployeeRegData values('" + FName + "','" + LName + "','" + EmailId + "','" + contact + "'," + roleid + ")", con);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int n = cmd.ExecuteNonQuery();
                    con.Close();
                    if (n > 0)
                    {
                        FillUser();
                        Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success', 'Record Save Successfully.', 'Success');", true);
                        //lblMSG.ForeColor = System.Drawing.Color.LimeGreen;
                        //lblMSG.Text = "Record Save Successfully.";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Error', 'Record Not Save.', 'warning');", true);
                        //lblMSG.ForeColor = System.Drawing.Color.Red;
                        //lblMSG.Text = "Record Not Save.";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        void Clear()
        {
            TxtFrstName.Text = string.Empty;
            TxtlstName.Text = string.Empty;
            TxtContact.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            ddlDsgnID.ClearSelection();
            BtnUpdate.Visible = false;
            BtnSave.Visible = true;
        }
        void FillDllRole()
        {
            try
            {
                con = cmncls.ClsConn();
                da = new SqlDataAdapter("select * from DesignationTbl order by DepartmentId ", con);
                dt = new DataTable();
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlDsgnID.DataSource = dt;
                    ddlDsgnID.DataValueField = "DepartmentId";
                    ddlDsgnID.DataTextField = "DepartmentDesc";

                    ddlDsgnID.DataBind();
                    ddlDsgnID.Items.Insert(0, new ListItem("-Select Designation-", "0"));
                }
                else
                {
                    lblMSG.Text = "Fail";
                }
            }
            catch (Exception ex)
            {
                lblMSG.Text = "Some Technical Error";
            }
        }
        internal void FillUser()
        {
            try
            {
                con = cmncls.ClsConn();
                da = new SqlDataAdapter("select emp.EmployeeID,emp.EmployeeFirstName,emp.EmployeeLastName,emp.DesignationId,emp.EmailId,emp.MobileNo,d.DepartmentDesc as DepartmentDesc from EmployeeRegData emp INNER JOIN DesignationTbl d on d.DepartmentId = emp.DesignationId  order by emp.EmployeeID", con);
                dt = new DataTable();
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;//to get data from database
                    GridView1.DataBind();

                    //Required for jQuery DataTables to work.
                    GridView1.UseAccessibleHeader = true;
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    lblMSG.Text = "Data Not Found";
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                lblMSG.Text = "Due To Some System Error. Please Call Administrator..";
                lblMSG.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btEdit = sender as LinkButton;
                lbl_userId.Text = (btEdit.NamingContainer.FindControl("lblid") as Label).Text.Trim();
                TxtFrstName.Text = (btEdit.NamingContainer.FindControl("lblFname") as Label).Text.Trim();
                TxtlstName.Text = (btEdit.NamingContainer.FindControl("lblLastname") as Label).Text.Trim();
                TxtContact.Text = (btEdit.NamingContainer.FindControl("lblMobileNo") as Label).Text.Trim();
                TxtEmail.Text = (btEdit.NamingContainer.FindControl("lblEmailId") as Label).Text.Trim();
                ddlDsgnID.SelectedValue = (btEdit.NamingContainer.FindControl("lblDesignationid") as Label).Text.Trim();
                BtnSave.Visible = false;
                BtnUpdate.Visible = true;
            }
            catch (Exception ex)
            {
                lblMSG.ForeColor = System.Drawing.Color.Red;
                lblMSG.Text = "Sorry Record Not Valid Field.";
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btndelete = sender as LinkButton;
                int uid = Convert.ToInt32((btndelete.NamingContainer.FindControl("lblid") as Label).Text.Trim());
                con = cmncls.ClsConn();
                cmd = new SqlCommand("delete EmployeeRegData where EmployeeID='" + uid + "' ", con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int n = cmd.ExecuteNonQuery();
                con.Close();
                if (n > 0)
                {
                    FillUser();
                    lblMSG.ForeColor = System.Drawing.Color.LimeGreen;
                    lblMSG.Text = "Record Delete Successfully.";
                }
                else
                {
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                    lblMSG.Text = "Record Not Delete.";
                }
            }
            catch (Exception ex)
            {
                lblMSG.ForeColor = System.Drawing.Color.Red;
                lblMSG.Text = "Some Technical Error.";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int roleid = Convert.ToInt32(ddlDsgnID.Text.Trim());
                int uid = Convert.ToInt32(lbl_userId.Text.Trim());
                string FName = TxtFrstName.Text.Trim();
                string LName = TxtlstName.Text.Trim();
                string contact = TxtContact.Text.Trim();
                string EmailId = TxtEmail.Text.Trim();

                if (string.IsNullOrEmpty(FName))
                {
                    lblMSG.Text = "Enter First Name.";
                    TxtFrstName.Focus();
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                }
                else if (string.IsNullOrEmpty(LName))
                {
                    lblMSG.Text = "Enter Last Name.";
                    TxtlstName.Focus();
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                }
                else if (string.IsNullOrEmpty(contact))
                {
                    lblMSG.Text = "Enter Contact number.";
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                    TxtContact.Focus();
                }
                else if (string.IsNullOrEmpty(EmailId))
                {
                    lblMSG.Text = "Enter Email Id.";
                    lblMSG.ForeColor = System.Drawing.Color.Red;
                    TxtEmail.Focus();
                }
                else
                {
                    con = cmncls.ClsConn();
                    cmd = new SqlCommand(" update EmployeeRegData set EmployeeFirstName='" + FName + "',EmployeeLastName='" + LName + "',EmailId='" + EmailId + "',MobileNo='" + contact + "',DesignationId='" + roleid + "' where EmployeeID='" + uid + "' ", con);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int n = cmd.ExecuteNonQuery();
                    con.Close();
                    if (n > 0)
                    {
                        FillUser();
                        Clear();
                        BtnSave.Visible = true;
                        BtnUpdate.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "swal('Success', 'Record Update Successfully.', 'Success');", true);

                        //lblMSG.ForeColor = System.Drawing.Color.LimeGreen;
                        //lblMSG.Text = "Record Update Successfully.";
                    }
                    else
                    {
                        lblMSG.ForeColor = System.Drawing.Color.Red;
                        lblMSG.Text = "Record Not Update.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMSG.ForeColor = System.Drawing.Color.Red;
                lblMSG.Text = "Some Technical Error.";
            }
        }
    }
}