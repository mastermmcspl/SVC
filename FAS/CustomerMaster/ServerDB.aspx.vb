Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Net
Imports System.Web
Imports System.Diagnostics
Imports System.Net.Dns
Imports System.Security.Cryptography
Imports System.Configuration
Imports System.Xml
Imports System.Net.Mail
Imports System.Data

Public Class CustomerMaster_ServerDB
    Inherits System.Web.UI.Page
    Private objclsServerConnection As New clsServerDB
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnConnect.ImageUrl = "~/Images/Connect.png"
        imgbtnCreate.ImageUrl = "~/Images/CreateDB24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

                LoadProductDetails()
                imgbtnCreate.Visible = False
                RFVServername.ControlToValidate = "txtServerName" : RFVServername.ErrorMessage = "Enter Server Name."
                RFVLogin.ControlToValidate = "txtLogin" : RFVLogin.ErrorMessage = "Enter Login Name."
                RFVPassword.ControlToValidate = "txtPassword" : RFVPassword.ErrorMessage = "Enter Password." : RFVPassword.ValidationGroup = "Validate"

                RFVProduct.ErrorMessage = "Select Product." : RFVProduct.InitialValue = "0"
                RFVDatabase.ControlToValidate = "txtDatabase" : RFVDatabase.ErrorMessage = "Enter Database Name."
                RFVAccessCode.ControlToValidate = "txtAccessCode" : RFVAccessCode.ErrorMessage = "Enter Access Code."
                RFVCompanyName.ControlToValidate = "txtCompanyName" : RFVCompanyName.ErrorMessage = "Enter Company Name."
                ddlProduct.Enabled = False : txtDatabase.Enabled = False : txtAccessCode.Enabled = False : txtCompanyName.Enabled = False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub imgbtnConnect_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnConnect.Click
        Dim conn As New SqlClient.SqlConnection
        Try
            conn.ConnectionString = "Data Source=" & Trim(txtServername.Text) & ";Persist Security Info=True;User ID=" & Trim(txtLogin.Text) & ";Password=" & Trim(txtPassword.Text) & " "
            conn.Open()
            txtAlterPassword.Text = txtPassword.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Connected Successfully','', 'success');", True)
            ddlProduct.Enabled = True : imgbtnCreate.Visible = True : txtDatabase.Enabled = True : txtAccessCode.Enabled = False : txtCompanyName.Enabled = False
            txtServername.Enabled = False : txtLogin.Enabled = False : txtPassword.Enabled = False : imgbtnConnect.Visible = False
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Failed to connect to the data source.','', 'warning');", True)
        Finally
            conn.Close()
        End Try
    End Sub

    Protected Sub imgbtnCreate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCreate.Click
        Dim sPath As String = "", sPath2 As String = "", sPath3 As String = ""
        Dim sConnectionstring As String = ""
        Try
            'If ddlProduct.SelectedIndex = 1 Then  'Trace PA
            '    sPath = Server.MapPath("~\Script\TRACe\Script.txt")
            'ElseIf ddlProduct.SelectedIndex = 2 Then  'TRACe Enterprise
            '    sPath = Server.MapPath("~\Script\TRACe\Script.txt")
            'ElseIf ddlProduct.SelectedIndex = 3 Then  'EDICT
            '    sPath = Server.MapPath("~\Script\EDICT\Script.txt")
            'ElseIf ddlProduct.SelectedIndex = 4 Then  'FAS Pro

            sPath = Server.MapPath("~\Script\FAS\Script.txt")
            sPath2 = Server.MapPath("~\Script\FAS\Script2.txt")
            sPath3 = Server.MapPath("~\Script\FAS\Script3.txt")
            ' End If

            imgbtnConnect.Visible = False
            objclsServerConnection.createDatabase(txtServername.Text, txtLogin.Text, txtAlterPassword.Text, txtDatabase.Text)
            objclsServerConnection.createTables(txtServername.Text, txtLogin.Text, txtAlterPassword.Text, txtDatabase.Text, sPath)
            objclsServerConnection.createTablesForSP(txtServername.Text, txtLogin.Text, txtAlterPassword.Text, txtDatabase.Text, sPath2)
            objclsServerConnection.createTables(txtServername.Text, txtLogin.Text, txtAlterPassword.Text, txtDatabase.Text, sPath3)
            sConnectionstring = AccessConnection()
            SaveDBAccess()
            objclsServerConnection.CreateAccessCode(ddlProduct.SelectedIndex, txtAccessCode.Text, txtCompanyName.Text, sConnectionstring)
            ' SendEmail()
            Refresh()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Created.','', 'success');", True)
            imgbtnConnect.Visible = False : imgbtnCreate.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub


    Public Sub LoadProductDetails()
        Try
            'ddlProduct.Items.Add(New ListItem("Select Product", "10"))
            'ddlProduct.Items.Add(New ListItem("TRACe PA", "1"))
            'ddlProduct.Items.Add(New ListItem("TRACe Enterprise", "2"))
            'ddlProduct.Items.Add(New ListItem("EDICT", "3"))
            ddlProduct.Items.Add(New ListItem("FAS Pro", "4"))

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub txtDatabase_TextChanged(sender As Object, e As EventArgs) Handles txtDatabase.TextChanged
        Try
            Dim sServerName As String = "", sUid As String = "", sPassword As String = ""
            Dim errors As New Exception
            Try

                Dim connString As String = "Data Source=" & Trim(txtServername.Text) & ";User ID='" & Trim(txtLogin.Text) & "';pwd='" & Trim(txtAlterPassword.Text) & "';TRUSTED_CONNECTION=NO"

                If objclsServerConnection.CheckDBExists(connString, txtDatabase.Text, errors) = True Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Database Already Exist. Enter New database Name.','', 'info');", True)
                    txtAccessCode.Enabled = False : txtCompanyName.Enabled = False
                Else
                    txtAccessCode.Enabled = True : txtCompanyName.Enabled = True

                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SaveDBAccess()
        Dim objstrDBAccess As New strServerDB
        Try
            objstrDBAccess.sMDA_DatabaseName = txtDatabase.Text
            objstrDBAccess.sMDA_AccessCode = txtAccessCode.Text
            objstrDBAccess.sMDA_CompanyName = txtCompanyName.Text
            objstrDBAccess.sMDA_IPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            objstrDBAccess.iMDA_Application = ddlProduct.SelectedIndex
            objclsServerConnection.SaveCode("MMCSPL", objstrDBAccess)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SendEmail()
        Try
            Dim sBody As String = ""
            Dim Mail As New MailMessage
            Mail.Subject = "Data Base Created."
            Mail.To.Add("vijeth@mmcspl.com")
            Mail.From = New MailAddress("mmcspl736@gmail.com")
            ' Mail.Body = "This is an ownage email using VB.NET"
            sBody = "Dear MMCSPL,"
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & "Database Name -'" + txtDatabase.Text + "'."
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & "Access Code -'" + txtAccessCode.Text + "'."
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & "Company Name -'" + txtCompanyName.Text + "'."
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & "Created on -" & Date.Today & "."
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & "______________________________________________________________________________"
            sBody = sBody & System.Environment.NewLine
            sBody = sBody & System.Environment.NewLine

            sBody = sBody & "This is an automated message. Please do not reply to this. " & System.Environment.NewLine
            Dim body As String = sBody
            Mail.Body = sBody
            Mail.IsBodyHtml = False


            Dim SMTP As New SmtpClient("smtp.gmail.com")
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential("mmcspl736@gmail.com", "mmcs123#")
            SMTP.Port = "587"
            SMTP.Send(Mail)

            'Dim sBody As String = ""
            'Dim [to] As String = "vijeth@mmcspl.com"
            'Dim from As String = "mmcspl736@gmail.com"
            'Dim subject As String = "Data Base Created."

            'sBody = "Dear MMCSPL,"
            'sBody = sBody & System.Environment.NewLine
            'sBody = sBody & System.Environment.NewLine
            'sBody = sBody & "Database Name -'" + txtDatabase.Text + "'."
            'sBody = sBody & System.Environment.NewLine
            'sBody = sBody & "Access Code -'" + txtAccessCode.Text + "'."
            'sBody = sBody & System.Environment.NewLine
            'sBody = sBody & "Company Name -'" + txtCompanyName.Text + "'."
            'sBody = sBody & System.Environment.NewLine
            'sBody = sBody & "Created on -" & Date.Today & "."
            'sBody = sBody & System.Environment.NewLine
            'sBody = sBody & System.Environment.NewLine
            'sBody = sBody & "______________________________________________________________________________"
            'sBody = sBody & System.Environment.NewLine
            'sBody = sBody & System.Environment.NewLine

            'sBody = sBody & "This is an automated message. Please do not reply to this. " & System.Environment.NewLine
            'Dim body As String = sBody
            'Using mm As New MailMessage("mmcspl736@gmail.com", "vijeth@mmcspl.com")
            '    mm.Subject = subject
            '    mm.Body = sBody
            '    mm.IsBodyHtml = False
            '    Dim SMTP As New SmtpClient("smtp.gmail.com")
            '    SMTP.Host = "smtp.gmail.com"
            '    smtp.EnableSsl = True
            '    smtp.Credentials = New System.Net.NetworkCredential("mmcspl736@gmail.com", "mmcs123#")
            '    smtp.UseDefaultCredentials = True
            '    smtp.Port = 587
            '    smtp.Send(mm)

            ClientScript.RegisterStartupScript(Me.GetType, "alert", "alert('Email sent.');", True)
            ' End Using
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
            Dim list As XmlNodeList = doc.DocumentElement.SelectNodes(String.Format("appSettings/add[@key='{0}']", txtAccessCode.Text))

            Dim node As XmlNode
            isNew = list.Count = 0
            If isNew Then
                node = doc.CreateNode(XmlNodeType.Element, "add", Nothing)
                Dim attribute As XmlAttribute = doc.CreateAttribute("key")
                attribute.Value = txtAccessCode.Text
                node.Attributes.Append(attribute)
                attribute = doc.CreateAttribute("value")
                attribute.Value = objclsServerConnection.CreatConnection(txtServername.Text, txtLogin.Text, txtAlterPassword.Text, txtDatabase.Text)
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

    Public Sub Refresh()
        Try
            txtAccessCode.Text = ""
            txtAlterPassword.Text = ""
            txtCompanyName.Text = ""
            txtDatabase.Text = ""
            txtLogin.Text = ""
            txtServername.Text = ""
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/CustomerMaster/ServerDetails.aspx"), False)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class