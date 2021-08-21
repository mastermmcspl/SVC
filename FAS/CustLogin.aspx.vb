Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Net
Imports System.Web
Imports System.Diagnostics
Imports System.Net.Dns
Imports System.Security.Cryptography
Imports System.Xml
Imports System.Net.Mail
Imports System.Data
Public Class CustLogin
    Inherits System.Web.UI.Page
    Private Shared sSession As AllSession
    Private objclsServerConnection As New clsServerDB
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnLogin.ImageUrl = "Images/Login_Button.png"
        Me.Form.DefaultButton = Me.imgbtnLogin.UniqueID
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnLogin_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnLogin.Click
        Dim sPassword As String
        Dim objclsLogin As New CustclsLogin
        Dim iValidUserID As Integer, iUserID As Integer
        Try
            iValidUserID = objclsLogin.CheckValidLoginUserName(txtUserName.Text.Trim)

            If iValidUserID > 0 Then
                Dim sIPAddress As String = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
                sPassword = objclsLogin.EncryptPassword(txtPassword.Text.Trim)
                iUserID = objclsLogin.GetLoginUserID(txtUserName.Text.Trim, sPassword)
                If iUserID > 0 Then
                    objclsLogin.AuditLogin(iUserID, sIPAddress)
                    objclsLogin.UpdateLoginDetails(iUserID, sIPAddress)
                    'Response.Redirect("test1.aspx")
                    Response.Redirect("~/CustomerMaster/ServerDetails.aspx")
                Else
                    objclsLogin.UnSuccusfullAttemptUpdate(txtUserName.Text)
                    lblValidationMsg.Text = "Invalid Login Details"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                    txtUserName.Text = ""
                End If
            ElseIf iUserID = 0 Then
                lblValidationMsg.Text = "Invalid Login Details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                txtUserName.Text = ""
            End If
        Catch
            If txtUserName.Text = "admin" Or txtUserName.Text = "Admin" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            Else
                lblValidationMsg.Text = "Invalid Login Details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
            End If
            'If IsDBNull(iValidUserID) = False Then
            '    If txtUserName.Text = "admin" And txtPassword.Text = "mmcs@456" Then
            '        Response.Redirect("~/CustomerMaster/ServerDetails.aspx")
            '    Else
            '        lblValidationMsg.Text = "Invalid Login Details"
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
            '        txtUserName.Text = ""
            '    End If
            'End If
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Dim sPath As String = "", sPath2 As String = "", sPath3 As String = ""
        Dim sConnectionstring As String = ""
        Try
            sPath = Server.MapPath("~\Script\ServerDetails\Script.txt")
            sPath2 = Server.MapPath("~\Script\ServerDetails\Script2.txt")
            sPath3 = Server.MapPath("~\Script\ServerDetails\Script3.txt")
            objclsServerConnection.createDatabase(txtServerName.Text, txtLogin.Text, txtsPassword.Text, "MMCSServer")
            objclsServerConnection.createTables(txtServerName.Text, txtLogin.Text, txtsPassword.Text, "MMCSServer", sPath)
            objclsServerConnection.createTablesForSP(txtServerName.Text, txtLogin.Text, txtsPassword.Text, "MMCSServer", sPath2)
            objclsServerConnection.createTables(txtServerName.Text, txtLogin.Text, txtsPassword.Text, "MMCSServer", sPath3)
            sConnectionstring = AccessConnection()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfull,Please Login again','', 'success');", True)
            lblValidationMsg.Text = "Successfull,Please Login again"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
            txtLogin.Text = "" : txtsPassword.Text = ""
            'Response.Redirect("~/CustLogin.aspx")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function AccessConnection() As String
        Try
            Dim webConfigFile As String = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config")
            'Dim webConfigFile As String = "C:\inetpub\wwwroot\FASPro\FAS\\web.config"
            Dim isNew As Boolean = False

            Dim doc As New XmlDocument()
            doc.Load(webConfigFile)
            Dim list As XmlNodeList = doc.DocumentElement.SelectNodes(String.Format("appSettings/add[@key='{0}']", "MMCSPL"))

            Dim node As XmlNode
            isNew = list.Count = 0
            If isNew Then
                node = doc.CreateNode(XmlNodeType.Element, "add", Nothing)
                Dim attribute As XmlAttribute = doc.CreateAttribute("key")
                attribute.Value = "MMCSPL"
                node.Attributes.Append(attribute)
                attribute = doc.CreateAttribute("value")
                attribute.Value = objclsServerConnection.CreatConnection(txtServerName.Text, txtLogin.Text, txtsPassword.Text, "MMCSServer")
                node.Attributes.Append(attribute)
            Else
                node = list(0)
            End If
            Dim conString As String = node.Attributes("value").Value

            If isNew Then
                doc.DocumentElement.SelectNodes("appSettings")(0).AppendChild(node)
            End If
            doc.Save(webConfigFile)
            Return conString
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
