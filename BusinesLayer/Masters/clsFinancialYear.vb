
Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsFinancialYear
    Dim objDB As New DBHelper
    Public Function SaveFinancialYear(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal iFromYear As Integer, ByVal iToYear As Integer, ByVal iIPAddress As String)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_FromDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = dFromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_ToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = dToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_From_Year", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iFromYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_To_Year", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iToYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_CrOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_Status", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = "W"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_Delflag", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = "W"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@YMS_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = iIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spFinanicalYear", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetFinancialYear(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Acc_Year_Master where YMS_ID =" & iYearID & " and YMS_CompID=" & iCompID & ""
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeletefinancialYearMaster(ByVal sNameSpace As String, iCompID As Integer, ByVal iMasterType As Integer, ByVal Mas_UserId As Integer, ByVal iIPAddress As String)
        Dim sSql As String
        Try
            sSql = ""
            sSql = "update Acc_Year_Master set YMS_DelFlag ='X',YMS_Status='D',YMs_DeletedBy=" & Mas_UserId & ",YMS_Operation='D',YMS_IPAddress='" & iIPAddress & "' where YMS_ID =" & iMasterType & " And YMS_CompID=" & iCompID & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ReCallfinancialYearMaster(ByVal sNameSpace As String, iCompID As Integer, ByVal iMasterType As Integer, ByVal Mas_UserId As Integer, ByVal iIPAddress As String)
        Dim sSql As String
        Try
            sSql = ""
            sSql = "update Acc_Year_Master set YMS_DelFlag='Y',YMS_Status='R',YMS_RecalledBy=" & Mas_UserId & ",YMS_Operation='D',YMS_IPAddress='" & iIPAddress & "' where YMS_ID =" & iMasterType & " and YMS_CompID=" & iCompID & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadYears(ByVal sNameSpace As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select YMS_ID,(Convert(nvarchar(50),YMS_From_Year)+'-'+Convert(nvarchar(50),YMS_To_Year)) as year from Acc_Year_Master  order by yms_From_year"
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDateFormat(ByVal sNameSpace As String, ByVal iDateCode As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Convert(varchar(10),GetDate()," & iDateCode & ")"
            Return objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch exp As System.Exception
            Throw
        End Try
    End Function
End Class
