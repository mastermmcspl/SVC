Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsPurchaseDashBoard
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadRegistryOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim sl As Integer = 0
        Dim i As Integer = 0
        Dim sSupplier As String = "" : Dim dAdvance As Double
        Try
            dt.Columns.Add("SLNo", GetType(String))
            dt.Columns.Add("Id", GetType(String))
            dt.Columns.Add("POID", GetType(String))

            dt.Columns.Add("Supplier", GetType(String))
            dt.Columns.Add("Address", GetType(String))

            dt.Columns.Add("GNID", GetType(String))
            dt.Columns.Add("INID", GetType(String))
            dt.Columns.Add("PO", GetType(String))
            dt.Columns.Add("GinNumber", GetType(String))
            dt.Columns.Add("Rejection", GetType(String))
            dt.Columns.Add("InvoiceNo", GetType(String))
            dt.Columns.Add("Advance", GetType(String))
            dt.Columns.Add("SentToAcc", GetType(String))

            'sSql = "Select PIM_ID,POM_ID,POM_OrderNo,POM_Supplier,PGM_ID,PGM_GIN_NUMBER,PRM_ID,PRM_DocumentRefNo,PRM_Supplier,PV_CreatedOn,PV_AppOn from purchase_registry_master "
            'sSql = sSql & " Left Join Purchase_Order_Master On PRM_OrderNo=POM_ID And POM_OralStatus='P' And POM_YearID=" & iYearID & ""
            'sSql = sSql & " Left Join Purchase_GIN_Master On PRM_OrderNo=PGM_OrderID and PRM_DocumentRefNo=PGM_DocumentRefNo And PGM_YearID=" & iYearID & ""
            'sSql = sSql & " Left Join Purchase_Invoice_Master On PIM_PRegesterID=PRM_ID And PIM_OrderID=PRM_OrderNo And PIM_YearID=" & iYearID & ""
            'sSql = sSql & " Left Join Purchase_Verification On PV_OrderNo=POM_ID And PV_YearID=" & iYearID & " And PV_CompID=" & iCompID & ""
            'sSql = sSql & " Where PRM_YearID=" & iYearID & " And PRM_CompID =" & iCompID & " Order By PRM_ID ASC"

            sSql = "Select PIM_ID,POM_ID,POM_OrderNo,POM_Supplier,PGM_ID,PGM_GIN_NUMBER,PRM_ID,PRM_DocumentRefNo,PRM_Supplier,PV_CreatedOn,PV_AppOn 
                    from Purchase_Order_Master   
                    Left Join Purchase_GIN_Master On POM_ID=PGM_OrderID and PGM_YearID=" & iYearID & " 
                    Left join purchase_registry_master on PRM_OrderNo=POM_ID And PRM_DocumentRefNo=PGM_DocumentRefNo 
                    Left Join Purchase_Invoice_Master On PIM_PRegesterID=PRM_ID And PIM_OrderID=POM_ID And PIM_YearID=" & iYearID & " 
                    Left Join Purchase_Verification On PV_OrderNo=POM_ID And PV_YearID=" & iYearID & " And PV_CompID=" & iCompID & " Where POM_OralStatus='P' And POM_YearID=" & iYearID & " Order By POM_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    sl = sl + 1
                    dr("SLNo") = sl
                    If IsDBNull(ds.Tables(0).Rows(i)("PRM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("PRM_ID").ToString()
                    Else
                        dr("Id") = 0
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("POM_ID").ToString()) = False Then
                        dr("POID") = ds.Tables(0).Rows(i)("POM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("POM_Supplier").ToString()) = False Then
                        dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("POM_Supplier") & " And CSM_CompID=" & iCompID & " ")
                        dr("Address") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Address From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("POM_Supplier") & " And CSM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_ID").ToString()) = False Then
                        dr("GNID") = ds.Tables(0).Rows(i)("PGM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("PIM_ID").ToString()) = False Then
                        dr("INID") = ds.Tables(0).Rows(i)("PIM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("POM_OrderNo").ToString()) = False Then
                        dr("PO") = ds.Tables(0).Rows(i)("POM_OrderNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_GIN_NUMBER").ToString()) = False Then
                        dr("GinNumber") = ds.Tables(0).Rows(i)("PGM_GIN_NUMBER").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_ID")) = False Then
                        sSql = "" : sSql = "Select GRM_ReturnNo From Goods_Return_Master where GRM_OrderID=" & ds.Tables(0).Rows(i)("POM_ID") & " And GRM_GINInvID=" & ds.Tables(0).Rows(i)("PGM_ID") & " And GRM_YearID=" & iYearID & " And GRM_CompID=" & iCompID & " "
                        If objDBL.SQLCheckForRecord(sNameSpace, sSql) = True Then
                            dr("Rejection") = objDBL.SQLGetDescription(sNameSpace, sSql)
                        Else
                            dr("Rejection") = ""
                        End If
                    Else
                        dr("Rejection") = ""
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PRM_DocumentRefNo").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("PRM_DocumentRefNo").ToString()
                    Else
                        dr("InvoiceNo") = ""
                    End If

                    sSql = "" : sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & ds.Tables(0).Rows(i)("POM_Supplier") & " And CSM_CompID=" & iCompID & ""
                    sSupplier = objDBL.SQLGetDescription(sNameSpace, sSql)

                    sSql = "" : sSql = "Select ATD_Debit from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And (Acc_PM_OrderNo=" & ds.Tables(0).Rows(i)("POM_ID") & ") And (Acc_PM_PaymentType=1 Or Acc_PM_PaymentType=4) And ATD_DborCr=1 And "
                    sSql = sSql & " ATD_SUBGL IN(select gl_id From chart_of_Accounts Where gl_Desc Like '%" & sSupplier & " Advance%' And gl_CompID=" & iCompID & ") And ATD_CompID=" & iCompID & ""
                    dAdvance = objDBL.SQLExecuteScalar(sNameSpace, sSql)

                    dr("Advance") = dAdvance

                    If IsDBNull(ds.Tables(0).Rows(i)("PV_AppOn").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("PV_AppOn").ToString() = "" Then
                            dr("SentToAcc") = ""
                        Else
                            dr("SentToAcc") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("PV_AppOn").ToString(), "D")
                        End If
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadRegistryOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
    '    Dim dt As New DataTable
    '    Dim ds As New DataSet
    '    Dim dc As New DataColumn
    '    Dim sSql As String = ""
    '    Dim dr As DataRow
    '    Dim sl As Integer = 0
    '    Dim i As Integer = 0
    '    Try
    '        dt.Columns.Add("SLNo", GetType(String))
    '        dt.Columns.Add("Id", GetType(String))
    '        dt.Columns.Add("GinNumber", GetType(String))
    '        dt.Columns.Add("Rejection", GetType(String))
    '        dt.Columns.Add("InvoiceNo", GetType(String))
    '        dt.Columns.Add("Advance", GetType(String))
    '        dt.Columns.Add("SentToAcc", GetType(String))

    '        'Divya Code
    '        'sSql = "Select * from purchase_registry_master where PRM_OrderNo in(select POM_ID from purchase_order_master where POM_OralStatus='P') and PRM_YearID=" & iYearID & " And PRM_CompID =" & iCompID & " "
    '        'sSql = sSql & " Order By PRM_ID ASC"

    '        ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            For i = 0 To ds.Tables(0).Rows.Count - 1
    '                dr = dt.NewRow
    '                sl = sl + 1
    '                dr("SLNo") = sl
    '                If IsDBNull(ds.Tables(0).Rows(i)("PRM_ID").ToString()) = False Then
    '                    dr("Id") = ds.Tables(0).Rows(i)("PRM_ID").ToString()
    '                End If

    '                If IsDBNull(ds.Tables(0).Rows(i)("PRM_DocumentRefNo").ToString()) = False Then
    '                    dr("InvoiceNo") = ds.Tables(0).Rows(i)("PRM_DocumentRefNo").ToString()
    '                End If

    '                If IsDBNull(ds.Tables(0).Rows(i)("PRM_InvoiceDate").ToString()) = False Then
    '                    dr("InvoiceDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("PRM_InvoiceDate").ToString(), "D")
    '                End If

    '                If IsDBNull(ds.Tables(0).Rows(i)("PRM_Supplier").ToString()) = False Then
    '                    dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("PRM_Supplier").ToString() & " And CSM_CompID=" & iCompID & " ")
    '                End If

    '                If IsDBNull(ds.Tables(0).Rows(i)("PRM_RegistryNo").ToString()) = False Then
    '                    dr("GinNumber") = ds.Tables(0).Rows(i)("PRM_RegistryNo").ToString()
    '                End If

    '                If IsDBNull(ds.Tables(0).Rows(i)("PRM_Status").ToString()) = False Then
    '                    If (ds.Tables(0).Rows(i)("PRM_Status").ToString() = "W") Then
    '                        dr("Status") = "Waiting For Approval"
    '                    Else
    '                        dr("Status") = "Approved"
    '                    End If
    '                End If

    '                dt.Rows.Add(dr)
    '            Next
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Sub UpdateJEMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Dispatch_Master Set SDM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " SDM_Status='A',SDM_ApprovedBy= " & iUserID & ",SDM_ApprovedOn=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " SDM_Status='D',SDM_DeletedBy= " & iUserID & ",SDM_DeletedOn=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " SDM_Status='A' "
            End If
            sSql = sSql & " Where SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " And SDM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateJEMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Sales_Dispatch_Master Where SDM_Status='" & sSelectedStatus & "' And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Sales_Dispatch_Master Set SDM_IPAddress='" & sIPAddress & "',"
                    If sStatus = "W" Then
                        sSql = sSql & " SDM_Status='A',SDM_ApprovedBy= " & iUserID & ",SDM_ApprovedOn=GetDate()"
                    ElseIf sStatus = "D" Then
                        sSql = sSql & " SDM_Status='D',SDM_DeletedBy= " & iUserID & ",SDM_DeletedOn=GetDate()"
                    ElseIf sStatus = "A" Then
                        sSql = sSql & " SDM_Status='A' "
                    End If
                    sSql = sSql & " Where SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " And SDM_ID = " & dtPurchase.Rows(j)("SDM_ID") & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadPurchaseORderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtPdetails As New DataTable
        Dim iSlNo As Integer = 0
        Try
            dt.Columns.Add("SLNO")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Goods")
            dt.Columns.Add("Units")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("RateAmount")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("TotalAmount")
            sSql = "Select a.Inv_Code as Commodity,b.Inv_Code as Goods,Mas_Desc,POD_Rate,POD_Quantity,POD_RateAmount,POD_Discount,"
            sSql = sSql & " POD_DiscountAmount,POD_GSTRate,POD_GSTAmount,POD_TotalAmount from Purchase_Order_Details "
            sSql = sSql & " Left Join Inventory_Master a On a.Inv_ID=POD_Commodity And a.Inv_compid=" & iCompID & ""
            sSql = sSql & " Left Join Inventory_Master b On b.Inv_ID=POD_DescriptionID And b.Inv_compid=" & iCompID & ""
            sSql = sSql & " Left Join acc_General_master On Mas_ID=POD_Unit And Mas_compid=" & iCompID & ""
            sSql = sSql & " where POD_MasterID=" & iMasterID & " and POD_CompID=" & iCompID & " and POD_Status='W' order by POD_ID"
            dtPdetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtPdetails.Rows.Count > 0 Then
                For i = 0 To dtPdetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    iSlNo = iSlNo + 1
                    dRow("SLNO") = iSlNo
                    If IsDBNull(dtPdetails.Rows(i)("Commodity")) = False Then
                        dRow("Commodity") = dtPdetails.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("Goods")) = False Then
                        dRow("Goods") = dtPdetails.Rows(i)("Goods")
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("Mas_Desc")) = False Then
                        dRow("Units") = dtPdetails.Rows(i)("Mas_Desc")
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_Rate")) = False Then
                        dRow("Rate") = dtPdetails.Rows(i)("POD_Rate")
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_Quantity")) = False Then
                        dRow("Quantity") = Math.Round(dtPdetails.Rows(i)("POD_Quantity"), 2)
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_RateAmount")) = False Then
                        dRow("RateAmount") = dtPdetails.Rows(i)("POD_RateAmount")
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_Discount")) = False Then
                        dRow("Discount") = dtPdetails.Rows(i)("POD_Discount")
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_DiscountAmount")) = False Then
                        dRow("DiscountAmt") = dtPdetails.Rows(i)("POD_DiscountAmount")
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_GSTRate")) = False Then
                        dRow("GSTRate") = dtPdetails.Rows(i)("POD_GSTRate")
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_GSTAmount")) = False Then
                        dRow("GSTAmount") = dtPdetails.Rows(i)("POD_GSTAmount")
                    End If
                    If IsDBNull(dtPdetails.Rows(i)("POD_TotalAmount")) = False Then
                        dRow("TotalAmount") = dtPdetails.Rows(i)("POD_TotalAmount")
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInwardDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal InwardNo As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Dim iSlNo As Integer = 0
        Try
            dtTab.Columns.Add("SLNO")
            dtTab.Columns.Add("Comodity")
            dtTab.Columns.Add("Units")
            dtTab.Columns.Add("Descriptions")
            dtTab.Columns.Add("Mrp")
            dtTab.Columns.Add("OrderedQty")
            dtTab.Columns.Add("ReceivedQty")
            dtTab.Columns.Add("AccpetedQty")
            dtTab.Columns.Add("RejectedQty")
            dtTab.Columns.Add("ExcessQty")
            sSql = "Select a.Inv_Code as Commodity,b.Inv_Code as Goods,Mas_Desc,PGD_MRP,PGD_OrderQnt,PGD_ReceivedQnt,PGD_Accepted,PGD_RejectedQnt,PGD_Excess From Purchase_GIN_Details"
            sSql = sSql & " Left Join Inventory_Master a On a.INV_ID=PGD_CommodityID And a.Inv_CompID=" & iCompID & ""
            sSql = sSql & " Left Join Inventory_Master b On b.INV_ID=PGD_DescriptionID And b.Inv_CompID=" & iCompID & ""
            sSql = sSql & " Left Join Acc_General_Master On Mas_ID=PGD_UnitID And Mas_CompID=" & iCompID & ""
            sSql = sSql & " Where PGD_CompID=" & iCompID & " and PGD_MasterID=" & InwardNo & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    iSlNo = iSlNo + 1
                    dr("SLNO") = iSlNo
                    If IsDBNull(dt.Rows(i)("Commodity")) = False Then
                        dr("Comodity") = dt.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dt.Rows(i)("Goods")) = False Then
                        dr("Descriptions") = dt.Rows(i)("Goods")
                    End If
                    If IsDBNull(dt.Rows(i)("Mas_Desc")) = False Then
                        dr("Units") = dt.Rows(i)("Mas_Desc")
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_MRP")) = False Then
                        dr("Mrp") = dt.Rows(i)("PGD_MRP")
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_OrderQnt")) = False Then
                        dr("OrderedQty") = dt.Rows(i)("PGD_OrderQnt")
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_ReceivedQnt")) = False Then
                        dr("ReceivedQty") = dt.Rows(i)("PGD_ReceivedQnt")
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_Accepted")) = False Then
                        dr("AccpetedQty") = dt.Rows(i)("PGD_Accepted")
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_RejectedQnt")) = False Then
                        dr("RejectedQty") = dt.Rows(i)("PGD_RejectedQnt")
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_Excess")) = False Then
                        dr("ExcessQty") = dt.Rows(i)("PGD_Excess")
                    End If
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindInvoiceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim iSlNo As Integer = 0
        Try
            dtTab.Columns.Add("SLNO")
            dtTab.Columns.Add("Commodity")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("RateAmount")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("Charges")
            dtTab.Columns.Add("TotalAmount")
            dtTab.Columns.Add("GSTRate")
            dtTab.Columns.Add("GSTAmount")
            dtTab.Columns.Add("FinalTotal")
            If iMasterID > 0 Then
                sSql = "Select a.Inv_Code as Commodity,b.Inv_Code as Goods,Mas_Desc,PID_Quantity,PID_Rate,PID_RateAmount,"
                sSql = sSql & " PID_Discount,PID_DiscountAmount,PID_ChargePerItem,PID_Amount,PID_GSTRate,PID_GSTAmount,PID_FinalTotal"
                sSql = sSql & " From PI_Accepted_Details "
                sSql = sSql & " Left Join Inventory_Master a On a.INV_ID=PID_CommodityID And a.INV_CompID=" & iCompID & " And a.INV_Parent=0"
                sSql = sSql & " Left Join Inventory_Master b On b.INV_ID=PID_DescID And b.INV_Parent=PID_CommodityID And b.INV_CompID=" & iCompID & ""
                sSql = sSql & " Left Join Acc_General_Master On Mas_ID=PID_UnitID And Mas_CompID=" & iCompID & ""
                sSql = sSql & " Where PID_MasterID=" & iMasterID & " And PID_CompID=" & iCompID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    iSlNo = iSlNo + 1
                    dRow("SLNO") = iSlNo
                    If IsDBNull(dt.Rows(i)("Commodity")) = False Then
                        dRow("Commodity") = dt.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dt.Rows(i)("Goods")) = False Then
                        dRow("Goods") = dt.Rows(i)("Goods")
                    End If
                    If IsDBNull(dt.Rows(i)("Mas_Desc")) = False Then
                        dRow("Unit") = dt.Rows(i)("Mas_Desc")
                    End If
                    If IsDBNull(dt.Rows(i)("PID_Quantity")) = False Then
                        dRow("Quantity") = dt.Rows(i)("PID_Quantity")
                    End If
                    If IsDBNull(dt.Rows(i)("PID_Rate")) = False Then
                        dRow("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_Rate")))
                    End If
                    If IsDBNull(dt.Rows(i)("PID_RateAmount")) = False Then
                        dRow("RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_RateAmount")))
                    End If
                    If IsDBNull(dt.Rows(i)("PID_Discount")) = False Then
                        dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_Discount")))
                    End If
                    If IsDBNull(dt.Rows(i)("PID_DiscountAmount")) = False Then
                        dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_DiscountAmount")))
                    End If
                    If IsDBNull(dt.Rows(i)("PID_ChargePerItem")) = False Then
                        dRow("Charges") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_ChargePerItem")))
                    End If
                    If IsDBNull(dt.Rows(i)("PID_Amount")) = False Then
                        dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_Amount")))
                    End If
                    If IsDBNull(dt.Rows(i)("PID_GSTRate")) = False Then
                        dRow("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_GSTRate")))
                    End If
                    If IsDBNull(dt.Rows(i)("PID_GSTAmount")) = False Then
                        dRow("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_GSTAmount")))
                    End If
                    If IsDBNull(dt.Rows(i)("PID_FinalTotal")) = False Then
                        dRow("FinalTotal") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PID_FinalTotal")))
                    End If
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBillRegistryOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim sl As Integer = 0
        Dim i As Integer = 0
        Dim sSupplier As String = "" : Dim dAdvance As Double
        Try
            dt.Columns.Add("SLNo", GetType(String))
            dt.Columns.Add("Id", GetType(String))
            dt.Columns.Add("POID", GetType(String))
            dt.Columns.Add("GNID", GetType(String))
            dt.Columns.Add("INID", GetType(String))
            dt.Columns.Add("PO", GetType(String))

            dt.Columns.Add("Supplier", GetType(String))
            dt.Columns.Add("Address", GetType(String))

            dt.Columns.Add("GinNumber", GetType(String))
            dt.Columns.Add("Rejection", GetType(String))
            dt.Columns.Add("InvoiceNo", GetType(String))
            dt.Columns.Add("Advance", GetType(String))
            dt.Columns.Add("Status", GetType(String))
            'dt.Columns.Add("PaidNot", GetType(String))

            sSql = "Select PIM_ID,POM_ID,POM_OrderNo,PGM_ID,PGM_GIN_NUMBER,PRM_ID,PRM_DocumentRefNo,PRM_Supplier,PRM_InvoiceDate,PRM_Status,GRM_ReturnNo,Acc_PJE_BillNo,Acc_PJE_pendingAmount from purchase_registry_master "
            sSql = sSql & " Left Join Purchase_Order_Master On PRM_OrderNo=POM_ID And POM_OralStatus='P' And POM_YearID=" & iYearID & ""
            sSql = sSql & " Left Join Purchase_GIN_Master On PRM_OrderNo=PGM_OrderID and PRM_DocumentRefNo=PGM_DocumentRefNo And PGM_YearID=" & iYearID & ""
            sSql = sSql & " Left join Goods_Return_Master On GRM_OrderID=POM_ID And GRM_GINInvID=PGM_ID"
            sSql = sSql & " Left Join Purchase_Invoice_Master On PIM_PRegesterID=PRM_ID And PIM_OrderID=PRM_OrderNo And PIM_YearID=" & iYearID & ""
            sSql = sSql & " Left join Purchase_Verification on PV_OrderNo=POM_ID And PV_GINNo=PGM_ID And PV_InvoiceID=PIM_ID"
            sSql = sSql & " Left join ACC_Purchase_JE_Master on Acc_PJE_InvoiceID=PV_ID"
            sSql = sSql & " Where Acc_PJE_YearID=" & iYearID & " And Acc_PJE_CompID =" & iCompID & " Order By Acc_PJE_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    sl = sl + 1
                    dr("SLNo") = sl
                    If IsDBNull(ds.Tables(0).Rows(i)("PRM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("PRM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("POM_ID").ToString()) = False Then
                        dr("POID") = ds.Tables(0).Rows(i)("POM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_ID").ToString()) = False Then
                        dr("GNID") = ds.Tables(0).Rows(i)("PGM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("PIM_ID").ToString()) = False Then
                        dr("INID") = ds.Tables(0).Rows(i)("PIM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("POM_OrderNo").ToString()) = False Then
                        dr("PO") = ds.Tables(0).Rows(i)("POM_OrderNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PRM_Supplier").ToString()) = False Then
                        dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("PRM_Supplier") & " And CSM_CompID=" & iCompID & " ")
                        dr("Address") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Address From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("PRM_Supplier") & " And CSM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PGM_GIN_NUMBER").ToString()) = False Then
                        dr("GinNumber") = ds.Tables(0).Rows(i)("PGM_GIN_NUMBER").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("GRM_ReturnNo").ToString()) = False Then
                        dr("Rejection") = ds.Tables(0).Rows(i)("GRM_ReturnNo").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("PRM_DocumentRefNo").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("PRM_DocumentRefNo").ToString()
                    End If

                    sSql = "" : sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & ds.Tables(0).Rows(i)("PRM_Supplier") & " And CSM_CompID=" & iCompID & ""
                    sSupplier = objDBL.SQLGetDescription(sNameSpace, sSql)

                    sSql = "" : sSql = "Select ATD_Debit from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And (Acc_PM_OrderNo=" & ds.Tables(0).Rows(i)("POM_ID") & ") And (Acc_PM_PaymentType=1 Or Acc_PM_PaymentType=4) And ATD_DborCr=1 And "
                    sSql = sSql & " ATD_SUBGL IN(select gl_id From chart_of_Accounts Where gl_Desc Like '%" & sSupplier & " Advance%' And gl_CompID=" & iCompID & ") And ATD_CompID=" & iCompID & ""
                    dAdvance = objDBL.SQLExecuteScalar(sNameSpace, sSql)

                    dr("Advance") = dAdvance

                    'If IsDBNull(ds.Tables(0).Rows(i)("PRM_Status").ToString()) = False Then
                    '    If (ds.Tables(0).Rows(i)("PRM_Status").ToString() = "W") Then
                    '        dr("Status") = "Waiting For Approval"
                    '    Else
                    '        dr("Status") = "Approved"
                    '    End If
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_PJE_PendingAmount").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("Acc_PJE_PendingAmount") = 0 Then
                            dr("Status") = "Paid"
                        Else
                            dr("Status") = ""
                        End If
                    Else
                        dr("Status") = ""
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
