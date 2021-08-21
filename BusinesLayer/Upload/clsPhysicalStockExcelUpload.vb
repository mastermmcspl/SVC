Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsPhysicalStockExcelUpload
    Private objDB As New DBHelper
    Private iInv_ID As Integer
    Private sInv_Code As String
    Private sInv_Description As String
    Private iInv_Parent As Integer
    Private sInv_Flag As String
    Private iInv_CompID As Integer
    Private sInv_Size As String
    Private sInv_Color As String
    Private sInv_Acode As String
    Private iInv_CreatedBy As Integer

    Private iInvH_ID As Integer
    Private iInvH_INV_ID As Integer
    Private sInvH_Flag As String
    Private iInvH_Unit As Integer
    Private iInvH_AlterUnit As Integer
    Private sInvH_Excise As String
    Private sInvH_Cst As String
    Private sInvH_Vat As String
    Private iInvH_CreatedBy As Integer
    Private iInvH_CompID As Integer
    Private iInvH_PerPieces As Integer
    Private dINVH_MRP As Double
    Private dINVH_Retail As Double
    Private dINVH_PreDeterminedPrice As Double
    Private dINVH_EffeFrom As Date
    Private dINVH_EffeTo As DateTime
    Private dINVH_RetailEffeFrom As DateTime
    Private dINVH_RetailEffeTo As DateTime
    Private dINVH_PurchaseEffeFrom As DateTime
    Private dINVH_PurchaseEffeTo As DateTime
    Private dINVH_Others As Double
    Private dPreDetermined As Double
    Private iSL_ID As Integer
    Private iSL_Commodity As Integer
    Private iSL_ItemID As Integer
    Private iSL_OpeningBalanceQty As Integer
    Private iSL_ClosingBalanceQty As Integer
    Private iSL_CompID As Integer
    Private iSL_YearID As Integer
    Private iSL_CrBy As Integer
    Private iSL_UpdatedBy As Integer
    Private iSL_IPAddress As String
    Private iSL_historyId As Integer
    Private dSL_OpeningBalanceAmount As Integer
    Private iSL_Branch As Integer

    Public Property Inv_ID() As Integer
        Get
            Return (iInv_ID)
        End Get
        Set(ByVal Value As Integer)
            iInv_ID = Value
        End Set
    End Property
    Public Property Inv_Code() As String
        Get
            Return (sInv_Code)
        End Get
        Set(ByVal Value As String)
            sInv_Code = Value
        End Set
    End Property
    Public Property Inv_Description() As String
        Get
            Return (sInv_Description)
        End Get
        Set(ByVal Value As String)
            sInv_Description = Value
        End Set
    End Property
    Public Property Inv_Parent() As Integer
        Get
            Return (iInv_Parent)
        End Get
        Set(ByVal Value As Integer)
            iInv_Parent = Value
        End Set
    End Property
    Public Property Inv_Flag() As String
        Get
            Return (sInv_Flag)
        End Get
        Set(ByVal Value As String)
            sInv_Flag = Value
        End Set
    End Property
    Public Property Inv_CompID() As Integer
        Get
            Return (iInv_CompID)
        End Get
        Set(ByVal Value As Integer)
            iInv_CompID = Value
        End Set
    End Property
    Public Property Inv_Size() As String
        Get
            Return (sInv_Size)
        End Get
        Set(ByVal Value As String)
            sInv_Size = Value
        End Set
    End Property

    Public Property Inv_Color() As String
        Get
            Return (sInv_Color)
        End Get
        Set(ByVal Value As String)
            sInv_Color = Value
        End Set
    End Property
    Public Property Inv_Acode() As String
        Get
            Return (sInv_Acode)
        End Get
        Set(ByVal Value As String)
            sInv_Acode = Value
        End Set
    End Property
    Public Property Inv_CreatedBy() As Integer
        Get
            Return (iInv_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iInv_CreatedBy = Value
        End Set
    End Property
    Public Property InvH_ID() As Integer
        Get
            Return (iInvH_ID)
        End Get
        Set(ByVal Value As Integer)
            iInvH_ID = Value
        End Set
    End Property
    Public Property InvH_INV_ID() As Integer
        Get
            Return (iInvH_INV_ID)
        End Get
        Set(ByVal Value As Integer)
            iInvH_INV_ID = Value
        End Set
    End Property
    Public Property InvH_Flag() As String
        Get
            Return (sInvH_Flag)
        End Get
        Set(ByVal Value As String)
            sInvH_Flag = Value
        End Set
    End Property
    Public Property InvH_Unit() As Integer
        Get
            Return (iInvH_Unit)
        End Get
        Set(ByVal Value As Integer)
            iInvH_Unit = Value
        End Set
    End Property
    Public Property InvH_AlterUnit() As Integer
        Get
            Return (iInvH_AlterUnit)
        End Get
        Set(ByVal Value As Integer)
            iInvH_AlterUnit = Value
        End Set
    End Property
    Public Property InvH_Excise() As String
        Get
            Return (sInvH_Excise)
        End Get
        Set(ByVal Value As String)
            sInvH_Excise = Value
        End Set
    End Property

    Public Property InvH_Cst() As String
        Get
            Return (sInvH_Cst)
        End Get
        Set(ByVal Value As String)
            sInvH_Cst = Value
        End Set
    End Property
    Public Property InvH_Vat() As String
        Get
            Return (sInvH_Vat)
        End Get
        Set(ByVal Value As String)
            sInvH_Vat = Value
        End Set
    End Property
    Public Property InvH_CreatedBy() As Integer
        Get
            Return (iInvH_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iInvH_CreatedBy = Value
        End Set
    End Property
    Public Property InvH_CompID() As Integer
        Get
            Return (iInvH_CompID)
        End Get
        Set(ByVal Value As Integer)
            iInvH_CompID = Value
        End Set
    End Property
    Public Property InvH_PerPieces() As Integer
        Get
            Return (iInvH_PerPieces)
        End Get
        Set(ByVal Value As Integer)
            iInvH_PerPieces = Value
        End Set
    End Property
    Public Property INVH_MRP() As Double
        Get
            Return (dINVH_MRP)
        End Get
        Set(ByVal Value As Double)
            dINVH_MRP = Value
        End Set
    End Property
    Public Property INVH_Retail() As Double
        Get
            Return (dINVH_Retail)
        End Get
        Set(ByVal Value As Double)
            dINVH_Retail = Value
        End Set
    End Property
    Public Property INVH_PreDeterminedPrice() As Double
        Get
            Return (dINVH_PreDeterminedPrice)
        End Get
        Set(ByVal Value As Double)
            dINVH_PreDeterminedPrice = Value
        End Set
    End Property
    Public Property INVH_EffeFrom() As Date
        Get
            Return (dINVH_EffeFrom)
        End Get
        Set(ByVal Value As Date)
            dINVH_EffeFrom = Value
        End Set
    End Property
    Public Property INVH_EffeTo() As Date
        Get
            Return (dINVH_EffeTo)
        End Get
        Set(ByVal Value As Date)
            dINVH_EffeTo = Value
        End Set
    End Property
    Public Property INVH_RetailEffeFrom() As DateTime
        Get
            Return (dINVH_RetailEffeFrom)
        End Get
        Set(ByVal Value As DateTime)
            dINVH_RetailEffeFrom = Value
        End Set
    End Property
    Public Property INVH_RetailEffeTo() As DateTime
        Get
            Return (dINVH_RetailEffeTo)
        End Get
        Set(ByVal Value As DateTime)
            dINVH_RetailEffeTo = Value
        End Set
    End Property
    Public Property INVH_PurchaseEffeFrom() As DateTime
        Get
            Return (dINVH_PurchaseEffeFrom)
        End Get
        Set(ByVal Value As DateTime)
            dINVH_PurchaseEffeFrom = Value
        End Set
    End Property
    Public Property INVH_PurchaseEffeTo() As DateTime
        Get
            Return (dINVH_PurchaseEffeTo)
        End Get
        Set(ByVal Value As DateTime)
            dINVH_PurchaseEffeTo = Value
        End Set
    End Property
    Public Property INVH_Others() As Double
        Get
            Return (dINVH_Others)
        End Get
        Set(ByVal Value As Double)
            dINVH_Others = Value
        End Set
    End Property
    Public Property SL_ID() As Integer
        Get
            Return (iSL_ID)
        End Get
        Set(ByVal Value As Integer)
            iSL_ID = Value
        End Set
    End Property
    Public Property SL_Commodity() As Integer
        Get
            Return (iSL_Commodity)
        End Get
        Set(ByVal Value As Integer)
            iSL_Commodity = Value
        End Set
    End Property
    Public Property SL_ItemID() As Integer
        Get
            Return (iSL_ItemID)
        End Get
        Set(ByVal Value As Integer)
            iSL_ItemID = Value
        End Set
    End Property
    Public Property SL_OpeningBalanceQty() As Integer
        Get
            Return (iSL_OpeningBalanceQty)
        End Get
        Set(ByVal Value As Integer)
            iSL_OpeningBalanceQty = Value
        End Set
    End Property

    Public Property SL_OpeningBalanceAmount() As Decimal
        Get
            Return (dSL_OpeningBalanceAmount)
        End Get
        Set(ByVal Value As Decimal)
            dSL_OpeningBalanceAmount = Value
        End Set
    End Property
    Public Property SL_ClosingBalanceQty() As Integer
        Get
            Return (iSL_ClosingBalanceQty)
        End Get
        Set(ByVal Value As Integer)
            iSL_ClosingBalanceQty = Value
        End Set
    End Property
    Public Property SL_CompID() As Integer
        Get
            Return (iSL_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSL_CompID = Value
        End Set
    End Property
    Public Property SL_YearID() As Integer
        Get
            Return (iSL_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSL_YearID = Value
        End Set
    End Property
    Public Property SL_CrBy() As Integer
        Get
            Return (iSL_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iSL_CrBy = Value
        End Set
    End Property
    Public Property SL_UpdatedBy() As Integer
        Get
            Return (iSL_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSL_UpdatedBy = Value
        End Set
    End Property
    Public Property SL_IPAddress() As String
        Get
            Return (iSL_IPAddress)
        End Get
        Set(ByVal Value As String)
            iSL_IPAddress = Value
        End Set
    End Property
    Public Property SL_historyId() As Integer
        Get
            Return (iSL_historyId)
        End Get
        Set(ByVal Value As Integer)
            iSL_historyId = Value
        End Set
    End Property
    Public Property PreDetermined() As Decimal
        Get
            Return (dPreDetermined)
        End Get
        Set(ByVal Value As Decimal)
            dPreDetermined = Value
        End Set
    End Property
    Public Property SL_Branch() As Integer
        Get
            Return (iSL_Branch)
        End Get
        Set(ByVal Value As Integer)
            iSL_Branch = Value
        End Set
    End Property
    Public Function CheckCommidityExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCommidity As String) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            sSql = "Select Inv_ID from Inventory_Master where Inv_Description='" & sCommidity & "' and Inv_Parent=0 and Inv_CompID=" & iCompID & ""
            iId = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDescriptionExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDescription As String) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            sSql = "" : sSql = "Select Inv_ID from Inventory_Master where Inv_Code='" & sDescription & "' and Inv_CompID=" & iCompID & ""
            iId = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckInventoryMasterHistory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            'Select Case* From inventory_Master_History Where InvH_INV_ID ='" & iHistoryID & "' and InvH_CompID=" & iCompID & "
            sSql = "" : sSql = "select InvH_ID from inventory_Master_History where InvH_INV_ID=" & iHistoryID & " and InvH_CompID=" & iCompID & ""
            iId = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUnitofMeasurement(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sUnitofMeasure As String) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            sSql = "Select Mas_Id from Acc_General_master where Mas_Desc ='" & sUnitofMeasure & "' and "
            sSql = sSql & "mas_Master = 1 And Mas_CompID=" & iCompID & " and Mas_Delflag = 'A'"
            iId = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetVatRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sUnitofMeasure As String) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            sSql = "Select Mas_Id from Acc_General_master where Mas_Desc ='" & sUnitofMeasure & "' and "
            sSql = sSql & "mas_Master = 14 And Mas_CompID=" & iCompID & " and Mas_Delflag = 'A'"
            iId = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCSTRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sUnitofMeasure As String) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            sSql = "Select Mas_Id from Acc_General_master where Mas_Desc ='" & sUnitofMeasure & "' and "
            sSql = sSql & "mas_Master = 15 And Mas_CompID=" & iCompID & " and Mas_Delflag = 'A'"
            iId = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetExciseRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sUnitofMeasure As String) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            sSql = "Select Mas_Id from Acc_General_master where Mas_Desc ='" & sUnitofMeasure & "' and "
            sSql = sSql & "mas_Master = 16 And Mas_CompID=" & iCompID & " and Mas_Delflag = 'A'"
            iId = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Shared Function CheckAlternativeUnit(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sAlternative As String) As Integer
    '    Dim sSql As String
    '    Dim dr As OleDb.OleDbDataReader
    '    Try
    '        'Select Mas_Id from Acc_General_master where Mas_Desc ='" & dtUpload.Rows(i)(4).ToString() & "' and Mas_Master=1 and Mas_CompID=" & iCompID & "
    '        sSql = "" : sSql = "Select Mas_Id from Acc_General_master where Mas_Desc ='" & sAlternative & "' and mas_Master = 1 and Mas_CompID=" & iCompID & ""
    '        dr = DBHelper.DBLDataReader(sNameSpace, sSql)
    '        If dr.HasRows = True Then
    '            CheckAlternativeUnit = dr("Mas_Id")
    '        End If
    '        dr.Close()
    '        Return CheckAlternativeUnit
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetPhysicalStock(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParent As Integer, ByVal iExistID As Integer) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            'Select * from Stock_Ledger where SL_Commodity =" & iParent & " and SL_ItemID= " & iExistID & " and SL_CompID=" & iCompID & "
            sSql = "select SL_ID from Stock_Ledger where SL_Commodity=" & iParent & " and SL_ItemID= " & iExistID & " and SL_CompID=" & iCompID & ""
            iId = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParent As Integer, ByVal iExistID As Integer) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            'Select SL_OpeningBalanceQty from Stock_Ledger where SL_Commodity =" & iParent & " and SL_ItemID= " & iExistID & " and SL_CompID=" & iCompID & "
            sSql = "select SL_OpeningBalanceQty from Stock_Ledger where SL_Commodity=" & iParent & " and SL_ItemID= " & iExistID & " and SL_CompID=" & iCompID & ""
            iId = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveInventoryMaster(ByVal sNameSpace As String, ByVal objPhysicalStock As clsPhysicalStockExcelUpload)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInv_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.sInv_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Description", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objPhysicalStock.sInv_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Parent", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInv_Parent
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPhysicalStock.sInv_Flag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInv_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Size", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.sInv_Size
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Color", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.sInv_Color
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_Acode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.sInv_Acode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Inv_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInv_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spInventoryMasterPhysicalUpload", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveInventoryMasterHistory(ByVal sNameSpace As String, ByVal objPhysicalStock As clsPhysicalStockExcelUpload)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInvH_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_INV_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInvH_INV_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPhysicalStock.sInvH_Flag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Unit", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInvH_Unit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_AlterUnit", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInvH_AlterUnit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Excise", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.sInvH_Excise
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Cst", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.InvH_Cst
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Vat", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.sInvH_Vat
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInvH_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInvH_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_PerPieces", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.InvH_PerPieces
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_MRP", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.dINVH_MRP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_Retail", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.dINVH_Retail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_PreDeterminedPrice", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.dINVH_PreDeterminedPrice
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_EffeFrom", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPhysicalStock.dINVH_EffeFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_EffeTo", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPhysicalStock.dINVH_EffeTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_Others", OleDb.OleDbType.Double, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.dINVH_Others
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_RetailEffeFrom", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objPhysicalStock.INVH_RetailEffeFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_RetailEffeTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objPhysicalStock.INVH_RetailEffeTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_PurchaseEffeFrom", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objPhysicalStock.INVH_PurchaseEffeFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_PurchaseEffeTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objPhysicalStock.INVH_PurchaseEffeTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spInventoryMasterHistoryPhysicalUpload", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveTaxDetails(ByVal sNameSpace As String, ByVal objPhysicalStock As clsPhysicalStockExcelUpload)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.InvH_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iMasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.InvH_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPhysicalStock.sInvH_Flag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Excise", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.sInvH_Excise
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Cst", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.InvH_Cst
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Vat", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.sInvH_Vat
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInvH_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iInvH_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_EffeFrom", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPhysicalStock.dINVH_EffeFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_EffeTo", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPhysicalStock.dINVH_EffeTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spInventoryTaxDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveStockLedger(ByVal sNameSpace As String, ByVal objPhysicalStock As clsPhysicalStockExcelUpload)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_Commodity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_Commodity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_ItemID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_ItemID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_OpeningBalanceQty", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_OpeningBalanceQty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_ClosingBalanceQty", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_ClosingBalanceQty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_historyId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_historyId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Purchase_Rate", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.dPreDetermined
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@OpningAmount", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.SL_OpeningBalanceAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SL_Branch", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPhysicalStock.iSL_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDB.ExecuteSPForInsertARR(sNameSpace, "spStockLedger", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Shared Function SavePhysicalStock(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dtUpload As DataTable, ByVal iUserID As Integer, ByVal sPSIPAddress As String) As Array
    '    Dim Arr() As String
    '    Dim sSql As String = ""
    '    Dim dr As OleDb.OleDbDataReader
    '    Dim iParent, iMax As Integer
    '    Dim iUnit As Integer = 0
    '    Dim iAlternative As Integer = 0
    '    Dim sCommidity As String = ""
    '    Dim sVat As String = ""
    '    Dim iExistID As Integer
    '    Dim iExistStockID As Integer
    '    Dim sExcise As String = ""
    '    'Dim sCst As String = ""
    '    Dim iHistoryID As Integer = 0
    '    Dim iPerPiece As Integer = 0
    '    Dim dMRP As Double = "0.0"
    '    Dim dRetail As Double = "0.0"
    '    Dim dPreDetermined As Double = "0.0"
    '    Dim dOthers As Double = "0.0"
    '    Dim iPQuantity As Integer
    '    Dim iOpeningQty As Integer
    '    Dim dEffectiveFrom As Date
    '    Dim dEffectiveTo As Date
    '    Dim sColor As String = ""
    '    Dim sSize As String = ""
    '    Dim sAcode As String = ""
    '    Try
    '        For i = 0 To dtUpload.Rows.Count - 1
    '            'Commodity
    '            If dtUpload.Rows(i)(0).ToString() = "" Then
    '                dtUpload.Rows(i)(0) = sCommidity
    '            Else
    '                sCommidity = dtUpload.Rows(i)(0).ToString()
    '            End If

    '            'PerPiece
    '            If dtUpload.Rows(i)(5).ToString() <> "" Then
    '                iPerPiece = dtUpload.Rows(i)(5).ToString()
    '            Else
    '                iPerPiece = "0"
    '            End If

    '            'VAT
    '            If dtUpload.Rows(i)(6).ToString() <> "" Then
    '                sVat = dtUpload.Rows(i)(6).ToString()
    '            Else
    '                sVat = ""
    '            End If

    '            'Excise
    '            If dtUpload.Rows(i)(7).ToString() <> "" Then
    '                sExcise = dtUpload.Rows(i)(7).ToString()
    '            Else
    '                sExcise = ""
    '            End If

    '            'MRP
    '            If dtUpload.Rows(i)(8).ToString() <> "" Then
    '                dMRP = Convert.ToDouble(dtUpload.Rows(i)(8).ToString())
    '            Else
    '                dMRP = "0.0"
    '            End If

    '            'Retail
    '            If dtUpload.Rows(i)(9).ToString() <> "" Then
    '                dRetail = Convert.ToDouble(dtUpload.Rows(i)(9).ToString())
    '            Else
    '                dRetail = "0.0"
    '            End If

    '            'PreDetermined
    '            If dtUpload.Rows(i)(12).ToString() <> "" Then
    '                dPreDetermined = Convert.ToDouble(dtUpload.Rows(i)(12).ToString())
    '            Else
    '                dPreDetermined = "0.0"
    '            End If
    '            'Others
    '            If dtUpload.Rows(i)(13).ToString() <> "" Then
    '                dOthers = dtUpload.Rows(i)(17).ToString()
    '            Else
    '                dOthers = "0.0"
    '            End If

    '            ''CST
    '            'If dtUpload.Rows(i)(17).ToString() <> "" Then
    '            '    sCst = Convert.ToDouble(dtUpload.Rows(i)(17).ToString())
    '            'Else
    '            '    sCst = "0"
    '            'End If

    '            'Color
    '            If dtUpload.Rows(i)(14).ToString() <> "" Then
    '                sColor = dtUpload.Rows(i)(14).ToString()
    '            Else
    '                sColor = ""
    '            End If
    '            'Size
    '            If dtUpload.Rows(i)(15).ToString() <> "" Then
    '                sSize = dtUpload.Rows(i)(15).ToString()
    '            Else
    '                sSize = ""
    '            End If
    '            'Alternative No/Color Code
    '            If dtUpload.Rows(i)(16).ToString() <> "" Then
    '                sAcode = dtUpload.Rows(i)(16).ToString()
    '            Else
    '                sAcode = ""
    '            End If

    '            ' dPhysical Quantity
    '            If dtUpload.Rows(i)(17).ToString() <> "" Then
    '                iPQuantity = dtUpload.Rows(i)(17).ToString()
    '            Else
    '                iPQuantity = "0"
    '            End If

    '            'Effective From
    '            If dtUpload.Rows(i)(10).ToString() = "" Then
    '                dEffectiveFrom = "01/01/1900"
    '            Else
    '                If dtUpload.Rows(i)(10).ToString().Length > 10 Then
    '                    dEffectiveFrom = DateTime.ParseExact(dtUpload.Rows(i)(10).ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                Else
    '                    dEffectiveFrom = DateTime.ParseExact(dtUpload.Rows(i)(10).ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                End If

    '            End If

    '            'Effective To
    '            If dtUpload.Rows(i)(11).ToString() = "" Then
    '                dEffectiveTo = "01/01/1900"
    '            Else
    '                If dtUpload.Rows(i)(11).ToString().Length > 10 Then
    '                    dEffectiveTo = DateTime.ParseExact(dtUpload.Rows(i)(11).ToString().Substring(0, 10), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                Else
    '                    dEffectiveTo = DateTime.ParseExact(dtUpload.Rows(i)(11).ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
    '                End If

    '            End If

    '            'Inventory Master Commidity
    '            'sSql = "Select * from inventory_master where Inv_Description ='" & dtUpload.Rows(i)(0).ToString() & "' and INV_COmpID=" & iCompID & " and Inv_Parent=0"
    '            'dr = DBHelper.DBLDataReader(sNameSpace, sSql)
    '            'If dr.HasRows = True Then
    '            '    dr.Read()
    '            '    iParent = dr("Inv_ID")
    '            'Else
    '            '    Arr = SaveInventoryMaster(sNameSpace, iCompID, "", dtUpload.Rows(i)(0).ToString(), 0, "", "", "", iUserID, 0)
    '            '    iParent = Arr(1)
    '            'End If
    '            'dr.Close()

    '            ' Inventory Master
    '            'sSql = "" : sSql = "Select * from inventory_master where Inv_Code ='" & dtUpload.Rows(i)(2) & "' and INV_COmpID=" & iCompID & " and Inv_Parent=" & iParent & ""
    '            'dr = DBHelper.DBLDataReader(sNameSpace, sSql)
    '            'If dr.HasRows = True Then
    '            '    iExistID = DBHelper.SQLDBExecScalarInteger(sNameSpace, "Select * from inventory_master where Inv_Description ='" & dtUpload.Rows(i)(1) & "' and INV_COmpID=" & iCompID & " and Inv_Parent=" & iParent & "")
    '            '    Arr = SaveInventoryMaster(sNameSpace, iCompID, dtUpload.Rows(i)(2).ToString(), dtUpload.Rows(i)(1).ToString(), iParent, sSize, sColor, sAcode, iUserID, iExistID)
    '            '    iHistoryID = iExistID
    '            'Else
    '            '    Arr = SaveInventoryMaster(sNameSpace, iCompID, dtUpload.Rows(i)(2).ToString(), dtUpload.Rows(i)(1).ToString(), iParent, sSize, sColor, sAcode, iUserID, 0)
    '            '    iHistoryID = Arr(1)
    '            'End If
    '            'dr.Close()

    '            ''Inventory Master History
    '            'iUnit = DBHelper.SQLDBExecScalar(sNameSpace, "Select Mas_Id from Acc_General_master where Mas_Desc ='" & dtUpload.Rows(i)(3).ToString() & "' and Mas_Master=1 and Mas_CompID=" & iCompID & "")
    '            'iAlternative = DBHelper.SQLDBExecScalar(sNameSpace, "Select Mas_Id from Acc_General_master where Mas_Desc ='" & dtUpload.Rows(i)(4).ToString() & "' and Mas_Master=1 and Mas_CompID=" & iCompID & "")

    '            'sSql = "" : sSql = "Select * from inventory_Master_History where InvH_INV_ID ='" & iHistoryID & "' and InvH_CompID=" & iCompID & ""
    '            'dr = DBHelper.DBLDataReader(sNameSpace, sSql)
    '            'If dr.HasRows = True Then
    '            '    iExistID = DBHelper.SQLDBExecScalarInteger(sNameSpace, "Select * from inventory_Master_History where InvH_INV_ID ='" & iHistoryID & "' and InvH_CompID=" & iCompID & "")
    '            '    Arr = SaveInventoryMasterHistory(sNameSpace, iCompID, iHistoryID, iUnit, iAlternative, sExcise, sVat, iUserID, iPerPiece, dMRP, dRetail, dPreDetermined, dEffectiveFrom, dEffectiveTo, dOthers, iExistID)
    '            '    iHistoryID = iExistID
    '            'Else
    '            '    Arr = SaveInventoryMasterHistory(sNameSpace, iCompID, iHistoryID, iUnit, iAlternative, sExcise, sVat, iUserID, iPerPiece, dMRP, dRetail, dPreDetermined, dEffectiveFrom, dEffectiveTo, dOthers, 0)
    '            '    iHistoryID = Arr(1)
    '            'End If

    '            ''Physical Stock Excel Upload
    '            'sSql = "Select * from inventory_master where Inv_Description ='" & dtUpload.Rows(i)(0).ToString() & "' and INV_COmpID=" & iCompID & " and Inv_Parent=0"
    '            'dr = DBHelper.DBLDataReader(sNameSpace, sSql)
    '            'If dr.HasRows = True Then
    '            '    dr.Read()
    '            '    iParent = dr("Inv_ID")
    '            'End If
    '            'dr.Close()
    '            'sSql = "" : sSql = "Select * from inventory_master where Inv_Code ='" & dtUpload.Rows(i)(2) & "' and INV_COmpID=" & iCompID & " and Inv_Parent=" & iParent & ""
    '            'dr = DBHelper.DBLDataReader(sNameSpace, sSql)
    '            'If dr.HasRows = True Then
    '            '    dr.Read()
    '            '    iExistID = dr("Inv_ID")
    '            '    iHistoryID = iExistID
    '            'End If
    '            'dr.Close()
    '            'sSql = "" : sSql = "Select * from Stock_Ledger where SL_Commodity =" & iParent & " and SL_ItemID= " & iExistID & " and SL_CompID=" & iCompID & ""
    '            'dr = DBHelper.DBLDataReader(sNameSpace, sSql)
    '            'If dr.HasRows = True Then
    '            '    dr.Read()
    '            '    iExistStockID = DBHelper.SQLDBExecScalarInteger(sNameSpace, "Select * from Stock_Ledger where SL_Commodity =" & iParent & " and SL_ItemID= " & iExistID & " and SL_CompID=" & iCompID & "")
    '            '    iOpeningQty = DBHelper.SQLDBExecScalarInteger(sNameSpace, "Select SL_OpeningBalanceQty from Stock_Ledger where SL_Commodity =" & iParent & " and SL_ItemID= " & iExistID & " and SL_CompID=" & iCompID & "")
    '            '    iOpeningQty = iOpeningQty + iPQuantity
    '            '    Arr = SaveStockLedger(sNameSpace, iCompID, iParent, iExistID, iOpeningQty, 0, sPSIPAddress, iHistoryID, iUserID, iExistStockID)
    '            '    iHistoryID = iExistID
    '            'Else
    '            '    Arr = SaveStockLedger(sNameSpace, iCompID, iParent, iExistID, iPQuantity, 0, sPSIPAddress, iHistoryID, iUserID, 0)
    '            '    iHistoryID = Arr(1)
    '            'End If
    '            'dr.Close()
    '        Next
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function CheckPhysicalStockUpload(ByVal sNameSpace As String, ByVal iCompID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * From Stock_Ledger Where SL_OrderID=0 And SL_CompID=" & iCompID & " "
            CheckPhysicalStockUpload = objDB.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckPhysicalStockUpload
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
