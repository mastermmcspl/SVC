Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsSalesDynamicReport
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function GetApplicationStartDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = ""
        Dim sDate As String = ""
        Try
            sSql = "Select AS_StartDate From Application_Settings Where AS_YearID=" & iYearID & " And AS_CompID=" & iCompID & " "
            sDate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function OrderNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Distinct(SPO_ID),SPO_OrderCode from sales_Proforma_order,Sales_Allocate_Master where SPO_OrderType='O' Or (SPO_ID=SAM_OrderNo And SPO_OrderType='S' And SAM_YearID =" & iYearID & " And SAM_CompID=" & iCompID & ") and SPO_YearID =" & iYearID & " And SPO_CompID=" & iCompID & " Order By SPO_OrderCode Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCommodity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadItems(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, Optional ByVal iCommodityID As Integer = 0) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            If iCommodityID > 0 Then
                'sSql = "Select INV_ID,INV_Description From Inventory_Master Where INV_Parent=" & iCommodityID & " And INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & ")"
                sSql = "Select INV_ID,(INV_Code + ' - ' +Inv_Description) As INV_Code From Inventory_Master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & " and SL_Commodity =" & iCommodityID & ") and INV_Code <> '' order by Inv_Code"
            Else
                'sSql = "Select INV_ID,INV_Description From Inventory_Master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & ")"
                sSql = "Select INV_ID,(INV_Code + ' - ' +Inv_Description) As INV_Code From Inventory_Master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & ") and INV_Code <> '' order by Inv_Code"
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDetailsNew(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderNo As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iParty As Integer, ByVal dFrom As Date, ByVal dTo As Date, ByVal dDiscount As Double, ByVal dVAT As Double, ByVal dCST As Double, ByVal dExcise As Double) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim sOrderNO As String = ""
        Dim sCommodity As String = ""

        Dim iTotalOrderedQty, iTotalAllocatedQty, iTotalDispatchedQty As Integer
        Dim dTotalDiscount, dTotalDiscountAmt, dMRPRate As Double
        Dim dTotalVAT, dTotalVATAmt As Double
        Dim dTotalCST, dTotalCSTAmt As Double
        Dim dTotalExcise, dTotalExciseAmt As Double
        Dim dTotalNetAmt As Double
        Try
            dt1.Columns.Add("OrderNo")
            dt1.Columns.Add("Commodity")
            dt1.Columns.Add("Description")
            dt1.Columns.Add("Party")
            dt1.Columns.Add("MRPRate")
            dt1.Columns.Add("OrderedQty")
            dt1.Columns.Add("AllocatedQty")
            dt1.Columns.Add("DispatchedQty")
            dt1.Columns.Add("Discount")
            dt1.Columns.Add("DiscountAmt")
            dt1.Columns.Add("VAT")
            dt1.Columns.Add("VATAmt")
            dt1.Columns.Add("CST")
            dt1.Columns.Add("CSTAmt")
            dt1.Columns.Add("Excise")
            dt1.Columns.Add("ExciseAmt")
            dt1.Columns.Add("Total")

            If iParty > 0 And iCommodityID > 0 And iItemID > 0 Then

            ElseIf iParty > 0 And iCommodityID > 0 And iItemID = 0 Then

            ElseIf iParty > 0 And iCommodityID = 0 And iItemID > 0 Then

            ElseIf iParty = 0 And iCommodityID > 0 And iItemID > 0 Then

            End If

            Dim dtCommodity As New DataTable
            dtCommodity = objDBL.SQLExecuteDataSet(sNameSpace, "Select Distinct(SDD_CommodityID) From Sales_Dispatch_Details Where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderNo & " And SDM_CompID=" & iCompID & ") ").Tables(0)

            If dtCommodity.Rows.Count > 0 Then
                For j = 0 To dtCommodity.Rows.Count - 1
                    sSql = "Select * From Sales_Dispatch_Details Where SDD_CommodityID=" & dtCommodity.Rows(j)("SDD_CommodityID") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderNo & " And SDM_CompID=" & iCompID & ") "
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            dRow = dt1.NewRow
                            'dRow("OrderNo") = objDBL.SQLGetDescription(sNameSpace, "Select SPO_OrderCode From Sales_ProForma_Order Where SPO_ID=" & iOrderNo & " And SPO_CompID=" & iCompID & " ")
                            dRow("OrderNo") = objDBL.SQLGetDescription(sNameSpace, "Select SPO_OrderCode From Sales_ProForma_Order Where SPO_ID=" & iOrderNo & " And SPO_CompID=" & iCompID & " ")
                            If dRow("OrderNo") = sOrderNO Then
                                dRow("OrderNo") = ""
                            End If
                            sOrderNO = objDBL.SQLGetDescription(sNameSpace, "Select SPO_OrderCode From Sales_ProForma_Order Where SPO_ID=" & iOrderNo & " And SPO_CompID=" & iCompID & " ")
                            'dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("SDD_CommodityID") & " And INV_Parent=0 And INV_CompID=" & iCompID & " ")
                            dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("SDD_CommodityID") & " And INV_Parent=0 And INV_CompID=" & iCompID & " ")
                            If dRow("Commodity") = sCommodity Then
                                dRow("Commodity") = ""
                            End If
                            sCommodity = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("SDD_CommodityID") & " And INV_Parent=0 And INV_CompID=" & iCompID & " ")

                            dRow("Description") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("SDD_DescID") & " And INV_Parent=" & dt.Rows(i)("SDD_CommodityID") & " And INV_CompID=" & iCompID & " ")
                            dRow("Party") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID in (Select SDM_SupplierID From Sales_Dispatch_Master where SDM_OrderID=" & iOrderNo & " And SDM_CompID=" & iCompID & ")")
                            dRow("MRPRate") = dt.Rows(i)("SDD_Rate")
                            dMRPRate = dMRPRate + dRow("MRPRate")

                            dRow("OrderedQty") = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SPOD_Quantity From Sales_ProForma_Order_Details where SPOD_SOID =" & iOrderNo & " And SPOD_CommodityID=" & dt.Rows(i)("SDD_CommodityID") & " And SPOD_ItemID=" & dt.Rows(i)("SDD_DescID") & " And SPOD_CompID=" & iCompID & " ")
                            iTotalOrderedQty = iTotalOrderedQty + dRow("OrderedQty")

                            dRow("AllocatedQty") = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SAD_PlacedQnt From Sales_Allocate_Details where SAD_MasterID in (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderNo & " And SAM_CompID=" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SDD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SDD_DescID") & " And SAD_CompID=" & iCompID & " ")
                            iTotalAllocatedQty = iTotalAllocatedQty + dRow("AllocatedQty")

                            dRow("DispatchedQty") = dt.Rows(i)("SDD_Quantity")
                            iTotalDispatchedQty = iTotalDispatchedQty + dRow("DispatchedQty")

                            dRow("Discount") = dt.Rows(i)("SDD_Discount")
                            dTotalDiscount = dTotalDiscount + dRow("Discount")

                            dRow("DiscountAmt") = dt.Rows(i)("SDD_DiscountAmount")
                            dTotalDiscountAmt = dTotalDiscountAmt + dRow("DiscountAmt")

                            dRow("VAT") = dt.Rows(i)("SDD_Vat")
                            dTotalVAT = dTotalVAT + dRow("VAT")

                            dRow("VATAmt") = dt.Rows(i)("SDD_VatAmount")
                            dTotalVATAmt = dTotalVATAmt + dRow("VATAmt")

                            dRow("CST") = dt.Rows(i)("SDD_CST")
                            dTotalCST = dTotalCST + dRow("CST")

                            dRow("CSTAmt") = dt.Rows(i)("SDD_CSTAmount")
                            dTotalCSTAmt = dTotalCSTAmt + dRow("CSTAmt")

                            dRow("Excise") = dt.Rows(i)("SDD_Excise")
                            dTotalExcise = dTotalExcise + dRow("Excise")

                            dRow("ExciseAmt") = dt.Rows(i)("SDD_ExciseAmount")
                            dTotalExciseAmt = dTotalExciseAmt + dRow("ExciseAmt")

                            dRow("Total") = dt.Rows(i)("SDD_TotalAmount")
                            dTotalNetAmt = dTotalNetAmt + dRow("Total")

                            dt1.Rows.Add(dRow)
                        Next
                    End If

                    dRow = dt1.NewRow
                    dRow("OrderNo") = "<B>" & "Total" & "</B>"
                    dRow("Commodity") = ""
                    dRow("Description") = ""
                    dRow("Party") = ""
                    dRow("MRPRate") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dMRPRate)) & "</B>"
                    dRow("OrderedQty") = "<B>" & iTotalOrderedQty & "</B>"
                    dRow("AllocatedQty") = "<B>" & iTotalAllocatedQty & "</B>"
                    dRow("DispatchedQty") = "<B>" & iTotalDispatchedQty & "</B>"
                    dRow("Discount") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dTotalDiscount)) & "</B>"
                    dRow("DiscountAmt") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dTotalDiscountAmt)) & "</B>"
                    dRow("VAT") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dTotalVAT)) & "</B>"
                    dRow("VATAmt") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dTotalVATAmt)) & "</B>"
                    dRow("CST") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dTotalCST)) & "</B>"
                    dRow("CSTAmt") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dTotalCSTAmt)) & "</B>"
                    dRow("Excise") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dTotalExcise)) & "</B>"
                    dRow("ExciseAmt") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dTotalExciseAmt)) & "</B>"
                    dRow("Total") = "<B>" & String.Format("{0:0.00}", Convert.ToDecimal(dTotalNetAmt)) & "</B>"
                    dt1.Rows.Add(dRow)

                    iTotalOrderedQty = 0 : iTotalAllocatedQty = 0 : iTotalDispatchedQty = 0 : dMRPRate = 0.0
                    dTotalDiscount = 0.0 : dTotalDiscountAmt = 0.0 : dTotalVAT = 0.0 : dTotalVATAmt = 0.0
                    dTotalCST = 0.0 : dTotalCSTAmt = 0.0 : dTotalExcise = 0.0 : dTotalExciseAmt = 0.0 : dTotalNetAmt = 0.0

                Next
            End If
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iParty As Integer, ByVal dFrom As Date, ByVal dTo As Date, ByVal dDiscount As Double, ByVal dVAT As Double, ByVal dCST As Double, ByVal dExcise As Double, ByVal iDispatchNo As Integer, ByVal dApplicationDate As String, ByVal iZone As Integer, ByVal iRegion As Integer, ByVal iArea As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try

            'sSql = "Select b.SDD_ID,a.SDM_ID,a.SDM_OrderID,a.SDM_Code,a.SDM_DispatchRefNo,a.SDM_OrderDate,a.SDM_SupplierID,a.SDM_DispatchDate,a.SDM_ShippingRate,a.SDM_GrandDiscount,a.SDM_GrandDiscountAmt,b.SDD_CommodityID,b.SDD_DescID,b.SDD_Rate,b.SDD_Quantity,b.SDD_Discount,b.SDD_DiscountAmount,b.SDD_Amount,b.SDD_SGST,b.SDD_SGSTAmount,b.SDD_CGST,b.SDD_CGSTAmount,b.SDD_IGST,b.SDD_IGSTAmount,b.SDD_TotalAmount,
            '        c.SPO_OrderCode,d.SPOD_Quantity,f.SAD_PlacedQnt,f.SAD_PendingQty,g.INV_Description As Commodity,h.INV_Code As Item,i.BM_Name As party,c.SPO_ZoneID,c.SPO_RegionID,c.SPO_AreaID,c.SPO_BranchID
            '        From Sales_Dispatch_Master a
            '        Join Sales_Dispatch_Details b On b.SDD_MasterID=a.SDM_ID 
            '        Left Join Sales_PRoForma_Order c on c.SPO_ID=a.SDM_OrderID And c.SPO_YearID=a.SDM_YearID
            '        Left Join Sales_PRoForma_Order_Details d On d.SPOD_SOID = a.SDM_OrderID and d.spod_commodityid = b.sdd_CommodityID and d.spod_itemid =b.sdd_DescID and d.spod_HistoryID = b.Sdd_historyID And d.SPOD_YearID=a.SDM_YearID
            '        Left Join Sales_Allocate_Master e On e.SAM_OrderNo=a.SDM_OrderID And e.SAM_ID=a.SDM_AllocateID And e.SAM_YearID=a.SDM_YearID
            '        Left Join Sales_Allocate_Details f On f.SAD_MasterID=e.SAM_ID and f.sad_commodity = b.sdd_CommodityID and f.sad_Descid =b.sdd_DescID and f.sad_HisotryID = b.Sdd_historyID And f.sad_YearID=a.SDM_YearID
            '        Left Join Inventory_Master g On g.INV_ID=b.SDD_CommodityID 
            '        Left Join Inventory_Master h On h.INV_ID=b.SDD_DescID And h.INV_ID=d.spod_itemid
            '        Left Join Sales_Buyers_Masters i On i.BM_ID=SDM_SupplierID
            '        where a.SDM_OrderDate>=" & objGen.FormatDtForRDBMS(dApplicationDate, "Q") & " And a.SDM_YearID=" & iYearID & " And a.SDM_CompID=" & iCompID & " "

            sSql = "Select Distinct(b.SDD_Rate) As SDD_Rate,a.SDM_ID,a.SDM_OrderID,a.SDM_Code,a.SDM_DispatchRefNo,a.SDM_OrderDate,a.SDM_SupplierID,a.SDM_DispatchDate,a.SDM_ShippingRate,a.SDM_GrandDiscount,a.SDM_GrandDiscountAmt,
                    b.SDD_ID,b.SDD_CommodityID,b.SDD_DescID,b.SDD_Quantity,b.SDD_Discount,b.SDD_DiscountAmount,b.SDD_Amount,b.SDD_SGST,b.SDD_SGSTAmount,b.SDD_CGST,b.SDD_CGSTAmount,b.SDD_IGST,b.SDD_IGSTAmount,b.SDD_TotalAmount,
                    c.SPO_OrderCode,c.SPO_ZoneID,c.SPO_RegionID,c.SPO_AreaID,c.SPO_BranchID,g.INV_Description As Commodity,h.INV_Code As Item,i.BM_Name As party
                    From Sales_Dispatch_Master a
                    Left Join Sales_Dispatch_Details b On b.SDD_MasterID=a.SDM_ID 
                    Left Join Sales_PRoForma_Order c on c.SPO_ID=a.SDM_OrderID And c.SPO_YearID=a.SDM_YearID
                    Join Sales_PRoForma_Order_Details d On d.SPOD_SOID = c.SPO_ID and d.spod_commodityid = b.sdd_CommodityID and d.spod_itemid =b.sdd_DescID 
                    Left Join Sales_Allocate_Master e On e.SAM_OrderNo=a.SDM_OrderID And e.SAM_ID=a.SDM_AllocateID And e.SAM_YearID=a.SDM_YearID
                    Left Join Sales_Allocate_Details f On f.SAD_MasterID=e.SAM_ID and f.sad_commodity = b.sdd_CommodityID and f.sad_Descid =b.sdd_DescID and f.sad_YearID=a.SDM_YearID
                    Left Join Inventory_Master g On g.INV_ID=b.SDD_CommodityID 
                    Left Join Inventory_Master h On h.INV_ID=b.SDD_DescID And h.INV_ID=d.spod_itemid
                    Left Join Sales_Buyers_Masters i On i.BM_ID=SDM_SupplierID
                    where a.SDM_OrderDate>=" & objGen.FormatDtForRDBMS(dApplicationDate, "Q") & " And a.SDM_YearID=" & iYearID & " And a.SDM_CompID=" & iCompID & " "

            If iZone > 0 Then
                sSql = sSql & " And c.SPO_ZoneID =" & iZone & ""
            End If
            If iRegion > 0 Then
                sSql = sSql & " And c.SPO_RegionID =" & iRegion & ""
            End If
            If iArea > 0 Then
                sSql = sSql & " And c.SPO_AreaID =" & iArea & ""
            End If
            If iBranch > 0 Then
                sSql = sSql & " And c.SPO_BranchID =" & iBranch & ""
            End If

            If iOrderNo > 0 Then
                sSql = sSql & "And SDM_OrderID=" & iOrderNo & " "
            End If

            If iDispatchNo > 0 Then
                sSql = sSql & "And SDM_ID=" & iDispatchNo & " "
            End If

            If iCommodityID > 0 Then
                sSql = sSql & "And b.SDD_CommodityID=" & iCommodityID & " "
            End If

            If iItemID > 0 Then
                sSql = sSql & "And b.SDD_DescID=" & iItemID & " "
            End If

            If iParty > 0 Then
                sSql = sSql & "And SDM_SupplierID=" & iParty & " "
            End If

            If dFrom <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And SDM_DispatchDate Between " & objGen.FormatDtForRDBMS(dFrom, "Q") & " "
            End If

            If dTo <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And " & objGen.FormatDtForRDBMS(dTo, "Q") & ""
            End If

            If dDiscount > 0 Then
                sSql = sSql & "And b.SDD_Discount=" & dDiscount & " "
            End If

            'If dVAT > 0 Then
            '    sSql = sSql & "And b.SDD_VAT=" & dVAT & " "
            'End If

            'If dCST > 0 Then
            '    sSql = sSql & "And b.SDD_CST=" & dCST & " "
            'End If

            'If dExcise > 0 Then
            '    sSql = sSql & "And b.SDD_Excise=" & dExcise & " "
            'End If

            sSql = sSql & " order by b.SDD_ID"

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt


            Dim dview As New DataView(dt)
            If (dVAT <> "") Then
                dview = dt.DefaultView
                dview.RowFilter = "VAT='" & dVAT & "'"
                dt = dview.ToTable
            End If
            If (dExcise <> "") Then
                dview = dt.DefaultView
                dview.RowFilter = "Exise='" & dExcise & "'"
                dt = dview.ToTable
            End If
            If (dCST <> "") Then
                dview = dt.DefaultView
                dview.RowFilter = "CST='" & dCST & "'"
                dt = dview.ToTable
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderIDFromDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SDM_ID From Sales_Dispatch_Master Where SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN "
            sSql = sSql & "From MST_Customer_master j "
            sSql = sSql & " Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT'"
            sSql = sSql & " Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX'"
            sSql = sSql & " Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN'"
            sSql = sSql & " Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN'"
            sSql = sSql & " Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN'"
            sSql = sSql & " Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' Where j.Cust_ID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select j.BM_NAME,j.BM_Address,j.BM_MobileNo,j.BM_EmailID,k.Buyer_Value As BVAT,l.Buyer_Value As BTAX,m.Buyer_Value As BPAN,n.Buyer_Value As BTAN,o.Buyer_Value As BTIN,p.Buyer_Value As BCIN "
            sSql = sSql & "From Sales_Buyers_masters j "
            sSql = sSql & " Left Join Sales_Buyer_Accounting_Template k On j.BM_ID=k.Buyer_ID And k.Buyer_Desc='VAT'"
            sSql = sSql & " Left Join Sales_Buyer_Accounting_Template l On j.BM_ID=l.Buyer_ID And l.Buyer_Desc='TAX'"
            sSql = sSql & " Left Join Sales_Buyer_Accounting_Template m On j.BM_ID=m.Buyer_ID And m.Buyer_Desc='PAN'"
            sSql = sSql & " Left Join Sales_Buyer_Accounting_Template n On j.BM_ID=n.Buyer_ID And n.Buyer_Desc='TAN'"
            sSql = sSql & " Left Join Sales_Buyer_Accounting_Template o On j.BM_ID=o.Buyer_ID And o.Buyer_Desc='TIN'"
            sSql = sSql & " Left Join Sales_Buyer_Accounting_Template p On j.BM_ID=p.Buyer_ID And p.Buyer_Desc='CIN' Where j.BM_ID=" & iPartyID & " And j.BM_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
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
    Public Function GetDispatchedOrderTotalQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iInvoiceID As Integer) As Double
        Dim sSql As String = ""
        Dim dQty As Double
        Dim bCheck As Boolean
        Try
            sSql = "" : sSql = "Select * From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sSql = "" : sSql = "Select SUM(SAD_OrderQnt) From Sales_Allocate_Details Where SAD_MasterID in (Select Top 1 SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & ") "
                dQty = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Else
                sSql = "" : sSql = "Select SUM(SPOD_Quantity) from Sales_ProForma_Order_Details where SPOD_SOID in (Select SPO_ID from Sales_ProForma_Order Where SPO_ID=" & iOrderID & " And SPO_YearID =" & iYearID & " And SPO_CompID =" & iCompID & ") "
                dQty = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return dQty
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDispatchRefNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String = ""
        Dim sDRefNo As String = ""
        Dim sStr As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Distinct(SDM_ID) From Sales_Dispatch_Master Where SDM_OrderID=" & iOrderID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "" : sSql = "Select Distinct(SDM_DispatchRefNo) From Sales_Dispatch_Master Where SDM_ID=" & dt.Rows(i)("SDM_ID") & " And SDM_OrderID=" & iOrderID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
                    sDRefNo = objDBL.SQLGetDescription(sNameSpace, sSql)
                    If sDRefNo <> "" Then
                        sStr = sStr & "," & sDRefNo
                    Else
                        sStr = sStr & ""
                    End If
                Next
            Else
                sStr = ""
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDetailsRefNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iInvoiceID As Integer) As String
        Dim sSql As String = ""
        Dim sDRefNo As String = ""
        Dim sStr As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select Distinct(SDM_DispatchRefNo) From Sales_Dispatch_Master Where SDM_ID=" & iInvoiceID & " And SDM_OrderID=" & iOrderID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
            sDRefNo = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sDRefNo
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDesc(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer) As Double
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & iID & " And MAS_CompID=" & iCompID & " "
            GetDesc = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetDesc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Double
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc='" & sDesc & "' And MAS_CompID=" & iCompID & " "
            GetID = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getOrderQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrder As Integer, ByVal sRate As String, ByVal iCommodity As Integer, ByVal iItem As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select SPOD_Quantity From Sales_PRoForma_Order_Details Where SPOD_SOID=" & iOrder & " And SPOD_MRPRate=" & sRate & " And SPOD_CommodityID =" & iCommodity & " And SPOD_ItemID =" & iItem & " And SPOD_YearID =" & iYearID & " And SPOD_compID=" & iCompID & " "
            getOrderQty = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return getOrderQty
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getPlacedQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrder As Integer, ByVal sRate As String, ByVal iCommodity As Integer, ByVal iItem As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select SAD_PlacedQnt From Sales_Allocate_Details Where SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrder & " And SAM_YearID =" & iYearID & ") And SAD_Commodity =" & iCommodity & " And SAD_DescID =" & iItem & " And SAD_MRP =" & sRate & " And SAD_CompID =" & iCompID & " "
            getPlacedQty = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return getPlacedQty
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getPendingQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrder As Integer, ByVal sRate As String, ByVal iCommodity As Integer, ByVal iItem As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select SAD_PendingQty From Sales_Allocate_Details Where SAD_MasterID in(Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrder & " And SAM_YearID =" & iYearID & ") And SAD_Commodity =" & iCommodity & " And SAD_DescID =" & iItem & " And SAD_MRP =" & sRate & " And SAD_CompID =" & iCompID & " "
            getPendingQty = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return getPendingQty
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
