Imports System
Imports System.Data
Imports DatabaseLayer

Public Class clsCustomerDetails
    Dim objDBL As New DBHelper
    Public Sub SaveCustRegDetails(ByVal sCD_ComapnyName As String, ByVal sCD_CompanyWebsite As String, ByVal sCD_CompanyEmailID As String,
                                  ByVal sCD_CompanyTelephoneno As String, ByVal sPD_Mobileno As String, ByVal sCD_ContactPerson As String, ByVal sCD_Address As String,
                                  ByVal iPD_ProductInterest As Integer, ByVal iPD_reason As Integer, ByVal sPD_Aboutus As String, ByVal sIPAddesss As String)

        Dim sSql As String = ""
        Dim sCD_RegNo As String
        Try
            sCD_RegNo = GetNewRegNo(iPD_ProductInterest)
            sSql = "INSERT into MMCSPL_Customer_Details(MCD_CD_RegNo,MCD_CD_CompanyName,MCD_CD_CompanyWebsite,MCD_CD_EmailID,MCD_CD_Telephoneno,MCD_CD_Mobilenumber,"
            sSql = sSql & "MCD_CD_ContactPerson,MCD_CD_Address,MCD_CD_ProductInterest,MCD_CD_Reason,MCD_CD_Aboutus,MCD_IPAddress,MCD_DateTime)values"
            sSql = sSql & "('" & sCD_RegNo & "','" & sCD_ComapnyName & "','" & sCD_CompanyWebsite & "','" & sCD_CompanyEmailID & "','" & sCD_CompanyTelephoneno & "',"
            sSql = sSql & "'" & sPD_Mobileno & "',"
            sSql = sSql & "'" & sCD_ContactPerson & "','" & sCD_Address & "' ,'" & iPD_ProductInterest & "',"
            sSql = sSql & "'" & iPD_reason & "','" & sPD_Aboutus & "','" & sIPAddesss & "',GetDate())"
            objDBL.SQLExecuteNonQuery("MMCSPL", sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function LoadAllCustomerDetails(ByVal iPD_ProductInterest As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Dim dtDisplay As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try
            sSql = "Select * from MMCSPL_Customer_Details"
            If iPD_ProductInterest > 0 Then
                sSql = sSql & " Where MCD_CD_ProductInterest=" & iPD_ProductInterest & ""
            End If
            sSql = sSql & " Order by MCD_CD_ProductInterest,MCD_CD_RegNo"
            dt = objDBL.SQLExecuteDataTable("MMCSPL", sSql)

            dtDisplay.Columns.Add("SlNo")
            dtDisplay.Columns.Add("PKID")
            dtDisplay.Columns.Add("RegNo")
            dtDisplay.Columns.Add("CompanyName")
            dtDisplay.Columns.Add("CompanyWebsite")
            dtDisplay.Columns.Add("EmailID")
            dtDisplay.Columns.Add("TelephoneNo")
            dtDisplay.Columns.Add("MobileNumber")
            dtDisplay.Columns.Add("ContactPerson")
            dtDisplay.Columns.Add("ProductInterest")
            dtDisplay.Columns.Add("Reason")

            For i = 0 To dt.Rows.Count - 1
                drow = dtDisplay.NewRow
                drow("SlNo") = i + 1
                drow("PKID") = dt.Rows(i)("MCD_CD_PKID")
                drow("RegNo") = dt.Rows(i)("MCD_CD_RegNo")
                drow("CompanyName") = dt.Rows(i)("MCD_CD_CompanyName")
                drow("CompanyWebsite") = dt.Rows(i)("MCD_CD_CompanyWebsite")
                drow("EmailID") = dt.Rows(i)("MCD_CD_EmailID")
                drow("TelephoneNo") = dt.Rows(i)("MCD_CD_Telephoneno")
                drow("MobileNumber") = dt.Rows(i)("MCD_CD_Mobilenumber")
                drow("ContactPerson") = dt.Rows(i)("MCD_CD_ContactPerson")
                If dt.Rows(i)("MCD_CD_ProductInterest") = 1 Then
                    drow("ProductInterest") = "TRACe PA"
                ElseIf dt.Rows(i)("MCD_CD_ProductInterest") = 2 Then
                    drow("ProductInterest") = "TRACe Enterprise"
                ElseIf dt.Rows(i)("MCD_CD_ProductInterest") = 3 Then
                    drow("ProductInterest") = "EDICT"
                ElseIf dt.Rows(i)("MCD_CD_ProductInterest") = 4 Then
                    drow("ProductInterest") = "FAS Pro"
                End If
                drow("Reason") = dt.Rows(i)("MCD_CD_Reason")
                dtDisplay.Rows.Add(drow)
            Next
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSelectedCustomerDetails(ByVal iPD_ProductInterest As Integer)
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from MMCSPL_Customer_Details where MCD_CD_PKID='" & iPD_ProductInterest & "'"
            dt = objDBL.SQLExecuteDataTable("MMCSPL", sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function searchByproduct(ByVal Value As String)
        Dim sSql As String
        Dim dt As New DataTable
        Dim dtDisplay As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try
            sSql = "Select * from MMCSPL_Customer_Details where MCD_CD_ProductInterest='" + Value + "'"
            dt = objDBL.SQLExecuteDataTable("MMCSPL", sSql)
            dtDisplay.Columns.Add("SlNo")
            dtDisplay.Columns.Add("PKID")
            dtDisplay.Columns.Add("RegNo")
            dtDisplay.Columns.Add("CompanyName")
            dtDisplay.Columns.Add("CompanyWebsite")
            dtDisplay.Columns.Add("EmailID")
            dtDisplay.Columns.Add("TelephoneNo")
            dtDisplay.Columns.Add("MobileNumber")
            dtDisplay.Columns.Add("ContactPerson")
            dtDisplay.Columns.Add("ProductInterest")
            dtDisplay.Columns.Add("Reason")

            For i = 0 To dt.Rows.Count - 1
                drow = dtDisplay.NewRow
                drow("SlNo") = i + 1
                drow("PKID") = dt.Rows(i)("MCD_CD_PKID")
                drow("RegNo") = dt.Rows(i)("MCD_CD_RegNo")
                drow("CompanyName") = dt.Rows(i)("MCD_CD_CompanyName")
                drow("CompanyWebsite") = dt.Rows(i)("MCD_CD_CompanyWebsite")
                drow("EmailID") = dt.Rows(i)("MCD_CD_EmailID")
                drow("TelephoneNo") = dt.Rows(i)("MCD_CD_Telephoneno")
                drow("MobileNumber") = dt.Rows(i)("MobileNumber")
                drow("ContactPerson") = dt.Rows(i)("MCD_CD_ContactPerson")
                If dt.Rows(i)("MCD_CD_ProductInterest") = 1 Then
                    drow("ProductInterest") = "TRACe PA"
                ElseIf dt.Rows(i)("MCD_CD_ProductInterest") = 2 Then
                    drow("ProductInterest") = "TRACe Enterprise"
                ElseIf dt.Rows(i)("MCD_CD_ProductInterest") = 3 Then
                    drow("ProductInterest") = "EDICT"
                ElseIf dt.Rows(i)("MCD_CD_ProductInterest") = 4 Then
                    drow("ProductInterest") = "FAS Pro"
                End If
                drow("Reason") = dt.Rows(i)("MCD_CD_Reason")
                dtDisplay.Rows.Add(drow)
            Next
            Return dtDisplay

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetNewRegNo(ByVal iPD_ProductInterest As Integer)
        Dim sSql As String = ""
        Dim iNewNo As Integer
        Dim sRegNo As String = ""
        Try
            sSql = "Select IsNull(Max(MCD_CD_PKID),0)+1 from MMCSPL_Customer_Details where MCD_CD_ProductInterest=" & iPD_ProductInterest & ""
            iNewNo = objDBL.SQLExecuteScalarInt("MMCSPL", sSql)
            If iPD_ProductInterest = 1 Then
                sRegNo = "MDTPA"
            ElseIf iPD_ProductInterest = 2 Then
                sRegNo = "MDTENT"
            ElseIf iPD_ProductInterest = 3 Then
                sRegNo = "MDEDICT"
            ElseIf iPD_ProductInterest = 4 Then
                sRegNo = "MDFAS"
            End If
            If iNewNo.ToString.Length = 1 Then
                sRegNo = sRegNo & "000" & iNewNo.ToString()
            ElseIf iNewNo.ToString.Length = 2 Then
                sRegNo = sRegNo & "00" & iNewNo.ToString()
            ElseIf iNewNo.ToString.Length = 3 Then
                sRegNo = sRegNo & "0" & iNewNo.ToString()
            ElseIf iNewNo.ToString.Length = 4 Then
                sRegNo = sRegNo & iNewNo.ToString()
            End If
            Return sRegNo
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAllserverDetails()
        Dim sSql As String
        Dim dt As New DataTable
        Dim dtDisplay As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try
            sSql = "Select MDA_DatabaseName,MDA_AccessCode,MDA_CompanyName,MDA_CreatedDate from MMCSPL_DB_Access"
            dt = objDBL.SQLExecuteDataTable("MMCSPL", sSql)

            dtDisplay.Columns.Add("SlNo")
            dtDisplay.Columns.Add("SD_DatabaseName")
            dtDisplay.Columns.Add("SD_AccessCode")
            dtDisplay.Columns.Add("SD_CompanyName")
            dtDisplay.Columns.Add("SD_CreatedOn")

            For i = 0 To dt.Rows.Count - 1
                drow = dtDisplay.NewRow
                drow("SlNo") = i + 1
                drow("SD_DatabaseName") = dt.Rows(i)("MDA_DatabaseName")
                drow("SD_AccessCode") = dt.Rows(i)("MDA_AccessCode")
                drow("SD_CompanyName") = dt.Rows(i)("MDA_CompanyName")
                drow("SD_CreatedOn") = dt.Rows(i)("MDA_CreatedDate")
                dtDisplay.Rows.Add(drow)
            Next
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
