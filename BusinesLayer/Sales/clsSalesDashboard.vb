Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsSalesDashboard
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
            dt.Columns.Add("SOID", GetType(String))
            dt.Columns.Add("DISPID", GetType(String))
            dt.Columns.Add("RETID", GetType(String))

            dt.Columns.Add("Supplier", GetType(String))
            dt.Columns.Add("Address", GetType(String))

            dt.Columns.Add("ALCID", GetType(String))
            dt.Columns.Add("INID", GetType(String))
            dt.Columns.Add("PO", GetType(String))
            dt.Columns.Add("GinNumber", GetType(String))
            dt.Columns.Add("Rejection", GetType(String))
            dt.Columns.Add("InvoiceNo", GetType(String))
            dt.Columns.Add("DispatchNo", GetType(String))
            dt.Columns.Add("Advance", GetType(String))
            dt.Columns.Add("SentToAcc", GetType(String))

            'sSql = "Select PIM_ID,POM_ID,POM_OrderNo,POM_Supplier,PGM_ID,PGM_GIN_NUMBER,PRM_ID,PRM_DocumentRefNo,PRM_Supplier,PV_CreatedOn,PV_AppOn from purchase_registry_master "
            'sSql = sSql & " Left Join Purchase_Order_Master On PRM_OrderNo=POM_ID And POM_OralStatus='P' And POM_YearID=" & iYearID & ""
            'sSql = sSql & " Left Join Purchase_GIN_Master On PRM_OrderNo=PGM_OrderID and PRM_DocumentRefNo=PGM_DocumentRefNo And PGM_YearID=" & iYearID & ""
            'sSql = sSql & " Left Join Purchase_Invoice_Master On PIM_PRegesterID=PRM_ID And PIM_OrderID=PRM_OrderNo And PIM_YearID=" & iYearID & ""
            'sSql = sSql & " Left Join Purchase_Verification On PV_OrderNo=POM_ID And PV_YearID=" & iYearID & " And PV_CompID=" & iCompID & ""
            'sSql = sSql & " Where PRM_YearID=" & iYearID & " And PRM_CompID =" & iCompID & " Order By PRM_ID ASC"

            sSql = "Select Sales_Return_ID,Sales_Return_ReturnNo,SDM_ID,SDM_DispatchRefNo,SDM_DispatchDate,DM_ID,DM_DispatchRefNo,SPO_ID,SPO_OrderCode,SPO_PartyName,SAM_ID,SAM_Code
                    from Sales_ProForma_Order   
                    Left Join Sales_Allocate_Master On SPO_ID=SAM_OrderNo and SAM_YearID=" & iYearID & "
					Left Join Dispatch_Master On DM_OrderID=SPO_ID And DM_YearID=" & iYearID & " 
					Left Join Sales_Dispatch_Master On SDM_OrderID=SPO_ID And SDM_YearID=" & iYearID & " 
					Left Join Sales_Return_Masters On Sales_Return_OrderNo=SPO_ID And Sales_Return_InvoiceNo=SDM_ID And Sales_Return_DispatchNo=DM_ID And Sales_Return_Year=" & iYearID & " 
					Where SPO_YearID=" & iYearID & " Order By SPO_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    sl = sl + 1
                    dr("SLNo") = sl
                    'If IsDBNull(ds.Tables(0).Rows(i)("PRM_ID").ToString()) = False Then
                    '    dr("Id") = ds.Tables(0).Rows(i)("PRM_ID").ToString()
                    'Else
                    '    dr("Id") = 0
                    'End If 

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_ID").ToString()) = False Then
                        dr("SOID") = ds.Tables(0).Rows(i)("SPO_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("DM_ID").ToString()) = False Then
                        dr("DISPID") = ds.Tables(0).Rows(i)("DM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("SAM_ID").ToString()) = False Then
                        dr("ALCID") = ds.Tables(0).Rows(i)("SAM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_ID").ToString()) = False Then
                        dr("INID") = ds.Tables(0).Rows(i)("SDM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Sales_Return_ID").ToString()) = False Then
                        dr("RETID") = ds.Tables(0).Rows(i)("Sales_Return_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_PartyName").ToString()) = False Then
                        dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("SPO_PartyName") & " And CSM_CompID=" & iCompID & " ")
                        dr("Address") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Address From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("SPO_PartyName") & " And CSM_CompID=" & iCompID & " ")
                    End If



                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_OrderCode").ToString()) = False Then
                        dr("PO") = ds.Tables(0).Rows(i)("SPO_OrderCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DM_DispatchRefNo").ToString()) = False Then
                        dr("DispatchNo") = ds.Tables(0).Rows(i)("DM_DispatchRefNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SAM_Code").ToString()) = False Then
                        dr("GinNumber") = ds.Tables(0).Rows(i)("SAM_Code").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_DispatchRefNo").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("SDM_DispatchRefNo").ToString()
                    Else
                        dr("InvoiceNo") = ""
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Sales_Return_ReturnNo").ToString()) = False Then
                        dr("Rejection") = ds.Tables(0).Rows(i)("Sales_Return_ReturnNo").ToString()
                    Else
                        dr("Rejection") = ""
                    End If

                    sSql = "" : sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & ds.Tables(0).Rows(i)("SPO_PartyName") & " And CSM_CompID=" & iCompID & ""
                    sSupplier = objDBL.SQLGetDescription(sNameSpace, sSql)

                    sSql = "" : sSql = "Select ATD_Debit from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And (Acc_PM_OrderNo=" & ds.Tables(0).Rows(i)("SPO_ID") & ") And (Acc_PM_PaymentType=1 Or Acc_PM_PaymentType=4) And ATD_DborCr=1 And "
                    sSql = sSql & " ATD_SUBGL In(Select gl_id From chart_of_Accounts Where gl_Desc Like '%" & sSupplier & " Advance%' And gl_CompID=" & iCompID & ") And ATD_CompID=" & iCompID & ""
                    dAdvance = objDBL.SQLExecuteScalar(sNameSpace, sSql)

                    dr("Advance") = dAdvance

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_DispatchDate").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("SDM_DispatchDate").ToString() = "" Then
                            dr("SentToAcc") = ""
                        Else
                            dr("SentToAcc") = objGen.FormatDtForRDBMS(ds.Tables(0).Rows(i)("SDM_DispatchDate").ToString(), "D")
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
            dt.Columns.Add("SOID", GetType(String))
            dt.Columns.Add("DISPID", GetType(String))
            dt.Columns.Add("RETID", GetType(String))
            dt.Columns.Add("RMID", GetType(String))

            dt.Columns.Add("Supplier", GetType(String))
            dt.Columns.Add("Address", GetType(String))

            dt.Columns.Add("ALCID", GetType(String))
            dt.Columns.Add("INID", GetType(String))
            dt.Columns.Add("PO", GetType(String))
            dt.Columns.Add("GinNumber", GetType(String))
            dt.Columns.Add("Rejection", GetType(String))
            dt.Columns.Add("InvoiceNo", GetType(String))
            dt.Columns.Add("DispatchNo", GetType(String))
            dt.Columns.Add("Advance", GetType(String))
            dt.Columns.Add("Status", GetType(String))

            'sSql = "Select PIM_ID,POM_ID,POM_OrderNo,POM_Supplier,PGM_ID,PGM_GIN_NUMBER,PRM_ID,PRM_DocumentRefNo,PRM_Supplier,PV_CreatedOn,PV_AppOn from purchase_registry_master "
            'sSql = sSql & " Left Join Purchase_Order_Master On PRM_OrderNo=POM_ID And POM_OralStatus='P' And POM_YearID=" & iYearID & ""
            'sSql = sSql & " Left Join Purchase_GIN_Master On PRM_OrderNo=PGM_OrderID and PRM_DocumentRefNo=PGM_DocumentRefNo And PGM_YearID=" & iYearID & ""
            'sSql = sSql & " Left Join Purchase_Invoice_Master On PIM_PRegesterID=PRM_ID And PIM_OrderID=PRM_OrderNo And PIM_YearID=" & iYearID & ""
            'sSql = sSql & " Left Join Purchase_Verification On PV_OrderNo=POM_ID And PV_YearID=" & iYearID & " And PV_CompID=" & iCompID & ""
            'sSql = sSql & " Where PRM_YearID=" & iYearID & " And PRM_CompID =" & iCompID & " Order By PRM_ID ASC"



            sSql = "Select Sales_Return_ID,Sales_Return_ReturnNo,SDM_ID,SDM_DispatchRefNo,DM_ID,DM_DispatchRefNo,SPO_ID,SPO_OrderCode,SPO_PartyName,SAM_ID,SAM_Code,Acc_RM_ID,Acc_RM_BalanceAmount
                    from Sales_ProForma_Order   
                    Left Join Sales_Allocate_Master On SPO_ID=SAM_OrderNo and SAM_YearID=" & iYearID & "
					Left Join Dispatch_Master On DM_OrderID=SPO_ID And DM_YearID=" & iYearID & " 
					Left Join Sales_Dispatch_Master On SDM_OrderID=SPO_ID And SDM_YearID=" & iYearID & " 
					Left Join Sales_Return_Masters On Sales_Return_OrderNo=SPO_ID And Sales_Return_InvoiceNo=SDM_ID And Sales_Return_DispatchNo=DM_ID And Sales_Return_Year=" & iYearID & " 
					Left Join acc_receipt_master On RIGHT(Acc_RM_BillNo, LEN(Acc_RM_BillNo) - 3)=SDM_Code And Acc_RM_YearID=4
                    Where SPO_YearID=" & iYearID & " Order By SPO_ID ASC"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    sl = sl + 1
                    dr("SLNo") = sl
                    'If IsDBNull(ds.Tables(0).Rows(i)("PRM_ID").ToString()) = False Then
                    '    dr("Id") = ds.Tables(0).Rows(i)("PRM_ID").ToString()
                    'Else
                    '    dr("Id") = 0
                    'End If
                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_ID").ToString()) = False Then
                        dr("SOID") = ds.Tables(0).Rows(i)("SPO_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("DM_ID").ToString()) = False Then
                        dr("DISPID") = ds.Tables(0).Rows(i)("DM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("SAM_ID").ToString()) = False Then
                        dr("ALCID") = ds.Tables(0).Rows(i)("SAM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_ID").ToString()) = False Then
                        dr("INID") = ds.Tables(0).Rows(i)("SDM_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Sales_Return_ID").ToString()) = False Then
                        dr("RETID") = ds.Tables(0).Rows(i)("Sales_Return_ID").ToString()
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("Acc_RM_ID").ToString()) = False Then
                        dr("RMID") = ds.Tables(0).Rows(i)("Acc_RM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_PartyName").ToString()) = False Then
                        dr("Supplier") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("SPO_PartyName") & " And CSM_CompID=" & iCompID & " ")
                        dr("Address") = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Address From CustomerSupplierMaster Where CSM_ID=" & ds.Tables(0).Rows(i)("SPO_PartyName") & " And CSM_CompID=" & iCompID & " ")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SPO_OrderCode").ToString()) = False Then
                        dr("PO") = ds.Tables(0).Rows(i)("SPO_OrderCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("DM_DispatchRefNo").ToString()) = False Then
                        dr("DispatchNo") = ds.Tables(0).Rows(i)("DM_DispatchRefNo").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SAM_Code").ToString()) = False Then
                        dr("GinNumber") = ds.Tables(0).Rows(i)("SAM_Code").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SDM_DispatchRefNo").ToString()) = False Then
                        dr("InvoiceNo") = ds.Tables(0).Rows(i)("SDM_DispatchRefNo").ToString()
                    Else
                        dr("InvoiceNo") = ""
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("Sales_Return_ReturnNo").ToString()) = False Then
                        dr("Rejection") = ds.Tables(0).Rows(i)("Sales_Return_ReturnNo").ToString()
                    Else
                        dr("Rejection") = ""
                    End If

                    sSql = "" : sSql = "Select CSM_Name from CustomerSupplierMaster where CSM_ID=" & ds.Tables(0).Rows(i)("SPO_PartyName") & " And CSM_CompID=" & iCompID & ""
                    sSupplier = objDBL.SQLGetDescription(sNameSpace, sSql)

                    sSql = "" : sSql = "Select ATD_Debit from Acc_Transactions_Details,Acc_Payment_Master where ATD_BillID=Acc_PM_ID And (Acc_PM_OrderNo=" & ds.Tables(0).Rows(i)("SPO_ID") & ") And (Acc_PM_PaymentType=1 Or Acc_PM_PaymentType=4) And ATD_DborCr=1 And "
                    sSql = sSql & " ATD_SUBGL In(Select gl_id From chart_of_Accounts Where gl_Desc Like '%" & sSupplier & " Advance%' And gl_CompID=" & iCompID & ") And ATD_CompID=" & iCompID & ""
                    dAdvance = objDBL.SQLExecuteScalar(sNameSpace, sSql)

                    dr("Advance") = dAdvance

                    If Not IsDBNull(ds.Tables(0).Rows(i)("Acc_RM_BalanceAmount")) Then
                        If ds.Tables(0).Rows(i)("Acc_RM_BalanceAmount") = 0 Then
                            dr("Status") = "Received"
                        Else
                            dr("Status") = "Partially Received"
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
