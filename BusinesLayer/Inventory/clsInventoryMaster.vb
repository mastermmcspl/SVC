Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsInventoryMaster
    Dim dbObj As New DBHelper
    Public Structure Inventory
        Dim iInv_ID As Integer
        Dim sInv_Code As String
        Dim sInv_Description As String
        Dim iInv_Parent As Integer
        Dim sInv_Flag As String
        Dim iInv_Unit As Integer
        Dim iInv_AlterUnit As Integer
        Dim sInv_Price As String
        Dim sInv_Excise As String
        Dim sInv_Vat As String
        Dim iInv_CreatedBy As Integer
        Dim dInv_CreatedOn As Date
        Dim iInv_ApporvedBy As Integer
        Dim dInv_ApprovedOn As Date
        Dim iInv_DeletedBy As Integer
        Dim dInv_DeletedOn As Date
        Dim iInv_RecallBy As Integer
        Dim dInv_RecalledOn As Date
        Dim iInv_CompID As Integer
        Dim iInv_PerPieces As Integer
        Dim sInv_Size As String
        Dim sInv_Color As String
        Dim sInv_Acode As String
        Dim sInv_Ccode As String
        Dim sINV_Operation As String
        Dim sINV_IPAddress As String

        Dim iGST_ID As Integer
        Dim iGST_ScheduleID As Integer
        Dim iGST_CommodityID As Integer
        Dim iGST_ItemID As Integer
        Dim iGST_ScheduleType As Integer
        Dim dGST_GSTRate As Double
        Dim sGST_SlNo As String
        Dim sGST_CHST As String
        Dim sGST_Chapter As String
        Dim sGST_Heading As String
        Dim sGST_SubHeading As String
        Dim sGST_Tarrif As String
        Dim sGST_SubSlNo As String
        Dim sGST_CESS As Double
        Dim sGST_GoodDescription As String
        Dim sGST_NotificationNo As String
        Dim dGST_NotificationFromDate As Date
        Dim dGST_NotificationToDate As Date
        Dim sGST_FileNo As String
        Dim dGST_FileFromDate As Date
        Dim dGST_FileToDate As Date
        Dim sGST_Status As String
        Dim iGST_CompID As Integer
        Dim iGST_YearID As Integer
        Dim sGST_Operation As String
        Dim sGST_IPAddress As String
    End Structure
    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return dbObj.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInventoryTreeView(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim pstrSQL As String = ""
        Try
            pstrSQL = "SELECT Inv_ID,Inv_Parent,Inv_Code from Inventory_Master where Inv_CompID = " & iCompID & " and Inv_code <> ''"
            Return dbObj.SQLExecuteDataTable(sNameSpace, pstrSQL)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserList(ByVal sNameSpace As String, ByVal sString As String)
        Dim sSql As String = "", sRes As String = ""
        Dim dt As DataTable
        Try
            sSql = "SELECT Inv_Description FROM inventory_master WHERE Inv_Description LIKE '%" & sString & "%'  ORDER BY Inv_Description "
            dt = dbObj.SQLExecuteDataTable(sNameSpace, sSql)
            For iIndx As Integer = 0 To dt.Rows.Count - 2
                sRes = sRes & dt.Rows(iIndx)("Inv_Description") & "|"
            Next
            If dt.Rows.Count > 0 Then
                sRes = sRes & dt.Rows(dt.Rows.Count - 1)("Inv_Description")
            End If
            Return sRes
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveInventoryMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objInv As Inventory, ByVal iSaveOrUpdate As Integer)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iInv_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objInv.sInv_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Description", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objInv.sInv_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Parent", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iInv_Parent
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objInv.sInv_Flag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Unit", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iInv_Unit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_AlterUnit", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iInv_AlterUnit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Price", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objInv.sInv_Price
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Excise", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objInv.sInv_Excise
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Vat", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objInv.sInv_Vat
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_CreatedBy ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iInv_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_CreatedOn  ", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objInv.dInv_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_PerPieces", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iInv_PerPieces
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INV_MRP", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INV_Retail", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INV_PreDeterminedPrice", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INV_EffeFrom", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INV_EffeTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INV_Others", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ""
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Size", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objInv.sInv_Size
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Color", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objInv.sInv_Color
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Acode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objInv.sInv_Acode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objInv.sINV_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objInv.sINV_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Ccode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objInv.sInv_Ccode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Debug", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iSaveOrUpdate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = dbObj.ExecuteSPForInsertARR(sNameSpace, "spInventoryMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function


    'Public Shared Function LoadUnitOfMeasurement(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master =1 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
    '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function GetInventoryMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParent As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Inventory_Master where Inv_ID =" & iParent & " and Inv_CompID =" & iCompID & ""
            Return dbObj.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SearchInventoryMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal SearchData As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Inventory_Master where Inv_Code like '%" & SearchData & "%' or Inv_Description like '%" & SearchData & "%' and Inv_CompID =" & iCompID & ""
            Return dbObj.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPath(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal lParent As Integer)
        Dim myDataSet As DataTable
        Dim objInv As New clsInventoryMaster.Inventory
        Dim sSql As String = ""
        Try
            sSql = "select * from Inventory_Master where Inv_ID =" & lParent & " and Inv_Flag= 'X' and Inv_CompID =" & iCompID & ""
            myDataSet = dbObj.SQLExecuteDataTable(sNameSpace, sSql)
            If myDataSet.Rows.Count <> 0 Then
                objInv.iInv_Parent = myDataSet.Rows(0).Item("Inv_Parent")
                objInv.sInv_Description = myDataSet.Rows(0).Item("Inv_Description")
            End If
            Return objInv
        Catch ex As Exception
            Throw
        Finally
            myDataSet = Nothing
        End Try
    End Function

    'Public Shared Function GetPaths(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal lParent As Integer)
    '    Dim myDataSet As DataTable
    '    Dim objInv As New clsInventoryMaster.Inventory
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "select * from Inventory_Master where Inv_ID =" & lParent & " and Inv_Flag= 'X' and Inv_CompID =" & iCompID & ""
    '        myDataSet = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If myDataSet.Rows.Count <> 0 Then
    '            objInv.iInv_Parent = myDataSet.Rows(0).Item("Inv_Parent")
    '            objInv.sInv_Description = myDataSet.Rows(0).Item("Inv_Description")
    '        End If
    '        Return objInv
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        myDataSet = Nothing
    '    End Try
    'End Function
    Public Function loaddetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Application_Settings where AS_CompID = " & iCompID & ""
            dt = dbObj.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Shared Function AutoSearch(ByVal sNameSpace As String, ByVal prefixText As String)
    '    Dim dr As OleDb.OleDbDataReader
    '    Dim customers As List(Of String) = New List(Of String)
    '    Dim sSql As String
    '    Try
    '        sSql = "Select * from inventory_master where Inv_Description like '%" & prefixText & "%'"
    '        dr = DBHelper.DBLDataReader(sNameSpace, sSql)
    '        While dr.Read
    '            customers.Add(dr("Inv_Description").ToString)
    '        End While
    '        Return customers
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function GetMaxIDofInventoryMaster(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            Return dbObj.SQLExecuteScalar(sNameSpace, "Select isnull(max(Inv_ID)+1,1) from inventory_master")
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Shared Function BindCommodity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "Select Inv_ID,Inv_Description from Inventory_Master where INV_Parent=0 And Inv_CompID = " & iCompID & ""
    '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Shared Function UpdateEditedData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iID As Integer) As String
    '    Dim sSql As String = ""
    '    Dim sStr As String = ""
    '    Try
    '        sSql = "Update Inventory_Master Set INV_Parent=" & iCommodityID & " Where INV_ID=" & iID & " And Inv_CompID = " & iCompID & ""
    '        DBHelper.ExecuteNoNQuery(sNameSpace, sSql)
    '        sStr = "Moved Successfully"
    '        Return sStr
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function 
    Public Function SearchGSTDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sHSNSearchDesc As String, ByVal sHSNCodeSearch As String) As DataTable
        Dim sSql As String = ""
        Dim iMasterID As Integer
        Try
            sSql = "" : sSql = "Select Top 1 GSTM_ID From Acc_GST_Schedule_Master Where GSTM_CompID=" & iCompID & " Order By GSTM_ID Desc "
            iMasterID = dbObj.SQLExecuteScalarInt(sNameSpace, sSql)

            If sHSNCodeSearch <> "" Then
                sSql = "Select AGS_ID,AGS_GoodDescription from Acc_GST_Schedule where (AGS_CHST like '" & sHSNCodeSearch & "%' Or AGS_CHST like '%" & sHSNCodeSearch & "%' Or AGS_CHST like '%" & sHSNCodeSearch & "') And AGS_GSTM_ID=" & iMasterID & " and AGS_CompID =" & iCompID & ""
                Return dbObj.SQLExecuteDataTable(sNameSpace, sSql)
            ElseIf sHSNSearchDesc <> "" Then
                sSql = "Select AGS_ID,AGS_GoodDescription from Acc_GST_Schedule where (AGS_GoodDescription like '" & sHSNSearchDesc & "%' Or AGS_GoodDescription like '%" & sHSNSearchDesc & "%' Or AGS_GoodDescription like '%" & sHSNSearchDesc & "') And AGS_GSTM_ID=" & iMasterID & " and AGS_CompID =" & iCompID & ""
                Return dbObj.SQLExecuteDataTable(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAGS_ID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Acc_GST_Schedule where AGS_ID=" & iAGS_ID & " and AGS_CompID =" & iCompID & ""
            Return dbObj.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveGSTRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objInv As Inventory, ByVal iSaveOrUpdate As Integer)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iGST_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_ScheduleID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iGST_ScheduleID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_CommodityID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iGST_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_ItemID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iGST_ItemID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_ScheduleType ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iGST_ScheduleType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_GSTRate", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objInv.dGST_GSTRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_SlNo", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objInv.sGST_SlNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_CHST", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objInv.sGST_CHST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_Chapter", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objInv.sGST_Chapter
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_Heading", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objInv.sGST_Heading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_SubHeading", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objInv.sGST_SubHeading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_Tarrif", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objInv.sGST_Tarrif
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_SubSlNo", OleDb.OleDbType.VarChar, 6)
            ObjParam(iParamCount).Value = objInv.sGST_SubSlNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_CESS", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objInv.sGST_CESS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_GoodDescription", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objInv.sGST_GoodDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_NotificationNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objInv.sGST_NotificationNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_NotificationFromDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objInv.dGST_NotificationFromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_NotificationToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objInv.dGST_NotificationToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_FileNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objInv.sGST_FileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_FileFromDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objInv.dGST_FileFromDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_FileToDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objInv.dGST_FileToDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_Status", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objInv.sGST_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iGST_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objInv.iGST_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objInv.sGST_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@GST_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objInv.sGST_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = dbObj.ExecuteSPForInsertARR(sNameSpace, "spGST_Rates", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTRatesDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINV_ID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Top 1 * from GST_Rates where GST_ItemID =" & iINV_ID & " and GST_CompID =" & iCompID & " Order By GST_ID Desc"
            Return dbObj.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCommodityID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iINV_ID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select INV_Parent from Inventory_Master where INV_ID =" & iINV_ID & " and INV_CompID =" & iCompID & ""
            Return dbObj.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNotificationFromDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCommodityID As Integer, ByVal iINV_ID As Integer) As Date
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Try

            bCheck = dbObj.SQLCheckForRecord(sNameSpace, "Select * From GST_rates Where GST_CommodityID=" & iCommodityID & " And GST_ItemID =" & iINV_ID & " and GST_CompID =" & iCompID & " ")
            If bCheck = True Then
                sSql = "Select Top 1 CONVERT(VARCHAR(10),GST_NotificationFromDate,103)As GST_NotificationFromDate From GST_Rates Where GST_CommodityID=" & iCommodityID & " And GST_ItemID =" & iINV_ID & " and GST_CompID =" & iCompID & " order by GST_ID Desc "
                GetNotificationFromDate = dbObj.SQLGetDescription(sNameSpace, sSql)
            Else
                GetNotificationFromDate = "01/01/1900"
            End If

            Return GetNotificationFromDate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadItems(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCommodityID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            If iCommodityID > 0 Then
                sSql = "Select INV_ID,INV_Description From Inventory_Master Where INV_Parent=" & iCommodityID & " And INV_CompID=" & iCompID & " "
            Else
                sSql = "Select INV_ID,INV_Description From Inventory_Master Where INV_CompID=" & iCompID & " "
            End If
            dt = dbObj.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
