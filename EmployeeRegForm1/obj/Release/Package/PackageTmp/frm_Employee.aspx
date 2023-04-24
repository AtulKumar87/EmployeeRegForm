<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_Employee.aspx.cs" Inherits="EmployeeRegForm1.frm_Employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.js"></script>
    <%--CDN : Data Table--%>
    <link href="//cdn.datatables.net/1.11.5/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="//cdn.datatables.net/1.11.5/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <script>
        $(document).ready(function () {
            DisplayUser();
        });
        function DisplayUser() {
            $(".Grid1").DataTable(
                {
                    bLengthChange: true,
                    lengthMenu: [[5, 10, -1], [5, 10, "All"]],
                    bFilter: true,
                    bSort: true,
                    bPaginate: true
                });
        }
    </script>
    <script>
        function validateForm() {
            var regexPhone = /[6-9][0-9]{9}/;
            var regexEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;

            var phone = $('#<% =TxtContact.ClientID %>').val().trim();
            var email = $('#<% =TxtEmail.ClientID %>').val().trim();
            var Fname = $('#<% =TxtFrstName.ClientID %>').val().trim();
            var Lname = $('#<% =TxtlstName.ClientID %>').val().trim();

            if (Fname == "" || Fname == undefined) {
                swal("Username", "Please enter First Name", "warning");
                return false;
            }
            else if (Lname == "" || Lname == undefined) {
                swal("Username", "Please enter Last Name", "warning");
                return false;
            }
            else if ($('#<%=ddlDsgnID.ClientID%>').val() == "0") {
                swal("Role", "Please select Designation", "warning");
                return false;
            }
            else if (regexEmail.test(email) == false) {
                swal("Email", "Please enter a valid email address.", "warning");
                return false;
            }
            else if ($('#<%=TxtContact.ClientID %>').val() == '') {
                swal("Contact", " Please Enter Mobile no..!!", "warning");
                return false;
            }
            else if (regexPhone.test(phone) == false) {
                swal("Contact", " Please Enter 10-Digit Valid Mobile no..!!", "warning");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="row bg-info">
                    <div class="col-sm-12">
                        <h3 class="text-center">User Registration</h3>
                    </div>
                </div>
                <p>
                    <asp:Label ID="lblMSG" CssClass="text-center" runat="server" Text=""></asp:Label>
                </p>
                <asp:Label runat="server" Visible="false" ID="lbl_userId"></asp:Label>
                <div class="col-sm-4">
                    <asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label>&nbsp;&nbsp;
                     <asp:TextBox ID="TxtFrstName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>&nbsp;&nbsp;
                    <asp:TextBox ID="TxtlstName" runat="server" CssClass="form-control"></asp:TextBox>

                </div>
                <div class="col-sm-4">
                    <asp:Label ID="Label4" runat="server" Text="Designation"></asp:Label>
                    &nbsp;&nbsp;                     
                <asp:DropDownList ID="ddlDsgnID" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <asp:Label ID="Label3" runat="server" Text="Email Address"></asp:Label>&nbsp;&nbsp;
                     <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:Label ID="Label5" runat="server" Text="Mobile No"></asp:Label>&nbsp;&nbsp;
                     <asp:TextBox ID="TxtContact" runat="server" CssClass="form-control numberonly" MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <div class="row py-4">
                        <div class="col-sm-6">
                            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn btn-success btn-block px-5" OnClientClick="return  validateForm();" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" Text="Update" OnClientClick="return  validateForm();" CssClass="btn btn-success btn-block px-5" Visible="false" />
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-danger btn-block px-5" OnClick="BtnClear_Click" />
                        </div>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="Grid1 display" AutoGenerateColumns="false" DataKeyNames="EmployeeID">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblid" runat="server" Text='<%#Eval("EmployeeID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFname" runat="server" Text='<%#Eval("EmployeeFirstName") %> '></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastname" runat="server" Text='<%#Eval("EmployeeLastName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile No">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DesignationId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesignationid" runat="server" Text='<%#Eval("DesignationId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Role">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesignationDesc" runat="server" Text='<%#Eval("DepartmentDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>

                                    <asp:LinkButton ID="BtnEdit" OnClick="BtnEdit_Click" CausesValidation="false" runat="server" CssClass="btn btn-sm btn-primary "><i class='fa fa-edit'> Edit</i></asp:LinkButton>

                                    <asp:LinkButton ID="BtnDelete" CausesValidation="false" OnClick="BtnDelete_Click" runat="server" CssClass="btn btn-sm btn-danger"
                                        OnClientClick="return confirm('Do you really want to delete this record.')">
                               <i class='fa fa-trash-o'> Delete</i></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
