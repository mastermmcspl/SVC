Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsInvenotryDetails
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iINVH_ID As Integer
    Private iINVH_INV_ID As Integer
    Private iINVH_Unit As Integer
    Private iINVH_AlterUnit As Integer
    Private sINVH_Flag As String
    Private iINVH_CreatedBy As Integer
    Private dINVH_CreatedOn As Date
    Private iINVH_ApporvedBy As Integer
    Private dINVH_ApprovedOn As Date
    Private iINVH_DeletedBy As Integer
    Private dINVH_DeletedOn As Date
    Private iINVH_RecallBy As Integer
    Private dINVH_RecalledOn As Date
    Private iINVH_CompID As Integer
    Private iINVH_PerPieces As Integer
    Private dINVH_MRP As Double
    Private dINVH_Retail As Double
    Private iINVH_CategoryID As Integer
    Private dINVH_PreDeterminedPrice As Double
    Private dINVH_EffeFrom As Date
    Private dINVH_EffeTo As Date
    Private dINVH_RetailEffeFrom As DateTime
    Private dINVH_RetailEffeTo As DateTime
    Private dINVH_PurchaseEffeFrom As DateTime
    Private dINVH_PurchaseEffeTo As DateTime
    Private sInvH_Operation As String
    Private sInvH_IPAddress As String


    Private iIMT_ID As Integer
    Private iIMT_MasterID As Integer
    Private iIMT_VAT As Integer
    Private dIMT_EffectiveVATFrom As DateTime
    Private dIMT_EffectiveVATTo As DateTime
    Private iIMT_CST As Integer
    Private dIMT_EffectiveCSTFrom As DateTime
    Private dIMT_EffectiveCSTTo As DateTime
    Private iIMT_Excise As Integer
    Private dIMT_EffectiveExciseFrom As DateTime
    Private dIMT_EffectiveExciseTo As DateTime
    Private sIMT_Status As String
    Private iIMT_CompID As Integer
    Private iIMT_CreatedBy As Integer
    Private dIMT_CreatedOn As DateTime
    Private sIMT_IPAddress As String
    Private sIMT_Operation As String

    Public Property IMT_ID() As Integer
        Get
            Return (iIMT_ID)
        End Get
        Set(ByVal Value As Integer)
            iIMT_ID = Value
        End Set
    End Property
    Public Property IMT_MasterID() As Integer
        Get
            Return (iIMT_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iIMT_MasterID = Value
        End Set
    End Property
    Public Property IMT_VAT() As Integer
        Get
            Return (iIMT_VAT)
        End Get
        Set(ByVal Value As Integer)
            iIMT_VAT = Value
        End Set
    End Property
    Public Property IMT_EffectiveVATFrom() As DateTime
        Get
            Return (dIMT_EffectiveVATFrom)
        End Get
        Set(ByVal Value As DateTime)
            dIMT_EffectiveVATFrom = Value
        End Set
    End Property
    Public Property IMT_EffectiveVATTo() As DateTime
        Get
            Return (dIMT_EffectiveVATTo)
        End Get
        Set(ByVal Value As DateTime)
            dIMT_EffectiveVATTo = Value
        End Set
    End Property
    Public Property IMT_CST() As Integer
        Get
            Return (iIMT_CST)
        End Get
        Set(ByVal Value As Integer)
            iIMT_CST = Value
        End Set
    End Property
    Public Property IMT_EffectiveCSTFrom() As DateTime
        Get
            Return (dIMT_EffectiveCSTFrom)
        End Get
        Set(ByVal Value As DateTime)
            dIMT_EffectiveCSTFrom = Value
        End Set
    End Property
    Public Property IMT_EffectiveCSTTo() As DateTime
        Get
            Return (dIMT_EffectiveCSTTo)
        End Get
        Set(ByVal Value As DateTime)
            dIMT_EffectiveCSTTo = Value
        End Set
    End Property
    Public Property IMT_Excise() As Integer
        Get
            Return (iIMT_Excise)
        End Get
        Set(ByVal Value As Integer)
            iIMT_Excise = Value
        End Set
    End Property
    Public Property IMT_EffectiveExciseFrom() As DateTime
        Get
            Return (dIMT_EffectiveExciseFrom)
        End Get
        Set(ByVal Value As DateTime)
            dIMT_EffectiveExciseFrom = Value
        End Set
    End Property
    Public Property IMT_EffectiveExciseTo() As DateTime
        Get
            Return (dIMT_EffectiveExciseTo)
        End Get
        Set(ByVal Value As DateTime)
            dIMT_EffectiveExciseTo = Value
        End Set
    End Property
    Public Property IMT_Status() As String
        Get
            Return (sIMT_Status)
        End Get
        Set(ByVal Value As String)
            sIMT_Status = Value
        End Set
    End Property
    Public Property IMT_CompID() As Integer
        Get
            Return (iIMT_CompID)
        End Get
        Set(ByVal Value As Integer)
            iIMT_CompID = Value
        End Set
    End Property
    Public Property IMT_CreatedBy() As Integer
        Get
            Return (iIMT_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iIMT_CreatedBy = Value
        End Set
    End Property
    Public Property IMT_CreatedOn() As DateTime
        Get
            Return (dIMT_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dIMT_CreatedOn = Value
        End Set
    End Property
    Public Property IMT_IPAddress() As String
        Get
            Return (sIMT_IPAddress)
        End Get
        Set(ByVal Value As String)
            sIMT_IPAddress = Value
        End Set
    End Property
    Public Property IMT_Operation() As String
        Get
            Return (sIMT_Operation)
        End Get
        Set(ByVal Value As String)
            sIMT_Operation = Value
        End Set
    End Property



    Public Property INVH_ID() As Integer
        Get
            Return (iINVH_ID)
        End Get
        Set(ByVal Value As Integer)
            iINVH_ID = Value
        End Set
    End Property
    Public Property INVH_INV_ID() As Integer
        Get
            Return (iINVH_INV_ID)
        End Get
        Set(ByVal Value As Integer)
            iINVH_INV_ID = Value
        End Set
    End Property
    Public Property INVH_Flag() As String
        Get
            Return (sINVH_Flag)
        End Get
        Set(ByVal Value As String)
            sINVH_Flag = Value
        End Set
    End Property
    Public Property INVH_Unit() As Integer
        Get
            Return (iINVH_Unit)
        End Get
        Set(ByVal Value As Integer)
            iINVH_Unit = Value
        End Set
    End Property
    Public Property INVH_AlterUnit() As Integer
        Get
            Return (iINVH_AlterUnit)
        End Get
        Set(ByVal Value As Integer)
            iINVH_AlterUnit = Value
        End Set
    End Property
    Public Property INVH_CreatedBy() As Integer
        Get
            Return (iINVH_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iINVH_CreatedBy = Value
        End Set
    End Property
    Public Property INVH_CreatedOn() As DateTime
        Get
            Return (dINVH_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dINVH_CreatedOn = Value
        End Set
    End Property
    Public Property INVH_ApporvedBy() As Integer
        Get
            Return (iINVH_ApporvedBy)
        End Get
        Set(ByVal Value As Integer)
            iINVH_ApporvedBy = Value
        End Set
    End Property
    Public Property INVH_ApprovedOn() As DateTime
        Get
            Return (dINVH_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dINVH_ApprovedOn = Value
        End Set
    End Property
    Public Property INVH_DeletedBy() As Integer
        Get
            Return (iINVH_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iINVH_DeletedBy = Value
        End Set
    End Property
    Public Property INVH_DeletedOn() As DateTime
        Get
            Return (dINVH_DeletedOn)
        End Get
        Set(ByVal Value As DateTime)
            dINVH_DeletedOn = Value
        End Set
    End Property
    Public Property INVH_RecallBy() As Integer
        Get
            Return (iINVH_RecallBy)
        End Get
        Set(ByVal Value As Integer)
            iINVH_RecallBy = Value
        End Set
    End Property
    Public Property INVH_RecalledOn() As DateTime
        Get
            Return (dINVH_RecalledOn)
        End Get
        Set(ByVal Value As DateTime)
            dINVH_RecalledOn = Value
        End Set
    End Property
    Public Property INVH_CompID() As Integer
        Get
            Return (iINVH_CompID)
        End Get
        Set(ByVal Value As Integer)
            iINVH_CompID = Value
        End Set
    End Property
    Public Property INVH_PerPieces() As Integer
        Get
            Return (iINVH_PerPieces)
        End Get
        Set(ByVal Value As Integer)
            iINVH_PerPieces = Value
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
    Public Property INVH_CategoryID() As Integer
        Get
            Return (iINVH_CategoryID)
        End Get
        Set(ByVal Value As Integer)
            iINVH_CategoryID = Value
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
    Public Property INVH_EffeFrom() As DateTime
        Get
            Return (dINVH_EffeFrom)
        End Get
        Set(ByVal Value As DateTime)
            dINVH_EffeFrom = Value
        End Set
    End Property
    Public Property INVH_EffeTo() As DateTime
        Get
            Return (dINVH_EffeTo)
        End Get
        Set(ByVal Value As DateTime)
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
    Public Property InvH_Operation() As String
        Get
            Return (sInvH_Operation)
        End Get
        Set(ByVal Value As String)
            sInvH_Operation = Value
        End Set
    End Property
    Public Property InvH_IPAddress() As String
        Get
            Return (sInvH_IPAddress)
        End Get
        Set(ByVal Value As String)
            sInvH_IPAddress = Value
        End Set
    End Property

    Public Function LoadCommodity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select INV_ID,INV_Description from Inventory_Master where INV_Parent=0 and INV_Flag='X' and INV_CompID=" & iCompID & " order by INV_Description"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadItem(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParentID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            If iParentID > 0 Then
                sSql = "Select INV_ID,INV_Code from Inventory_Master where INV_Parent=" & iParentID & " and INV_Flag='X' and INV_CompID=" & iCompID & " order by INV_Description"
                Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select INV_ID,INV_Code from Inventory_Master where INV_Parent<>0 and INV_Flag='X' and INV_CompID=" & iCompID & " order by INV_Description"
                Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVAT(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_ID,Mas_Desc from Acc_General_Master where Mas_Master=14 and Mas_DelFlag='A' and Mas_CompID=" & iCompID & " order by Mas_Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCST(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_ID,Mas_Desc from Acc_General_Master where Mas_Master=15 and Mas_DelFlag='A' and Mas_CompID=" & iCompID & " order by Mas_Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_ID,Mas_Desc from Acc_General_Master where Mas_Master=23 and Mas_DelFlag='A' and Mas_CompID=" & iCompID & " order by Mas_Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExciseDuty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_ID,Mas_Desc from Acc_General_Master where Mas_Master=16 and Mas_DelFlag='A' and Mas_CompID=" & iCompID & " order by Mas_Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUnitOfMeasurement(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master =1 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function CreateHistory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal objInv As clsInvenotryDetails, ByVal iInvID As Integer, ByVal iIPAddress As String, ByVal sSaveOrUpdate As String)
    '    Dim sSql As String = ""
    '    Dim iMaxID As Integer
    '    Try
    '        iMaxID = iInvID
    '        If sSaveOrUpdate = "Save" Then
    '            iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select isnull(max(INVH_ID)+1,1) from Inventory_Master_History")
    '            sSql = "" : sSql = "Insert Into Inventory_Master_History(InvH_ID,INVH_INV_ID,InvH_Flag,InvH_Unit,InvH_AlterUnit,InvH_Vat,InvH_Excise,"
    '            sSql = sSql & "InvH_CreatedBy,InvH_CreatedOn,InvH_CompID,InvH_PerPieces,INVH_MRP,INVH_Retail,INVH_PreDeterminedPrice,INVH_EffeFrom,INVH_EffeTo,InvH_Cst,INVH_Operation,INVH_IPAddress,INVH_CategoryID,INVH_RetailEffeFrom,INVH_RetailEffeTo,INVH_PurchaseEffeFrom,INVH_PurchaseEffeTo)"
    '            sSql = sSql & "Values(" & iMaxID & "," & objInv.iInv_ID & ",'" & objInv.sInv_Flag & "'," & objInv.iInv_Unit & "," & objInv.iInv_AlterUnit & ","
    '            sSql = sSql & "" & objInv.sInv_Vat & ",'" & objInv.sInv_Excise & "'," & objInv.iInv_CreatedBy & ","
    '            sSql = sSql & "GetDate()," & iCompID & "," & objInv.iInv_PerPieces & "," & objInv.dInv_MRP & "," & objInv.dInv_Retail & ","
    '            sSql = sSql & "" & objInv.dInv_PreDeterminedPrice & "," & objGen.FormatDtForRDBMS(objInv.dInv_EffeFrom, "I") & ","
    '            sSql = sSql & "" & objGen.FormatDtForRDBMS(objInv.dInv_EffeTo, "I") & ",'" & objInv.sInv_Cst & "','C','" & iIPAddress & "','" & objInv.iInv_CategoryID & "'," & objGen.FormatDtForRDBMS(objInv.dINVH_RetailEffeFrom, "I") & "," & objGen.FormatDtForRDBMS(objInv.dINVH_RetailEffeTo, "I") & "," & objGen.FormatDtForRDBMS(objInv.dINVH_PurchaseEffeFrom, "I") & "," & objGen.FormatDtForRDBMS(objInv.dINVH_PurchaseEffeTo, "I") & ")"
    '        Else
    '            sSql = "" : sSql = "Update Inventory_Master_History set InvH_Unit = " & objInv.iInv_Unit & ",InvH_AlterUnit= " & objInv.iInv_AlterUnit & ","
    '            sSql = sSql & "InvH_Vat = " & objInv.sInv_Vat & ",InvH_Cst = '" & objInv.sInv_Cst & "',InvH_Excise='" & objInv.sInv_Excise & "',InvH_PerPieces  =" & objInv.iInv_PerPieces & ","
    '            sSql = sSql & "INVH_MRP=" & objInv.dInv_MRP & ",INVH_Retail=" & objInv.dInv_Retail & ",INVH_PreDeterminedPrice=" & objInv.dInv_PreDeterminedPrice & ","
    '            sSql = sSql & "INVH_EffeFrom=" & objGen.FormatDtForRDBMS(objInv.dInv_EffeFrom, "I") & ",INVH_EffeTo=" & objGen.FormatDtForRDBMS(objInv.dInv_EffeTo, "I") & ","
    '            sSql = sSql & "INVH_Operation='U',INVH_IPAddress='" & iIPAddress & "',INVH_CategoryID='" & objInv.iInv_CategoryID & "',INVH_RetailEffeFrom=" & objGen.FormatDtForRDBMS(objInv.dINVH_RetailEffeFrom, "I") & ",INVH_RetailEffeTo=" & objGen.FormatDtForRDBMS(objInv.dINVH_RetailEffeTo, "I") & ","
    '            sSql = sSql & "INVH_PurchaseEffeFrom=" & objGen.FormatDtForRDBMS(objInv.dINVH_PurchaseEffeFrom, "I") & ",INVH_PurchaseEffeTo=" & objGen.FormatDtForRDBMS(objInv.dINVH_PurchaseEffeTo, "I") & " Where InvH_ID = " & iInvID & " and InvH_CompID =" & iCompID & ""
    '        End If
    '        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
    '        Return iMaxID
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function SaveInventoryDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objinv As clsInvenotryDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.INVH_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_INV_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.INVH_INV_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objinv.INVH_Flag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Unit", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.INVH_Unit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_AlterUnit", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.INVH_AlterUnit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.INVH_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.INVH_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.INVH_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_PerPieces", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.INVH_PerPieces
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_MRP", OleDb.OleDbType.Decimal)
            ObjParam(iParamCount).Value = objinv.INVH_MRP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_Retail", OleDb.OleDbType.Decimal)
            ObjParam(iParamCount).Value = objinv.INVH_Retail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_PreDeterminedPrice", OleDb.OleDbType.Decimal)
            ObjParam(iParamCount).Value = objinv.INVH_PreDeterminedPrice
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_EffeFrom", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.INVH_EffeFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_EffeTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.INVH_EffeTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objinv.InvH_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@InvH_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objinv.InvH_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_CategoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.INVH_CategoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_RetailEffeFrom", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.INVH_RetailEffeFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_RetailEffeTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.INVH_RetailEffeTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_PurchaseEffeFrom", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.INVH_PurchaseEffeFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@INVH_PurchaseEffeTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.INVH_PurchaseEffeTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spInventory_Master_History", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindHistory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Dim bCheck As Boolean
        Try
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("MRP")
            dtTab.Columns.Add("MRPEffeFromDate")
            dtTab.Columns.Add("MRPEffeToDate")
            dtTab.Columns.Add("Retail")
            dtTab.Columns.Add("RetailEffeFromDate")
            dtTab.Columns.Add("RetailEffeToDate")
            dtTab.Columns.Add("PRate")
            dtTab.Columns.Add("PurchaseEffeFromDate")
            dtTab.Columns.Add("PurchaseEffeToDate")
            dtTab.Columns.Add("AlterUnit")
            dtTab.Columns.Add("UnitOfMsrmnt")
            dtTab.Columns.Add("NumOfUnits")
            dtTab.Columns.Add("Type")
            dtTab.Columns.Add("Status")

            sSql = "Select * from Inventory_Master_History Where INVH_INV_ID=" & iItemID & " And INVH_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("ID") = dt.Rows(i)("INVH_ID")

                If (dt.Rows(i)("INVH_MRP").ToString() = "") Then
                    dr("MRP") = 0
                Else
                    dr("MRP") = dt.Rows(i)("INVH_MRP")
                End If

                If IsDBNull(dt.Rows(i)("INVH_EffeFrom")) = False Then
                    If (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_EffeFrom"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_EffeFrom"), "D").ToString() = "01-01-1900") Then
                        dr("MRPEffeFromDate") = ""
                    Else
                        dr("MRPEffeFromDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_EffeFrom"), "D")
                    End If
                Else
                    dr("MRPEffeFromDate") = ""
                End If

                If IsDBNull(dt.Rows(i)("INVH_EffeTo")) = False Then
                    If (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_EffeTo"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_EffeTo"), "D").ToString() = "01-01-1900") Then
                        dr("MRPEffeToDate") = ""
                    Else
                        dr("MRPEffeToDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_EffeTo"), "D")
                    End If
                Else
                    dr("MRPEffeToDate") = ""
                End If


                If (dt.Rows(i)("INVH_Retail").ToString() = "") Then
                    dr("Retail") = 0
                Else
                    dr("Retail") = dt.Rows(i)("INVH_Retail")
                End If
                If IsDBNull(dt.Rows(i)("INVH_RetailEffeFrom")) = False Then
                    If (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_RetailEffeFrom"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_RetailEffeFrom"), "D").ToString() = "01-01-1900") Then
                        dr("RetailEffeFromDate") = ""
                    Else
                        dr("RetailEffeFromDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_RetailEffeFrom"), "D")
                    End If
                Else
                    dr("RetailEffeFromDate") = ""
                End If
                If IsDBNull(dt.Rows(i)("INVH_RetailEffeTo")) = False Then
                    If (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_RetailEffeTo"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_RetailEffeTo"), "D").ToString() = "01-01-1900") Then
                        dr("RetailEffeToDate") = ""
                    Else
                        dr("RetailEffeToDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_RetailEffeTo"), "D")
                    End If
                Else
                    dr("RetailEffeToDate") = ""
                End If

                If (dt.Rows(i)("INVH_PreDeterminedPrice").ToString() = "") Then
                    dr("PRate") = 0
                Else
                    dr("PRate") = dt.Rows(i)("INVH_PreDeterminedPrice")
                End If
                If IsDBNull(dt.Rows(i)("INVH_PurchaseEffeFrom")) = False Then
                    If (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_PurchaseEffeFrom"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_PurchaseEffeFrom"), "D").ToString() = "01-01-1900") Then
                        dr("PurchaseEffeFromDate") = ""
                    Else
                        dr("PurchaseEffeFromDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_PurchaseEffeFrom"), "D")
                    End If
                Else
                    dr("PurchaseEffeFromDate") = ""
                End If
                If IsDBNull(dt.Rows(i)("INVH_PurchaseEffeTo")) = False Then
                    If (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_PurchaseEffeTo"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_PurchaseEffeTo"), "D").ToString() = "01-01-1900") Then
                        dr("PurchaseEffeToDate") = ""
                    Else
                        dr("PurchaseEffeToDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("INVH_PurchaseEffeTo"), "D")
                    End If
                Else
                    dr("PurchaseEffeToDate") = ""
                End If

                dr("AlterUnit") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_desc from acc_general_master where Mas_id=" & dt.Rows(i)("InvH_AlterUnit") & "")
                dr("UnitOfMsrmnt") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_desc from acc_general_master where Mas_id=" & dt.Rows(i)("InvH_Unit") & "")
                dr("NumOfUnits") = dt.Rows(i)("InvH_PerPieces")

                If IsDBNull(dt.Rows(i)("INVH_CategoryID")) = False Then
                    dr("Type") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_desc from acc_general_master where Mas_id=" & dt.Rows(i)("INVH_CategoryID") & "")
                Else
                    dr("Type") = 0
                End If

                'bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Inventory_Master_TAXDetails Where IMT_MasterID=" & dt.Rows(i)("INVH_ID") & " And IMT_CompID=" & iCompID & " ")
                'If bCheck = True Then
                '    dr("Status") = "TAX Details Created."
                'Else
                '    dr("Status") = "TAX Details Not Created."
                'End If

                dr("Status") = objDBL.SQLGetDescription(sNameSpace, "Select MAs_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("InvH_CategoryID") & " And MAs_CompID=" & iCompID & " ")

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindTaxDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHistoryID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("INVHID")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("VAT")
            dtTab.Columns.Add("VATEffeFromDate")
            dtTab.Columns.Add("VATEffeToDate")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("CSTEffeFromDate")
            dtTab.Columns.Add("CSTEffeToDate")
            dtTab.Columns.Add("Excise")
            dtTab.Columns.Add("ExciseEffeFromDate")
            dtTab.Columns.Add("ExciseEffeToDate")

            sSql = "Select * from Inventory_Master_TaxDetails Where IMT_MasterID=" & iHistoryID & " And IMT_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("INVHID") = dt.Rows(i)("IMT_MasterID")
                dr("ID") = dt.Rows(i)("IMT_ID")
                If (dt.Rows(i)("IMT_VAT").ToString() = "") Then
                    dr("VAT") = 0
                Else
                    dr("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("IMT_VAT") & " And Mas_Master=14 And MAs_DelFlag='A' And MAS_CompID=" & iCompID & " ")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATFrom"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATFrom"), "D").ToString() = "01-01-1900") Then
                    dr("VATEffeFromDate") = ""
                Else
                    dr("VATEffeFromDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATFrom"), "D")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATTo"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATTo"), "D").ToString() = "01-01-1900") Then
                    dr("VATEffeToDate") = ""
                Else
                    dr("VATEffeToDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveVATTo"), "D")
                End If

                If (dt.Rows(i)("IMT_CST").ToString() = "") Then
                    dr("CST") = 0
                Else
                    dr("CST") = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From  Acc_General_Master Where MAS_ID=" & dt.Rows(i)("IMT_CST") & " And Mas_Master=15 And MAs_DelFlag='A' And MAS_CompID=" & iCompID & " ")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTFrom"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTFrom"), "D").ToString() = "01-01-1900") Then
                    dr("CSTEffeFromDate") = ""
                Else
                    dr("CSTEffeFromDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTFrom"), "D")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTTo"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTTo"), "D").ToString() = "01-01-1900") Then
                    dr("CSTEffeToDate") = ""
                Else
                    dr("CSTEffeToDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveCSTTo"), "D")
                End If

                If (dt.Rows(i)("IMT_Excise").ToString() = "") Then
                    dr("Excise") = 0
                Else
                    dr("Excise") = objDBL.SQLGetDescription(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & dt.Rows(i)("IMT_Excise") & " And Mas_Master=16 And MAs_DelFlag='A' And MAS_CompID=" & iCompID & " ")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseFrom"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseFrom"), "D").ToString() = "01-01-1900") Then
                    dr("ExciseEffeFromDate") = ""
                Else
                    dr("ExciseEffeFromDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseFrom"), "D")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseTo"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseTo"), "D").ToString() = "01-01-1900") Then
                    dr("ExciseEffeToDate") = ""
                Else
                    dr("ExciseEffeToDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("IMT_EffectiveExciseTo"), "D")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iINVHID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            If iItemID > 0 And iINVHID = 0 Then
                sSql = "Select top 1 * from Inventory_Master_History where INVH_INV_ID=" & iItemID & " and INVH_CompID=" & iCompID & ""
            ElseIf iItemID > 0 And iINVHID > 0 Then
                sSql = "Select * from Inventory_Master_History where INVH_ID=" & iINVHID & " and INVH_INV_ID = " & iItemID & " and INVH_CompID=" & iCompID & ""
            ElseIf iItemID = 0 And iINVHID > 0 Then
                sSql = "Select * from Inventory_Master_History where INVH_ID=" & iINVHID & " and INVH_CompID=" & iCompID & ""
            Else
                sSql = "Select * from Inventory_Master_History where INVH_CompID=" & iCompID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetInventoryMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParent As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Inventory_Master where Inv_ID =" & iParent & " and Inv_CompID =" & iCompID & ""
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetPath(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal lParent As Integer)
    '    Dim myDataSet As DataSet
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "select * from Inventory_Master where Inv_ID =" & lParent & " and Inv_Flag= 'X' and Inv_CompID =" & iCompID & ""
    '        myDataSet = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
    '        If myDataSet.Tables(0).Rows.Count <> 0 Then
    '            objInv.iInv_Parent = myDataSet.Tables(0).Rows(0).Item("Inv_Parent")
    '            objInv.Inv_Description = myDataSet.Tables(0).Rows(0).Item("Inv_Description")
    '        End If
    '        Return objInv
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        myDataSet = Nothing
    '    End Try
    'End Function
    Public Function GetPath(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal lParent As Integer) As DataTable
        Dim myDataSet As DataTable
        Dim sSql As String = ""
        Try
            sSql = "select * from Inventory_Master where Inv_ID =" & lParent & " and Inv_Flag= 'X' and Inv_CompID =" & iCompID & ""
            myDataSet = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            'If myDataSet.Tables(0).Rows.Count <> 0 Then
            '    objInv.iInv_Parent = myDataSet.Tables(0).Rows(0).Item("Inv_Parent")
            '    objInv.Inv_Description = myDataSet.Tables(0).Rows(0).Item("Inv_Description")
            'End If
            Return myDataSet
        Catch ex As Exception
            Throw
        Finally
            myDataSet = Nothing
        End Try
    End Function
    Public Function GetBrandID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select INV_Parent From Inventory_Master Where INV_ID=" & iItemID & " And INV_Parent<>0 And INV_CompID=" & iCompID & " "
            GetBrandID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return GetBrandID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExistingInventoryDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Inventory_Master_History where InvH_ID = " & iItemID & " and InvH_compID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteInventoryValues(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From Inventory_Master_History Where INVH_CompID=" & iCompID & " And InvH_ID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckMasterINUSE(ByVal sNameSpace As String, ByVal IcompID As Integer, ByVal Mtype As Integer, ByVal ItemID As Integer) As Boolean
        Dim sSql As String = ""
        Dim flag As String = True
        Try
            ''Customer Master
            'sSql = "select CUST_CITY from MST_CUSTOMER_MASTER where  "

            If IsDBNull(Mtype) = False Then
                Select Case Mtype
                    Case 1 '
                        flag = ChekInventoryItem(sNameSpace, IcompID, Mtype, ItemID)
                            'CheckUnitOfMesureMent(sNameSpace, IcompID, Mtype, ItemID)
                    Case 2
                       ' flag = CheckIndustryType(sNameSpace, IcompID, Mtype, ItemID)
                    Case 3
                        'iQnt4 = iQnt4 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size4") = iQnt4
                        'iQnt = iQnt + iQnt4
                    Case 4
                        'iQnt4 = iQnt4 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size4") = iQnt4
                        'iQnt = iQnt + iQnt4
                    Case 5
                        'iQnt5 = iQnt5 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size5") = iQnt5
                        'iQnt = iQnt + iQnt5

                    Case 6
                        'iQnt5 = iQnt5 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size5") = iQnt5
                        'iQnt = iQnt + iQnt5

                    Case 7
                        'iQnt6 = iQnt6 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size6") = iQnt6
                        'iQnt = iQnt + iQnt6

                    Case 8
                        'iQnt6 = iQnt6 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size6") = iQnt6
                        'iQnt = iQnt + iQnt6

                    Case 9
                        'iQnt7 = iQnt7 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size7") = iQnt7
                        'iQnt = iQnt + iQnt7

                    Case 10
                        'iQnt7 = iQnt7 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size7") = iQnt7
                        'iQnt = iQnt + iQnt7

                    Case 11
                        'iQnt8 = iQnt8 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size8") = iQnt8
                        'iQnt = iQnt + iQnt8

                    Case 12
                        'iQnt8 = iQnt8 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size8") = iQnt8
                        'iQnt = iQnt + iQnt8

                    Case 13
                        'iQnt9 = iQnt9 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size9") = iQnt9
                        'iQnt = iQnt + iQnt9

                    Case 14
                        'iQnt9 = iQnt9 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size9") = iQnt9
                        'iQnt = iQnt + iQnt9

                    Case 15
                        'iQnt10 = iQnt10 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size10") = iQnt10
                        'iQnt = iQnt + iQnt10

                    Case 16
                        'iQnt10 = iQnt10 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size10") = iQnt10
                        'iQnt = iQnt + iQnt10

                    Case 17
                        'iQnt11 = iQnt11 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size11") = iQnt11
                        'iQnt = iQnt + iQnt11

                    Case 18
                        'iQnt11 = iQnt11 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size11") = iQnt11
                        'iQnt = iQnt + iQnt11

                    Case 19
                        'iQnt12 = iQnt12 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size12") = iQnt12
                        'iQnt = iQnt + iQnt12

                    Case 20
                        'iQnt12 = iQnt12 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_Size12") = iQnt12
                        'iQnt = iQnt + iQnt12
                    Case 21
                        'iQnt0 = iQnt0 + dtDetails.Rows(i)("POD_Quantity").ToString()
                        'dRow("Inv_SizeO") = iQnt0
                        'iQnt = iQnt + iQnt0

                End Select
            End If
            Return flag
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ChekInventoryItem(ByVal sNameSpace As String, ByVal IcompID As Integer, ByVal Mtype As Integer, ByVal historyID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            'Purchase_order_master
            sSql = "Select POD_HistoryID From Purchase_order_details Where POD_CompID=" & IcompID & " and POD_HistoryID=" & historyID & ""
            If objDBL.SQLCheckForRecord(sNameSpace, sSql) = True Then
                Return True
            End If
            'Stock_ledger
            sSql = "Select SL_HistoryID from Stock_ledger Where SL_CompID=" & IcompID & " and SL_HistoryID=" & historyID & ""
            If objDBL.SQLCheckForRecord(sNameSpace, sSql) = True Then
                Return True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTAXDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objinv As clsInvenotryDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.IMT_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.IMT_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_VAT", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.IMT_VAT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_EffectiveVATFrom", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.IMT_EffectiveVATFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_EffectiveVATTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.IMT_EffectiveVATTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_CST", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.IMT_CST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_EffectiveCSTFrom", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.IMT_EffectiveCSTFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_EffectiveCSTTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.IMT_EffectiveCSTTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_Excise", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.IMT_Excise
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_EffectiveExciseFrom", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.IMT_EffectiveExciseFrom
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_EffectiveExciseTo", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.IMT_EffectiveExciseTo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_Status ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objinv.IMT_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.IMT_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objinv.IMT_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objinv.IMT_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objinv.IMT_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IMT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objinv.IMT_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spInventory_Master_TaxDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteTaxDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From Inventory_Master_TaxDetails Where IMT_CompID=" & iCompID & " And IMT_ID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Function CheckTaxINUSE() As DataTable
    '    Dim sSql As String
    '    Try
    '        sSql = "Select * From "
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function LoadVATUsingDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal Sdate As String, ByVal HistoriID As String) As DataTable
        Dim sSql As String = ""
        Try
            '   sSql = "Select Mas_ID,Mas_Desc from Acc_General_Master where Mas_Master=14 and Mas_DelFlag='A' and Mas_CompID=" & iCompID & " order by Mas_Desc"
            sSql = "Select MAS_ID,MAS_Desc From Acc_General_Master Where MAS_ID In (Select IMT_VAT from Inventory_Master_TaxDetails A where A.IMT_MasterID=" & HistoriID & " And A.IMT_EffectiveVATFRom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(Sdate, "CT") & "')) "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCSTusingDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal Sdate As String, ByVal HistoriID As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID,MAS_Desc From Acc_General_Master Where MAS_ID In (Select IMT_CST from Inventory_Master_TaxDetails A where A.IMT_MasterID=" & HistoriID & " And A.IMT_EffectiveVATFRom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(Sdate, "CT") & "')) "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadExciseUsingDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal Sdate As String, ByVal HistoriID As String) As String
        Dim sSql As String = ""
        Try
            '   sSql = "Select Mas_ID,Mas_Desc from Acc_General_Master where Mas_Master=14 and Mas_DelFlag='A' and Mas_CompID=" & iCompID & " order by Mas_Desc"
            sSql = "Select top 1 MAS_Desc From Acc_General_Master Where MAS_ID In (Select IMT_Excise from Inventory_Master_TaxDetails A where A.IMT_MasterID=" & HistoriID & " And A.IMT_EffectiveVATFRom <= Convert(datetime,'" & objGen.FormatDtForRDBMS(Sdate, "CT") & "')) "
            Return objDBL.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
