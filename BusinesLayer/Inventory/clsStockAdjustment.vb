
Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class clsStockAdjustment
    Dim objDB As New DBHelper
    Dim objFas As New clsFASGeneral
    Dim objFasGnrl As New clsGeneralFunctions
    Private SM_ID As Integer
    Private SM_AjustedDate As Date
    Private SM_AjustedNo As String
    Private SM_CreatedBy As Integer

    Private SM_AjustReason As String
    Private SM_AjustedBy As String
    Private SM_YearID As Integer

    Private SD_ID As Integer
    Private SD_MasterID As Integer
    Private SD_Commodity As Integer
    Private SD_DescriptionID As Integer
    Private SD_HistoryID As Integer

    Private SD_UnitID As Integer
    Private SD_StockID As Integer
    Private SD_Reason As String
    Private SD_Status As String
    Private Sd_Type As String
    Private Sd_Flag As String

    Private SD_Rate As Double
    Private SD_TotRate As Double
    Private SD_AjustedRate As Double
    Private SD_Quantity As Double
    Private SD_AjustedQuantity As Double
    Private SD_AjustedAmount As Double

    Private SD_ExistingQty As Double
    Private SD_ExistingAmount As Double
    'Private POM_SaleTyps As Integer
    'Private POD_Quantity As String
    'Private POD_RateAmount As String
    'Private POD_Discount As String
    'Private POD_DiscountAmount As String
    'Private POD_Excise As String
    'Private POD_ExciseAmount As String
    'Private POD_Frieght As String
    'Private POD_FrieghtAmount As String
    'Private POD_VAT As String
    'Private POD_VATAmount As String
    'Private POD_CST As String
    'Private POD_CSTAmount As String
    'Private POD_RequiredDate As Date
    'Private POD_TotalAmount As String
    'Private POD_CompID As Integer
    'Private sPOM_DocRef As String
    'Private iPOD_Rejected As Integer
    'Private iPOD_Accepted As Integer
    'Private iPOD_ReceivedQty As Integer
    'Private iPOD_OrderedQty As Integer
    'Private POD_IPAddress As String
    'Private BatchNumber As String
    'Private POM_DEliveryChlnNo As String
    Public Property sSM_ID() As String
        Get
            Return (SM_ID)
        End Get
        Set(ByVal Value As String)
            SM_ID = Value
        End Set
    End Property

    Public Property sSM_AjustedDate() As Date
        Get
            Return (SM_AjustedDate)
        End Get
        Set(ByVal Value As Date)
            SM_AjustedDate = Value
        End Set
    End Property
    Public Property sSM_AjustedNo() As String
        Get
            Return (SM_AjustedNo)
        End Get
        Set(ByVal Value As String)
            SM_AjustedNo = Value
        End Set
    End Property
    Public Property sSM_AjustedBy() As String
        Get
            Return (SM_AjustedBy)
        End Get
        Set(ByVal Value As String)
            SM_AjustedBy = Value
        End Set
    End Property

    Public Property iSD_ID() As Integer
        Get
            Return (SD_ID)
        End Get
        Set(ByVal Value As Integer)
            SD_ID = Value
        End Set
    End Property
    Public Property iSD_MasterID() As Integer
        Get
            Return (SD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            SD_MasterID = Value
        End Set
    End Property

    Public Property iSD_Commodity() As Integer
        Get
            Return (SD_Commodity)
        End Get
        Set(ByVal Value As Integer)
            SD_Commodity = Value
        End Set
    End Property

    Public Property iSD_DescriptionID() As Integer
        Get
            Return (SD_DescriptionID)
        End Get
        Set(ByVal Value As Integer)
            SD_DescriptionID = Value
        End Set
    End Property
    Public Property iSD_UnitID() As Integer
        Get
            Return (SD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            SD_UnitID = Value
        End Set
    End Property

    Public Property iSD_HistoryID() As Integer
        Get
            Return (SD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            SD_HistoryID = Value
        End Set
    End Property


    Public Property iSD_StockID() As Integer
        Get
            Return (SD_StockID)
        End Get
        Set(ByVal Value As Integer)
            SD_StockID = Value
        End Set
    End Property





    Public Property dSD_TotRate() As Double
        Get
            Return (SD_TotRate)
        End Get
        Set(ByVal Value As Double)
            SD_TotRate = Value
        End Set
    End Property

    Public Property dSD_Rate() As Double
        Get
            Return (SD_Rate)
        End Get
        Set(ByVal Value As Double)
            SD_Rate = Value
        End Set
    End Property

    Public Property dSD_AjustedRate() As Double
        Get
            Return (SD_AjustedRate)
        End Get
        Set(ByVal Value As Double)
            SD_AjustedRate = Value
        End Set
    End Property
    Public Property iSM_YearID() As Integer
        Get
            Return (SM_YearID)
        End Get
        Set(ByVal Value As Integer)
            SM_YearID = Value
        End Set
    End Property
    Public Property iSM_CreatedBy() As Integer
        Get
            Return (SM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            SM_CreatedBy = Value
        End Set
    End Property


    Public Property sSD_Reason() As String
        Get
            Return (SD_Reason)
        End Get
        Set(ByVal Value As String)
            SD_Reason = Value
        End Set
    End Property

    Public Property sSd_Type() As String
        Get
            Return (Sd_Type)
        End Get
        Set(ByVal Value As String)
            Sd_Type = Value
        End Set
    End Property

    Public Property sSd_Flag() As String
        Get
            Return (Sd_Flag)
        End Get
        Set(ByVal Value As String)
            Sd_Flag = Value
        End Set
    End Property

    Public Property sSd_Status() As String
        Get
            Return (SD_Status)
        End Get
        Set(ByVal Value As String)
            SD_Status = Value
        End Set
    End Property

    Public Property dSD_AjustedQuantity() As Double
        Get
            Return (SD_AjustedQuantity)
        End Get
        Set(ByVal Value As Double)
            SD_AjustedQuantity = Value
        End Set
    End Property
    Public Property dSD_AjustedAmount() As Double
        Get
            Return (SD_AjustedAmount)
        End Get
        Set(ByVal Value As Double)
            SD_AjustedAmount = Value
        End Set
    End Property
    Public Property dSD_ExistingAmount() As Double
        Get
            Return (SD_ExistingAmount)
        End Get
        Set(ByVal Value As Double)
            SD_ExistingAmount = Value
        End Set
    End Property
    Public Property dSD_ExistingQty() As Double
        Get
            Return (SD_ExistingQty)
        End Get
        Set(ByVal Value As Double)
            SD_ExistingQty = Value
        End Set
    End Property


    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "select SL_ID,SL_ItemID,Inv_ID,SL_OrderID+'-'+Inv_Code as Inv_Code from inventory_master
                    Left Join stock_ledger on SL_ItemID=Inv_ID where SL_Commodity=" & iCommodity & " And SL_CompID =" & iCompID & ""
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescritionStart(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_CompID =" & iCompID & " And Inv_Code <> '' and Inv_Parent <> 0"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
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
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtNew.NewRow
                dRow("InvH_ID") = dt.Rows(i)("InvH_ID")
                dRow("INVH_PreDeterminedPrice") = dt.Rows(i)("INVH_PreDeterminedPrice") & " - " & objFas.FormatDtForRDBMS(dt.Rows(i)("InvH_EffeFrom"), "D")
                dtNew.Rows.Add(dRow)
            Next
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadStockRateQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer, ByVal SL_ID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtNew As New DataTable
        Dim dRow As DataRow
        Try
            dtNew.Columns.Add("SL_ClosingBalanceQty")
            dtNew.Columns.Add("PurchaseRate")
            dtNew.Columns.Add("SL_HistoryID")
            sSql = "select SL_ClosingBalanceQty,PurchaseRate,SL_HistoryID from stock_ledger where SL_ID = " & SL_ID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtNew.NewRow
                dRow("SL_ClosingBalanceQty") = dt.Rows(i)("SL_ClosingBalanceQty")
                dRow("PurchaseRate") = dt.Rows(i)("PurchaseRate")
                dRow("SL_HistoryID") = dt.Rows(i)("SL_HistoryID")
                dtNew.Rows.Add(dRow)
            Next
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function



    Public Function SaveStockAdgestment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dOrderDate As Date, ByVal objPO As clsStockAdjustment) As Integer
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Stock_Adgestment_Master where Sm_Code = '" & objPO.sSM_AjustedNo & "' and Sm_CompID =" & iCompID & " and Sm_YearID =" & objPO.iSM_YearID & ""
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sSql = "" : sSql = "Update Stock_Adgestment_Master set Sm_Adjested_Date  = " & objFas.FormatDtForRDBMS(dOrderDate, "I") & ""
                sSql = sSql & " Where Sm_Code = '" & objPO.sSM_AjustedNo & "' and Sm_CompID =" & iCompID & " and Sm_YearID=" & objPO.iSM_YearID & ""
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dr("sm_ID")
            Else
                iMax = objFasGnrl.GetMaxID(sNameSpace, iCompID, "Stock_Adgestment_Master", "Sm_ID", "SM_CompID")
                sSql = "" : sSql = "Insert into Stock_Adgestment_Master(Sm_ID,Sm_Adjested_Date,Sm_Code,Sm_CompID,"
                sSql = sSql & "Sm_YearID,Sm_Flag,Sm_CreatedBy,sm_CreatedOn)Values(" & iMax & "," & objFas.FormatDtForRDBMS(dOrderDate, "I") & ",'" & objPO.sSM_AjustedNo & "'," & iCompID & ","
                sSql = sSql & "" & objPO.iSM_YearID & ",'W'," & objPO.iSM_CreatedBy & ",GetDate())"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If
            dr.Close()
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveStockAdgestmentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dRequiredDate As Date, ByVal objPO As clsStockAdjustment)
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Stock_Adgestment_Details where Sd_MasterID = " & objPO.iSD_MasterID & " and Sd_CommodityID = " & objPO.iSD_MasterID & " and "
            sSql = sSql & "Sd_DescriptionID = " & objPO.iSD_DescriptionID & " and Sd_HistoryID =" & objPO.iSD_HistoryID & " and Sd_CompID= " & iCompID & ""
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                sSql = "" : sSql = "Update Stock_Adgestment_Details set Sd_StockID = " & objPO.iSD_StockID & ",Sd_CommodityID='" & objPO.iSD_Commodity & "',Sd_DescriptionID = '" & objPO.SD_DescriptionID & "',Sd_UnitID=" & objPO.iSD_UnitID & ","
                sSql = sSql & "Sd_Rate = '" & objPO.dSD_Rate & "',Sd_Adjested_Date=" & objFas.FormatDtForRDBMS(objPO.sSM_AjustedDate, "I") & ",Sd_Reason='" & objPO.sSD_Reason & "',"
                sSql = sSql & "Sd_AdgestedQty = " & objPO.dSD_AjustedQuantity & ",Sd_AdgestedAmount = " & objPO.dSD_AjustedAmount & ",Sd_ExistingQty=" & objPO.dSD_ExistingQty & ","
                sSql = sSql & "Sd_ExistingAmount='" & objPO.dSD_ExistingAmount & "',Sd_CreatedBy='" & objPO.iSM_CreatedBy & "',Sd_CreatedOn=GetDate(),"
                sSql = sSql & "Sd_CompID=" & iCompID & ",sd_Status='W' where Sd_MasterID = " & objPO.iSD_MasterID & " and "
                sSql = sSql & "Sd_CommodityID = " & objPO.iSD_Commodity & " and Sd_DescriptionID = " & objPO.iSD_DescriptionID & "  and "
                sSql = sSql & "Sd_HistoryID =" & objPO.iSD_HistoryID & " and sd_CompID = " & iCompID & ""
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objFasGnrl.GetMaxID(sNameSpace, iCompID, "Stock_Adgestment_Details", "SD_ID", "SD_CompID")
                sSql = "" : sSql = "Insert into Stock_Adgestment_Details(SD_ID,sd_MasterID,Sd_StockID,Sd_CommodityID,"
                sSql = sSql & "Sd_DescriptionID,Sd_HistoryID,Sd_UnitID,Sd_Rate,Sd_Flag,"
                sSql = sSql & "Sd_Adjested_Date,Sd_Reason,Sd_AdgestedQty,Sd_AdgestedAmount,"
                sSql = sSql & "Sd_ExistingQty,Sd_ExistingAmount,Sd_CreatedBy,Sd_CreatedOn,"
                sSql = sSql & "Sd_CompID,Sd_Status)"
                sSql = sSql & "Values(" & iMax & "," & objPO.iSD_MasterID & "," & objPO.iSD_StockID & "," & objPO.iSD_Commodity & ","
                sSql = sSql & "" & objPO.iSD_DescriptionID & "," & objPO.iSD_HistoryID & "," & objPO.iSD_UnitID & "," & objPO.dSD_Rate & ",'" & objPO.sSd_Flag & "',"
                sSql = sSql & "" & objFas.FormatDtForRDBMS(objPO.sSM_AjustedDate, "I") & ",'" & objPO.sSD_Reason & "'," & objPO.dSD_AjustedQuantity & "," & objPO.dSD_AjustedAmount & ","
                sSql = sSql & "" & objPO.dSD_ExistingQty & "," & objPO.dSD_ExistingAmount & ",'" & objPO.iSM_CreatedBy & "',GetDate()," & iCompID & ",'" & objPO.sSd_Status & "')"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Shared Function LoadStockRateQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer, ByVal GInID As Integer, ByVal OrderId As Integer)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable, dtNew As New DataTable
    '    Dim dRow As DataRow
    '    Try
    '        dtNew.Columns.Add("SL_PurchaseQty")
    '        dtNew.Columns.Add("PurchaseRate")
    '        dtNew.Columns.Add("SL_HistoryID")
    '        sSql = "select SL_PurchaseQty,PurchaseRate,SL_HistoryID from stock_ledger where SL_OrderID = " & OrderId & " and SL_CompID =" & iCompID & " and SL_GINID=" & GInID & ""
    '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        For i = 0 To dt.Rows.Count - 1
    '            dRow = dtNew.NewRow
    '            dRow("SL_PurchaseQty") = dt.Rows(i)("SL_PurchaseQty")
    '            dRow("PurchaseRate") = dt.Rows(i)("PurchaseRate")
    '            dRow("SL_HistoryID") = dt.Rows(i)("SL_HistoryID")
    '            dtNew.Rows.Add(dRow)
    '        Next
    '        Return dtNew
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function GetPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from inventory_master_History where InvH_ID =" & iHistoryID & " and InvH_CompID =" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iTransactionID As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Stock_Adgestment_Master Set sm_Status='A',sm_ApporvedBy=" & iUserID & ",sm_ApprovedOn=GetDate() "
            sSql = sSql & "Where sm_Code='" & iTransactionID & "' And sm_CompID=" & iCompID & " and sm_YearID = " & iYearID & ""
            objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As String) As String
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select sm_Status From Stock_Adgestment_Master Where sm_ID='" & iTransactionID & "' And sm_CompID=" & iCompID & " and sm_YearID = " & iYearID & ""
            sStatus = objDB.SQLGetDescription(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function


    'Public Shared Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim bCheckO As Boolean
    '    Dim bCheckP As Boolean
    '    Try
    '        If sSearch <> "" Then
    '            bCheckO = DBHelper.DBCheckForRecord(sNameSpace, "Select * From Purchase_Order_Master where POM_OrderNo ='" & sSearch & "' And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & "")
    '            If bCheckO = True Then
    '                sSql = "Select POM_ID,POM_OrderNo From Purchase_Order_Master where POM_OrderNo ='" & sSearch & "' And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & ""
    '                dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '                'Else
    '                '    bCheckP = DBHelper.DBCheckForRecord(sNameSpace, "Select * From Purchase_Order_Master where POM_Supplier='" & sSearch & "' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & "")
    '                '    If bCheckP = True Then
    '                '        sSql = "Select SPO_ID,SPO_OrderCode From Sales_Proforma_Order where SPO_OrderType='S' And SPO_PartyCode ='" & sSearch & "' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & ""
    '                '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '                '    End If
    '            End If
    '        Else
    '            sSql = "Select POM_ID,POM_OrderNo From Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & " Order By POM_ID Desc"
    '            dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim bCheckO As Boolean
        Dim bCheckP As Boolean

        Try
            'If sSearch <> "" Then
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " and POM_Status<>'D' order by POM_ID desc"
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            ' End If
            '    bCheckO = DBHelper.DBCheckForRecord(sNameSpace, "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PGD_ReceivedQnt) As RecvedQty from purchase_order_master
            '        Left Join Purchase_Order_Details on POM_ID=POD_MasterID
            '        Left Join Purchase_GIN_Details on POM_ID=PGD_OrderID
            '        group by POM_OrderNo, POM_ID,POM_CompID,POM_YearID,POM_Status having 
            '        Convert(varchar, sum(PGD_ReceivedQnt)) Is NULL Or sum(PGD_ReceivedQnt)<sum(POD_Quantity) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " And POM_OrderNo ='" & sSearch & "' and POM_Status<>'D' order by POM_ID desc")
            '    If bCheckO = True Then
            '        sSql = "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PGD_ReceivedQnt) As RecvedQty from purchase_order_master
            '        Left Join Purchase_Order_Details on POM_ID=POD_MasterID
            '        Left Join Purchase_GIN_Details on POM_ID=PGD_OrderID
            '        group by POM_OrderNo, POM_ID,POM_CompID,POM_YearID,POM_Status having 
            '        Convert(varchar, sum(PGD_ReceivedQnt)) Is NULL Or sum(PGD_ReceivedQnt)<sum(POD_Quantity) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " And POM_OrderNo ='" & sSearch & "' and POM_Status<>'D' order by POM_ID desc"
            '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
            '    End If
            'Else
            '    sSql = "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PGD_ReceivedQnt) As RecvedQty from purchase_order_master
            '        Left Join Purchase_Order_Details on POM_ID=POD_MasterID
            '        Left Join Purchase_GIN_Details on POM_ID=PGD_OrderID
            '        group by POM_OrderNo, POM_ID,POM_CompID,POM_YearID,POM_Status having 
            '        Convert(varchar, sum(PGD_ReceivedQnt)) Is NULL Or sum(PGD_ReceivedQnt)<sum(POD_Quantity) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " and POM_Status<>'D' order by POM_ID desc"
            '    dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
            ' End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPurchaseORderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtPdetails As New DataTable
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
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("ExciseDuty")
            dt.Columns.Add("ExciseAmt")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")
            dt.Columns.Add("TotalAmount")
            sSql = "Select * from Purchase_Order_Details where POD_MasterID =" & iMasterID & " and POD_CompID=" & iCompID & " and  POD_Status='W' order by POD_ID"
            dtPdetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dtPdetails.Rows.Count > 0 Then
                For i = 0 To dtPdetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("ID") = dtPdetails.Rows(i)("POD_ID")
                    dRow("CommodityID") = dtPdetails.Rows(i)("POD_Commodity")
                    dRow("DescriptionID") = dtPdetails.Rows(i)("POD_DescriptionID")
                    dRow("HistoryID") = dtPdetails.Rows(i)("POD_HistoryID")
                    dRow("UnitsID") = dtPdetails.Rows(i)("POD_Unit")
                    iSlNo = iSlNo + 1
                    dRow("SLNO") = iSlNo
                    dRow("Goods") = objDB.SQLExecuteScalar(sNameSpace, "Select Inv_Code from Inventory_Master where Inv_ID='" & dtPdetails.Rows(i)("POD_DescriptionID") & "' and Inv_compid=" & iCompID & "")
                    dRow("Units") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & dtPdetails.Rows(i)("POD_Unit") & "' and Mas_compid=" & iCompID & "")
                    dRow("Rate") = dtPdetails.Rows(i)("POD_Rate")
                    If IsDBNull(dtPdetails.Rows(i)("POD_Quantity")) = False Then
                        dRow("Quantity") = Math.Round(dtPdetails.Rows(i)("POD_Quantity"), 2)
                    End If
                    dRow("RateAmount") = dtPdetails.Rows(i)("POD_RateAmount")
                    dRow("Discount") = dtPdetails.Rows(i)("POD_Discount")
                    dRow("DiscountAmt") = dtPdetails.Rows(i)("POD_DiscountAmount")
                    dRow("ExciseDuty") = dtPdetails.Rows(i)("POD_Excise")
                    dRow("ExciseAmt") = dtPdetails.Rows(i)("POD_ExciseAmount")
                    dRow("VAT") = dtPdetails.Rows(i)("POD_VAT")
                    dRow("VATAmt") = dtPdetails.Rows(i)("POD_VATAmount")
                    dRow("CST") = dtPdetails.Rows(i)("POD_CST")
                    dRow("CSTAmount") = dtPdetails.Rows(i)("POD_CSTAmount")
                    dRow("TotalAmount") = dtPdetails.Rows(i)("POD_TotalAmount")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GeneratePurchaseOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = "", sMonth As String = "", sMonthCode As String = ""
        Dim sDate As String = "", sMaxID As String = "", sLastID As String = "", sSDate As String = ""
        Try
            sMaximumID = objDB.SQLGetDescription(sNameSpace, "Select Top 1 sm_ID From Stock_Adgestment_master where sm_COmpID = " & iCompID & " Order By sm_ID Desc")
            sYear = objDB.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDB.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDB.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
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
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " and POM_Status<>'D' order by POM_ID desc"
            sSql = "Select sm_ID,sm_Code from Stock_Adgestment_Master where sm_CompID=" & iCompID & " and sm_YearID =" & iYearID & "  order by sm_ID desc"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dOrderDate As Date, ByVal objPO As clsPurchaseOrder) As Integer
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Purchase_Order_Master where POM_OrderNo = '" & objPO.sPOM_OrderNo & "' and POM_CompID =" & iCompID & " and POM_YearID =" & objPO.iPOM_YearID & ""
            dr = objDB.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sSql = "" : sSql = "Update Purchase_Order_Master set POM_Supplier = " & objPO.iPOM_Supplier & ",POM_MPayment=" & objPO.iPOM_MethodofPayment & ",POM_PaymentTerms=" & objPO.iPOM_Paymentterms & ",POM_ModeOfShipping = " & objPO.iPOM_ModeOfShipping & ",POM_DSchdule=" & objPO.iPOM_DSchdule & " "
                sSql = sSql & "Where POM_OrderNo = '" & objPO.sPOM_OrderNo & "' and POM_CompID =" & iCompID & " and POM_YearID=" & objPO.iPOM_YearID & ""
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dr("POM_ID")
            Else
                iMax = objFasGnrl.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Master", "POM_ID", "POM_CompID")
                sSql = "" : sSql = "Insert into Purchase_Order_Master(POM_ID,POM_OrderDate,POM_OrderNo,POM_Supplier,"
                sSql = sSql & "POM_ModeOfShipping,POM_Status,POM_CreatedBy,POM_CreatedOn,"
                sSql = sSql & "POM_YearID,POM_CompID,POM_MPayment,POM_PaymentTerms,POM_DSchdule,POM_TypeOfPurchase,POM_CstCategory)Values(" & iMax & "," & objFas.FormatDtForRDBMS(dOrderDate, "I") & ",'" & objPO.sPOM_OrderNo & "'," & objPO.iPOM_Supplier & ","
                sSql = sSql & "" & objPO.iPOM_ModeOfShipping & ",'" & objPO.sPOM_Status & "'," & objPO.iPOM_CreatedBy & ",GetDate(),"
                sSql = sSql & "" & objPO.iPOM_YearID & "," & iCompID & "," & objPO.iPOM_MethodofPayment & "," & objPO.iPOM_Paymentterms & "," & objPO.iPOM_DSchdule & "," & objPO.iPOM_SaleType & "," & objPO.iPOM_iCSTCtgry & ")"
                objDB.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If
            dr.Close()
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAjustmentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
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
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")
            dt.Columns.Add("TotalAmount")
            sSql = "Select * from Stock_Adgestment_Details where sD_MasterID =" & iMasterID & " and sd_CompID=" & iCompID & "  order by sd_ID"
            ds = objDB.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("ID") = ds.Tables(0).Rows(i)("sd_ID")
                    dRow("CommodityID") = ds.Tables(0).Rows(i)("Sd_CommodityID")
                    dRow("DescriptionID") = ds.Tables(0).Rows(i)("Sd_DescriptionID")
                    dRow("HistoryID") = ds.Tables(0).Rows(i)("Sd_HistoryID")
                    dRow("UnitsID") = ds.Tables(0).Rows(i)("Sd_UnitID")
                    iSlNo = iSlNo + 1
                    dRow("SLNO") = iSlNo
                    dRow("Goods") = objDB.SQLExecuteScalar(sNameSpace, "Select Inv_Code from Inventory_Master where Inv_ID='" & ds.Tables(0).Rows(i)("Sd_DescriptionID") & "' and Inv_compid=" & iCompID & "")
                    dRow("Units") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & ds.Tables(0).Rows(i)("Sd_UnitID") & "' and Mas_compid=" & iCompID & "")
                    dRow("Rate") = ds.Tables(0).Rows(i)("sd_Rate")
                    If IsDBNull(ds.Tables(0).Rows(i)("Sd_AdgestedQty")) = False Then
                        dRow("Quantity") = Math.Round(ds.Tables(0).Rows(i)("Sd_AdgestedQty"), 2)
                    End If

                    dRow("RateAmount") = ds.Tables(0).Rows(i)("Sd_AdgestedAmount")
                    dRow("Discount") = 0
                    dRow("DiscountAmt") = 0
                    dRow("VAT") = 0
                    dRow("VATAmt") = 0
                    dRow("CST") = 0
                    dRow("CSTAmount") = 0
                    dRow("TotalAmount") = 0
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
            'sSql = "Update Purchase_Order_Details set POD_Status='D' Where POD_MasterID in(select POM_ID from Purchase_Order_Master "
            'sSql = sSql & "where POM_OrderNo='" & OrderNo & "' and POM_YearID =" & iYearId & " and POM_COmpID =" & iCompID & " and  POM_Status='W') and POD_DescriptionID=" & DcritionID & " and POD_CompID = " & iCompID & ""
            'objDB.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeleteOrderValuesFromMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal OrderNo As String)
        Dim sSql As String = ""
        Try
            'sSql = "Update Purchase_Order_Master set POM_Status='D' where POM_OrderNo='" & OrderNo & "' and POM_YearID =" & iYearId & " and POM_COmpID =" & iCompID & " and  POM_Status='W'"
            'objDB.SQLExecuteNonQuery(sNameSpace, sSql)
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
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_Unit")
                dRow("Mas_Desc") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & dt.Rows(0)("InvH_Unit") & "' and Mas_compid=" & iCompID & "")
                dtNew.Rows.Add(dRow)

                dRow = dtNew.NewRow
                dRow("Mas_ID") = dt.Rows(0)("InvH_AlterUnit")
                dRow("Mas_Desc") = objDB.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & dt.Rows(0)("InvH_AlterUnit") & "' and Mas_compid=" & iCompID & "")
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
            sSql = "" : sSql = "Select * from Stock_Adgestment_Master where sm_ID = " & iPomID & " and sm_CompID = " & iCompID & " and sm_YearID =" & iYearID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPurchaseOderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer, ByVal iCommodity As Integer, ByVal iDescriptionID As Integer, ByVal iHistoryID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Stock_Adgestment_details where sd_MasterID = " & iMasterID & " and sd_Commodity = " & iCommodity & " and "
            sSql = sSql & "sd_DescriptionID = " & iDescriptionID & " and  sd_HistoryID = " & iHistoryID & " and sd_CompID = " & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
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
            dr = objDB.SQLDataReader(sNameSpace, sSql)
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
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPaymentTerms(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=18 and Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDeliverySchdule(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=17 and Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadModeShiping(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=13 and Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadNumberOfDays(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=20 and Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMethodOfPayment(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=11 and Mas_Status='A'"
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPomID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtFRomTable As New DataTable
        Dim dRow As DataRow
        Dim subtotal As Double = 0
        Try
            dt.Columns.Add("POM_OrderNo")
            dt.Columns.Add("POM_OrderDate")
            dt.Columns.Add("POM_Supplier")
            dt.Columns.Add("POD_Commodity")
            dt.Columns.Add("POD_Rate")
            dt.Columns.Add("POD_Quantity")
            dt.Columns.Add("POD_CST")
            dt.Columns.Add("POD_VAT")
            dt.Columns.Add("POD_CSTAmount")
            dt.Columns.Add("POD_RateAmount")
            dt.Columns.Add("POD_Discount")
            dt.Columns.Add("POD_DiscountAmount")
            dt.Columns.Add("POD_TotalAmount")
            dt.Columns.Add("POD_VATAmount")
            dt.Columns.Add("POD_Unit")
            dt.Columns.Add("INV_Code")
            dt.Columns.Add("INV_Description")
            dt.Columns.Add("CSM_Name")
            dt.Columns.Add("CSM_Address")
            dt.Columns.Add("CSM_MobileNo")
            dt.Columns.Add("CSM_EmailID")
            dt.Columns.Add("Mas_Desc")
            dt.Columns.Add("CUST_CODE")
            dt.Columns.Add("CUST_COMM_ADDRESS")
            dt.Columns.Add("CUST_EMAIL")
            dt.Columns.Add("CUST_COMM_TEL")
            dt.Columns.Add("CUST_NAME")
            dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Mdate")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("POD_Excise")
            dt.Columns.Add("POD_ExciseAmount")
            dt.Columns.Add("POD_Frieght")
            dt.Columns.Add("POD_FrieghtAmount")
            sSql = "Select POM_OrderNo, Convert(VARCHAR(10), POM_OrderDate, 103)As POM_OrderDate, POM_Supplier, b.POD_Commodity, b.POD_Rate, b.POD_Quantity, "
            sSql = sSql & "b.POD_CST, Convert(money, b.POD_CSTAmount) As POD_CSTAmount, Convert(money, b.POD_RateAmount) As POD_RateAmount,"
            sSql = sSql & "Convert(money, b.POD_Discount)As POD_Discount,Convert(money,b.POD_DiscountAmount) As POD_DiscountAmount,"
            sSql = sSql & "Convert(money,b.POD_TotalAmount)As POD_TotalAmount,"
            sSql = sSql & "Convert(money,b.POD_VAT)As POD_VAT,Convert(money,b.POD_Excise)As POD_Excise,POD_Frieght,POD_FrieghtAmount,POD_ExciseAmount,Convert(money,b.POD_ExciseAmount)As POD_ExciseAmount,Convert(money,b.POD_VATAmount)As POD_VATAmount,b.POD_Unit, "
            sSql = sSql & "c.INV_Code,c.INV_Description,d.CSM_Name,d.CSM_Address,d.CSM_MobileNo,d.CSM_EmailID,e.Mas_Desc,m.CUST_CODE,m.CUST_Name,m.CUST_COMM_ADDRESS,m.CUST_EMAIL,m.CUST_COMM_TEL,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate "
            sSql = sSql & "From Purchase_Order_Master "
            sSql = sSql & "join Purchase_Order_Details b On POM_ID=" & iPomID & " And POM_ID=b.POD_MasterID And b.POD_Status <> 'D' "
            sSql = sSql & "Join Inventory_master_history InvH on  POD_HistoryID=InvH.InvH_ID "
            sSql = sSql & "Join Inventory_master c on  POD_DescriptionID=c.INV_ID "
            sSql = sSql & "Join CustomerSupplierMaster d On POM_Supplier=d.CSM_ID "
            sSql = sSql & "Join Acc_General_master e on b.POD_Unit=e.Mas_ID "
            sSql = sSql & "Join MST_CUSTOMER_MASTER m on b.POD_CompID=m.CUST_ID "
            dtFRomTable = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dtFRomTable.Rows.Count > 0 Then
                For i = 0 To dtFRomTable.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtFRomTable.Rows(i)("POM_OrderNo")) = False Then
                        dRow("POM_OrderNo") = dtFRomTable.Rows(i)("POM_OrderNo")
                    Else
                        dRow("POM_OrderNo") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POM_OrderDate")) = False Then
                        dRow("POM_OrderDate") = dtFRomTable.Rows(i)("POM_OrderDate")
                    Else
                        dRow("POM_OrderDate") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Commodity")) = False Then
                        dRow("POD_Commodity") = dtFRomTable.Rows(i)("POD_Commodity")
                    Else
                        dRow("POD_Commodity") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Rate")) = False Then
                        dRow("POD_Rate") = dtFRomTable.Rows(i)("POD_Rate")
                    Else
                        dRow("POD_Rate") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Quantity")) = False Then
                        dRow("POD_Quantity") = dtFRomTable.Rows(i)("POD_Quantity")
                    Else
                        dRow("POD_Quantity") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_CST")) = False Then
                        dRow("POD_CST") = dtFRomTable.Rows(i)("POD_CST")
                    Else
                        dRow("POD_CST") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_VAT")) = False Then
                        dRow("POD_VAT") = dtFRomTable.Rows(i)("POD_VAT")
                    Else
                        dRow("POD_VAT") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("POD_CSTAmount") = Convert.ToDouble(dtFRomTable.Rows(i)("POD_CSTAmount"))
                    Else
                        dRow("POD_CSTAmount") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_RateAmount")) = False Then
                        dRow("POD_RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dtFRomTable.Rows(i)("POD_RateAmount") - dtFRomTable.Rows(i)("POD_DiscountAmount"))))
                        subtotal = subtotal + dRow("POD_RateAmount")
                    Else
                        dRow("POD_RateAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Discount")) = False Then
                        dRow("POD_Discount") = dtFRomTable.Rows(i)("POD_Discount")
                    Else
                        dRow("POD_Discount") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_DiscountAmount")) = False Then
                        dRow("POD_DiscountAmount") = dtFRomTable.Rows(i)("POD_DiscountAmount")
                    Else
                        dRow("POD_DiscountAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_TotalAmount")) = False Then
                        dRow("POD_TotalAmount") = dtFRomTable.Rows(i)("POD_TotalAmount")
                    Else
                        dRow("POD_TotalAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_VATAmount")) = False Then
                        dRow("POD_VATAmount") = dtFRomTable.Rows(i)("POD_VATAmount")
                    Else
                        dRow("POD_VATAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Unit")) = False Then
                        dRow("POD_Unit") = dtFRomTable.Rows(i)("POD_Unit")
                    Else
                        dRow("POD_Unit") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("INV_Code")) = False Then
                        dRow("INV_Code") = dtFRomTable.Rows(i)("INV_Code")
                    Else
                        dRow("INV_Code") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("INV_Description")) = False Then
                        dRow("INV_Description") = dtFRomTable.Rows(i)("INV_Description")
                    Else
                        dRow("INV_Description") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("CSM_Name")) = False Then
                        dRow("CSM_Name") = dtFRomTable.Rows(i)("CSM_Name")
                    Else
                        dRow("CSM_Name") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("CSM_Address")) = False Then
                        dRow("CSM_Address") = dtFRomTable.Rows(i)("CSM_Address")
                    Else
                        dRow("CSM_Address") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("CSM_Address")) = False Then
                        dRow("CSM_Address") = dtFRomTable.Rows(i)("CSM_Address")
                    Else
                        dRow("CSM_Address") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("CSM_MobileNo")) = False Then
                        dRow("CSM_MobileNo") = dtFRomTable.Rows(i)("CSM_MobileNo")
                    Else
                        dRow("CSM_MobileNo") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("CSM_EmailID")) = False Then
                        dRow("CSM_EmailID") = dtFRomTable.Rows(i)("CSM_EmailID")
                    Else
                        dRow("CSM_EmailID") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("Mas_Desc")) = False Then
                        dRow("Mas_Desc") = dtFRomTable.Rows(i)("Mas_Desc")
                    Else
                        dRow("Mas_Desc") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("CUST_CODE")) = False Then
                        dRow("CUST_CODE") = dtFRomTable.Rows(i)("CUST_CODE")
                    Else
                        dRow("CUST_CODE") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("CUST_NAME")) = False Then
                        dRow("CUST_NAME") = dtFRomTable.Rows(i)("CUST_NAME")
                    Else
                        dRow("CUST_NAME") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("CUST_COMM_ADDRESS")) = False Then
                        dRow("CUST_COMM_ADDRESS") = dtFRomTable.Rows(i)("CUST_COMM_ADDRESS")
                    Else
                        dRow("CUST_COMM_ADDRESS") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("CUST_EMAIL")) = False Then
                        dRow("CUST_EMAIL") = dtFRomTable.Rows(i)("CUST_EMAIL")
                    Else
                        dRow("CUST_EMAIL") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("CUST_COMM_TEL")) = False Then
                        dRow("CUST_COMM_TEL") = dtFRomTable.Rows(i)("CUST_COMM_TEL")
                    Else
                        dRow("CUST_COMM_TEL") = ""
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("INVH_MRP")) = False Then
                        dRow("INVH_MRP") = dtFRomTable.Rows(i)("INVH_MRP")
                    Else
                        dRow("INVH_MRP") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("INVH_Mdate")) = False Then
                        dRow("INVH_Mdate") = dtFRomTable.Rows(i)("INVH_Mdate")
                    Else
                        dRow("INVH_Mdate") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("INVH_Edate")) = False Then
                        dRow("INVH_Edate") = dtFRomTable.Rows(i)("INVH_Edate")
                    Else
                        dRow("INVH_Edate") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_Excise")) = False Then
                        dRow("POD_Excise") = dtFRomTable.Rows(i)("POD_Excise")
                    Else
                        dRow("POD_Excise") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("POD_ExciseAmount")) = False Then
                        dRow("POD_ExciseAmount") = dtFRomTable.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("POD_ExciseAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_Frieght")) = False Then
                        dRow("POD_Frieght") = dtFRomTable.Rows(i)("POD_Frieght")
                    Else
                        dRow("POD_Frieght") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("POD_FrieghtAmount")) = False Then
                        dRow("POD_FrieghtAmount") = dtFRomTable.Rows(i)("POD_FrieghtAmount")
                    Else
                        dRow("POD_FrieghtAmount") = 0
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Shared Function RemoveDublicate(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("POD_VAT"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("POD_VAT"), String.Empty)
                End If
            Next
            For Each DataRow As DataRow In duplicateList
                dt.Rows.Remove(DataRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
