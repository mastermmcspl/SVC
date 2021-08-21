Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSalesReturn
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iSRM_ID As Integer
    Private sSRM_ReturnOrderCode As String
    Private iSRM_OrderNo As Integer
    Private dSRM_OrderDate As Date
    Private sSRM_ReferenceNo As String
    Private dSRM_ReturnDate As Date
    Private iSRM_PartyID As Integer
    Private sSRM_PartyCode As String
    Private iSRM_ModeOfReturn As Integer
    Private sSRM_Narration As String
    Private sSRM_Status As String
    Private iSRM_YearID As Integer
    Private iSRM_CompID As Integer
    Private iSRM_CreatedBy As Integer
    Private dSRM_CreatedOn As Date
    Private sSRM_Operation As String
    Private sSRM_IPAddress As String
    Private iSRM_DispatchID As Integer
    Private sSRM_DispatchRefNo As String
    Private sSRM_ESugamNo As String
    Private iSRM_PaymentType As Integer
    Private iSRM_Category As Integer
    Private iSRM_SaleType As Integer
    Private iSRM_OtherType As Integer
    Private dSRM_DispatchDate As Date
    Private iSRM_ReturnReason As Integer

    Private sSRD_Operation As String
    Private sSRD_IPAddress As String

    Private iSRD_ID As Integer
    Private iSRD_MasterID As Integer
    Private iSRD_CommodityID As Integer
    Private iSRD_DescriptionID As Integer
    Private iSRD_HistoryID As Integer
    Private iSRD_SaleQnty As Integer
    Private iSRD_ReturnQnty As Integer
    Private dSRD_Rate As Double
    Private iSRD_UnitOfMeasurement As Integer
    Private dSRD_TotalAmount As Double
    Private iSRD_CompID As Integer
    Private iSRD_YearID As Integer

    Private iSRD_Discount As Double
    Private sSRD_DiscountAmount As String
    Private iSRD_VAT As Double
    Private sSRD_VATAmount As String
    Private iSRD_CST As Double
    Private sSRD_CSTAmount As String
    Private iSRD_Excise As Double
    Private sSRD_ExciseAmount As String
    Private iSRD_Return As String
    Private sSRD_Status As String
    Private iSRD_RateAmount As Decimal
    Private iSRD_EnteredPrice As Decimal
    Private iSRD_DifferencePrice As Decimal

    Public Property SRM_Operation() As String
        Get
            Return (sSRM_Operation)
        End Get
        Set(ByVal Value As String)
            sSRM_Operation = Value
        End Set
    End Property
    Public Property SRM_IPAddress() As String
        Get
            Return (sSRM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSRM_IPAddress = Value
        End Set
    End Property
    Public Property SRM_ID() As Integer
        Get
            Return (iSRM_ID)
        End Get
        Set(ByVal Value As Integer)
            iSRM_ID = Value
        End Set
    End Property
    Public Property SRM_ReturnOrderCode() As String
        Get
            Return (sSRM_ReturnOrderCode)
        End Get
        Set(ByVal Value As String)
            sSRM_ReturnOrderCode = Value
        End Set
    End Property
    Public Property SRM_OrderNo() As Integer
        Get
            Return (iSRM_OrderNo)
        End Get
        Set(ByVal Value As Integer)
            iSRM_OrderNo = Value
        End Set
    End Property
    Public Property SRM_OrderDate() As Date
        Get
            Return (dSRM_OrderDate)
        End Get
        Set(ByVal Value As Date)
            dSRM_OrderDate = Value
        End Set
    End Property
    Public Property SRM_ReferenceNo() As String
        Get
            Return (sSRM_ReferenceNo)
        End Get
        Set(ByVal Value As String)
            sSRM_ReferenceNo = Value
        End Set
    End Property
    Public Property SRM_ReturnDate() As Date
        Get
            Return (dSRM_ReturnDate)
        End Get
        Set(ByVal Value As Date)
            dSRM_ReturnDate = Value
        End Set
    End Property
    Public Property SRM_PartyID() As Integer
        Get
            Return (iSRM_PartyID)
        End Get
        Set(ByVal Value As Integer)
            iSRM_PartyID = Value
        End Set
    End Property
    Public Property SRM_PartyCode() As String
        Get
            Return (sSRM_PartyCode)
        End Get
        Set(ByVal Value As String)
            sSRM_PartyCode = Value
        End Set
    End Property
    Public Property SRM_ModeOfReturn() As Integer
        Get
            Return (iSRM_ModeOfReturn)
        End Get
        Set(ByVal Value As Integer)
            iSRM_ModeOfReturn = Value
        End Set
    End Property
    Public Property SRM_Narration() As String
        Get
            Return (sSRM_Narration)
        End Get
        Set(ByVal Value As String)
            sSRM_Narration = Value
        End Set
    End Property
    Public Property SRM_Status() As String
        Get
            Return (sSRM_Status)
        End Get
        Set(ByVal Value As String)
            sSRM_Status = Value
        End Set
    End Property
    Public Property SRM_YearID() As Integer
        Get
            Return (iSRM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSRM_YearID = Value
        End Set
    End Property
    Public Property SRM_CompID() As Integer
        Get
            Return (iSRM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSRM_CompID = Value
        End Set
    End Property
    Public Property SRM_CreatedBy() As Integer
        Get
            Return (iSRM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iSRM_CreatedBy = Value
        End Set
    End Property
    Public Property SRM_CreatedOn() As Date
        Get
            Return (dSRM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dSRM_CreatedOn = Value
        End Set
    End Property
    Public Property SRD_ID() As Integer
        Get
            Return (iSRD_ID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_ID = Value
        End Set
    End Property
    Public Property SRD_MasterID() As Integer
        Get
            Return (iSRD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_MasterID = Value
        End Set
    End Property
    Public Property SRD_CommodityID() As Integer
        Get
            Return (iSRD_CommodityID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_CommodityID = Value
        End Set
    End Property
    Public Property SRD_DescriptionID() As Integer
        Get
            Return (iSRD_DescriptionID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_DescriptionID = Value
        End Set
    End Property
    Public Property SRD_HistoryID() As Integer
        Get
            Return (iSRD_HistoryID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_HistoryID = Value
        End Set
    End Property
    Public Property SRD_SaleQnty() As Integer
        Get
            Return (iSRD_SaleQnty)
        End Get
        Set(ByVal Value As Integer)
            iSRD_SaleQnty = Value
        End Set
    End Property
    Public Property SRD_ReturnQnty() As Integer
        Get
            Return (iSRD_ReturnQnty)
        End Get
        Set(ByVal Value As Integer)
            iSRD_ReturnQnty = Value
        End Set
    End Property
    Public Property SRD_Rate() As Double
        Get
            Return (dSRD_Rate)
        End Get
        Set(ByVal Value As Double)
            dSRD_Rate = Value
        End Set
    End Property
    Public Property SRD_UnitOfMeasurement() As Integer
        Get
            Return (iSRD_UnitOfMeasurement)
        End Get
        Set(ByVal Value As Integer)
            iSRD_UnitOfMeasurement = Value
        End Set
    End Property
    Public Property SRD_Discount() As Double
        Get
            Return (iSRD_Discount)
        End Get
        Set(ByVal Value As Double)
            iSRD_Discount = Value
        End Set
    End Property
    Public Property SRD_DiscountAmount() As String
        Get
            Return (sSRD_DiscountAmount)
        End Get
        Set(ByVal Value As String)
            sSRD_DiscountAmount = Value
        End Set
    End Property
    Public Property SRD_VAT() As Double
        Get
            Return (iSRD_VAT)
        End Get
        Set(ByVal Value As Double)
            iSRD_VAT = Value
        End Set
    End Property
    Public Property SRD_VATAmount() As String
        Get
            Return (sSRD_VATAmount)
        End Get
        Set(ByVal Value As String)
            sSRD_VATAmount = Value
        End Set
    End Property
    Public Property SRD_CST() As Double
        Get
            Return (iSRD_CST)
        End Get
        Set(ByVal Value As Double)
            iSRD_CST = Value
        End Set
    End Property
    Public Property SRD_CSTAmount() As String
        Get
            Return (sSRD_CSTAmount)
        End Get
        Set(ByVal Value As String)
            sSRD_CSTAmount = Value
        End Set
    End Property
    Public Property SRD_Excise() As Double
        Get
            Return (iSRD_Excise)
        End Get
        Set(ByVal Value As Double)
            iSRD_Excise = Value
        End Set
    End Property
    Public Property SRD_ExciseAmount() As String
        Get
            Return (sSRD_ExciseAmount)
        End Get
        Set(ByVal Value As String)
            sSRD_ExciseAmount = Value
        End Set
    End Property
    Public Property SRD_TotalAmount() As Double
        Get
            Return (dSRD_TotalAmount)
        End Get
        Set(ByVal Value As Double)
            dSRD_TotalAmount = Value
        End Set
    End Property
    Public Property SRD_CompID() As Integer
        Get
            Return (iSRD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_CompID = Value
        End Set
    End Property
    Public Property SRD_YearID() As Integer
        Get
            Return (iSRD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iSRD_YearID = Value
        End Set
    End Property
    Public Property SRD_Return() As Integer
        Get
            Return (iSRD_Return)
        End Get
        Set(ByVal Value As Integer)
            iSRD_Return = Value
        End Set
    End Property
    Public Property SRD_Operation() As String
        Get
            Return (sSRD_Operation)
        End Get
        Set(ByVal Value As String)
            sSRD_Operation = Value
        End Set
    End Property
    Public Property SRD_IPAddress() As String
        Get
            Return (sSRD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sSRD_IPAddress = Value
        End Set
    End Property
    Public Property SRD_Status() As String
        Get
            Return (sSRD_Status)
        End Get
        Set(ByVal Value As String)
            sSRD_Status = Value
        End Set
    End Property
    Public Property SRD_RateAmount() As Double
        Get
            Return (iSRD_RateAmount)
        End Get
        Set(ByVal Value As Double)
            iSRD_RateAmount = Value
        End Set
    End Property
    Public Property SRM_DispatchID() As Integer
        Get
            Return (iSRM_DispatchID)
        End Get
        Set(ByVal Value As Integer)
            iSRM_DispatchID = Value
        End Set
    End Property
    Public Property SRM_DispatchRefNo() As String
        Get
            Return (sSRM_DispatchRefNo)
        End Get
        Set(ByVal Value As String)
            sSRM_DispatchRefNo = Value
        End Set
    End Property
    Public Property SRM_ESugamNo() As String
        Get
            Return (sSRM_ESugamNo)
        End Get
        Set(ByVal Value As String)
            sSRM_ESugamNo = Value
        End Set
    End Property
    Public Property SRM_PaymentType() As Integer
        Get
            Return (iSRM_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            iSRM_PaymentType = Value
        End Set
    End Property
    Public Property SRM_ReturnReason() As Integer
        Get
            Return (iSRM_ReturnReason)
        End Get
        Set(ByVal Value As Integer)
            iSRM_ReturnReason = Value
        End Set
    End Property
    Public Property SRM_Category() As Integer
        Get
            Return (iSRM_Category)
        End Get
        Set(ByVal Value As Integer)
            iSRM_Category = Value
        End Set
    End Property
    Public Property SRM_SaleType() As Integer
        Get
            Return (iSRM_SaleType)
        End Get
        Set(ByVal Value As Integer)
            iSRM_SaleType = Value
        End Set
    End Property
    Public Property SRM_OtherType() As Integer
        Get
            Return (iSRM_OtherType)
        End Get
        Set(ByVal Value As Integer)
            iSRM_OtherType = Value
        End Set
    End Property
    Public Property SRM_DispatchDate() As Date
        Get
            Return (dSRM_DispatchDate)
        End Get
        Set(ByVal Value As Date)
            dSRM_DispatchDate = Value
        End Set
    End Property
    Public Property SRD_EnteredPrice() As Double
        Get
            Return (iSRD_EnteredPrice)
        End Get
        Set(ByVal Value As Double)
            iSRD_EnteredPrice = Value
        End Set
    End Property
    Public Property SRD_DifferencePrice() As Double
        Get
            Return (iSRD_DifferencePrice)
        End Get
        Set(ByVal Value As Double)
            iSRD_DifferencePrice = Value
        End Set
    End Property
    Public Function GetZeroVAT(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc='0' And Mas_Master=14 And Mas_CompID=" & iCompID & " And Mas_DelFlag='A' "
            GetZeroVAT = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZeroCST(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where  MAS_Desc='0' And Mas_Master=15 And Mas_CompID=" & iCompID & " And Mas_DelFlag='A' "
            GetZeroCST = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZeroExcise(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where  MAS_Desc='0' And Mas_Master=16 And Mas_CompID=" & iCompID & " And Mas_DelFlag='A' "
            GetZeroExcise = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
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
    Public Function GetMRP(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sHistoryID As String, ByVal sCode As String, ByVal iCategoryID As Integer, ByVal iItemID As Integer, ByVal iCommodityID As Integer, ByVal iorderNo As Integer, ByVal iInvoiceID As Integer) As String
        Dim sMRP As String = "", sSql As String = ""
        Try
            'If iCategoryID > 0 Then
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    End If
            'Else
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select INVH_Retail From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select INVH_MRP From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    End If
            'End If

            sSql = "Select SDD_Rate From Sales_Dispatch_Details Where SDD_CommodityID=" & iCommodityID & " And SDD_DescID=" & iItemID & " And SDD_HistoryID in (" & sHistoryID & ") And SDD_MasterID in(Select SDM_ID From Sales_Dispatch_Master Where SDM_OrderNo=" & iorderNo & " And SDM_ID=" & iInvoiceID & " And SDM_CompID =" & iCompID & " And SDM_YearID=" & iYearID & ") "
            sMRP = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sMRP
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
    Public Function GetINVHID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iCategoryID As Integer, ByVal sPartyCode As String, ByVal iCommodityID As Integer, ByVal iorderID As Integer, ByVal iInvoiceID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String = ""
        Dim iINVHID As Integer
        Try
            'If iCategoryID > 0 Then
            '    If sPartyCode = "P" Then
            '        sSql = "Select INVH_ID From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID=" & iItemID & " "
            '    ElseIf sPartyCode = "C" Then
            '        sSql = "Select INVH_ID From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID=" & iItemID & " "
            '    End If
            'Else
            '    If sPartyCode = "P" Then
            '        sSql = "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " "
            '    ElseIf sPartyCode = "C" Then
            '        sSql = "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID=" & iItemID & " "
            '    End If
            'End If
            sSql = "Select SDD_HistoryID From Sales_Dispatch_Details Where SDD_DescID=" & iItemID & " And SDD_CommodityID=" & iCommodityID & " And SDD_MasterID in (Select SDM_ID from Sales_Dispatch_Master Where SDM_OrderID=" & iorderID & " And SDM_ID=" & iInvoiceID & " And SDM_YearID=" & iYearID & " And SDD_CompID=" & iCompID & ") "
            iINVHID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iINVHID
        Catch ex As Exception
            Throw
        End Try
    End Function
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
    Public Function GetExciseOFThisRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sExcise As String) As Integer
        Dim sSql As String = ""
        Dim iCSTID As Integer
        Try
            sSql = "Select Mas_ID From Acc_General_Master Where Mas_Master=16 And MAs_Desc='" & sExcise & "'"
            iCSTID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iCSTID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATOFThisRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sVAT As String) As Integer
        Dim sSql As String = ""
        Dim iVATID As Integer
        Try
            sSql = "Select Mas_ID From Acc_General_Master Where Mas_Master=14 And MAs_Desc='" & sVAT & "' "
            iVATID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iVATID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTOFThisRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sCST As String) As Integer
        Dim sSql As String = ""
        Dim iCSTID As Integer
        Try
            sSql = "Select Mas_ID From Acc_General_Master Where Mas_Master=15 And MAs_Desc='" & sCST & "' "
            iCSTID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iCSTID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindMRP(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sHistoryID As String, ByVal sCode As String, ByVal iCategoryID As Integer, ByVal iItemID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'If iCategoryID > 0 Then
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    End If
            'Else
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    End If
            'End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCommodityID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iInvoiceID As Integer, ByVal iItemID As Integer) As Integer
        Dim sSql As String = ""
        Dim iCommodity As Integer
        Try
            iCommodity = objDBL.SQLExecuteScalarInt(sNameSpace, "Select SDD_CommodityID From Sales_Dispatch_Details,Sales_Dispatch_Master Where SDD_MasterID=SDM_ID And SDM_ID=" & iInvoiceID & " And SDM_OrderID=" & iOrderNo & " And SDD_DescID=" & iItemID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " ")
            Return iCommodity
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As String
        Dim sSql As String = ""
        Dim sCode As String = ""
        Try
            sCode = objDBL.SQLGetDescription(sNameSpace, "Select BM_Code From Sales_Buyers_Masters Where BM_ID=" & iParty & " And BM_CompID=" & iCompID & " ")
            'sCode = objDBL.SQLGetDescription(sNameSpace, "Select ACM_Code From Acc_Customer_Master Where ACM_ID=" & iParty & " And ACM_CompID=" & iCompID & "  ")
            Return sCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Invoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select SPO_ID,SPO_OrderCode from Sales_Proforma_Order where Spo_id in(select SDM_OrderID from Sales_Dispatch_Master "
            sSql = sSql & "where SDM_YearID =" & iYearID & " And SDM_CompID =" & iCompID & ") And SPO_YearID = " & iYearID & " and SPO_CompID =" & iCompID & " order by SPO_ID Desc"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select SDM_ID,SDM_Code from Sales_Dispatch_Master where SDM_OrderID=" & iOrderID & " And SDM_YearID =" & iYearID & " And SDM_CompID =" & iCompID & " order by SDM_ID Desc "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadItems(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvoiceID As Integer, Optional ByVal iCommodityID As Integer = 0) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            If iCommodityID > 0 Then
                sSql = "Select INV_ID,INV_Code From Inventory_Master,Sales_Dispatch_Details Where INV_ID=SDD_DescID And INV_Parent=SDD_CommodityID And SDD_MasterID=" & iInvoiceID & " And SDD_CommodityID=" & iCommodityID & " And SDD_CompID=" & iCompID & " "
            Else
                sSql = "Select INV_ID,INV_Code From Inventory_Master,Sales_Dispatch_Details Where INV_ID=SDD_DescID And INV_Parent=SDD_CommodityID And SDD_MasterID=" & iInvoiceID & " And SDD_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVATAndCSTFromGeneralMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sStr = "VAT" Then
                sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=14 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            Else
                sSql = "select Mas_Id,Mas_Desc from acc_General_Master where Mas_Master=15 and Mas_Delflag ='A' and Mas_CompID =" & iCompID & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExciseFromGeneralMaster(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
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
    Public Function LoadDiscounts(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
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
    Public Function LoadGroups(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description from Inventory_Master where Inv_Parent=0 and Inv_Flag='X' and Inv_CompID=" & iCompID & " order by Inv_Description"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
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
    Public Function BindPaymentType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc From acc_general_master Where Mas_Master In (Select Mas_ID From acc_master_Type Where Mas_Type='Payment Type') And Mas_CompID=" & iCompID & " and Mas_Status='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select BM_ID,BM_Name From Sales_Buyers_Masters Where BM_CompID=" & iCompID & " and BM_DelFlag='A' order by BM_Name "
            'sSql = "Select ACM_ID,ACM_Name From Acc_Customer_Master Where ACM_CompID=" & iCompID & " and ACM_Status='A' order by ACM_Name "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
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
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 SRM_ID From Sales_Return_Master Order By SRM_ID Desc")
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
            sStr = "SR" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDispatchMasterData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim bCheck As Boolean
        Dim dt As New DataTable
        Try
            If iDispatchID > 0 Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderNo & " And SDM_CompID=" & iCompID & " And SDM_YearID =" & iYearID & "")
                If bCheck = True Then
                    sSql = "Select * From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderNo & " And SDM_CompID=" & iCompID & " And SDM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    Return dt
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartyCategory(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_Category From Sales_ProForma_Order Where SPO_ID=" & iOrderNo & " And SPO_CompID=" & iCompID & " And SPO_YearID=" & iYearID & " "
            GetPartyCategory = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPartyCategory
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindCategory(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc From acc_general_master Where Mas_Master In (Select Mas_ID From acc_master_Type Where Mas_Type='Type Of Retailer')  And Mas_CompID=" & iCompID & " and Mas_Status='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetStockHistoryID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iInvoiceID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As String
        Dim sSql As String = ""
        Dim dtHistory As New DataTable
        Dim sINVHID As String = ""
        Try
            sSql = "Select Distinct(SDD_HistoryID) As SDD_HistoryID From Sales_Dispatch_Details,Sales_Dispatch_Master Where SDD_masterID=SDM_ID And SDM_OrderID=" & iOrderNo & " And SDM_ID=" & iInvoiceID & " And SDM_YearID=" & iYearID & " And SDD_CommodityID=" & iCommodityID & " And SDD_DescID=" & iItemID & " "
            dtHistory = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dtHistory.Rows.Count > 0 Then
                For i = 0 To dtHistory.Rows.Count - 1
                    sINVHID = sINVHID & "," & dtHistory.Rows(i)("SDD_HistoryID")
                Next
            End If

            If sINVHID.StartsWith(",") Then
                sINVHID = sINVHID.Remove(0, 1)
            End If
            If sINVHID.EndsWith(",") Then
                sINVHID = sINVHID.Remove(Len(sINVHID) - 1, 1)
            End If
            Return sINVHID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer, ByVal iInvoiceID As Integer, ByVal iCommodityID As Integer, ByVal iItemID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'If iCategoryID > 0 Then
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_CategoryID=" & iCategoryID & " And INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    End If
            'Else
            '    If sCode.StartsWith("P") Then
            '        sSql = "Select INVH_ID,INVH_Retail From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    ElseIf sCode.StartsWith("C") Then
            '        sSql = "Select INVH_ID,INVH_MRP From Inventory_Master_History Where INVH_INV_ID in (" & iItemID & ") and INVH_CompID =" & iCompID & ""
            '    End If
            'End If
            sSql = "Select * From Sales_Dispatch_Details,Sales_Dispatch_Master Where SDD_masterID=SDM_ID And SDM_OrderID=" & iOrderNo & " And SDM_ID=" & iInvoiceID & " And SDM_YearID=" & iYearID & " And SDD_CommodityID=" & iCommodityID & " And SDD_DescID=" & iItemID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSalesReturnMaster(ByVal sNameSpace As String, ByVal objSR As ClsSalesReturn) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(27) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_ReturnOrderCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSR.sSRM_ReturnOrderCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_OrderNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_OrderNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_OrderDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSR.dSRM_OrderDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_ReferenceNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSR.sSRM_ReferenceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_ReturnDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSR.dSRM_ReturnDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_PartyID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_PartyID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_PartyCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSR.sSRM_PartyCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_ModeOfReturn", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_ModeOfReturn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_Narration", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objSR.sSRM_Narration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objSR.sSRM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_CompID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objSR.iSRM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSR.dSRM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objSR.SRM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSR.SRM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_DispatchID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_DispatchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_DispatchRefNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objSR.sSRM_DispatchRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_ESugamNo", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objSR.sSRM_ESugamNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_Category", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_Category
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_SaleType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_SaleType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_OtherType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_OtherType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_DispatchDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objSR.dSRM_DispatchDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRM_ReturnReason", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRM_ReturnReason
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSales_Return_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSalesReturnDetails(ByVal sNameSpace As String, ByVal objSR As ClsSalesReturn) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_ID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_CommodityID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_CommodityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_DescriptionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_DescriptionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_HistoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_HistoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_SaleQnty", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_SaleQnty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_ReturnQnty", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_ReturnQnty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Rate", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSR.dSRD_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_UnitOfMeasurement", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_UnitOfMeasurement
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Discount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSR.SRD_Discount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_DiscountAmount", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSR.SRD_DiscountAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_VAT", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSR.SRD_VAT
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_VATAmount", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSR.SRD_VATAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_CST", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSR.SRD_CST
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_CSTAmount", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSR.SRD_CSTAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Excise", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSR.SRD_Excise
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_ExciseAmount", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objSR.SRD_ExciseAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_TotalAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSR.dSRD_TotalAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_CompID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objSR.iSRD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Return", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objSR.iSRD_Return
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_RateAmount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSR.SRD_RateAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objSR.SRD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objSR.SRD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSR.SRD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_EnteredPrice", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSR.SRD_EnteredPrice
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SRD_DifferencePrice", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objSR.SRD_DifferencePrice
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSales_Return_Details", 1, Arr, ObjParam)
            Return Arr
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
            dtTab.Columns.Add("CommodityID")
            dtTab.Columns.Add("DescID")
            dtTab.Columns.Add("HistoryID")
            dtTab.Columns.Add("UnitID")
            dtTab.Columns.Add("SlNo")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("UnitOfMeassurement")
            'dtTab.Columns.Add("SalesQty")
            dtTab.Columns.Add("ReturnQty")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("Amount")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("VAT")
            dtTab.Columns.Add("VATAmount")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("CSTAmount")
            dtTab.Columns.Add("Excise")
            dtTab.Columns.Add("ExciseAmount")
            dtTab.Columns.Add("NetAmount")

            If iMasterID > 0 Then
                sSql = "Select * From Sales_Return_Details Where SRD_MasterID=" & iMasterID & " And SRD_CompiD=" & iCompID & " Order By SRD_ID "
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("SRD_CommodityID")
                    dRow("DescID") = dt.Rows(i)("SRD_DescriptionID")
                    dRow("HistoryID") = dt.Rows(i)("SRD_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("SRD_UnitOfMeasurement")
                    dRow("SlNo") = i + 1
                    dRow("Goods") = objDBL.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & dt.Rows(i)("SRD_DescriptionID") & " And INV_Parent=" & dt.Rows(i)("SRD_CommodityID") & " and Inv_CompID = " & iCompID & "")
                    dRow("UnitOfMeassurement") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("SRD_UnitOfMeasurement") & " and Mas_CompID =" & iCompID & "")

                    'dRow("SalesQty") = ""
                    dRow("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SRD_Rate")))
                    dRow("Amount") = dt.Rows(i)("SRD_RateAmount")

                    dRow("ReturnQty") = dt.Rows(i)("SRD_ReturnQnty")

                    If IsDBNull(dt.Rows(i)("SRD_Discount")) = False Then
                        dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SRD_Discount")))
                        dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SRD_DiscountAmount")))
                    Else
                        dRow("Discount") = 0
                        dRow("DiscountAmount") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("SRD_VAT")) = False Then
                        dRow("VAT") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_Master=14 And Mas_ID=" & dt.Rows(i)("SRD_VAT") & " ")
                        dRow("VATAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SRD_VATAmount")))
                    Else
                        dRow("VAT") = 0
                        dRow("VATAmount") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("SRD_CST")) = False Then
                        dRow("CST") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_Master=15 And Mas_ID=" & dt.Rows(i)("SRD_CST") & " ")
                        dRow("CSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SRD_CSTAmount")))
                    Else
                        dRow("CST") = 0
                        dRow("CSTAmount") = 0
                    End If

                    If IsDBNull(dt.Rows(i)("SRD_Excise")) = False Then
                        dRow("Excise") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_Master=16 And Mas_ID=" & dt.Rows(i)("SRD_Excise") & " ")
                        dRow("ExciseAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SRD_ExciseAmount")))
                    Else
                        dRow("Excise") = 0
                        dRow("ExciseAmount") = 0
                    End If

                    dRow("NetAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("SRD_TotalAmount")))

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSearch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim bCheckO As Boolean
        Try
            If sSearch <> "" Then
                bCheckO = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Sales_Return_Master where SRM_ReturnOrderCode ='" & sSearch & "' And SRM_CompID=" & iCompID & " and SRM_YearID =" & iYearID & "")
                If bCheckO = True Then
                    sSql = "Select SRM_ID,SRM_ReturnOrderCode From Sales_Return_Master where SRM_ReturnOrderCode ='" & sSearch & "' And SRM_CompID=" & iCompID & " and SRM_YearID =" & iYearID & ""
                    dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                End If
            Else
                sSql = "Select SRM_ID,SRM_ReturnOrderCode From Sales_Return_Master where SRM_CompID=" & iCompID & " and SRM_YearID = " & iYearID & " Order By SRM_ID Desc"
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Sales_Return_Master Where SRM_ID=" & iOrderID & " And SRM_CompID=" & iCompID & " and SRM_YearID = " & iYearID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AcceptMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iAllocationID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Update Sales_Return_Master Set SRM_Status='A',SRM_ApprovedBy=" & iUserID & ",SRM_ApprovedOn=GetDate(),SRM_Operation='A',SRM_IPAddress='" & iIPAddress & "' Where SRM_ID=" & iAllocationID & " And SRM_CompID=" & iCompID & " and SRM_YearID =" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function EffectToStockMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIPAddress As String, ByVal iOrderNo As Integer, ByVal iAllocationID As Integer, ByVal sRemarks As String, ByVal iCommodityID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer, ByVal dReturnQty As Double) As Integer
        Dim sSql As String = ""
        Dim iClosingBalQty As Double
        Dim iTotalQty As Double
        Dim dt As New DataTable
        Dim iSLID As Integer
        Try
            sSql = "" : sSql = "Select Top 1 * From Stock_Ledger Where SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_HistoryID=" & iHistoryID & " And SL_CompID=" & iCompID & "  Order By SL_OrderID Desc "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                iClosingBalQty = dt.Rows(0)("SL_ClosingBalanceQty")
                iSLID = dt.Rows(0)("SL_ID")

                If iClosingBalQty < 0 Then
                    iTotalQty = iClosingBalQty - dReturnQty
                Else
                    iTotalQty = iClosingBalQty + dReturnQty
                End If

                sSql = "" : sSql = "Update Stock_Ledger Set SL_ClosingBalanceQty=" & iTotalQty & " Where SL_ID=" & iSLID & " And SL_Commodity=" & iCommodityID & " And SL_ItemID=" & iItemID & " And SL_HistoryID=" & iHistoryID & " And SL_CompID=" & iCompID & "  "
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
