Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Reports_Viewer_SalesInvoiceItemWiseRpt
    Inherits System.Web.UI.Page
    Private sFormName As String = "Reports_Viewer_SalesInvoiceItemWiseRpt"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objItemWise As New ClsItemWiseSalesInvoice
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iOrderID As Integer
        Dim iDispatchID As Integer
        Try
            sSession = Session("AllSession")
            lblError.Text = ""
            If IsPostBack = False Then
                iOrderID = Request.QueryString("ExistingOrder")
                iDispatchID = Request.QueryString("ExistingDispatch")
                If iOrderID > 0 Then
                    loadgrid(iOrderID, iDispatchID)
                End If
                'CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                'LoadInvoice()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    'Public Sub CheckAuidtPermission(ByVal sNameSpace As String, ByVal iUsrId As Integer)
    '    Dim sbret As String
    '    Try
    '        sbret = clsGeneralMaster.CheckUmsPermit(sNameSpace, sSession.AccessCodeID, iUsrId, "FasDORIN", "ALL")
    '        If sbret = "False" Or sbret = "" Then
    '            Response.Redirect("~/Permissions/SalesPermission.aspx")
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub LoadInvoice()
        Try
            ddlInvoice.DataSource = objItemWise.Invoice(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlInvoice.DataTextField = "SPO_OrderCode"
            ddlInvoice.DataValueField = "SPO_ID"
            ddlInvoice.DataBind()
            ddlInvoice.Items.Insert(0, New ListItem("--- Select Invoice No. ---", "0"))
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCommodity")
        End Try
    End Sub
    Private Sub LoadDispatchNo(ByVal iOrderID As Integer)
        Try
            ddlDispatch.DataSource = objItemWise.BindDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            ddlDispatch.DataTextField = "SDM_Code"
            ddlDispatch.DataValueField = "SDM_ID"
            ddlDispatch.DataBind()
            ddlDispatch.Items.Insert(0, New ListItem("--- Select Dispatch No. ---", "0"))
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCommodity")
        End Try
    End Sub
    Protected Sub ddlInvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInvoice.SelectedIndexChanged
        Try
            If ddlInvoice.SelectedIndex > 0 Then
                LoadDispatchNo(ddlInvoice.SelectedValue)
            Else
                ddlDispatch.Items.Clear()
            End If
            'loadgrid()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInvoice_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub loadgrid(ByVal iOrderID As Integer, ByVal iDispatchID As Integer)
        Dim sOrderType As String = ""
        Dim dt As New DataTable
        Try
            sOrderType = objItemWise.GetOrderType(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            If sOrderType = "S" Then
                loadAllocationDetails(sSession.AccessCode, sSession.AccessCodeID, iOrderID, iDispatchID)
            ElseIf sOrderType = "O" Then
                loadOralDetails(sSession.AccessCode, sSession.AccessCodeID, iOrderID, iDispatchID)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub loadAllocationDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvice As Integer, ByVal iDispatchID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer

        Dim dAmountTot, dVATTot, dCSTTot, dExciseTot, dGrandDis, dGrandDiscountAmt, dShipping As Double
        Dim TotalinWord As String = ""
        Dim iTotalQty As Integer

        Dim intpart As Integer
        Dim decpart As Double
        Dim dDiscountAmtTotal, dGSTAmtTotal, dAmtTotal, dOtherChargesTotalAmt As Double
        Try
            dt.Columns.Add("CName")
            dt.Columns.Add("CAddress")
            dt.Columns.Add("CPIN")
            dt.Columns.Add("CTel")
            dt.Columns.Add("CEmail")
            dt.Columns.Add("CVAT")
            dt.Columns.Add("CTAX")
            dt.Columns.Add("CPAN")
            dt.Columns.Add("CTAN")
            dt.Columns.Add("CTIN")
            dt.Columns.Add("CCIN")
            dt.Columns.Add("CGSTN")

            dt.Columns.Add("BName")
            dt.Columns.Add("BAddress")
            dt.Columns.Add("BPIN")
            dt.Columns.Add("BTel")
            dt.Columns.Add("BEmail")
            dt.Columns.Add("BVAT")
            dt.Columns.Add("BTAX")
            dt.Columns.Add("BPAN")
            dt.Columns.Add("BTAN")
            dt.Columns.Add("BTIN")
            dt.Columns.Add("BCIN")
            dt.Columns.Add("BGSTN")

            dt.Columns.Add("OrderNo")
            dt.Columns.Add("OrderDate")
            dt.Columns.Add("DispatchNo")
            dt.Columns.Add("DispatchDate")
            dt.Columns.Add("DispatchedThrough")
            dt.Columns.Add("DispatchRefNo")
            dt.Columns.Add("ESugamNo")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("BuyerOrderNo")
            dt.Columns.Add("BuyerOrderDate")

            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Unit")
            dt.Columns.Add("Qty")
            dt.Columns.Add("MRP")
            dt.Columns.Add("RateAmount")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("Amount")

            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmount")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")
            dt.Columns.Add("Excise")
            dt.Columns.Add("ExciseAmount")
            dt.Columns.Add("GrandDiscount")
            dt.Columns.Add("GrandDiscountAmt")
            dt.Columns.Add("Shipping")

            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalinWord")

            dt.Columns.Add("ManufactureDate")
            dt.Columns.Add("ExpireDate")
            dt.Columns.Add("MRPPrice")

            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("HSNCode")
            dt.Columns.Add("OtherCharges")
            dt.Columns.Add("Amt")

            Dim dtVAT As New DataTable
            dtVAT = objItemWise.GetDispatchVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            Dim dtCST As New DataTable
            dtCST = objItemWise.GetDispatchCST(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            dt1 = objItemWise.GetData(sNameSpace, iCompID, iInvice, iDispatchID)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()

                    dRow("CName") = dt1.Rows(i)("CUST_NAME")
                    dRow("CAddress") = dt1.Rows(i)("SDM_DeliveryFrom")
                    dRow("CPIN") = dt1.Rows(i)("CUST_PIN")
                    dRow("CTel") = dt1.Rows(i)("CUST_Comm_Tel")
                    dRow("CEmail") = dt1.Rows(i)("CUST_Email")

                    dRow("CVAT") = dt1.Rows(i)("CVAT")
                    dRow("CTAX") = dt1.Rows(i)("CTAX")
                    dRow("CPAN") = dt1.Rows(i)("CPAN")
                    dRow("CTAN") = dt1.Rows(i)("CTAN")
                    dRow("CTIN") = dt1.Rows(i)("CTIN")
                    dRow("CCIN") = dt1.Rows(i)("CCIN")
                    dRow("CGSTN") = dt1.Rows(i)("SDM_DeliveryFromGSTNRegNo")

                    dRow("BName") = dt1.Rows(i)("BM_Name")
                    dRow("BAddress") = dt1.Rows(i)("SDM_DeliveryAddress")
                    dRow("BPIN") = dt1.Rows(i)("BM_PinCode")
                    dRow("BTel") = dt1.Rows(i)("BM_MobileNo")
                    dRow("BEmail") = dt1.Rows(i)("BM_EmailID")

                    dRow("BVAT") = dt1.Rows(i)("BVAT")
                    dRow("BTAX") = dt1.Rows(i)("BTAX")
                    dRow("BPAN") = dt1.Rows(i)("BPAN")
                    dRow("BTAN") = dt1.Rows(i)("BTAN")
                    dRow("BTIN") = dt1.Rows(i)("BTIN")
                    dRow("BCIN") = dt1.Rows(i)("BCIN")
                    dRow("BGSTN") = dt1.Rows(i)("SDM_DeliveryGSTNRegNo")

                    dRow("OrderNo") = dt1.Rows(i)("SPO_OrderCode")
                    dRow("OrderDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_OrderDate"), "D")
                    dRow("DispatchNo") = dt1.Rows(i)("SDM_Code")
                    If objGen.FormatDtForRDBMS(dt1.Rows(i)("SDM_DispatchDate"), "D") <> "30/12/1899" And (objGen.FormatDtForRDBMS(dt1.Rows(i)("SDM_DispatchDate"), "D") <> "30-12-1899") Then
                        dRow("DispatchDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SDM_DispatchDate"), "D")
                    Else
                        dRow("DispatchDate") = ""
                    End If

                    dRow("DispatchedThrough") = dt1.Rows(i)("ModeOfDispatch")
                    If IsDBNull(dt1.Rows(i)("SDM_DispatchRefNo")) = False Then
                        dRow("DispatchRefNo") = dt1.Rows(i)("SDM_DispatchRefNo")
                    Else
                        dRow("DispatchRefNo") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("SDM_ESugamNo")) = False Then
                        dRow("ESugamNo") = dt1.Rows(i)("SDM_ESugamNo")
                    Else
                        dRow("ESugamNo") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("SDM_Remarks")) = False Then
                        dRow("Remarks") = dt1.Rows(i)("SDM_Remarks")
                    Else
                        dRow("Remarks") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("SPO_BuyerOrderNo")) = False Then
                        dRow("BuyerOrderNo") = dt1.Rows(i)("SPO_BuyerOrderNo")
                    Else
                        dRow("BuyerOrderNo") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("SPO_BuyerOrderDate")) = False Then
                        If objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_BuyerOrderDate"), "D") <> "30/12/1899" And (objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_BuyerOrderDate"), "D") <> "30-12-1899") Then
                            dRow("BuyerOrderDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_BuyerOrderDate"), "D")
                        Else
                            dRow("BuyerOrderDate") = ""
                        End If
                    Else
                        dRow("BuyerOrderDate") = ""
                    End If

                    dRow("SlNo") = i + 1
                    dRow("Commodity") = dt1.Rows(i)("Commodity")
                    dRow("Description") = dt1.Rows(i)("INV_Code")
                    dRow("Unit") = dt1.Rows(i)("Unit")
                    dRow("Qty") = dt1.Rows(i)("SDD_Quantity")
                    iTotalQty = iTotalQty + dt1.Rows(i)("SDD_Quantity")

                    dRow("MRP") = dt1.Rows(i)("SDD_Rate")
                    dRow("RateAmount") = dt1.Rows(i)("SDD_RateAmount")
                    dRow("Discount") = dt1.Rows(i)("SDD_Discount")
                    dRow("DiscountAmt") = dt1.Rows(i)("SDD_DiscountAmount")
                    dDiscountAmtTotal = dDiscountAmtTotal + dt1.Rows(i)("SDD_DiscountAmount")

                    'If IsDBNull(dt1.Rows(i)("SDD_CST")) = False And dt1.Rows(i)("SDD_CST") > 0 Then
                    '    dRow("Amount") = dt1.Rows(i)("SDD_TotalAmount") - dt1.Rows(i)("SDD_CSTAmount")
                    '    dAmountTot = dAmountTot + dRow("Amount")
                    'Else
                    '    dRow("Amount") = dt1.Rows(i)("SDD_TotalAmount") - dt1.Rows(i)("SDD_VATAmount")
                    '    dAmountTot = dAmountTot + dRow("Amount")
                    'End If

                    dRow("Amount") = dt1.Rows(i)("SDD_TotalAmount")
                    dAmountTot = dAmountTot + dRow("Amount")

                    dRow("VAT") = dt1.Rows(i)("SDD_GSTRate")
                    'objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt1.Rows(i)("SDD_VAT") & " And MAS_CompID=" & sSession.AccessCodeID & " ")
                    dRow("VATAmount") = dt1.Rows(i)("SDD_GSTAmount")
                    dVATTot = dVATTot + dt1.Rows(i)("SDD_GSTAmount")

                    dRow("CST") = ""
                    'objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt1.Rows(i)("SDD_CST") & " And MAS_CompID=" & sSession.AccessCodeID & " ")
                    dRow("CSTAmount") = 0
                    'dt1.Rows(i)("SDD_CSTAmount")
                    dCSTTot = dCSTTot + dt1.Rows(i)("SDD_CSTAmount")

                    dRow("Excise") = ""
                    'dt1.Rows(i)("SDD_Excise")
                    dRow("ExciseAmount") = 0
                    'dt1.Rows(i)("SDD_ExciseAmount")
                    dExciseTot = dExciseTot + dt1.Rows(i)("SDD_ExciseAmount")

                    dRow("GrandDiscount") = dt1.Rows(i)("TradeDiscount")
                    dGrandDis = dt1.Rows(i)("TradeDiscount")

                    dRow("GrandDiscountAmt") = dt1.Rows(i)("TradeDisAmt")
                    dGrandDiscountAmt = dt1.Rows(i)("TradeDisAmt")

                    'dRow("Shipping") = dt1.Rows(i)("SDM_ShippingRate")
                    'dShipping = dt1.Rows(i)("SDM_ShippingRate")
                    dShipping = objItemWise.GetChargedAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)
                    dRow("Shipping") = dShipping

                    If IsDBNull(dt1.Rows(i)("PGD_ManufactureDate")) = False Then
                        dRow("ManufactureDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("PGD_ManufactureDate"), "D")
                    Else
                        dRow("ManufactureDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("PGD_ExpireDate")) = False Then
                        dRow("ExpireDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("PGD_ExpireDate"), "D")
                    Else
                        dRow("ExpireDate") = ""
                    End If

                    'If IsDBNull(dt1.Rows(i)("INVH_MRP")) = False Then
                    '    dRow("MRPPrice") = dt1.Rows(i)("INVH_MRP")
                    'Else
                    '    dRow("MRPPrice") = ""
                    'End If
                    If IsDBNull(dt1.Rows(i)("SDD_Rate")) = False Then
                        dRow("MRPPrice") = dt1.Rows(i)("SDD_Rate")
                    Else
                        dRow("MRPPrice") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("SDD_GSTRate")) = False Then
                        dRow("GSTRate") = dt1.Rows(i)("SDD_GSTRate")
                    Else
                        dRow("GSTRate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("SDD_GSTAmount")) = False Then
                        dRow("GSTAmount") = dt1.Rows(i)("SDD_GSTAmount")
                        dGSTAmtTotal = dGSTAmtTotal + dt1.Rows(i)("SDD_GSTAmount")
                    Else
                        dRow("GSTAmount") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("GST_CHST")) = False Then
                        dRow("HSNCode") = dt1.Rows(i)("GST_CHST")
                    Else
                        dRow("HSNCode") = ""
                    End If

                    'Dim dTotalValue As Double
                    'dTotalValue = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details Where SDD_MasterID=" & iDispatchID & " And SDD_CompID=" & iCompID & " ")
                    If IsDBNull(dt1.Rows(i)("SDD_ChargesPerItem")) = False Then
                        dRow("OtherCharges") = dt1.Rows(i)("SDD_ChargesPerItem")
                        dOtherChargesTotalAmt = dOtherChargesTotalAmt + dt1.Rows(i)("SDD_ChargesPerItem")
                    Else
                        dRow("OtherCharges") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("SDD_Amount")) = False Then
                        dRow("Amt") = dt1.Rows(i)("SDD_Amount")
                        dAmtTotal = dAmtTotal + dt1.Rows(i)("SDD_Amount")
                    Else
                        dRow("Amt") = ""
                    End If

                    dt.Rows.Add(dRow)
                Next
            End If

            'dRow = dt.NewRow()
            'dRow("Amount") = dAmountTot
            'dRow("VATAmount") = dVATTot
            'dRow("CSTAmount") = dCSTTot
            'dRow("ExciseAmount") = dExciseTot
            'dRow("GrandDiscount") = dGrandDis
            'dRow("GrandDiscountAmt") = dGrandDiscountAmt
            'dRow("Shipping") = dShipping


            Dim sVat As String = ""
            If dtVAT.Rows.Count = 1 Then
                sVat = dtVAT.Rows(0)("SDD_GSTRate")
                'objItemWise.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dtVAT.Rows(0)("SDD_VAT"))
                'dVATTot = Math.Round(((((dAmountTot - dGrandDiscountAmt) + dExciseTot) * sVat) / 100), 2)
                dVATTot = Math.Round(((((dAmountTot - dGrandDiscountAmt)) * sVat) / 100), 2)
            End If

            'Dim sCst As String = ""
            'If dtCST.Rows.Count = 1 Then
            '    sCst = objItemWise.GetCST(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dtCST.Rows(0)("SDD_CST"))
            '    dCSTTot = Math.Round(((((dAmountTot - dGrandDiscountAmt) + dExciseTot) * sCst) / 100), 2)
            'End If

            Dim dRoundTotal As Double
            Dim sSFinalTotal As String = ""

            'If dCSTTot > 0 Then
            '    dRoundTotal = (((dAmountTot - dGrandDiscountAmt) + dExciseTot + dCSTTot) + dShipping)
            'Else
            'dRoundTotal = (((dAmountTot - dGrandDiscountAmt) + dExciseTot + dVATTot) + dShipping)
            dRoundTotal = ((dAmountTot - dGrandDiscountAmt) + dExciseTot)
            'End If

            dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dRoundTotal)))

            'Round Off'
            intpart = CType(dRoundTotal, Integer)
            decpart = String.Format("{0:0.00}", Convert.ToDecimal(dRoundTotal - intpart))
            'Round Off'

            sSFinalTotal = Math.Round(dRoundTotal)
            'TotalinWord = NumberToWord(sSFinalTotal) & "Only"
            TotalinWord = GetInWords(sSFinalTotal)
            'TotalinWord = NumberToWord(String.Format("{0:0.00}", dRow("GrandTotal"))) & " Only"
            dRow("TotalinWord") = TotalinWord

            'dt.Rows.Add(dRow)

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")

            Dim NetAmount As ReportParameter() = New ReportParameter() {New ReportParameter("NetAmount", dAmountTot)}
            ReportViewer1.LocalReport.SetParameters(NetAmount)

            Dim TradeDiscount As ReportParameter() = New ReportParameter() {New ReportParameter("TradeDiscount", dGrandDis)}
            ReportViewer1.LocalReport.SetParameters(TradeDiscount)

            Dim TradeDisAmt As ReportParameter() = New ReportParameter() {New ReportParameter("TradeDisAmt", dGrandDiscountAmt)}
            ReportViewer1.LocalReport.SetParameters(TradeDisAmt)

            'Dim VATAmount As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmount", dVATTot)}
            'ReportViewer1.LocalReport.SetParameters(VATAmount)
            Dim VATAmount As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmount", 0)}
            ReportViewer1.LocalReport.SetParameters(VATAmount)

            Dim CSTAmount As ReportParameter() = New ReportParameter() {New ReportParameter("CSTAmount", dCSTTot)}
            ReportViewer1.LocalReport.SetParameters(CSTAmount)

            Dim ExciseAmount As ReportParameter() = New ReportParameter() {New ReportParameter("ExciseAmount", dExciseTot)}
            ReportViewer1.LocalReport.SetParameters(ExciseAmount)

            'Dim Shipping As ReportParameter() = New ReportParameter() {New ReportParameter("Shipping", dShipping)}
            'ReportViewer1.LocalReport.SetParameters(Shipping)
            Dim Shipping As ReportParameter() = New ReportParameter() {New ReportParameter("Shipping", 0)}
            ReportViewer1.LocalReport.SetParameters(Shipping)

            Dim GrandTotal As ReportParameter() = New ReportParameter() {New ReportParameter("GrandTotal", String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dRoundTotal))))}
            ReportViewer1.LocalReport.SetParameters(GrandTotal)

            Dim TotalInWords As ReportParameter() = New ReportParameter() {New ReportParameter("TotalInWords", TotalinWord)}
            ReportViewer1.LocalReport.SetParameters(TotalInWords)

            Dim TermsConditions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsConditions", UCase(objItemWise.GetTermsConditions(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(TermsConditions)

            'Dim VAT As ReportParameter() = New ReportParameter() {New ReportParameter("VAT", UCase(objItemWise.GetALLVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice)))}
            Dim VAT As ReportParameter() = New ReportParameter() {New ReportParameter("VAT", 0)}
            ReportViewer1.LocalReport.SetParameters(VAT)

            Dim TotalQty As ReportParameter() = New ReportParameter() {New ReportParameter("TotalQty", iTotalQty)}
            ReportViewer1.LocalReport.SetParameters(TotalQty)

            Dim SalesPersonName As ReportParameter() = New ReportParameter() {New ReportParameter("SalesPersonName", sSession.UserFullName)}
            ReportViewer1.LocalReport.SetParameters(SalesPersonName)

            Dim RoundOffDecimal As ReportParameter() = New ReportParameter() {New ReportParameter("RoundOffDecimal", decpart)}
            ReportViewer1.LocalReport.SetParameters(RoundOffDecimal)

            Dim Heading As ReportParameter() = New ReportParameter() {New ReportParameter("Heading", "Sales Invoice")}
            ReportViewer1.LocalReport.SetParameters(Heading)

            Dim AmountTotal As ReportParameter() = New ReportParameter() {New ReportParameter("AmountTotal", dAmtTotal)}
            ReportViewer1.LocalReport.SetParameters(AmountTotal)

            Dim DiscountAmtTotal As ReportParameter() = New ReportParameter() {New ReportParameter("DiscountAmtTotal", dDiscountAmtTotal)}
            ReportViewer1.LocalReport.SetParameters(DiscountAmtTotal)

            Dim GSTAmtTotal As ReportParameter() = New ReportParameter() {New ReportParameter("GSTAmtTotal", dGSTAmtTotal)}
            ReportViewer1.LocalReport.SetParameters(GSTAmtTotal)

            Dim OtherChargesTotalAmt As ReportParameter() = New ReportParameter() {New ReportParameter("OtherChargesTotalAmt", dOtherChargesTotalAmt)}
            ReportViewer1.LocalReport.SetParameters(OtherChargesTotalAmt)

            Dim dt2 As New DataTable
            'dt2 = objItemWise.GetVATBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            Dim rds1 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")

            Dim dAmount, dVATAmount As Double
            If dt2.Rows.Count > 0 Then
                For j = 0 To dt2.Rows.Count - 1
                    dAmount = dAmount + dt2.Rows(j)("Amount")
                    dVATAmount = dVATAmount + dt2.Rows(j)("VATAmount")
                Next
            End If

            Dim AmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("AmtBifercation", dAmount)}
            ReportViewer1.LocalReport.SetParameters(AmtBifercation)

            Dim VATAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmtBifercation", dVATAmount)}
            ReportViewer1.LocalReport.SetParameters(VATAmtBifercation)


            Dim dt3 As New DataTable
            'dt3 = objItemWise.GetCSTBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            Dim rds2 As New ReportDataSource("DataSet3", dt3)
            ReportViewer1.LocalReport.DataSources.Add(rds2)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")

            Dim dCAmount, dCSTAmount As Double
            If dt3.Rows.Count > 0 Then
                For k = 0 To dt3.Rows.Count - 1
                    dCAmount = dCAmount + dt3.Rows(k)("CAmount")
                    dCSTAmount = dCSTAmount + dt3.Rows(k)("CSTAmount")
                Next
            End If

            Dim CAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("CAmtBifercation", dCAmount)}
            ReportViewer1.LocalReport.SetParameters(CAmtBifercation)

            Dim CSTAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("CSTAmtBifercation", dCSTAmount)}
            ReportViewer1.LocalReport.SetParameters(CSTAmtBifercation)

            Dim dt4 As New DataTable
            dt4 = objItemWise.GetCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            Dim rds3 As New ReportDataSource("DataSet4", dt4)
            ReportViewer1.LocalReport.DataSources.Add(rds3)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")


            Dim dt5 As New DataTable
            dt5 = objItemWise.GetGSTBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iDispatchID)

            Dim rds4 As New ReportDataSource("DataSet5", dt5)
            ReportViewer1.LocalReport.DataSources.Add(rds4)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")

            ReportViewer1.LocalReport.Refresh()


            Dim dtURD As New DataTable
            dtURD = objItemWise.GetURD(sSession.AccessCode, sSession.AccessCodeID, iInvice, iDispatchID)

            Dim rdsURD As New ReportDataSource("DataSet6", dtURD)
            ReportViewer1.LocalReport.DataSources.Add(rdsURD)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")
            ReportViewer1.LocalReport.Refresh()

            Dim dtHSN As New DataTable
            dtHSN = objItemWise.GetHSN(sSession.AccessCode, sSession.AccessCodeID, iInvice, iDispatchID)

            Dim rdsHSN As New ReportDataSource("DataSet7", dtHSN)
            ReportViewer1.LocalReport.DataSources.Add(rdsHSN)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")
            ReportViewer1.LocalReport.Refresh()

            'Return dt   
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAllocationDetails")
        End Try
    End Sub
    Public Sub loadOralDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvice As Integer, ByVal iDispatchID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer

        Dim dAmountTot, dVATTot, dCSTTot, dExciseTot, dGrandDis, dGrandDiscountAmt, dShipping As Double
        Dim TotalinWord As String = ""
        Dim iTotalQty As Integer

        Dim intpart As Integer
        Dim decpart As Double
        Dim dGSTAmtTotal, dAmtTotal, dOtherChargesTotalAmt, dDiscountAmtTotal As Double
        Try
            dt.Columns.Add("CName")
            dt.Columns.Add("CAddress")
            dt.Columns.Add("CPIN")
            dt.Columns.Add("CTel")
            dt.Columns.Add("CEmail")
            dt.Columns.Add("CVAT")
            dt.Columns.Add("CTAX")
            dt.Columns.Add("CPAN")
            dt.Columns.Add("CTAN")
            dt.Columns.Add("CTIN")
            dt.Columns.Add("CCIN")

            dt.Columns.Add("BName")
            dt.Columns.Add("BAddress")
            dt.Columns.Add("BPIN")
            dt.Columns.Add("BTel")
            dt.Columns.Add("BEmail")
            dt.Columns.Add("BVAT")
            dt.Columns.Add("BTAX")
            dt.Columns.Add("BPAN")
            dt.Columns.Add("BTAN")
            dt.Columns.Add("BTIN")
            dt.Columns.Add("BCIN")

            dt.Columns.Add("OrderNo")
            dt.Columns.Add("OrderDate")
            dt.Columns.Add("DispatchNo")
            dt.Columns.Add("DispatchDate")
            dt.Columns.Add("DispatchedThrough")
            dt.Columns.Add("ESugamNo")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("BuyerOrderNo")
            dt.Columns.Add("BuyerOrderDate")
            dt.Columns.Add("DispatchRefNo")

            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Unit")
            dt.Columns.Add("Qty")
            dt.Columns.Add("MRP")
            dt.Columns.Add("RateAmount")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("Amount")

            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmount")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")
            dt.Columns.Add("Excise")
            dt.Columns.Add("ExciseAmount")
            dt.Columns.Add("GrandDiscount")
            dt.Columns.Add("GrandDiscountAmt")
            dt.Columns.Add("Shipping")

            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalinWord")

            dt.Columns.Add("ManufactureDate")
            dt.Columns.Add("ExpireDate")
            dt.Columns.Add("MRPPrice")

            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("HSNCode")
            dt.Columns.Add("OtherCharges")
            dt.Columns.Add("Amt")

            Dim dtVAT As New DataTable
            dtVAT = objItemWise.GetDispatchVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            Dim dtCST As New DataTable
            dtCST = objItemWise.GetDispatchCST(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            dt1 = objItemWise.GetDataOral(sNameSpace, iCompID, iInvice, iDispatchID)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()

                    dRow("CName") = dt1.Rows(i)("CUST_NAME")
                    dRow("CAddress") = dt1.Rows(i)("CUST_Comm_Address")
                    dRow("CPIN") = dt1.Rows(i)("CUST_PIN")
                    dRow("CTel") = dt1.Rows(i)("CUST_Comm_Tel")
                    dRow("CEmail") = dt1.Rows(i)("CUST_Email")

                    dRow("CVAT") = dt1.Rows(i)("CVAT")
                    dRow("CTAX") = dt1.Rows(i)("CTAX")
                    dRow("CPAN") = dt1.Rows(i)("CPAN")
                    dRow("CTAN") = dt1.Rows(i)("CTAN")
                    dRow("CTIN") = dt1.Rows(i)("CTIN")
                    dRow("CCIN") = dt1.Rows(i)("CCIN")

                    dRow("BName") = dt1.Rows(i)("BM_Name")
                    dRow("BAddress") = dt1.Rows(i)("BM_Address")
                    dRow("BPIN") = dt1.Rows(i)("BM_PinCode")
                    dRow("BTel") = dt1.Rows(i)("BM_MobileNo")
                    dRow("BEmail") = dt1.Rows(i)("BM_EmailID")

                    dRow("BVAT") = dt1.Rows(i)("BVAT")
                    dRow("BTAX") = dt1.Rows(i)("BTAX")
                    dRow("BPAN") = dt1.Rows(i)("BPAN")
                    dRow("BTAN") = dt1.Rows(i)("BTAN")
                    dRow("BTIN") = dt1.Rows(i)("BTIN")
                    dRow("BCIN") = dt1.Rows(i)("BCIN")

                    dRow("OrderNo") = dt1.Rows(i)("SPO_OrderCode")
                    dRow("OrderDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_OrderDate"), "D")
                    dRow("DispatchNo") = dt1.Rows(i)("SDM_Code")
                    If objGen.FormatDtForRDBMS(dt1.Rows(i)("SDM_DispatchDate"), "D") <> "30/12/1899" And (objGen.FormatDtForRDBMS(dt1.Rows(i)("SDM_DispatchDate"), "D") <> "30-12-1899") Then
                        dRow("DispatchDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SDM_DispatchDate"), "D")
                    Else
                        dRow("DispatchDate") = ""
                    End If
                    'dRow("DispatchDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SDM_DispatchDate"), "D")
                    dRow("DispatchedThrough") = dt1.Rows(i)("ModeOfDispatch")
                    If IsDBNull(dt1.Rows(i)("SDM_ESugamNo")) = False Then
                        dRow("ESugamNo") = dt1.Rows(i)("SDM_ESugamNo")
                    Else
                        dRow("ESugamNo") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("SDM_Remarks")) = False Then
                        dRow("Remarks") = dt1.Rows(i)("SDM_Remarks")
                    Else
                        dRow("Remarks") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("SPO_BuyerOrderNo")) = False Then
                        dRow("BuyerOrderNo") = dt1.Rows(i)("SPO_BuyerOrderNo")
                    Else
                        dRow("BuyerOrderNo") = ""
                    End If

                    If (objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_BuyerOrderDate"), "D") <> "30/12/1899") And (objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_BuyerOrderDate"), "D") <> "30-12-1899") Then
                        dRow("BuyerOrderDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_BuyerOrderDate"), "D")
                    Else
                        dRow("BuyerOrderDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("SDM_DispatchRefNo")) = False Then
                        dRow("DispatchRefNo") = dt1.Rows(i)("SDM_DispatchRefNo")
                    Else
                        dRow("DispatchRefNo") = ""
                    End If


                    dRow("SlNo") = i + 1
                    dRow("Commodity") = dt1.Rows(i)("Commodity")
                    dRow("Description") = dt1.Rows(i)("INV_Code")
                    dRow("Unit") = dt1.Rows(i)("Unit")
                    dRow("Qty") = dt1.Rows(i)("SDD_Quantity")
                    iTotalQty = iTotalQty + dt1.Rows(i)("SDD_Quantity")

                    dRow("MRP") = dt1.Rows(i)("SDD_Rate")
                    dRow("RateAmount") = dt1.Rows(i)("SDD_RateAmount")
                    dRow("Discount") = dt1.Rows(i)("SDD_Discount")
                    dRow("DiscountAmt") = dt1.Rows(i)("SDD_DiscountAmount")
                    dDiscountAmtTotal = dDiscountAmtTotal + dt1.Rows(i)("SDD_DiscountAmount")

                    'If IsDBNull(dt1.Rows(i)("SDD_CST")) = False And dt1.Rows(i)("SDD_CST") > 0 Then
                    '    dRow("Amount") = dt1.Rows(i)("SDD_TotalAmount") - dt1.Rows(i)("SDD_CSTAmount")
                    '    dAmountTot = dAmountTot + dRow("Amount")
                    'Else
                    '    dRow("Amount") = dt1.Rows(i)("SDD_TotalAmount") - dt1.Rows(i)("SDD_VATAmount")
                    '    dAmountTot = dAmountTot + dRow("Amount")
                    'End If

                    dRow("Amount") = dt1.Rows(i)("SDD_TotalAmount")
                    dAmountTot = dAmountTot + dRow("Amount")

                    dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt1.Rows(i)("SDD_VAT") & " And MAS_CompID=" & sSession.AccessCodeID & " ")
                    dRow("VATAmount") = dt1.Rows(i)("SDD_VATAmount")
                    dVATTot = dVATTot + dt1.Rows(i)("SDD_VATAmount")

                    dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt1.Rows(i)("SDD_CST") & " And MAS_CompID=" & sSession.AccessCodeID & " ")
                    dRow("CSTAmount") = dt1.Rows(i)("SDD_CSTAmount")
                    dCSTTot = dCSTTot + dt1.Rows(i)("SDD_CSTAmount")

                    dRow("Excise") = dt1.Rows(i)("SDD_Excise")
                    dRow("ExciseAmount") = dt1.Rows(i)("SDD_ExciseAmount")
                    dExciseTot = dExciseTot + dt1.Rows(i)("SDD_ExciseAmount")

                    If IsDBNull(dt1.Rows(i)("TradeDiscount")) = False Then
                        dRow("GrandDiscount") = dt1.Rows(i)("TradeDiscount")
                        dGrandDis = dt1.Rows(i)("TradeDiscount")
                    Else
                        dRow("GrandDiscount") = 0
                        dGrandDis = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("TradeDisAmt")) = False Then
                        dRow("GrandDiscountAmt") = dt1.Rows(i)("TradeDisAmt")
                        dGrandDiscountAmt = dt1.Rows(i)("TradeDisAmt")
                    Else
                        dRow("GrandDiscountAmt") = 0
                        dGrandDiscountAmt = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("SDM_ShippingRate")) = False Then
                        'dRow("Shipping") = dt1.Rows(i)("SDM_ShippingRate")
                        'dShipping = dt1.Rows(i)("SDM_ShippingRate")
                        dRow("Shipping") = objItemWise.GetChargedAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)
                        dShipping = objItemWise.GetChargedAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)
                    Else
                        dRow("Shipping") = 0
                        dShipping = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("PGD_ManufactureDate")) = False Then
                        dRow("ManufactureDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("PGD_ManufactureDate"), "D")
                    Else
                        dRow("ManufactureDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("PGD_ExpireDate")) = False Then
                        dRow("ExpireDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("PGD_ExpireDate"), "D")
                    Else
                        dRow("ExpireDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("INVH_MRP")) = False Then
                        dRow("MRPPrice") = dt1.Rows(i)("INVH_MRP")
                    Else
                        dRow("MRPPrice") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("SDD_GSTRate")) = False Then
                        dRow("GSTRate") = dt1.Rows(i)("SDD_GSTRate")
                    Else
                        dRow("GSTRate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("SDD_GSTAmount")) = False Then
                        dRow("GSTAmount") = dt1.Rows(i)("SDD_GSTAmount")
                        dGSTAmtTotal = dGSTAmtTotal + dt1.Rows(i)("SDD_GSTAmount")
                    Else
                        dRow("GSTAmount") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("GST_CHST")) = False Then
                        dRow("HSNCode") = dt1.Rows(i)("GST_CHST")
                    Else
                        dRow("HSNCode") = ""
                    End If

                    'Dim dTotalValue As Double
                    'dTotalValue = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details Where SDD_MasterID=" & iDispatchID & " And SDD_CompID=" & iCompID & " ")
                    If IsDBNull(dt1.Rows(i)("SDD_ChargesPerItem")) = False Then
                        dRow("OtherCharges") = dt1.Rows(i)("SDD_ChargesPerItem")
                        dOtherChargesTotalAmt = dOtherChargesTotalAmt + dt1.Rows(i)("SDD_ChargesPerItem")
                    Else
                        dRow("OtherCharges") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("SDD_Amount")) = False Then
                        dRow("Amt") = dt1.Rows(i)("SDD_Amount")
                        dAmtTotal = dAmtTotal + dt1.Rows(i)("SDD_Amount")
                    Else
                        dRow("Amt") = ""
                    End If

                    dt.Rows.Add(dRow)
                Next
            End If

            'Dim sVat As String = ""
            'If dtVAT.Rows.Count = 1 Then
            '    sVat = objItemWise.GetVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dtVAT.Rows(0)("SDD_VAT"))
            '    dVATTot = Math.Round(((((dAmountTot - dGrandDiscountAmt) + dExciseTot) * sVat) / 100), 2)
            'End If

            'Dim sCst As String = ""
            'If dtCST.Rows.Count = 1 Then
            '    sCst = objItemWise.GetCST(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dtCST.Rows(0)("SDD_CST"))
            '    dCSTTot = Math.Round(((((dAmountTot - dGrandDiscountAmt) + dExciseTot) * sCst) / 100), 2)
            'End If

            Dim dRoundTotal As Double
            Dim sSFinalTotal As String = ""

            'If dCSTTot > 0 Then
            '    dRoundTotal = (((dAmountTot - dGrandDiscountAmt) + dExciseTot + dCSTTot) + dShipping)
            'Else
            '    dRoundTotal = (((dAmountTot - dGrandDiscountAmt) + dExciseTot + dVATTot) + dShipping)
            'End If
            dRoundTotal = ((dAmountTot - dGrandDiscountAmt) + dExciseTot)

            dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dRoundTotal)))

            'Round Off'
            intpart = CType(dRoundTotal, Integer)
            decpart = String.Format("{0:0.00}", Convert.ToDecimal(dRoundTotal - intpart))
            'Round Off'

            sSFinalTotal = Math.Round(dRoundTotal)
            'TotalinWord = NumberToWord(sSFinalTotal) & "Only"
            TotalinWord = GetInWords(sSFinalTotal)
            'TotalinWord = NumberToWord(String.Format("{0:0.00}", dRow("GrandTotal"))) & " Only"
            dRow("TotalinWord") = TotalinWord

            'dt.Rows.Add(dRow)

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")

            Dim NetAmount As ReportParameter() = New ReportParameter() {New ReportParameter("NetAmount", dAmountTot)}
            ReportViewer1.LocalReport.SetParameters(NetAmount)

            Dim TradeDiscount As ReportParameter() = New ReportParameter() {New ReportParameter("TradeDiscount", dGrandDis)}
            ReportViewer1.LocalReport.SetParameters(TradeDiscount)

            Dim TradeDisAmt As ReportParameter() = New ReportParameter() {New ReportParameter("TradeDisAmt", dGrandDiscountAmt)}
            ReportViewer1.LocalReport.SetParameters(TradeDisAmt)

            Dim VATAmount As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmount", 0)}
            ReportViewer1.LocalReport.SetParameters(VATAmount)

            Dim CSTAmount As ReportParameter() = New ReportParameter() {New ReportParameter("CSTAmount", 0)}
            ReportViewer1.LocalReport.SetParameters(CSTAmount)

            Dim ExciseAmount As ReportParameter() = New ReportParameter() {New ReportParameter("ExciseAmount", 0)}
            ReportViewer1.LocalReport.SetParameters(ExciseAmount)

            Dim Shipping As ReportParameter() = New ReportParameter() {New ReportParameter("Shipping", dShipping)}
            ReportViewer1.LocalReport.SetParameters(Shipping)

            Dim GrandTotal As ReportParameter() = New ReportParameter() {New ReportParameter("GrandTotal", String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dRoundTotal))))}
            ReportViewer1.LocalReport.SetParameters(GrandTotal)

            Dim TotalInWords As ReportParameter() = New ReportParameter() {New ReportParameter("TotalInWords", TotalinWord)}
            ReportViewer1.LocalReport.SetParameters(TotalInWords)

            Dim TermsConditions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsConditions", UCase(objItemWise.GetTermsConditions(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(TermsConditions)

            Dim VAT As ReportParameter() = New ReportParameter() {New ReportParameter("VAT", UCase(objItemWise.GetALLVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice)))}
            ReportViewer1.LocalReport.SetParameters(VAT)

            Dim TotalQty As ReportParameter() = New ReportParameter() {New ReportParameter("TotalQty", iTotalQty)}
            ReportViewer1.LocalReport.SetParameters(TotalQty)

            'Dim dt2 As New DataTable
            'dt2 = objItemWise.GetVATBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice)

            'Dim rds1 As New ReportDataSource("DataSet2", dt2)
            'ReportViewer1.LocalReport.DataSources.Add(rds1)
            'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RptSalesInvoiceItemWise.rdlc")

            Dim SalesPersonName As ReportParameter() = New ReportParameter() {New ReportParameter("SalesPersonName", sSession.UserFullName)}
            ReportViewer1.LocalReport.SetParameters(SalesPersonName)

            Dim RoundOffDecimal As ReportParameter() = New ReportParameter() {New ReportParameter("RoundOffDecimal", decpart)}
            ReportViewer1.LocalReport.SetParameters(RoundOffDecimal)

            Dim Heading As ReportParameter() = New ReportParameter() {New ReportParameter("Heading", "Cash Invoice")}
            ReportViewer1.LocalReport.SetParameters(Heading)

            Dim AmountTotal As ReportParameter() = New ReportParameter() {New ReportParameter("AmountTotal", dAmtTotal)}
            ReportViewer1.LocalReport.SetParameters(AmountTotal)

            Dim DiscountAmtTotal As ReportParameter() = New ReportParameter() {New ReportParameter("DiscountAmtTotal", dDiscountAmtTotal)}
            ReportViewer1.LocalReport.SetParameters(DiscountAmtTotal)

            Dim GSTAmtTotal As ReportParameter() = New ReportParameter() {New ReportParameter("GSTAmtTotal", dGSTAmtTotal)}
            ReportViewer1.LocalReport.SetParameters(GSTAmtTotal)

            Dim OtherChargesTotalAmt As ReportParameter() = New ReportParameter() {New ReportParameter("OtherChargesTotalAmt", dOtherChargesTotalAmt)}
            ReportViewer1.LocalReport.SetParameters(OtherChargesTotalAmt)


            Dim dt2 As New DataTable
            'dt2 = objItemWise.GetVATBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            Dim rds1 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")

            Dim dAmount, dVATAmount As Double
            'If dt2.Rows.Count > 0 Then
            '    For j = 0 To dt2.Rows.Count - 1
            '        dAmount = dAmount + dt2.Rows(j)("Amount")
            '        dVATAmount = dVATAmount + dt2.Rows(j)("VATAmount")
            '    Next
            'End If

            Dim AmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("AmtBifercation", 0)}
            ReportViewer1.LocalReport.SetParameters(AmtBifercation)

            Dim VATAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmtBifercation", 0)}
            ReportViewer1.LocalReport.SetParameters(VATAmtBifercation)

            Dim dt3 As New DataTable
            'dt3 = objItemWise.GetCSTBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            Dim rds2 As New ReportDataSource("DataSet3", dt3)
            ReportViewer1.LocalReport.DataSources.Add(rds2)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")

            Dim dCAmount, dCSTAmount As Double
            'If dt3.Rows.Count > 0 Then
            '    For k = 0 To dt3.Rows.Count - 1
            '        dCAmount = dCAmount + dt3.Rows(k)("CAmount")
            '        dCSTAmount = dCSTAmount + dt3.Rows(k)("CSTAmount")
            '    Next
            'End If

            Dim CAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("CAmtBifercation", 0)}
            ReportViewer1.LocalReport.SetParameters(CAmtBifercation)

            Dim CSTAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("CSTAmtBifercation", 0)}
            ReportViewer1.LocalReport.SetParameters(CSTAmtBifercation)

            Dim dt4 As New DataTable
            dt4 = objItemWise.GetCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iInvice, iDispatchID)

            Dim rds3 As New ReportDataSource("DataSet4", dt4)
            ReportViewer1.LocalReport.DataSources.Add(rds3)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")

            Dim dt5 As New DataTable
            dt5 = objItemWise.GetGSTBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iDispatchID)

            Dim rds4 As New ReportDataSource("DataSet5", dt5)
            ReportViewer1.LocalReport.DataSources.Add(rds4)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")

            ReportViewer1.LocalReport.Refresh()

            Dim dtURD As New DataTable
            dtURD = objItemWise.GetOralURD(sSession.AccessCode, sSession.AccessCodeID, iInvice, iDispatchID)

            Dim rdsURD As New ReportDataSource("DataSet6", dtURD)
            ReportViewer1.LocalReport.DataSources.Add(rdsURD)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")
            ReportViewer1.LocalReport.Refresh()

            Dim dtHSN As New DataTable
            dtHSN = objItemWise.GetOralHSN(sSession.AccessCode, sSession.AccessCodeID, iInvice, iDispatchID)

            Dim rdsHSN As New ReportDataSource("DataSet7", dtHSN)
            ReportViewer1.LocalReport.DataSources.Add(rdsHSN)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RptSalesInvoiceItemWise.rdlc")
            ReportViewer1.LocalReport.Refresh()

            'Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadOralDetails")
        End Try
    End Sub
    Private Sub ddlDispatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDispatch.SelectedIndexChanged
        Dim iOrderID As Integer
        Try
            loadgrid(iOrderID, ddlDispatch.SelectedValue)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetInWords(ByVal num As Long) As String
        On Error Resume Next
        Dim str As String
        Dim subnum As Long
        Dim Digits As New TextBox
        str = ""
        Digits.Text = num.ToString
        If Len(Digits.Text) > 11 Then
            str = GetSubInWords(CLng(Mid(Digits.Text, 1, Len(Digits.Text) - 9)))

            Digits.Text = Mid(Digits.Text, Len(Digits.Text) - 9 + 1, 9)

        End If

        If Len(Digits.Text) = 11 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Billion "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Billion "
            End If
            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 9, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 10, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str + " Rupees only "
        End If
        If Len(Digits.Text) = 10 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Billion "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Billion "
            End If
            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 8, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 9, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                ' str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str + " Rupees only "
        End If
        If Len(Digits.Text) = 9 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 8, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                ' str += " Rupees only "
            End If
            str = str + " Rupees only "
        End If
        If Len(Digits.Text) = 8 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str + " Rupees only "
        End If
        If Len(Digits.Text) = 7 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str + " Rupees only "
        End If
        If Len(Digits.Text) = 6 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str + " Rupees only "
        End If
        If Len(Digits.Text) = 5 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str + " Rupees only "
        End If

        If Len(Digits.Text) = 4 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str + " Rupees only "

        End If
        If Len(Digits.Text) = 3 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str + " Rupees only "

        End If
        If Len(Digits.Text) = 2 Or Len(Digits.Text) = 1 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str + " Rupees only "
        End If
        If Len(Digits.Text) = 0 Then
            str = ""
        End If

        Return str

    End Function
    Public Function GetTens(ByVal num As Integer) As String
        On Error Resume Next
        Select Case (num)
            Case 0
                Return ("")
            Case 1
                Return ("One")
            Case 2
                Return ("Two")
            Case 3
                Return ("Three")
            Case 4
                Return ("Four")
            Case 5
                Return ("Five")
            Case 6
                Return ("Six")
            Case 7
                Return ("Seven")
            Case 8
                Return ("Eight")
            Case 9
                Return ("Nine")
            Case 10
                Return ("Ten")
            Case 11
                Return ("Eleven")
            Case 12
                Return ("Twelve")
            Case 13
                Return ("Thirteen")
            Case 14
                Return ("Fourteen")
            Case 15
                Return ("Fifteen")
            Case 16
                Return ("Sixteen")
            Case 17
                Return ("Seventeen")
            Case 18
                Return ("Eighteen")
            Case 19
                Return ("Nineteen")

        End Select

        Return ("")

    End Function
    Public Function GetTwenty(ByVal num As Integer) As String
        On Error Resume Next
        Select Case (num)
            Case 0
                Return ("")
            Case 1
                Return ("One")
            Case 2
                Return ("Twenty")
            Case 3
                Return ("Thirty")
            Case 4
                Return ("Forty")
            Case 5
                Return ("Fifty")
            Case 6
                Return ("Sixty")
            Case 7
                Return ("Seventy")
            Case 8
                Return ("Eighty")
            Case 9
                Return ("Ninety")

        End Select
        Return ("")
    End Function
    Public Function GetSubInWords(ByVal num As Long) As String
        On Error Resume Next
        Dim str As String
        Dim subnum As Long
        Dim Digits As New TextBox
        str = ""
        Digits.Text = num.ToString
        If Len(Digits.Text) = 11 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Billion "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Billion "
            End If
            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 9, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 10, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)

            ElseIf subnum > 0 Then
                str += GetTens(subnum)

            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 10 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Billion "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Billion "
            End If
            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 8, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 9, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)

            ElseIf subnum > 0 Then
                str += GetTens(subnum)

            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 9 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 8, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)

            ElseIf subnum > 0 Then
                str += GetTens(subnum)

            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 8 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)

            ElseIf subnum > 0 Then
                str += GetTens(subnum)

            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 7 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                ' str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 6 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 5 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If

        If Len(Digits.Text) = 4 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 3 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 2 Or Len(Digits.Text) = 1 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 0 Then
            str = ""
        End If

        Return str
    End Function

End Class
