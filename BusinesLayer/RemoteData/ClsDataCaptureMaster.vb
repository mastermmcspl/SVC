Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsDataCaptureMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function LoadMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0

        Dim dtATD As New DataTable
        Dim iFilesIndexed, iEnteredNo As Integer
        Try
            dc = New DataColumn("BatchID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CompanyID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CustomerID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TrTypeID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransactionNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Company", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Customer", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TrType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BatchNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("VoucherNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransactionDate", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from Data_Capture where DC_YearID=" & iYearID & " And DC_CompID =" & iCompID & " Order By DC_ID ASC "

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_BatchNo").ToString()) = False Then
                        dr("BatchID") = ds.Tables(0).Rows(i)("DC_BatchNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_Company").ToString()) = False Then
                        dr("CompanyID") = ds.Tables(0).Rows(i)("DC_Company").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_Customer").ToString()) = False Then
                        dr("CustomerID") = ds.Tables(0).Rows(i)("DC_Customer").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_TrType").ToString()) = False Then
                        dr("TrTypeID") = ds.Tables(0).Rows(i)("DC_TrType").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_TransactionNo").ToString()) = False Then
                        dr("TransactionNo") = ds.Tables(0).Rows(i)("DC_TransactionNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_Company").ToString()) = False Then
                        dr("Company") = objDBL.SQLGetDescription(sNameSpace, "Select CBN_NAME from EDT_Cabinet where CBN_Parent=-1 And CBN_DelStatus='A' And CBN_NODE=" & ds.Tables(0).Rows(i)("DC_Company").ToString() & "  ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_TrType").ToString()) = False Then
                        dr("TrType") = objDBL.SQLGetDescription(sNameSpace, "Select CBN_NAME from EDT_Cabinet where CBN_Parent<>-1 And CBN_DelStatus='A' And CBN_NODE=" & ds.Tables(0).Rows(i)("DC_TrType").ToString() & "  ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_Customer").ToString()) = False Then
                        If dr("TrType") = "Purchase" Or dr("TrType") = "Payment" Or dr("TrType") = "Cash purchase" Or dr("TrType") = "Purchase Return" Or dr("TrType") = "GIN" Then
                            dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_NAME from CustomerSupplierMaster where CSM_ID=" & ds.Tables(0).Rows(i)("DC_Customer").ToString() & "  ")
                        ElseIf dr("TrType") = "Sales" Or dr("TrType") = "Receipt" Or dr("TrType") = "Sales Dispatch" Or dr("TrType") = "Sales Invoice" Or dr("TrType") = "Cash Sales" Or dr("TrType") = "Sales Return" Then
                            dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_NAME from Sales_Buyers_Masters where BM_ID=" & ds.Tables(0).Rows(i)("DC_Customer").ToString() & "  ")
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_BatchNo").ToString()) = False Then
                        dr("BatchNo") = objDBL.SQLGetDescription(sNameSpace, "Select FOL_NAME From EDT_Folder Where FOL_FOLID=" & ds.Tables(0).Rows(i)("DC_BatchNo").ToString() & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_VoucherNo").ToString()) = False Then
                        dr("VoucherNo") = ds.Tables(0).Rows(i)("DC_VoucherNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DC_TrDate").ToString()) = False Then
                        dr("TransactionDate") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("DC_TrDate").ToString(), "D")
                    End If

                    If (dr("TrType") = "Purchase") Or (dr("TrType") = "Cash Purchase") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(POM_ID) From PURCHASE_ORDER_MASTER Where POM_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND POM_COMPID=" & iCompID & " AND POM_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "Purchase Return") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(GRM_ID) From Goods_Return_Master Where GRM_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND GRM_COMPID=" & iCompID & " AND GRM_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "GIN") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGM_ID) From Purchase_GIN_Master Where PGM_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND PGM_COMPID=" & iCompID & " AND PGM_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "Payment") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(Acc_PM_ID) From Acc_Payment_Master Where Acc_PM_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND Acc_PM_COMPID=" & iCompID & " AND Acc_PM_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "Receipt") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(Acc_RM_ID) From Acc_Receipt_Master Where Acc_RM_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND Acc_RM_COMPID=" & iCompID & " AND Acc_RM_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "Patty Cash") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(Acc_PCM_ID) From Acc_PettyCash_Master Where Acc_PCM_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND Acc_PCM_COMPID=" & iCompID & " AND Acc_PCM_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "Journal Entry") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(Acc_JE_ID) From Acc_JE_Master Where Acc_JE_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND Acc_JE_COMPID=" & iCompID & " AND Acc_JE_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "Sales") Or (dr("TrType") = "Cash Sales") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(SPO_ID) From Sales_Proforma_Order Where SPO_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND SPO_COMPID=" & iCompID & " AND SPO_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "Sales Dispatch") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(DM_ID) From Dispatch_Master Where DM_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND DM_COMPID=" & iCompID & " AND DM_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "Sales Invoice") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(SDM_ID) From Sales_Dispatch_Master Where SDM_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND SDM_COMPID=" & iCompID & " AND SDM_YEARID=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
                        End If
                    ElseIf (dr("TrType") = "Sales Return") Then
                        iFilesIndexed = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(PGE_BASENAME) From EDT_PAGE Where PGE_CABINET=" & ds.Tables(0).Rows(i)("DC_Company") & " And PGE_SubCABINET=" & ds.Tables(0).Rows(i)("DC_TrType") & " And PGE_FOLDER=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " ")
                        iEnteredNo = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Count(Sales_Return_ID) From Sales_Return_Masters Where Sales_Return_BATCHNO=" & ds.Tables(0).Rows(i)("DC_BatchNo") & " AND Sales_Return_COMPID=" & iCompID & " AND Sales_Return_YEAR=" & iYearID & " ")
                        If iFilesIndexed <> iEnteredNo Then
                            dr("Status") = "IN - PROGRESS"
                        Else
                            dr("Status") = "COMPLETED"
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
    Public Function BindPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("OrderNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Supplier")

            sSql = "Select * from Purchase_Order_Master Where POM_BatchNo=" & iBatchNo & " And POM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("OrderNo") = dt.Rows(i)("POM_OrderNo")

                If dt.Rows(i)("POM_DcNo").ToString() = "" Then
                    dr("VoucherNo") = 0
                Else
                    dr("VoucherNo") = dt.Rows(i)("POM_DcNo")
                End If

                dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & dt.Rows(i)("POM_Supplier") & " And CSM_compID=" & iCompID & " ")

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSalesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("OrderNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Customer")

            sSql = "Select * from Sales_Proforma_Order Where SPO_BatchNo=" & iBatchNo & " And SPO_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("OrderNo") = dt.Rows(i)("SPO_OrderCode")

                If dt.Rows(i)("SPO_BuyerOrderNo").ToString() = "" Then
                    dr("VoucherNo") = 0
                Else
                    dr("VoucherNo") = dt.Rows(i)("SPO_BuyerOrderNo")
                End If

                dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("SPO_PartyName") & " And BM_compID=" & iCompID & " ")

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPaymentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try

            dtTab.Columns.Add("PaymentNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Supplier")

            sSql = "Select * from Acc_Payment_Master Where Acc_PM_BatchNo=" & iBatchNo & " And Acc_PM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PaymentNo") = dt.Rows(i)("Acc_PM_TransactionNo")

                'If dt.Rows(i)("SPO_BuyerOrderNo").ToString() = "" Then
                dr("VoucherNo") = 0
                'Else
                '    dr("VoucherNo") = dt.Rows(i)("SPO_BuyerOrderNo")
                'End If

                If dt.Rows(i)("Acc_PCM_Location") = 1 Then  'Customer
                    dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("ACc_PCM_Party") & " And BM_compID=" & iCompID & " ")
                ElseIf dt.Rows(i)("Acc_PCM_Location") = 2 Then  'Supplier
                    dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & dt.Rows(i)("ACc_PCM_Party") & " And CSM_compID=" & iCompID & " ")
                ElseIf dt.Rows(i)("Acc_PCM_Location") = 3 Then  'GL

                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindReceiptDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try

            dtTab.Columns.Add("ReceiptNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Customer")

            sSql = "Select * from Acc_Receipt_Master Where Acc_RM_BatchNo=" & iBatchNo & " And Acc_RM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PaymentNo") = dt.Rows(i)("Acc_RM_TransactionNo")

                'If dt.Rows(i)("SPO_BuyerOrderNo").ToString() = "" Then
                dr("VoucherNo") = 0
                'Else
                '    dr("VoucherNo") = dt.Rows(i)("SPO_BuyerOrderNo")
                'End If

                If dt.Rows(i)("Acc_PCM_Location") = 1 Then  'Customer
                    dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("ACc_PCM_Party") & " And BM_compID=" & iCompID & " ")
                ElseIf dt.Rows(i)("Acc_PCM_Location") = 2 Then  'Supplier
                    dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & dt.Rows(i)("ACc_PCM_Party") & " And CSM_compID=" & iCompID & " ")
                ElseIf dt.Rows(i)("Acc_PCM_Location") = 3 Then  'GL

                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPettyCashDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try

            dtTab.Columns.Add("PettyCashNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Customer")

            sSql = "Select * from Acc_PettyCash_Master Where Acc_PCM_BatchNo=" & iBatchNo & " And Acc_PCM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PaymentNo") = dt.Rows(i)("Acc_PCM_TransactionNo")

                'If dt.Rows(i)("SPO_BuyerOrderNo").ToString() = "" Then
                dr("VoucherNo") = 0
                'Else
                '    dr("VoucherNo") = dt.Rows(i)("SPO_BuyerOrderNo")
                'End If

                If dt.Rows(i)("Acc_PCM_Location") = 1 Then  'Customer
                    dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("ACc_PCM_Party") & " And BM_compID=" & iCompID & " ")
                ElseIf dt.Rows(i)("Acc_PCM_Location") = 2 Then  'Supplier
                    dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & dt.Rows(i)("ACc_PCM_Party") & " And CSM_compID=" & iCompID & " ")
                ElseIf dt.Rows(i)("Acc_PCM_Location") = 3 Then  'GL

                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindJEDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try

            dtTab.Columns.Add("PettyCashNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Customer")

            sSql = "Select * from Acc_JE_Master Where Acc_JE_BatchNo=" & iBatchNo & " And Acc_JE_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("PaymentNo") = dt.Rows(i)("Acc_JE_TransactionNo")

                'If dt.Rows(i)("SPO_BuyerOrderNo").ToString() = "" Then
                dr("VoucherNo") = 0
                'Else
                '    dr("VoucherNo") = dt.Rows(i)("SPO_BuyerOrderNo")
                'End If

                If dt.Rows(i)("Acc_PCM_Location") = 1 Then  'Customer
                    dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("ACc_JE_Party") & " And BM_compID=" & iCompID & " ")
                ElseIf dt.Rows(i)("Acc_PCM_Location") = 2 Then  'Supplier
                    dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & dt.Rows(i)("ACc_JE_Party") & " And CSM_compID=" & iCompID & " ")
                ElseIf dt.Rows(i)("Acc_PCM_Location") = 3 Then  'GL

                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindGINDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("GINNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Supplier")

            sSql = "Select * from Purchase_GIN_Master Where PGM_BatchNo=" & iBatchNo & " And PGM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("GINNo") = dt.Rows(i)("PGM_DocumentRefNo")

                If dt.Rows(i)("PGM_DcNo").ToString() = "" Then
                    dr("VoucherNo") = 0
                Else
                    dr("VoucherNo") = dt.Rows(i)("PGM_DcNo")
                End If

                dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & dt.Rows(i)("PGM_Supplier") & " And CSM_compID=" & iCompID & " ")

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSalesDispatchDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("DispatchNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Customer")

            sSql = "Select * from Dispatch_Master Where DM_BatchNo=" & iBatchNo & " And DM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("DispatchNo") = dt.Rows(i)("DM_Code")

                If dt.Rows(i)("DM_DispatchRefNo").ToString() = "" Then
                    dr("VoucherNo") = 0
                Else
                    dr("VoucherNo") = dt.Rows(i)("DM_DispatchRefNo")
                End If

                dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("DM_SupplierID") & " And BM_compID=" & iCompID & " ")

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSalesInvoiceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("InvoiceNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Customer")

            sSql = "Select * from Sales_Dispatch_Master Where SDM_BatchNo=" & iBatchNo & " And SDM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("InvoiceNo") = dt.Rows(i)("SDM_Code")

                If dt.Rows(i)("SDM_DispatchRefNo").ToString() = "" Then
                    dr("VoucherNo") = 0
                Else
                    dr("VoucherNo") = dt.Rows(i)("SDM_DispatchRefNo")
                End If

                dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("SDM_SupplierID") & " And BM_compID=" & iCompID & " ")

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindSalesReturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SalesReturnNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Customer")

            sSql = "Select * from Sales_Return_Masters Where SRM_BatchNo=" & iBatchNo & " And SRM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("InvoiceNo") = dt.Rows(i)("Sales_Return_ReturnNo")

                If dt.Rows(i)("Sales_Return_GoodsReturnNo").ToString() = "" Then
                    dr("VoucherNo") = 0
                Else
                    dr("VoucherNo") = dt.Rows(i)("Sales_Return_GoodsReturnNo")
                End If

                dr("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("Sales_Return_Customer") & " And BM_compID=" & iCompID & " ")

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPurchaseReturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBatchNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("PurchaseReturnNo")
            dtTab.Columns.Add("VoucherNo")
            dtTab.Columns.Add("Supplier")

            sSql = "Select * from Goods_Return_Master Where GRM_BatchNo=" & iBatchNo & " And GRM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("GINNo") = dt.Rows(i)("GRM_ReturnNo")

                If dt.Rows(i)("GRM_ReturnRefNo").ToString() = "" Then
                    dr("VoucherNo") = 0
                Else
                    dr("VoucherNo") = dt.Rows(i)("GRM_ReturnRefNo")
                End If

                dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & dt.Rows(i)("GRM_Supplier") & " And CSM_compID=" & iCompID & " ")

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
