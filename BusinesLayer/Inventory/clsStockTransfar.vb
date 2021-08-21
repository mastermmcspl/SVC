
Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer

Public Class clsStockTransfar
    Dim objPo As New clsPurchaseOrder
    Dim objGnrlfnction As New clsGeneralFunctions
    Dim objComp As New clsCompanyMaster
    Dim objFasgnrl As New clsFASGeneral
    Dim obDB As New DBHelper
    Private iSTM_Id As Integer
    Private iSTM_Branch As Integer
    Private iSTM_VATClass As Integer
    Private iSTM_FormReceive As Integer
    Private iSTM_CompID As Integer
    Private iSTM_YearID As Integer
    Private iSTM_CreatedBy As Integer
    Private iSTM_UpdatedBy As Integer
    Private iSTM_DeletedBy As Integer
    Private iSTM_ApprovedBy As Integer
    Private sSTM_SeriesNo As String
    Private sSTM_RefNo As String
    Private sSTM_FormNo As String
    Private sSTM_Narration As String
    Private sSTM_Status As String
    Private sSTM_Operation As String
    Private sSTM_IPAddress As String
    Private sSTM_OutwardNo As String
    Private dSTM_ApprovedOn As Date
    Private dSTM_UpdatedOn As Date
    Private dSTM_DeletedOn As Date
    Private dSTM_FormDate As Date
    Private dSTM_CreatedOn As Date

    Private iSTD_Id As Integer
    Private iSTD_MasterID As Integer
    Private iSTD_StockLedgerID As Integer
    Private iSTD_CommodityID As Integer
    Private iSTD_DescriptionID As Integer
    Private iSTD_HisotryID As Integer
    Private iSTD_Quantity As Integer
    Private dSTD_Rate As Double
    Private iSTD_Per As Integer
    Private dSTD_NetAmount As Double
    Private iSTD_CompID As Integer
    Private iSTD_YearID As Integer
    Public Property STM_CreatedBy() As Integer
        Get
            Return (iSTM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSTM_CreatedBy = Value
        End Set
    End Property
    Public Property STM_UpdatedBy() As Integer
        Get
            Return (iSTM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSTM_UpdatedBy = Value
        End Set
    End Property
    Public Property STM_ApprovedBy() As Integer
        Get
            Return (iSTM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iSTM_ApprovedBy = Value
        End Set
    End Property
    Public Property STM_DeletedBy() As Integer
        Get
            Return (iSTM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iSTM_DeletedBy = Value
        End Set
    End Property

    Public Property STM_YearID() As Integer
        Get
            Return (iSTM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSTM_YearID = Value
        End Set
    End Property
    Public Property STM_CompID() As Integer
        Get
            Return (iSTM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSTM_CompID = Value
        End Set
    End Property
    Public Property STM_FormReceive() As Integer
        Get
            Return (iSTM_FormReceive)
        End Get
        Set(ByVal Value As Integer)
            iSTM_FormReceive = Value
        End Set
    End Property
    Public Property STM_VATClass() As Integer
        Get
            Return (iSTM_VATClass)
        End Get
        Set(ByVal Value As Integer)
            iSTM_VATClass = Value
        End Set
    End Property
    Public Property STM_Id() As Integer
        Get
            Return (iSTM_Id)
        End Get
        Set(ByVal Value As Integer)
            iSTM_Id = Value
        End Set
    End Property
    Public Property STM_Branch() As Integer
        Get
            Return (iSTM_Branch)
        End Get
        Set(ByVal Value As Integer)
            iSTM_Branch = Value
        End Set
    End Property
    Public Property STM_RefNo() As String
        Get
            Return (sSTM_RefNo)
        End Get
        Set(ByVal Value As String)
            sSTM_RefNo = Value
        End Set
    End Property
    Public Property STM_FormNo() As String
        Get
            Return (sSTM_FormNo)
        End Get
        Set(ByVal Value As String)
            sSTM_FormNo = Value
        End Set
    End Property
    Public Property STM_Narration() As String
        Get
            Return (sSTM_Narration)
        End Get
        Set(ByVal Value As String)
            sSTM_Narration = Value
        End Set
    End Property
    Public Property STM_Status() As String
        Get
            Return (sSTM_Status)
        End Get
        Set(ByVal Value As String)
            sSTM_Status = Value
        End Set
    End Property
    Public Property STM_OutwardNo() As String
        Get
            Return (sSTM_OutwardNo)
        End Get
        Set(ByVal Value As String)
            sSTM_OutwardNo = Value
        End Set

    End Property


    Public Property STM_UpdatedOn() As DateTime
        Get
            Return (dSTM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSTM_UpdatedOn = Value
        End Set
    End Property

    Public Property STM_DeletedOn() As DateTime
        Get
            Return (dSTM_DeletedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSTM_DeletedOn = Value
        End Set
    End Property

    Public Property STM_FormDate() As DateTime
        Get
            Return (dSTM_FormDate)
        End Get
        Set(ByVal Value As DateTime)
            dSTM_FormDate = Value
        End Set
    End Property

    Public Property STM_CreatedOn() As DateTime
        Get
            Return (dSTM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dSTM_CreatedOn = Value
        End Set
    End Property
    Public Property STM_IPAddress() As String
        Get
            Return (sSTM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSTM_IPAddress = Value
        End Set
    End Property


    Public Property STD_Rate() As Decimal
        Get
            Return (dSTD_Rate)
        End Get
        Set(ByVal Value As Decimal)
            dSTD_Rate = Value
        End Set
    End Property
    Public Property STD_Per() As Integer
        Get
            Return (iSTD_Per)
        End Get
        Set(ByVal Value As Integer)
            iSTD_Per = Value
        End Set
    End Property
    Public Property STD_NetAmount() As Decimal
        Get
            Return (dSTD_NetAmount)
        End Get
        Set(ByVal Value As Decimal)
            dSTD_NetAmount = Value
        End Set
    End Property

    Public Property STD_YearID() As Integer
        Get
            Return (iSTD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSTD_YearID = Value
        End Set
    End Property

    Public Property STD_CompID() As Integer
        Get
            Return (iSTD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSTD_CompID = Value
        End Set
    End Property

    Public Property STD_CommodityID() As Integer
        Get
            Return (iSTD_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iSTD_CommodityID = Value
        End Set
    End Property
    Public Property STD_DescriptionID() As Integer
        Get
            Return (iSTD_DescriptionID)
        End Get
        Set(ByVal Value As Integer)
            iSTD_DescriptionID = Value
        End Set
    End Property
    Public Property STD_HisotryID() As Integer
        Get
            Return (iSTD_HisotryID)
        End Get
        Set(ByVal Value As Integer)
            iSTD_HisotryID = Value
        End Set
    End Property

    Public Property STD_Id() As Integer
        Get
            Return (iSTD_Id)
        End Get
        Set(ByVal Value As Integer)
            iSTD_Id = Value
        End Set
    End Property
    Public Property STD_MasterID() As Integer
        Get
            Return (iSTD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iSTD_MasterID = Value
        End Set
    End Property
    Public Property STD_StockLedgerID() As Integer
        Get
            Return (iSTD_StockLedgerID)
        End Get
        Set(ByVal Value As Integer)
            iSTD_StockLedgerID = Value
        End Set
    End Property
    Public Property STD_Quantity() As Integer
        Get
            Return (iSTD_Quantity)
        End Get
        Set(ByVal Value As Integer)
            iSTD_Quantity = Value
        End Set
    End Property

    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & " and SL_Commodity =" & iCommodity & ") and INV_Code <> '' order by Inv_Code"
            Return obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescritionStart(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master Where INV_ID in(Select SL_ItemID From Stock_Ledger Where SL_CompID=" & iCompID & ") and INV_Code <> '' order by Inv_Code"
            Return obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckDescriptionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtNew As New DataTable
        Dim dRow As DataRow
        Try
            dtNew.Columns.Add("InvH_ID")
            dtNew.Columns.Add("INVH_PreDeterminedPrice")

            sSql = "Select * from inventory_master_history where Invh_Inv_ID = " & iDescriptionID & " and InvH_CompID =" & iCompID & ""
            dt = obDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtNew.NewRow
                dRow("InvH_ID") = dt.Rows(i)("InvH_ID")
                dRow("INVH_PreDeterminedPrice") = dt.Rows(i)("INVH_PreDeterminedPrice") & " - " & objFasgnrl.FormatDtForRDBMS(dt.Rows(i)("InvH_EffeFrom"), "D")
                dtNew.Rows.Add(dRow)
            Next
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from inventory_master_History where InvH_ID =" & iHistoryID & " and InvH_CompID =" & iCompID & ""
            dt = obDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iTransactionID As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Purchase_Order_Master Set POM_Status='A',POM_ApporvedBy=" & iUserID & ",POM_ApprovedOn=GetDate() "
            sSql = sSql & "Where POM_OrderNo='" & iTransactionID & "' And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & ""
            obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As String) As String
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select POM_Status From Purchase_Order_Master Where POM_ID='" & iTransactionID & "' And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & ""
            sStatus = obDB.SQLGetDescription(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GeneratePurchaseOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = "", sMonth As String = "", sMonthCode As String = ""
        Dim sDate As String = "", sMaxID As String = "", sLastID As String = "", sSDate As String = ""
        Try
            sMaximumID = obDB.SQLGetDescription(sNameSpace, "Select Top 1 STM_ID From Stock_Transfer_master where STM_COmpID = " & iCompID & " Order By STM_ID Desc")
            sYear = obDB.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = obDB.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = obDB.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
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
            sStr = "" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " and CSM_Delflag='A' order by CSM_Name"
            Return obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select STM_Id,STM_OutwardNo from Stock_transfer_Master where STM_CompID=" & iCompID & " and STM_YearID =" & iYearID & "  order by STM_ID desc"
            Return obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePurchaseOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dOrderDate As Date, ByVal objPO As clsStockTransfar) As Integer
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Stock_transfer_Master where STM_OutwardNo = '" & objPO.STM_OutwardNo & "' and STM_CompID =" & iCompID & " and STM_YearID =" & objPO.STM_YearID & ""
            dr = obDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sSql = "" : sSql = "Update Stock_transfer_Master set STM_RefNo  = '" & objPO.STM_RefNo & "',STM_Branch =" & objPO.STM_Branch & ",STM_VATClass =" & objPO.STM_VATClass & ",STM_FormReceive  = " & objPO.STM_FormReceive & ""
                sSql = sSql & " Where STM_OutwardNo  = '" & objPO.STM_OutwardNo & "' and STM_CompID =" & iCompID & " and STM_YearID=" & objPO.STM_YearID & ""
                obDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dr("STM_Id")
            Else
                iMax = objGnrlfnction.GetMaxID(sNameSpace, iCompID, "Stock_transfer_Master", "STM_Id", "STM_CompID")
                sSql = "" : sSql = "Insert into Stock_transfer_Master(STM_Id,STM_FormDate,STM_OutwardNo,STM_RefNo,"
                sSql = sSql & "STM_Branch,STM_VATClass,STM_FormReceive,STM_CreatedOn,"
                sSql = sSql & "STM_YearID,STM_CompID,STM_FormNo,STM_Narration,STM_Status,STM_SeriesNo)Values(" & iMax & "," & objFasgnrl.FormatDtForRDBMS(objPO.STM_FormDate, "I") & ",'" & objPO.STM_OutwardNo & "','" & objPO.STM_RefNo & "',"
                sSql = sSql & "" & objPO.STM_Branch & "," & objPO.STM_VATClass & "," & objPO.STM_FormReceive & ",GetDate(),"
                sSql = sSql & "" & objPO.STM_YearID & "," & iCompID & ",'" & objPO.STM_FormNo & "','" & objPO.STM_Narration & "','W','" & objPO.STM_RefNo & "')"
                obDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If
            dr.Close()
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveStockTransfarDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dRequiredDate As Date, ByVal objPO As clsStockTransfar)
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Stock_transfer_Details where STD_MasterID = " & objPO.STD_MasterID & " and STD_CommodityID = " & objPO.STD_CommodityID & " and "
            sSql = sSql & "STD_DescriptionID = " & objPO.STD_DescriptionID & " and STD_HisotryID =" & objPO.STD_HisotryID & " and STD_CompID = " & iCompID & ""
            dr = obDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                sSql = "" : sSql = "Update Stock_Transfer_Details set STD_Per = " & objPO.STD_Per & ",STD_Rate=" & objPO.STD_Rate & ",STD_NetAmount= " & objPO.STD_NetAmount & ",STD_Quantity=" & objPO.STD_Quantity & ""
                sSql = sSql & "where STD_MasterID = " & objPO.STD_MasterID & " And "
                sSql = sSql & "STD_CommodityID = " & objPO.STD_CommodityID & " And STD_DescriptionID = " & objPO.STD_DescriptionID & " And "
                sSql = sSql & "STD_HisotryID =" & objPO.STD_HisotryID & "  And STD_CompID = " & iCompID & ""
                obDB.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objGnrlfnction.GetMaxID(sNameSpace, iCompID, "Stock_Transfer_Details", "STD_Id", "STD_CompID")
                sSql = "" : sSql = "Insert into Stock_Transfer_Details(STD_Id, STD_MasterID,"
                sSql = sSql & "STD_CommodityID, STD_DescriptionID, STD_HisotryID, STD_Quantity, STD_Rate, "
                sSql = sSql & "STD_Per, STD_NetAmount,STD_CompID, STD_YearID,STD_Status)"
                sSql = sSql & "Values(" & iMax & "," & objPO.STD_MasterID & "," & objPO.STD_CommodityID & ","
                sSql = sSql & "" & objPO.STD_DescriptionID & "," & objPO.STD_HisotryID & "," & objPO.STD_Quantity & "," & objPO.STD_Rate & ","
                sSql = sSql & "" & objPO.STD_Per & "," & objPO.STD_NetAmount & "," & iCompID & "," & objPO.STD_YearID & ",'W')"
                obDB.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseORderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim ds As New DataSet
        Dim iSlNo As Integer = 0
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("CommodityID")
            dt.Columns.Add("DescriptionID")
            dt.Columns.Add("HistoryID")
            dt.Columns.Add("UnitsID")
            dt.Columns.Add("SLNO")
            dt.Columns.Add("Goods")
            dt.Columns.Add("Units")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("RateAmount")
            dt.Columns.Add("TotalAmount")
            sSql = "Select * from Stock_Transfer_Details where STD_MasterID  =" & iMasterID & " and STD_CompID =" & iCompID & " and STD_Status='W' order by STD_Id"
            ds = obDB.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("ID") = ds.Tables(0).Rows(i)("STD_Id")
                    dRow("CommodityID") = ds.Tables(0).Rows(i)("STD_CommodityID")
                    dRow("DescriptionID") = ds.Tables(0).Rows(i)("STD_DescriptionID")
                    dRow("HistoryID") = ds.Tables(0).Rows(i)("STD_HisotryID")
                    dRow("UnitsID") = ds.Tables(0).Rows(i)("STD_Per")
                    iSlNo = iSlNo + 1
                    dRow("SLNO") = iSlNo
                    dRow("Goods") = obDB.SQLExecuteScalar(sNameSpace, "Select Inv_Code from Inventory_Master where Inv_ID=" & ds.Tables(0).Rows(i)("STD_DescriptionID") & " and Inv_compid=" & iCompID & "")
                    dRow("Units") = obDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID=" & ds.Tables(0).Rows(i)("STD_Per") & " and Mas_compid=" & iCompID & "")
                    dRow("Rate") = ds.Tables(0).Rows(i)("STD_Rate")
                    dRow("Quantity") = ds.Tables(0).Rows(i)("STD_Quantity")
                    dRow("RateAmount") = ds.Tables(0).Rows(i)("STD_Rate")
                    dRow("TotalAmount") = ds.Tables(0).Rows(i)("STD_NetAmount")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub DeleteOrderValues(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal OrderNo As String, ByVal DcritionID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Stock_Transfer_Details set STD_Status='D' Where STD_MasterID in(select STM_ID from Stock_Transfer_Master "
            sSql = sSql & "where STM_OutwardNo='" & OrderNo & "' and STM_YearID =" & iYearId & " and STM_COmpID =" & iCompID & " and  STM_Status='W') and STD_DescriptionID=" & DcritionID & " and STD_CompID = " & iCompID & ""
            obDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub DeleteOrderValuesFromMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal OrderNo As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Purchase_Order_Master set POM_Status='D' where POM_OrderNo='" & OrderNo & "' and POM_YearID =" & iYearId & " and POM_COmpID =" & iCompID & " and  POM_Status='W'"
            obDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function LoadUnitOFMeasurement(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtNew As New DataTable
        Try
            dtNew.Columns.Add("Mas_ID")
            dtNew.Columns.Add("Mas_Desc")

            sSql = "Select * from inventory_master_history where Invh_Inv_ID = " & iInvID & " And InvH_CompID =" & iCompID & ""
            dt = obDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_Unit")
                dRow("Mas_Desc") = obDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & dt.Rows(0)("InvH_Unit") & "' and Mas_compid=" & iCompID & "")
                dtNew.Rows.Add(dRow)

                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_AlterUnit")
                dRow("Mas_Desc") = obDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & dt.Rows(0)("InvH_AlterUnit") & "' and Mas_compid=" & iCompID & "")
                dtNew.Rows.Add(dRow)
            End If
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseOderMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPomID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Stock_transfer_Master where STM_ID = " & iPomID & " and STM_CompID = " & iCompID & " and STM_YearID =" & iYearID & ""
            dt = obDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseOderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iCommodity As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Stock_Transfer_Details where STD_MasterID = " & iMasterID & " and STD_CommodityID = " & iCommodity & " and "
            sSql = sSql & "STD_DescriptionID  = " & iDescriptionID & " and  STD_HisotryID  = " & iHistoryID & " and STD_CompID  = " & iCompID & ""
            dt = obDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetSupplierCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer)
        Dim sSql As String = "", sCOde As String = ""
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select * from customerSupplierMaster where CSM_ID =" & iSupplier & " and CSM_CompID = " & iCompID & ""
            dr = obDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sCOde = dr("CSM_Code")
            End If
            Return sCOde
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetOtherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from inventory_Master_History where InvH_Id = " & iHistoryID & " and InvH_CompID = " & iCompID & ""
            dt = obDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPaymentTerms(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=18 and Mas_Status='A'"
            Return obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDeliverySchdule(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=17 and Mas_Status='A'"
            Return obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBranches(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CUSTB_ID,CUSTB_Name from MST_CUSTOMER_MASTER_Branch"
            Return obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMethodOfPayment(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=11 and Mas_Status='A'"
            Return obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadBranchDetails(ByVal sNameSpace As String, ByVal BranchID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "select * from MST_CUSTOMER_MASTER_Branch where CUSTB_ID=" & BranchID & ""

            Return obDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccessCode(ByVal sAccessName As String)
        Dim sSql As String
        Dim sAccessCode As String
        Try
            sSql = "Select SAD_CMS_AccessCode from Sad_CompanyMaster_Settings"
            sAccessCode = obDB.SQLExecuteScalar(sAccessName, sSql)
            Return sAccessCode
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPomID As Integer) As DataTable
        Dim dt As New DataTable
        Dim dt0 As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim dt4 As New DataTable
        Dim dRow As DataRow
        Dim ctin As String = "" : Dim Cpan As String = "" : Dim Span As String = "" : Dim Stin As String = "" : Dim company As String = "" : Dim suplierId As String = "" : Dim BranchID As String = "" : Dim temp1 As String = ""
        Dim SubTotal As String = "", CSTAmtTotal = "", TotalVat = "", GrandTotal = "", UnitId = "", AltUnit = "" : Dim OrderNo, BillNo As Integer
        Dim TotalinWord As String = ""
        Dim Totalamt As Double = 0

        Try
            company = GetAccessCode(sNameSpace)
            dt0 = objComp.LoadCompanyDetails(sNameSpace, iCompID, sNameSpace)

            BranchID = obDB.SQLGetDescription(sNameSpace, "  select STM_Branch from Stock_Transfer_Master where STM_Id= " & iPomID & "")
            dt2 = LoadBranchDetails(sNameSpace, BranchID)


            dt4 = loadDetailsB(sNameSpace, iCompID, iPomID, BillNo)
            For i = 0 To dt4.Rows.Count - 1
                ' If i = dt4.Rows.Count - 1 Then
                Totalamt = Totalamt + Convert.ToDecimal(dt4.Rows(i)("Total"))
                TotalinWord = NumberToWord(String.Format("{0:0.00}", Totalamt)) & " Only"
                ' End If
            Next
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("UnitID")
            dt.Columns.Add("Total")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("NoToWord")
            dt.Columns.Add("OutWordNo")
            dt.Columns.Add("OutWordDate")
            dt.Columns.Add("CName")
            dt.Columns.Add("CAdd")
            dt.Columns.Add("CPh")
            dt.Columns.Add("CEmail")
            dt.Columns.Add("Ctin")
            dt.Columns.Add("CPan")
            dt.Columns.Add("CUSTB_ADDRESS")
            dt.Columns.Add("CUSTB_Name")
            dt.Columns.Add("CUSTB_ContectPerson")
            For i = 0 To dt4.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SlNo") = dt4.Rows(i)("SlNo")
                dRow("Commodity") = dt4.Rows(i)("Commodity")
                dRow("Description") = dt4.Rows(i)("Description")
                dRow("TotalQty") = dt4.Rows(i)("TotalQty")
                dRow("Rate") = dt4.Rows(i)("Rate")
                dRow("Total") = dt4.Rows(i)("Total")
                dRow("GrandTotal") = String.Format("{0:0.00}", Totalamt.ToString())
                dRow("UnitId") = dt4.Rows(i)("UnitId")
                dRow("NoToWord") = TotalinWord
                dRow("OutWordNo") = dt4.Rows(i)("OutWordNo")
                dRow("OutWordDate") = dt4.Rows(i)("OutWordDate")
                dRow("CName") = GetAccessCode(sNameSpace)
                dRow("CAdd") = dt0.Rows(0).Item("CUST_COMM_ADDRESS")
                dRow("CPh") = "Ph  " & dt0.Rows(0).Item("CUST_COMM_TEL")
                dRow("CEmail") = "E-mail  " & dt0.Rows(0).Item("CUST_EMAIL")
                dRow("Ctin") = "1"
                dRow("CPan") = "2"
                dRow("CUSTB_ADDRESS") = dt2.Rows(0).Item("CUSTB_ADDRESS")
                dRow("CUSTB_Name") = dt2.Rows(0).Item("CUSTB_Name")
                dRow("CUSTB_ContectPerson") = "a"

                dt.Rows.Add(dRow)
            Next

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
                    num = num - (hundreds * 10000000)
                Else
                    crore = num / 100
                    num = num - (hundreds * 10000000)
                End If
            End If
            If (num > 100000) Then
                If ((Convert.ToString(num / 100000)).Contains(".")) Then
                    lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                    num = num - (hundreds * 100000)
                Else
                    lakhs = num / 100000
                    num = num - (hundreds * 100000)
                End If
            End If

            If (num > 1000) Then
                If ((Convert.ToString(num / 1000)).Contains(".")) Then
                    thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
                    num = num - (thousands * 1000)
                Else
                    thousands = num / 1000
                    num = num - (thousands * 1000)
                End If
            End If

            If (num > 100) Then

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
                    words += " and " + aftrdecimalWord
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
            If ((Convert.ToString(num / 100000)).Contains(".")) Then
                lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                num = num - (hundreds * 100000)
            Else
                lakhs = num / 100000
                num = num - (hundreds * 100000)
            End If
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
    Public Function loadDetailsB(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As DataRow
        Dim dr As DataRow
        Dim dtDetails As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim VAT As String = "", CST As String = "", Exise As String = ""
        Dim Cstval As String = ""
        Dim Total, TotalAmt, Totaltax, TotalVat As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtdiscount, gtdiscountAmt, GrandTotal, subTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("UnitId")
            dt.Columns.Add("Total")
            dt.Columns.Add("OutWordNo")
            dt.Columns.Add("OutWordDate")
            sSql = "" : sSql = " Select * From Stock_Transfer_Details Where STD_Status ='W' and STD_MasterID in(select STM_Id from Stock_Transfer_Master"
            sSql = sSql & " Stock_Transfer_Master where STM_ID=" & iorder & ")"
            dtDetails = obDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                If IsDBNull(dtDetails.Rows(i)("STD_CommodityID")) = False Then
                    dRow("Commodity") = obDB.SQLGetDescription(sNameSpace, "Select Inv_Description from Inventory_master where Inv_ID= '" & dtDetails.Rows(i)("STD_CommodityID") & "'")
                End If
                If IsDBNull(dtDetails.Rows(i)("STD_DescriptionID")) = False Then
                    dRow("SlNo") = i + 1
                    dRow("Description") = obDB.SQLGetDescription(sNameSpace, "Select Inv_Code from Inventory_master where Inv_ID= '" & dtDetails.Rows(i)("STD_DescriptionID") & "'")
                End If
                If IsDBNull(dtDetails.Rows(i)("STD_Quantity")) = False Then
                    dRow("TotalQty") = dtDetails.Rows(i)("STD_Quantity")
                    gtQty = gtQty + dtDetails.Rows(i)("STD_Quantity")
                Else
                    dRow("TotalQty") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("STD_Rate")) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("STD_Rate")
                Else
                    dRow("Rate") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("STD_Per")) = False Then
                    dRow("UnitId") = obDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id =" & dtDetails.Rows(i)("STD_Per") & "")
                Else
                    dRow("UnitId") = "0"
                End If
                If IsDBNull(dtDetails.Rows(i)("STD_NetAmount")) = False Then
                    dRow("Total") = dtDetails.Rows(i)("STD_NetAmount")
                Else
                    dRow("Total") = "0"
                End If
                If IsDBNull(dtDetails.Rows(i)("STD_MasterID")) = False Then
                    dRow("OutWordNo") = obDB.SQLGetDescription(sNameSpace, "Select STM_OutwardNo from Stock_Transfer_Master where STM_Id =" & dtDetails.Rows(i)("STD_MasterID") & "")
                Else
                    dRow("OutWordNo") = "0"
                End If
                If IsDBNull(dtDetails.Rows(i)("STD_MasterID")) = False Then
                    dRow("OutWordDate") = obDB.SQLGetDescription(sNameSpace, "Select STM_FormDate from Stock_Transfer_Master where STM_Id =" & dtDetails.Rows(i)("STD_MasterID") & "")
                Else
                    dRow("OutWordDate") = "0"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BranchDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal BID As Integer) As DataTable
        Dim dt, dtTab As New DataTable
        Dim txtorderqty As String = "", sSql As String = ""
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("CUSTB_ADDRESS")
            dtTab.Columns.Add("CUSTB_ContactPerson")
            dtTab.Columns.Add("CUSTB_TEL")
            sSql = "" : sSql = "Select * From MST_CUSTOMER_MASTER_Branch Where CUSTB_ID=" & BID & ""
            dt = obDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("CUSTB_ADDRESS") = dt.Rows(i)("CUSTB_ADDRESS")
                dr("CUSTB_TEL") = dt.Rows(i)("CUSTB_TEL")
                dr("CUSTB_ContactPerson") = dt.Rows(i)("CUSTB_ContactPerson")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
