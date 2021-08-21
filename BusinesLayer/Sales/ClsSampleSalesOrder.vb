Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSampleSalesOrder
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral


    Private iSSOM_ID As Integer
    Private sSSOM_SampleOrderNo As String
    Private dSSOM_SampleDate As DateTime
    Private iSSOM_Party As Integer
    Private sSSOM_PartyCode As String
    Private sSSOM_ContantNo As String
    Private sSSOM_Address As String
    Private iSSOM_ModeOfShipping As Integer
    Private dSSOM_ShippingDate As DateTime
    Private iSSOM_Communication As Integer
    Private iSSOM_IssuedBy As Integer
    Private dSSOM_IssuedOn As DateTime
    Private sSSOM_DelFlag As String
    Private sSSOM_Status As String
    Private iSSOM_CreatedBy As Integer
    Private dSSOM_CreatedOn As DateTime
    Private iSSOM_UpdatedBy As Integer
    Private dSSOM_UpdatedOn As DateTime
    Private sSSOM_Operation As String
    Private sSSOM_IPAddress As String
    Private iSSOM_CompID As Integer
    Private iSSOM_YearID As Integer

    Private iSSOD_ID As Integer
    Private iSSOD_MasterID As Integer
    Private iSSOD_CommodityId As Integer
    Private iSSOD_ItemID As Integer
    Private iSSOD_Historyid As Integer
    Private iSSOD_UnitID As Integer
    Private iSSOD_Quantity As Integer
    Private iSSOD_Amount As Double
    Private iSSOD_TotalAmount As Double
    Private sSSOD_DelFlag As String
    Private sSSOD_Status As String
    Private iSSOD_CreatedBy As Integer
    Private dSSOD_CreatedOn As DateTime
    Private sSSOD_Operation As String
    Private sSSOD_IPAddress As String
    Private iSSOD_CompID As Integer
    Private iSSOD_YearID As Integer

    Public Property SSOM_ID() As Integer
        Get
            Return (iSSOM_ID)
        End Get
        Set(ByVal Value As Integer)
            iSSOM_ID = Value
        End Set
    End Property
    Public Property SSOM_SampleOrderNo() As String
        Get
            Return (sSSOM_SampleOrderNo)
        End Get
        Set(ByVal Value As String)
            sSSOM_SampleOrderNo = Value
        End Set
    End Property
    Public Property SSOM_SampleDate() As DateTime
        Get
            Return (dSSOM_SampleDate)
        End Get
        Set(ByVal Value As DateTime)
            dSSOM_SampleDate = Value
        End Set
    End Property
    Public Property SSOM_Party() As Integer
        Get
            Return (iSSOM_Party)
        End Get
        Set(ByVal Value As Integer)
            iSSOM_Party = Value
        End Set
    End Property
    Public Property SSOM_PartyCode() As String
        Get
            Return (sSSOM_PartyCode)
        End Get
        Set(ByVal Value As String)
            sSSOM_PartyCode = Value
        End Set
    End Property
    Public Property SSOM_ContantNo() As String
        Get
            Return (sSSOM_ContantNo)
        End Get
        Set(ByVal Value As String)
            sSSOM_ContantNo = Value
        End Set
    End Property
    Public Property SSOM_Address() As String
        Get
            Return (sSSOM_Address)
        End Get
        Set(ByVal Value As String)
            sSSOM_Address = Value
        End Set
    End Property
    Public Property SSOM_ModeOfShipping() As Integer
        Get
            Return (iSSOM_ModeOfShipping)
        End Get
        Set(ByVal Value As Integer)
            iSSOM_ModeOfShipping = Value
        End Set
    End Property
    Public Property SSOM_ShippingDate() As DateTime
        Get
            Return (dSSOM_ShippingDate)
        End Get
        Set(ByVal Value As DateTime)
            dSSOM_ShippingDate = Value
        End Set
    End Property
    Public Property SSOM_Communication() As Integer
        Get
            Return (iSSOM_Communication)
        End Get
        Set(ByVal Value As Integer)
            iSSOM_Communication = Value
        End Set
    End Property
    Public Property SSOM_IssuedBy() As Integer
        Get
            Return (iSSOM_IssuedBy)
        End Get
        Set(ByVal Value As Integer)
            iSSOM_IssuedBy = Value
        End Set
    End Property
    Public Property SSOM_IssuedOn() As DateTime
        Get
            Return (dSSOM_IssuedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSSOM_IssuedOn = Value
        End Set
    End Property
    Public Property SSOM_DelFlag() As String
        Get
            Return (sSSOM_DelFlag)
        End Get
        Set(ByVal Value As String)
            sSSOM_DelFlag = Value
        End Set
    End Property
    Public Property SSOM_Status() As String
        Get
            Return (sSSOM_Status)
        End Get
        Set(ByVal Value As String)
            sSSOM_Status = Value
        End Set
    End Property
    Public Property SSOM_CreatedBy() As Integer
        Get
            Return (iSSOM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSSOM_CreatedBy = Value
        End Set
    End Property
    Public Property SSOM_CreatedOn() As DateTime
        Get
            Return (dSSOM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSSOM_CreatedOn = Value
        End Set
    End Property
    Public Property SSOM_UpdatedBy() As Integer
        Get
            Return (iSSOM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSSOM_UpdatedBy = Value
        End Set
    End Property
    Public Property SSOM_UpdatedOn() As DateTime
        Get
            Return (dSSOM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSSOM_UpdatedOn = Value
        End Set
    End Property
    Public Property SSOM_Operation() As String
        Get
            Return (sSSOM_Operation)
        End Get
        Set(ByVal Value As String)
            sSSOM_Operation = Value
        End Set
    End Property
    Public Property SSOM_IPAddress() As String
        Get
            Return (sSSOM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSSOM_IPAddress = Value
        End Set
    End Property
    Public Property SSOM_CompID() As Integer
        Get
            Return (iSSOM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSSOM_CompID = Value
        End Set
    End Property
    Public Property SSOM_YearID() As Integer
        Get
            Return (iSSOM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSSOM_YearID = Value
        End Set
    End Property


    Public Property SSOD_ID() As Integer
        Get
            Return (iSSOD_ID)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_ID = Value
        End Set
    End Property
    Public Property SSOD_MasterID() As Integer
        Get
            Return (iSSOD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_MasterID = Value
        End Set
    End Property
    Public Property SSOD_CommodityId() As Integer
        Get
            Return (iSSOD_CommodityId)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_CommodityId = Value
        End Set
    End Property
    Public Property SSOD_ItemID() As Integer
        Get
            Return (iSSOD_ItemID)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_ItemID = Value
        End Set
    End Property
    Public Property SSOD_Historyid() As Integer
        Get
            Return (iSSOD_Historyid)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_Historyid = Value
        End Set
    End Property
    Public Property SSOD_UnitID() As Integer
        Get
            Return (iSSOD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_UnitID = Value
        End Set
    End Property
    Public Property SSOD_Quantity() As Integer
        Get
            Return (iSSOD_Quantity)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_Quantity = Value
        End Set
    End Property
    Public Property SSOD_Amount() As Double
        Get
            Return (iSSOD_Amount)
        End Get
        Set(ByVal Value As Double)
            iSSOD_Amount = Value
        End Set
    End Property
    Public Property SSOD_TotalAmount() As Double
        Get
            Return (iSSOD_TotalAmount)
        End Get
        Set(ByVal Value As Double)
            iSSOD_TotalAmount = Value
        End Set
    End Property
    Public Property SSOD_DelFlag() As String
        Get
            Return (sSSOD_DelFlag)
        End Get
        Set(ByVal Value As String)
            sSSOD_DelFlag = Value
        End Set
    End Property
    Public Property SSOD_Status() As String
        Get
            Return (sSSOD_Status)
        End Get
        Set(ByVal Value As String)
            sSSOD_Status = Value
        End Set
    End Property
    Public Property SSOD_CreatedBy() As Integer
        Get
            Return (iSSOD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_CreatedBy = Value
        End Set
    End Property
    Public Property SSOD_CreatedOn() As DateTime
        Get
            Return (dSSOD_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSSOD_CreatedOn = Value
        End Set
    End Property
    Public Property SSOD_Operation() As String
        Get
            Return (sSSOD_Operation)
        End Get
        Set(ByVal Value As String)
            sSSOD_Operation = Value
        End Set
    End Property
    Public Property SSOD_IPAddress() As String
        Get
            Return (sSSOD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSSOD_IPAddress = Value
        End Set
    End Property
    Public Property SSOD_CompID() As Integer
        Get
            Return (iSSOD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_CompID = Value
        End Set
    End Property
    Public Property SSOD_YearID() As Integer
        Get
            Return (iSSOD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSSOD_YearID = Value
        End Set
    End Property

    Public Function LoadItems(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, Optional ByVal iCommodityID As Integer = 0) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            If iCommodityID > 0 Then
                sSql = "Select INV_ID,INV_Code From Inventory_Master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & " and SL_Commodity =" & iCommodityID & ") and INV_Code <> '' order by Inv_Code"
            Else
                sSql = "Select INV_ID,INV_Code From Inventory_Master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & ") and INV_Code <> '' order by Inv_Code"
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iID As Integer, ByVal iHistoryID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim dRow As DataRow
        Dim iUnit, iAlterUnit As Integer
        Dim sArray As String()
        Dim sValue As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            dt.Columns.Add("MAS_ID")
            dt.Columns.Add("MAS_Desc")

            iUnit = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_Unit From inventory_master_history Where INVH_ID=" & iHistoryID & " And InvH_INV_ID=" & iID & " and InvH_CompID =" & iCompID & "")
            iAlterUnit = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_AlterUnit From inventory_master_history Where INVH_ID=" & iHistoryID & " And InvH_INV_ID=" & iID & " and InvH_CompID =" & iCompID & "")
            sValue = iUnit & "," & iAlterUnit
            sArray = sValue.Split(",")

            For i = 0 To sArray.Length - 1
                sSql = "Select MAS_ID,MAS_Desc from Acc_General_Master Where MAS_ID =" & sArray(i) & " and Mas_CompId = " & iCompID & ""
                dr = objDBL.SQLDataReader(sNameSpace, sSql)
                If dr.HasRows = True Then
                    While dr.Read()
                        dRow = dt.NewRow
                        dRow("MAS_ID") = dr("MAS_ID")
                        dRow("MAS_Desc") = dr("MAS_Desc")
                        dt.Rows.Add(dRow)
                    End While
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMethodOfShiping(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " And Mas_master=13 And Mas_Delflag='A'"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
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
    Public Function BindModeOfCommunication(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc From acc_general_master Where Mas_Master In (Select Mas_ID From acc_master_Type Where Mas_Type='Mode Of Communication') And Mas_CompID=" & iCompID & " and Mas_Status='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSampleSalesMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objSampleSales As ClsSampleSalesOrder) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_SampleOrderNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_SampleOrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_SampleDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_SampleDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_PartyCode", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_PartyCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_ContantNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_ContantNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_Address", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_Address
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_ModeOfShipping ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_ModeOfShipping
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_ShippingDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_ShippingDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_Communication", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_Communication
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_IssuedBy", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_IssuedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_IssuedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_IssuedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_DelFlag ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_Status ", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSampleSales.SSOM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSales_Sample_Order_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSampleSalesDetails(ByVal sNameSpace As String, ByVal objSampleSales As ClsSampleSalesOrder, ByVal iYearID As Integer) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_CommodityId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_CommodityId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_ItemID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_ItemID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_Historyid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_Historyid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_Quantity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_Amount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_TotalAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_TotalAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_DelFlag ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_Status ", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSampleSales.SSOD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SSOD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSales_Sample_Order_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMonth As String = ""
        Dim sMonthCode As String = ""
        Dim sDate As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""
        Dim sSDate As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 SSOM_ID From Sales_Sample_Order_Master Order By SSOM_ID Desc")
            sYear = objDBL.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDBL.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDBL.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
            If sMaximumID = Nothing Then
                sMaxID = "0001"
            Else
                sLastID = sMaximumID + 1
                If sLastID.Length = 1 Then
                    sMaxID = "000" & "" & sLastID & ""
                ElseIf sLastID.Length = 2 Then
                    sMaxID = "00" & "" & sLastID & ""
                ElseIf sLastID.Length = 3 Then
                    sMaxID = "0" & "" & sLastID & ""
                End If
            End If

            If sMonth.Length = 1 Then
                sMonthCode = "0" & "" & sMonth & ""
            Else
                sMonthCode = sMonth
            End If

            If sDate.Length = 1 Then
                sSDate = "0" & "" & sDate & ""
            Else
                sSDate = sDate
            End If
            sStr = "SS-" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("SlNo")
            dtTab.Columns.Add("Commodity")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")
            dtTab.Columns.Add("Quantity")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("TotalAmount")

            If iMasterID > 0 Then
                sSql = "Select * From Sales_Sample_Order_Details Where SSOD_Status <>'C' And SSOD_MasterID=" & iMasterID & " And SSOD_CompID=" & iCompID & " Order By SSOD_ID "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ID") = dt.Rows(i)("SSOD_ID")
                    dRow("CommodityID") = dt.Rows(i)("SSOD_CommodityId")
                    dRow("ItemID") = dt.Rows(i)("SSOD_ItemID")
                    dRow("HistoryID") = dt.Rows(i)("SSOD_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("SSOD_UnitID")
                    dRow("SlNo") = i + 1
                    dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("SSOD_CommodityId") & " And INV_Parent=0 and Inv_CompID = " & iCompID & "")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From Inventory_Master Where INV_ID=" & dt.Rows(i)("SSOD_ItemID") & " And INV_Parent=" & dt.Rows(i)("SSOD_CommodityId") & " and Inv_CompID = " & iCompID & "")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SSOD_UnitID") & " and Mas_CompID =" & iCompID & "")
                    dRow("Quantity") = dt.Rows(i)("SSOD_Quantity")
                    dRow("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SSOD_Amount")))
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SSOD_TotalAmount")))

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAvailableStock(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String = ""
        Dim iAvailableQty As Integer
        Try
            sSql = "Select SL_ClosingBalanceQty From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_HistoryID=" & iHistoryID & " And SL_CompID=" & iCompID & "  "
            iAvailableQty = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iAvailableQty
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateStockTable(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal iQnty As Integer)
        Dim sSql As String = ""
        Dim iSalesQnty, iSalesQntyTot, iOpeningBalanceQty, iClosingBalanceQty As Integer
        Dim dt As New DataTable
        Try
            sSql = "Select * From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_HistoryID=" & iHistoryID & " And SL_CompID=" & iCompID & "  "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                iSalesQnty = dt.Rows(0)("SL_SaleQnty")
                iOpeningBalanceQty = dt.Rows(0)("SL_OpeningBalanceQty")
            End If

            iSalesQntyTot = iSalesQnty + iQnty
            iClosingBalanceQty = iOpeningBalanceQty - iSalesQntyTot

            sSql = ""
            sSql = "Update Stock_Ledger Set SL_SaleQnty=" & iSalesQntyTot & ", SL_ClosingBalanceQty=" & iClosingBalanceQty & " Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_HistoryID=" & iHistoryID & " And SL_CompID=" & iCompID & "  "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Shared Function GetHistoryID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim iHistoryID As Integer
    '    Try
    '        sSql = "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " And INVH_CompID=" & iCompID & "  "
    '        iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
    '        Return iHistoryID
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetHistoryID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As Integer
        Dim iHistoryID As String = "", sSql As String = ""
        Try
            sSql = "" : sSql = "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID In (Select INV_ID From Inventory_Master Where "
            sSql = sSql & "INV_ID =" & iItemID & " And INV_Parent=" & iCommodityID & " And Inv_CompID = " & iCompID & ") And "
            sSql = sSql & "InvH_CompID =" & iCompID & ""
            iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iHistoryID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select SSOM_ID,SSOM_SampleOrderNo From Sales_Sample_Order_Master where SSOM_CompID=" & iCompID & " and SSOM_YearID = " & iYearID & " Order By SSOM_ID Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Sales_Sample_Order_Master Where SSOM_ID=" & iOrderID & " And SSOM_CompID=" & iCompID & " and SSOM_YearID = " & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssuedBy(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select USR_ID,USR_FullName from Sad_UserDetails where USR_CompID=" & iCompID & " And USR_Delflag='A'"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iID As Integer, ByVal iOrderID As Integer, ByVal iIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Sample_Order_Details Set SSOD_DeletedBy =" & iUserID & ",SSOD_DeletedOn =GetDate(),SSOD_Status ='C',SSOD_Operation='D',SSOD_IPAddress='" & iIPAddress & "' Where SSOD_MasterID=" & iOrderID & " And SSOD_Id =" & iID & "  "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iorderno As Integer) As String
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select SSOM_Delflag from Sales_Sample_Order_Master where SSOM_CompID=" & iCompID & " and SSOM_ID=" & iorderno & " and SSOM_YearID =" & iYearID & ""
            sStatus = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iAllocationID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Sample_Order_Master Set SSOM_DelFlag='A',SSOM_Status='A',SSOM_ApprovedBy=" & iUserID & ",SSOM_ApprovedOn=GetDate(),SSOM_Operation='A',SSOM_IPAddress='" & iIPAddress & "' Where SSOM_ID=" & iAllocationID & " And SSOM_CompID=" & iCompID & " and SSOM_YearID =" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRetailRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As String
        Dim sSql As String = "", sCode As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_ID in (Select SL_HistoryID From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_CompID=" & iCompID & ") and INVH_CompID =" & iCompID & ""
            GetRetailRate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetRetailRate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMRPRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As String
        Dim sSql As String = "", sCode As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_ID in (Select SL_HistoryID From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_CompID=" & iCompID & ") and INVH_CompID =" & iCompID & ""
            GetMRPRate = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetMRPRate
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iAllocationID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Sample_Order_Master Set SSOM_DelFlag='X',SSOM_Status='X',SSOM_DeletedBy=" & iUserID & ",SSOM_DeletedOn=GetDate(),SSOM_Operation='D',SSOM_IPAddress='" & iIPAddress & "' Where SSOM_ID=" & iAllocationID & " And SSOM_CompID=" & iCompID & " and SSOM_YearID =" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal iQnty As Integer, ByVal iIPAddress As String, ByVal iID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Sample_Order_Details Set SSOD_DelFlag='X',SSOD_Status='X',SSOD_DeletedBy=" & iUserID & ",SSOD_DeletedOn=GetDate(),SSOD_Operation='D',SSOD_IPAddress='" & iIPAddress & "' Where SSOD_ID=" & iID & " And SSOD_MasterID=" & iOrderID & " And SSOD_CommodityID=" & iCommodityID & " And SSOD_ItemID=" & iItemID & " And SSOD_HistoryID=" & iHistoryID & " And SSOD_CompID=" & iCompID & " and SSOD_YearID =" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RecallMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iAllocationID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Sample_Order_Master Set SSOM_DelFlag='Y',SSOM_Status='Y',SSOM_RecalledBy=" & iUserID & ",SSOM_RecalledOn=GetDate(),SSOM_Operation='R',SSOM_IPAddress='" & iIPAddress & "' Where SSOM_ID=" & iAllocationID & " And SSOM_CompID=" & iCompID & " and SSOM_YearID =" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RecallDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal iQnty As Integer, ByVal iIPAddress As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Sample_Order_Details Set SSOD_DelFlag='Y',SSOD_Status='Y',SSOD_RecalledBy=" & iUserID & ",SSOD_RecalledOn=GetDate(),SSOD_Operation='R',SSOD_IPAddress='" & iIPAddress & "' Where SSOD_MasterID=" & iOrderID & " And SSOD_CommodityID=" & iCommodityID & " And SSOD_ItemID=" & iItemID & " And SSOD_HistoryID=" & iHistoryID & " And SSOD_CompID=" & iCompID & " and SSOD_YearID =" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " and BM_CompID =" & iCompID & ""
            'sSql = "Select Top 1 *, ACM_Code  From Acc_Customer_Address_Details,Acc_Customer_Master Where ACAD_MasterID=ACM_ID And ACM_ID=" & iPartyID & " and ACM_Status='A' And ACAD_CompID=" & iCompID & "  "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCommodityID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCommodity As Integer
        Try
            iCommodity = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SL_Commodity From Stock_Ledger Where SL_ItemID=" & iItemID & " And SL_CompID=" & iCompID & " ")
            Return iCommodity
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
