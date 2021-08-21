Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Reports_Viewer_SalesDynamicReport
    Inherits System.Web.UI.Page
    Private sFormName As String = "Reports_Viewer_SalesDynamicReport"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objSDR As New clsSalesDynamicReport
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Dim objAccSetting As New clsAccountSetting
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnSink.ImageUrl = "~/Images/CheckMark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SDR")
                imgbtnSink.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        imgbtnSink.Visible = True
                    End If
                End If
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasSDR", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/SalesPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then

                '    End If
                'End If
                LoadZone()
                LoadOrder()
                LoadCommodity()
                LoadParty()
                BindDescription(0)
                LoadGrideDetails()
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
    Private Sub LoadOrder()
        Try
            ddlorder.DataSource = objSDR.OrderNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlorder.DataTextField = "SPO_OrderCode"
            ddlorder.DataValueField = "SPO_ID"
            ddlorder.DataBind()
            ddlorder.Items.Insert(0, "--- Select Order No ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCommodity()
        Try
            ddlCommodity.DataSource = objSDR.LoadCommodity(sSession.AccessCode, sSession.AccessCodeID)
            ddlCommodity.DataTextField = "Inv_Description"
            ddlCommodity.DataValueField = "INV_ID"
            ddlCommodity.DataBind()
            ddlCommodity.Items.Insert(0, "--- Select Commodity ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadParty()
        Try
            ddlParty.DataSource = objSDR.BindParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "BM_Name"
            ddlParty.DataValueField = "BM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "--- Select Party ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDescription(ByVal iCommodityID As Integer)
        Try
            ddlItem.DataSource = objSDR.LoadItems(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCommodityID)
            ddlItem.DataTextField = "INV_Code"
            ddlItem.DataValueField = "INV_ID"
            ddlItem.DataBind()
            ddlItem.Items.Insert(0, "--- Select Goods ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearAll()
        Try
            lblError.Text = ""
            ddlorder.SelectedIndex = 0 : ddlDispatchNo.Items.Clear() : ddlParty.SelectedIndex = 0 : txtfrom.Text = "" : txtTo.Text = ""
            ddlCommodity.SelectedIndex = 0 : ddlItem.SelectedIndex = 0
            txtDiscount.Text = "" : txtvat.Text = "" : txtCst.Text = "" : txtExcise.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadGrideDetails()
        Dim dt, dtDetails As New DataTable
        Dim iOrderNo, iCommodity, iItemID, iParty As Integer
        Dim dFromDate, dTo As Date
        Dim dDiscount, dVAT, dCST, dExcise As Double

        Dim dRow As DataRow
        Dim dt2, dt3, dtParty, dtCust As New DataTable
        Dim iDispatchNo As Integer
        Dim dApplicationDate As String = ""
        Dim sStrMesg As String = ""
        Dim iZone As Integer = 0, iRegion As Integer = 0, iArea As Integer = 0, iBranch As Integer = 0
        Try
            lblError.Text = ""

            If txtfrom.Text <> "" And txtTo.Text = "" Then
                lblError.Text = "Select To Date."
                Exit Sub
            End If
            If txtfrom.Text = "" And txtTo.Text <> "" Then
                lblError.Text = "Select From Date."
                Exit Sub
            End If

            sStrMesg = "Report based on "

            If (ddlAccZone.SelectedIndex > 0) Then
                iZone = ddlAccZone.SelectedValue
                sStrMesg = sStrMesg & "Zone " & ddlAccZone.SelectedItem.Text & " And "
            End If
            If (ddlAccRgn.SelectedIndex > 0) Then
                iRegion = ddlAccRgn.SelectedValue
                sStrMesg = sStrMesg & "Region " & ddlAccRgn.SelectedItem.Text & " And "
            End If
            If (ddlAccArea.SelectedIndex > 0) Then
                iArea = ddlAccArea.SelectedValue
                sStrMesg = sStrMesg & "Area " & ddlAccArea.SelectedItem.Text & " And "
            End If
            If (ddlAccBrnch.SelectedIndex > 0) Then
                iBranch = ddlAccBrnch.SelectedValue
                sStrMesg = sStrMesg & "Branch " & ddlAccBrnch.SelectedItem.Text & " And "
            End If

            If ddlorder.SelectedIndex > 0 Then
                iOrderNo = ddlorder.SelectedValue
                sStrMesg = sStrMesg & "Order No " & ddlorder.SelectedItem.Text & " And "
                If ddlDispatchNo.SelectedIndex > 0 Then
                    iDispatchNo = ddlDispatchNo.SelectedValue
                    sStrMesg = sStrMesg & "Invoice No " & ddlDispatchNo.SelectedItem.Text & " And "
                Else
                    iDispatchNo = 0
                End If
            Else
                iOrderNo = 0
            End If

            If ddlCommodity.SelectedIndex > 0 Then
                iCommodity = ddlCommodity.SelectedValue
                sStrMesg = sStrMesg & "Commodity " & ddlCommodity.SelectedItem.Text & " And "
            Else
                iCommodity = 0
            End If
            If ddlItem.SelectedIndex > 0 Then
                iItemID = ddlItem.SelectedValue
                sStrMesg = sStrMesg & "Item " & ddlItem.SelectedItem.Text & " And "
            Else
                iItemID = 0
            End If
            If ddlParty.SelectedIndex > 0 Then
                iParty = ddlParty.SelectedValue
                sStrMesg = sStrMesg & "Party " & ddlParty.SelectedItem.Text & " And "
            Else
                iParty = 0
            End If
            If txtfrom.Text <> "" Then
                dFromDate = Date.ParseExact(Trim(objGen.FormatDtForRDBMS(txtfrom.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                sStrMesg = sStrMesg & "From Date " & objGen.FormatDtForRDBMS(txtfrom.Text, "D") & " And "
            Else
                dFromDate = "01/01/1900"
            End If
            If txtTo.Text <> "" Then
                dTo = Date.ParseExact(Trim(objGen.FormatDtForRDBMS(txtTo.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                sStrMesg = sStrMesg & "To Date " & objGen.FormatDtForRDBMS(txtTo.Text, "D") & " And "
            Else
                dTo = "01/01/1900"
            End If
            If txtDiscount.Text <> "" Then
                dDiscount = txtDiscount.Text
                sStrMesg = sStrMesg & "Discount " & txtDiscount.Text & " And "
            Else
                dDiscount = 0
            End If
            If txtvat.Text <> "" Then
                'dVAT = txtvat.Text
                dVAT = objSDR.GetID(sSession.AccessCode, sSession.AccessCodeID, Trim(txtvat.Text))
                sStrMesg = sStrMesg & "VAT " & txtvat.Text & " And "
            Else
                dVAT = 0
            End If
            If txtCst.Text <> "" Then
                'dCST = txtCst.Text
                dCST = objSDR.GetID(sSession.AccessCode, sSession.AccessCodeID, Trim(txtCst.Text))
                sStrMesg = sStrMesg & "CST " & txtCst.Text & " And "
            Else
                dCST = 0
            End If
            If txtExcise.Text <> "" Then
                'dExcise = txtExcise.Text
                dExcise = objSDR.GetID(sSession.AccessCode, sSession.AccessCodeID, Trim(txtExcise.Text))
                sStrMesg = sStrMesg & "Excise " & txtExcise.Text & " And "
            Else
                dExcise = 0
            End If


            dApplicationDate = objSDR.GetApplicationStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dtDetails = objSDR.GetAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderNo, iCommodity, iItemID, iParty, dFromDate, dTo, dDiscount, dVAT, dCST, dExcise, iDispatchNo, dApplicationDate, iZone, iRegion, iArea, iBranch)
            'Session("Report") = dtDetails

            dt = GetGrid(dtDetails)

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesDynamicRpt.rdlc")
            ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
            ReportViewer1.ZoomPercent = 125

            dt2.Columns.Add("Cust_name")
            dt2.Columns.Add("Cust_address")
            dt2.Columns.Add("Cust_email")
            dt2.Columns.Add("Cust_ph")
            dt2.Columns.Add("Cust_vat")
            dt2.Columns.Add("Cust_tax")
            dt2.Columns.Add("Cust_pan")
            dt2.Columns.Add("Cust_tan")
            dt2.Columns.Add("Cust_tin")
            dt2.Columns.Add("Cust_cin")

            dtCust = objSDR.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dtCust.Rows.Count > 0 Then
                dRow = dt2.NewRow()
                dRow("Cust_name") = dtCust.Rows(0)("CUST_NAME")
                dRow("Cust_address") = dtCust.Rows(0)("CUST_Comm_Address")
                dRow("Cust_email") = dtCust.Rows(0)("CUST_Email")
                dRow("Cust_ph") = dtCust.Rows(0)("CUST_Comm_Tel")
                dRow("Cust_vat") = dtCust.Rows(0)("CVAT")
                dRow("Cust_tax") = dtCust.Rows(0)("CTAX")
                dRow("Cust_pan") = dtCust.Rows(0)("CPAN")
                dRow("Cust_tan") = dtCust.Rows(0)("CTAN")
                dRow("Cust_tin") = dtCust.Rows(0)("CTIN")
                dRow("Cust_cin") = dtCust.Rows(0)("CCIN")
                dt2.Rows.Add(dRow)
            End If

            Dim rds1 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesDynamicRpt.rdlc")

            dt3.Columns.Add("BM_name")
            dt3.Columns.Add("BM_address")
            dt3.Columns.Add("BM_email")
            dt3.Columns.Add("BM_ph")
            dt3.Columns.Add("BM_vat")
            dt3.Columns.Add("BM_tax")
            dt3.Columns.Add("BM_pan")
            dt3.Columns.Add("BM_tan")
            dt3.Columns.Add("BM_tin")
            dt3.Columns.Add("BM_cin")
            If ddlParty.SelectedIndex > 0 Then
                dtParty = objSDR.GetPartyDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)

                If dtParty.Rows.Count > 0 Then
                    dRow = dt3.NewRow()
                    dRow("BM_name") = dtParty.Rows(0)("BM_NAME")
                    dRow("BM_address") = dtParty.Rows(0)("BM_Address")
                    dRow("BM_email") = dtParty.Rows(0)("BM_EmailID")
                    dRow("BM_ph") = dtParty.Rows(0)("BM_MobileNo")
                    dRow("BM_vat") = dtParty.Rows(0)("BVAT")
                    dRow("BM_tax") = dtParty.Rows(0)("BTAX")
                    dRow("BM_pan") = dtParty.Rows(0)("BPAN")
                    dRow("BM_tan") = dtParty.Rows(0)("BTAN")
                    dRow("BM_tin") = dtParty.Rows(0)("BTIN")
                    dRow("BM_cin") = dtParty.Rows(0)("BCIN")
                    dt3.Rows.Add(dRow)
                End If
            Else

                dRow = dt3.NewRow()
                dRow("BM_name") = ""
                dRow("BM_address") = ""
                dRow("BM_email") = ""
                dRow("BM_ph") = ""
                dRow("BM_vat") = ""
                dRow("BM_tax") = ""
                dRow("BM_pan") = ""
                dRow("BM_tan") = ""
                dRow("BM_tin") = ""
                dRow("BM_cin") = ""
                dt3.Rows.Add(dRow)

            End If

            Dim rds2 As New ReportDataSource("DataSet3", dt3)
            ReportViewer1.LocalReport.DataSources.Add(rds2)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesDynamicRpt.rdlc")

            Dim AppStartDate As ReportParameter() = New ReportParameter() {New ReportParameter("AppStartDate", dApplicationDate)}
            ReportViewer1.LocalReport.SetParameters(AppStartDate)

            If sStrMesg = "Report based on " Then
                sStrMesg = ""
            End If
            If sStrMesg.EndsWith(" And ") Then
                sStrMesg = sStrMesg.Remove(Len(sStrMesg) - 4)
            End If

            Dim sMesg As ReportParameter() = New ReportParameter() {New ReportParameter("sMesg", sStrMesg)}
            ReportViewer1.LocalReport.SetParameters(sMesg)

            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loaddetails")
        End Try
    End Sub
    Private Function GetGrid(ByVal dtData As DataTable) As DataTable
        Dim dt, dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim sOrderNO As String = "" : Dim sCommodity As String = ""

        Dim iTotalOrderedQty, iTotalAllocatedQty, iTotalDispatchedQty, iTotalPendingQty As Double
        Dim dTotalDiscount, dTotalDiscountAmt, dMRPRate As Double
        Dim dTotalVAT, dTotalVATAmt As Double
        Dim dTotalCST, dTotalCSTAmt As Double
        Dim dTotalExcise, dTotalExciseAmt As Double
        Dim dTotalNetAmt, dBasicAmt As Double

        Dim iOrderID As Integer
        Dim dtDetails As New DataTable
        Dim sOrderID As String = ""
        Dim dShipping, dTradeDiscount, dTradeDiscountAmt As Double

        Dim dUnitTotal, dVAT, dCST, dExcise As Double

        Dim sInvoiceNo As String = "" : Dim sDispatchNo As String = "" : Dim sDispatchDate As String = "" : Dim sOrderDate As String = "" : Dim sParty As String = ""
        Dim sStrDRefNo As String = ""
        Dim dVATDesc As Double : Dim dCSTDesc As Double : Dim dExciseDesc As Double
        Try
            dt1.Columns.Add("OrderNo")
            dt1.Columns.Add("Commodity")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("OrderDate")
            dt1.Columns.Add("Party")
            dt1.Columns.Add("DispatchedNo")
            dt1.Columns.Add("DispatchRefNo")
            dt1.Columns.Add("DispatchedDate")
            dt1.Columns.Add("ShippingRate")
            dt1.Columns.Add("MRPRate")
            dt1.Columns.Add("OrderedQty")
            dt1.Columns.Add("AllocatedQty")
            dt1.Columns.Add("PendingQty")
            dt1.Columns.Add("DispatchedQty")
            dt1.Columns.Add("TradeDiscount")
            dt1.Columns.Add("TradeDiscountAmt")
            dt1.Columns.Add("Discount")
            dt1.Columns.Add("DiscountAmt")
            dt1.Columns.Add("VAT")
            dt1.Columns.Add("VATAmt")
            dt1.Columns.Add("CST")
            dt1.Columns.Add("CSTAmt")
            dt1.Columns.Add("Excise")
            dt1.Columns.Add("ExciseAmt")
            dt1.Columns.Add("BasicPrice")
            dt1.Columns.Add("Total")


            dt = dtData
            Dim dview As New DataView(dt)

            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    iOrderID = dt.Rows(j)("SDM_OrderID")

                    If sOrderID.Contains(iOrderID) = False Then
                        If (iOrderID > 0) Then
                            dview = dt.DefaultView
                            dview.RowFilter = "SDM_OrderID='" & iOrderID & "'"
                            dtDetails = dview.ToTable
                            sOrderID = sOrderID & "," & iOrderID

                            If dtDetails.Rows.Count > 0 Then
                                For i = 0 To dtDetails.Rows.Count - 1
                                    dRow = dt1.NewRow

                                    dRow("OrderNo") = "<B>" & dtDetails.Rows(i)("SPO_OrderCode") & "</B>"
                                    If dRow("OrderNo") = sOrderNO Then
                                        dRow("OrderNo") = ""
                                    End If
                                    sOrderNO = "<B>" & dtDetails.Rows(i)("SPO_OrderCode") & "</B>"

                                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                                    If dRow("Commodity") = sCommodity Then
                                        dRow("Commodity") = ""
                                    End If
                                    sCommodity = dtDetails.Rows(i)("Commodity")

                                    dRow("Description") = dtDetails.Rows(i)("Item")

                                    dRow("OrderDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_OrderDate"), "D")
                                    If dRow("OrderDate") = sOrderDate Then
                                        dRow("OrderDate") = ""
                                    End If
                                    sOrderDate = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_OrderDate"), "D")

                                    dRow("Party") = dtDetails.Rows(i)("Party")
                                    If dRow("Party") = sParty Then
                                        dRow("Party") = ""
                                    End If
                                    sParty = dtDetails.Rows(i)("Party")

                                    dRow("DispatchedNo") = dtDetails.Rows(i)("SDM_Code")
                                    If dRow("DispatchedNo") = sInvoiceNo Then
                                        dRow("DispatchedNo") = ""
                                    End If
                                    sInvoiceNo = dtDetails.Rows(i)("SDM_Code")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_DispatchRefNo")) = False Then
                                        sStrDRefNo = objSDR.GetDispatchRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
                                        dRow("DispatchRefNo") = objSDR.GetDetailsRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dtDetails.Rows(i)("SDM_ID"))

                                    Else
                                        dRow("DispatchRefNo") = ""
                                    End If

                                    dRow("DispatchedDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_DispatchDate"), "D")
                                    If dRow("DispatchedDate") = sDispatchDate Then
                                        dRow("DispatchedDate") = ""
                                    End If
                                    sDispatchDate = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_DispatchDate"), "D")

                                    dShipping = dtDetails.Rows(i)("SDM_ShippingRate")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_ShippingRate")) = False Then
                                        dRow("ShippingRate") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDM_ShippingRate")))
                                    Else
                                        dRow("ShippingRate") = ""
                                    End If

                                    dRow("MRPRate") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Rate")))
                                    dMRPRate = dMRPRate + dRow("MRPRate")

                                    dRow("OrderedQty") = objSDR.getOrderQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dtDetails.Rows(i)("SDD_Rate"), dtDetails.Rows(i)("SDD_CommodityID"), dtDetails.Rows(i)("SDD_DescID"))
                                    'dtDetails.Rows(i)("SPOD_Quantity")

                                    iTotalOrderedQty = objSDR.GetDispatchedOrderTotalQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dt.Rows(j)("SDM_ID"))

                                    If IsDBNull(dtDetails.Rows(i)("SDD_Rate")) = False Then
                                        dRow("AllocatedQty") = objSDR.getPlacedQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dtDetails.Rows(i)("SDD_Rate"), dtDetails.Rows(i)("SDD_CommodityID"), dtDetails.Rows(i)("SDD_DescID"))
                                        'dtDetails.Rows(i)("SAD_PlacedQnt")
                                        If IsDBNull(dRow("AllocatedQty")) = False Then
                                            iTotalAllocatedQty = iTotalAllocatedQty + dRow("AllocatedQty")
                                        Else
                                            iTotalAllocatedQty = iTotalAllocatedQty + 0
                                        End If
                                    End If

                                    dRow("PendingQty") = objSDR.getPendingQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dtDetails.Rows(i)("SDD_Rate"), dtDetails.Rows(i)("SDD_CommodityID"), dtDetails.Rows(i)("SDD_DescID"))
                                    'dtDetails.Rows(i)("SAD_PendingQty")

                                    dRow("DispatchedQty") = dtDetails.Rows(i)("SDD_Quantity")
                                    iTotalDispatchedQty = iTotalDispatchedQty + dRow("DispatchedQty")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_GrandDiscount")) = False Then
                                        dRow("TradeDiscount") = ""
                                        dTradeDiscount = dtDetails.Rows(i)("SDM_GrandDiscount")
                                    Else
                                        dRow("TradeDiscount") = ""
                                    End If

                                    If IsDBNull(dtDetails.Rows(i)("SDM_GrandDiscountAmt")) = False Then
                                        dRow("TradeDiscountAmt") = ""
                                        dTradeDiscountAmt = dtDetails.Rows(i)("SDM_GrandDiscountAmt")
                                    Else
                                        dRow("TradeDiscountAmt") = ""
                                    End If

                                    dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Discount")))
                                    dTotalDiscount = dTotalDiscount + dRow("Discount")

                                    dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_DiscountAmount")))
                                    dTotalDiscountAmt = dTotalDiscountAmt + dRow("DiscountAmt")

                                    dRow("VAT") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_SGST")))
                                    'dVATDesc = objSDR.GetDesc(sSession.AccessCode, sSession.AccessCodeID, dtDetails.Rows(i)("SDD_SGST"))
                                    'dRow("VAT") = dVATDesc
                                    dTotalVAT = dTotalVAT + dRow("VAT")

                                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_SGSTAmount")))
                                    dTotalVATAmt = dTotalVATAmt + dRow("VATAmt")

                                    dRow("CST") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CGST")))
                                    'dCSTDesc = objSDR.GetDesc(sSession.AccessCode, sSession.AccessCodeID, dtDetails.Rows(i)("SDD_CGST"))
                                    'dRow("CST") = dCSTDesc
                                    dTotalCST = dTotalCST + dRow("CST")

                                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CGSTAmount")))
                                    dTotalCSTAmt = dTotalCSTAmt + dRow("CSTAmt")

                                    dRow("Excise") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_IGST")))
                                    'dExciseDesc = objSDR.GetDesc(sSession.AccessCode, sSession.AccessCodeID, dtDetails.Rows(i)("SDD_IGST"))
                                    'dRow("Excise") = dExciseDesc
                                    dTotalExcise = dTotalExcise + dRow("Excise")

                                    dRow("ExciseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_IGSTAmount")))
                                    dTotalExciseAmt = dTotalExciseAmt + dRow("ExciseAmt")

                                    'If dtDetails.Rows(i)("SDD_CSTAmount") > 0 Then
                                    '    dRow("BasicPrice") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") - dtDetails.Rows(i)("SDD_CSTAmount")))
                                    '    dBasicAmt = dBasicAmt + dRow("BasicPrice")
                                    'Else
                                    '    dRow("BasicPrice") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") - dtDetails.Rows(i)("SDD_VatAmount")))
                                    '    dBasicAmt = dBasicAmt + dRow("BasicPrice")
                                    'End If

                                    dRow("BasicPrice") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Amount")))
                                    dBasicAmt = dBasicAmt + dRow("BasicPrice")

                                    dUnitTotal = dtDetails.Rows(i)("SDD_TotalAmount")
                                    dVAT = dtDetails.Rows(i)("SDD_SGSTAmount")
                                    dCST = dtDetails.Rows(i)("SDD_CGSTAmount")
                                    dExcise = dtDetails.Rows(i)("SDD_IGSTAmount")

                                    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dUnitTotal))
                                    dTotalNetAmt = dTotalNetAmt + dtDetails.Rows(i)("SDD_TotalAmount")

                                    dt1.Rows.Add(dRow)

                                    dUnitTotal = 0 : dVAT = 0 : dCST = 0 : dExcise = 0

                                Next

                                dRow = dt1.NewRow
                                dRow("OrderNo") = "<B>" & "Total" & "</B>"
                                dRow("Commodity") = ""
                                dRow("Description") = ""
                                dRow("OrderDate") = ""
                                dRow("Party") = ""
                                dRow("DispatchedDate") = ""
                                dRow("ShippingRate") = "<B>" & dShipping & "</B>"
                                dRow("MRPRate") = ""
                                dRow("OrderedQty") = "<B>" & iTotalOrderedQty & "</B>"
                                dRow("AllocatedQty") = "<B>" & iTotalAllocatedQty & "</B>"
                                dRow("PendingQty") = "<B>" & iTotalOrderedQty - iTotalAllocatedQty & "</B>"
                                dRow("DispatchedQty") = "<B>" & iTotalDispatchedQty & "</B>"
                                dRow("TradeDiscount") = "<B>" & dTradeDiscount & "</B>"
                                dRow("TradeDiscountAmt") = "<B>" & dTradeDiscountAmt & "</B>"
                                dRow("Discount") = ""
                                dRow("DiscountAmt") = "<B>" & dTotalDiscountAmt & "</B>"
                                dRow("VAT") = ""
                                dRow("VATAmt") = "<B>" & dTotalVATAmt & "</B>"
                                dRow("CST") = ""
                                dRow("CSTAmt") = "<B>" & dTotalCSTAmt & "</B>"
                                dRow("Excise") = ""
                                dRow("ExciseAmt") = "<B>" & dTotalExciseAmt & "</B>"
                                dRow("BasicPrice") = "<B>" & dBasicAmt & "</B>"

                                'If dTotalCSTAmt > 0 Then
                                '    dRow("Total") = "<B>" & Math.Round((dTotalNetAmt - dTradeDiscountAmt) + dTotalCSTAmt + dTotalExciseAmt + dShipping) & "</B>"
                                'Else
                                '    dRow("Total") = "<B>" & Math.Round((dTotalNetAmt - dTradeDiscountAmt) + dTotalVATAmt + dTotalExciseAmt + dShipping) & "</B>"
                                'End If

                                dRow("Total") = "<B>" & Math.Round((dTotalNetAmt - dTradeDiscountAmt) + dShipping) & "</B>"

                                dt1.Rows.Add(dRow)

                                sCommodity = ""
                                dShipping = 0 : dTradeDiscount = 0 : dTradeDiscountAmt = 0 : dBasicAmt = 0
                                dMRPRate = 0 : iTotalOrderedQty = 0 : iTotalAllocatedQty = 0 : iTotalDispatchedQty = 0 : dTotalDiscount = 0 : dTotalDiscountAmt = 0
                                dTotalVAT = 0 : dTotalVATAmt = 0 : dTotalCST = 0 : dTotalCSTAmt = 0 : dTotalExcise = 0 : dTotalExciseAmt = 0 : dTotalNetAmt = 0 : iTotalPendingQty = 0

                                sInvoiceNo = "" : sDispatchNo = "" : sDispatchDate = "" : sOrderDate = "" : sParty = ""
                            End If

                        End If

                    End If
                Next
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub ddlCommodity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCommodity.SelectedIndexChanged
        Try
            If ddlCommodity.SelectedIndex > 0 Then
                BindDescription(ddlCommodity.SelectedValue)
            Else
                BindDescription(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCommodity_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSink_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSink.Click
        Dim dt As New DataTable
        Dim iOrderNo, iCommodity, iItemID, iParty As Integer
        Dim dFromDate, dTo As Date
        Dim dDiscount, dVAT, dCST, dExcise As Double
        Dim dtDetails As New DataTable

        Dim dt2, dt3, dtCust, dtParty As New DataTable
        Dim dRow As DataRow
        Dim iDispatchNo As Integer
        Dim dApplicationDate As String = ""

        Dim dtVAT As New DataTable : Dim dtCST As New DataTable
        Dim sRVat As String = ""

        Dim sStrMesg As String = ""
        Dim iZone As Integer = 0, iRegion As Integer = 0, iArea As Integer = 0, iBranch As Integer = 0
        Try

            sStrMesg = "Report based on "

            If (ddlAccZone.SelectedIndex > 0) Then
                iZone = ddlAccZone.SelectedValue
                sStrMesg = sStrMesg & "Zone " & ddlAccZone.SelectedItem.Text & " And "
            End If
            If (ddlAccRgn.SelectedIndex > 0) Then
                iRegion = ddlAccRgn.SelectedValue
                sStrMesg = sStrMesg & "Region " & ddlAccRgn.SelectedItem.Text & " And "
            End If
            If (ddlAccArea.SelectedIndex > 0) Then
                iArea = ddlAccArea.SelectedValue
                sStrMesg = sStrMesg & "Area " & ddlAccArea.SelectedItem.Text & " And "
            End If
            If (ddlAccBrnch.SelectedIndex > 0) Then
                iBranch = ddlAccBrnch.SelectedValue
                sStrMesg = sStrMesg & "Branch " & ddlAccBrnch.SelectedItem.Text & " And "
            End If

            If ddlorder.SelectedIndex > 0 Then
                iOrderNo = ddlorder.SelectedValue
                sStrMesg = sStrMesg & "Order No " & ddlorder.SelectedItem.Text & " And "
                If ddlDispatchNo.SelectedIndex > 0 Then
                    iDispatchNo = ddlDispatchNo.SelectedValue
                    sStrMesg = sStrMesg & "Invoice No " & ddlDispatchNo.SelectedItem.Text & " And "
                Else
                    iDispatchNo = 0
                End If
            Else
                iOrderNo = 0
            End If
            If ddlCommodity.SelectedIndex > 0 Then
                iCommodity = ddlCommodity.SelectedValue
                sStrMesg = sStrMesg & "Commodity " & ddlCommodity.SelectedItem.Text & " And "
            Else
                iCommodity = 0
            End If
            If ddlItem.SelectedIndex > 0 Then
                iItemID = ddlItem.SelectedValue
                sStrMesg = sStrMesg & "Item " & ddlItem.SelectedItem.Text & " And "
            Else
                iItemID = 0
            End If
            If ddlParty.SelectedIndex > 0 Then
                iParty = ddlParty.SelectedValue
                sStrMesg = sStrMesg & "Party " & ddlParty.SelectedItem.Text & " And "
            Else
                iParty = 0
            End If
            If txtfrom.Text <> "" Then
                dFromDate = Date.ParseExact(Trim(objGen.FormatDtForRDBMS(txtfrom.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                sStrMesg = sStrMesg & "From Date " & objGen.FormatDtForRDBMS(txtfrom.Text, "D") & " And "
            Else
                dFromDate = "01/01/1900"
            End If
            If txtTo.Text <> "" Then
                dTo = Date.ParseExact(Trim(objGen.FormatDtForRDBMS(txtTo.Text, "D")), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                sStrMesg = sStrMesg & "To Date " & objGen.FormatDtForRDBMS(txtTo.Text, "D") & " And "
            Else
                dTo = "01/01/1900"
            End If
            If txtDiscount.Text <> "" Then
                dDiscount = txtDiscount.Text
                sStrMesg = sStrMesg & "Discount " & txtDiscount.Text & " And "
            Else
                dDiscount = 0
            End If
            If txtvat.Text <> "" Then
                dVAT = txtvat.Text
                sStrMesg = sStrMesg & "VAT " & txtvat.Text & " And "
            Else
                dVAT = 0
            End If
            If txtCst.Text <> "" Then
                dCST = txtCst.Text
                sStrMesg = sStrMesg & "CST " & txtCst.Text & " And "
            Else
                dCST = 0
            End If
            If txtExcise.Text <> "" Then
                dExcise = txtExcise.Text
                sStrMesg = sStrMesg & "Excise " & txtExcise.Text & " And "
            Else
                dExcise = 0
            End If

            dApplicationDate = objSDR.GetApplicationStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dtDetails = objSDR.GetAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderNo, iCommodity, iItemID, iParty, dFromDate, dTo, dDiscount, dVAT, dCST, dExcise, iDispatchNo, dApplicationDate, iZone, iRegion, iArea, iBranch)
            'Session("Report") = dtDetails

            dt = GetSinkGrid(dtDetails)

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesDynamicRpt.rdlc")
            ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
            ReportViewer1.ZoomPercent = 125

            dt2.Columns.Add("Cust_name")
            dt2.Columns.Add("Cust_address")
            dt2.Columns.Add("Cust_email")
            dt2.Columns.Add("Cust_ph")
            dt2.Columns.Add("Cust_vat")
            dt2.Columns.Add("Cust_tax")
            dt2.Columns.Add("Cust_pan")
            dt2.Columns.Add("Cust_tan")
            dt2.Columns.Add("Cust_tin")
            dt2.Columns.Add("Cust_cin")

            dtCust = objSDR.GetCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If dtCust.Rows.Count > 0 Then
                dRow = dt2.NewRow()
                dRow("Cust_name") = dtCust.Rows(0)("CUST_NAME")
                dRow("Cust_address") = dtCust.Rows(0)("CUST_Comm_Address")
                dRow("Cust_email") = dtCust.Rows(0)("CUST_Email")
                dRow("Cust_ph") = dtCust.Rows(0)("CUST_Comm_Tel")
                dRow("Cust_vat") = dtCust.Rows(0)("CVAT")
                dRow("Cust_tax") = dtCust.Rows(0)("CTAX")
                dRow("Cust_pan") = dtCust.Rows(0)("CPAN")
                dRow("Cust_tan") = dtCust.Rows(0)("CTAN")
                dRow("Cust_tin") = dtCust.Rows(0)("CTIN")
                dRow("Cust_cin") = dtCust.Rows(0)("CCIN")
                dt2.Rows.Add(dRow)
            End If

            Dim rds1 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesDynamicRpt.rdlc")

            dt3.Columns.Add("BM_name")
            dt3.Columns.Add("BM_address")
            dt3.Columns.Add("BM_email")
            dt3.Columns.Add("BM_ph")
            dt3.Columns.Add("BM_vat")
            dt3.Columns.Add("BM_tax")
            dt3.Columns.Add("BM_pan")
            dt3.Columns.Add("BM_tan")
            dt3.Columns.Add("BM_tin")
            dt3.Columns.Add("BM_cin")
            If ddlParty.SelectedIndex > 0 Then
                dtParty = objSDR.GetPartyDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlParty.SelectedValue)

                If dtParty.Rows.Count > 0 Then
                    dRow = dt3.NewRow()
                    dRow("BM_name") = dtParty.Rows(0)("BM_NAME")
                    dRow("BM_address") = dtParty.Rows(0)("BM_Address")
                    dRow("BM_email") = dtParty.Rows(0)("BM_EmailID")
                    dRow("BM_ph") = dtParty.Rows(0)("BM_MobileNo")
                    dRow("BM_vat") = dtParty.Rows(0)("BVAT")
                    dRow("BM_tax") = dtParty.Rows(0)("BTAX")
                    dRow("BM_pan") = dtParty.Rows(0)("BPAN")
                    dRow("BM_tan") = dtParty.Rows(0)("BTAN")
                    dRow("BM_tin") = dtParty.Rows(0)("BTIN")
                    dRow("BM_cin") = dtParty.Rows(0)("BCIN")
                    dt3.Rows.Add(dRow)
                End If
            Else

                dRow = dt3.NewRow()
                dRow("BM_name") = ""
                dRow("BM_address") = ""
                dRow("BM_email") = ""
                dRow("BM_ph") = ""
                dRow("BM_vat") = ""
                dRow("BM_tax") = ""
                dRow("BM_pan") = ""
                dRow("BM_tan") = ""
                dRow("BM_tin") = ""
                dRow("BM_cin") = ""
                dt3.Rows.Add(dRow)

            End If

            Dim rds2 As New ReportDataSource("DataSet3", dt3)
            ReportViewer1.LocalReport.DataSources.Add(rds2)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesDynamicRpt.rdlc")

            Dim AppStartDate As ReportParameter() = New ReportParameter() {New ReportParameter("AppStartDate", dApplicationDate)}
            ReportViewer1.LocalReport.SetParameters(AppStartDate)

            If sStrMesg = "Report based on " Then
                sStrMesg = ""
            End If
            If sStrMesg.EndsWith(" And ") Then
                sStrMesg = sStrMesg.Remove(Len(sStrMesg) - 4)
            End If

            Dim sMesg As ReportParameter() = New ReportParameter() {New ReportParameter("sMesg", sStrMesg)}
            ReportViewer1.LocalReport.SetParameters(sMesg)

            ReportViewer1.LocalReport.Refresh()

            ClearAll()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSink_Click")
        End Try
    End Sub
    Private Function GetSinkGrid(ByVal dtData As DataTable) As DataTable
        Dim dt, dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim sOrderNO As String = "" : Dim sCommodity As String = ""

        Dim iTotalOrderedQty, iTotalAllocatedQty, iTotalDispatchedQty, iTotalPendingQty As Double
        Dim dTotalDiscount, dTotalDiscountAmt, dMRPRate As Double
        Dim dTotalVATAmt As Double
        Dim dTotalCST, dTotalCSTAmt As Double
        Dim dTotalExcise, dTotalExciseAmt As Double
        Dim dBasicAmt, dTotalNetAmt As Double

        Dim iOrderID As Integer
        Dim dtDetails As New DataTable
        Dim sOrderID As String = "" : Dim sParty As String = "" : Dim sOrderDate As String = "" : Dim sDispatchDate As String = ""
        Dim dShipping, dTradeDiscount, dTradeDiscountAmt As Double

        Dim sDispatchNo As String = "" : Dim sDispatchRefNo As String = ""
        Dim dUnitTotal, dVAT, dCST, dExcise As Double
        Dim dTotalVAT As Double

        Dim sVat As String = "" : Dim sVatD As String = ""
        Dim sVatA As String = "" : Dim sVatDA As String = ""
        Dim dVatSingleAmt As Double
        Dim dLastVatAmt As Double
        Dim sDisplayVat As String = ""

        Dim sCst As String = "" : Dim sCstD As String = ""
        Dim sCstA As String = "" : Dim sCstDA As String = ""
        Dim dCstSingleAmt As Double
        Dim dLastCstAmt As Double
        Dim sDisplayCst As String = ""

        Dim sArrayV As String() : Dim sbretV As String = "" : Dim sRVat As String = ""
        Dim sArrayC As String() : Dim sbretC As String = "" : Dim sRCst As String = ""

        Dim ReturnStr, temp, ReturnStrC, tempC As String
        Dim sInvoice, sInvoiceStr As String : Dim sInvoiceDate, sInvoiceDateStr As String

        Dim sStrDRefNo As String = ""
        Try
            dt1.Columns.Add("OrderNo")
            dt1.Columns.Add("Commodity")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("OrderDate")
            dt1.Columns.Add("Party")
            dt1.Columns.Add("DispatchedNo")
            dt1.Columns.Add("DispatchRefNo")
            dt1.Columns.Add("DispatchedDate")
            dt1.Columns.Add("ShippingRate")
            dt1.Columns.Add("MRPRate")
            dt1.Columns.Add("OrderedQty")
            dt1.Columns.Add("AllocatedQty")
            dt1.Columns.Add("PendingQty")
            dt1.Columns.Add("DispatchedQty")
            dt1.Columns.Add("TradeDiscount")
            dt1.Columns.Add("TradeDiscountAmt")
            dt1.Columns.Add("Discount")
            dt1.Columns.Add("DiscountAmt")
            dt1.Columns.Add("VAT")
            dt1.Columns.Add("VATAmt")
            dt1.Columns.Add("CST")
            dt1.Columns.Add("CSTAmt")
            dt1.Columns.Add("Excise")
            dt1.Columns.Add("ExciseAmt")
            dt1.Columns.Add("BasicPrice")
            dt1.Columns.Add("Total")

            dt = dtData
            Dim dview As New DataView(dt)

            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    iOrderID = dt.Rows(j)("SDM_OrderID")

                    sStrDRefNo = objSDR.GetDispatchRefNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)

                    If sOrderID.Contains(iOrderID) = False Then
                        If (iOrderID > 0) Then
                            dview = dt.DefaultView
                            dview.RowFilter = "SDM_OrderID='" & iOrderID & "'"
                            dtDetails = dview.ToTable
                            sOrderID = sOrderID & "," & iOrderID

                            If dtDetails.Rows.Count > 0 Then
                                For i = 0 To dtDetails.Rows.Count - 1
                                    dRow = dt1.NewRow

                                    dRow("OrderNo") = dtDetails.Rows(i)("SPO_OrderCode")
                                    If dRow("OrderNo") = sOrderNO Then
                                        dRow("OrderNo") = ""
                                    End If
                                    sOrderNO = dtDetails.Rows(i)("SPO_OrderCode")

                                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                                    If dRow("Commodity") = sCommodity Then
                                        dRow("Commodity") = ""
                                    End If
                                    sCommodity = dtDetails.Rows(i)("Commodity")

                                    dRow("Description") = dtDetails.Rows(i)("Item")

                                    dRow("OrderDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_OrderDate"), "D")
                                    sOrderDate = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_OrderDate"), "D")

                                    dRow("Party") = dtDetails.Rows(i)("Party")
                                    sParty = dtDetails.Rows(i)("Party")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_Code")) = False Then
                                        dRow("DispatchedNo") = dtDetails.Rows(i)("SDM_Code")
                                        sDispatchNo = sDispatchNo & "," & dtDetails.Rows(i)("SDM_Code")
                                    Else
                                        dRow("DispatchedNo") = ""
                                        sDispatchNo = sDispatchNo & "," & ""
                                    End If

                                    sDispatchRefNo = sStrDRefNo

                                    dRow("DispatchedDate") = objGen.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_DispatchDate"), "D")
                                    sDispatchDate = sDispatchDate & "," & objGen.FormatDtForRDBMS(dtDetails.Rows(i)("SDM_DispatchDate"), "D")

                                    dShipping = dShipping + dtDetails.Rows(i)("SDM_ShippingRate")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_ShippingRate")) = False Then
                                        dRow("ShippingRate") = dtDetails.Rows(i)("SDM_ShippingRate")
                                    Else
                                        dRow("ShippingRate") = ""
                                    End If


                                    dRow("MRPRate") = dtDetails.Rows(i)("SDD_Rate")
                                    dMRPRate = dMRPRate + dRow("MRPRate")

                                    dRow("OrderedQty") = dtDetails.Rows(i)("SPOD_Quantity")

                                    iTotalOrderedQty = objSDR.GetDispatchedOrderTotalQty(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dt.Rows(j)("SDM_ID"))

                                    If IsDBNull(dtDetails.Rows(i)("SAD_PlacedQnt")) = False Then
                                        dRow("AllocatedQty") = dtDetails.Rows(i)("SAD_PlacedQnt")
                                        iTotalAllocatedQty = iTotalAllocatedQty + dRow("AllocatedQty")
                                    End If

                                    dRow("PendingQty") = dtDetails.Rows(i)("SAD_PendingQty")

                                    dRow("DispatchedQty") = dtDetails.Rows(i)("SDD_Quantity")
                                    iTotalDispatchedQty = iTotalDispatchedQty + dRow("DispatchedQty")

                                    If IsDBNull(dtDetails.Rows(i)("SDM_GrandDiscount")) = False Then
                                        dRow("TradeDiscount") = ""
                                        dTradeDiscount = dtDetails.Rows(i)("SDM_GrandDiscount")
                                    Else
                                        dRow("TradeDiscount") = ""
                                    End If

                                    If IsDBNull(dtDetails.Rows(i)("SDM_GrandDiscountAmt")) = False Then
                                        dRow("TradeDiscountAmt") = ""
                                        dTradeDiscountAmt = dtDetails.Rows(i)("SDM_GrandDiscountAmt")
                                    Else
                                        dRow("TradeDiscountAmt") = ""
                                    End If

                                    dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Discount")))
                                    dTotalDiscount = dRow("Discount")

                                    dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_DiscountAmount")))
                                    dTotalDiscountAmt = dTotalDiscountAmt + dRow("DiscountAmt")

                                    sVat = sVat & "," & dtDetails.Rows(i)("SDD_SGST")

                                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_SGSTAmount")))
                                    dLastVatAmt = dLastVatAmt + dRow("VATAmt")

                                    sCst = sCst & "," & dtDetails.Rows(i)("SDD_CGST")

                                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CGSTAmount")))
                                    dLastCstAmt = dLastCstAmt + dRow("CSTAmt")

                                    dRow("Excise") = dtDetails.Rows(i)("SDD_IGST")
                                    dTotalExcise = dTotalExcise + dRow("Excise")

                                    dRow("ExciseAmt") = dtDetails.Rows(i)("SDD_IGSTAmount")
                                    dTotalExciseAmt = dTotalExciseAmt + dRow("ExciseAmt")

                                    dRow("BasicPrice") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_Amount")))
                                    dBasicAmt = dBasicAmt + dRow("BasicPrice")

                                    dUnitTotal = dtDetails.Rows(i)("SDD_TotalAmount")
                                    dVAT = dtDetails.Rows(i)("SDD_SGSTAmount")
                                    dCST = dtDetails.Rows(i)("SDD_CGSTAmount")
                                    dExcise = dtDetails.Rows(i)("SDD_IGSTAmount")

                                    dRow("Total") = String.Format("{0:0.00}", Convert.ToDecimal(dUnitTotal))
                                    dTotalNetAmt = dTotalNetAmt + dtDetails.Rows(i)("SDD_TotalAmount")

                                    dUnitTotal = 0 : dVAT = 0 : dCST = 0 : dExcise = 0
                                    'dt1.Rows.Add(dRow)
                                Next

                                If sVat.StartsWith(",") Then
                                    sVat = sVat.Remove(0, 1)
                                End If
                                If sVat.EndsWith(",") Then
                                    sVat = sVat.Remove(Len(sVat) - 1, 1)
                                End If

                                sbretV = sVat
                                sArrayV = sbretV.Split(",")
                                For i = 0 To sArrayV.Length - 1
                                    If sArrayV(i) <> "0.0000" And sArrayV(i) <> "0" Then
                                        sRVat = sRVat & "," & sArrayV(i)
                                    End If
                                Next
                                If sRVat.StartsWith(",") Then
                                    sRVat = sRVat.Remove(0, 1)
                                End If
                                If sRVat.EndsWith(",") Then
                                    sRVat = sRVat.Remove(Len(sRVat) - 1, 1)
                                End If

                                ReturnStr = sRVat
                                temp = String.Join(",", ReturnStr.Split(","c).Distinct().ToArray())

                                If sCst.StartsWith(",") Then
                                    sCst = sCst.Remove(0, 1)
                                End If
                                If sCst.EndsWith(",") Then
                                    sCst = sCst.Remove(Len(sCst) - 1, 1)
                                End If

                                sbretC = sCst
                                sArrayC = sbretC.Split(",")
                                For i = 0 To sArrayC.Length - 1
                                    If sArrayC(i) <> "0.0000" And sArrayC(i) <> "0" Then
                                        sRCst = sRCst & "," & sArrayC(i)
                                    End If
                                Next
                                If sRCst.StartsWith(",") Then
                                    sRCst = sRCst.Remove(0, 1)
                                End If
                                If sRCst.EndsWith(",") Then
                                    sRCst = sRCst.Remove(Len(sRCst) - 1, 1)
                                End If

                                ReturnStrC = sRCst
                                tempC = String.Join(",", ReturnStrC.Split(","c).Distinct().ToArray())


                                If sDispatchNo.StartsWith(",") Then
                                    sDispatchNo = sDispatchNo.Remove(0, 1)
                                End If
                                If sDispatchNo.EndsWith(",") Then
                                    sDispatchNo = sDispatchNo.Remove(Len(sDispatchNo) - 1, 1)
                                End If

                                If sDispatchRefNo.StartsWith(",") Then
                                    sDispatchRefNo = sDispatchRefNo.Remove(0, 1)
                                End If
                                If sDispatchRefNo.EndsWith(",") Then
                                    sDispatchRefNo = sDispatchRefNo.Remove(Len(sDispatchRefNo) - 1, 1)
                                End If

                                If sDispatchDate.StartsWith(",") Then
                                    sDispatchDate = sDispatchDate.Remove(0, 1)
                                End If
                                If sDispatchDate.EndsWith(",") Then
                                    sDispatchDate = sDispatchDate.Remove(Len(sDispatchDate) - 1, 1)
                                End If

                                sInvoiceStr = sDispatchNo
                                sInvoice = String.Join(",", sInvoiceStr.Split(","c).Distinct().ToArray())

                                sInvoiceDateStr = sDispatchDate
                                sInvoiceDate = String.Join(",", sInvoiceDateStr.Split(","c).Distinct().ToArray())

                                dRow = dt1.NewRow
                                dRow("OrderNo") = "<B>" & sOrderNO & "</B>"
                                dRow("Commodity") = ""
                                dRow("Description") = ""
                                dRow("OrderDate") = sOrderDate
                                dRow("Party") = sParty
                                dRow("DispatchedNo") = sInvoice
                                dRow("DispatchRefNo") = sDispatchRefNo
                                dRow("DispatchedDate") = sInvoiceDate
                                dRow("ShippingRate") = "<B>" & dShipping & "</B>"
                                dRow("MRPRate") = ""
                                dRow("OrderedQty") = ""
                                dRow("AllocatedQty") = ""
                                dRow("PendingQty") = ""
                                dRow("DispatchedQty") = ""
                                dRow("TradeDiscount") = "<B>" & dTradeDiscount & "</B>"
                                dRow("TradeDiscountAmt") = "<B>" & dTradeDiscountAmt & "</B>"
                                dRow("Discount") = "<B>" & dTotalDiscount & "</B>"
                                dRow("DiscountAmt") = "<B>" & dTotalDiscountAmt & "</B>"
                                dRow("VAT") = ""
                                dRow("VATAmt") = "<B>" & dLastVatAmt & "</B>"
                                dRow("CST") = ""
                                dRow("CSTAmt") = "<B>" & dLastCstAmt & "</B>"
                                dRow("Excise") = ""
                                dRow("ExciseAmt") = "<B>" & dTotalExciseAmt & "</B>"
                                dRow("BasicPrice") = "<B>" & dBasicAmt & "</B>"
                                'If dLastCstAmt > 0 Then
                                '    dRow("Total") = "<B>" & Math.Round((dTotalNetAmt - dTradeDiscountAmt) + dLastCstAmt + dTotalExciseAmt + dShipping) & "</B>"
                                'Else
                                '    dRow("Total") = "<B>" & Math.Round((dTotalNetAmt - dTradeDiscountAmt) + dLastVatAmt + dTotalExciseAmt + dShipping) & "</B>"
                                'End If
                                dRow("Total") = "<B>" & Math.Round((dTotalNetAmt - dTradeDiscountAmt) + dShipping) & "</B>"
                                dt1.Rows.Add(dRow)

                                dShipping = 0 : dTradeDiscountAmt = 0 : dTradeDiscountAmt = 0
                                dMRPRate = 0 : iTotalOrderedQty = 0 : iTotalAllocatedQty = 0 : iTotalDispatchedQty = 0 : dTotalDiscount = 0 : dTotalDiscountAmt = 0
                                dTotalVAT = 0 : dTotalVATAmt = 0 : dTotalCST = 0 : dTotalCSTAmt = 0 : dTotalExcise = 0 : dTotalExciseAmt = 0 : dTotalNetAmt = 0 : iTotalPendingQty = 0
                                sDispatchNo = "" : sDispatchRefNo = "" : sDispatchDate = ""
                                sVat = "" : sVatD = "" : sDisplayVat = "" : sVatDA = "" : dVatSingleAmt = 0 : dLastVatAmt = 0 : dBasicAmt = 0
                                sCst = "" : sCstD = "" : sDisplayCst = "" : sCstDA = "" : dCstSingleAmt = 0 : dLastCstAmt = 0
                                temp = "" : tempC = "" : ReturnStr = "" : ReturnStrC = "" : sRVat = "" : sRCst = ""

                                sInvoice = "" : sInvoiceStr = "" : sInvoiceDate = "" : sInvoiceDateStr = ""
                                sStrDRefNo = ""
                            End If

                        End If

                    End If
                Next
            End If
            'Session("Report") = dt1
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub ddlorder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlorder.SelectedIndexChanged
        Try
            If ddlorder.SelectedIndex > 0 Then
                LoadDispatchNo(ddlorder.SelectedValue)
            Else
                ddlDispatchNo.Items.Clear()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlorder_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadDispatchNo(ByVal iOrderID As Integer)
        Try
            ddlDispatchNo.DataSource = objSDR.BindDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            ddlDispatchNo.DataTextField = "SDM_Code"
            ddlDispatchNo.DataValueField = "SDM_ID"
            ddlDispatchNo.DataBind()
            ddlDispatchNo.Items.Insert(0, New ListItem("--- Select Dispatch No. ---", "0"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDispatchNo")
        End Try
    End Sub

    Private Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Try
            lblError.Text = ""
            LoadGrideDetails()
            ClearAll()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "brnSearch_Click")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccZone_SelectedIndexChanged")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged")
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
End Class
