Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsSalesInvoice
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function GetOrderType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer) As String
        Dim sStr As String = ""
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_OrderType From Sales_ProForma_Order Where SPO_ID=" & iInvice & " And SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & "  "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Invoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select SPO_ID,SPO_OrderCode from Sales_Proforma_Order where Spo_id in(select SDM_OrderID from Sales_Dispatch_Master "
            sSql = sSql & "where SDM_YearID =" & iYearID & " And SDM_CompID =" & iCompID & ") And SPO_YearID = " & iYearID & " and SPO_CompID =" & iCompID & " order by SPO_ID Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select SDM_ID,SDM_Code from Sales_Dispatch_Master where SDM_OrderID=" & iOrderID & " And SDM_YearID =" & iYearID & " And SDM_CompID =" & iCompID & " order by SDM_ID Desc "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadAllocationDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvice As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As DataRow
        Dim dr As DataRow
        Dim dtDetails As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim VAT As String = "", CST As String = "", Exise As String = ""
        Dim Cstval As String = ""
        Dim Total, TotalAmt As Double
        Dim gtQty, gtMRP, gtRate, gtDiscount, gtDiscountAmt, gtDTamt, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, GrandTotal As Double
        gtQty = 0 : gtMRP = 0 : gtRate = 0 : gtDiscount = 0 : gtDiscountAmt = 0 : gtDTamt = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Dim sDescription As String = ""
        Dim sCommodity As String = ""
        Dim sColor As String = ""
        Dim Totaltax As Double

        Dim dShipping, dTradeDiscount, dTradeDis As Double
        Dim iTotQty As Double
        Dim dLastAmount As Double

        Dim i0Qty, i0TotalQty As Double
        Dim i3Qty, i3TotalQty As Double
        Dim i4Qty, i4TotalQty As Double
        Dim i5Qty, i5TotalQty As Double
        Dim i6Qty, i6TotalQty As Double

        Dim i7Qty, i7TotalQty As Double
        Dim i8Qty, i8TotalQty As Double

        Dim i9Qty, i9TotalQty As Double
        Dim i10Qty, i10TotalQty As Double
        Dim i11Qty, i11TotalQty As Double
        Dim i12Qty, i12TotalQty As Double

        Try
            dt.Columns.Add("Sl No.")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Colour")
            dt.Columns.Add("3")
            dt.Columns.Add("4")
            dt.Columns.Add("5")
            dt.Columns.Add("6")
            dt.Columns.Add("7")
            dt.Columns.Add("8")
            dt.Columns.Add("9")
            dt.Columns.Add("10")
            dt.Columns.Add("11")
            dt.Columns.Add("12")
            dt.Columns.Add("Total")
            dt.Columns.Add("MRP")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("DisAmt")
            dt.Columns.Add("Amount")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("Total Amount")
            dt.Columns.Add("Shipping")
            dt.Columns.Add("TradeDiscount")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Unit")
            dt.Columns.Add("TradeDis")
            dt.Columns.Add("0")
            dt.Columns.Add("LastColAmount")
            dt.Columns.Add("ItemID")

            dt.Columns.Add("Total0")
            dt.Columns.Add("Total3")
            dt.Columns.Add("Total4")
            dt.Columns.Add("Total5")
            dt.Columns.Add("Total6")
            dt.Columns.Add("Total7")
            dt.Columns.Add("Total8")
            dt.Columns.Add("Total9")
            dt.Columns.Add("Total10")
            dt.Columns.Add("Total11")
            dt.Columns.Add("Total12")
            dt.Columns.Add("MRPPrice")

            Dim dtVAT As New DataTable
            sSql = "" : sSql = "Select Distinct(SDD_VAT) From Sales_Dispatch_Details Where SDD_CompID=" & iCompID & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master where SDM_OrderID=" & iInvice & " And SDM_CompID=" & iCompID & ") "
            dtVAT = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            Dim dtCST As New DataTable
            sSql = "" : sSql = "Select Distinct(SDD_CST) From Sales_Dispatch_Details Where SDD_CompID=" & iCompID & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master where SDM_OrderID=" & iInvice & " And SDM_CompID=" & iCompID & ") "
            dtCST = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            sSql = "" : sSql = "select Distinct(SDD_ID),SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_ShippingRate,g.SPO_ID,g.SPO_OrderCode,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Description,e.INV_Description,
                                SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP,
                                SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_TotalAmount,f.INV_Description Commodity,c.SAM_GrandDiscountAmt,h.Mas_Desc,c.SAM_GrandDiscount
                                from Sales_Dispatch_Details a
                                join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID
                                join Sales_Allocate_Master c on b.SDM_OrderID=c.SAM_OrderNo
                                join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID
                                join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id
                                join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
								join Inventory_Master e on a.SDD_DescID=e.Inv_ID 
                                join Acc_General_master h on a.SDD_UnitID=h.Mas_ID where SDD_CompID=" & iCompID & ""
            If iInvice <> 0 Then
                sSql = sSql & " And c.SAM_OrderNo= " & iInvice & " And b.SDM_ID=" & iDispatchID & " "
            End If

            sSql = sSql & " order by b.SDM_OrderID,a.SDD_HistoryID"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                TotalAmt = 0
                Totaltax = 0
                If IsDBNull(dtDetails.Rows(i)("SDD_CommodityID")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_DescID")) = False Then
                    dRow("Sl No.") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                End If

                'If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False And dtDetails.Rows(i)("Inv_Size") <> "" Then
                If IsDBNull(dtDetails.Rows(i)("SDD_Quantity")) = False Then
                    If ((dtDetails.Rows(i)("Inv_Size").ToString() = "") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "0")) Then
                        dRow("0") = dtDetails.Rows(i)("SDD_Quantity")
                        i0Qty = i0Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("0") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "3") Then
                        dRow("3") = dtDetails.Rows(i)("SDD_Quantity")
                        i3Qty = i3Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("3") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "4") Then
                        dRow("4") = dtDetails.Rows(i)("SDD_Quantity")
                        i4Qty = i4Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("4") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "5") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "39") Then
                        dRow("5") = dtDetails.Rows(i)("SDD_Quantity")
                        i5Qty = i5Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("5") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "6") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "40") Then
                        dRow("6") = dtDetails.Rows(i)("SDD_Quantity")
                        i6Qty = i6Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("6") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "7") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "41") Then
                        dRow("7") = dtDetails.Rows(i)("SDD_Quantity")
                        i7Qty = i7Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("7") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "8") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "42") Then
                        dRow("8") = dtDetails.Rows(i)("SDD_Quantity")
                        i8Qty = i8Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("8") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "9") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "43") Then
                        dRow("9") = dtDetails.Rows(i)("SDD_Quantity")
                        i9Qty = i9Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("9") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "10") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "44") Then
                        dRow("10") = dtDetails.Rows(i)("SDD_Quantity")
                        i10Qty = i10Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("10") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "11") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "45") Then
                        dRow("11") = dtDetails.Rows(i)("SDD_Quantity")
                        i11Qty = i11Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("11") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "12") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "46") Then
                        dRow("12") = dtDetails.Rows(i)("SDD_Quantity")
                        i12Qty = i12Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("12") = 0
                    End If

                End If

                dRow("Total0") = i0Qty
                i0TotalQty = i0Qty

                dRow("Total3") = i3Qty
                i3TotalQty = i3Qty

                dRow("Total4") = i4Qty
                i4TotalQty = i4Qty

                dRow("Total5") = i5Qty
                i5TotalQty = i5Qty

                dRow("Total6") = i6Qty
                i6TotalQty = i6Qty

                dRow("Total7") = i7Qty
                i7TotalQty = i7Qty

                dRow("Total8") = i8Qty
                i8TotalQty = i8Qty

                dRow("Total9") = i9Qty
                i9TotalQty = i9Qty

                dRow("Total10") = i10Qty
                i10TotalQty = i10Qty

                dRow("Total11") = i11Qty
                i11TotalQty = i11Qty

                dRow("Total12") = i12Qty
                i12TotalQty = i12Qty

                'End If

                If IsDBNull(dtDetails.Rows(i)("SDD_Quantity")) = False Then
                    dRow("Total") = dtDetails.Rows(i)("SDD_Quantity")
                    gtQty = gtQty + dtDetails.Rows(i)("SDD_Quantity")
                Else
                    dRow("Total") = 0
                End If

                dRow("TotalQty") = gtQty
                iTotQty = gtQty

                If IsDBNull(dtDetails.Rows(i)("SDD_Rate")) = False Then
                    'dRow("Rate") = dtDetails.Rows(i)("SDD_Quantity") * dtDetails.Rows(i)("SDD_Rate")
                    dRow("Rate") = dtDetails.Rows(i)("SDD_Rate")
                    gtMRP = gtMRP + dtDetails.Rows(i)("SDD_RateAmount")
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_VAT")) = False Then
                    dRow("VAT") = dtDetails.Rows(i)("SDD_VAT")
                    gtVAT = gtVAT + dtDetails.Rows(i)("SDD_VAT")
                Else
                    dRow("VAT") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_VATAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_VATAmount")))
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False Then
                    dRow("CST") = dtDetails.Rows(i)("SDD_CST")
                    gtCST = gtCST + dtDetails.Rows(i)("SDD_CST")
                Else
                    dRow("CST") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_CSTAmount")) = False Then
                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CSTAmount")))
                    gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("SDD_CSTAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("SDD_CSTAmount")
                Else
                    dRow("CSTAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("SDD_Excise")
                    gtExise = gtExise + dtDetails.Rows(i)("SDD_Excise")
                Else
                    dRow("Exise") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_ExciseAmount")))
                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("SDD_ExciseAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("SDD_ExciseAmount")
                Else
                    dRow("ExiseAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_Discount")) = False And dtDetails.Rows(i)("SDD_Discount") > 0 Then
                    dRow("Discount") = dtDetails.Rows(i)("SDD_Discount")
                    gtDiscount = gtDiscount + dRow("Discount")
                Else
                    dRow("Discount") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_DiscountAmount")) = False Then
                    dRow("DiscountAmt") = dtDetails.Rows(i)("SDD_DiscountAmount")
                    gtDiscountAmt = gtDiscountAmt + dRow("DiscountAmt")
                Else
                    dRow("DiscountAmt") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_DiscountAmount")) = False Then
                    dRow("DisAmt") = dtDetails.Rows(i)("SDD_DiscountAmount")
                Else
                    dRow("DisAmt") = "0"
                End If

                TotalAmt = Totaltax + ((dRow("Rate") * dRow("Total")) - dRow("DiscountAmt"))
                'dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_RateAmount") - dtDetails.Rows(i)("SDD_DiscountAmount")))

                'If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False And dtDetails.Rows(i)("SDD_CST") > 0 Then
                '    dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") - dtDetails.Rows(i)("SDD_CSTAmount")))
                'Else
                '    dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") - dtDetails.Rows(i)("SDD_VATAmount")))
                'End If
                dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount")))
                GrandTotal = GrandTotal + dRow("Total Amount")

                If IsDBNull(dtDetails.Rows(i)("SDM_ShippingRate")) = False Then
                    dRow("Shipping") = dtDetails.Rows(i)("SDM_ShippingRate")
                    dShipping = dtDetails.Rows(i)("SDM_ShippingRate")
                Else
                    dRow("Shipping") = 0
                    dShipping = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SAM_GrandDiscountAmt")) = False Then
                    dRow("TradeDiscount") = dtDetails.Rows(i)("SAM_GrandDiscountAmt")
                    dTradeDiscount = dtDetails.Rows(i)("SAM_GrandDiscountAmt")
                Else
                    dRow("TradeDiscount") = 0
                    dTradeDiscount = 0
                End If

                If dtVAT.Rows.Count = 1 Then
                    'Commented'

                    gtVATAmt = Math.Round(((((GrandTotal - dTradeDiscount) + gtExiseAmt) * dtDetails.Rows(i)("SDD_VAT")) / 100), 2)

                    'gtCSTAmt = Math.Round((((GrandTotal - dTradeDiscount) * dtDetails.Rows(i)("SDD_CST")) / 100), 2)

                    'Commented'
                End If

                If dtCST.Rows.Count = 1 Then

                    gtCSTAmt = Math.Round(((((GrandTotal - dTradeDiscount) + gtExiseAmt) * dtDetails.Rows(i)("SDD_CST")) / 100), 2)

                End If

                'dRow("LastAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") * dtDetails.Rows(i)("SDD_Quantity")))
                'dLastAmount = dLastAmount + dRow("LastAmount")

                dRow("Unit") = dtDetails.Rows(i)("MAS_Desc")

                If IsDBNull(dtDetails.Rows(i)("SAM_GrandDiscount")) = False Then
                    dRow("TradeDis") = dtDetails.Rows(i)("SAM_GrandDiscount")
                    dTradeDis = dtDetails.Rows(i)("SAM_GrandDiscount")
                Else
                    dRow("TradeDis") = 0
                    dTradeDis = 0
                End If

                dRow("LastColAmount") = (dtDetails.Rows(i)("SDD_Quantity") * dtDetails.Rows(i)("SDD_TotalAmount"))

                dRow("ItemID") = dtDetails.Rows(i)("SDD_DescID")

                If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
                    dRow("MRPPrice") = dtDetails.Rows(i)("INVH_MRP")
                Else
                    dRow("MRPPrice") = 0
                End If

                dt.Rows.Add(dRow)
            Next

            dr = dt.NewRow()
            dr("Commodity") = <b>Total</b>
            dr("Description") = <b>Total</b>
            dr("Total") = gtQty
            dr("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(gtMRP))
            dr("VAT") = gtVAT
            dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
            dr("CST") = gtCST
            dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
            dr("Exise") = gtExise
            dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtExiseAmt))
            dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(gtDiscount))
            dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtDiscountAmt))
            dr("DisAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtDiscountAmt))
            dr("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandTotal))
            dr("Shipping") = String.Format("{0:0.00}", Convert.ToDecimal(dShipping))
            dr("TradeDiscount") = String.Format("{0:0.00}", Convert.ToDecimal(dTradeDiscount))
            dr("TotalQty") = iTotQty
            dr("TradeDis") = String.Format("{0:0.00}", Convert.ToDecimal(dTradeDis))

            dr("Total0") = i0TotalQty
            dr("Total3") = i3TotalQty
            dr("Total4") = i4TotalQty
            dr("Total5") = i5TotalQty
            dr("Total6") = i6TotalQty
            dr("Total7") = i7TotalQty
            dr("Total8") = i8TotalQty
            dr("Total9") = i9TotalQty
            dr("Total10") = i10TotalQty
            dr("Total11") = i11TotalQty
            dr("Total12") = i12TotalQty

            'dr("LastAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dLastAmount))
            dt.Rows.Add(dr)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPrintData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Print_Settings Where PS_Status='" & sStr & "' "
            GetPrintData = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetPrintData
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getImageName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As String
        Dim sSql As String = ""
        Dim sImageName As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT (PS_FileName + '.' + PS_Extn) As PS_FileName FROM Print_Settings WHERE PS_Status='" & sStr & "'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If IsDBNull(dt.Rows(0)("PS_FileName")) = False Then
                sImageName = dt.Rows(0)("PS_FileName")
                If sImageName = "NULL.NULL" Then
                    sImageName = ""
                End If
            Else
                sImageName = ""
            End If

            'If IsDBNull(objDBL.SQLGetDescription(sNameSpace, sSql)) = False Then
            '    sImageName = ""
            'Else
            '    sImageName = objDBL.SQLGetDescription(sNameSpace, sSql)
            'End If
            Return sImageName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function RemoveDublicate(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("Description"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("Description"), String.Empty)
                End If
            Next
            For Each DataRow As DataRow In duplicateList
                dt.Rows.Remove(DataRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadOralDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvice As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As DataRow
        Dim dr As DataRow
        Dim dtDetails As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim VAT As String = "", CST As String = "", Exise As String = ""
        Dim Cstval As String = ""
        Dim Total, TotalAmt As Double
        Dim gtQty, gtMRP, gtRate, gtDiscount, gtDiscountAmt, gtDTamt, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, GrandTotal As Double
        gtQty = 0 : gtMRP = 0 : gtRate = 0 : gtDiscount = 0 : gtDiscountAmt = 0 : gtDTamt = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Dim sDescription As String = ""
        Dim sCommodity As String = ""
        Dim sColor As String = ""
        Dim Totaltax As Double

        Dim dShipping, dTradeDiscount, dTradeDis As Double
        Dim iTotQty As Double

        Dim i0Qty, i0TotalQty As Double
        Dim i3Qty, i3TotalQty As Double
        Dim i4Qty, i4TotalQty As Double
        Dim i5Qty, i5TotalQty As Double
        Dim i6Qty, i6TotalQty As Double

        Dim i7Qty, i7TotalQty As Double
        Dim i8Qty, i8TotalQty As Double

        Dim i9Qty, i9TotalQty As Double
        Dim i10Qty, i10TotalQty As Double
        Dim i11Qty, i11TotalQty As Double
        Dim i12Qty, i12TotalQty As Double
        Try
            dt.Columns.Add("Sl No.")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Colour")
            dt.Columns.Add("3")
            dt.Columns.Add("4")
            dt.Columns.Add("5")
            dt.Columns.Add("6")
            dt.Columns.Add("7")
            dt.Columns.Add("8")
            dt.Columns.Add("9")
            dt.Columns.Add("10")
            dt.Columns.Add("11")
            dt.Columns.Add("12")
            dt.Columns.Add("Total")
            dt.Columns.Add("MRP")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("DisAmt")
            dt.Columns.Add("Amount")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("Total Amount")
            dt.Columns.Add("Shipping")
            dt.Columns.Add("TradeDiscount")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Unit")
            dt.Columns.Add("TradeDis")
            dt.Columns.Add("0")
            dt.Columns.Add("LastColAmount")
            dt.Columns.Add("ItemID")

            dt.Columns.Add("Total0")
            dt.Columns.Add("Total3")
            dt.Columns.Add("Total4")
            dt.Columns.Add("Total5")
            dt.Columns.Add("Total6")
            dt.Columns.Add("Total7")
            dt.Columns.Add("Total8")
            dt.Columns.Add("Total9")
            dt.Columns.Add("Total10")
            dt.Columns.Add("Total11")
            dt.Columns.Add("Total12")
            dt.Columns.Add("MRPPrice")

            Dim dtVAT As New DataTable
            sSql = "" : sSql = "Select Distinct(SDD_VAT) From Sales_Dispatch_Details Where SDD_CompID=" & iCompID & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master where SDM_OrderID=" & iInvice & " And SDM_CompID=" & iCompID & ") "
            dtVAT = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            Dim dtCST As New DataTable
            sSql = "" : sSql = "Select Distinct(SDD_CST) From Sales_Dispatch_Details Where SDD_CompID=" & iCompID & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master where SDM_OrderID=" & iInvice & " And SDM_CompID=" & iCompID & ") "
            dtCST = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            sSql = "" : sSql = "select Distinct(SDD_ID),SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_ShippingRate,c.SPO_ID,c.SPO_OrderCode,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Description,e.INV_Description,
                                SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP,
                                SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_TotalAmount,f.INV_Description Commodity,c.SPO_GrandDiscountAmt,h.Mas_Desc,c.SPO_GrandDiscount
                                from Sales_Dispatch_Details a
                                join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID
                                join Sales_Proforma_Order c on b.SDM_OrderID=c.SPO_ID
                                join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id
                                join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
								join Inventory_Master e on a.SDD_DescID=e.Inv_ID 
                                join Acc_General_master h on a.SDD_UnitID=h.Mas_ID where SDD_CompID=" & iCompID & ""
            If iInvice <> 0 Then
                sSql = sSql & " And c.SPO_ID= " & iInvice & " And b.SDM_ID=" & iDispatchID & " "
            End If

            sSql = sSql & " order by b.SDM_OrderID,a.SDD_HistoryID"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                TotalAmt = 0
                Totaltax = 0
                If IsDBNull(dtDetails.Rows(i)("SDD_CommodityID")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_DescID")) = False Then
                    dRow("Sl No.") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                End If

                'If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                If IsDBNull(dtDetails.Rows(i)("SDD_Quantity")) = False Then

                    If ((dtDetails.Rows(i)("Inv_Size").ToString() = "") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "0")) Then
                        dRow("0") = dtDetails.Rows(i)("SDD_Quantity")
                        i0Qty = i0Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("0") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "3") Then
                        dRow("3") = dtDetails.Rows(i)("SDD_Quantity")
                        i3Qty = i3Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("3") = 0
                    End If

                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "4") Then
                        dRow("4") = dtDetails.Rows(i)("SDD_Quantity")
                        i4Qty = i4Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("4") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "5") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "39") Then
                        dRow("5") = dtDetails.Rows(i)("SDD_Quantity")
                        i5Qty = i5Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("5") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "6") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "40") Then
                        dRow("6") = dtDetails.Rows(i)("SDD_Quantity")
                        i6Qty = i6Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("6") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "7") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "41") Then
                        dRow("7") = dtDetails.Rows(i)("SDD_Quantity")
                        i7Qty = i7Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("7") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "8") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "42") Then
                        dRow("8") = dtDetails.Rows(i)("SDD_Quantity")
                        i8Qty = i8Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("8") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "9") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "43") Then
                        dRow("9") = dtDetails.Rows(i)("SDD_Quantity")
                        i9Qty = i9Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("9") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "10") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "44") Then
                        dRow("10") = dtDetails.Rows(i)("SDD_Quantity")
                        i10Qty = i10Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("10") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "11") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "45") Then
                        dRow("11") = dtDetails.Rows(i)("SDD_Quantity")
                        i11Qty = i11Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("11") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "12") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "46") Then
                        dRow("12") = dtDetails.Rows(i)("SDD_Quantity")
                        i12Qty = i12Qty + dtDetails.Rows(i)("SDD_Quantity")
                    Else
                        dRow("12") = 0
                    End If
                End If

                dRow("Total0") = i0Qty
                i0TotalQty = i0Qty

                dRow("Total3") = i3Qty
                i3TotalQty = i3Qty

                dRow("Total4") = i4Qty
                i4TotalQty = i4Qty

                dRow("Total5") = i5Qty
                i5TotalQty = i5Qty

                dRow("Total6") = i6Qty
                i6TotalQty = i6Qty

                dRow("Total7") = i7Qty
                i7TotalQty = i7Qty

                dRow("Total8") = i8Qty
                i8TotalQty = i8Qty

                dRow("Total9") = i9Qty
                i9TotalQty = i9Qty

                dRow("Total10") = i10Qty
                i10TotalQty = i10Qty

                dRow("Total11") = i11Qty
                i11TotalQty = i11Qty

                dRow("Total12") = i12Qty
                i12TotalQty = i12Qty

                'End If
                If IsDBNull(dtDetails.Rows(i)("SDD_Quantity")) = False Then
                    dRow("Total") = dtDetails.Rows(i)("SDD_Quantity")
                    gtQty = gtQty + dtDetails.Rows(i)("SDD_Quantity")
                Else
                    dRow("Total") = 0
                End If

                dRow("TotalQty") = gtQty
                iTotQty = gtQty

                If IsDBNull(dtDetails.Rows(i)("SDD_Rate")) = False Then
                    'dRow("Rate") = dtDetails.Rows(i)("SDD_Quantity") * dtDetails.Rows(i)("SDD_Rate")
                    dRow("Rate") = dtDetails.Rows(i)("SDD_Rate")
                    gtMRP = gtMRP + dtDetails.Rows(i)("SDD_RateAmount")
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_VAT")) = False Then
                    dRow("VAT") = dtDetails.Rows(i)("SDD_VAT")
                    'dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dtDetails.Rows(i)("SDD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    gtVAT = gtVAT + dRow("VAT")
                Else
                    dRow("VAT") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_VATAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_VATAmount")))
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False Then
                    dRow("CST") = dtDetails.Rows(i)("SDD_CST")
                    'dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dtDetails.Rows(i)("SDD_CST") & " And MAS_CompID=" & iCompID & " ")
                    gtCST = gtCST + dRow("CST")
                Else
                    dRow("CST") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_CSTAmount")) = False Then
                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_CSTAmount")))
                    gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("SDD_CSTAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("SDD_CSTAmount")
                Else
                    dRow("CSTAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("SDD_Excise")
                    'dRow("Exise") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dtDetails.Rows(i)("SDD_Excise") & " And MAS_CompID=" & iCompID & " ")
                    gtExise = gtExise + dRow("Exise")
                Else
                    dRow("Exise") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_ExciseAmount")))
                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("SDD_ExciseAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("SDD_ExciseAmount")
                Else
                    dRow("ExiseAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_Discount")) = False And dtDetails.Rows(i)("SDD_Discount") > 0 Then
                    dRow("Discount") = dtDetails.Rows(i)("SDD_Discount")
                    gtDiscount = gtDiscount + dRow("Discount")
                Else
                    dRow("Discount") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_DiscountAmount")) = False Then
                    dRow("DiscountAmt") = dtDetails.Rows(i)("SDD_DiscountAmount")
                    gtDiscountAmt = gtDiscountAmt + dRow("DiscountAmt")
                Else
                    dRow("DiscountAmt") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("SDD_DiscountAmount")) = False Then
                    dRow("DisAmt") = dtDetails.Rows(i)("SDD_DiscountAmount")
                Else
                    dRow("DisAmt") = "0"
                End If

                TotalAmt = Totaltax + ((dRow("Rate") * dRow("Total")) - dRow("DiscountAmt"))
                'dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_RateAmount") - dtDetails.Rows(i)("SDD_DiscountAmount")))
                'If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False And dtDetails.Rows(i)("SDD_CST") > 0 Then
                '    dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") - dtDetails.Rows(i)("SDD_CSTAmount")))
                'Else
                '    dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") - dtDetails.Rows(i)("SDD_VATAmount")))
                'End If
                dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount")))
                GrandTotal = GrandTotal + dRow("Total Amount")

                If IsDBNull(dtDetails.Rows(i)("SDM_ShippingRate")) = False Then
                    dRow("Shipping") = dtDetails.Rows(i)("SDM_ShippingRate")
                    dShipping = dtDetails.Rows(i)("SDM_ShippingRate")
                Else
                    dRow("Shipping") = 0
                    dShipping = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SPO_GrandDiscountAmt")) = False Then
                    dRow("TradeDiscount") = dtDetails.Rows(i)("SPO_GrandDiscountAmt")
                    dTradeDiscount = dtDetails.Rows(i)("SPO_GrandDiscountAmt")
                Else
                    dRow("TradeDiscount") = 0
                    dTradeDiscount = 0
                End If

                Dim sVat As String = ""
                If dtVAT.Rows.Count = 1 Then
                    'Commeneted'

                    sVat = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dtVAT.Rows(0)("SDD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    gtVATAmt = Math.Round(((((GrandTotal - dTradeDiscount) + gtExiseAmt) * sVat) / 100), 2)

                    'gtCSTAmt = Math.Round((((GrandTotal - dTradeDiscount) * dtDetails.Rows(i)("SDD_CST")) / 100), 2)

                    'Commeneted'
                End If

                Dim sCst As String = ""
                If dtCST.Rows.Count = 1 Then

                    sCst = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dtCST.Rows(0)("SDD_CST") & " And MAS_CompID=" & iCompID & " ")
                    gtCSTAmt = Math.Round(((((GrandTotal - dTradeDiscount) + gtExiseAmt) * sCst) / 100), 2)

                End If

                dRow("Unit") = dtDetails.Rows(i)("Mas_Desc")

                If IsDBNull(dtDetails.Rows(i)("SPO_GrandDiscount")) = False Then
                    dRow("TradeDis") = dtDetails.Rows(i)("SPO_GrandDiscount")
                    dTradeDis = dtDetails.Rows(i)("SPO_GrandDiscount")
                Else
                    dRow("TradeDis") = 0
                    dTradeDis = 0
                End If

                dRow("LastColAmount") = (dtDetails.Rows(i)("SDD_Quantity") * dtDetails.Rows(i)("SDD_TotalAmount"))

                dRow("ItemID") = dtDetails.Rows(i)("SDD_DescID")

                If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
                    dRow("MRPPrice") = dtDetails.Rows(i)("INVH_MRP")
                Else
                    dRow("MRPPrice") = 0
                End If

                dt.Rows.Add(dRow)
            Next


            dr = dt.NewRow()
            dr("Commodity") = <b>Total</b>
            dr("Description") = <b>Total</b>
            dr("Total") = gtQty
            dr("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(gtMRP))
            dr("VAT") = gtVAT
            dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
            dr("CST") = gtCST
            dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
            dr("Exise") = gtExise
            dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtExiseAmt))
            dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(gtDiscount))
            dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtDiscountAmt))
            dr("DisAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtDiscountAmt))
            dr("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandTotal))
            dr("Shipping") = String.Format("{0:0.00}", Convert.ToDecimal(dShipping))
            dr("TradeDiscount") = String.Format("{0:0.00}", Convert.ToDecimal(dTradeDiscount))
            dr("TotalQty") = iTotQty
            dr("TradeDis") = String.Format("{0:0.00}", Convert.ToDecimal(dTradeDis))

            dr("Total0") = i0TotalQty
            dr("Total3") = i3TotalQty
            dr("Total4") = i4TotalQty
            dr("Total5") = i5TotalQty
            dr("Total6") = i6TotalQty
            dr("Total7") = i7TotalQty
            dr("Total8") = i8TotalQty
            dr("Total9") = i9TotalQty
            dr("Total10") = i10TotalQty
            dr("Total11") = i11TotalQty
            dr("Total12") = i12TotalQty

            'dr("LastAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dLastAmount))

            dt.Rows.Add(dr)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Commodity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Description, Inv_Parent from Inventory_Master where Inv_Parent=0 And Inv_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function Invoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "" : sSql = "Select SPO_ID,SPO_OrderCode from Sales_Proforma_Order where Spo_id in(select SDM_OrderID from Sales_Dispatch_Master "
    '        sSql = sSql & "where SDM_YearID =" & iYearID & " And SDM_CompID =" & iCompID & ") And SPO_YearID = " & iYearID & " and SPO_CompID =" & iCompID & " order by SPO_ID Desc"
    '        Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function Party(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoice As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "select SPO_ID,SPO_PartyName,SPO_BuyerOrderNo,SPO_BuyerOrderDate,SPO_OrderDate,SPO_OrderCode,b.BM_Name,b.BM_Code,b.BM_Address,b.BM_Address1,b.BM_Address2,b.BM_Address3,b.BM_MobileNo,b.BM_EmailID,b.BM_PinCode,c.SDM_DispatchDate,b.BM_GSTNRegNo,c.SDM_PaymentType from Sales_Proforma_Order a
					join Sales_Buyers_Masters b on a.SPO_PartyName=b.BM_ID 
                    join Sales_Dispatch_Master c on a.SPO_ID=c.SDM_OrderID
                    where SPO_ID=" & iInvoice & " and  SPO_CompID=" & iCompID & " and SPO_YearID =" & iYearID & ""
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridStatutoryReferencesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("sID")
            dt.Columns.Add("Statutory Name")
            dt.Columns.Add("Statutory Value")

            sSql = "Select Buyer_PKID,Buyer_ID,Buyer_Desc,Buyer_Value,Buyer_Status from Sales_Buyer_Accounting_Template Where Buyer_ID=" & iPartyID & " and  Buyer_CompID=" & iCompID & "  Order by Buyer_Desc"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("sID") = dr("Buyer_PKID")
                    dRow("Statutory Name") = dr("Buyer_Desc")
                    dRow("Statutory Value") = dr("Buyer_Value")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateNetAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal dDiscount As Double, ByVal iDispatchID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetails As New DataTable
        Dim iVAT As Integer
        Try

            sRate = dMRPRate
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SDD_Discount From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
            If bCheck = True Then

                'sDiscount = objDBL.SQLGetDescription(sNameSpace, "Select SDD_Discount From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
                sDiscount = dDiscount
                sDiscountAmt = (sRate * sDiscount) / 100
                sNetAmt = (sRate - sDiscountAmt)

                iVAT = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SDD_VAT From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
                If iVAT > 0 Then
                    sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID in (Select SDD_VAT From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & ") ")
                    sAmt = ((sRate - sDiscountAmt) * sVat) / (100)
                    sVAtAmt = (sRate - sAmt)
                    sNetAmt = sRate - sDiscountAmt
                End If

                iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SDD_CST From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
                If iCST > 0 Then
                    sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID in (Select SDD_CST From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And  SDD_CompID=" & iCompID & ") ")
                    sCSTAmt = ((sRate - sDiscountAmt) * sCST) / 100
                    sNetAmt = (sRate - sDiscountAmt)
                End If

                iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SDD_Excise From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
                If iExcise > 0 Then
                    sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID in (Select SDD_Excise From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & ") ")
                    sExciseAmt = ((sRate - sDiscountAmt) * sExcise) / 100
                    sNetAmt = (sRate - sDiscountAmt)
                End If
            Else
                iVAT = objDBL.SQLExecuteScalarInt(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID in (Select SDD_VAT From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & ") ")
                If iVAT > 0 Then
                    sAmt = (sRate * sVat) / (100)
                    sVAtAmt = (sRate - sAmt)
                    sNetAmt = sRate
                End If

                iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SDD_CST From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And  SDD_CompID=" & iCompID & " ")
                If iCST > 0 Then
                    sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID in (Select SDD_CST From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And  SDD_CompID=" & iCompID & ") ")
                    sCSTAmt = (sRate * sCST) / 100
                    sNetAmt = sRate
                End If

                iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SDD_Excise From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
                If iExcise > 0 Then
                    sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID in (Select SDD_Excise From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & ") ")
                    sExciseAmt = (sRate * sExcise) / 100
                    sNetAmt = sRate
                End If
            End If
            Return sNetAmt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateDiscountAmt(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal dDiscount As Double, ByVal iDispatchID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""
        Try
            'sSql = "Select Distinct(SDD_Rate) From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " "
            'sRate = objDBL.SQLGetDescription(sNameSpace, sSql)
            sRate = dMRPRate
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SDD_Discount From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
            If bCheck = True Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SDD_VAT From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
                If bCheck = True Then
                    'sDiscount = objDBL.SQLGetDescription(sNameSpace, "Select Distinct(SDD_Discount) From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
                    sDiscount = dDiscount
                    sDiscountAmt = (sRate * sDiscount) / 100
                    sNetAmt = (sRate - sDiscountAmt)

                    sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID in (Select SDD_VAT From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & ") ")
                    sAmt = (sRate * sVat) / (100)
                    sVAtAmt = (sRate - sAmt)
                    sNetAmt = sRate - sDiscountAmt

                Else
                    'sDiscount = objDBL.SQLGetDescription(sNameSpace, "Select Distinct(SDD_Discount) From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
                    sDiscount = dDiscount
                    sDiscountAmt = (sRate * sDiscount) / 100
                    sNetAmt = (sRate - sDiscountAmt)
                End If
            End If
            Return sDiscountAmt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccessCode(ByVal sNameSpace As String, ByVal sAccessID As Integer)
        Dim sSql As String
        Dim sAccessCode As String
        Try
            sSql = "Select SAD_CMS_AccessCode from Sad_CompanyMaster_Settings Where Sad_CMS_ID=" & sAccessID & ""
            sAccessCode = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sAccessCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from MST_Customer_Master where Cust_CODE='" & sNameSpace & "' and Cust_Delflg <> 'D' and CUST_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridOtherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("sID")
            dt.Columns.Add("Statutory Name")
            dt.Columns.Add("Statutory Value")

            sSql = "Select Cmp_PKID,Cmp_Desc,Cmp_Value,Cmp_Status,Cmp_ID from Company_Accounting_Template Where Cmp_ID=" & iCompID & " Order by Cmp_Desc"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("sID") = dr("Cmp_PKID")
                    dRow("Statutory Name") = dr("Cmp_Desc")
                    dRow("Statutory Value") = dr("Cmp_Value")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateNetAmountMRP(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sItemID As String, ByVal dDiscount As Double, ByVal iDispatchID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetails As New DataTable
        Dim dItemTotal As Double : Dim dTotal As Double
        Try

            sRate = dMRPRate
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SDD_VAT From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
            If bCheck = True Then
                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SDD_Quantity,SDD_Discount,SDD_VAT,SDD_CST,SDD_Excise From Sales_Dispatch_Details where SDD_DescID in(" & sItemID & ") And SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1
                        dTotal = 0
                        sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_VAT") & " ")
                        sAmt = (sRate * 100) / (sVat + 100)
                        sVAtAmt = (sRate - sAmt)
                        sNetAmt = sRate - sVAtAmt

                        If IsDBNull(dtDetails.Rows(i)("SDD_Discount")) = False Then
                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                            sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False Then
                            sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_CST") & " ")
                            sCSTAmt = ((sRate - sVAtAmt) * sCST) / 100
                            sNetAmt = (((sRate - sVAtAmt) - sDiscountAmt))
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_Excise")) = False Then
                            sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_Excise") & " ")
                            sExciseAmt = ((sRate - sVAtAmt) * sExcise) / 100
                            sNetAmt = ((((sRate - sVAtAmt) - sDiscountAmt)))
                        End If

                        dItemTotal = dtDetails.Rows(i)("SDD_Quantity") * sNetAmt
                        dTotal = dTotal + dItemTotal
                        'sNetAmt = 0
                        'dItemTotal = 0
                        dTotal = sNetAmt
                    Next
                End If
            Else
                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SDD_Quantity,SDD_Discount,SDD_CST,SDD_Excise From Sales_Dispatch_Details where SDD_DescID in(" & sItemID & ") And SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1

                        If IsDBNull(dtDetails.Rows(i)("SDD_Discount")) = False Then
                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = (sRate * sDiscount) / 100
                            sNetAmt = (sRate - sDiscountAmt)
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False Then
                            sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_CST") & " ")
                            sCSTAmt = (sRate * sCST) / 100
                            sNetAmt = ((sRate - sDiscountAmt))
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_Excise")) = False Then
                            sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_Excise") & " ")
                            sExciseAmt = (sRate * sExcise) / 100
                            sNetAmt = (((sRate - sDiscountAmt)))
                        End If

                        dItemTotal = dtDetails.Rows(i)("SDD_Quantity") * sNetAmt
                        dTotal = dTotal + dItemTotal
                        sNetAmt = 0
                        dItemTotal = 0
                    Next
                End If

            End If
            Return dTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateDiscountAmtMRP(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sItemID As String, ByVal dDiscount As Double, ByVal iDispatchID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetails As New DataTable
        Dim dTotalDis As Double : Dim dItemDiscount As Double
        Try

            sRate = dMRPRate
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SDD_Discount From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
            If bCheck = True Then
                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SDD_Quantity,SDD_Discount,SDD_VAT From Sales_Dispatch_Details where SDD_DescID in(" & sItemID & ") And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1
                        dTotalDis = 0
                        If IsDBNull(dtDetails.Rows(i)("SDD_VAT")) = False Then
                            sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_VAT") & " ")
                            sAmt = (sRate * 100) / (sVat + 100)
                            sVAtAmt = (sRate - sAmt)
                            sNetAmt = sRate - sVAtAmt

                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                            sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                        Else
                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = (sRate * sDiscount) / 100
                            sNetAmt = (sRate - sDiscountAmt)
                        End If

                        dItemDiscount = dtDetails.Rows(i)("SDD_Quantity") * sDiscountAmt
                        dTotalDis = dTotalDis + dItemDiscount
                        'sDiscountAmt = 0
                        'dItemDiscount = 0
                        dTotalDis = sDiscountAmt
                    Next
                End If

            End If
            Return dTotalDis
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDispatchMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetALLVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc As String = "" : Dim sVATAmt As String = ""
        Dim sStr As String = ""
        Dim sTotalAmt As String = "" : Dim sTradeDis As String = ""
        Dim dExciseAmont As Double
        Try
            sSql = "Select Distinct(SPOD_VAT) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SPO_GrandDiscountAmt From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")
                    dExciseAmont = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_ExciseAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    If sTradeDis <> "" Then
                        sVATAmt = (((sTotalAmt - sTradeDis) + dExciseAmont) * sVatDesc) / 100
                    End If
                    sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    sStr = sStr & "," & sValue
                Next
            ElseIf dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_VATAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    sStr = sStr & "," & sValue
                Next
            End If
            If sStr.StartsWith(",") Then
                sStr = sStr.Remove(0, 1)
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetALLVATAllocation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc As String = "" : Dim sVATAmt As String = ""
        Dim sStr As String = ""
        Dim sTotalAmt As String = "" : Dim sTradeDis As String = ""
        Dim dExciseAmont As Double
        Try
            sSql = "Select Distinct(SAD_VAT) From Sales_Allocate_Details where SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_Status='A' And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SAD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SAD_PlacedTotalAmount) From Sales_Allocate_Details where SAD_VAT=" & dt.Rows(i)("SAD_VAT") & " And SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_Status='A' And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & ") ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SAM_GrandDiscountAmt From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ")
                    dExciseAmont = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SAD_ExiceAmount) From Sales_Allocate_Details where SAD_VAT=" & dt.Rows(i)("SAD_VAT") & " And SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_Status='A' And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & ") ")
                    If sTradeDis <> "" Then
                        sVATAmt = (((sTotalAmt - sTradeDis) + dExciseAmont) * sVatDesc) / 100
                    End If
                    sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    sStr = sStr & "," & sValue
                Next
            ElseIf dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SAD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SAD_VATAmount) From Sales_Allocate_Details where SAD_VAT=" & dt.Rows(i)("SAD_VAT") & " And SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_Status='A' And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & ") ")
                    sValue = "VAT@" & sVatDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sVATAmt))
                    sStr = sStr & "," & sValue
                Next
            End If
            If sStr.StartsWith(",") Then
                sStr = sStr.Remove(0, 1)
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateDiscountAmtMRPCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sItemID As String, ByVal dDiscount As Double, ByVal iDispatchID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetails As New DataTable
        Dim dTotalDis As Double : Dim dItemDiscount As Double
        Try

            sRate = dMRPRate
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SDD_Discount From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
            If bCheck = True Then
                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SDD_Quantity,SDD_Discount,SDD_CST,SDD_VAT From Sales_Dispatch_Details where SDD_DescID in(" & sItemID & ") And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1
                        dTotalDis = 0
                        If IsDBNull(dtDetails.Rows(i)("SDD_VAT")) = False And dtDetails.Rows(i)("SDD_VAT") > 0 Then
                            sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_VAT") & " And MAS_CompID=" & iCompID & " ")
                            sAmt = (sRate * 100) / (sVat + 100)
                            sVAtAmt = (sRate - sAmt)
                            sNetAmt = sRate - sVAtAmt

                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                            sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                        Else
                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = (sRate * sDiscount) / 100
                            sNetAmt = (sRate - sDiscountAmt)
                        End If

                        dItemDiscount = dtDetails.Rows(i)("SDD_Quantity") * sDiscountAmt
                        dTotalDis = dTotalDis + dItemDiscount
                        'sDiscountAmt = 0
                        'dItemDiscount = 0
                        dTotalDis = sDiscountAmt
                    Next
                End If

            End If
            Return dTotalDis
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateNetAmountMRPCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sItemID As String, ByVal dDiscount As Double, ByVal iDispatchID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetails As New DataTable
        Dim dItemTotal As Double : Dim dTotal As Double
        Try

            sRate = dMRPRate
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SDD_VAT From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
            If bCheck = True Then
                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SDD_Quantity,SDD_Discount,SDD_VAT,SDD_CST,SDD_Excise From Sales_Dispatch_Details where SDD_DescID in(" & sItemID & ") And SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1
                        dTotal = 0
                        If dtDetails.Rows(i)("SDD_VAT") > 0 Then
                            sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_VAT") & " ")
                            sAmt = (sRate * 100) / (sVat + 100)
                            sVAtAmt = (sRate - sAmt)
                            sNetAmt = sRate - sVAtAmt
                        Else
                            sVAtAmt = 0
                            sNetAmt = sRate - sVAtAmt
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_Discount")) = False Then
                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                            sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False Then
                            sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_CST") & " ")
                            sCSTAmt = ((sRate - sVAtAmt) * sCST) / 100
                            sNetAmt = (((sRate - sVAtAmt) - sDiscountAmt))
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_Excise")) = False Then
                            sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_Excise") & " ")
                            sExciseAmt = ((sRate - sVAtAmt) * sExcise) / 100
                            sNetAmt = ((((sRate - sVAtAmt) - sDiscountAmt)))
                        End If

                        dItemTotal = dtDetails.Rows(i)("SDD_Quantity") * sNetAmt
                        dTotal = dTotal + dItemTotal
                        'sNetAmt = 0
                        'dItemTotal = 0
                        dTotal = sNetAmt
                    Next
                End If
            Else
                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SDD_Quantity,SDD_Discount,SDD_CST,SDD_Excise From Sales_Dispatch_Details where SDD_DescID in(" & sItemID & ") And SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1

                        If IsDBNull(dtDetails.Rows(i)("SDD_Discount")) = False Then
                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = (sRate * sDiscount) / 100
                            sNetAmt = (sRate - sDiscountAmt)
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False Then
                            sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_CST") & " ")
                            sCSTAmt = (sRate * sCST) / 100
                            sNetAmt = ((sRate - sDiscountAmt))
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_Excise")) = False Then
                            sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_Excise") & " ")
                            sExciseAmt = (sRate * sExcise) / 100
                            sNetAmt = (((sRate - sDiscountAmt)))
                        End If

                        dItemTotal = dtDetails.Rows(i)("SDD_Quantity") * sNetAmt
                        dTotal = dTotal + dItemTotal
                        'sNetAmt = 0
                        'dItemTotal = 0
                        dTotal = sNetAmt
                    Next
                End If

            End If
            Return dTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetALLCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sCstDesc As String = "" : Dim sCSTAmt As String = ""
        Dim sStr As String = ""
        Dim sTotalAmt As String = "" : Dim sTradeDis As String = ""
        Dim dExciseAmont As Double
        Try
            sSql = "Select Distinct(SPOD_CST) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sCstDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SPO_GrandDiscountAmt From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")
                    dExciseAmont = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_ExciseAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    If sTradeDis <> "" Then
                        sCSTAmt = (((sTotalAmt - sTradeDis) + dExciseAmont) * sCstDesc) / 100
                    End If
                    sValue = "CST@" & sCstDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sCSTAmt))
                    sStr = sStr & "," & sValue
                Next
            ElseIf dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sCstDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")
                    sCSTAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_CSTAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sValue = "CST@" & sCstDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sCSTAmt))
                    sStr = sStr & "," & sValue
                Next
            End If
            If sStr.StartsWith(",") Then
                sStr = sStr.Remove(0, 1)
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetALLCSTAllocation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sCstDesc As String = "" : Dim sCSTAmt As String = ""
        Dim sStr As String = ""
        Dim sTotalAmt As String = "" : Dim sTradeDis As String = ""
        Dim dExciseAmont As Double
        Try
            sSql = "Select Distinct(SAD_CST) From Sales_Allocate_Details where SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_Status='A' And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sCstDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SAD_CST") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SAD_PlacedTotalAmount) From Sales_Allocate_Details where SAD_CST=" & dt.Rows(i)("SAD_CST") & " And SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_Status='A' And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & ") ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SAM_GrandDiscountAmt From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ")
                    dExciseAmont = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SAD_ExiceAmount) From Sales_Allocate_Details where SAD_CST=" & dt.Rows(i)("SAD_CST") & " And SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_Status='A' And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & ") ")
                    If sTradeDis <> "" Then
                        sCSTAmt = (((sTotalAmt - sTradeDis) + dExciseAmont) * sCstDesc) / 100
                    End If
                    sValue = "CST@" & sCstDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sCSTAmt))
                    sStr = sStr & "," & sValue
                Next
            ElseIf dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sCstDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SAD_CST") & " And MAS_CompID=" & iCompID & " ")
                    sCSTAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SAD_CSTAmount) From Sales_Allocate_Details where SAD_CST=" & dt.Rows(i)("SAD_CST") & " And SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_Status='A' And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & ") ")
                    sValue = "CST@" & sCstDesc & " - " & String.Format("{0:0.00}", Convert.ToDecimal(sCSTAmt))
                    sStr = sStr & "," & sValue
                Next
            End If
            If sStr.StartsWith(",") Then
                sStr = sStr.Remove(0, 1)
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTermsConditions(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sStr As String = ""
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_Desc From Acc_General_Master Where MAS_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Terms & Conditions') And Mas_CompID=" & iCompID & " "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow

        Dim dTradeDisAmt As Double
        Try

            dt1.Columns.Add("VAT")
            dt1.Columns.Add("Amount")
            dt1.Columns.Add("VATAmount")

            sSql = "Select Distinct(SDD_VAT) From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    'sVatDesc = dt.Rows(i)("SDD_VAT")
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SDD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(i)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_VATAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(i)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")

                    dRow("VAT") = sVatDesc
                    dRow("Amount") = sAmountTot
                    dRow("VATAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            ElseIf dt.Rows.Count = 1 Then
                Dim sVATAmtFinal As String = ""
                dRow = dt1.NewRow()
                'sVatDesc = dt.Rows(0)("SDD_VAT")
                sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(0)("SDD_VAT") & " And MAS_CompID=" & iCompID & " ")
                dTradeDisAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDM_GrandDiscountAmt) From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " ")
                sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(0)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_VATAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(0)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                sVATAmtFinal = (((sAmountTot - dTradeDisAmt)) * sVatDesc) / 100

                dRow("VAT") = sVatDesc
                dRow("Amount") = sAmountTot
                dRow("VATAmount") = sVATAmtFinal

                dt1.Rows.Add(dRow)
                sVatDesc = "" : sAmountTot = "" : sVATAmt = "" : sVATAmtFinal = "" : dTradeDisAmt = 0
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow

        Dim dTradeDisAmt As Double
        Try

            dt1.Columns.Add("CST")
            dt1.Columns.Add("CAmount")
            dt1.Columns.Add("CSTAmount")

            sSql = "Select Distinct(SDD_CST) From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    'sVatDesc = dt.Rows(i)("SDD_CST")
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SDD_CST") & " And MAS_CompID=" & iCompID & " ")
                    If dt.Rows(i)("SDD_CST") > 0 Then
                        sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(i)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                    Else
                        sAmountTot = 0
                    End If
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_CSTAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(i)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")

                    dRow("CST") = sVatDesc
                    dRow("CAmount") = sAmountTot
                    dRow("CSTAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            ElseIf dt.Rows.Count = 1 Then
                Dim sCSTAmtFinal As String = ""
                dRow = dt1.NewRow()
                'sVatDesc = dt.Rows(0)("SDD_CST")
                sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(0)("SDD_CST") & " And MAS_CompID=" & iCompID & " ")
                dTradeDisAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDM_GrandDiscountAmt) From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " ")
                If dt.Rows(0)("SDD_CST") > 0 Then
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(0)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                Else
                    sAmountTot = 0
                End If
                sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_CSTAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(0)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                sCSTAmtFinal = (((sAmountTot - dTradeDisAmt)) * sVatDesc) / 100

                dRow("CST") = sVatDesc
                dRow("CAmount") = sAmountTot
                dRow("CSTAmount") = sCSTAmtFinal

                dt1.Rows.Add(dRow)
                sVatDesc = "" : sAmountTot = "" : sVATAmt = "" : sCSTAmtFinal = "" : dTradeDisAmt = 0
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATBifercationOral(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Try

            dt1.Columns.Add("VAT")
            dt1.Columns.Add("Amount")
            dt1.Columns.Add("VATAmount")

            sSql = "Select Distinct(SDD_VAT) From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = dt.Rows(i)("SDD_VAT")
                    'objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SDD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(i)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_VATAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(i)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")

                    dRow("VAT") = sVatDesc
                    dRow("Amount") = sAmountTot
                    dRow("VATAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTBifercationOral(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Try

            dt1.Columns.Add("CST")
            dt1.Columns.Add("CAmount")
            dt1.Columns.Add("CSTAmount")

            sSql = "Select Distinct(SDD_CST) From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = dt.Rows(i)("SDD_CST")
                    If dt.Rows(i)("SDD_CST") > 0 Then
                        sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(i)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                    Else
                        sAmountTot = 0
                    End If
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_CSTAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(i)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")

                    dRow("CST") = sVatDesc
                    dRow("CAmount") = sAmountTot
                    dRow("CSTAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select C_ChargeType As ChargeType,C_ChargeAmount As ChargeAmount From Charges_Master Where C_DispatchID=" & iDispatchID & " And C_OrderID=" & iOrderID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetChargedAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Sum(C_ChargeAmount) As ChargeAmount From Charges_Master Where C_DispatchID=" & iDispatchID & " And C_OrderID=" & iOrderID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("ChargeAmount")) = False Then
                    GetChargedAmount = objDBL.SQLGetDescription(sNameSpace, sSql)
                Else
                    GetChargedAmount = 0
                End If
            End If
            Return GetChargedAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateDiscountAmtMRPCSTNORMAL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sItemID As String, ByVal dDiscount As Double, ByVal iDispatchID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetails As New DataTable
        Dim dTotalDis As Double : Dim dItemDiscount As Double
        Try

            sRate = dMRPRate
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SDD_Discount From Sales_Dispatch_Details where SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
            If bCheck = True Then
                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SDD_Quantity,SDD_Discount,SDD_CST,SDD_VAT From Sales_Dispatch_Details where SDD_DescID in(" & sItemID & ") And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1
                        dTotalDis = 0
                        If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False And dtDetails.Rows(i)("SDD_CST") > 0 Then
                            sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_CST") & " And MAS_CompID=" & iCompID & " ")
                            sAmt = (sRate * 100) / (sVat + 100)
                            sVAtAmt = (sRate - sAmt)
                            sNetAmt = sRate - sVAtAmt

                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                            sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                        Else
                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = (sRate * sDiscount) / 100
                            sNetAmt = (sRate - sDiscountAmt)
                        End If

                        dItemDiscount = dtDetails.Rows(i)("SDD_Quantity") * sDiscountAmt
                        dTotalDis = dTotalDis + dItemDiscount
                        'sDiscountAmt = 0
                        'dItemDiscount = 0
                        dTotalDis = sDiscountAmt
                    Next
                End If

            End If
            Return dTotalDis
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateNetAmountMRPCSTNORMAL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sItemID As String, ByVal dDiscount As Double, ByVal iDispatchID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetails As New DataTable
        Dim dItemTotal As Double : Dim dTotal As Double
        Try

            sRate = dMRPRate
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SDD_VAT From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ")
            If bCheck = True Then
                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SDD_Quantity,SDD_Discount,SDD_VAT,SDD_CST,SDD_Excise From Sales_Dispatch_Details where SDD_DescID in(" & sItemID & ") And SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1
                        dTotal = 0
                        If dtDetails.Rows(i)("SDD_VAT") > 0 Then
                            sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_VAT") & " ")
                            sAmt = (sRate * 100) / (sVat + 100)
                            sVAtAmt = (sRate - sAmt)
                            sNetAmt = sRate - sVAtAmt
                        Else
                            sVAtAmt = 0
                            sNetAmt = sRate - sVAtAmt
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False Then
                            sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_CST") & " ")
                            'sCSTAmt = ((sRate - sVAtAmt) * sCST) / 100
                            'sNetAmt = (((sRate - sVAtAmt) - sDiscountAmt))
                            sAmt = (sRate * 100) / (sCST + 100)
                            sVAtAmt = (sRate - sAmt)
                            sNetAmt = sRate - sVAtAmt
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_Discount")) = False Then
                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                            sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_Excise")) = False Then
                            sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_Excise") & " ")
                            sExciseAmt = ((sRate - sVAtAmt) * sExcise) / 100
                            sNetAmt = ((((sRate - sVAtAmt) - sDiscountAmt)))
                        End If

                        dItemTotal = dtDetails.Rows(i)("SDD_Quantity") * sNetAmt
                        dTotal = dTotal + dItemTotal
                        'sNetAmt = 0
                        'dItemTotal = 0
                        dTotal = sNetAmt
                    Next
                End If
            Else
                dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SDD_Quantity,SDD_Discount,SDD_CST,SDD_Excise From Sales_Dispatch_Details where SDD_DescID in(" & sItemID & ") And SDD_MasterID In (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iInvice & " And Sdm_YearID=" & iYearID & ") And SDD_CompID=" & iCompID & " ").Tables(0)
                If dtDetails.Rows.Count > 0 Then
                    For i = 0 To dtDetails.Rows.Count - 1

                        If IsDBNull(dtDetails.Rows(i)("SDD_Discount")) = False Then
                            'sDiscount = dtDetails.Rows(i)("SDD_Discount")
                            sDiscount = dDiscount
                            sDiscountAmt = (sRate * sDiscount) / 100
                            sNetAmt = (sRate - sDiscountAmt)
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_CST")) = False Then
                            sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_CST") & " ")
                            sCSTAmt = (sRate * sCST) / 100
                            sNetAmt = ((sRate - sDiscountAmt))
                        End If

                        If IsDBNull(dtDetails.Rows(i)("SDD_Excise")) = False Then
                            sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dtDetails.Rows(i)("SDD_Excise") & " ")
                            sExciseAmt = (sRate * sExcise) / 100
                            sNetAmt = (((sRate - sDiscountAmt)))
                        End If

                        dItemTotal = dtDetails.Rows(i)("SDD_Quantity") * sNetAmt
                        dTotal = dTotal + dItemTotal
                        'sNetAmt = 0
                        'dItemTotal = 0
                        dTotal = sNetAmt
                    Next
                End If

            End If
            Return dTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPaymentType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasId As Integer) As String
        Dim sStr As String = ""
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_desc From acc_general_master Where Mas_id=" & iMasId & " And Mas_CompID=" & iCompID & "  "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetBankName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGlId As Integer) As String
        Dim sStr As String = ""
        Dim sSql As String = ""
        Try
            sSql = "Select gl_desc From chart_of_accounts Where gl_id =" & iGlId & " And gl_CompID=" & iCompID & "  "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
