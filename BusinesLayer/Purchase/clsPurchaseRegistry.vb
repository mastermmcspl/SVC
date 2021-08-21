Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer

Public Class clsPurchaseRegistry
    Dim objDb As New DBHelper
    Dim objPO As New clsPurchaseOrder
    'Dim objGIN As New ClsGoodsInward

    Dim sSession As New AllSession
    Dim objFasGnrl As New clsFASGeneral
    Dim objGnrlFnctn As New clsGeneralFunctions
    Dim objInvntry As New clsInvenotryDetails

    Dim PRM_ID As Integer
    Dim PRM_OrderNo As Integer
    Dim PRM_OrderDate As Date
    Dim PRM_InvoiceDate As Date
    Dim PRM_Supplier As Integer
    Dim PRM_DocumentRefNo As String
    Dim PRM_RegistryNo As String
    Dim PRM_ESugamNo As String
    Dim PRM_CreatedBy As Integer
    Dim PRM_CreatedOn As Date
    Dim PRM_ApprovedBy As Integer
    Dim PRM_ApprovedOn As Date
    Dim PRM_YearID As Integer
    Dim PRM_Status As String
    Dim PRM_CompID As Integer
    Dim PRD_ID As Integer
    Dim PRD_MasterID As Integer
    Dim PRD_OrderNo As Integer
    Dim PRD_Commodity As Integer
    Dim PRD_DescID As Integer
    Dim PRD_HistoryID As Integer
    Dim PRD_UnitID As Integer
    Dim PRD_OrderQuntity As Double
    Dim PRD_OrderRate As Double
    Dim PRD_MRP As Double
    Dim PRD_RecievedQnt As Double
    Dim PRD_Rejected As Double
    Dim PRD_Discount As Double
    Dim PRD_DiscountAmount As Double
    Dim PRD_Excise As Double
    Dim PRD_ExciseAMount As Double
    Dim PRD_VAT As Double
    Dim PRD_Accepted As Double
    Dim PRD_VATAmount As String
    Dim PRD_CST As Double
    Dim PRD_CSTAmount As Double
    Dim PRD_TotalAmount As Double
    Dim PRD_Status As String
    Dim PRD_CompID As Integer
    Dim PRD_ExcessQty As Double
    Dim PRD_ManufactureDate As Date
    Dim PRD_ExpireDate As Date
    Dim PRM_IPAddress As String
    Dim PRD_IPAddress As String
    Dim PRM_DcNo As String
    Dim PRM_DelFlag As String
    Dim PRD_Delflag As String
    Dim PRD_PendIng As Double
    Dim PRD_BatchNo As String
    Dim PRD_DocRefNo As String

    Public Property sPRD_BatchNo() As String
        Get
            Return (PRD_BatchNo)
        End Get
        Set(ByVal Value As String)
            PRD_BatchNo = Value
        End Set
    End Property
    Public Property sPRM_RegistryNo() As String
        Get
            Return (PRM_RegistryNo)
        End Get
        Set(ByVal Value As String)
            PRM_RegistryNo = Value
        End Set
    End Property
    Public Property sPRM_ESugamNo() As String
        Get
            Return (PRM_ESugamNo)
        End Get
        Set(ByVal Value As String)
            PRM_ESugamNo = Value
        End Set
    End Property
    Public Property iPRM_CreatedBy() As Integer
        Get
            Return (PRM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            PRM_CreatedBy = Value
        End Set
    End Property
    Public Property dPRM_CreatedOn() As Date
        Get
            Return (PRM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            PRM_CreatedOn = Value
        End Set
    End Property
    Public Property iPRM_ApprovedBy() As Integer
        Get
            Return (PRM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            PRM_ApprovedBy = Value
        End Set
    End Property
    Public Property dPRM_ApprovedOn() As Date
        Get
            Return (PRM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            PRM_ApprovedOn = Value
        End Set
    End Property
    Public Property iPRM_YearID() As Integer
        Get
            Return (PRM_YearID)
        End Get
        Set(ByVal Value As Integer)
            PRM_YearID = Value
        End Set
    End Property
    Public Property iPRM_Status() As String
        Get
            Return (PRM_Status)
        End Get
        Set(ByVal Value As String)
            PRM_Status = Value
        End Set
    End Property

    Public Property iPRM_ID() As Integer
        Get
            Return (PRM_ID)
        End Get
        Set(ByVal Value As Integer)
            PRM_ID = Value
        End Set
    End Property

    Public Property iPRM_OrderNo() As Integer
        Get
            Return (PRM_OrderNo)
        End Get
        Set(ByVal Value As Integer)
            PRM_OrderNo = Value
        End Set
    End Property
    Public Property dPRM_OrderDate() As Date
        Get
            Return (PRM_OrderDate)
        End Get
        Set(ByVal Value As Date)
            PRM_OrderDate = Value
        End Set
    End Property
    Public Property dPRM_InvoiceDate() As Date
        Get
            Return (PRM_InvoiceDate)
        End Get
        Set(ByVal Value As Date)
            PRM_InvoiceDate = Value
        End Set
    End Property
    Public Property iPRM_Supplier() As Integer
        Get
            Return (PRM_Supplier)
        End Get
        Set(ByVal Value As Integer)
            PRM_Supplier = Value
        End Set
    End Property
    Public Property sPRM_DocumentRefNo() As String
        Get
            Return (PRM_DocumentRefNo)
        End Get
        Set(ByVal Value As String)
            PRM_DocumentRefNo = Value
        End Set
    End Property

    Public Property iPRM_CompID() As Integer
        Get
            Return (PRM_CompID)
        End Get
        Set(ByVal Value As Integer)
            PRM_CompID = Value
        End Set
    End Property

    Public Property iPRD_ID() As Integer
        Get
            Return (PRD_ID)
        End Get
        Set(ByVal Value As Integer)
            PRD_ID = Value
        End Set
    End Property


    Public Property iPRD_MasterID() As Integer
        Get
            Return (PRD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            PRD_MasterID = Value
        End Set
    End Property
    Public Property iPRD_OrderNo() As Integer
        Get
            Return (PRD_OrderNo)
        End Get
        Set(ByVal Value As Integer)
            PRD_OrderNo = Value
        End Set
    End Property
    Public Property iPRD_Commodity() As Integer
        Get
            Return (PRD_Commodity)
        End Get
        Set(ByVal Value As Integer)
            PRD_Commodity = Value
        End Set
    End Property
    Public Property iPRD_DescID() As Integer
        Get
            Return (PRD_DescID)
        End Get
        Set(ByVal Value As Integer)
            PRD_DescID = Value
        End Set
    End Property
    Public Property iPRD_HistoryID() As Integer
        Get
            Return (PRD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            PRD_HistoryID = Value
        End Set
    End Property
    Public Property iPRD_UnitID() As Integer
        Get
            Return (PRD_UnitID)
        End Get
        Set(ByVal Value As Integer)
            PRD_UnitID = Value
        End Set
    End Property
    Public Property dPRD_OrderQuntity() As Double
        Get
            Return (PRD_OrderQuntity)
        End Get
        Set(ByVal Value As Double)
            PRD_OrderQuntity = Value
        End Set
    End Property

    Public Property sPRD_OrderRate() As Double
        Get
            Return (PRD_OrderRate)
        End Get
        Set(ByVal Value As Double)
            PRD_OrderRate = Value
        End Set
    End Property

    Public Property dPRD_MRP() As Double
        Get
            Return (PRD_MRP)
        End Get
        Set(ByVal Value As Double)
            PRD_MRP = Value
        End Set
    End Property

    Public Property dPRD_RecievedQnt() As Double
        Get
            Return (PRD_RecievedQnt)
        End Get
        Set(ByVal Value As Double)
            PRD_RecievedQnt = Value
        End Set
    End Property

    Public Property dPRD_Rejected() As Double
        Get
            Return (PRD_Rejected)
        End Get
        Set(ByVal Value As Double)
            PRD_Rejected = Value
        End Set
    End Property

    Public Property dPRD_Discount() As Double
        Get
            Return (PRD_Discount)
        End Get
        Set(ByVal Value As Double)
            PRD_Discount = Value
        End Set
    End Property

    Public Property dPRD_DiscountAmount() As Double
        Get
            Return (PRD_DiscountAmount)
        End Get
        Set(ByVal Value As Double)
            PRD_DiscountAmount = Value
        End Set
    End Property

    Public Property dPRD_Excise() As Double
        Get
            Return (PRD_Excise)
        End Get
        Set(ByVal Value As Double)
            PRD_Excise = Value
        End Set
    End Property

    Public Property dPRD_ExciseAMount() As Double
        Get
            Return (PRD_ExciseAMount)
        End Get
        Set(ByVal Value As Double)
            PRD_ExciseAMount = Value
        End Set
    End Property

    Public Property dPRD_VAT() As Double
        Get
            Return (PRD_VAT)
        End Get
        Set(ByVal Value As Double)
            PRD_VAT = Value
        End Set
    End Property

    Public Property dPRD_Accepted() As Double
        Get
            Return (PRD_Accepted)
        End Get
        Set(ByVal Value As Double)
            PRD_Accepted = Value
        End Set
    End Property

    Public Property sPRD_VATAmount() As String
        Get
            Return (PRD_VATAmount)
        End Get
        Set(ByVal Value As String)
            PRD_Accepted = Value
        End Set
    End Property

    Public Property dPRD_CST() As Double
        Get
            Return (sPRD_VATAmount)
        End Get
        Set(ByVal Value As Double)
            PRD_Accepted = Value
        End Set
    End Property

    Public Property dPRD_CSTAmount() As Double
        Get
            Return (sPRD_VATAmount)
        End Get
        Set(ByVal Value As Double)
            PRD_Accepted = Value
        End Set
    End Property
    Public Property dPRD_TotalAmount() As Double
        Get
            Return (sPRD_VATAmount)
        End Get
        Set(ByVal Value As Double)
            PRD_Accepted = Value
        End Set
    End Property

    Public Property sPRD_Status() As String
        Get
            Return (PRD_Status)
        End Get
        Set(ByVal Value As String)
            PRD_Status = Value
        End Set
    End Property

    Public Property iPRD_CompID() As Integer
        Get
            Return (PRD_CompID)
        End Get
        Set(ByVal Value As Integer)
            PRD_CompID = Value
        End Set
    End Property

    Public Property dPRD_ExcessQty() As Double
        Get
            Return (PRD_ExcessQty)
        End Get
        Set(ByVal Value As Double)
            PRD_ExcessQty = Value
        End Set
    End Property

    Public Property dPRD_ManufactureDate() As Date
        Get
            Return (PRD_ManufactureDate)
        End Get
        Set(ByVal Value As Date)
            PRD_ManufactureDate = Value
        End Set
    End Property

    Public Property dPRD_ExpireDate() As Date
        Get
            Return (PRD_ExpireDate)
        End Get
        Set(ByVal Value As Date)
            PRD_ExpireDate = Value
        End Set
    End Property

    Public Property sPRM_IPAddress() As String
        Get
            Return (PRM_IPAddress)
        End Get
        Set(ByVal Value As String)
            PRM_IPAddress = Value
        End Set
    End Property

    Public Property sPRD_IPAddress() As String
        Get
            Return (PRD_IPAddress)
        End Get
        Set(ByVal Value As String)
            PRD_IPAddress = Value
        End Set
    End Property

    Public Property sPRM_DcNo() As String
        Get
            Return (PRM_DcNo)
        End Get
        Set(ByVal Value As String)
            PRM_DcNo = Value
        End Set
    End Property

    Public Property sPRM_DelFlag() As String
        Get
            Return (PRM_DelFlag)
        End Get
        Set(ByVal Value As String)
            PRM_DelFlag = Value
        End Set
    End Property

    Public Property sPRD_DelFlag() As String
        Get
            Return (PRD_Delflag)
        End Get
        Set(ByVal Value As String)
            PRD_Delflag = Value
        End Set
    End Property
    Public Property dPRD_PendIng() As Double
        Get
            Return (PRD_PendIng)
        End Get
        Set(ByVal Value As Double)
            PRD_PendIng = Value
        End Set
    End Property
    Public Function GeneratePurchaseRegCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMonth As String = ""
        Dim sMonthCode As String = ""
        Dim sDate As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""
        Try
            sMaximumID = objDb.SQLGetDescription(sNameSpace, "Select Top 1 PRM_ID From Purchase_Registry_master Order By PRM_ID Desc")
            sYear = objDb.SQLGetDescription(sNameSpace, "Select Year(GetDate())")
            sMonth = objDb.SQLGetDescription(sNameSpace, "Select Month(GetDate())")
            sDate = objDb.SQLGetDescription(sNameSpace, "Select Day(GetDate())")
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
            sStr = "" & sYear & "" & "" & sMonthCode & "" & "" & sDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRegExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal docRef As String, ByVal OrderNo As String) As Boolean
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select * From Purchase_Registry_master Where PRM_DocumentRefNo ='" & docRef & "' And PRM_CompID=" & iCompID & " and PRM_YearID =" & iYearID & " and PRM_OrderNo =" & OrderNo & ""
            sStatus = objDb.SQLCheckForRecord(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ExistingRegNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal docRef As String, ByVal OrderNo As String) As String
        Dim sSql As String = "", sStatus As String = ""
        Try
            sSql = "Select PRM_RegistryNo From Purchase_Registry_master Where PRM_DocumentRefNo ='" & docRef & "' And PRM_CompID=" & iCompID & " and PRM_YearID =" & iYearID & " and PRM_OrderNo =" & OrderNo & ""
            sStatus = objDb.SQLExecuteScalar(sNameSpace, sSql)
            Return sStatus
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckVerifiedOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer, ByVal DcoRefNo As String) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select * from Purchase_verification where PV_OrderNo=" & OrderNo & " and PV_DocRefNo='" & DcoRefNo & "' and PV_YearID =" & iYearID & " and PV_CompID =" & iCompID & ""
            Return objDb.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckRegistredOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer, ByVal DcoRefNo As String) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "select * from Purchase_Registry_master where PRM_OrderNo=" & OrderNo & " and PRM_DocumentRefNo='" & DcoRefNo & "' and PRM_YearID =" & iYearID & " and PRM_CompID =" & iCompID & ""
            Return objDb.SQLCheckForRecord(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCurrentMonthID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SELECT MONTH(GETDATE())"
            Return objDb.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPODetailsFromGin(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal DocRefID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim TReceivedQty As Integer
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("BatchNumber", GetType(String))
            dtTab.Columns.Add(dc)
            sSql = "Select * From Purchase_gin_details Where PGD_MasterID In(Select PGM_ID from Purchase_gin_master where PGM_ID=" & DocRefID & " and PGM_OrderID=" & iMasterID & " and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & ") and PGD_CompID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    TReceivedQty = objDb.SQLExecuteScalarInt(sNameSpace, "Select sum(PRD_ReceivedQnt) from Purchase_Registry_details where PRD_MasterID In (Select PRM_ID from Purchase_Registry_master where PRM_OrderID=" & iMasterID & " and PRM_YearID =" & iYearID & " and PRM_CompID =" & iCompID & ") And PRD_DescID='" & dt.Rows(i)("PGD_DescriptionID") & "' and PRD_CompID =" & iCompID & "")
                    dr = dtTab.NewRow


                    If IsDBNull(dt.Rows(i)("PGD_CommodityID")) = False Then
                        dr("ComodityId") = dt.Rows(i)("PGD_CommodityID")
                    Else
                        dr("ComodityId") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_DescriptionID")) = False Then
                        dr("ItemId") = dt.Rows(i)("PGD_DescriptionID")
                    Else
                        dr("ItemId") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_HistoryID")) = False Then
                        dr("HistoryId") = dt.Rows(i)("PGD_HistoryID")
                    Else
                        dr("HistoryId") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_UnitID")) = False Then
                        dr("UnitId") = dt.Rows(i)("PGD_UnitID")
                    Else
                        dr("UnitId") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_CommodityID")) = False Then
                        dr("Comodity") = objDb.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("PGD_CommodityID") & "' and Inv_CompID = " & iCompID & "")

                    Else
                        dr("Comodity") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_UnitID")) = False Then
                        dr("Units") = objDb.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("PGD_UnitID") & " and Mas_CompID =" & iCompID & "")
                    Else
                        dr("Units") = objDb.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("PGD_UnitID") & " and Mas_CompID =" & iCompID & "")
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_DescriptionID")) = False Then
                        dr("Descriptions") = objDb.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID='" & dt.Rows(i)("PGD_DescriptionID") & "' and Inv_CompID = " & iCompID & "")
                    Else
                        dr("Descriptions") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_MRP")) = False Then
                        dr("Mrp") = dt.Rows(i)("PGD_MRP")
                    Else
                        dr("Mrp") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_OrderQnt")) = False Then
                        dr("OrderedQty") = dt.Rows(i)("PGD_OrderQnt")
                    Else
                        dr("OrderedQty") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_ReceivedQnt")) = False Then
                        dr("ReceivedQty") = dt.Rows(i)("PGD_ReceivedQnt")
                    Else
                        dr("ReceivedQty") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_Accepted")) = False Then
                        dr("AccpetedQty") = dt.Rows(i)("PGD_Accepted")
                    Else
                        dr("AccpetedQty") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_RejectedQnt")) = False Then
                        dr("RejectedQty") = dt.Rows(i)("PGD_RejectedQnt")
                    Else
                        dr("RejectedQty") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_Excess")) = False Then
                        dr("ExcessQty") = dt.Rows(i)("PGD_Excess")
                    Else
                        dr("ExcessQty") = 0
                    End If



                    If (dt.Rows(i)("PGD_ManufactureDate") <> "#1/1/1900 12:00:00 AM#") Then
                        dr("MDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PGD_ManufactureDate"), "D")
                    Else
                        dr("MDate") = ""
                    End If
                    If (dt.Rows(i)("PGD_ExpireDate") <> "#1/1/1900 12:00:00 AM#") Then
                        dr("Edate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PGD_ExpireDate"), "D")
                    Else
                        dr("Edate") = ""
                    End If

                    If IsDBNull(dt.Rows(i)("PGD_OrderQnt")) = False Then
                        dr("PendingItem") = Convert.ToDecimal(dt.Rows(i)("PGD_OrderQnt")) - TReceivedQty
                    Else
                        dr("PendingItem") = 0
                    End If
                    If IsDBNull(dt.Rows(i)("PGD_BatchNumber")) = False Then
                        dr("BatchNumber") = dt.Rows(i)("PGD_BatchNumber")
                    Else
                        dr("BatchNumber") = 0
                    End If
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTransactionInvoiceDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objRegistry As clsPurchaseRegistry) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRegistry.iPRM_OrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_GINID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRegistry.sPRM_RegistryNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_Commodity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRegistry.PRD_Commodity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_DescriptionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRegistry.PRD_DescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_InvoiceRefNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objRegistry.sPRM_DocumentRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRegistry.iPRD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRegistry.iPRD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_AcceptedQnt", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objRegistry.dPRD_Accepted
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_MRP", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objRegistry.dPRD_MRP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objRegistry.sPRD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_Excess", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objRegistry.PRD_ExcessQty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIA_YearID", OleDb.OleDbType.Integer, 4)
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

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "sPPurchaseInvoiceAccepted", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ExistingInwardDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal DocRef As String, ByVal orderID As Integer) As DataTable
        Dim dt, dtTab As New DataTable
        Dim txtorderqty As String = "", sSql As String = ""
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("PGM_DocumentRefNo")
            dtTab.Columns.Add("PGM_Gin_Number")
            dtTab.Columns.Add("PGM_OrderDate")
            dtTab.Columns.Add("PGM_ESugamNo")
            dtTab.Columns.Add("PGM_InvoiceDate")
            dtTab.Columns.Add("PGM_OrderID")
            dtTab.Columns.Add("PGM_DcNo")
            dtTab.Columns.Add("PGM_Supplier")
            dtTab.Columns.Add("PGM_Status")
            sSql = "" : sSql = "Select * From Purchase_GIN_master Where PGM_CompID=" & iCompID & " and PGM_YearID =" & iYearID & " and PGM_DocumentRefNo='" & DocRef & "' and PGM_OrderID=" & orderID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("PGM_DocumentRefNo")) = False Then
                    dr("PGM_DocumentRefNo") = dt.Rows(i)("PGM_DocumentRefNo")
                Else
                    dr("PGM_DocumentRefNo") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_Gin_Number")) = False Then
                    dr("PGM_Gin_Number") = dt.Rows(i)("PGM_Gin_Number")
                Else
                    dr("PGM_Gin_Number") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_OrderDate")) = False Then
                    dr("PGM_OrderDate") = dt.Rows(i)("PGM_OrderDate")
                Else
                    dr("PGM_OrderDate") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_ESugamNo")) = False Then
                    dr("PGM_ESugamNo") = dt.Rows(i)("PGM_ESugamNo")
                Else
                    dr("PGM_ESugamNo") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_InvoiceDate")) = False Then
                    dr("PGM_InvoiceDate") = dt.Rows(i)("PGM_InvoiceDate")
                Else
                    dr("PGM_InvoiceDate") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_OrderID")) = False Then
                    dr("PGM_OrderID") = dt.Rows(i)("PGM_OrderID")
                Else
                    dr("PGM_OrderID") = 0
                End If

                If IsDBNull(dt.Rows(i)("PGM_DcNo")) = False Then
                    dr("PGM_DcNo") = dt.Rows(i)("PGM_DcNo")
                Else
                    dr("PGM_DcNo") = ""
                End If

                If IsDBNull(dt.Rows(i)("PGM_Supplier")) = False Then
                    dr("PGM_Supplier") = dt.Rows(i)("PGM_Supplier")
                Else
                    dr("PGM_Supplier") = 0
                End If

                If IsDBNull(dt.Rows(i)("PGM_ESugamNo")) = False Then
                    dr("PGM_ESugamNo") = dt.Rows(i)("PGM_ESugamNo")
                Else
                    dr("PGM_ESugamNo") = 0
                End If

                If IsDBNull(dt.Rows(i)("PGM_Status")) = False Then
                    If (dt.Rows(i)("PGM_Status") = "W") Then
                        dr("PGM_Status") = "Waiting For Approval"
                    Else
                        dr("PGM_Status") = "Approved"
                    End If
                    'dr("PGM_Status") = dt.Rows(i)("PGM_Status")
                Else
                    dr("PGM_Status") = ""
                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iExistInwardID As Integer) As Integer
        Dim sSql As String = ""
        Dim i As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Purchase_registry_master where PRM_ID=" & iExistInwardID & " And PRM_CompID=" & iCompID & " And PRM_YearID =" & iYearID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "Begin "
                    sSql = sSql & "Update Purchase_registry_master Set PRM_Status='A',PRM_ApprovedBy=" & iUserID & ",PRM_ApprovedOn=GetDate() where PRM_id = " & dt.Rows(i)("PRM_id") & ""
                    sSql = sSql & " End"
                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
            sSql = "" : sSql = "Select * from Purchase_registry_details  Where PRD_MasterID=" & iExistInwardID & " and PRD_CompID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sSql = "Begin "
                    sSql = sSql & "Update Purchase_registry_details Set PRD_Status='A',PRD_ApprovedBy=" & iUserID & ",PRD_ApprovedOn=GetDate() where PRD_id = " & dt.Rows(i)("PRD_id") & ""
                    sSql = sSql & " End"
                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function




    Public Function ExistingRegistryDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal GinNo As Integer) As DataTable
        Dim dt, dtTab As New DataTable
        Dim txtorderqty As String = "", sSql As String = ""
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("PRM_DocumentRefNo")
            dtTab.Columns.Add("PRM_Registry_Number")
            dtTab.Columns.Add("PRM_OrderDate")
            dtTab.Columns.Add("PRM_ESugamNo")
            dtTab.Columns.Add("PRM_InvoiceDate")
            dtTab.Columns.Add("PRM_OrderID")
            dtTab.Columns.Add("PRM_DcNo")
            dtTab.Columns.Add("PRM_Supplier")
            dtTab.Columns.Add("PRM_Status")

            sSql = "" : sSql = "Select * From Purchase_Registry_master Where PRM_CompID=" & iCompID & " and PRM_YearID =" & iYearID & " and PRM_ID=" & GinNo & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("PRM_DocumentRefNo")) = False Then
                    dr("PRM_DocumentRefNo") = dt.Rows(i)("PRM_DocumentRefNo")
                Else
                    dr("PRM_DocumentRefNo") = ""
                End If

                If IsDBNull(dt.Rows(i)("PRM_RegistryNo")) = False Then
                    dr("PRM_Registry_Number") = dt.Rows(i)("PRM_RegistryNo")
                Else
                    dr("PRM_Registry_Number") = ""
                End If

                If IsDBNull(dt.Rows(i)("PRM_OrderDate")) = False Then
                    dr("PRM_OrderDate") = dt.Rows(i)("PRM_OrderDate")
                Else
                    dr("PRM_OrderDate") = ""
                End If

                If IsDBNull(dt.Rows(i)("PRM_ESugamNo")) = False Then
                    dr("PRM_ESugamNo") = dt.Rows(i)("PRM_ESugamNo")
                Else
                    dr("PRM_ESugamNo") = ""
                End If

                If IsDBNull(dt.Rows(i)("PRM_InvoiceDate")) = False Then
                    dr("PRM_InvoiceDate") = dt.Rows(i)("PRM_InvoiceDate")
                Else
                    dr("PRM_InvoiceDate") = ""
                End If

                If IsDBNull(dt.Rows(i)("PRM_OrderNo")) = False Then
                    dr("PRM_OrderID") = dt.Rows(i)("PRM_OrderNo")
                Else
                    dr("PRM_OrderID") = 0
                End If

                If IsDBNull(dt.Rows(i)("PRM_DcNo")) = False Then
                    dr("PRM_DcNo") = dt.Rows(i)("PRM_DcNo")
                Else
                    dr("PRM_DcNo") = ""
                End If

                If IsDBNull(dt.Rows(i)("PRM_Supplier")) = False Then
                    dr("PRM_Supplier") = dt.Rows(i)("PRM_Supplier")
                Else
                    dr("PRM_Supplier") = 0
                End If


                If IsDBNull(dt.Rows(i)("PRM_Status")) = False Then
                    If (dt.Rows(i)("PRM_Status") = "W") Then
                        dr("PRM_Status") = "Waiting For Approval"
                    Else
                        dr("PRM_Status") = "Approved"
                    End If
                    'dr("PGM_Status") = dt.Rows(i)("PGM_Status")
                Else
                    dr("PGM_Status") = ""
                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Shared Function LoadOurRefNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "select POM_OrderNo,POM_ID,sum(POD_Quantity) As orderdQty,sum(PRD_RecievedQnt) As RecvedQty from purchase_order_master
    '                Left Join Purchase_Order_Details on POM_ID=POD_MasterID 
    '                Left Join Purchase_Registry_Details on POM_ID=PRD_OrderNo
    '                group by POM_OrderNo, POM_ID, POM_CompID, POM_YearID, POM_Status having 
    '                (Convert(varchar, sum(PRD_RecievedQnt)) Is NULL Or sum(PRD_RecievedQnt)<sum(POD_Quantity)) and POM_CompID=" & iCompID & " And POM_YearID =" & iYearID & " and POM_Status='A' order by POM_ID desc"
    '        ' sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_Status<>'W' And POM_CompID=" & iCompID & " and POM_YearID =" & iYearID & " Order By POM_ID desc "
    '        Return DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    'Public Shared Function LoadPurchaseOderMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoice As Integer, ByVal iRegistryNo As Integer, ByVal iDocRefNo As Integer)
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim dtNew As New DataTable

    '    Dim dRow As DataRow
    '    Try
    '        dtNew.Columns.Add("POM_ID")
    '        dtNew.Columns.Add("POM_OrderDate")
    '        dtNew.Columns.Add("POM_Supplier")
    '        dtNew.Columns.Add("POM_SupplierID")
    '        dtNew.Columns.Add("DocRefNo")
    '        dtNew.Columns.Add("PGM_DcNo")
    '        dtNew.Columns.Add("InvoiceDate")
    '        dtNew.Columns.Add("E-sugamNo")

    '        sSql = "" : sSql = "Select * from  Purchase_Order_Master where POM_ID = " & iInvoice & "  and POM_CompID = " & iCompID & " and POM_YearID =" & iYearID & ""
    '        dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        dRow = dtNew.NewRow
    '        dRow("POM_ID") = dt.Rows(0)("POM_ID")
    '        dRow("POM_OrderDate") = clsTRACeGeneral.FormatDtForRDBMS(dt.Rows(0)("POM_OrderDate"), "D")
    '        dRow("POM_Supplier") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "CSM_Name", "CSM_ID", dt.Rows(0)("POM_Supplier"), "CustomerSupplierMaster")
    '        dRow("POM_SupplierId") = dt.Rows(0)("POM_Supplier")
    '        dRow("DocRefNo") = ""
    '        dRow("E-sugamNo") = ""
    '        dRow("PGM_DcNo") = ""
    '        dtNew.Rows.Add(dRow)
    '        If iRegistryNo <> 0 Then
    '            dt.Clear()
    '            dtNew.Clear()
    '            sSql = "" : sSql = "Select * from  Purchase_Registry_master where PRM_OrderNo = " & iInvoice & " and PRM_ID=" & iRegistryNo & " and PRM_YearID=" & iYearID & " and PRM_CompID = " & iCompID & ""
    '            dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '            dRow = dtNew.NewRow
    '            dRow("POM_ID") = dt.Rows(0)("PRM_ID")
    '            dRow("POM_OrderDate") = clsTRACeGeneral.FormatDtForRDBMS(dt.Rows(0)("PRM_OrderDate"), "D")
    '            dRow("POM_Supplier") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "CSM_Name", "CSM_ID", dt.Rows(0)("PRM_Supplier"), "CustomerSupplierMaster")
    '            dRow("POM_SupplierId") = dt.Rows(0)("PRM_Supplier")
    '            dRow("DocRefNo") = dt.Rows(0)("PRM_DocumentRefNo")
    '            dRow("InvoiceDate") = clsTRACeGeneral.FormatDtForRDBMS(dt.Rows(0)("PRM_InvoiceDate"), "D")
    '            dRow("PGM_DcNo") = dt.Rows(0)("PRM_DcNo")
    '            dRow("E-sugamNo") = dt.Rows(0)("PRM_ESugamNo")
    '            dtNew.Rows.Add(dRow)
    '            Return dtNew
    '        End If
    '        If iDocRefNo <> 0 Then
    '            dt.Clear()
    '            dtNew.Clear()
    '            sSql = "" : sSql = "Select * from  Purchase_GIN_Master where PGM_OrderID = " & iInvoice & " and PGM_ID=" & iDocRefNo & " and PGM_YearID =" & iYearID & " and PGM_CompID = " & iCompID & ""
    '            dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '            dRow = dtNew.NewRow
    '            dRow("POM_ID") = dt.Rows(0)("PGM_ID")
    '            dRow("POM_OrderDate") = clsTRACeGeneral.FormatDtForRDBMS(dt.Rows(0)("PGM_OrderDate"), "D")
    '            dRow("POM_Supplier") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "CSM_Name", "CSM_ID", dt.Rows(0)("PGM_Supplier"), "CustomerSupplierMaster")
    '            dRow("POM_SupplierId") = dt.Rows(0)("PGM_Supplier")
    '            dRow("DocRefNo") = dt.Rows(0)("PGM_DocumentRefNo")
    '            dRow("InvoiceDate") = clsTRACeGeneral.FormatDtForRDBMS(dt.Rows(0)("PGM_InvoiceDate"), "D")
    '            dRow("PGM_DcNo") = dt.Rows(0)("PGM_DcNo")
    '            dRow("E-sugamNo") = dt.Rows(0)("PGM_ESugamNo")
    '            dtNew.Rows.Add(dRow)
    '            Return dtNew
    '        End If
    '        Return dtNew
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Shared Function LoadPurchaseOderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvoice As Integer, ByVal iRegistryNo As Integer, ByVal iDocRefNo As Integer, ByVal iYearID As Integer)
    '    Dim sSql As String = ""
    '    Dim TReceivedQty As Integer
    '    Dim dt As New DataTable
    '    Dim dtNew As New DataTable
    '    Dim dRow As DataRow
    '    Dim date1 As String = ""
    '    Try
    '        dtNew.Columns.Add("Commodity")
    '        dtNew.Columns.Add("Description")
    '        dtNew.Columns.Add("CommodityId")
    '        dtNew.Columns.Add("DescriptionId")
    '        dtNew.Columns.Add("Unit")
    '        dtNew.Columns.Add("UnitId")
    '        dtNew.Columns.Add("Rate")
    '        dtNew.Columns.Add("OrderedQty")
    '        dtNew.Columns.Add("AccpetedQty")
    '        dtNew.Columns.Add("RateAmount")
    '        dtNew.Columns.Add("Discount")
    '        dtNew.Columns.Add("DiscountAmount")
    '        dtNew.Columns.Add("Excise")
    '        dtNew.Columns.Add("ExciseAmount")
    '        dtNew.Columns.Add("VAT")
    '        dtNew.Columns.Add("VATAmount")
    '        dtNew.Columns.Add("CST")
    '        dtNew.Columns.Add("CSTAmount")
    '        dtNew.Columns.Add("TotalAmount")
    '        dtNew.Columns.Add("HistoryID")
    '        dtNew.Columns.Add("ExcessQty")
    '        dtNew.Columns.Add("MDate")
    '        dtNew.Columns.Add("Edate")

    '        If iRegistryNo = 0 And iDocRefNo = 0 Then
    '            sSql = "" : sSql = "Select  POD_ID,POD_MasterID,POD_Commodity,POD_DescriptionID,POD_HistoryID,POD_Unit,POD_Rate,POD_Quantity,POD_RateAmount,POD_Discount,
    '                            POD_DiscountAmount,POD_Excise,POD_ExciseAmount,POD_VAT,
    '                            POD_VATAmount,POD_CST,POD_CSTAmount,POD_RequiredDate,POD_TotalAmount,POD_CompID,b.Inv_ID as CommodityId,b.Inv_Description as Commodity,c.Inv_ID,c.Inv_Description 
    '                            from Purchase_Order_Details a
    '                            Join Inventory_Master b on a.POD_Commodity=b.Inv_ID
    '                            Join Inventory_Master c on a.POD_DescriptionID=c.Inv_ID  where POD_MasterID=" & iInvoice & " and POD_CompID = " & iCompID & " and POD_Status<>'D'"
    '            dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '            For i = 0 To dt.Rows.Count - 1
    '                dRow = dtNew.NewRow
    '                TReceivedQty = DBHelper.SQLDBExecScalarInteger(sNameSpace, "Select sum(PRD_RecievedQnt) from Purchase_Registry_Details where PRD_MasterID in (select  PRM_ID from Purchase_Registry_master where PRM_OrderNo=" & iInvoice & " and PRM_YearID = " & iYearID & " and PRM_CompID =" & iCompID & ") And PRD_DescID ='" & dt.Rows(i)("POD_DescriptionID") & "' and PRD_CompID =" & iCompID & "")
    '                If ((dt.Rows(i)("POD_Quantity") - TReceivedQty) > 0) Then
    '                    dRow("CommodityId") = dt.Rows(i)("POD_Commodity")
    '                    dRow("DescriptionId") = dt.Rows(i)("POD_DescriptionID")
    '                    dRow("Commodity") = dt.Rows(i)("Commodity")
    '                    dRow("Description") = dt.Rows(i)("Inv_Description")
    '                    dRow("Unit") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "Mas_Desc", "Mas_ID", dt.Rows(i)("POD_Unit"), "acc_General_master")
    '                    dRow("UnitId") = dt.Rows(i)("POD_Unit")
    '                    dRow("Rate") = dt.Rows(i)("POD_Rate")
    '                    dRow("OrderedQty") = dt.Rows(i)("POD_Quantity")
    '                    dRow("AccpetedQty") = ""
    '                    dRow("RateAmount") = dt.Rows(i)("POD_RateAmount")
    '                    dRow("Discount") = dt.Rows(i)("POD_Discount")
    '                    dRow("DiscountAmount") = dt.Rows(i)("POD_DiscountAmount")
    '                    dRow("Excise") = dt.Rows(i)("POD_Excise")
    '                    dRow("ExciseAmount") = dt.Rows(i)("POD_ExciseAmount")
    '                    dRow("VAT") = dt.Rows(i)("POD_VAT")
    '                    dRow("VATAmount") = dt.Rows(i)("POD_VATAmount")
    '                    dRow("CST") = dt.Rows(i)("POD_CST")
    '                    dRow("CSTAmount") = dt.Rows(i)("POD_CSTAmount")
    '                    dRow("TotalAmount") = dt.Rows(i)("POD_TotalAmount")
    '                    dRow("HistoryID") = dt.Rows(i)("POD_HistoryID")
    '                    dRow("ExcessQty") = ""
    '                    dRow("MDate") = ""
    '                    dRow("Edate") = ""
    '                    dtNew.Rows.Add(dRow)
    '                End If
    '            Next
    '        End If

    '        If iDocRefNo <> 0 Then
    '            dt.Clear()
    '            dtNew.Clear()
    '            sSql = "" : sSql = "Select * from Purchase_GIN_Details where PGD_OrderID=" & iInvoice & " and PGD_MasterID=" & iDocRefNo & " and PGD_CompID = " & iCompID & ""
    '            dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '            For i = 0 To dt.Rows.Count - 1
    '                dRow = dtNew.NewRow

    '                dRow("CommodityId") = dt.Rows(i)("PGD_CommodityID")
    '                dRow("DescriptionId") = dt.Rows(i)("PGD_DescriptionID")
    '                dRow("Commodity") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "Inv_Description", "Inv_ID", dt.Rows(i)("PGD_CommodityID"), "Inventory_Master")
    '                dRow("Description") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "Inv_Description", "Inv_ID", dt.Rows(i)("PGD_DescriptionID"), "Inventory_Master")
    '                dRow("Unit") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "Mas_Desc", "Mas_ID", dt.Rows(i)("PGD_UnitID"), "acc_General_master")
    '                dRow("UnitId") = dt.Rows(i)("PGD_UnitID")
    '                dRow("Rate") = dt.Rows(i)("PGD_MRP")
    '                dRow("OrderedQty") = dt.Rows(i)("PGD_OrderQnt")
    '                dRow("AccpetedQty") = dt.Rows(i)("PGD_ReceivedQnt")
    '                dRow("HistoryID") = dt.Rows(i)("PGD_HistoryID")
    '                dRow("ExcessQty") = dt.Rows(i)("PGD_Excess")
    '                date1 = ""
    '                date1 = clsTRACeGeneral.FormatDtForRDBMS((dt.Rows(i)("PGD_ManufactureDate")), "D")
    '                If date1 = "01/01/1900" Or date1 = "30/12/1899" Then
    '                    dRow("MDate") = ""
    '                Else
    '                    dRow("MDate") = date1
    '                End If
    '                date1 = ""
    '                date1 = clsTRACeGeneral.FormatDtForRDBMS((dt.Rows(i)("PGD_ExpireDate")), "D")
    '                If date1 = "01/01/1900" Or date1 = "30/12/1899" Then
    '                    dRow("Edate") = ""
    '                Else
    '                    dRow("Edate") = date1
    '                End If
    '                dtNew.Rows.Add(dRow)
    '            Next
    '        End If
    '        If iRegistryNo <> 0 Then
    '            dt.Clear()
    '            dtNew.Clear()
    '            sSql = "" : sSql = "Select * from Purchase_Registry_Details where PRD_OrderNo=" & iInvoice & " and  PRD_MasterID=" & iRegistryNo & " and PRD_CompID = " & iCompID & ""
    '            dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '            For i = 0 To dt.Rows.Count - 1
    '                dRow = dtNew.NewRow

    '                dRow("CommodityId") = dt.Rows(i)("PRD_Commodity")
    '                dRow("DescriptionId") = dt.Rows(i)("PRD_DescID")
    '                dRow("Commodity") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "Inv_Description", "Inv_ID", dt.Rows(i)("PRD_Commodity"), "Inventory_Master")
    '                dRow("Description") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "Inv_Description", "Inv_ID", dt.Rows(i)("PRD_DescID"), "Inventory_Master")
    '                dRow("Unit") = clsTRACeGeneral.GetColumnDescriptionInt(sNameSpace, "Mas_Desc", "Mas_ID", dt.Rows(i)("PRD_UnitID"), "acc_General_master")
    '                dRow("UnitId") = dt.Rows(i)("PRD_UnitID")
    '                dRow("Rate") = dt.Rows(i)("PRD_MRP")
    '                dRow("OrderedQty") = dt.Rows(i)("PRD_OrderQuntity")
    '                dRow("AccpetedQty") = dt.Rows(i)("PRD_RecievedQnt")
    '                dRow("RateAmount") = dt.Rows(i)("PRD_OrderRate")
    '                dRow("Discount") = dt.Rows(i)("PRD_Discount")
    '                dRow("DiscountAmount") = dt.Rows(i)("PRD_DiscountAmount")
    '                dRow("Excise") = dt.Rows(i)("PRD_Excise")
    '                dRow("ExciseAmount") = dt.Rows(i)("PRD_ExciseAMount")
    '                dRow("VAT") = dt.Rows(i)("PRD_VAT")
    '                dRow("VATAmount") = dt.Rows(i)("PRD_VATAmount")
    '                dRow("CST") = dt.Rows(i)("PRD_CST")
    '                dRow("CSTAmount") = dt.Rows(i)("PRD_CSTAmount")
    '                dRow("TotalAmount") = dt.Rows(i)("PRD_TotalAmount")
    '                dRow("HistoryID") = dt.Rows(i)("PRD_HistoryID")

    '                dRow("ExcessQty") = dt.Rows(i)("PRD_ExcessQty")
    '                date1 = ""
    '                date1 = clsTRACeGeneral.FormatDtForRDBMS((dt.Rows(i)("PRD_ManufactureDate")), "D")
    '                If date1 = "01/01/1900" Or date1 = "30/12/1899" Then
    '                    dRow("MDate") = ""
    '                Else
    '                    dRow("MDate") = date1
    '                End If
    '                date1 = ""
    '                date1 = clsTRACeGeneral.FormatDtForRDBMS((dt.Rows(i)("PRD_ExpireDate")), "D")
    '                If date1 = "01/01/1900" Or date1 = "30/12/1899" Then
    '                    dRow("Edate") = ""
    '                Else
    '                    dRow("Edate") = date1
    '                End If
    '                dtNew.Rows.Add(dRow)
    '            Next
    '        End If
    '        Return dtNew
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadPurchaseRegistry(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal InwardNo As String) As DataTable
        Dim sSql As String = ""
        Dim sdate As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Dim qnt As Integer = 0
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("BatchNumber", GetType(String))
            dtTab.Columns.Add(dc)
            sSql = "Select * from Purchase_Registry_Details where PRD_CompID=" & iCompID & " and PRD_MasterID=" & InwardNo & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("ComodityId") = dt.Rows(i)("PRD_Commodity")
                    dr("ItemId") = dt.Rows(i)("PRD_DescID")
                    dr("HistoryId") = dt.Rows(i)("PRD_HistoryID")
                    dr("UnitId") = dt.Rows(i)("PRD_UnitID")
                    dr("Comodity") = objDb.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("PRD_Commodity") & "' and inv_CompID =" & iCompID & "")
                    dr("Units") = objDb.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID='" & dt.Rows(i)("PRD_UnitID") & "' and Mas_CompID = " & iCompID & "")
                    dr("Descriptions") = objDb.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID='" & dt.Rows(i)("PRD_DescID") & "' and inv_CompID =" & iCompID & "")
                    dr("Mrp") = dt.Rows(i)("PRD_MRP")
                    dr("OrderedQty") = dt.Rows(i)("PRD_OrderQuntity")
                    dr("ReceivedQty") = dt.Rows(i)("PRD_RecievedQnt")
                    dr("AccpetedQty") = dt.Rows(i)("PRD_RecievedQnt")
                    dr("RejectedQty") = 0
                    dr("ExcessQty") = dt.Rows(i)("PRD_Excise")
                    If (objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ManufactureDate"), "D") <> "01/01/0001") And (objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ManufactureDate"), "D") <> "01-01-0001") Then
                        dr("MDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ManufactureDate"), "D")
                        'dr("MDate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ManufactureDate"), "D")
                        'If (dr("MDate") = "01/01/1900") Then
                        '    dr("MDate") = ""
                        'End If
                    Else
                        dr("MDate") = ""
                    End If
                    If (objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ExpireDate"), "D") <> "01/01/1900") And (objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ExpireDate"), "D") <> "01-01-1900") Then
                        dr("Edate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ExpireDate"), "D")
                        'dr("Edate") = objFasGnrl.FormatDtForRDBMS(dt.Rows(i)("PRD_ExpireDate"), "D")
                        'If (dr("Edate") = "01/01/1900") Then
                        '    dr("Edate") = ""
                        'End If
                    Else
                        dr("Edate") = ""
                    End If
                    dr("PendingItem") = 0
                    dr("BatchNumber") = dt.Rows(i)("PRD_BatchNumber")
                    dr("Status") = objDb.SQLGetDescription(sNameSpace, "Select PRM_Status From Purchase_Registry_Master Where PRM_ID='" & dt.Rows(i)("PRD_MasterID") & "' and PRM_YearID =" & iYearId & " and PRM_CompID =" & iCompID & "")
                    dtTab.Rows.Add(dr)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPODetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim dr As DataRow
        Dim TReceivedQty As Integer
        Dim dtTab As New DataTable
        Try
            dc = New DataColumn("ComodityId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ItemId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("HistoryId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("UnitId", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Comodity", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Units", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Descriptions", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Mrp", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("OrderedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ReceivedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("AccpetedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("RejectedQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("ExcessQty", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("MDate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("Edate", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("PendingItem", GetType(String))
            dtTab.Columns.Add(dc)
            dc = New DataColumn("BatchNumber", GetType(String))
            dtTab.Columns.Add(dc)

            sSql = "" : sSql = "Select * From Purchase_Order_details Where POD_MasterID = " & iMasterID & " And POD_CompID=" & iCompID & " and POD_Status <> 'D'"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    TReceivedQty = objDb.SQLExecuteScalarInt(sNameSpace, "Select sum(PRD_RecievedQnt) from purchase_registry_details where PRD_MasterID in (select  PRM_ID from purchase_registry_master where PRM_OrderNo=" & iMasterID & " and PRM_YearID = " & iYearID & " and PRM_CompID =" & iCompID & ") And PRD_DescID ='" & dt.Rows(i)("POD_DescriptionID") & "' and PRD_CompID =" & iCompID & "")
                    If ((dt.Rows(i)("POD_Quantity") - TReceivedQty) > 0) Then
                        dr = dtTab.NewRow
                        dr("ComodityId") = dt.Rows(i)("POD_Commodity")
                        dr("ItemId") = dt.Rows(i)("POD_DescriptionID")
                        dr("HistoryId") = dt.Rows(i)("POD_HistoryID")
                        dr("UnitId") = dt.Rows(i)("POD_Unit")
                        dr("Comodity") = objDb.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("POD_Commodity") & "' and Inv_CompID =" & iCompID & "")
                        dr("Units") = objDb.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("POD_Unit") & " ")
                        dr("Descriptions") = objDb.SQLGetDescription(sNameSpace, "Select Inv_Description From Inventory_Master Where INV_ID='" & dt.Rows(i)("POD_DescriptionID") & "' and Inv_CompID =" & iCompID & "")
                        dr("Mrp") = dt.Rows(i)("POD_Rate")
                        dr("OrderedQty") = dt.Rows(i)("POD_Quantity")
                        If IsDBNull(dt.Rows(i)("POD_Quantity")) = False Then
                            dr("OrderedQty") = Math.Round(dt.Rows(i)("POD_Quantity"), 2)
                        End If
                        dr("ReceivedQty") = ""
                        dr("AccpetedQty") = ""
                        dr("RejectedQty") = ""
                        dr("ExcessQty") = ""
                        dr("MDate") = ""
                        dr("Edate") = ""
                        dr("BatchNumber") = ""
                        dr("PendingItem") = Convert.ToDecimal(dt.Rows(i)("POD_Quantity")) - TReceivedQty
                        dtTab.Rows.Add(dr)
                    End If
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadRegistryNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select * from Purchase_Registry_master where PRM_OrderNo in(select POM_ID from purchase_order_master where POM_OralStatus='P') and PRM_compid=" & iCompID & " and PRM_YearID = " & iYearID & " order by PRM_ID desc"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadExistDocRefNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select PGM_ID,PGM_DocumentRefNo From Purchase_GIN_Master Where PGM_OrderID=" & iID & " and PGM_Compid=" & iCompID & " and PGM_YearID =" & iYearID & " and PGM_Status='A' order by PGM_ID"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function PurchaseRegistryMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objPr As clsPurchaseRegistry) As Object
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Purchase_Registry_master where PRM_OrderNo=" & objPr.iPRM_OrderNo & " and PRM_RegistryNo = '" & objPr.sPRM_RegistryNo & "' and PRM_CompID =" & iCompID & " and PRM_YearID =" & iYearID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Purchase_Registry_master set PRM_InvoiceDate =" & objFasGnrl.FormatDtForRDBMS(objPr.dPRM_InvoiceDate, "I") & ","
                sSql = sSql & "PRM_Operation='U',PRM_IPAddress='" & objPr.sPRM_IPAddress & "' "
                sSql = sSql & "Where PRM_RegistryNo = '" & objPr.sPRM_RegistryNo & "' and PRM_CompID =" & iCompID & " and PRM_YearID =" & iYearID & " and PRM_DcNo='" & objPr.sPRM_DcNo & "' "
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dt.Rows(0)("PRM_ID")
            Else
                iMax = objGnrlFnctn.GetMaxID(sNameSpace, iCompID, "Purchase_Registry_master", "PRM_ID", "PRM_CompID")
                sSql = "" : sSql = "Insert into Purchase_Registry_master(PRM_ID,PRM_OrderNo,PRM_OrderDate,PRM_InvoiceDate,PRM_RegistryNo,PRM_Supplier,"
                sSql = sSql & "PRM_DocumentRefNo,PRM_CreatedBy,PRM_CreatedOn,PRM_YearID,PRM_Status,PRM_CompID,PRM_Operation,PRM_IPAddress,PRM_DcNo) "
                sSql = sSql & "Values(" & iMax & "," & objPr.iPRM_OrderNo & "," & objFasGnrl.FormatDtForRDBMS(objPr.dPRM_OrderDate, "I") & "," & objFasGnrl.FormatDtForRDBMS(objPr.dPRM_InvoiceDate, "I") & ","
                sSql = sSql & "'" & objPr.sPRM_RegistryNo & "'," & objPr.iPRM_Supplier & ",'" & objPr.sPRM_DocumentRefNo & "',"
                sSql = sSql & "" & objPr.iPRM_CreatedBy & "," & objFasGnrl.FormatDtForRDBMS(objPr.dPRM_CreatedOn, "I") & "," & iYearID & ",'" & objPr.iPRM_Status & "'," & iCompID & ",'C','" & objPr.sPRM_IPAddress & "','" & objPr.sPRM_DcNo & "')"
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTransactionReturnsDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal ObjReg As clsPurchaseRegistry) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_OrderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjReg.iPRD_OrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_GINID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjReg.sPRM_RegistryNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_Commodity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjReg.iPRD_Commodity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_DescriptionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjReg.iPRD_DescID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjReg.iPRD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_UnitID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjReg.iPRD_UnitID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_InvoiceRefNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjReg.sPRM_DocumentRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_AcceptedQnt", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjReg.dPRD_Accepted
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_RejectedQty", OleDb.OleDbType.Decimal, 10)
            ObjParam(iParamCount).Value = ObjReg.dPRD_Rejected
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_MRP", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjReg.dPRD_MRP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_Status", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = ObjReg.sPRD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PIR_YearID", OleDb.OleDbType.Integer, 4)
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

            Arr = objDb.ExecuteSPForInsertARR(sNameSpace, "sPPurchaseInvoiceRejected", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function PurchaseRegistryDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objPr As clsPurchaseRegistry) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            'sSql = "" : sSql = "Select * from Purchase_Registry_Details  where PRD_MasterID = " & objPr.iPRD_MasterID & " and PRD_Commodity = " & objPr.iPRD_Commodity & " and "
            'sSql = sSql & "PRD_DescID = " & objPr.iPRD_DescID & " and PRD_HistoryID =" & objPr.iPRD_HistoryID & " and PRD_CompID = " & iCompID & ""
            'dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            'If dt.Rows.Count > 0 Then
            '    sSql = "" : sSql = "Update Purchase_Registry_Details set PRD_OrderQuntity='" & objPr.dPRD_OrderQuntity & "',PRD_RecievedQnt = '" & objPr.dPRD_RecievedQnt & "',PRD_ExcessQty='" & objPr.dPRD_ExcessQty & "',"
            '    sSql = sSql & "PRD_ManufactureDate = " & objFasGnrl.FormatDtForRDBMS(objPr.dPRD_ManufactureDate, "I") & ",PRD_ExpireDate = " & objFasGnrl.FormatDtForRDBMS(objPr.dPRD_ExpireDate, "I") & ","
            '    sSql = sSql & "PRD_Operation='U',PRD_IPAddress='" & objPr.sPRD_IPAddress & "'"
            '    sSql = sSql & "where PRD_MasterID = " & objPr.iPRD_MasterID & " and "
            '    sSql = sSql & "PRD_Commodity = " & objPr.iPRD_Commodity & " and PRD_DescID = " & objPr.iPRD_DescID & " and "
            '    sSql = sSql & "PRD_HistoryID =" & objPr.iPRD_HistoryID & " and PRD_CompID = " & iCompID & ""
            '    objDb.SQLExecuteNonQuery(sNameSpace, sSql)

            '    Return "2"
            'Else
            iMax = objGnrlFnctn.GetMaxID(sNameSpace, iCompID, "Purchase_Registry_Details", "PRD_ID", "PRD_CompID")
            sSql = "" : sSql = "Insert into Purchase_Registry_Details (PRD_ID,PRD_MasterID,PRD_OrderNo,PRD_Commodity,PRD_DescID,PRD_HistoryID,PRD_UnitID,PRD_OrderQuntity,"
            sSql = sSql & "PRD_MRP,PRD_RecievedQnt,PRD_ExcessQty,PRD_ManufactureDate,PRD_ExpireDate,PRD_Status,PRD_CompID,PRD_Operation,PRD_IPAddress,PGD_PendingQnt,PRD_Rejected,PRD_AccptedQty) "
            sSql = sSql & "Values(" & iMax & "," & objPr.iPRD_MasterID & "," & objPr.iPRD_OrderNo & "," & objPr.iPRD_Commodity & "," & objPr.iPRD_DescID & "," & objPr.iPRD_HistoryID & ","
            sSql = sSql & "" & objPr.iPRD_UnitID & "," & objPr.dPRD_OrderQuntity & ",'" & objPr.dPRD_MRP & "'," & objPr.dPRD_RecievedQnt & "," & objPr.dPRD_ExcessQty & ","
            sSql = sSql & "" & objFasGnrl.FormatDtForRDBMS(objPr.dPRD_ManufactureDate, "I") & "," & objFasGnrl.FormatDtForRDBMS(objPr.dPRD_ExpireDate, "I") & ",'A','" & iCompID & "','C','" & objPr.sPRD_IPAddress & "'," & objPr.dPRD_PendIng & "," & objPr.dPRD_Rejected & "," & objPr.dPRD_Accepted & ")"
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
            Return "3"
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
