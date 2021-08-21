Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsPROFormaHR
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Public Function Party(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoice As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "select a.SPO_ID,a.SPO_PartyName,a.SPO_OrderCode,a.SPO_OrderDate,a.SPO_BuyerOrderNo,a.SPO_BuyerOrderDate,b.BM_Name,b.BM_Code,b.BM_Address,b.BM_Address1,b.BM_Address2,b.BM_Address3,b.BM_MobileNo,b.BM_EmailID,b.BM_PinCode from Sales_Proforma_Order a
								join Sales_Buyers_Masters b on a.SPO_PartyName=b.BM_ID where SPO_ID=" & iInvoice & " and  SPO_CompID=" & iCompID & " and SPO_YearID =" & iYearID & ""
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadSalesPROFormaDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvice As Integer) As DataTable
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

        'Dim sItemID As String = ""
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
            dt.Columns.Add("0")
            dt.Columns.Add("TradeDis")
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

            'sSql = "" : sSql = "select SPOD_ID,SPOD_SOID,b.SPO_ID,b.SPO_OrderCode,b.SPO_PartyName,SPOD_HistoryID,d.Inv_Color,d.Inv_Size,d.INV_Description,d.INV_Description,
            '                    SPOD_CommodityID,SPOD_ItemID,SPOD_HistoryID,SPOD_MRPRate,SPOD_Quantity,SPOD_RateAmount,SPOD_CompID,c.INVH_MRP,
            '                    SPOD_Discount,SPOD_DiscountRate,SPOD_VAT,SPOD_VATAmount,SPOD_Excise,SPOD_ExciseAmount,SPOD_CST,SPOD_CSTAmount,SPOD_TotalAmount,e.INV_Description Commodity,b.SPO_GrandDiscountAmt,g.Mas_Desc,
            '                    f.BM_Name,f.BM_Address,f.BM_MobileNo,f.BM_EmailID
            '                    from Sales_Proforma_Order_Details a
            '                    join Sales_Proforma_Order b on a.SPOD_SOID=b.SPO_ID
            '                    join Inventory_Master_History c on a.SPOD_HistoryID=c.InvH_Id
            '                    join Inventory_Master d on a.SPOD_CommodityID=d.Inv_ID   
            '                    join Inventory_Master e on a.SPOD_ItemID=e.Inv_ID 
            '                    Join Sales_Buyers_Masters f On b.SPO_PartyName=f.BM_ID
            '                    join Acc_General_master g on a.SPOD_UnitOfMeasurement=g.Mas_ID where SPOD_CompID=" & iCompID & " And b.SPO_ID= " & iInvice & " And b.SPO_OrderType='S' order by b.SPO_ID,a.SPOD_HistoryID"

            Dim dtVAT As New DataTable
            sSql = "" : sSql = "Select Distinct(SPOD_VAT) From Sales_Proforma_Order_Details Where SPOD_Status <> 'C' and SPOD_CompID=" & iCompID & " And SPOD_SOID= " & iInvice & " "
            dtVAT = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            Dim dtCST As New DataTable
            sSql = "" : sSql = "Select Distinct(SPOD_CST) From Sales_Proforma_Order_Details Where SPOD_Status <> 'C' and SPOD_CompID=" & iCompID & " And SPOD_SOID= " & iInvice & " "
            dtCST = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)


            sSql = "" : sSql = "select SPOD_ID,SPOD_SOID,b.SPO_ID,b.SPO_OrderCode,b.SPO_PartyName,SPOD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Description,e.INV_Description,
                                SPOD_CommodityID,SPOD_ItemID,SPOD_HistoryID,SPOD_MRPRate,SPOD_Quantity,SPOD_RateAmount,SPOD_CompID,c.INVH_MRP,
                                SPOD_Discount,SPOD_DiscountRate,SPOD_VAT,SPOD_VATAmount,SPOD_Excise,SPOD_ExciseAmount,SPOD_CST,SPOD_CSTAmount,SPOD_TotalAmount,d.INV_Description Commodity,b.SPO_GrandDiscountAmt,g.Mas_Desc,b.SPO_GrandDiscount
                                from Sales_Proforma_Order_Details a 
                                join Sales_Proforma_Order b on a.SPOD_SOID=b.SPO_ID
                                join Inventory_Master_History c on a.SPOD_HistoryID=c.InvH_Id
                                join Inventory_Master d on a.SPOD_CommodityID=d.Inv_ID   
                                join Inventory_Master e on a.SPOD_ItemID=e.Inv_ID 
                                join Acc_General_master g on a.SPOD_UnitOfMeasurement=g.Mas_ID where SPOD_Status <> 'C' and SPOD_CompID=" & iCompID & " And b.SPO_ID= " & iInvice & " And b.SPO_OrderType='S' order by b.SPO_ID,a.SPOD_HistoryID"

            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                TotalAmt = 0
                Totaltax = 0
                If IsDBNull(dtDetails.Rows(i)("SPOD_CommodityID")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_ItemID")) = False Then
                    dRow("Sl No.") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                End If

                'If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                If IsDBNull(dtDetails.Rows(i)("SPOD_Quantity")) = False Then

                    If ((dtDetails.Rows(i)("Inv_Size").ToString() = "") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "0")) Then
                        dRow("0") = dtDetails.Rows(i)("SPOD_Quantity")
                        i0Qty = i0Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("0") = 0
                    End If

                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "3") Then
                        dRow("3") = dtDetails.Rows(i)("SPOD_Quantity")
                        i3Qty = i3Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("3") = 0
                    End If

                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "4") Then
                        dRow("4") = dtDetails.Rows(i)("SPOD_Quantity")
                        i4Qty = i4Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("4") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "5") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "39") Then
                        dRow("5") = dtDetails.Rows(i)("SPOD_Quantity")
                        i5Qty = i5Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("5") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "6") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "40") Then
                        dRow("6") = dtDetails.Rows(i)("SPOD_Quantity")
                        i6Qty = i6Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("6") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "7") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "41") Then
                        dRow("7") = dtDetails.Rows(i)("SPOD_Quantity")
                        i7Qty = i7Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("7") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "8") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "42") Then
                        dRow("8") = dtDetails.Rows(i)("SPOD_Quantity")
                        i8Qty = i8Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("8") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "9") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "43") Then
                        dRow("9") = dtDetails.Rows(i)("SPOD_Quantity")
                        i9Qty = i9Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("9") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "10") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "44") Then
                        dRow("10") = dtDetails.Rows(i)("SPOD_Quantity")
                        i10Qty = i10Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("10") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "11") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "45") Then
                        dRow("11") = dtDetails.Rows(i)("SPOD_Quantity")
                        i11Qty = i11Qty + dtDetails.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("11") = 0
                    End If
                    If (dtDetails.Rows(i)("Inv_Size").ToString() = "12") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "46") Then
                        dRow("12") = dtDetails.Rows(i)("SPOD_Quantity")
                        i12Qty = i12Qty + dtDetails.Rows(i)("SPOD_Quantity")
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
                If IsDBNull(dtDetails.Rows(i)("SPOD_Quantity")) = False Then
                    dRow("Total") = dtDetails.Rows(i)("SPOD_Quantity")
                    gtQty = gtQty + dtDetails.Rows(i)("SPOD_Quantity")
                Else
                    dRow("Total") = 0
                End If

                dRow("TotalQty") = gtQty
                iTotQty = gtQty

                If IsDBNull(dtDetails.Rows(i)("SPOD_MRPRate")) = False Then
                    'dRow("Rate") = dtDetails.Rows(i)("SDD_Quantity") * dtDetails.Rows(i)("SDD_Rate")
                    dRow("Rate") = dtDetails.Rows(i)("SPOD_MRPRate")
                    gtMRP = gtMRP + dtDetails.Rows(i)("SPOD_RateAmount")
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_VAT")) = False Then
                    If dtDetails.Rows(i)("SPOD_VAT") > 0 Then
                        dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "SElect Mas_Desc From Acc_General_Master Where Mas_ID=" & dtDetails.Rows(i)("SPOD_VAT") & " and mas_Master=14 and mas_compID=" & iCompID & " ")
                    Else
                        dRow("VAT") = 0
                    End If
                    gtVAT = gtVAT + dRow("VAT")
                Else
                    dRow("VAT") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_VATAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SPOD_VATAmount")))
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_CST")) = False Then
                    If dtDetails.Rows(i)("SPOD_CST") > 0 Then
                        dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "SElect Mas_Desc From Acc_General_Master Where Mas_ID=" & dtDetails.Rows(i)("SPOD_CST") & " and mas_Master=15 and mas_compID=" & iCompID & " ")
                    Else
                        dRow("CST") = 0
                    End If
                    'dtDetails.Rows(i)("SPOD_CST")
                    gtCST = gtCST + dtDetails.Rows(i)("SPOD_CST")
                Else
                    dRow("CST") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_CSTAmount")) = False Then
                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SPOD_CSTAmount")))
                    gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("SPOD_CSTAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("SPOD_CSTAmount")
                Else
                    dRow("CSTAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("SPOD_Excise")
                    gtExise = gtExise + dtDetails.Rows(i)("SPOD_Excise")
                Else
                    dRow("Exise") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SPOD_ExciseAmount")))
                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("SPOD_ExciseAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("SPOD_ExciseAmount")
                Else
                    dRow("ExiseAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_Discount")) = False And dtDetails.Rows(i)("SPOD_Discount") > 0 Then
                    dRow("Discount") = dtDetails.Rows(i)("SPOD_Discount")
                    'objDBL.SQLGetDescription(sNameSpace, "SElect Mas_Desc From Acc_General_Master Where Mas_ID=" & dtDetails.Rows(i)("SPOD_Discount") & " and mas_Master=19 and mas_compID=" & iCompID & " ")
                    gtDiscount = gtDiscount + dRow("Discount")
                Else
                    dRow("Discount") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_DiscountRate")) = False And dtDetails.Rows(i)("SPOD_DiscountRate") > 0.0 Then
                    dRow("DiscountAmt") = dtDetails.Rows(i)("SPOD_DiscountRate")
                    gtDiscountAmt = gtDiscountAmt + dRow("DiscountAmt")
                Else
                    dRow("DiscountAmt") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("SPOD_DiscountRate")) = False And dtDetails.Rows(i)("SPOD_DiscountRate") > 0.0 Then
                    dRow("DisAmt") = dtDetails.Rows(i)("SPOD_DiscountRate")
                Else
                    dRow("DisAmt") = "0"
                End If

                TotalAmt = Totaltax + ((dRow("Rate") * dRow("Total")) - dRow("DiscountAmt"))
                'dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_RateAmount") - dtDetails.Rows(i)("SDD_DiscountAmount")))
                dRow("Total Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SPOD_TotalAmount")))
                GrandTotal = GrandTotal + dRow("Total Amount")

                'If IsDBNull(dtDetails.Rows(i)("SDM_ShippingRate")) = False Then
                '    dRow("Shipping") = dtDetails.Rows(i)("SDM_ShippingRate")
                '    dShipping = dtDetails.Rows(i)("SDM_ShippingRate")
                'Else
                '    dRow("Shipping") = 0
                '    dShipping = 0
                'End If

                dRow("Shipping") = 0
                dShipping = 0

                If IsDBNull(dtDetails.Rows(i)("SPO_GrandDiscountAmt")) = False Then
                    dRow("TradeDiscount") = dtDetails.Rows(i)("SPO_GrandDiscountAmt")
                    dTradeDiscount = dtDetails.Rows(i)("SPO_GrandDiscountAmt")
                Else
                    dRow("TradeDiscount") = 0
                    dTradeDiscount = 0
                End If

                If dtVAT.Rows.Count = 1 Then
                    'Commented '

                    gtVATAmt = Math.Round(((((GrandTotal - dTradeDiscount) + gtExiseAmt) * objDBL.SQLGetDescription(sNameSpace, "SElect Mas_Desc From Acc_General_Master Where Mas_ID=" & dtDetails.Rows(i)("SPOD_VAT") & " and mas_Master=14 and mas_compID=" & iCompID & " ")) / 100), 2)

                    'gtCSTAmt = Math.Round((((GrandTotal - dTradeDiscount) * objDBL.SQLGetDescription(sNameSpace, "SElect Mas_Desc From Acc_General_Master Where Mas_ID=" & dtDetails.Rows(i)("SPOD_CST") & " and mas_Master=15 and mas_compID=" & iCompID & " ")) / 100), 2)

                    'Commented '
                End If

                If dtCST.Rows.Count = 1 Then

                    gtCSTAmt = Math.Round(((((GrandTotal - dTradeDiscount) + gtExiseAmt) * objDBL.SQLGetDescription(sNameSpace, "SElect Mas_Desc From Acc_General_Master Where Mas_ID=" & dtDetails.Rows(i)("SPOD_CST") & " and mas_Master=15 and mas_compID=" & iCompID & " ")) / 100), 2)

                End If


                'dRow("LastAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("SDD_TotalAmount") * dtDetails.Rows(i)("SDD_Quantity")))
                'dLastAmount = dLastAmount + dRow("LastAmount")

                dRow("Unit") = dtDetails.Rows(i)("MAS_Desc")

                If IsDBNull(dtDetails.Rows(i)("SPO_GrandDiscount")) = False Then
                    dRow("TradeDis") = dtDetails.Rows(i)("SPO_GrandDiscount")
                    dTradeDis = dtDetails.Rows(i)("SPO_GrandDiscount")
                Else
                    dRow("TradeDis") = 0
                    dTradeDis = 0
                End If

                dRow("LastColAmount") = (dtDetails.Rows(i)("SPOD_Quantity") * dtDetails.Rows(i)("SPOD_TotalAmount"))

                dRow("ItemID") = dtDetails.Rows(i)("SPOD_ItemID")

                'sItemID = sItemID & "," & dtDetails.Rows(i)("SPOD_ItemID")

                'dRow("OrderCode") = dtDetails.Rows(i)("SPO_OrderCode")
                'dRow("OrderDate") = dtDetails.Rows(i)("SPO_OrderDate")
                'dRow("BM_Name") = dtDetails.Rows(i)("BM_Name")
                'dRow("BM_Address") = dtDetails.Rows(i)("BM_Address")
                'dRow("BM_MobileNo") = dtDetails.Rows(i)("BM_MobileNo")
                'dRow("BM_EmailID") = dtDetails.Rows(i)("BM_EmailID")

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

            'dr("ItemID") = sItemID

            'dr("LastAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dLastAmount))
            dt.Rows.Add(dr)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateNetAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sPartyCode As String, ByVal dDiscount As Double) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""
        Try
            'sSql = "Select Distinct(SPOD_MRPRate) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " "
            'sRate = objDBL.SQLGetDescription(sNameSpace, sSql)
            If sPartyCode.StartsWith("C") Then
                sRate = dMRPRate
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & "")
                If bCheck = True Then
                    sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                    sAmt = (sRate * 100) / (sVat + 100)
                    sVAtAmt = (sRate - sAmt)
                    sNetAmt = sRate - sVAtAmt

                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If bCheck = True Then
                        'sDiscount = objDBL.SQLGetDescription(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                        sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                    End If
                    iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iCST > 0 Then
                        sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sCSTAmt = ((sRate - sVAtAmt) * sCST) / 100
                        sNetAmt = (((sRate - sVAtAmt) - sDiscountAmt))
                    End If
                    iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iExcise > 0 Then
                        sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sExciseAmt = ((sRate - sVAtAmt) * sExcise) / 100
                        sNetAmt = ((((sRate - sVAtAmt) - sDiscountAmt)))
                    End If
                Else
                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If bCheck = True Then
                        'sDiscount = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLCheckForRecord(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = (sRate * sDiscount) / 100
                        sNetAmt = (sRate - sDiscountAmt)
                    End If
                    iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iCST > 0 Then
                        sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sCSTAmt = (sRate * sCST) / 100
                        sNetAmt = ((sRate - sDiscountAmt))
                    End If
                    iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iExcise > 0 Then
                        sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sExciseAmt = (sRate * sExcise) / 100
                        sNetAmt = (((sRate - sDiscountAmt)))
                    End If
                End If
            ElseIf sPartyCode.StartsWith("P") Then
                sRate = dMRPRate
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & "")
                If bCheck = True Then
                    sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                    sAmt = (sRate * sVat) / (100)
                    sVAtAmt = (sRate - sAmt)
                    sNetAmt = sRate

                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If bCheck = True Then
                        'sDiscount = objDBL.SQLGetDescription(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = (sRate * sDiscount) / 100
                        sNetAmt = ((sRate) - sDiscountAmt)
                    End If
                    iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iCST > 0 Then
                        sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sCSTAmt = (sRate * sCST) / 100
                        sNetAmt = (sRate - sDiscountAmt)
                    End If
                    iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iExcise > 0 Then
                        sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sExciseAmt = (sRate * sExcise) / 100
                        sNetAmt = (sRate - sDiscountAmt)
                    End If
                Else
                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If bCheck = True Then
                        'sDiscount = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLCheckForRecord(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = (sRate * sDiscount) / 100
                        sNetAmt = (sRate - sDiscountAmt)
                    End If
                    iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iCST > 0 Then
                        sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sCSTAmt = (sRate * sCST) / 100
                        sNetAmt = ((sRate - sDiscountAmt))
                    End If
                    iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iExcise > 0 Then
                        sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sExciseAmt = (sRate * sExcise) / 100
                        sNetAmt = (((sRate - sDiscountAmt)))
                    End If
                End If
            End If

            Return sNetAmt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateDiscountAmt(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sPartyCode As String, ByVal dDiscount As Double) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""
        Try
            'sSql = "Select Distinct(SPOD_MRPRate) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " "
            'sRate = objDBL.SQLGetDescription(sNameSpace, sSql)

            If sPartyCode.StartsWith("C") Then
                sRate = dMRPRate
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                If bCheck = True Then
                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If bCheck = True Then
                        sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sAmt = (sRate * 100) / (sVat + 100)
                        sVAtAmt = (sRate - sAmt)
                        sNetAmt = sRate - sVAtAmt

                        'sDiscount = objDBL.SQLGetDescription(sNameSpace, "Select Distinct(SPOD_Discount) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select Distinct(SPOD_Discount) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                        sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                    Else
                        'sDiscount = objDBL.SQLGetDescription(sNameSpace, "Select Distinct(SPOD_Discount) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select Distinct(SPOD_Discount) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = (sRate * sDiscount) / 100
                        sNetAmt = (sRate - sDiscountAmt)
                    End If
                End If
            ElseIf sPartyCode.StartsWith("P") Then
                sRate = dMRPRate
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                If bCheck = True Then
                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If bCheck = True Then
                        sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sAmt = (sRate * sVat) / (100)
                        sVAtAmt = (sRate - sAmt)
                        sNetAmt = sRate

                        'sDiscount = objDBL.SQLGetDescription(sNameSpace, "Select Distinct(SPOD_Discount) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select Distinct(SPOD_Discount) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = (sRate * sDiscount) / 100
                        sNetAmt = (sRate - sDiscountAmt)
                    Else
                        'sDiscount = objDBL.SQLGetDescription(sNameSpace, "Select Distinct(SPOD_Discount) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select Distinct(SPOD_Discount) From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = (sRate * sDiscount) / 100
                        sNetAmt = (sRate - sDiscountAmt)
                    End If
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
            sSql = "Select * from MST_Customer_Master where Cust_CODE = '" & sNameSpace & "' and Cust_Delflg <> 'D' and CUST_CompID=" & iCompID & ""
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
    Public Function GetALLVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc As String = "" : Dim sVATAmt As String = ""
        Dim sStr As String = ""
        Dim sTotalAmt As String = "" : Dim sTradeDis As String = ""
        Dim dExciseAmt As Double
        Try
            sSql = "Select Distinct(SPOD_VAT) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SPO_GrandDiscountAmt From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")
                    dExciseAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_ExciseAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    If sTradeDis <> "" Then
                        sVATAmt = (((sTotalAmt - sTradeDis) + dExciseAmt) * sVatDesc) / 100
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
    Public Function CalculateNetAmountMRP(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sPartyCode As String, ByVal sItemID As String, ByVal dDiscount As Double) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetails As New DataTable
        Dim dTotalAmount As Double
        Dim sNetAmt As Double
        Dim dItemAmount As Double
        Try

            If sPartyCode.StartsWith("C") Then
                sRate = dMRPRate
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & "")
                If bCheck = True Then
                    dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SPOD_Quantity,SPOD_VAT,SPOD_Discount,SPOD_CST,SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_ItemID in (" & sItemID & ") And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ").Tables(0)
                    If dtDetails.Rows.Count > 0 Then
                        For i = 0 To dtDetails.Rows.Count - 1
                            dTotalAmount = 0
                            sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID =" & dtDetails.Rows(i)("SPOD_VAT") & " ")
                            sAmt = (sRate * 100) / (sVat + 100)
                            sVAtAmt = (sRate - sAmt)
                            sNetAmt = sRate - sVAtAmt

                            If IsDBNull(dtDetails.Rows(i)("SPOD_Discount")) = False Then
                                'sDiscount = dtDetails.Rows(i)("SPOD_Discount")
                                sDiscount = dDiscount
                                sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                                sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                            End If

                            If IsDBNull(dtDetails.Rows(i)("SPOD_CST")) = False Then
                                sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID =" & dtDetails.Rows(i)("SPOD_CST") & " ")
                                sCSTAmt = ((sRate - sVAtAmt) * sCST) / 100
                                sNetAmt = (((sRate - sVAtAmt) - sDiscountAmt))
                            End If

                            If IsDBNull(dtDetails.Rows(i)("SPOD_Excise")) = False Then
                                sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID =" & dtDetails.Rows(i)("SPOD_Excise") & " ")
                                sExciseAmt = ((sRate - sVAtAmt) * sExcise) / 100
                                sNetAmt = ((((sRate - sVAtAmt) - sDiscountAmt)))
                            End If

                            dItemAmount = dtDetails.Rows(i)("SPOD_Quantity") * sNetAmt
                            'dTotalAmount = dTotalAmount + dItemAmount
                            'dItemAmount = 0
                            dTotalAmount = sNetAmt
                        Next
                    End If

                Else
                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If bCheck = True Then
                        'sDiscount = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLCheckForRecord(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = (sRate * sDiscount) / 100
                        sNetAmt = (sRate - sDiscountAmt)
                    End If
                    iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iCST > 0 Then
                        sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sCSTAmt = (sRate * sCST) / 100
                        sNetAmt = ((sRate - sDiscountAmt))
                    End If
                    iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iExcise > 0 Then
                        sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sExciseAmt = (sRate * sExcise) / 100
                        sNetAmt = (((sRate - sDiscountAmt)))
                    End If
                End If

            End If

            Return dTotalAmount

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateDiscountAmtMRP(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sPartyCode As String, ByVal sItemID As String, ByVal dDiscount As Double) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetail As New DataTable
        Dim dDisTotal As Double
        Dim dItemDiscount As Double
        Try

            If sPartyCode.StartsWith("C") Then
                sRate = dMRPRate
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                If bCheck = True Then
                    dtDetail = objDBL.SQLExecuteDataSet(sNameSpace, "Select SPOD_Quantity,SPOD_VAT,SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_ItemID in(" & sItemID & ") And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ").Tables(0)
                    If dtDetail.Rows.Count > 0 Then
                        For j = 0 To dtDetail.Rows.Count - 1
                            dDisTotal = 0
                            If IsDBNull(dtDetail.Rows(j)("SPOD_VAT")) = False And dtDetail.Rows(j)("SPOD_VAT") > 0 Then
                                sVat = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID=" & dtDetail.Rows(j)("SPOD_VAT") & " ")
                                sAmt = (sRate * 100) / (sVat + 100)
                                sVAtAmt = (sRate - sAmt)
                                sNetAmt = sRate - sVAtAmt

                                'sDiscount = dtDetail.Rows(j)("SPOD_Discount")
                                sDiscount = dDiscount
                                sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                                sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                            Else
                                'sDiscount = dtDetail.Rows(j)("SPOD_Discount")
                                sDiscount = dDiscount
                                sDiscountAmt = (sRate * sDiscount) / 100
                                sNetAmt = (sRate - sDiscountAmt)
                            End If
                            dItemDiscount = dtDetail.Rows(j)("SPOD_Quantity") * sDiscountAmt
                            dDisTotal = dDisTotal + dItemDiscount
                            'sDiscountAmt = 0 : dItemDiscount = 0
                            dDisTotal = sDiscountAmt
                        Next
                    End If
                End If
            End If

            Return dDisTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateDiscountAmtMRPCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sPartyCode As String, ByVal sItemID As String, ByVal dDiscount As Double) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim sNetAmt As String = "" : Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetail As New DataTable
        Dim dDisTotal As Double
        Dim dItemDiscount As Double
        Try

            If sPartyCode.StartsWith("C") Then
                sRate = dMRPRate
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                If bCheck = True Then
                    dtDetail = objDBL.SQLExecuteDataSet(sNameSpace, "Select SPOD_Quantity,SPOD_CST,SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_ItemID in(" & sItemID & ") And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ").Tables(0)
                    If dtDetail.Rows.Count > 0 Then
                        For j = 0 To dtDetail.Rows.Count - 1
                            dDisTotal = 0
                            If IsDBNull(dtDetail.Rows(j)("SPOD_CST")) = False And dtDetail.Rows(j)("SPOD_CST") > 0 Then
                                sVat = objDBL.SQLGetDescription(sNameSpace, "Select InvH_Vat From Inventory_Master_History Where INVH_INV_ID=" & sItemID & " ")
                                sAmt = (sRate * 100) / (sVat + 100)
                                sVAtAmt = (sRate - sAmt)
                                sNetAmt = sRate - sVAtAmt

                                'sDiscount = dtDetail.Rows(j)("SPOD_Discount")
                                sDiscount = dDiscount
                                sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                                sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                            Else
                                'sDiscount = dtDetail.Rows(j)("SPOD_Discount")
                                sDiscount = dDiscount
                                sDiscountAmt = (sRate * sDiscount) / 100
                                sNetAmt = (sRate - sDiscountAmt)
                            End If
                            dItemDiscount = dtDetail.Rows(j)("SPOD_Quantity") * sDiscountAmt
                            dDisTotal = dDisTotal + dItemDiscount
                            'sDiscountAmt = 0 : dItemDiscount = 0
                            dDisTotal = sDiscountAmt
                        Next
                    End If
                End If
            End If

            Return dDisTotal
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CalculateNetAmountMRPCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer, ByVal dMRPRate As Double, ByVal sPartyCode As String, ByVal sItemID As String, ByVal dDiscount As Double) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sRate As String = ""
        Dim sVat As String = "" : Dim sAmt As String = "" : Dim sVAtAmt As String = ""
        Dim sDiscount As String = "" : Dim sCST As String = "" : Dim sExcise As String = "" : Dim sDiscountAmt As String = ""
        Dim iCST As Integer : Dim sCSTAmt As String = "" : Dim iExcise As String = "" : Dim sExciseAmt As String = ""

        Dim dtDetails As New DataTable
        Dim dTotalAmount As Double
        Dim sNetAmt As Double
        Dim dItemAmount As Double
        Try

            If sPartyCode.StartsWith("C") Then
                sRate = dMRPRate
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_VAT From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & "")
                If bCheck = True Then
                    dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, "Select SPOD_Quantity,SPOD_VAT,SPOD_Discount,SPOD_CST,SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_ItemID in (" & sItemID & ") And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ").Tables(0)
                    If dtDetails.Rows.Count > 0 Then
                        For i = 0 To dtDetails.Rows.Count - 1
                            dTotalAmount = 0
                            sVat = objDBL.SQLGetDescription(sNameSpace, "Select INVH_Vat From Inventory_Master_History Where INVH_INV_ID =" & sItemID & " ")
                            sAmt = (sRate * 100) / (sVat + 100)
                            sVAtAmt = (sRate - sAmt)
                            sNetAmt = sRate - sVAtAmt

                            If IsDBNull(dtDetails.Rows(i)("SPOD_Discount")) = False Then
                                'sDiscount = dtDetails.Rows(i)("SPOD_Discount")
                                sDiscount = dDiscount
                                sDiscountAmt = ((sRate - sVAtAmt) * sDiscount) / 100
                                sNetAmt = ((sRate - sVAtAmt) - sDiscountAmt)
                            End If

                            If IsDBNull(dtDetails.Rows(i)("SPOD_CST")) = False Then
                                sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID =" & dtDetails.Rows(i)("SPOD_CST") & " ")
                                sCSTAmt = (((sRate - sVAtAmt) - sDiscountAmt) * sCST) / 100
                                sNetAmt = (((sRate - sVAtAmt) - sDiscountAmt))
                            End If

                            If IsDBNull(dtDetails.Rows(i)("SPOD_Excise")) = False Then
                                sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID =" & dtDetails.Rows(i)("SPOD_Excise") & " ")
                                sExciseAmt = (((sRate - sVAtAmt) - sDiscountAmt) * sExcise) / 100
                                sNetAmt = ((((sRate - sVAtAmt) - sDiscountAmt)))
                            End If

                            dItemAmount = dtDetails.Rows(i)("SPOD_Quantity") * sNetAmt
                            'dTotalAmount = dTotalAmount + dItemAmount
                            'dItemAmount = 0
                            dTotalAmount = sNetAmt
                        Next
                    End If

                Else
                    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If bCheck = True Then
                        'sDiscount = objDBL.SQLCheckForRecord(sNameSpace, "Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                        'objDBL.SQLCheckForRecord(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Discount From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sDiscount = dDiscount
                        sDiscountAmt = (sRate * sDiscount) / 100
                        sNetAmt = (sRate - sDiscountAmt)
                    End If
                    iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iCST > 0 Then
                        sCST = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_CST From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sCSTAmt = (sRate * sCST) / 100
                        sNetAmt = ((sRate - sDiscountAmt))
                    End If
                    iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " ")
                    If iExcise > 0 Then
                        sExcise = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID in (Select SPOD_Excise From Sales_ProForma_Order_Details where SPOD_SOID =" & iInvice & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & ") ")
                        sExciseAmt = (sRate * sExcise) / 100
                        sNetAmt = (((sRate - sDiscountAmt)))
                    End If
                    dTotalAmount = sNetAmt

                End If

            End If

            Return dTotalAmount

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
        Dim dExciseAmt As Double
        Try
            sSql = "Select Distinct(SPOD_CST) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                For i = 0 To dt.Rows.Count - 1
                    sCstDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")
                    sTotalAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sTradeDis = objDBL.SQLGetDescription(sNameSpace, "Select SPO_GrandDiscountAmt From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")
                    dExciseAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_ExciseAmount) From Sales_Proforma_Order_Details where SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    If sTradeDis <> "" Then
                        sCSTAmt = (((sTotalAmt - sTradeDis) + dExciseAmt) * sCstDesc) / 100
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
    Public Function GetVATBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim dTradeDiscountAmt As Double
        Try

            dt1.Columns.Add("VAT")
            dt1.Columns.Add("Amount")
            dt1.Columns.Add("VATAmount")

            sSql = "Select Distinct(SPOD_VAT) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_VATAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")

                    dRow("VAT") = sVatDesc
                    dRow("Amount") = sAmountTot
                    dRow("VATAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            ElseIf dt.Rows.Count = 1 Then
                dRow = dt1.NewRow()
                sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(0)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_VAT=" & dt.Rows(0)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_VATAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_VAT=" & dt.Rows(0)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                dTradeDiscountAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPO_GrandDiscountAmt) From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")

                dRow("VAT") = sVatDesc
                dRow("Amount") = sAmountTot
                dRow("VATAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((sAmountTot - dTradeDiscountAmt) * sVatDesc) / 100))

                dt1.Rows.Add(dRow)
                sVatDesc = "" : sAmountTot = "" : sVATAmt = "" : dTradeDiscountAmt = 0

            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt As String
        Dim sAmountTot As String = ""
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow

        Dim dTradeDiscountAmt As Double
        Try

            dt1.Columns.Add("CST")
            dt1.Columns.Add("CAmount")
            dt1.Columns.Add("CSTAmount")

            sSql = "Select Distinct(SPOD_CST) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")

                    If dt.Rows(i)("SPOD_CST") > 0 Then
                        sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    End If
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_CSTAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_CST=" & dt.Rows(i)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")

                    dRow("CST") = sVatDesc
                    dRow("CAmount") = sAmountTot
                    dRow("CSTAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            ElseIf dt.Rows.Count = 1 Then
                dRow = dt1.NewRow()
                sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(0)("SPOD_CST") & " And MAS_CompID=" & iCompID & " ")

                If dt.Rows(0)("SPOD_CST") > 0 Then
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_TotalAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_CST=" & dt.Rows(0)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                End If
                sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_CSTAmount) From Sales_Proforma_Order_Details where SPOD_Status<>'C' And SPOD_CST=" & dt.Rows(0)("SPOD_CST") & " And SPOD_SOID=" & iOrderID & " And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                dTradeDiscountAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPO_GrandDiscountAmt) From Sales_Proforma_Order where SPO_ID=" & iOrderID & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " ")

                dRow("CST") = sVatDesc
                dRow("CAmount") = sAmountTot
                dRow("CSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((sAmountTot - dTradeDiscountAmt) * sVatDesc) / 100))

                dt1.Rows.Add(dRow)
                sVatDesc = "" : sAmountTot = "" : sVATAmt = "" : dTradeDiscountAmt = 0
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
