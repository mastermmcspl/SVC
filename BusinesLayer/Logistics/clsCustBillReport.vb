Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsCustBillReport
    Dim objGen As New clsFASGeneral
    Private objDBL As New DatabaseLayer.DBHelper
    Public Function LoadCustomer1(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
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

    Public Function GetAccessCode(ByVal sAccessName As String) As DataTable
        Dim sSql As String
        Dim sAccessCode As DataTable
        Try
            sSql = "Select SAD_CMS_AccessCode,Sad_CMS_Name from Sad_CompanyMaster_Settings"
            sAccessCode = objDBL.SQLExecuteDataSet(sAccessName, sSql).Tables(0)
            Return sAccessCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadCustomerDetails1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * from Sales_Buyers_Masters where BM_ID = " & sCode & " and bm_Delflag <> 'D' and bm_CompID=" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadCustBillDetails1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Lgst_CustomerBilling where lcb_CustomerId = " & sCode & " and LCB_Delflag <> 'D' and LCB_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadCompanyDetails1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * from MST_Customer_Master where Cust_Code = '" & sCode & "' and Cust_Delflg <> 'D' and CUST_CompID=" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'Public Function LoadInvDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String, ByVal sInvCOde As String) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "" : sSql = "Select * from Lgst_CustomerBilling where LCB_CustomerID = '" & sCode & "' and LCB_InvNo='" & sInvCOde & "' and LCB_Delflag <> 'D' and LCB_CompID=" & iCompID & ""
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function loadTripDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustId As Integer, ByVal sInvoiceNo As String, ByVal iRouteId As Integer, ByVal sStartDate As String, ByVal sTodate As String, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtRoute As New DataTable
        Dim sRouteID As String
        Try

            'sSql = "" : sSql = "Select LRM_Id from lgst_route_Master where LRM_StartDestPlace='" & sRouteName & "' and LRM_CompID=" & iCompID & ""
            'dtRoute = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            'If dtRoute.Rows.Count > 0 Then
            '    For i = 0 To dtRoute.Rows.Count - 1
            '        If i = 0 Then
            '            sRouteID = dtRoute.Rows(i).Item("LRM_Id")
            '        Else
            '            sRouteID = sRouteID & "," & dtRoute.Rows(i).Item("LRM_Id")
            '        End If
            '    Next
            'End If
            '  sStartDate = objGen.FormatDtForRDBMS(sStartDate, "T")
            sStartDate = objGen.FormatDtForRDBMS(sStartDate, "Q")
            sTodate = objGen.FormatDtForRDBMS(sTodate, "Q")
            sSql = "" : sSql = "Select * from Lgst_TripGeneration_Master where LTGM_DestinationCustomer = '" & iCustId & "' and  LTGM_Delflag <> 'D' and LTGM_CompID=" & iCompID & " and LTGM_RouteID=" & iRouteId & " and LTGM_StopDate Between " & sStartDate & " And " & sTodate & " and LTGM_YearID= " & iYearId & " order by LTGM_StartDate asc"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function NumberToWord(ByVal num1 As String) As String
        Dim words, strones(100), strtens(100), aftrdecimalWord As String
        Dim crore, lakhs, thousands, hundreds, tens, ssingle, aftrDecimal1, aftrDecimal, num As Double
        Try
            If (num1.Contains(".")) Then
                Dim str1 As String() = Strings.Split(num1, ".")
                num = Convert.ToDouble(str1(0))
            Else
                num = Convert.ToDouble(num1)
            End If
            aftrDecimal1 = num

            If num = 0 Then
                Return ""
            End If


            If num < 0 Then
                Return "Not supported"
            End If

            words = ""
            strones = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
            strtens = {"Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
            crore = 0
            lakhs = 0
            thousands = 0
            hundreds = 0
            tens = 0
            ssingle = 0

            If (num > 10000000) Then

                If ((Convert.ToString(num / 10000000)).Contains(".")) Then
                    crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
                    num = num - (crore * 10000000)
                Else
                    crore = num / 100
                    num = num - (crore * 10000000)
                End If
            End If

            If (num > 100000) Then

                If ((Convert.ToString(num / 100000)).Contains(".")) Then
                    lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                    num = num - (lakhs * 100000)
                Else
                    lakhs = num / 100000
                    num = num - (lakhs * 100000)
                End If
            End If


            'lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))        
            If (num > 1000) Then

                If ((Convert.ToString(num / 1000)).Contains(".")) Then
                    thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
                    num = num - (thousands * 1000)
                Else
                    thousands = num / 1000
                    num = num - (thousands * 1000)
                End If
            End If
            'thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))        

            If (num >= 100) Then

                If ((Convert.ToString(num / 100)).Contains(".")) Then
                    hundreds = Convert.ToInt32((num / 100).ToString().Substring(0, (num / 100).ToString().IndexOf(".")))
                    num = num - (hundreds * 100)
                Else
                    hundreds = num / 100
                    num = num - (hundreds * 100)
                End If
            End If
            If num > 19 Then
                If ((Convert.ToString(num / 10)).Contains(".")) Then
                    tens = Convert.ToInt32((num / 10).ToString().Substring(0, (num / 10).ToString().IndexOf(".")))
                    num = num - (tens * 10)
                Else
                    tens = num / 10
                    num = num - (tens * 10)
                End If

            End If

            ssingle = num

            If crore > 0 Then
                If crore > 19 Then
                    words += NumberToWord(crore) + "Crore "
                Else
                    words += strones(crore - 1) + " Crore "
                End If
            End If
            If lakhs > 0 Then

                If lakhs > 19 Then
                    words += NumberToWord(lakhs) + "Lakh "
                Else
                    words += strones(lakhs - 1) + " Lakh "
                End If
            End If

            If thousands > 0 Then

                If thousands > 19 Then
                    ' Dim s As String = Convert.ToString(thousands).Substring(1, 2).ToString()
                    ' Dim Ts As Double = Convert.ToDouble(s)
                    words += NumberToWord(thousands) + "Thousand "
                Else
                    words += strones(thousands - 1) + " Thousand "
                End If
            End If


            If hundreds > 0 Then
                words += strones(hundreds - 1) + " Hundred "
            End If


            If tens > 0 Then
                words += strtens(tens - 2) + " "
            End If

            If ssingle > 0 Then
                words += strones(ssingle - 1) + " "
            End If

            If (num1.Contains(".")) Then
                Dim str As String() = Strings.Split(num1, ".")
                aftrDecimal = Convert.ToDouble(str(1))
                aftrdecimalWord = AfterDecimalfunction(aftrDecimal)
                If aftrdecimalWord = "zero" Then
                    words += ""
                Else
                    aftrdecimalWord += " Paise"
                    words += " And " + aftrdecimalWord

                End If
            End If
            Return words
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AfterDecimalfunction(ByVal num As Decimal) As String
        Dim words, strones(100), strtens(100) As String
        Dim crore, lakhs, thousands, hundreds, tens, ssingle As Decimal
        Try
            If num = 0 Then
                Return "Zero"
            End If

            If num < 0 Then
                Return "Not supported"
            End If
            words = ""
            strones = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
            strtens = {"Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
            crore = 0
            lakhs = 0
            thousands = 0
            hundreds = 0
            tens = 0
            ssingle = 0

            If ((Convert.ToString(num / 10000000)).Contains(".")) Then
                crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
                num = num - (hundreds * 10000000)
            Else
                crore = num / 10000000
                num = num - (hundreds * 10000000)
            End If

            'crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
            'num = num - (crore * 10000000)


            If ((Convert.ToString(num / 100000)).Contains(".")) Then
                lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                num = num - (hundreds * 100000)
            Else
                lakhs = num / 100000
                num = num - (hundreds * 100000)
            End If

            'lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))

            'num = num - (lakhs * 100000)

            If ((Convert.ToString(num / 1000)).Contains(".")) Then
                thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
                num = num - (thousands * 1000)
            Else
                thousands = num / 1000
                num = num - (thousands * 1000)
            End If

            thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
            num = num - (thousands * 1000)


            If ((Convert.ToString(num / 100)).Contains(".")) Then
                hundreds = Convert.ToInt32((num / 100).ToString().Substring(0, (num / 100).ToString().IndexOf(".")))
                num = num - (hundreds * 100)
            Else
                hundreds = num / 100
                num = num - (hundreds * 100)
            End If
            If num > 19 Then
                If ((Convert.ToString(num / 10)).Contains(".")) Then
                    tens = Convert.ToInt32((num / 10).ToString().Substring(0, (num / 10).ToString().IndexOf(".")))
                    num = num - (tens * 10)
                Else
                    tens = num / 10
                    num = num - (tens * 10)
                End If

            End If

            ssingle = num

            If crore > 0 Then
                If crore > 19 Then
                    words += NumberToWord(crore) + "Crore "
                Else
                    words += strones(crore - 1) + " Crore "
                End If
            End If
            If lakhs > 0 Then

                If lakhs > 19 Then
                    words += NumberToWord(lakhs) + "Lakh "
                Else
                    words += strones(lakhs - 1) + " Lakh "
                End If
            End If

            If thousands > 0 Then

                If thousands > 19 Then
                    words += NumberToWord(thousands) + "Thousand "
                Else
                    words += strones(thousands - 1) + " Thousand "
                End If
            End If

            If hundreds > 0 Then
                words += strones(hundreds - 1) + " Hundred "
            End If

            If tens > 0 Then
                words += strtens(tens - 2) + " "
            End If

            If ssingle > 0 Then
                words += strones(ssingle - 1) + " "
            End If
            Return words
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function Route(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As Integer)
        Dim sSql As String
        Dim Routename As String
        Try
            sSql = "Select LRM_StartDestPlace from lgst_Route_master where LRM_ID= '" & sCode & "' and LRM_Delflag <> 'D' and LRM_CompID=" & iCompID & ""
            RouteNAme = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return Routename
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CustomerStates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As Integer)
        Dim sSql As String
        Dim CustomerState As String
        Try
            sSql = " Select Mas_desc From ACC_General_Master Where Mas_CompID = 1 And mas_id = '" & sCode & "'  And Mas_Master In(Select Mas_ID From Acc_Master_Type Where Mas_Type='State' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            CustomerState = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return CustomerState
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RouteNAme(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iRouteId As Integer)
        Dim sSql As String
        Dim sRoutName As String
        Try
            sSql = " Select LRM_StartDestPlace From Lgst_Route_Master Where LRM_ID ='" & iRouteId & "'  and LRM_Delflag ='A'"
            sRoutName = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sRoutName
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInvoiceNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LCB_ID,LCB_InvNo From lgst_Customerbilling Where LCB_CustomerID='" & iCustId & "' and LCB_CompID=" & iCompID & " and LCB_YearId=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function



    Public Function LoadInvDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustId As Integer, ByVal sInvNoNo As String, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtDriverBillDetails As New DataTable
        Dim dRow As DataRow
        Try

            dt.Columns.Add("RouteID")
            dt.Columns.Add("InvNo")
            dt.Columns.Add("InvDate")
            dt.Columns.Add("CustOrderRef")
            dt.Columns.Add("Agreement")
            dt.Columns.Add("SGST")
            dt.Columns.Add("SGSTAmount")
            dt.Columns.Add("CGST")
            dt.Columns.Add("CGSTAmount")
            dt.Columns.Add("IGST")
            dt.Columns.Add("IGSTAmount")
            dt.Columns.Add("FromDate")
            dt.Columns.Add("ToDate")
            dt.Columns.Add("TotalAmt")
            dt.Columns.Add("sFromDate")
            dt.Columns.Add("sToDate")
            sSql = "" : sSql = "Select * from Lgst_CustomerBilling where LCB_CustomerID = '" & iCustId & "' and LCB_InvNo='" & sInvNoNo & "' and LCB_Delflag <> 'D' and LCB_CompID=" & iCompID & " and LCB_YearID=" & iYearId & ""
            dtDriverBillDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtDriverBillDetails.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("RouteID") = dtDriverBillDetails.Rows(0)("LCB_RouteID").ToString()
                dRow("InvNo") = dtDriverBillDetails.Rows(0)("LCB_InvNo").ToString.Substring(0, 10)
                dRow("InvDate") = objGen.FormatDtForRDBMS(dtDriverBillDetails.Rows(0)("LCB_InvDate").ToString(), "D")
                dRow("CustOrderRef") = dtDriverBillDetails.Rows(0)("LCB_CustOrderRef").ToString()
                dRow("Agreement") = dtDriverBillDetails.Rows(0)("LCB_Agreement").ToString()
                dRow("SGST") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LCB_SGST").ToString()))
                dRow("SGSTAmount") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LCB_SGSTAmount").ToString()))
                dRow("CGST") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LCB_CGST").ToString()))
                dRow("CGSTAmount") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LCB_CGSTAmount").ToString()))
                dRow("IGST") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LCB_IGST").ToString()))
                dRow("IGSTAmount") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LCB_IGSTAmount").ToString()))
                dRow("FromDate") = objGen.FormatDtForRDBMS(dtDriverBillDetails.Rows(0)("LCB_FromDate").ToString(), "D")
                dRow("ToDate") = objGen.FormatDtForRDBMS(dtDriverBillDetails.Rows(0)("LCB_ToDate").ToString(), "D")
                dRow("TotalAmt") = String.Format("{0:0.0}", Convert.ToDecimal(dtDriverBillDetails.Rows(0)("LCB_TotalAmt").ToString()))
                dRow("sFromDate") = dtDriverBillDetails.Rows(0)("LCB_FromDate")
                dRow("sToDate") = dtDriverBillDetails.Rows(0)("LCB_ToDate")
                dt.Rows.Add(dRow)
            End If
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerDetails1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtCustomerDetails As New DataTable
        Dim dRow As DataRow
        Try

            dt.Columns.Add("State")
            dt.Columns.Add("Name")
            dt.Columns.Add("Address")
            dt.Columns.Add("GSTNRegNo")
            dt.Columns.Add("Code")
            sSql = "" : sSql = "Select * from Sales_Buyers_Masters where BM_ID = " & sCode & " and bm_Delflag <> 'D' and bm_CompID=" & iCompID & ""
            dtCustomerDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtCustomerDetails.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("State") = dtCustomerDetails.Rows(0)("BM_State").ToString()
                dRow("Name") = dtCustomerDetails.Rows(0)("BM_Name").ToString()
                dRow("Address") = dtCustomerDetails.Rows(0)("BM_Address").ToString()
                dRow("GSTNRegNo") = dtCustomerDetails.Rows(0)("BM_GSTNRegNo").ToString()
                dRow("Code") = dtCustomerDetails.Rows(0)("BM_Code").ToString()
                dt.Rows.Add(dRow)
            End If
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyDetails1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtCompanyDetails As New DataTable
        Dim dRow As DataRow
        Try

            dt.Columns.Add("CState")
            dt.Columns.Add("CName")
            dt.Columns.Add("CAddress")
            dt.Columns.Add("CGSTNRegNo")
            dt.Columns.Add("CCode")
            sSql = "" : sSql = "Select * from MST_Customer_Master where Cust_Code = '" & sCode & "' and Cust_Delflg <> 'D' and CUST_CompID=" & iCompID & ""
            dtCompanyDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtCompanyDetails.Rows.Count > 0 Then
                dRow = dt.NewRow()
                dRow("CState") = dtCompanyDetails.Rows(0)("CUST_COMM_STATE").ToString()
                dRow("CName") = dtCompanyDetails.Rows(0)("CUST_NAME").ToString()
                dRow("CAddress") = dtCompanyDetails.Rows(0)("CUST_COMM_ADDRESS").ToString()
                dRow("CGSTNRegNo") = dtCompanyDetails.Rows(0)("CUST_FinalNo").ToString()
                dRow("CCode") = dtCompanyDetails.Rows(0)("CUST_CODE").ToString()
                dt.Rows.Add(dRow)
            End If
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
