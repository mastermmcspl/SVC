Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsAllocateSalesOrder
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iSAM_ID As Integer
    Private iSAM_OrderNo As Integer
    Private iSAM_Party As Integer
    Private sSAM_Remarks As String
    Private sSAM_Status As String
    Private iSAM_CompID As Integer
    Private iSAM_YearID As Integer
    Private iSAM_CreatedBy As Integer
    Private iSAM_ApprovedBy As Integer

    Private iSAM_GrandDiscount As Double
    Private iSAM_GrandDiscountAmt As Double
    Private iSAM_GrandTotal As Double
    Private iSAM_GrandTotalAmt As Double
    Private sSAM_Code As String

    Private iSAD_ID As Integer
    Private iSAD_MasterID As Integer
    Private iSAD_Commodity As Integer
    Private iSAD_DescID As Integer
    Private iSAD_HisotryID As Integer
    Private iSAD_OpeningBal As Integer
    Private iSAD_UnitID As Integer
    Private mSAD_MRP As Decimal
    Private iSAD_OrderQnt As Double
    Private mSAD_OrderAmount As Decimal
    Private mSAD_Discount As Decimal
    Private mSAD_DiscountAmount As Decimal
    Private mSAD_TotalAmount As Decimal

    Private iSAD_PlacedQnt As Double
    Private mSAD_PlacedQntAmount As Decimal
    Private mSAD_PlacedDiscount As Decimal
    Private mSAD_PlacedDiscountAmount As Decimal
    Private mSAD_PlacedTotalAmount As Decimal

    Private iSAD_ClosingBal As Double
    Private iSAD_CompID As Integer
    Private iSAD_YearID As Integer

    Private sSAM_Operation As String
    Private sSAM_IPAddress As String

    Private sSAD_Operation As String
    Private sSAD_IPAddress As String

    Private mSAD_VAT As Double
    Private mSAD_VATAmount As Double

    Private mSAD_CST As Double
    Private mSAD_CSTAmount As Double

    Private mSAD_Exice As Double
    Private mSAD_ExiceAmount As Double
    Private sSAM_DispatchFlag As String
    Private iSAD_PendingQty As Double

    Public Property SAM_Code() As String
        Get
            Return (sSAM_Code)
        End Get
        Set(ByVal Value As String)
            sSAM_Code = Value
        End Set
    End Property
    Public Property SAD_PendingQty() As Double
        Get
            Return (iSAD_PendingQty)
        End Get
        Set(ByVal Value As Double)
            iSAD_PendingQty = Value
        End Set
    End Property
    Public Property SAM_DispatchFlag() As String
        Get
            Return (sSAM_DispatchFlag)
        End Get
        Set(ByVal Value As String)
            sSAM_DispatchFlag = Value
        End Set
    End Property
    Public Property SAM_GrandDiscount() As Double
        Get
            Return (iSAM_GrandDiscount)
        End Get
        Set(ByVal Value As Double)
            iSAM_GrandDiscount = Value
        End Set
    End Property
    Public Property SAM_GrandDiscountAmt() As Double
        Get
            Return (iSAM_GrandDiscountAmt)
        End Get
        Set(ByVal Value As Double)
            iSAM_GrandDiscountAmt = Value
        End Set
    End Property
    Public Property SAM_GrandTotal() As Double
        Get
            Return (iSAM_GrandTotal)
        End Get
        Set(ByVal Value As Double)
            iSAM_GrandTotal = Value
        End Set
    End Property
    Public Property SAM_GrandTotalAmt() As Double
        Get
            Return (iSAM_GrandTotalAmt)
        End Get
        Set(ByVal Value As Double)
            iSAM_GrandTotalAmt = Value
        End Set
    End Property
    Public Property SAM_Operation() As String
        Get
            Return (sSAM_Operation)
        End Get
        Set(ByVal Value As String)
            sSAM_Operation = Value
        End Set
    End Property
    Public Property SAM_IPAddress() As String
        Get
            Return (sSAM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSAM_IPAddress = Value
        End Set
    End Property
    Public Property SAD_Operation() As String
        Get
            Return (sSAD_Operation)
        End Get
        Set(ByVal Value As String)
            sSAD_Operation = Value
        End Set
    End Property
    Public Property SAD_IPAddress() As String
        Get
            Return (sSAD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSAD_IPAddress = Value
        End Set
    End Property
    Public Property SAM_ID() As Integer
        Get
            Return (iSAM_ID)
        End Get
        Set(ByVal Value As Integer)
            iSAM_ID = Value
        End Set
    End Property
    Public Property SAM_OrderNo() As Integer
        Get
            Return (iSAM_OrderNo)
        End Get
        Set(ByVal Value As Integer)
            iSAM_OrderNo = Value
        End Set
    End Property
    Public Property SAM_Party() As Integer
        Get
            Return (iSAM_Party)
        End Get
        Set(ByVal Value As Integer)
            iSAM_Party = Value
        End Set
    End Property
    Public Property SAM_Remarks() As String
        Get
            Return (sSAM_Remarks)
        End Get
        Set(ByVal Value As String)
            sSAM_Remarks = Value
        End Set
    End Property
    Public Property SAM_Status() As String
        Get
            Return (sSAM_Status)
        End Get
        Set(ByVal Value As String)
            sSAM_Status = Value
        End Set
    End Property
    Public Property SAM_CompID() As Integer
        Get
            Return (iSAM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSAM_CompID = Value
        End Set
    End Property

    Public Property SAM_YearID() As Integer
        Get
            Return (iSAM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSAM_YearID = Value
        End Set
    End Property
    Public Property SAM_CreatedBy() As Integer
        Get
            Return (iSAM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSAM_CreatedBy = Value
        End Set
    End Property
    Public Property SAM_ApprovedBy() As Integer
        Get
            Return (iSAM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iSAM_ApprovedBy = Value
        End Set
    End Property

    Public Property SAD_ID() As Integer
        Get
            Return (iSAD_ID)
        End Get
        Set(ByVal Value As Integer)
            iSAD_ID = Value
        End Set
    End Property
    Public Property SAD_MasterID() As Integer
        Get
            Return (iSAD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iSAD_MasterID = Value
        End Set
    End Property
    Public Property SAD_Commodity() As Integer
        Get
            Return (iSAD_Commodity)
        End Get
        Set(ByVal Value As Integer)
            iSAD_Commodity = Value
        End Set
    End Property
    Public Property SAD_DescID() As Integer
        Get
            Return (iSAD_DescID)
        End Get
        Set(ByVal Value As Integer)
            iSAD_DescID = Value
        End Set
    End Property
    Public Property SAD_HisotryID() As Integer
        Get
            Return (iSAD_HisotryID)
        End Get
        Set(ByVal Value As Integer)
            iSAD_HisotryID = Value
        End Set
    End Property
    Public Property SAD_OpeningBal() As Integer
        Get
            Return (iSAD_OpeningBal)
        End Get
        Set(ByVal Value As Integer)
            iSAD_OpeningBal = Value
        End Set
    End Property
    Public Property SAD_UnitID() As Integer
        Get
            Return (iSAD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            iSAD_UnitID = Value
        End Set
    End Property
    Public Property SAD_MRP() As Decimal
        Get
            Return (mSAD_MRP)
        End Get
        Set(ByVal Value As Decimal)
            mSAD_MRP = Value
        End Set
    End Property
    Public Property SAD_OrderQnt() As Double
        Get
            Return (iSAD_OrderQnt)
        End Get
        Set(ByVal Value As Double)
            iSAD_OrderQnt = Value
        End Set
    End Property
    Public Property SAD_OrderAmount() As Decimal
        Get
            Return (mSAD_OrderAmount)
        End Get
        Set(ByVal Value As Decimal)
            mSAD_OrderAmount = Value
        End Set
    End Property
    Public Property SAD_Discount() As Decimal
        Get
            Return (mSAD_Discount)
        End Get
        Set(ByVal Value As Decimal)
            mSAD_Discount = Value
        End Set
    End Property
    Public Property SAD_DiscountAmount() As Decimal
        Get
            Return (mSAD_DiscountAmount)
        End Get
        Set(ByVal Value As Decimal)
            mSAD_DiscountAmount = Value
        End Set
    End Property
    Public Property SAD_TotalAmount() As Decimal
        Get
            Return (mSAD_TotalAmount)
        End Get
        Set(ByVal Value As Decimal)
            mSAD_TotalAmount = Value
        End Set
    End Property
    Public Property SAD_PlacedQnt() As Double
        Get
            Return (iSAD_PlacedQnt)
        End Get
        Set(ByVal Value As Double)
            iSAD_PlacedQnt = Value
        End Set
    End Property
    Public Property SAD_PlacedQntAmount() As Decimal
        Get
            Return (mSAD_PlacedQntAmount)
        End Get
        Set(ByVal Value As Decimal)
            mSAD_PlacedQntAmount = Value
        End Set
    End Property
    Public Property SAD_PlacedDiscount() As Decimal
        Get
            Return (mSAD_PlacedDiscount)
        End Get
        Set(ByVal Value As Decimal)
            mSAD_PlacedDiscount = Value
        End Set
    End Property
    Public Property SAD_PlacedDiscountAmount() As Decimal
        Get
            Return (mSAD_PlacedDiscountAmount)
        End Get
        Set(ByVal Value As Decimal)
            mSAD_PlacedDiscountAmount = Value
        End Set
    End Property
    Public Property SAD_VAT() As Double
        Get
            Return (mSAD_VAT)
        End Get
        Set(ByVal Value As Double)
            mSAD_VAT = Value
        End Set
    End Property
    Public Property SAD_VATAmount() As Double
        Get
            Return (mSAD_VATAmount)
        End Get
        Set(ByVal Value As Double)
            mSAD_VATAmount = Value
        End Set
    End Property
    Public Property SAD_CST() As Double
        Get
            Return (mSAD_CST)
        End Get
        Set(ByVal Value As Double)
            mSAD_CST = Value
        End Set
    End Property
    Public Property SAD_CSTAmount() As Double
        Get
            Return (mSAD_CSTAmount)
        End Get
        Set(ByVal Value As Double)
            mSAD_CSTAmount = Value
        End Set
    End Property
    Public Property SAD_Exice() As Double
        Get
            Return (mSAD_Exice)
        End Get
        Set(ByVal Value As Double)
            mSAD_Exice = Value
        End Set
    End Property
    Public Property SAD_ExiceAmount() As Double
        Get
            Return (mSAD_ExiceAmount)
        End Get
        Set(ByVal Value As Double)
            mSAD_ExiceAmount = Value
        End Set
    End Property
    Public Property SAD_PlacedTotalAmount() As Decimal
        Get
            Return (mSAD_PlacedTotalAmount)
        End Get
        Set(ByVal Value As Decimal)
            mSAD_PlacedTotalAmount = Value
        End Set
    End Property
    Public Property SAD_ClosingBal() As Double
        Get
            Return (iSAD_ClosingBal)
        End Get
        Set(ByVal Value As Double)
            iSAD_ClosingBal = Value
        End Set
    End Property
    Public Property SAD_CompID() As Integer
        Get
            Return (iSAD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSAD_CompID = Value
        End Set
    End Property
    Public Property SAD_YearID() As Integer
        Get
            Return (iSAD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSAD_YearID = Value
        End Set
    End Property
    Public Function LoadOrderNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select Distinct(SPO_ID) As SPO_ID,SPO_OrderCode From Sales_Proforma_Order A
            '        Left Join Sales_Allocate_Master B On B.SAM_OrderNo=a.SPO_ID
            '        Where (A.SPO_ID In (Select SAM_OrderNo From Sales_Allocate_Master Where SAM_Status <> 'A' And SAM_DispatchFlag <> 1) Or A.SPO_ID Not In (Select SAM_OrderNo From Sales_Allocate_Master)) And A.SPO_DispatchFlag <> 1 And A.SPO_OrderType='S' And A.SPO_CompID=" & iCompID & " and A.SPO_YearID =" & iYearID & " Order By SPO_OrderCode Desc"

            sSql = "Select Distinct(SPO_ID) As SPO_ID,SPO_OrderCode From Sales_Proforma_Order A
                    Left Join Sales_Allocate_Master B On B.SAM_OrderNo=a.SPO_ID 
                    Where A.SPO_DispatchFlag <> 1 And A.SPO_OrderType='S' And A.SPO_CompID=" & iCompID & " and A.SPO_YearID =" & iYearID & " And A.SPO_BranchID=" & iBranch & " Order By SPO_OrderCode Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select BM_ID,BM_Name from sales_Buyers_Masters where BM_CompID=" & iCompID & ""
            'sSql = "Select ACM_ID,ACM_Name from Acc_Customer_Master where ACM_Status='A' And ACM_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iorderno As Integer) As Integer
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iParty As Integer = 0
        Try
            sSql = "Select SPO_PartyName from Sales_Proforma_Order where SPO_CompID=" & iCompID & " and SPO_ID=" & iorderno & " and SPO_YearID =" & iYearID & ""
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("SPO_PartyName")) = False Then
                    iParty = dr("SPO_PartyName")
                End If
            End If
            dr.Close()
            Return iParty
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAllocationID As Integer) As String
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select SAM_Status from Sales_Allocate_Master where SAM_CompID=" & iCompID & " and SAM_ID=" & iAllocationID & " and SAM_YearID =" & iYearID & ""
            sStatus = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllocateOrderID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderNo As Integer) As Integer
        Dim sSql As String
        Dim iSamId As Integer = 0
        Try
            sSql = "Select SAM_ID from Sales_Allocate_Master where SAM_OrderNo =" & iOrderNo & " and SAM_CompID =" & iCompID & ""
            iSamId = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iSamId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iAllocationID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Allocate_Master Set SAM_Status='A',SAM_ApprovedBy=" & iUserID & ",SAM_ApprovedOn=GetDate(),SAM_Operation='A',SAM_IPAddress='" & iIPAddress & "' Where SAM_ID=" & iAllocationID & " And SAM_CompID=" & iCompID & " and SAM_YearID =" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RejectMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iOrderNo As Integer, ByVal iAllocationID As Integer, ByVal sRemarks As String, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal dRejectedQty As Double) As Integer
        Dim sSql As String = ""
        Dim iPendingQty As Double
        Dim dRejectQnt As Double
        Try
            sSql = "" : sSql = "Select SAD_PendingQty From Sales_Allocate_Details Where SAD_ID in (select max(SAD_ID) from Sales_Allocate_Details,Sales_Allocate_Master Where SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderNo & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ) "
            iPendingQty = objDBL.SQLGetDescription(sNameSpace, sSql)
            dRejectQnt = iPendingQty + dRejectedQty

            sSql = "" : sSql = "Update Sales_Allocate_Master Set SAM_Status='R',SAM_Remarks ='" & sRemarks & "',SAM_DeletedBy=" & iUserID & ",SAM_DeletedOn=GetDate(),SAM_Operation='R',SAM_IPAddress='" & iIPAddress & "' Where SAM_OrderNo=" & iOrderNo & " And SAM_ID=" & iAllocationID & " And SAM_CompID=" & iCompID & " and SAM_YearID =" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)


            sSql = "" : sSql = "Update Sales_Allocate_Details Set SAD_PendingQty=" & dRejectQnt & " Where SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID in (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderNo & " And SAM_ID=" & iAllocationID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & ") "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllocateDetailsID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As Integer
        Dim sSql As String
        Dim iId As Integer = 0
        Try
            sSql = "Select SAD_ID from Sales_Allocate_Details where SAD_MasterID =" & iMasterID & " and SAD_CompID =" & iCompID & ""
            iId = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iId
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Shared Function BindGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAllocateID As Integer, ByVal iorderno As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dtTab As New DataTable
    '    Dim dt As New DataTable
    '    Dim dRow As DataRow
    '    Dim i As Integer
    '    Dim sStosk As String = ""
    '    Dim dtAllocate As New DataTable
    '    Try
    '        dtTab.Columns.Add("ItemsID")
    '        dtTab.Columns.Add("CommodityID")
    '        dtTab.Columns.Add("ItemID")
    '        dtTab.Columns.Add("HistoryID")
    '        dtTab.Columns.Add("UnitID")
    '        dtTab.Columns.Add("Goods")
    '        dtTab.Columns.Add("Unit")
    '        dtTab.Columns.Add("AvailableStock")
    '        dtTab.Columns.Add("MRP")
    '        dtTab.Columns.Add("OrderQuantity")
    '        dtTab.Columns.Add("OrderedAmount")
    '        dtTab.Columns.Add("PRODiscount")
    '        dtTab.Columns.Add("PRODiscountAmount")
    '        dtTab.Columns.Add("PROTotalAmount")
    '        dtTab.Columns.Add("PlacedQuantity")
    '        dtTab.Columns.Add("Total")

    '        'dtTab.Columns.Add("VAT")
    '        dtTab.Columns.Add("VATAmount")
    '        'dtTab.Columns.Add("CST")
    '        dtTab.Columns.Add("CSTAmount")
    '        'dtTab.Columns.Add("Exice")
    '        dtTab.Columns.Add("ExiceAmount")

    '        dtTab.Columns.Add("Discount")
    '        dtTab.Columns.Add("DiscountAmount")
    '        dtTab.Columns.Add("NetAmount")
    '        dtTab.Columns.Add("ClosingStock")
    '        dtTab.Columns.Add("PendingQty")

    '        If iAllocateID > 0 Then
    '            sSql = "Select * from Sales_Allocate_Details Where SAD_MasterID in (Select SAM_ID from Sales_Allocate_Master Where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iorderno & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & ") order By SAD_ID"
    '        Else
    '            sSql = "Select * from Sales_Allocate_Details Where SAD_MasterID in (Select Top 1 SAM_ID from Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " order By SAM_ID Desc) order By SAD_ID"
    '        End If

    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                dRow = dtTab.NewRow
    '                dRow("ItemsID") = dt.Rows(i)("SAD_ID")
    '                dRow("CommodityID") = dt.Rows(i)("SAD_Commodity")
    '                dRow("ItemID") = dt.Rows(i)("SAD_DescID")
    '                dRow("HistoryID") = dt.Rows(i)("SAD_HisotryID")
    '                dRow("UnitID") = dt.Rows(i)("SAD_UnitID")
    '                dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SAD_DescID") & " And INV_Parent=" & dt.Rows(i)("SAD_Commodity") & " and INV_CompID =" & iCompID & "")
    '                dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select MAs_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SAD_UnitID") & " And Mas_CompID=" & iCompID & " ")
    '                If objDBL.SQLCheckForRecord(sNameSpace, "Select * From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SAD_DescID") & " And SL_Commodity=" & dt.Rows(i)("SAD_Commodity") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SAD_DescID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "") = True Then
    '                    dRow("AvailableStock") = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SL_ClosingBalanceQty) From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SAD_DescID") & " And SL_Commodity=" & dt.Rows(i)("SAD_Commodity") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SAD_DescID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "")
    '                Else
    '                    dRow("AvailableStock") = 0
    '                End If
    '                If IsDBNull(dt.Rows(i)("SAD_MRP")) = False Then
    '                    dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_MRP")))
    '                Else
    '                    dRow("MRP") = ""
    '                End If
    '                If IsDBNull(dt.Rows(i)("SAD_OrderQnt")) = False Then
    '                    dRow("OrderQuantity") = dt.Rows(i)("SAD_OrderQnt")
    '                Else
    '                    dRow("OrderQuantity") = ""
    '                End If
    '                If IsDBNull(dt.Rows(i)("SAD_OrderAmount")) = False Then
    '                    dRow("OrderedAmount") = dt.Rows(i)("SAD_OrderAmount")
    '                Else
    '                    dRow("OrderedAmount") = ""
    '                End If
    '                If IsDBNull(dt.Rows(i)("SAD_Discount")) = False Then
    '                    dRow("PRODiscount") = dt.Rows(i)("SAD_Discount")
    '                Else
    '                    dRow("PRODiscount") = ""
    '                End If
    '                If IsDBNull(dt.Rows(i)("SAD_DiscountAmount")) = False Then
    '                    dRow("PRODiscountAmount") = dt.Rows(i)("SAD_DiscountAmount")
    '                Else
    '                    dRow("PRODiscountAmount") = ""
    '                End If
    '                If IsDBNull(dt.Rows(i)("SAD_TotalAmount")) = False Then
    '                    dRow("PROTotalAmount") = dt.Rows(i)("SAD_TotalAmount")
    '                Else
    '                    dRow("PROTotalAmount") = ""
    '                End If

    '                dRow("PlacedQuantity") = ""
    '                'dt.Rows(i)("SAD_PlacedQnt")
    '                dRow("Total") = ""
    '                'dt.Rows(i)("SAD_PlacedQntAmount")

    '                'dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_VAT From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
    '                dRow("VATAmount") = ""
    '                'dt.Rows(i)("SAD_VATAmount")

    '                'dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_CST From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
    '                dRow("CSTAmount") = ""
    '                'dt.Rows(i)("SAD_CSTAmount")

    '                'dRow("Exice") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_Exice From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
    '                dRow("ExiceAmount") = dt.Rows(i)("SAD_ExiceAmount")

    '                'dRow("Discount") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_PlacedDiscount From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
    '                dRow("DiscountAmount") = ""
    '                'dt.Rows(i)("SAD_PlacedDiscountAmount")
    '                dRow("NetAmount") = ""
    '                'dt.Rows(i)("SAD_PlacedTotalAmount")
    '                dRow("ClosingStock") = ""
    '                'dt.Rows(i)("SAD_ClosingBal")
    '                dRow("PendingQty") = dt.Rows(i)("SAD_PendingQty")

    '                dtTab.Rows.Add(dRow)
    '            Next
    '        End If
    '        Return dtTab
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function BindGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAllocateID As Integer, ByVal iorderno As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim sStosk As String = ""
        Dim dtAllocate As New DataTable

        Dim dAllocatedQty As Double
        Try
            dtTab.Columns.Add("ItemsID")
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")
            dtTab.Columns.Add("AvailableStock")
            dtTab.Columns.Add("MRP")
            dtTab.Columns.Add("OrderQuantity")
            dtTab.Columns.Add("OrderedAmount")
            dtTab.Columns.Add("PRODiscount")
            dtTab.Columns.Add("PRODiscountAmount")
            dtTab.Columns.Add("PROTotalAmount")
            dtTab.Columns.Add("PlacedQuantity")
            dtTab.Columns.Add("Total")

            'dtTab.Columns.Add("VAT")
            dtTab.Columns.Add("VATAmount")
            'dtTab.Columns.Add("CST")
            dtTab.Columns.Add("CSTAmount")
            'dtTab.Columns.Add("Exice")
            dtTab.Columns.Add("ExiceAmount")

            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("NetAmount")
            dtTab.Columns.Add("ClosingStock")
            dtTab.Columns.Add("PendingQty")

            If iAllocateID > 0 Then
                sSql = "" : sSql = "Select * from Sales_Allocate_Details Where SAD_MasterID in (Select SAM_ID from Sales_Allocate_Master Where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iorderno & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & ") order By SAD_ID"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dRow = dtTab.NewRow
                        dRow("ItemsID") = dt.Rows(i)("SAD_ID")
                        dRow("CommodityID") = dt.Rows(i)("SAD_Commodity")
                        dRow("ItemID") = dt.Rows(i)("SAD_DescID")
                        dRow("HistoryID") = dt.Rows(i)("SAD_HisotryID")
                        dRow("UnitID") = dt.Rows(i)("SAD_UnitID")
                        dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SAD_DescID") & " And INV_Parent=" & dt.Rows(i)("SAD_Commodity") & " and INV_CompID =" & iCompID & "")
                        dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select MAs_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SAD_UnitID") & " And Mas_CompID=" & iCompID & " ")
                        If objDBL.SQLCheckForRecord(sNameSpace, "Select * From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SAD_DescID") & " And SL_Commodity=" & dt.Rows(i)("SAD_Commodity") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SAD_DescID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "") = True Then
                            dRow("AvailableStock") = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SL_ClosingBalanceQty) From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SAD_DescID") & " And SL_Commodity=" & dt.Rows(i)("SAD_Commodity") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SAD_DescID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "")
                        Else
                            dRow("AvailableStock") = 0
                        End If
                        If IsDBNull(dt.Rows(i)("SAD_MRP")) = False Then
                            dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_MRP")))
                        Else
                            dRow("MRP") = ""
                        End If
                        If IsDBNull(dt.Rows(i)("SAD_OrderQnt")) = False Then
                            dRow("OrderQuantity") = dt.Rows(i)("SAD_OrderQnt")
                        Else
                            dRow("OrderQuantity") = ""
                        End If
                        If IsDBNull(dt.Rows(i)("SAD_OrderAmount")) = False Then
                            dRow("OrderedAmount") = dt.Rows(i)("SAD_OrderAmount")
                        Else
                            dRow("OrderedAmount") = ""
                        End If
                        If IsDBNull(dt.Rows(i)("SAD_Discount")) = False Then
                            dRow("PRODiscount") = dt.Rows(i)("SAD_Discount")
                        Else
                            dRow("PRODiscount") = ""
                        End If
                        If IsDBNull(dt.Rows(i)("SAD_DiscountAmount")) = False Then
                            dRow("PRODiscountAmount") = dt.Rows(i)("SAD_DiscountAmount")
                        Else
                            dRow("PRODiscountAmount") = ""
                        End If
                        If IsDBNull(dt.Rows(i)("SAD_TotalAmount")) = False Then
                            dRow("PROTotalAmount") = dt.Rows(i)("SAD_TotalAmount")
                        Else
                            dRow("PROTotalAmount") = ""
                        End If

                        dRow("PlacedQuantity") = ""
                        'dt.Rows(i)("SAD_PlacedQnt")
                        dRow("Total") = ""
                        'dt.Rows(i)("SAD_PlacedQntAmount")

                        'dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_VAT From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                        dRow("VATAmount") = ""
                        'dt.Rows(i)("SAD_VATAmount")

                        'dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_CST From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                        dRow("CSTAmount") = ""
                        'dt.Rows(i)("SAD_CSTAmount")

                        'dRow("Exice") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_Exice From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                        dRow("ExiceAmount") = dt.Rows(i)("SAD_ExiceAmount")

                        'dRow("Discount") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_PlacedDiscount From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                        dRow("DiscountAmount") = ""
                        'dt.Rows(i)("SAD_PlacedDiscountAmount")
                        dRow("NetAmount") = ""
                        'dt.Rows(i)("SAD_PlacedTotalAmount")
                        dRow("ClosingStock") = ""
                        'dt.Rows(i)("SAD_ClosingBal")
                        dRow("PendingQty") = dt.Rows(i)("SAD_PendingQty")

                        dtTab.Rows.Add(dRow)
                    Next
                End If
            Else
                sSql = "" : sSql = "Select * From Sales_ProForma_order_Details Where SPOD_Status <> 'C' And SPOD_SOID=" & iorderno & " And SPOD_YearID=" & iYearID & " And SPOD_CompID=" & iCompID & " "
                Dim dtP As New DataTable : Dim bCheck As Boolean
                dtP = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dtP.Rows.Count > 0 Then
                    For k = 0 To dtP.Rows.Count - 1
                        bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_MasterID=SAM_ID And SAM_OrderNo=" & iorderno & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " And SAD_Commodity=" & dtP.Rows(k)("SPOD_CommodityID") & " And SAD_DescID=" & dtP.Rows(k)("SPOD_ItemID") & " And SAD_HisotryID=" & dtP.Rows(k)("SPOD_HistoryID") & " Order By SAD_ID DESC ")
                        If bCheck = True Then 'Allocation Details

                            dAllocatedQty = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Sum(SAD_PlacedQnt) From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_MasterID=SAM_ID And SAM_OrderNo=" & iorderno & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " And SAD_Commodity=" & dtP.Rows(k)("SPOD_CommodityID") & " And SAD_DescID=" & dtP.Rows(k)("SPOD_ItemID") & " And SAD_HisotryID=" & dtP.Rows(k)("SPOD_HistoryID") & " ")
                            If IsDBNull(dtP.Rows(k)("SPOD_Quantity")) = False Then
                                If dtP.Rows(k)("SPOD_Quantity") <> dAllocatedQty Then

                                    sSql = "" : sSql = "Select Top 1 * From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_MasterID=SAM_ID And SAM_OrderNo=" & iorderno & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " And SAD_PendingQty>0 And SAD_Commodity=" & dtP.Rows(k)("SPOD_CommodityID") & " And SAD_DescID=" & dtP.Rows(k)("SPOD_ItemID") & " And SAD_HisotryID=" & dtP.Rows(k)("SPOD_HistoryID") & " Order By SAD_ID DESC"
                                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                                    If dt.Rows.Count > 0 Then
                                        For i = 0 To dt.Rows.Count - 1
                                            dRow = dtTab.NewRow
                                            dRow("ItemsID") = dt.Rows(i)("SAD_ID")
                                            dRow("CommodityID") = dt.Rows(i)("SAD_Commodity")
                                            dRow("ItemID") = dt.Rows(i)("SAD_DescID")
                                            dRow("HistoryID") = dt.Rows(i)("SAD_HisotryID")
                                            dRow("UnitID") = dt.Rows(i)("SAD_UnitID")
                                            dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SAD_DescID") & " And INV_Parent=" & dt.Rows(i)("SAD_Commodity") & " and INV_CompID =" & iCompID & "")
                                            dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select MAs_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SAD_UnitID") & " And Mas_CompID=" & iCompID & " ")
                                            If objDBL.SQLCheckForRecord(sNameSpace, "Select * From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SAD_DescID") & " And SL_Commodity=" & dt.Rows(i)("SAD_Commodity") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SAD_DescID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "") = True Then
                                                dRow("AvailableStock") = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SL_ClosingBalanceQty) From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SAD_DescID") & " And SL_Commodity=" & dt.Rows(i)("SAD_Commodity") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SAD_DescID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "")
                                            Else
                                                dRow("AvailableStock") = 0
                                            End If
                                            If IsDBNull(dt.Rows(i)("SAD_MRP")) = False Then
                                                dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_MRP")))
                                            Else
                                                dRow("MRP") = ""
                                            End If
                                            If IsDBNull(dt.Rows(i)("SAD_OrderQnt")) = False Then
                                                dRow("OrderQuantity") = dt.Rows(i)("SAD_OrderQnt")
                                            Else
                                                dRow("OrderQuantity") = ""
                                            End If
                                            If IsDBNull(dt.Rows(i)("SAD_OrderAmount")) = False Then
                                                dRow("OrderedAmount") = dt.Rows(i)("SAD_OrderAmount")
                                            Else
                                                dRow("OrderedAmount") = ""
                                            End If
                                            If IsDBNull(dt.Rows(i)("SAD_Discount")) = False Then
                                                dRow("PRODiscount") = dt.Rows(i)("SAD_Discount")
                                            Else
                                                dRow("PRODiscount") = ""
                                            End If
                                            If IsDBNull(dt.Rows(i)("SAD_DiscountAmount")) = False Then
                                                dRow("PRODiscountAmount") = dt.Rows(i)("SAD_DiscountAmount")
                                            Else
                                                dRow("PRODiscountAmount") = ""
                                            End If
                                            If IsDBNull(dt.Rows(i)("SAD_TotalAmount")) = False Then
                                                dRow("PROTotalAmount") = dt.Rows(i)("SAD_TotalAmount")
                                            Else
                                                dRow("PROTotalAmount") = ""
                                            End If

                                            dRow("PlacedQuantity") = ""
                                            'dt.Rows(i)("SAD_PlacedQnt")
                                            dRow("Total") = ""
                                            'dt.Rows(i)("SAD_PlacedQntAmount")

                                            'dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_VAT From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                                            dRow("VATAmount") = ""
                                            'dt.Rows(i)("SAD_VATAmount")

                                            'dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_CST From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                                            dRow("CSTAmount") = ""
                                            'dt.Rows(i)("SAD_CSTAmount")

                                            'dRow("Exice") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_Exice From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                                            dRow("ExiceAmount") = dt.Rows(i)("SAD_ExiceAmount")

                                            'dRow("Discount") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_PlacedDiscount From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                                            dRow("DiscountAmount") = ""
                                            'dt.Rows(i)("SAD_PlacedDiscountAmount")
                                            dRow("NetAmount") = ""
                                            'dt.Rows(i)("SAD_PlacedTotalAmount")
                                            dRow("ClosingStock") = ""
                                            'dt.Rows(i)("SAD_ClosingBal")
                                            dRow("PendingQty") = dt.Rows(i)("SAD_PendingQty")

                                            dtTab.Rows.Add(dRow)
                                        Next
                                    End If

                                End If
                            End If

                        Else 'Proforma Details
                            sSql = "" : sSql = "Select * from sales_Proforma_Order_Details Where SPOD_CommodityID=" & dtP.Rows(k)("SPOD_CommodityID") & " And SPOD_ItemID=" & dtP.Rows(k)("SPOD_ItemID") & " And SPOD_HistoryID=" & dtP.Rows(k)("SPOD_HistoryID") & " And SPOD_SOID=" & iorderno & " and SPOD_Status <> 'C' order By SPOD_ID"
                            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                            If dt.Rows.Count > 0 Then
                                For i = 0 To dt.Rows.Count - 1
                                    dRow = dtTab.NewRow
                                    dRow("ItemsID") = dt.Rows(i)("SPOD_ID")
                                    dRow("CommodityID") = dt.Rows(i)("SPOD_CommodityID")
                                    dRow("ItemID") = dt.Rows(i)("SPOD_ItemID")
                                    dRow("HistoryID") = dt.Rows(i)("SPOD_HistoryID")
                                    dRow("UnitID") = dt.Rows(i)("SPOD_UnitOfMeasurement")
                                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SPOD_ItemID") & " And INV_Parent=" & dt.Rows(i)("SPOD_CommodityID") & " and INV_CompID =" & iCompID & "")
                                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select MAs_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SPOD_UnitOfMeasurement") & " And Mas_CompID=" & iCompID & " ")
                                    If objDBL.SQLCheckForRecord(sNameSpace, "Select * From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SPOD_ItemID") & " And SL_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SPOD_ItemID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "") = True Then
                                        dRow("AvailableStock") = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SL_ClosingBalanceQty) From stock_ledger Where SL_ItemID=" & dt.Rows(i)("SPOD_ItemID") & " And SL_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SPOD_ItemID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "")
                                    Else
                                        dRow("AvailableStock") = 0
                                    End If
                                    If IsDBNull(dt.Rows(i)("SPOD_MRPRate")) = False Then
                                        dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_MRPRate")))
                                    Else
                                        dRow("MRP") = ""
                                    End If
                                    If IsDBNull(dt.Rows(i)("SPOD_Quantity")) = False Then
                                        dRow("OrderQuantity") = dt.Rows(i)("SPOD_Quantity")
                                    Else
                                        dRow("OrderQuantity") = ""
                                    End If
                                    If IsDBNull(dt.Rows(i)("SPOD_RateAmount")) = False Then
                                        dRow("OrderedAmount") = dt.Rows(i)("SPOD_RateAmount")
                                    Else
                                        dRow("OrderedAmount") = ""
                                    End If
                                    If IsDBNull(dt.Rows(i)("SPOD_Discount")) = False Then
                                        dRow("PRODiscount") = dt.Rows(i)("SPOD_Discount")
                                        'objDBL.SQLExecuteScalarInt(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_Master=19 And MAS_ID=" & dt.Rows(i)("SPOD_Discount") & " And Mas_Delflag ='A' And MAs_CompID=" & iCompID & " ")
                                    Else
                                        dRow("PRODiscount") = ""
                                    End If
                                    If IsDBNull(dt.Rows(i)("SPOD_DiscountRate")) = False Then
                                        dRow("PRODiscountAmount") = dt.Rows(i)("SPOD_DiscountRate")
                                    Else
                                        dRow("PRODiscountAmount") = ""
                                    End If
                                    If IsDBNull(dt.Rows(i)("SPOD_TotalAmount")) = False Then
                                        dRow("PROTotalAmount") = dt.Rows(i)("SPOD_TotalAmount")
                                    Else
                                        dRow("PROTotalAmount") = ""
                                    End If

                                    dRow("PlacedQuantity") = dt.Rows(i)("SPOD_Quantity")
                                    dRow("Total") = dt.Rows(i)("SPOD_RateAmount")

                                    'dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_VAT From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                                    dRow("VATAmount") = dt.Rows(i)("SPOD_VATAmount")
                                    'dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_CST From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                                    dRow("CSTAmount") = dt.Rows(i)("SPOD_CSTAmount")
                                    'dRow("Exice") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_Exice From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                                    dRow("ExiceAmount") = dt.Rows(i)("SPOD_ExciseAmount")

                                    dRow("Discount") = dt.Rows(i)("SPOD_Discount")
                                    'objDBL.SQLExecuteScalarInt(sNameSpace, "Select Mas_Desc From Acc_General_Master Where MAS_Master=19 And Mas_ID=" & dt.Rows(i)("SPOD_Discount") & " And Mas_Delflag ='A' And MAs_CompID=" & iCompID & " ")
                                    dRow("DiscountAmount") = dt.Rows(i)("SPOD_DiscountRate")
                                    dRow("NetAmount") = dt.Rows(i)("SPOD_TotalAmount")
                                    dRow("ClosingStock") = dRow("AvailableStock") - dt.Rows(i)("SPOD_Quantity")
                                    dRow("PendingQty") = dt.Rows(i)("SPOD_Quantity")
                                    dtTab.Rows.Add(dRow)
                                Next
                            End If

                        End If

                    Next
                End If

            End If


            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAllocateOrder(ByVal sNameSpace As String, ByVal objAllocate As clsAllocateSalesOrder) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(17) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_ID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_OrderNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAM_OrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAM_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_Remarks", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objAllocate.sSAM_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_Status", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objAllocate.sSAM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_CompID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objAllocate.iSAM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_GrandDiscount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAM_GrandDiscount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_GrandDiscountAmt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAM_GrandDiscountAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_GrandTotal", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAM_GrandTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_GrandTotalAmt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAM_GrandTotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objAllocate.sSAM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAllocate.sSAM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_DispatchFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAllocate.sSAM_DispatchFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAM_Code", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAllocate.sSAM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAllocateSalesOrder", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAllocateDetails(ByVal sNameSpace As String, ByVal objAllocate As clsAllocateSalesOrder) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(31) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Commodity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_Commodity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_DescID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_DescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_HisotryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_OpeningBal", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_OpeningBal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_MRP", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.mSAD_MRP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_OrderQnt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_OrderQnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_OrderAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.mSAD_OrderAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Discount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.mSAD_Discount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_DiscountAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.mSAD_DiscountAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_TotalAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.mSAD_TotalAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_PlacedQnt", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_PlacedQnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_PlacedQntAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.mSAD_PlacedQntAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_PlacedDiscount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.mSAD_PlacedDiscount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_PlacedDiscountAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.mSAD_PlacedDiscountAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_PlacedTotalAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.mSAD_PlacedTotalAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_ClosingBal", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_ClosingBal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_CompID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objAllocate.iSAD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAllocate.iSAD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objAllocate.sSAD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAllocate.sSAD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_VAT", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.SAD_VAT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_VATAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.SAD_VATAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_CST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.SAD_CST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_CSTAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.SAD_CSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_Exice", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.SAD_Exice
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_ExiceAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.SAD_ExiceAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SAD_PendingQty", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objAllocate.SAD_PendingQty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAllocateSalesDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartiesOrderedForThisItem(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderNo As Integer, ByVal iItemID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select SPOD_CommodityID,SPOD_ItemID,SPOD_Quantity,SPO_PartyName From Sales_Proforma_Order_details,Sales_Proforma_Order Where SPOD_ItemID=" & iItemID & " And SPOD_SOID=SPO_ID "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetAllocateMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAllocateID As Integer, Optional ByVal iOrderNo As Integer = 0) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iOrderNo > 0 Then
                sSql = "Select Top 1 * From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderNo & " And SAM_CompID=" & iCompID & " and SAM_YearID =" & iYearID & " order by SAM_ID Desc "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select * From Sales_Allocate_Master Where SAM_ID=" & iAllocateID & " And SAM_CompID=" & iCompID & " and SAM_YearID =" & iYearID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindVAt(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=14 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindCST(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=15 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindExice(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=16 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATFromPROFORMA(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal iAllocateID As Integer) As Integer
        Dim sSql As String = ""
        Dim iVAT As Integer
        Dim bCheck As Boolean
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Top 1 * From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " Order By SAD_ID Desc ")
            If bCheck = True Then
                If iAllocateID > 0 Then
                    iVAT = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SAD_VAT From Sales_Allocate_Details Where SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID in (Select SAM_ID From Sales_Allocate_Master where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ) ")
                Else
                    iVAT = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Top 1 SAD_VAT From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " Order By SAD_ID Desc ")
                End If
            Else
                sSql = "select SPOD_VAT from Sales_PROForma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodityID & " And SPOD_ItemID=" & iItemID & " And SPOD_HistoryID=" & iHistoryID & " And SPOD_CompID =" & iCompID & ""
                iVAT = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return iVAT
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTFromPROFORMA(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal iAllocateID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCST As Integer
        Dim bCheck As Boolean
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Top 1 * From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_DescID=" & iItemID & " And SAD_Commodity=" & iCommodityID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " Order By SAD_ID Desc ")
            If bCheck = True Then
                If iAllocateID > 0 Then
                    iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SAD_CST From Sales_Allocate_Details Where SAD_DescID=" & iItemID & " And SAD_Commodity=" & iCommodityID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID in (Select SAM_ID From Sales_Allocate_Master where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ) ")
                Else
                    iCST = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Top 1 SAD_CST From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " Order By SAD_ID Desc ")
                End If
            Else
                sSql = "select SPOD_CST from Sales_PROForma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodityID & " And SPOD_ItemID=" & iItemID & " And SPOD_HistoryID=" & iHistoryID & " And SPOD_CompID =" & iCompID & ""
                iCST = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return iCST
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExciseFromPROFORMA(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal iAllocateID As Integer) As Integer
        Dim sSql As String = ""
        Dim iExcise As Integer
        Dim bCheck As Boolean
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Top 1 * From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_DescID=" & iItemID & " And SAD_Commodity=" & iCommodityID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " Order By SAD_ID Desc ")
            If bCheck = True Then
                If iAllocateID > 0 Then
                    iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SAD_Exice From Sales_Allocate_Details Where SAD_MasterID in (Select SAM_ID From Sales_Allocate_Master where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ) And SAD_DescID=" & iItemID & " And SAD_Commodity=" & iCommodityID & " And SAD_HisotryID=" & iHistoryID & " ")
                Else
                    iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, "Select Top 1 SAD_Exice From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_DescID=" & iItemID & " And SAD_Commodity=" & iCommodityID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " Order By SAD_ID Desc ")
                End If
            Else
                sSql = "select SPOD_Excise from Sales_PROForma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodityID & " And SPOD_HistoryID=" & iHistoryID & " And SPOD_ItemID=" & iItemID & " And SPOD_CompID =" & iCompID & ""
                iExcise = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return iExcise
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Shared Function GetExice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iItemID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim iExice As Integer
    '    Dim bCheck As Boolean
    '    Try
    '        bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Allocate_Details Where SAD_MasterID in (Select SAM_ID From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ) And SAD_DescID=" & iItemID & " And SAD_CompID=" & iCompID & " And SAD_YearID=" & iYearID & " ")
    '        If bCheck = True Then
    '            iExice = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SAD_Exice From Sales_Allocate_Details Where SAD_MasterID in (Select SAM_ID From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ) And SAD_DescID=" & iItemID & " And SAD_CompID=" & iCompID & " And SAD_YearID=" & iYearID & " ")
    '        Else
    '            iExice = 0
    '        End If
    '        Return iExice
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function CheckOrderForDispatch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAllocationID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sStr As String = ""
        Try
            sSql = "Select * From Sales_Dispatch_Master Where SDM_AllocateID=" & iAllocationID & " And SDM_YearID=" & iYearID & " And SDM_CompID=" & iCompID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sStr = "True"
            Else
                sStr = "False"
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPROFormaOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select * from Sales_PROForma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodityID & " And SPOD_ItemID=" & iItemID & " And SPOD_HistoryID=" & iHistoryID & " And SPOD_CompID =" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllocatedOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal iAllocateID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAllocateID > 0 Then
                sSql = "Select * From Sales_Allocate_Details Where SAD_MasterID in (Select SAM_ID From Sales_Allocate_Master where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ) And SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " And SAD_CompID=" & iCompID & " And SAD_YearID=" & iYearID & " "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                'sSql = "Select * From Sales_Allocate_Details Where SAD_MasterID in (Select Top 1 SAM_ID From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " Order By SAM_ID Desc) And SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " And SAD_CompID=" & iCompID & " And SAD_YearID=" & iYearID & " "
                sSql = "Select Top 1 * From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " And SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " Order By SAD_ID Desc "
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForAllocation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sStr As String = ""
        Try
            sSql = "Select * From Sales_Allocate_Master Where SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sStr = "True"
            Else
                sStr = "False"
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindProFormaGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iorderno As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim sStosk As String = ""
        Dim dtAllocate As New DataTable
        Try
            dtTab.Columns.Add("ItemsID")
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")
            dtTab.Columns.Add("AvailableStock")
            dtTab.Columns.Add("MRP")
            dtTab.Columns.Add("OrderQuantity")
            dtTab.Columns.Add("OrderedAmount")
            dtTab.Columns.Add("PRODiscount")
            dtTab.Columns.Add("PRODiscountAmount")
            dtTab.Columns.Add("PROTotalAmount")
            dtTab.Columns.Add("PlacedQuantity")
            dtTab.Columns.Add("Total")

            'dtTab.Columns.Add("VAT")
            dtTab.Columns.Add("VATAmount")
            'dtTab.Columns.Add("CST")
            dtTab.Columns.Add("CSTAmount")
            'dtTab.Columns.Add("Exice")
            dtTab.Columns.Add("ExiceAmount")

            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("NetAmount")
            dtTab.Columns.Add("ClosingStock")
            dtTab.Columns.Add("PendingQty")

            sSql = "Select * from sales_Proforma_Order_Details Where SPOD_SOID=" & iorderno & " and SPOD_Status <> 'C' order By SPOD_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ItemsID") = dt.Rows(i)("SPOD_ID")
                    dRow("CommodityID") = dt.Rows(i)("SPOD_CommodityID")
                    dRow("ItemID") = dt.Rows(i)("SPOD_ItemID")
                    dRow("HistoryID") = dt.Rows(i)("SPOD_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("SPOD_UnitOfMeasurement")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SPOD_ItemID") & " And INV_Parent=" & dt.Rows(i)("SPOD_CommodityID") & " and INV_CompID =" & iCompID & "")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select MAs_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SPOD_UnitOfMeasurement") & " And Mas_CompID=" & iCompID & " ")
                    If objDBL.SQLCheckForRecord(sNameSpace, "Select * From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SPOD_ItemID") & " And SL_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SPOD_ItemID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "") = True Then
                        dRow("AvailableStock") = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SL_ClosingBalanceQty) From stock_ledger Where SL_ItemID=" & dt.Rows(i)("SPOD_ItemID") & " And SL_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SPOD_ItemID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "")
                    Else
                        dRow("AvailableStock") = 0
                    End If
                    If IsDBNull(dt.Rows(i)("SPOD_MRPRate")) = False Then
                        dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SPOD_MRPRate")))
                    Else
                        dRow("MRP") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SPOD_Quantity")) = False Then
                        dRow("OrderQuantity") = dt.Rows(i)("SPOD_Quantity")
                    Else
                        dRow("OrderQuantity") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SPOD_RateAmount")) = False Then
                        dRow("OrderedAmount") = dt.Rows(i)("SPOD_RateAmount")
                    Else
                        dRow("OrderedAmount") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SPOD_Discount")) = False Then
                        dRow("PRODiscount") = dt.Rows(i)("SPOD_Discount")
                        'objDBL.SQLExecuteScalarInt(sNameSpace, "Select MAS_Desc From Acc_General_Master Where MAS_Master=19 And MAS_ID=" & dt.Rows(i)("SPOD_Discount") & " And Mas_Delflag ='A' And MAs_CompID=" & iCompID & " ")
                    Else
                        dRow("PRODiscount") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SPOD_DiscountRate")) = False Then
                        dRow("PRODiscountAmount") = dt.Rows(i)("SPOD_DiscountRate")
                    Else
                        dRow("PRODiscountAmount") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SPOD_TotalAmount")) = False Then
                        dRow("PROTotalAmount") = dt.Rows(i)("SPOD_TotalAmount")
                    Else
                        dRow("PROTotalAmount") = ""
                    End If

                    dRow("PlacedQuantity") = dt.Rows(i)("SPOD_Quantity")
                    dRow("Total") = dt.Rows(i)("SPOD_RateAmount")

                    'dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_VAT From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                    dRow("VATAmount") = dt.Rows(i)("SPOD_VATAmount")
                    'dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_CST From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                    dRow("CSTAmount") = dt.Rows(i)("SPOD_CSTAmount")
                    'dRow("Exice") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_Exice From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                    dRow("ExiceAmount") = dt.Rows(i)("SPOD_ExciseAmount")

                    dRow("Discount") = dt.Rows(i)("SPOD_Discount")
                    'objDBL.SQLExecuteScalarInt(sNameSpace, "Select Mas_Desc From Acc_General_Master Where MAS_Master=19 And Mas_ID=" & dt.Rows(i)("SPOD_Discount") & " And Mas_Delflag ='A' And MAs_CompID=" & iCompID & " ")
                    dRow("DiscountAmount") = dt.Rows(i)("SPOD_DiscountRate")
                    dRow("NetAmount") = dt.Rows(i)("SPOD_TotalAmount")
                    dRow("ClosingStock") = dRow("AvailableStock") - dt.Rows(i)("SPOD_Quantity")
                    dRow("PendingQty") = dt.Rows(i)("SPOD_Quantity")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveGrandTotalToOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iIPAddress As String, ByVal iAllocateID As Integer, ByVal sGrandDiscount As String, ByVal sGrandDiscountAmt As String, ByVal sGrandTotal As String, ByVal sGrandTotalAmt As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Allocate_Master set SAM_GrandDiscount=" & sGrandDiscount & ",SAM_GrandDiscountAmt=" & sGrandDiscountAmt & ", "
            sSql = sSql & "SAM_GrandTotal=" & sGrandTotal & ",SAM_GrandTotalAmt=" & sGrandTotalAmt & ",SAM_Operation='U',SAM_IPAddress='" & iIPAddress & "' "
            sSql = sSql & "Where SAM_ID=" & iAllocateID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Sales_PROForma_Order Where SPO_ID=" & iOrderNo & " And SPO_CompID=" & iCompID & " and SPO_YearID =" & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDiscount(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=19 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDiscountFromPROFORMA(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal iAllocateID As Integer) As Integer
        Dim sSql As String = ""
        Dim iDiscount As Integer
        Dim bCheck As Boolean
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Top 1 * From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_DescID=" & iItemID & " And SAD_Commodity=" & iCommodityID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ")
            If bCheck = True Then
                If iAllocateID > 0 Then
                    sSql = ""
                    sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=19 And MAS_Desc in(Select SAD_PlacedDiscount From Sales_Allocate_Details Where SAD_MasterID in (Select SAM_ID From Sales_Allocate_Master where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " ) And SAD_DescID=" & iItemID & " And SAD_Commodity=" & iCommodityID & " And SAD_HisotryID=" & iHistoryID & ") And Mas_Delflag ='A' And MAs_CompID=" & iCompID & ""
                    iDiscount = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
                Else
                    sSql = ""
                    sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=19 And MAS_Desc in(Select Top 1 SAD_PlacedDiscount From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_DescID=" & iItemID & " And SAD_Commodity=" & iCommodityID & " And SAD_HisotryID=" & iHistoryID & " And SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_CompID=" & iCompID & " And SAM_YearID=" & iYearID & " Order By SAD_ID Desc) And Mas_Delflag ='A' And MAs_CompID=" & iCompID & ""
                    iDiscount = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
                End If

            Else
                sSql = ""
                sSql = "Select MAS_ID From Acc_General_Master Where MAS_Master=19 And MAS_Desc in(select SPOD_Discount from Sales_PROForma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_CommodityID=" & iCommodityID & " And SPOD_ItemID=" & iItemID & " And SPOD_HistoryID=" & iHistoryID & " And SPOD_CompID =" & iCompID & " And SPOD_YearID=" & iYearID & ") And Mas_Delflag ='A' And MAs_CompID=" & iCompID & ""
                iDiscount = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return iDiscount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPartyID As Integer) As String
        Dim sSql As String = ""
        Dim sStr As String = ""
        Try
            sSql = "Select BM_CODE From Sales_Buyers_Masters Where BM_ID=" & iPartyID & " And BM_CompID=" & iCompID & " "
            'sSql = "Select ACM_CODE From Acc_Customer_Master Where ACM_ID=" & iPartyID & " And ACM_CompID=" & iCompID & " "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            If sStr.StartsWith("C") Then
                GetPartyCode = "C"
            End If
            If sStr.StartsWith("P") Then
                GetPartyCode = "P"
            End If
            Return GetPartyCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Shared Function GetVATOFEachItem(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal sRate As String, ByVal sStr As String) As String
    '    Dim sSql As String = ""
    '    Dim sVAT As String = ""
    '    Try
    '        If sStr.StartsWith("P") Then
    '            sSql = "Select INVH_VAT From Inventory_Master_History Where INVH_Retail=" & sRate & " And INVH_INV_ID=" & iItemID & " "
    '        Else
    '            sSql = "Select INVH_VAT From Inventory_Master_History Where INVH_MRP=" & sRate & " And INVH_INV_ID=" & iItemID & " "
    '        End If
    '        sVAT = objDBL.SQLGetDescription(sNameSpace, sSql)
    '        Return sVAT
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetVATOFEachItem(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As String
        Dim sSql As String = ""
        Dim sVAT As String = ""
        Try
            sSql = "Select INVH_VAT From Inventory_Master_History Where INVH_ID=" & iHistoryID & " And INVH_INV_ID=" & iItemID & " "
            sVAT = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sVAT
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
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 SAM_ID From Sales_Allocate_Master Order By SAM_ID Desc")
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
            sStr = "AL-" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim bCheckO As Boolean
        Dim bCheckP As Boolean
        Try
            If sSearch <> "" Then
                bCheckO = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Allocate_Master where SAM_DispatchFlag <> 1 And SPO_Code ='" & sSearch & "' And SAM_CompID=" & iCompID & " and SAM_YearID =" & iYearID & "")
                If bCheckO = True Then
                    sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_DispatchFlag <> 1 And SAM_Code ='" & sSearch & "' And SAM_CompID=" & iCompID & " and SAM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    'Else
                    'bCheckP = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Proforma_Order where SPO_DispatchFlag <> 1 And SPO_OrderType='S' And SPO_PartyCode ='" & sSearch & "' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & "")
                    'If bCheckP = True Then
                    '    sSql = "Select SPO_ID,SPO_OrderCode From Sales_Proforma_Order where SPO_DispatchFlag <> 1 And SPO_OrderType='S' And SPO_PartyCode ='" & sSearch & "' And SPO_CompID=" & iCompID & " and SPO_YearID = " & iYearID & ""
                    '    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    'End If
                End If
            Else
                sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_DispatchFlag <> 1 And SAM_CompID=" & iCompID & " and SAM_YearID = " & iYearID & " Order By SAM_ID Desc"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindExistingCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iOrderID > 0 Then
                sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_OrderNo=" & iOrderID & " And SAM_Status<>'A' And SAM_DispatchFlag <> 1 And SAM_CompID=" & iCompID & " and SAM_YearID = " & iYearID & " Order By SAM_ID Desc"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_Status<>'A' And SAM_DispatchFlag <> 1 And SAM_CompID=" & iCompID & " and SAM_YearID = " & iYearID & " Order By SAM_ID Desc"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAllocatedGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAllocateID As Integer, ByVal iorderno As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Dim sStosk As String = ""
        Dim dtAllocate As New DataTable
        Try
            dtTab.Columns.Add("ItemsID")
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("ItemID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("Unit")
            dtTab.Columns.Add("AvailableStock")
            dtTab.Columns.Add("MRP")
            dtTab.Columns.Add("OrderQuantity")
            dtTab.Columns.Add("OrderedAmount")
            dtTab.Columns.Add("PRODiscount")
            dtTab.Columns.Add("PRODiscountAmount")
            dtTab.Columns.Add("PROTotalAmount")
            dtTab.Columns.Add("PlacedQuantity")
            dtTab.Columns.Add("Total")

            'dtTab.Columns.Add("VAT")
            dtTab.Columns.Add("VATAmount")
            'dtTab.Columns.Add("CST")
            dtTab.Columns.Add("CSTAmount")
            'dtTab.Columns.Add("Exice")
            dtTab.Columns.Add("ExiceAmount")

            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("NetAmount")
            dtTab.Columns.Add("ClosingStock")
            dtTab.Columns.Add("PendingQty")

            If iAllocateID > 0 Then
                sSql = "Select * from Sales_Allocate_Details Where SAD_MasterID in (Select SAM_ID from Sales_Allocate_Master Where SAM_ID=" & iAllocateID & " And SAM_OrderNo=" & iorderno & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & ") order By SAD_ID"
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ItemsID") = dt.Rows(i)("SAD_ID")
                    dRow("CommodityID") = dt.Rows(i)("SAD_Commodity")
                    dRow("ItemID") = dt.Rows(i)("SAD_DescID")
                    dRow("HistoryID") = dt.Rows(i)("SAD_HisotryID")
                    dRow("UnitID") = dt.Rows(i)("SAD_UnitID")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select (INV_Code) From  Inventory_Master Where INV_ID=" & dt.Rows(i)("SAD_DescID") & " And INV_Parent=" & dt.Rows(i)("SAD_Commodity") & " and INV_CompID =" & iCompID & "")
                    dRow("Unit") = objDBL.SQLGetDescription(sNameSpace, "Select MAs_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SAD_UnitID") & " And Mas_CompID=" & iCompID & " ")
                    If objDBL.SQLCheckForRecord(sNameSpace, "Select * From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SAD_DescID") & " And SL_Commodity=" & dt.Rows(i)("SAD_Commodity") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SAD_DescID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "") = True Then
                        dRow("AvailableStock") = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SL_ClosingBalanceQty) From  stock_ledger Where SL_ItemID=" & dt.Rows(i)("SAD_DescID") & " And SL_Commodity=" & dt.Rows(i)("SAD_Commodity") & " And SL_HistoryID in (Select INVH_ID From Inventory_Master_History where INVH_INV_ID = " & dt.Rows(i)("SAD_DescID") & " And INVH_CompID=" & iCompID & ") and SL_CompID =" & iCompID & "")
                    Else
                        dRow("AvailableStock") = 0
                    End If
                    If IsDBNull(dt.Rows(i)("SAD_MRP")) = False Then
                        dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SAD_MRP")))
                    Else
                        dRow("MRP") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SAD_OrderQnt")) = False Then
                        dRow("OrderQuantity") = dt.Rows(i)("SAD_OrderQnt")
                    Else
                        dRow("OrderQuantity") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SAD_OrderAmount")) = False Then
                        dRow("OrderedAmount") = dt.Rows(i)("SAD_OrderAmount")
                    Else
                        dRow("OrderedAmount") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SAD_Discount")) = False Then
                        dRow("PRODiscount") = dt.Rows(i)("SAD_Discount")
                    Else
                        dRow("PRODiscount") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SAD_DiscountAmount")) = False Then
                        dRow("PRODiscountAmount") = dt.Rows(i)("SAD_DiscountAmount")
                    Else
                        dRow("PRODiscountAmount") = ""
                    End If
                    If IsDBNull(dt.Rows(i)("SAD_TotalAmount")) = False Then
                        dRow("PROTotalAmount") = dt.Rows(i)("SAD_TotalAmount")
                    Else
                        dRow("PROTotalAmount") = ""
                    End If

                    dRow("PlacedQuantity") = dt.Rows(i)("SAD_PlacedQnt")

                    dRow("Total") = dt.Rows(i)("SAD_PlacedQntAmount")

                    'dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_VAT From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                    dRow("VATAmount") = dt.Rows(i)("SAD_VATAmount")

                    'dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_CST From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                    dRow("CSTAmount") = dt.Rows(i)("SAD_CSTAmount")

                    'dRow("Exice") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_Exice From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                    dRow("ExiceAmount") = dt.Rows(i)("SAD_ExiceAmount")

                    'dRow("Discount") = objDBL.SQLGetDescription(sNameSpace, "Select SAD_PlacedDiscount From Sales_Allocate_Details Where SAD_MasterID In (Select SAM_ID From Sales_Allocate_Master Where SAM_OrderNo=" & iorderno & " and SAM_YearID =" & iYearID & " and SAM_CompID =" & iCompID & ") And SAD_Commodity=" & dt.Rows(i)("SPOD_CommodityID") & " And SAD_DescID=" & dt.Rows(i)("SPOD_ItemID") & " and SAD_CompID = " & iCompID & " and SAD_YearID =" & iYearID & "")
                    dRow("DiscountAmount") = dt.Rows(i)("SAD_PlacedDiscountAmount")
                    dRow("NetAmount") = dt.Rows(i)("SAD_PlacedTotalAmount")
                    dRow("ClosingStock") = dt.Rows(i)("SAD_ClosingBal")
                    dRow("PendingQty") = dt.Rows(i)("SAD_PendingQty")

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForAllocationItems(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As String
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim sStr As String = ""
        Try
            sSql = "Select Top 1 * From Sales_Allocate_Details,Sales_Allocate_Master Where SAD_MasterID=SAM_ID And SAM_OrderNo=" & iOrderID & " And SAM_YearID=" & iYearID & " And SAM_CompID=" & iCompID & " And SAD_Commodity=" & iCommodityID & " And SAD_DescID=" & iItemID & " And SAD_HisotryID=" & iHistoryID & " Order By SAD_ID Desc "
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If bCheck = True Then
                sStr = "True"
            Else
                sStr = "False"
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDetailsPartiesForThisItem(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderNo As Integer, ByVal iItemId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("Commodity")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("PartyCode")
            dtTab.Columns.Add("PartyName")
            dtTab.Columns.Add("OrderedQty")

            dt = GetPartiesOrderedForThisItem(sNameSpace, iCompID, iOrderNo, iItemId)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("Commodity") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("SPOD_CommodityID") & " And INV_Parent=0 and Inv_CompID =" & iCompID & "")
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Description From Inventory_Master Where INV_ID=" & dt.Rows(i)("SPOD_ItemID") & " And INV_Parent=" & dt.Rows(i)("SPOD_CommodityID") & " and Inv_CompID =" & iCompID & "")
                    dRow("PartyCode") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Code From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("SPO_PartyName") & " and BM_CompID =" & iCompID & "")
                    'dRow("PartyCode") = objDBL.SQLGetDescription(sNameSpace, "Select ACM_Code From Acc_Customer_Master Where ACM_ID=" & dt.Rows(i)("SPO_PartyName") & " and ACM_CompID =" & iCompID & "")
                    dRow("PartyName") = objDBL.SQLGetDescription(sNameSpace, "Select BM_Name From Sales_Buyers_Masters Where BM_ID=" & dt.Rows(i)("SPO_PartyName") & " and BM_CompID =" & iCompID & "")
                    'dRow("PartyName") = objDBL.SQLGetDescription(sNameSpace, "Select ACM_Name From Acc_Customer_Master Where ACM_ID=" & dt.Rows(i)("SPO_PartyName") & " and ACM_CompID =" & iCompID & "")
                    dRow("OrderedQty") = dt.Rows(i)("SPOD_Quantity")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab


        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindAllocationCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iOrderID > 0 Then
                sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_OrderNo in (Select SPO_ID From Sales_Proforma_Order Where SPO_ID=" & iOrderID & " And SPO_BranchID=" & iBranch & ") And SAM_CompID=" & iCompID & " and SAM_YearID = " & iYearID & " Order By SAM_ID Desc"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Else
                sSql = "Select SAM_ID,SAM_Code From Sales_Allocate_Master where SAM_OrderNo in (Select SPO_ID From Sales_Proforma_Order Where SPO_BranchID=" & iBranch & ") And SAM_CompID=" & iCompID & " and SAM_YearID = " & iYearID & " Order By SAM_ID Desc"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
