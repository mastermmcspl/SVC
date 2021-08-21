
'Partial Class Reports_Purchase_PurchasedynamicReport
'    Inherits System.Web.UI.Page

'End Class
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Drawing
Partial Class Reports_Purchase_PurchasedynamicReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "PurchaseReport.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objPReport As New clsPurchaseReport
    Dim objclsModulePermission As New clsModulePermission
    Private objAccSetting As New clsAccountSetting
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        imgbtnSearch.ImageUrl = "~/Images/Search16.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Try
            sSession = Session("AllSession")
            sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DR")

            If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                Response.Redirect("~/Permissions/PurchasePermission.aspx", False) 'Permissions/PurchasePermission
                Exit Sub
            Else
                If sFormButtons.Contains(",View,") = True Then
                End If
            End If
            lblError.Text = ""
            If IsPostBack = False Then
                '  CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                LoadZone()
                LoadOrder()
                LoadSuppliers()
                LoadCommodity()
                LoadItem()
                loaddetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "--- Select Zone ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccZone.SelectedIndexChanged
        Try
            If ddlAccZone.SelectedIndex > 0 Then
                LoadRegion(ddlAccZone.SelectedValue)
            Else
                ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccZone As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
            ddlAccRgn.DataTextField = "org_name"
            ddlAccRgn.DataValueField = "org_node"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "--- Select Region ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccRgn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccRgn.SelectedIndexChanged
        Try
            If ddlAccRgn.SelectedIndex > 0 Then
                LoadArea(ddlAccRgn.SelectedValue)
            Else
                ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadArea(ByVal iAccRgn As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
            ddlAccArea.DataTextField = "org_name"
            ddlAccArea.DataValueField = "org_node"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "--- Select Area ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccArea.SelectedIndexChanged
        Try
            If ddlAccArea.SelectedIndex > 0 Then
                LoadAccBrnch(ddlAccArea.SelectedValue)
            Else
                ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            'lblError.Text = ""
            'lblStatus.Text = ""
            'oStatus = HttpUtility.UrlEncode(objFasGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/HomePages/Home.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    'Public Sub CheckAuidtPermission(ByVal sNameSpace As String, ByVal iUsrId As Integer)
    '    Dim sbret As String
    '    Try
    '        sbret = clsGeneralMaster.CheckUmsPermit(sNameSpace, sSession.AccessCodeID, iUsrId, "FasDyR", "ALL")
    '        If sbret = "False" Or sbret = "" Then
    '            Response.Redirect("~/Permissions/PurchasePermission.aspx")
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Private Sub loaddetails()
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dtCust As New DataTable
        Dim onjFasGnrl As New clsFASGeneral
        Dim dRow As DataRow
        Dim invId As Integer = 0
        Dim str As String = " "
        Dim dFromDate As Date, dTo As Date
        Dim iZone As Integer = 0, iRegion As Integer = 0, iArea As Integer = 0, iBranch As Integer = 0

        Try
            str = "Report is based on "
            If (ddlAccZone.SelectedIndex > 0) Then
                iZone = ddlAccZone.SelectedValue
                str = str + ddlAccZone.SelectedItem.Text + " And "
            End If
            If (ddlAccRgn.SelectedIndex > 0) Then
                iRegion = ddlAccRgn.SelectedValue
                str = str + ddlAccRgn.SelectedItem.Text + " And "
            End If
            If (ddlAccArea.SelectedIndex > 0) Then
                iArea = ddlAccArea.SelectedValue
                str = str + ddlAccArea.SelectedItem.Text + " And "
            End If
            If (ddlAccBrnch.SelectedIndex > 0) Then
                iBranch = ddlAccBrnch.SelectedValue
                str = str + ddlAccBrnch.SelectedItem.Text + " And "
            End If

            If (ddlorder.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + ddlorder.SelectedItem.Text + " "
                Else
                    str = str + ddlorder.SelectedItem.Text + " And "
                End If

            End If

            If (ddlSuppliers.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + ddlSuppliers.SelectedItem.Text + " "
                Else
                    str = str + ddlSuppliers.SelectedItem.Text + " And "
                End If
            End If
            If (ddlCommodity.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + ddlCommodity.SelectedItem.Text + " "
                Else
                    str = str + ddlCommodity.SelectedItem.Text + " And "
                End If

            End If
            If (ddlInvoiceNo.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + ddlInvoiceNo.SelectedItem.Text + " "
                Else
                    str = str + ddlInvoiceNo.SelectedItem.Text + " And "
                End If
                invId = ddlInvoiceNo.SelectedValue
            End If

            If (ddlItem.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + ddlItem.SelectedItem.Text + " "
                Else
                    str = str + ddlItem.SelectedItem.Text + " And "
                End If

            End If

            If (txtCst.Text <> "") Then
                If str = " " Then
                    str = str + txtCst.Text + " "
                Else
                    str = str + txtCst.Text + " And "
                End If

            End If
            If (txtDiscount.Text <> "") Then
                If str = " " Then
                    str = str + txtDiscount.Text + " "
                Else
                    str = str + txtDiscount.Text + " And "
                End If

            End If
            If (txtvat.Text <> "") Then
                If str = " " Then
                    str = str + txtvat.Text + " "
                Else
                    str = str + txtvat.Text + " And "
                End If

            End If

            If (txtfrom.Text <> "" And txtTo.Text <> "") Then
                If str = " " Then
                    str = str + " Date between" + onjFasGnrl.FormatDtForRDBMS(txtfrom.Text, "D") + " And " + onjFasGnrl.FormatDtForRDBMS(txtTo.Text, "D") + " "
                Else
                    str = str + "Date between" + onjFasGnrl.FormatDtForRDBMS(txtfrom.Text, "D") + " And " + onjFasGnrl.FormatDtForRDBMS(txtTo.Text, "D") + " And "
                End If
            End If

            If (str = " ") Then
                str = "Purchase Report"
            Else
                str = " " + str
            End If

            If txtfrom.Text <> "" Then
                dFromDate = Date.ParseExact(Trim(onjFasGnrl.FormatDtForRDBMS(txtfrom.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            Else
                dFromDate = "01/01/1900"
            End If
            If txtTo.Text <> "" Then
                dTo = Date.ParseExact(Trim(onjFasGnrl.FormatDtForRDBMS(txtTo.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            Else

                dTo = "01/01/1900"
            End If
            dt = objPReport.loadDetails(sSession.AccessCode, sSession.AccessCodeID, rbtlCat.SelectedValue, ddlorder.SelectedValue, invId, ddlSuppliers.SelectedValue, ddlCommodity.SelectedValue, ddlItem.SelectedValue, txtvat.Text, txtExcise.Text, txtCst.Text, txtDiscount.Text, dFromDate, dTo, iZone, iRegion, iArea, iBranch)
            If (rbtlCat.SelectedValue = 4) Then
                ReportViewer1.Reset()
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/SinkPurchasreReport.rdlc")
                ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                ReportViewer1.ZoomPercent = 125
                ReportViewer1.LocalReport.Refresh()
                dt2.Columns.Add("CCust_name")
                dt2.Columns.Add("CCust_address")
                dt2.Columns.Add("CCust_email")
                dt2.Columns.Add("CCust_ph")
                dt2.Columns.Add("CCust_vat")
                dt2.Columns.Add("CCust_tax")
                dt2.Columns.Add("CCust_pan")
                dt2.Columns.Add("CCust_tan")
                dt2.Columns.Add("CCust_tin")
                dt2.Columns.Add("CCust_cin")

                dtCust = objPReport.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If dtCust.Rows.Count > 0 Then
                    dRow = dt2.NewRow()
                    dRow("CCust_name") = dtCust.Rows(0)("CUST_NAME")
                    dRow("CCust_address") = dtCust.Rows(0)("CUST_Comm_Address")
                    dRow("CCust_email") = dtCust.Rows(0)("CUST_Email")
                    dRow("CCust_ph") = dtCust.Rows(0)("CUST_Comm_Tel")
                    dRow("CCust_vat") = dtCust.Rows(0)("CVAT")
                    dRow("CCust_tax") = dtCust.Rows(0)("CTAX")
                    dRow("Ccust_pan") = dtCust.Rows(0)("CPAN")
                    dRow("CCust_tan") = dtCust.Rows(0)("CTAN")
                    dRow("CCust_tin") = dtCust.Rows(0)("CTIN")
                    dRow("CCust_cin") = dtCust.Rows(0)("CCIN")
                    dt2.Rows.Add(dRow)
                End If
                Dim rds1 As New ReportDataSource("DataSet2", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/SinkPurchasreReport.rdlc")


                ' If str <> " Then
                'Dim Phead As ReportParameter() = New ReportParameter() {New ReportParameter("Phead", str)}
                'ReportViewer1.LocalReport.SetParameters(Phead)
                '' End If
                ReportViewer1.LocalReport.Refresh()
            Else
                ReportViewer1.Reset()
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/rptPurchaseReport.rdlc")
                ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                ReportViewer1.ZoomPercent = 125
                ReportViewer1.ZoomPercent = 125
                dt2.Columns.Add("CCust_name")
                dt2.Columns.Add("CCust_address")
                dt2.Columns.Add("CCust_email")
                dt2.Columns.Add("CCust_ph")
                dt2.Columns.Add("CCust_vat")
                dt2.Columns.Add("CCust_tax")
                dt2.Columns.Add("CCust_pan")
                dt2.Columns.Add("CCust_tan")
                dt2.Columns.Add("CCust_tin")
                dt2.Columns.Add("CCust_cin")
                'If str <> 0 Then
                '    Dim Phead As ReportParameter() = New ReportParameter() {New ReportParameter("Phead", ClsPurchaseOrderHR.NumberToWord(String.Format("{0:0.00}", str)) & " Only")}
                '    ReportViewer1.LocalReport.SetParameters(Phead)
                'End If

                dtCust = objPReport.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                If dtCust.Rows.Count > 0 Then
                    dRow = dt2.NewRow()
                    dRow("CCust_name") = dtCust.Rows(0)("CUST_NAME")
                    dRow("CCust_address") = dtCust.Rows(0)("CUST_Comm_Address")
                    dRow("CCust_email") = dtCust.Rows(0)("CUST_Email")
                    dRow("CCust_ph") = dtCust.Rows(0)("CUST_Comm_Tel")
                    dRow("CCust_vat") = dtCust.Rows(0)("CVAT")
                    dRow("CCust_tax") = dtCust.Rows(0)("CTAX")
                    dRow("Ccust_pan") = dtCust.Rows(0)("CPAN")
                    dRow("CCust_tan") = dtCust.Rows(0)("CTAN")
                    dRow("CCust_tin") = dtCust.Rows(0)("CTIN")
                    dRow("CCust_cin") = dtCust.Rows(0)("CCIN")
                    dt2.Rows.Add(dRow)
                End If
                Dim rds1 As New ReportDataSource("DataSet2", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/rptPurchaseReport.rdlc")
                ReportViewer1.LocalReport.Refresh()
            End If

            If str.EndsWith(" And ") Then
                str = str.Remove(Len(str) - 5, 5)
            End If

            Dim Phead As ReportParameter() = New ReportParameter() {New ReportParameter("Phead", str)}
            ReportViewer1.LocalReport.SetParameters(Phead)
            ' End If

            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loaddetails")
        End Try
    End Sub
    Private Sub LoadSuppliers()
        Try
            ddlSuppliers.DataSource = objPReport.LoadSuppliers(sSession.AccessCode, sSession.AccessCodeID)
            ddlSuppliers.DataTextField = "CSM_Name"
            ddlSuppliers.DataValueField = "CSM_ID"
            ddlSuppliers.DataBind()
            ddlSuppliers.Items.Insert(0, New ListItem("--- Select Supplier ---", "0"))
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSuppliers")
        End Try
    End Sub
    Private Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objPReport.Commodity(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "Inv_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, New ListItem("--- Select Commodity ---", "0"))
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCommodity")
        End Try
    End Sub
    Private Sub LoadItem()
        Try
            ddlItem.DataSource = objPReport.Item(sSession.AccessCode, sSession.AccessCodeID, ddlCommodity.SelectedValue)
            ddlItem.DataTextField = "Inv_Description"
            ddlItem.DataValueField = "Inv_ID"
            ddlItem.DataBind()
            ddlItem.Items.Insert(0, New ListItem("--- Select Item ---", "0"))
        Catch ex As Exception
            lblError.Text = ex.Message

            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCommodity")
        End Try
    End Sub
    Private Sub LoadOrder()
        Try
            ddlorder.DataSource = objPReport.Order(sSession.AccessCode, sSession.AccessCodeID)
            ddlorder.DataTextField = "POM_OrderNo"
            ddlorder.DataValueField = "POM_ID"
            ddlorder.DataBind()
            ddlorder.Items.Insert(0, New ListItem("--- Select Order No. ---", "0"))

        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCommodity")
        End Try
    End Sub
    Protected Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            LoadItem()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        Try
            loaddetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "brnSearch_Click")
        End Try
    End Sub
    Protected Sub rbtlCat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbtlCat.SelectedIndexChanged
        Try
            loaddetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rbtlCat_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlorder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlorder.SelectedIndexChanged
        Try
            If (ddlorder.SelectedIndex > 0) Then
                LoadExistGoodsInwardNo(ddlorder.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlorder_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadExistGoodsInwardNo(ByVal iTransactionID As Integer)
        Try
            ddlInvoiceNo.DataSource = objPReport.LoadInwardNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iTransactionID)
            ddlInvoiceNo.DataTextField = "PGM_DocumentRefNo"
            ddlInvoiceNo.DataValueField = "PGM_ID"
            ddlInvoiceNo.DataBind()
            ddlInvoiceNo.Items.Insert(0, "--- Select Invoice NO ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Try
            loaddetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "brnSearch_Click")
        End Try
    End Sub
    Protected Sub ddlSuppliers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSuppliers.SelectedIndexChanged

    End Sub
End Class
