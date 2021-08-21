Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class ClsVendorDynamicReports
    Dim objDB As New DBHelper
    Dim objGen As New clsFASGeneral
    Dim objClsFasgnrl As New clsFASGeneral

    Public Function LoadSupplierName(ByVal sNamespace As String, ByVal iCompId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select AVR_ID,AVR_Party from Acc_Vendor_Reconcilation where AVR_CompID=" & iCompId & ""
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetReferenceNo(ByVal sNamespace As String, ByVal iCompId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select AVR_ID,AVR_RefNo from Acc_Vendor_Reconcilation where AVR_CompID=" & iCompId & ""
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVoucherNo(ByVal sNamespace As String, ByVal iCompId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select AVR_ID,AVR_VoucherNo from Acc_Vendor_Reconcilation where AVR_CompID=" & iCompId & ""
            dt = objDB.SQLExecuteDataTable(sNamespace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDetails1(ByVal sNamespace As String, ByVal iCompId As Integer, ByVal irbtnStatus As Integer, ByVal iRefId As Integer, ByVal iSupplierId As Integer,
                                ByVal iVoucherno As Integer, ByVal sTrtype As String, ByVal dFromDate As Date, ByVal dTo As Date, ByVal sDebit As String, ByVal sCredit As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            dt = LoadAllDetails(sNamespace, iCompId, irbtnStatus, iRefId, iSupplierId, iVoucherno, sTrtype, dFromDate, dTo, sDebit, sCredit)
            If dt Is Nothing Then
                Return dt
            Else
                Dim dview As New DataView(dt)
                If irbtnStatus = 1 Then
                    dview = dt.DefaultView
                    dview.RowFilter = "Status='Matched'"
                    dt = dview.ToTable
                ElseIf irbtnStatus = 2 Then
                    dview = dt.DefaultView
                    dview.RowFilter = "Status='Un-Matched'"
                    dt = dview.ToTable
                ElseIf irbtnStatus = 3 Then
                    dview = dt.DefaultView
                    dt = dview.ToTable
                End If
                Return dt
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails(ByVal sNamespace As String, ByVal iCompId As Integer, ByVal irbtnStatus As Integer, ByVal iRefId As Integer, ByVal iSupplierId As Integer, ByVal iVoucherno As Integer, ByVal sTrtype As String, dFromDate As Date, ByVal dTo As Date, ByVal sDebit As String, ByVal sCredit As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtDetails As New DataTable
        Try
            dt.Columns.Add("iRefId")
            dt.Columns.Add("iSupplierID")
            dt.Columns.Add("iVoucherID")
            dt.Columns.Add("sTrType")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("sDebit")
            dt.Columns.Add("sCredit")
            dt.Columns.Add("Status")
            sSql = "select * from Acc_Vendor_Reconcilation where AVR_CompID=" & iCompId & ""
            If iRefId > 0 Then
                sSql = sSql & " And AVR_ID =" & iRefId & ""
            End If
            If iSupplierId > 0 Then
                sSql = sSql & " and AVR_Party=" & iSupplierId & ""
            End If
            If iVoucherno > 0 Then
                sSql = sSql & "and AVR_VoucherNo=" & iVoucherno & ""
            End If
            If sTrtype <> "" Then
                sSql = sSql & " and AVR_TrType=" & sTrtype & ""
            End If
            If dFromDate <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And AVR_Date Between " & objClsFasgnrl.FormatDtForRDBMS(dFromDate, "Q") & " "
            End If
            If dTo <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And " & objClsFasgnrl.FormatDtForRDBMS(dTo, "Q") & " "
            End If

            If sDebit <> "" Then
                sSql = sSql & " and AVR_TrDebit=" & sDebit & ""
            End If
            If sCredit <> "" Then
                sSql = sSql & " and AVR_TrCredit='" & sCredit & "'"
            End If

            dtDetails = objDB.SQLExecuteDataTable(sNamespace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("AVR_RefNo")) = False Then
                    dRow("iRefId") = dtDetails.Rows(i)("AVR_RefNo")
                Else
                    dRow("iRefId") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AVR_Party")) = False Then
                    dRow("iSupplierID") = dtDetails.Rows(i)("AVR_Party")
                Else
                    dRow("iSupplierID") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AVR_VoucherNo")) = False Then
                    dRow("iVoucherID") = dtDetails.Rows(i)("AVR_VoucherNo")
                Else
                    dRow("iVoucherID") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AVR_TrType")) = False Then
                    dRow("sTrType") = dtDetails.Rows(i)("AVR_TrType")
                Else
                    dRow("sTrType") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AVR_Date")) = False Then
                    dRow("PurchaseDate") = objClsFasgnrl.FormatDtForRDBMS(dtDetails.Rows(i)("AVR_Date"), "D")
                Else
                    dRow("PurchaseDate") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("AVR_TrDebit")) = False Then
                    dRow("sDebit") = dtDetails.Rows(i)("AVR_TrDebit")
                Else
                    dRow("sDebit") = "0.00"
                End If
                If IsDBNull(dtDetails.Rows(i)("AVR_TrCredit")) = False Then
                    dRow("sCredit") = dtDetails.Rows(i)("AVR_TrCredit")
                Else
                    dRow("sCredit") = "0.00"
                End If

                If dtDetails.Rows(i)("AVR_VendorStatus") = "EAM" Then
                    dRow("Status") = "Matched"
                ElseIf dtDetails.Rows(i)("AVR_VendorStatus") = "EAN" Then
                    dRow("Status") = "Un-Matched"
                End If
                dt.Rows.Add(dRow)
            Next
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
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
