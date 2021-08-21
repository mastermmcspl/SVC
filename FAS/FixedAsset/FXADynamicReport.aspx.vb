Imports System.Data
Imports BusinesLayer
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms
Partial Class FixedAsset_FXADynamicReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "FixedAsset\FXADynamicReport.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objDynReport As New ClsFXADynamicReport
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSearch.ImageUrl = "~/Images/Search16.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadZone()
                LoadAssetReferenceNo()
                LoadSupplier()
                LoadAssetType()
                LoadAdditionReasons()
                AssetTransfer()
                ' LoadAssetNo()
                ' loaddetails()
                loadAssetDeletion()
                loadExistingTRnNo()
                loadDeletionStatus()
                rbtnStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub loadAssetDeletion()
        Dim dt As New DataTable
        Try
            ddlDeletion.Items.Insert(0, New ListItem("Asset Deletion", "0"))
            ddlDeletion.Items.Insert(1, New ListItem("Sold", "1"))
            ddlDeletion.Items.Insert(2, New ListItem("Transfer", "2"))
            ddlDeletion.Items.Insert(2, New ListItem("Stolen", "3"))
            ddlDeletion.Items.Insert(3, New ListItem("Destroyed", "4"))
            ddlDeletion.Items.Insert(4, New ListItem("Absolite", "5"))
            ddlDeletion.Items.Insert(4, New ListItem("Repair", "6"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetDeletion")
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objDynReport.LoadZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "Org_Name"
            ddlAccZone.DataValueField = "Org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "Select Zone")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadZone")
        End Try
    End Sub

    Private Sub ddlAccZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccZone.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlAccZone.SelectedIndex > 0 Then
                LoadRegion(ddlAccZone.SelectedValue)
            Else
                ddlAccZone.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccZone_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iZoneId As Integer)
        Dim dt As New DataTable
        Try
            dt = objDynReport.LoadRegion(sSession.AccessCode, sSession.AccessCodeID, iZoneId)
            ddlAccRgn.DataTextField = "Org_Name"
            ddlAccRgn.DataValueField = "Org_node"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "Select Region")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadRegion")
        End Try
    End Sub

    Private Sub ddlAccRgn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccRgn.SelectedIndexChanged
        Try
            If ddlAccRgn.SelectedIndex > 0 Then
                LoadArea(ddlAccRgn.SelectedValue)
            Else
                ddlAccRgn.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadArea(ByVal iRegionId As Integer)
        Dim dt As New DataTable
        Try
            dt = objDynReport.LoadArea(sSession.AccessCode, sSession.AccessCodeID, iRegionId)
            ddlAccArea.DataTextField = "Org_Name"
            ddlAccArea.DataValueField = "Org_node"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "Select Area")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadArea")
        End Try
    End Sub
    Private Sub ddlAccArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccArea.SelectedIndexChanged
        Try
            If ddlAccArea.SelectedIndex > 0 Then
                LoadBranch(ddlAccArea.SelectedValue)
            Else
                ddlAccArea.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadBranch(ByVal iareaId As Integer)
        Dim dt As New DataTable
        Try
            dt = objDynReport.LoadBranch(sSession.AccessCode, sSession.AccessCodeID, iareaId)
            ddlAccBrnch.DataTextField = "Org_Name"
            ddlAccBrnch.DataValueField = "Org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "Select Branch")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadBranch")
        End Try
    End Sub
    Public Sub LoadAssetReferenceNo()
        Dim dt As New DataTable
        Try
            dt = objDynReport.LoadAssetReferenceNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlRefno.DataTextField = "AFAA_AssetRefNo"
            ddlRefno.DataValueField = "AFAA_ID"
            ddlRefno.DataSource = dt
            ddlRefno.DataBind()
            ddlRefno.Items.Insert(0, "Select AssetRefno")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAssetReferenceNo")
        End Try
    End Sub
    Public Sub LoadSupplier()
        Dim dt As New DataTable
        Try
            dt = objDynReport.LoadSupplier(sSession.AccessCode, sSession.AccessCodeID)
            ddlSuppliers.DataTextField = "CSM_Name"
            ddlSuppliers.DataValueField = "CSM_ID"
            ddlSuppliers.DataSource = dt
            ddlSuppliers.DataBind()
            ddlSuppliers.Items.Insert(0, New ListItem("Select Supplier", "0"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSupplier")
        End Try
    End Sub
    Public Sub LoadAssetType()
        Dim dt As New DataTable
        Try
            dt = objDynReport.LoadAssetType(sSession.AccessCode, sSession.AccessCodeID)
            drpAstype.DataTextField = "GL_Desc"
            drpAstype.DataValueField = "GL_ID"
            drpAstype.DataSource = dt
            drpAstype.DataBind()
            drpAstype.Items.Insert(0, New ListItem("Select AssetType", "0"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAssetType")
        End Try
    End Sub
    'Public Sub LoadAssetNo()
    '    Dim dt As New DataTable
    '    Try
    '        dt = objDynReport.LoadAssetNo(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlAssetNo.DataTextField = "AFAA_AssetNo"
    '        ddlAssetNo.DataValueField = "AFAA_ID"
    '        ddlAssetNo.DataSource = dt
    '        ddlAssetNo.DataBind()
    '        ddlAssetNo.Items.Insert(0, New ListItem("Select AssetNo", "0"))
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAssetNo")
    '    End Try
    'End Sub
    Public Sub LoadAdditionReasons()
        Try
            ddlTrTypes.Items.Insert(0, New ListItem("Select Transaction Type", "0"))
            ddlTrTypes.Items.Insert(1, New ListItem("Addition", "1"))
            ddlTrTypes.Items.Insert(2, New ListItem("Transfers", "2"))
            ddlTrTypes.Items.Insert(3, New ListItem("Revaluation", "3"))
            ddlTrTypes.Items.Insert(4, New ListItem("Foreign Exchange", "4"))
            ddlTrTypes.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAdditionReasons")
        End Try
    End Sub
    Public Sub AssetTransfer()
        Try
            ddlAssetTrnfr.Items.Insert(0, New ListItem("Select Asset Transfer", "0"))
            ddlAssetTrnfr.Items.Insert(1, New ListItem("Local", "1"))
            ddlAssetTrnfr.Items.Insert(2, New ListItem("Imported", "2"))
            ddlAssetTrnfr.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AssetTransfer")
        End Try
    End Sub
    Private Sub loaddetails()
        Dim dt, dt2 As New DataTable
        Dim dt3, dt4 As New DataTable
        Dim dtCust As New DataTable
        Dim onjFasGnrl As New clsFASGeneral
        Dim dRow As DataRow
        Dim iRefId As Integer = 0
        Dim iDelId As Integer = 0
        Dim str As String = " "
        Dim dFromDate As Date, dTo As Date
        Dim dFromDatePurchase As Date, dPurchaseTo As Date
        Dim iZone As Integer = 0, iRegion As Integer = 0, iArea As Integer = 0, iBranch As Integer = 0
        Dim irstatus As Integer = 0
        Dim Msg1 As String = ""
        Try
            lblError.Text = ""
            str = "Report is based on "
            If (rbtnStatus.SelectedValue > 0) Then
                irstatus = rbtnStatus.SelectedValue
                str = str + " And " + rbtnStatus.SelectedItem.Text
            End If

            If (ddlAccZone.SelectedIndex > 0) Then
                iZone = ddlAccZone.SelectedValue
                str = str + " And " + ddlAccZone.SelectedItem.Text
            End If
            If (ddlAccRgn.SelectedIndex > 0) Then
                iRegion = ddlAccRgn.SelectedValue
                str = str + " And " + ddlAccRgn.SelectedItem.Text
            End If
            If (ddlAccArea.SelectedIndex > 0) Then
                iArea = ddlAccArea.SelectedValue
                str = str + " And " + ddlAccArea.SelectedItem.Text
            End If
            If (ddlAccBrnch.SelectedIndex > 0) Then
                iBranch = ddlAccBrnch.SelectedValue
                str = str + " And " + ddlAccBrnch.SelectedItem.Text
            End If
            If (ddlRefno.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + " " + ddlRefno.SelectedItem.Text
                Else
                    str = str + " And " + ddlRefno.SelectedItem.Text
                End If
                iRefId = ddlRefno.SelectedValue
                iDelId = ddlDelRefNo.SelectedValue
            End If
            If (ddlSuppliers.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + " " + ddlSuppliers.SelectedItem.Text
                Else
                    str = str + " And " + ddlSuppliers.SelectedItem.Text
                End If
            End If
            If (drpAstype.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + " " + drpAstype.SelectedItem.Text
                Else
                    str = str + " And " + drpAstype.SelectedItem.Text
                End If
            End If
            If (ddlAssetNo.SelectedIndex > 0) Then
                If str = " " Then
                    str = str + " " + ddlAssetNo.SelectedItem.Text
                Else
                    str = str + " And " + ddlAssetNo.SelectedItem.Text
                End If
            End If
            If (ddlTrTypes.SelectedIndex > 0) Then
                If str = "" Then
                    str = str + "" + ddlTrTypes.SelectedItem.Text
                Else
                    str = str + " And " + ddlTrTypes.SelectedItem.Text
                End If
            End If
            If (ddlAssetTrnfr.SelectedIndex > 0) Then
                If str = "" Then
                    str = str + "" + ddlAssetTrnfr.SelectedItem.Text
                Else
                    str = str + " And " + ddlAssetTrnfr.SelectedItem.Text
                End If
            End If
            If (txtfrom.Text <> "" And txtTo.Text <> "") Then
                If str = " " Then
                    str = str + " " + " Date between" + onjFasGnrl.FormatDtForRDBMS(txtfrom.Text, "D") + " And " + onjFasGnrl.FormatDtForRDBMS(txtTo.Text, "D")
                Else
                    str = str + " And " + "Date between" + onjFasGnrl.FormatDtForRDBMS(txtfrom.Text, "D") + " And " + onjFasGnrl.FormatDtForRDBMS(txtTo.Text, "D")
                End If
            End If
            If (txtPurchasefrom.Text <> "" And txtpurchaseTo.Text <> "") Then
                If str = " " Then
                    str = str + " " + " Date between" + onjFasGnrl.FormatDtForRDBMS(txtPurchasefrom.Text, "D") + " And " + onjFasGnrl.FormatDtForRDBMS(txtpurchaseTo.Text, "D")
                Else
                    str = str + " And " + "Date between" + onjFasGnrl.FormatDtForRDBMS(txtPurchasefrom.Text, "D") + " And " + onjFasGnrl.FormatDtForRDBMS(txtpurchaseTo.Text, "D")
                End If
            End If
            If (txtAssetAge.Text <> "") Then
                If str = "" Then
                    str = str + "" + txtAssetAge.Text
                Else
                    str = str + "and" + txtAssetAge.Text
                End If
            End If
            'If (str = " ") Then
            '    str = "Dynamic Report"
            'Else
            '    str = "Report Based On " + str
            'End If
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
            If txtPurchasefrom.Text <> "" Then
                dFromDatePurchase = Date.ParseExact(Trim(onjFasGnrl.FormatDtForRDBMS(txtPurchasefrom.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                dFromDatePurchase = "01/01/1900"
            End If
            If txtpurchaseTo.Text <> "" Then
                dPurchaseTo = Date.ParseExact(Trim(onjFasGnrl.FormatDtForRDBMS(txtpurchaseTo.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                dPurchaseTo = "01/01/1900"
            End If
            If txtdepRate.Text <> "" Then
                If str = "" Then
                    str = str + "" + txtdepRate.Text
                Else
                    str = str + "and" + txtdepRate.Text
                End If
            End If
            If txtItemcode.Text <> "" Then
                If str = "" Then
                    str = str + "" + txtItemcode.Text
                Else
                    str = str + "and" + txtItemcode.Text
                End If
            End If
            If txtItemDesc.Text <> "" Then
                If str = "" Then
                    str = str + "" + txtItemDesc.Text
                Else
                    str = str + "and" + txtItemDesc.Text
                End If
            End If
            Dim sAsstNo As String = ""
            If ddlAssetNo.SelectedIndex = -1 Or ddlAssetNo.SelectedIndex = 0 Then
                sAsstNo = ""
            Else
                sAsstNo = ddlAssetNo.SelectedValue
            End If
            If rbtnStatus.SelectedValue <> 3 And rbtnStatus.SelectedValue <> 4 And rbtnStatus.SelectedValue <> 5 Then
                dt = objDynReport.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, iZone, iRegion, iArea, iBranch, rbtnStatus.SelectedValue, iRefId, ddlSuppliers.SelectedValue, drpAstype.SelectedValue, sAsstNo, dFromDate, dTo, ddlTrTypes.SelectedIndex, ddlAssetTrnfr.SelectedIndex, txtAssetAge.Text, dFromDatePurchase, dPurchaseTo, txtdepRate.Text, txtItemcode.Text, txtItemDesc.Text, sSession.YearID)
                If (rbtnStatus.SelectedValue = 4) Then
                    ReportViewer1.Reset()
                    Dim rds As New ReportDataSource("DataSet2", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
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
                    dtCust = objDynReport.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
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
                    Dim rds1 As New ReportDataSource("DataSet1", dt2)
                    ReportViewer1.LocalReport.DataSources.Add(rds1)
                    Dim rds2 As New ReportDataSource("DataSet3", dt3)
                    ReportViewer1.LocalReport.DataSources.Add(rds2)
                    Dim rds3 As New ReportDataSource("DataSet4", dt4)
                    ReportViewer1.LocalReport.DataSources.Add(rds3)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
                    ReportViewer1.LocalReport.Refresh()
                Else
                    ReportViewer1.Reset()
                    Dim rds As New ReportDataSource("DataSet2", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
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
                    dtCust = objDynReport.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
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
                    Dim rds1 As New ReportDataSource("DataSet1", dt2)
                    ReportViewer1.LocalReport.DataSources.Add(rds1)
                    Dim rds2 As New ReportDataSource("DataSet3", dt3)
                    ReportViewer1.LocalReport.DataSources.Add(rds2)
                    Dim rds3 As New ReportDataSource("DataSet4", dt4)
                    ReportViewer1.LocalReport.DataSources.Add(rds3)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
                    ReportViewer1.LocalReport.Refresh()
                End If
                Msg1 = "Additional Details."
            ElseIf rbtnStatus.SelectedValue = 3 Then
                dt3 = objDynReport.LoadDeleteDetails(sSession.AccessCode, sSession.AccessCodeID, rbtnStatus.SelectedValue, iDelId, ddlDeletion.SelectedValue, drpAstype.SelectedValue, sAsstNo, dFromDate, dTo, txtAssetAge.Text, dFromDatePurchase, dPurchaseTo, txtItemDesc.Text, ddlDelStatus.SelectedIndex, sSession.YearID)
                ReportViewer1.Reset()
                Dim rds2 As New ReportDataSource("DataSet3", dt3)
                ReportViewer1.LocalReport.DataSources.Add(rds2)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
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
                dtCust = objDynReport.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
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
                Dim rds1 As New ReportDataSource("DataSet1", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                Dim rds As New ReportDataSource("DataSet2", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                Dim rds3 As New ReportDataSource("DataSet4", dt4)
                ReportViewer1.LocalReport.DataSources.Add(rds3)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
                ReportViewer1.LocalReport.Refresh()
                Msg1 = "Deletion Details."
            ElseIf rbtnStatus.SelectedValue = 4 Then
                dt3 = objDynReport.LoadDeleteDetails(sSession.AccessCode, sSession.AccessCodeID, rbtnStatus.SelectedValue, iDelId, ddlDeletion.SelectedValue, drpAstype.SelectedValue, sAsstNo, dFromDate, dTo, txtAssetAge.Text, dFromDatePurchase, dPurchaseTo, txtItemDesc.Text, ddlDelStatus.SelectedIndex, sSession.YearID)
                ReportViewer1.Reset()
                Dim rds2 As New ReportDataSource("DataSet3", dt3)
                ReportViewer1.LocalReport.DataSources.Add(rds2)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
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
                dtCust = objDynReport.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
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
                Dim rds1 As New ReportDataSource("DataSet1", dt2)
                dt = objDynReport.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, iZone, iRegion, iArea, iBranch, rbtnStatus.SelectedValue, iRefId, ddlSuppliers.SelectedValue, drpAstype.SelectedValue, sAsstNo, dFromDate, dTo, ddlTrTypes.SelectedIndex, ddlAssetTrnfr.SelectedIndex, txtAssetAge.Text, dFromDatePurchase, dPurchaseTo, txtdepRate.Text, txtItemcode.Text, txtItemDesc.Text, sSession.YearID)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                Dim rds As New ReportDataSource("DataSet2", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                Dim rds3 As New ReportDataSource("DataSet4", dt4)
                ReportViewer1.LocalReport.DataSources.Add(rds3)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
                ReportViewer1.LocalReport.Refresh()
                Msg1 = "Addition details and Deletion Details."
            Else
                dt4 = objDynReport.LoadMasterDetails(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue, sAsstNo, sSession.YearID)
                ReportViewer1.Reset()
                Dim rds3 As New ReportDataSource("DataSet4", dt4)
                ReportViewer1.LocalReport.DataSources.Add(rds3)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
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
                dtCust = objDynReport.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
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
                Dim rds2 As New ReportDataSource("DataSet3", dt3)
                ReportViewer1.LocalReport.DataSources.Add(rds2)
                Dim rds1 As New ReportDataSource("DataSet1", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds1)
                Dim rds As New ReportDataSource("DataSet2", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetDynamicReport.rdlc")
                ReportViewer1.LocalReport.Refresh()
                Msg1 = "Masters Details."
            End If
            'Dim str1 As String = "DynamicReport"
            'Dim Phead As ReportParameter() = New ReportParameter() {New ReportParameter("Phead", str1)}
            Dim Phead As ReportParameter() = New ReportParameter() {New ReportParameter("Phead", str)}
            ReportViewer1.LocalReport.SetParameters(Phead)
            Dim Msg As ReportParameter() = New ReportParameter() {New ReportParameter("Msg", Msg1)}
            ReportViewer1.LocalReport.SetParameters(Msg)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loaddetails")
        End Try
    End Sub
    Private Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Try
            loaddetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/FixedAsset/AssetAdditionDashBoard.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            ReportViewer1.Reset()
            ddlAssetTrnfr.SelectedIndex = 0 : ddlAccZone.SelectedIndex = 0 : ddlRefno.SelectedIndex = 0 : ddlAssetNo.SelectedIndex = 0
            txtfrom.Text = "" : txtTo.Text = "" : txtPurchasefrom.Text = "" : txtpurchaseTo.Text = "" : ddlAssetTrnfr.SelectedIndex = 0 : txtAssetAge.Text = ""
            txtdepRate.Text = "" : txtItemcode.Text = "" : txtItemDesc.Text = ""
            ddlAccRgn.SelectedIndex = -1 : ddlAccArea.SelectedIndex = -1 : ddlAccBrnch.SelectedIndex = -1 : ddlSuppliers.SelectedIndex = 0
            ddlTrTypes.SelectedIndex = 0 : drpAstype.SelectedIndex = 0 : ddlDelRefNo.SelectedIndex = 0 : ddlDeletion.SelectedIndex = -1
            txtDelAsstAge.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click")
        End Try
    End Sub

    Private Sub drpAstype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpAstype.SelectedIndexChanged
        Try
            ddlAssetNo.DataSource = objDynReport.ExistingItemCode(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue)
            ddlAssetNo.DataTextField = "AFAM_ItemCode"
            ddlAssetNo.DataValueField = "AFAM_ItemCode"
            ddlAssetNo.DataBind()
            ddlAssetNo.Items.Insert(0, "Select Itemcode")
            ddlDelRefNo.SelectedIndex = 0 : ddlDeletion.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub loadExistingTRnNo()
        Try
            ddlDelRefNo.DataSource = objDynReport.ExistingTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlDelRefNo.DataTextField = "AFAD_AssetTransNo"
            ddlDelRefNo.DataValueField = "AFAD_ID"
            ddlDelRefNo.DataBind()
            ddlDelRefNo.Items.Insert(0, "Select deletion reference no")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingTRnNo")
        End Try
    End Sub

    Private Sub ddlDelRefNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelRefNo.SelectedIndexChanged
        ddlDeletion.SelectedIndex = -1
    End Sub

    Private Sub rbtnStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbtnStatus.SelectedIndexChanged
        Try
            If rbtnStatus.SelectedValue = 1 Or rbtnStatus.SelectedValue = 2 Then
                pnlAdditon.Visible = True
                pnlDeletion.Visible = False
                pnlCommon.Visible = True
            ElseIf rbtnStatus.SelectedValue = 4 Then
                pnlAdditon.Visible = False
                pnlDeletion.Visible = False
                pnlCommon.Visible = False
            ElseIf rbtnStatus.SelectedValue = 3 Then
                pnlDeletion.Visible = True
                pnlAdditon.Visible = False
                pnlCommon.Visible = True
            End If
            loaddetails()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub loadDeletionStatus()
        Dim dt As New DataTable
        Try
            ddlDelStatus.Items.Insert(0, New ListItem("Deletion status", "0"))
            ddlDelStatus.Items.Insert(1, New ListItem("Waiting For Approval", "1"))
            ddlDelStatus.Items.Insert(2, New ListItem("Deleted", "2"))
            ddlDelStatus.Items.Insert(3, New ListItem("Transfered/Repair", "3"))
            ddlDelStatus.Items.Insert(4, New ListItem("Reactivated", "4"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadDeletionStatus")
        End Try
    End Sub
End Class
