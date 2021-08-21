﻿Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Structure strDesc_Details
    Dim iDesc_Id As Integer
    Dim sDesc_Name As String
    Dim sDesc_Note As String
    Dim sDesc_Size As String
    Dim iDesc_DType As String
    Dim sDesc_Status As String
    Dim iDesc_CrBy As Integer
    Dim iDesc_UpdatedBy As Integer
    Dim sDesc_DefaultValue As String
    Dim iDesc_CompId As Integer
    Dim sDesc_IPAddress As String
    Dim sDesc_Flag As String
    Public Property sDescFlag() As String
        Get
            Return (sDesc_Flag)
        End Get
        Set(ByVal Value As String)
            sDesc_Flag = Value
        End Set
    End Property

    Public Property iDescId() As Integer
        Get
            Return (iDesc_Id)
        End Get
        Set(ByVal Value As Integer)
            iDesc_Id = Value
        End Set
    End Property
    Public Property sDescName() As String
        Get
            Return (sDesc_Name)
        End Get
        Set(ByVal Value As String)
            sDesc_Name = Value
        End Set
    End Property
    Public Property sDescNote() As String
        Get
            Return (sDesc_Note)
        End Get
        Set(ByVal Value As String)
            sDesc_Note = Value
        End Set
    End Property
    Public Property sDescSize() As String
        Get
            Return (sDesc_Size)
        End Get
        Set(ByVal Value As String)
            sDesc_Size = Value
        End Set
    End Property
    Public Property iDescDType() As Integer
        Get
            Return (iDesc_DType)
        End Get
        Set(ByVal Value As Integer)
            iDesc_DType = Value
        End Set
    End Property
    Public Property sDescStatus() As String
        Get
            Return (sDesc_Status)
        End Get
        Set(ByVal Value As String)
            sDesc_Status = Value
        End Set
    End Property
    Public Property iDescCrBy() As Integer
        Get
            Return (iDesc_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iDesc_CrBy = Value
        End Set
    End Property
    Public Property iDescUpdatedBy() As Integer
        Get
            Return (iDesc_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iDesc_UpdatedBy = Value
        End Set
    End Property
    Public Property sDescDefaultValue() As String
        Get
            Return (sDesc_DefaultValue)
        End Get
        Set(ByVal Value As String)
            sDesc_DefaultValue = Value
        End Set
    End Property
    Public Property iDescCompId() As Integer
        Get
            Return (iDesc_CompId)
        End Get
        Set(ByVal Value As Integer)
            iDesc_CompId = Value
        End Set
    End Property
    Public Property sDescIPAddress() As String
        Get
            Return (sDesc_IPAddress)
        End Get
        Set(ByVal Value As String)
            sDesc_IPAddress = Value
        End Set
    End Property
End Structure
Public Class clsDescriptor
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsFASGeneral As New clsFASGeneral
    Public Function LoadDescDataType(ByVal sAC As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select DT_ID,DT_DataType from EDT_DESC_Type Where DT_Status='A' Order by DT_DataType Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDescriptorsDetails(ByVal sAC As String, ByVal iDescID As Integer, ByVal iStatus As Integer, Optional ByVal sUsrName As String = "", Optional ByVal sOrderBy As String = "DESC_NAME", Optional ByVal sOrderType As String = "ASC") As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("DescID")
            dt.Columns.Add("Name")
            dt.Columns.Add("Note")
            dt.Columns.Add("DataType")
            dt.Columns.Add("Size")
            dt.Columns.Add("CrBy")
            dt.Columns.Add("CrOn")
            dt.Columns.Add("Status")
            dt.Columns.Add("DescValue")

            sSql = "Select a.DES_ID,a.DESC_NAME,a.DESC_NOTE,c.DT_DataType,a.DESC_SIZE,a.DESC_CRON,a.DESC_DelFlag,b.usr_FullName as DESC_CRBY,a.EDD_DefaultValues From EDT_DESCRIPTIOS a"
            sSql = sSql & " Left Join Sad_UserDetails b On b.usr_ID=a.DESC_CRBY "
            sSql = sSql & " Left Join EDT_DESC_Type c On c.DT_ID=a.DESC_DATATYPE"
            sSql = sSql & "  where  "
            If iDescID > 0 Then
                sSql = sSql & "  a.DES_ID=" & iDescID & " And"
            End If
            If iStatus = 0 Then
                sSql = sSql & " a.DESC_DelFlag='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " a.DESC_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " a.DESC_DelFlag='W'" 'Waiting for approval
            End If
            If sUsrName <> "" Then
                sSql = sSql & " And " & sOrderBy & " Like ('" & sUsrName & "%') Order By " & sOrderBy & " " & sOrderType & ""
            Else
                sSql = sSql & " Order By " & sOrderBy & " " & sOrderType
            End If
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dt.NewRow
                    If IsDBNull(dtdetails.Rows(i)("DES_ID")) = False Then
                        dRow("DescID") = dtdetails.Rows(i)("DES_ID")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DESC_NAME")) = False Then
                        dRow("Name") = objclsFASGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DESC_NAME"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DESC_NOTE")) = False Then
                        dRow("Note") = objclsFASGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DESC_NOTE"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DT_DataType")) = False Then
                        dRow("DataType") = objclsFASGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DT_DataType"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DESC_SIZE")) = False Then
                        dRow("Size") = objclsFASGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DESC_SIZE"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DESC_CRBY")) = False Then
                        dRow("CrBy") = objclsFASGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DESC_CRBY"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DESC_CRON")) = False Then
                        dRow("CrOn") = objclsFASGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("DESC_CRON"), "F")
                    End If
                    If dtdetails.Rows(i)("DESC_DelFlag") = "A" Then
                        dRow("Status") = "Activated"
                    ElseIf dtdetails.Rows(i)("DESC_DelFlag") = "D" Then
                        dRow("Status") = "De-Activated"
                    ElseIf dtdetails.Rows(i)("DESC_DelFlag") = "W" Then
                        dRow("Status") = "Waiting for Approval"
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_DefaultValues")) = False Then
                        dRow("DescValue") = objclsFASGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("EDD_DefaultValues"))
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DescApproveStatus(ByVal sAC As String, ByVal iUserID As Integer, ByVal iDescID As Integer, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update EDT_DESCRIPTIOS set"
            If sType = "Created" Then
                sSql = sSql & " DESC_DelFlag='A',DESC_APPROVEDBY=" & iUserID & ", DESC_APPROVEDON=Getdate()"
            ElseIf sType = "De-Activated" Then
                sSql = sSql & " DESC_DelFlag='D',DESC_DELETEDBY=" & iUserID & ", DESC_DELETEDON=Getdate()"
            ElseIf sType = "Activated" Then
                sSql = sSql & " DESC_DelFlag='A',DESC_RECALLBY=" & iUserID & ", DESC_RECALLON=Getdate()"
            End If
            sSql = sSql & " Where DES_ID=" & iDescID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckAvailabilityDescName(ByVal sAC As String, ByVal sName As String, Optional ByVal iDescId As Int16 = 0) As Boolean
        Dim sSql As String
        Dim iRet As Integer
        Try
            sSql = "Select count(*) from EDT_DESCRIPTIOS where DESC_NAME='" & sName & "' and DES_ID<>" & iDescId & " "
            iRet = objDBL.SQLExecuteScalar(sAC, sSql)
            If iRet = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDescriptorDetails(ByVal sAC As String, ByVal objDescriptor As strDesc_Details)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Dim sSql As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DES_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objDescriptor.iDescId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DESC_NAME", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objDescriptor.sDescName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DESC_NOTE", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objDescriptor.sDescNote
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DESC_DATATYPE", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDescriptor.iDescDType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DESC_SIZE", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objDescriptor.sDescSize
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DESC_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objDescriptor.iDescCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DESC_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objDescriptor.iDescUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DESC_STATUS", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDescriptor.sDescStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DESC_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objDescriptor.iDescCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DESC_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDescriptor.sDescIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "InOrUpDescriptor", 1, Arr, ObjParam)

            sSql = "Update EDT_DESCRIPTIOS Set EDD_DefaultValues='" & objDescriptor.sDescDefaultValue & "' where DESC_NAME='" & objDescriptor.sDescName & "'"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
