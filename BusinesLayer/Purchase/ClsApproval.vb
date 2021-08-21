'Public Class ClsApproval

'End Class
'Public Class ClsDabitOrCreditNote

'End Class


Imports System
Imports System.Data
Imports DatabaseLayer
Imports System.Data.OleDb
Public Class ClsApproval
    Dim objDB As New DBHelper
    Public Function loadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
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
        Dim Total, TotalAmt, Totaltax As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtdiscount, gtdiscountAmt, GrandTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Colour")
            dt.Columns.Add("t3")
            dt.Columns.Add("t4")
            dt.Columns.Add("t5")
            dt.Columns.Add("t6")
            dt.Columns.Add("t7")
            dt.Columns.Add("t8")
            dt.Columns.Add("t9")
            dt.Columns.Add("t10")
            dt.Columns.Add("t11")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("TotalAmount")
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,"
            sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_AcceptedQnt,b.PIA_MRP,b.PIA_Status,"
            sSql = sSql & " b.PIA_CompID,b.PIA_Excess,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,"
            sSql = sSql & " d.Inv_Description Commodity,"
            sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
            sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status"
            sSql = sSql & "  from Purchase_verification"
            sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID  where PIA_CompID=" & iCompID & ""
            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo= " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                TotalAmt = 0
                Totaltax = 0
                If IsDBNull(dtDetails.Rows(i)("PIA_Commodity")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                End If
                If IsDBNull(dtDetails.Rows(i)("PIA_DescriptionID")) = False Then
                    dRow("SlNo") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                End If
                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                End If
                If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        If (dtDetails.Rows(i)("Inv_Size") = 3) Then
                            dRow("t3") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 4) Then
                            dRow("t4") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 5) Or (dtDetails.Rows(i)("Inv_Size") = 39) Then
                            dRow("t5") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 6) Or (dtDetails.Rows(i)("Inv_Size") = 40) Then
                            dRow("t6") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 7) Or (dtDetails.Rows(i)("Inv_Size") = 41) Then
                            dRow("t7") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 8) Or (dtDetails.Rows(i)("Inv_Size") = 42) Then
                            dRow("t8") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 9) Or (dtDetails.Rows(i)("Inv_Size") = 43) Then
                            dRow("t9") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 10) Or (dtDetails.Rows(i)("Inv_Size") = 44) Then
                            dRow("t10") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 11) Or (dtDetails.Rows(i)("Inv_Size") = 45) Then
                            dRow("t11") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        End If
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                    dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                    gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                Else
                    dRow("TotalQty") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIA_MRP")) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("PIA_MRP")
                    gtMRP = gtMRP + dtDetails.Rows(i)("PIA_MRP")
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                    dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                    gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                Else
                    dRow("VAT") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_VATAmount")))
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                    dRow("CST") = dtDetails.Rows(i)("POD_CST")
                    gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                Else
                    dRow("CST") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                    gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                Else
                    dRow("CSTAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                    gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                Else
                    dRow("Exise") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                Else
                    dRow("ExiseAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False Then
                    dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                    gtdiscount = gtdiscount + dRow("Discount")
                Else
                    dRow("Discount") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                    dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                    gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                Else
                    dRow("DiscountAmt") = "0"
                End If

                TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(TotalAmt))
                GrandTotal = GrandTotal + TotalAmt

                dt.Rows.Add(dRow)
            Next

            dtDetails.Clear()

            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,PV_DocRefNo,
                                b.PIE_ID,b.PIE_OrderID,b.PIE_GINID,b.PIE_CommodityID,b.PIE_Description,b.PIE_HistoryID,b.PIE_UnitID,b.PIE_Rate,b.PIE_Quantity,b.PIE_RateAmount,
                                b.PIE_Discount,b.PIE_DiscountAmount,b.PIE_Excise,b.PIE_ExciseAmount,b.PIE_Vat,b.PIE_VatAmount,b.PIE_TotalAmount,b.PIE_AcceptQty,b.PIE_DocRef,
                            	c.Inv_Description,c.Inv_Color,c.Inv_Size,
                              	d.Inv_Description Commodity	
                                from Purchase_verification
	                            join Purchase_Invoice_Excess b on PV_DocRefNo=b.PIE_DocRef
	                            join Inventory_Master c on b.PIE_Description=c.Inv_ID
                            	join Inventory_Master d on b.PIE_CommodityID=d.Inv_ID
                            	join Purchase_GIN_Master e on PV_GinNo=e.PGM_ID 
                            	join Purchase_Order_Master f on PV_OrderNo =f.POM_ID"

            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo= " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIE_ID,b.PIE_Description"
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                TotalAmt = 0
                Totaltax = 0
                If IsDBNull(dtDetails.Rows(i)("PIE_CommodityID")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Description")) = False Then
                    dRow("SlNo") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("PIE_AcceptQty")) = False Then
                        If (dtDetails.Rows(i)("Inv_Size") = 3) Then
                            dRow("t3") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If

                        If (dtDetails.Rows(i)("Inv_Size") = 4) Then
                            dRow("t4") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 5) Or (dtDetails.Rows(i)("Inv_Size") = 39) Then
                            dRow("t5") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 6) Or (dtDetails.Rows(i)("Inv_Size") = 40) Then
                            dRow("t6") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 7) Or (dtDetails.Rows(i)("Inv_Size") = 41) Then
                            dRow("t7") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 8) Or (dtDetails.Rows(i)("Inv_Size") = 42) Then
                            dRow("t8") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 9) Or (dtDetails.Rows(i)("Inv_Size") = 43) Then
                            dRow("t9") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 10) Or (dtDetails.Rows(i)("Inv_Size") = 44) Then
                            dRow("t10") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 11) Or (dtDetails.Rows(i)("Inv_Size") = 45) Then
                            dRow("t11") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_AcceptQty")) = False Then
                    dRow("TotalQty") = dtDetails.Rows(i)("PIE_AcceptQty")
                    gtQty = gtQty + dtDetails.Rows(i)("PIE_AcceptQty")
                Else
                    dRow("TotalQty") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Rate")) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("PIE_Rate")
                    gtMRP = gtMRP + dtDetails.Rows(i)("PIE_Rate")
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Vat")) = False Then
                    dRow("VAT") = dtDetails.Rows(i)("PIE_Vat")
                    gtVAT = gtVAT + dtDetails.Rows(i)("PIE_Vat")
                Else
                    dRow("VAT") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_VatAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("PIE_VatAmount")))
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If
                'If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                '    dRow("CST") = dtDetails.Rows(i)("POD_CST")
                '    gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                'Else
                dRow("CST") = 0
                dRow("CSTAmt") = 0
                'End If
                If IsDBNull(dtDetails.Rows(i)("PIE_Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("PIE_Excise")
                    gtExise = gtExise + dtDetails.Rows(i)("PIE_Excise")
                Else
                    dRow("Exise") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("PIE_ExciseAmount")))
                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("PIE_ExciseAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("PIE_ExciseAmount")
                Else
                    dRow("ExiseAmt") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_Discount")) = False And dtDetails.Rows(i)("PIE_Discount") <> "" Then
                    dRow("Discount") = dtDetails.Rows(i)("PIE_Discount")
                    gtdiscount = gtdiscount + dRow("Discount")
                Else
                    dRow("Discount") = "0"
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_DiscountAmount")) = False And dtDetails.Rows(i)("PIE_DiscountAmount") <> "" Then
                    dRow("DiscountAmt") = dtDetails.Rows(i)("PIE_DiscountAmount")
                    gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                Else
                    dRow("DiscountAmt") = "0"
                End If
                TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(TotalAmt))
                GrandTotal = GrandTotal + TotalAmt
                dt.Rows.Add(dRow)
            Next
            'dr = dt.NewRow()
            'dr("Commodity") = <b>Total</b>
            'dr("TotalQty") = gtQty
            'dr("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(gtMRP))
            'dr("VAT") = gtVAT
            'dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
            'dr("CST") = gtCST
            'dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
            'dr("Exise") = gtExise
            'dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtExiseAmt))
            'dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscount))
            'dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscountAmt))
            'dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandTotal))
            'dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function



    Public Function ApproveReturnItem(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As String, ByVal ItemId As String) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            sSql = "update Purchase_Invoice_Rejected set PIR_Status='A'  where PIR_OrderID =" & iorder & " And PIR_DescriptionID = " & ItemId & " and PIR_GINID = " & iInvoice & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function RemoveDublicate(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("VAT"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("VAT"), String.Empty)
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
    Public Function loadDetailsB(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
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
        Dim Total, TotalAmt, Totaltax, TotalVat As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtdiscount, gtdiscountAmt, GrandTotal, subTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("CSTAmtTotal")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("SubTotal")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalVat")
            dt.Columns.Add("UnitId")
            dt.Columns.Add("AltUnit")
            dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("INVH_Mdate")
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,"
            sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,pir.PIR_RejectedQty PIA_AcceptedQnt,b.PIA_MRP,b.PIA_Status,"
            sSql = sSql & " b.PIA_CompID,b.PIA_Excess,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,"
            sSql = sSql & " d.Inv_Description Commodity,"
            sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
            sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate"
            sSql = sSql & "  from Purchase_verification"
            sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            sSql = sSql & "  join Purchase_Invoice_Rejected pir On PV_GinNo=pir.PIR_GINID"
            sSql = sSql & " join Inventory_Master_history InvH On b.PIA_HistoryID=InvH.InvH_ID"
            sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID  where PIA_CompID=" & iCompID & " "
            sSql = sSql & " And PV_OrderNo= " & iorder & " "
            sSql = sSql & " And PV_ID= " & iInvoice & " "
            sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    If IsDBNull(dtDetails.Rows(i)("PIA_Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_DescriptionID")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_MRP")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("PIA_MRP")
                        gtMRP = gtMRP + dtDetails.Rows(i)("PIA_MRP")
                    Else
                        dRow("Rate") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                    Else
                        dRow("VAT") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_VAT") * (dRow("Rate") * dRow("TotalQty"))) / 100))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                        gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                    Else
                        dRow("CSTAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("INVH_Mdate")) = False Then
                        dRow("INVH_Mdate") = dtDetails.Rows(i)("INVH_Mdate")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("INVH_Edate")) = False Then
                        dRow("INVH_Edate") = dtDetails.Rows(i)("INVH_Edate")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
                        dRow("INVH_MRP") = dtDetails.Rows(i)("INVH_MRP")
                    End If
                    subTotal = subTotal + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = subTotal
                    dRow("TotalVat") = TotalVat
                    dRow("CSTAmtTotal") = gtCSTAmt
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(subTotal + TotalVat + gtCSTAmt))
                    dt.Rows.Add(dRow)
                End If
            Next

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function loadDetailsExcess(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
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
        Dim Total, TotalAmt, Totaltax, TotalVat As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtdiscount, gtdiscountAmt, GrandTotal, subTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("CSTAmtTotal")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("SubTotal")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalVat")
            dt.Columns.Add("UnitId")
            dt.Columns.Add("AltUnit")
            dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("INVH_Mdate")
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,"
            sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_MRP,b.PIA_Status,"
            sSql = sSql & " b.PIA_CompID,b.PIA_Excess as PIA_AcceptedQnt,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,"
            sSql = sSql & " d.Inv_Description Commodity,"
            sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,b.PIA_Excess as POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
            sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate"
            sSql = sSql & "  from Purchase_verification"
            sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            sSql = sSql & "  join Purchase_Invoice_Rejected pir On PV_GinNo=pir.PIR_GINID"
            sSql = sSql & " join Inventory_Master_history InvH On b.PIA_HistoryID=InvH.InvH_ID"
            sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID  where PIA_CompID=" & iCompID & " and b.PIA_Excess<>0 "
            sSql = sSql & " And PV_OrderNo= " & iorder & " "
            sSql = sSql & " And PV_ID= " & iInvoice & " "
            sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    If IsDBNull(dtDetails.Rows(i)("PIA_Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_DescriptionID")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_MRP")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("PIA_MRP")
                        gtMRP = gtMRP + dtDetails.Rows(i)("PIA_MRP")
                    Else
                        dRow("Rate") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                    Else
                        dRow("VAT") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_VAT") * (dRow("Rate") * dRow("TotalQty"))) / 100))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                        gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                    Else
                        dRow("CSTAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("INVH_Mdate")) = False Then
                        dRow("INVH_Mdate") = dtDetails.Rows(i)("INVH_Mdate")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("INVH_Edate")) = False Then
                        dRow("INVH_Edate") = dtDetails.Rows(i)("INVH_Edate")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
                        dRow("INVH_MRP") = dtDetails.Rows(i)("INVH_MRP")
                    End If

                    subTotal = subTotal + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = subTotal
                    dRow("TotalVat") = TotalVat
                    dRow("CSTAmtTotal") = gtCSTAmt
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(subTotal + TotalVat + gtCSTAmt))
                    dt.Rows.Add(dRow)
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Commodity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Description, Inv_Parent from Inventory_Master where Inv_Parent=0 And Inv_CompID=" & iCompID & ""
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Order(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_Status='A' And POM_CompID=" & iCompID & " Order By POM_ID desc"

            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Invoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer) As DataTable
        Dim sSql As String = ""
        Try
            If iorder <> 0 Then
                sSql = "" : sSql = "select PV_ID, PV_BillNo from Purchase_verification where PV_OrderNo=" & iorder & " and PV_CompID=" & iCompID & " Order By PV_ID "
            Else
                sSql = "" : sSql = "select PV_ID, PV_BillNo from Purchase_verification where PV_CompID=" & iCompID & " Order By PV_ID "
            End If

            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SupplierMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select * from Purchase_Order_Master Join customerSupplierMaster b on POM_Supplier=b.CSM_ID where  POM_ID=" & iorder & " And POM_CompID=" & iCompID & " "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SupplierDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal isupplier As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select CST_ID,CST_SupplierID,CST_Description,CST_Value,CST_CompID,CST_Status from  Customer_Supplier_Template where  CST_SupplierID=" & isupplier & " And CST_CompID=" & iCompID & " "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class



