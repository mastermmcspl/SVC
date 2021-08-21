Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class clsPurchaseReturn
    Private PRM_ID As Integer
    Private PRM_OrderDate As Date
    Private PRM_ReturnNo As String
    Private PRM_InwardNo As String
    Private PRM_Remarks As String
    Private PRM_Status As String
    Private PRM_Supplier As Integer
    Private PRM_DSchdule As Integer
    Private PRM_ModeOfShipping As Integer
    Private PRM_MethodofPayment As Integer
    Private PRM_Paymentterms As Integer
    Private PRM_CreatedBy As Integer
    Private PRM_ApporvedBy As Integer
    Private PRM_YearID As Integer
    Private PRD_ID As Integer
    Private PRD_MasterID As Integer
    Private PRD_Commodity As Integer
    Private PRD_DescriptionID As Integer
    Private PRD_HistoryID As Integer
    Private PRD_Unit As Integer
    Private PRD_Rate As String
    Private PRM_CSTCtgry As Integer
    Private PRM_SaleType As Integer
    Private PRD_Quantity As String
    Private PRD_RateAmount As String
    Private PRD_Discount As String
    Private PRD_DiscountAmount As String
    Private PRD_Excise As String
    Private PRD_ExciseAmount As String
    Private PRD_Frieght As String
    Private PRD_FrieghtAmount As String
    Private PRD_VAT As String
    Private PRD_VATAmount As String
    Private PRD_CST As String
    Private PRD_CSTAmount As String
    Private PRD_RequiredDate As Date
    Private PRD_TotalAmount As String
    Private PRD_CompID As Integer
    Private PRM_DocRef As String
    Private PRD_Rejected As Integer
    Private PRD_Accepted As Integer
    Private PRD_ReceivedQty As Integer
    Private PRD_OrderedQty As Integer
    Private PRD_IPAddress As String
    Private BatchNumber As String
    Private PRM_DEliveryChlnNo As String
    Private PRM_TypeOfReturn As Integer
    Public Property sPRM_DEliveryChlnNo() As String
        Get
            Return (PRM_DEliveryChlnNo)
        End Get
        Set(ByVal Value As String)
            PRM_DEliveryChlnNo = Value
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
    Public Property iPRD_Accepted() As Integer
        Get
            Return (PRD_Accepted)
        End Get
        Set(ByVal Value As Integer)
            PRD_Accepted = Value
        End Set
    End Property
    Public Property iPRD_Rejected() As Integer
        Get
            Return (PRD_Rejected)
        End Get
        Set(ByVal Value As Integer)
            PRD_Rejected = Value
        End Set
    End Property
    Public Property sPRM_DocRef() As String
        Get
            Return (PRM_DocRef)
        End Get
        Set(ByVal Value As String)
            PRM_DocRef = Value
        End Set
    End Property
    Public Property sPRD_RateAmount() As String
        Get
            Return (PRD_RateAmount)
        End Get
        Set(ByVal Value As String)
            PRD_RateAmount = Value
        End Set
    End Property
    Public Property sPRD_TotalAmount() As String
        Get
            Return (PRD_TotalAmount)
        End Get
        Set(ByVal Value As String)
            PRD_TotalAmount = Value
        End Set
    End Property
    Public Property dPRD_RequiredDate() As DateTime
        Get
            Return (PRD_RequiredDate)
        End Get
        Set(ByVal Value As DateTime)
            PRD_RequiredDate = Value
        End Set
    End Property
    Public Property sPRD_CSTAmount() As String
        Get
            Return (PRD_CSTAmount)
        End Get
        Set(ByVal Value As String)
            PRD_CSTAmount = Value
        End Set
    End Property
    Public Property sPRD_CST() As String
        Get
            Return (PRD_CST)
        End Get
        Set(ByVal Value As String)
            PRD_CST = Value
        End Set
    End Property
    Public Property sPRD_VATAmount() As String
        Get
            Return (PRD_VATAmount)
        End Get
        Set(ByVal Value As String)
            PRD_VATAmount = Value
        End Set
    End Property
    Public Property sPRD_VAT() As String
        Get
            Return (PRD_VAT)
        End Get
        Set(ByVal Value As String)
            PRD_VAT = Value
        End Set
    End Property
    Public Property sPRD_ExciseAmount() As String
        Get
            Return (PRD_ExciseAmount)
        End Get
        Set(ByVal Value As String)
            PRD_ExciseAmount = Value
        End Set
    End Property
    Public Property sPRD_Excise() As String
        Get
            Return (PRD_Excise)
        End Get
        Set(ByVal Value As String)
            PRD_Excise = Value
        End Set
    End Property

    Public Property sPRD_Frieght() As String
        Get
            Return (PRD_Frieght)
        End Get
        Set(ByVal Value As String)
            PRD_Frieght = Value
        End Set
    End Property

    Public Property sPRD_FrieghtAmount() As String
        Get
            Return (PRD_FrieghtAmount)
        End Get
        Set(ByVal Value As String)
            PRD_FrieghtAmount = Value
        End Set
    End Property


    Public Property iPRD_ReceivedQty() As Integer
        Get
            Return (PRD_ReceivedQty)
        End Get
        Set(ByVal Value As Integer)
            PRD_ReceivedQty = Value
        End Set
    End Property

    Public Property iPRD_OrderedQty() As Integer
        Get
            Return (PRD_OrderedQty)
        End Get
        Set(ByVal Value As Integer)
            PRD_OrderedQty = Value
        End Set
    End Property
    Public Property sPRD_DiscountAmount() As String
        Get
            Return (PRD_DiscountAmount)
        End Get
        Set(ByVal Value As String)
            PRD_DiscountAmount = Value
        End Set
    End Property

    Public Property sPRD_Discount() As String
        Get
            Return (PRD_Discount)
        End Get
        Set(ByVal Value As String)
            PRD_Discount = Value
        End Set
    End Property
    Public Property sPRD_Quantity() As String
        Get
            Return (PRD_Quantity)
        End Get
        Set(ByVal Value As String)
            PRD_Quantity = Value
        End Set
    End Property

    Public Property sPRD_Rate() As String
        Get
            Return (PRD_Rate)
        End Get
        Set(ByVal Value As String)
            PRD_Rate = Value
        End Set
    End Property
    Public Property iPRD_Unit() As Integer
        Get
            Return (PRD_Unit)
        End Get
        Set(ByVal Value As Integer)
            PRD_Unit = Value
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
    Public Property iPRD_DescriptionID() As Integer
        Get
            Return (PRD_DescriptionID)
        End Get
        Set(ByVal Value As Integer)
            PRD_DescriptionID = Value
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
    Public Property iPRD_MasterID() As Integer
        Get
            Return (PRD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            PRD_MasterID = Value
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

    Public Property iPRM_YearID() As Integer
        Get
            Return (PRM_YearID)
        End Get
        Set(ByVal Value As Integer)
            PRM_YearID = Value
        End Set
    End Property

    Public Property iPRM_SaleType() As Integer
        Get
            Return (PRM_SaleType)
        End Get
        Set(ByVal Value As Integer)
            PRM_SaleType = Value
        End Set
    End Property

    Public Property iPRM_iCSTCtgry() As Integer
        Get
            Return (PRM_CSTCtgry)
        End Get
        Set(ByVal Value As Integer)
            PRM_CSTCtgry = Value
        End Set
    End Property
    Public Property iPRM_ApporvedBy() As Integer
        Get
            Return (PRM_ApporvedBy)
        End Get
        Set(ByVal Value As Integer)
            PRM_ApporvedBy = Value
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
    Public Property iPRM_MethodofPayment() As Integer
        Get
            Return (PRM_MethodofPayment)
        End Get
        Set(ByVal Value As Integer)
            PRM_MethodofPayment = Value
        End Set
    End Property

    Public Property iPRM_DSchdule() As Integer
        Get
            Return (PRM_DSchdule)
        End Get
        Set(ByVal Value As Integer)
            PRM_DSchdule = Value
        End Set
    End Property
    Public Property iPRM_Paymentterms() As Integer
        Get
            Return (PRM_Paymentterms)
        End Get
        Set(ByVal Value As Integer)
            PRM_Paymentterms = Value
        End Set
    End Property


    Public Property iPRM_ModeOfShipping() As Integer
        Get
            Return (PRM_ModeOfShipping)
        End Get
        Set(ByVal Value As Integer)
            PRM_ModeOfShipping = Value
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

    Public Property sBatchNumber() As String
        Get
            Return (BatchNumber)
        End Get
        Set(ByVal Value As String)
            BatchNumber = Value
        End Set
    End Property
    Public Property sPRM_ReturnNo() As String
        Get
            Return (PRM_ReturnNo)
        End Get
        Set(ByVal Value As String)
            PRM_ReturnNo = Value
        End Set
    End Property

    Public Property sPRM_Status() As String
        Get
            Return (PRM_Status)
        End Get
        Set(ByVal Value As String)
            PRM_Status = Value
        End Set
    End Property

    Public Property sPRM_InwardNo() As String
        Get
            Return (PRM_InwardNo)
        End Get
        Set(ByVal Value As String)
            PRM_InwardNo = Value
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
    Public Property iPRM_ID() As Integer
        Get
            Return (PRM_ID)
        End Get
        Set(ByVal Value As Integer)
            PRM_ID = Value
        End Set
    End Property
    Public Property iPRM_TypeOfReturn() As Integer
        Get
            Return (PRM_TypeOfReturn)
        End Get
        Set(ByVal Value As Integer)
            PRM_TypeOfReturn = Value
        End Set
    End Property

    Public Property sPRM_Remarks() As String
        Get
            Return (PRM_Remarks)
        End Get
        Set(ByVal Value As String)
            PRM_Remarks = Value
        End Set
    End Property
    Private objDb As New DBHelper
    Private objFasgnrl As New clsFASGeneral
    Private objFasAllgnrl As New clsGeneralFunctions
    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " and CSM_Delflag='A' order by CSM_Name"
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadModeOfReturn(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Mas_id,Mas_desc from acc_general_master where Mas_CompID=" & iCompID & " and Mas_master=13 and Mas_Status='A'"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master Where POM_ID In (SElect PV_OrderNo From Purchase_Verification Where PV_YearID=" & iYearID & " And PV_CompID=" & iCompID & ")"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSalesOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_ID,SPO_OrderCode from Sales_Proforma_Order"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadExistingOrder(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select PRM_ID,PRM_ReturnNo from Purchase_Return_Master where PRM_CompID=" & iCompID & " And PRM_YearID =" & iYearID & " And PRM_Status<>'D' order by PRM_ID desc"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInvoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select PGM_ID,PGM_GIN_Number from Purchase_GIN_Master where PGM_YearID=" & iYearID & " And PGM_CompID=" & iCompID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceNum As Integer, ByVal orderNo As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_ID in(select PGD_DescriptionID from Purchase_GIN_details where PGD_MasterID=" & InvoiceNum & " and PGD_OrderID=" & orderNo & " ) and Inv_CompID =" & iCompID & " "

            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSalesDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceNum As Integer, ByVal orderNo As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_ID in(select SDD_DescID from Sales_Dispatch_Details where SDD_MasterID=" & InvoiceNum & " and SDD_MasterID in(select SDM_OrderID from Sales_Dispatch_Master where SDM_OrderID=" & orderNo & ") ) and Inv_CompID =" & iCompID & " "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getInvoiceDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceNum As String, ByVal orderNo As Integer, ByVal YearID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select PGM_InvoiceDate from  Purchase_GIN_Master where PGM_DocumentRefNo ='" & InvoiceNum & "' and PGM_YearID =" & YearID & " and PGM_CompID =" & iCompID & ""
            Return objDb.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getSalesDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceNum As String, ByVal orderNo As Integer, ByVal YearID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select SDM_DispatchDate from  Sales_Dispatch_Master where SDM_DispatchRefNo ='" & InvoiceNum & "' and SDM_YearID =" & YearID & " and SDM_CompID =" & iCompID & " and SDM_OrderID= " & orderNo & ""
            Return objDb.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function



    'txtInvoiceDate.Text = DBHelper.GetDescription(sSession.AccessCode, "Select PGM_InvoiceDate from  Purchase_GIN_Master where PGM_DocumentRefNo ='" & ddlInvoiceNo.SelectedItem.Text & "' and PGM_YearID =" & sSession.YearID & " and PGM_CompID =" & sSession.AccessCodeID & "")

    'Public Function getInvoiceDate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceRef As String, ByVal orderNo As Integer, ByVal iYearID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Try
    '        sSql = "select PGM_InvoiceDate from  Purchase_GIN_Master where PGM_DocumentRefNo ='" & InvoiceRef & "' and PGM_YearID =" & iYearID & " and PGM_CompID =" & iCompID & ""
    '        Return objDb.SQLGetDescription(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadDescriptionFromComodity(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal InvoiceNum As Integer, ByVal orderNo As Integer, ByVal comodityID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID, Inv_Code from inventory_master where Inv_ID in(select PGD_DescriptionID from Purchase_GIN_details where PGD_MasterID=" & InvoiceNum & " and PGD_OrderID=" & orderNo & " and PGD_CommodityID=" & comodityID & "  ) and Inv_CompID =" & iCompID & " "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePurchaseReturn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dOrderDate As Date, ByVal objPO As clsPurchaseReturn) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Purchase_Return_Master where PRM_ReturnNo = '" & objPO.sPRM_ReturnNo & "' and PRM_CompID =" & iCompID & " and PRM_YearID =" & objPO.iPRM_YearID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                sSql = "" : sSql = "Update Purchase_Return_Master set PRM_Supplier = " & objPO.iPRM_Supplier & ",PRM_MPayment=" & objPO.iPRM_MethodofPayment & ",PRM_PaymentTerms=" & objPO.iPRM_Paymentterms & ",PRM_ModeOfShipping = " & objPO.iPRM_ModeOfShipping & ",PRM_DSchdule=" & objPO.iPRM_DSchdule & " "
                sSql = sSql & "Where PRM_ReturnNo = '" & objPO.sPRM_ReturnNo & "' and PRM_CompID =" & iCompID & " and PRM_YearID=" & objPO.iPRM_YearID & " and PRM_Remarks='" & objPO.sPRM_Remarks & "'"
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dt.Rows(0)("PRM_ID")
            Else
                iMax = objFasAllgnrl.GetMaxID(sNameSpace, iCompID, "Purchase_Return_Master", "PRM_ID", "PRM_CompID")
                sSql = "" : sSql = "Insert into Purchase_Return_Master(PRM_ID,PRM_OrderDate,PRM_ReturnNo,PrM_Supplier,"
                sSql = sSql & "PRM_ModeOfShipping,PRM_Status,PRM_CreatedBy,PrM_CreatedOn,"
                sSql = sSql & "PRM_YearID,PrM_CompID,PRM_MPayment,PrM_PaymentTerms,PRM_DSchdule,PRM_TypeOfPurchase,PRM_CstCategory,PRM_Remarks)Values(" & iMax & "," & objFasgnrl.FormatDtForRDBMS(dOrderDate, "I") & ",'" & objPO.sPRM_ReturnNo & "'," & objPO.iPRM_Supplier & ","
                sSql = sSql & "" & objPO.iPRM_ModeOfShipping & ",'" & objPO.sPRM_Status & "'," & objPO.iPRM_CreatedBy & ",GetDate(),"
                sSql = sSql & "" & objPO.iPRM_YearID & "," & iCompID & "," & objPO.iPRM_MethodofPayment & "," & objPO.iPRM_Paymentterms & "," & objPO.iPRM_DSchdule & "," & objPO.iPRM_SaleType & "," & objPO.iPRM_iCSTCtgry & ",'" & objPO.sPRM_Remarks & "')"
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Return iMax
            End If
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
            dt.Columns.Add("PRM_ReturnNo")
            dt.Columns.Add("PRM_OrderDate")
            dt.Columns.Add("PRM_Supplier")
            dt.Columns.Add("PRD_Commodity")
            dt.Columns.Add("PRD_Rate")
            dt.Columns.Add("PRD_Quantity")
            dt.Columns.Add("PRD_CST")
            dt.Columns.Add("PRD_VAT")
            dt.Columns.Add("PRD_CSTAmount")
            dt.Columns.Add("PRD_RateAmount")
            dt.Columns.Add("PRD_Discount")
            dt.Columns.Add("PRD_DiscountAmount")
            dt.Columns.Add("PRD_TotalAmount")
            dt.Columns.Add("PRD_VATAmount")
            dt.Columns.Add("PRD_Unit")
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
            dt.Columns.Add("PRD_Excise")
            dt.Columns.Add("PRD_ExciseAmount")
            dt.Columns.Add("PRD_Frieght")
            dt.Columns.Add("PRD_FrieghtAmount")
            dt.Columns.Add("PRM_Remarks")
            sSql = "Select PRM_ReturnNo, Convert(VARCHAR(10), PRM_OrderDate, 103)As PRM_OrderDate, PRM_Supplier, b.PRD_Commodity, b.PRD_Rate, b.PRD_Quantity,PRM_Remarks, "
            sSql = sSql & "b.PRD_CST, Convert(money, b.PRD_CSTAmount) As PRD_CSTAmount, Convert(money, b.PRD_RateAmount) As PRD_RateAmount,"
            sSql = sSql & "Convert(money, b.PRD_Discount)As PRD_Discount,Convert(money,b.PRD_DiscountAmount) As PRD_DiscountAmount,"
            sSql = sSql & "Convert(money,b.PRD_TotalAmount)As PRD_TotalAmount,"
            sSql = sSql & "Convert(money,b.PRD_VAT)As PRD_VAT,Convert(money,b.PRD_Excise)As PRD_Excise,PRD_Frieght,PRD_FrieghtAmount,PRD_ExciseAmount,Convert(money,b.PRD_ExciseAmount)As PRD_ExciseAmount,Convert(money,b.PRD_VATAmount)As PRD_VATAmount,b.PRD_Unit, "
            sSql = sSql & "c.INV_Code,c.INV_Description,d.CSM_Name,d.CSM_Address,d.CSM_MobileNo,d.CSM_EmailID,e.Mas_Desc,m.CUST_CODE,m.CUST_Name,m.CUST_COMM_ADDRESS,m.CUST_EMAIL,m.CUST_COMM_TEL,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate "
            sSql = sSql & "From Purchase_Return_Master "
            sSql = sSql & "join Purchase_Return_Details b On PRM_ID=" & iPomID & " And PRM_ID=b.PRD_MasterID And b.PRD_Status <> 'D' "
            sSql = sSql & "Join Inventory_master_history InvH on  PRD_HistoryID=InvH.InvH_ID "
            sSql = sSql & "Join Inventory_master c on  PRD_DescriptionID=c.INV_ID "
            sSql = sSql & "Join CustomerSupplierMaster d On PRM_Supplier=d.CSM_ID "
            sSql = sSql & "Join Acc_General_master e on b.PRD_Unit=e.Mas_ID "
            sSql = sSql & "Join MST_CUSTOMER_MASTER m on b.PRD_CompID=m.CUST_ID "
            dtFRomTable = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dtFRomTable.Rows.Count > 0 Then
                For i = 0 To dtFRomTable.Rows.Count - 1
                    dRow = dt.NewRow()
                    If IsDBNull(dtFRomTable.Rows(i)("PRM_ReturnNo")) = False Then
                        dRow("PRM_ReturnNo") = dtFRomTable.Rows(i)("PRM_ReturnNo")
                    Else
                        dRow("PRM_ReturnNo") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRM_OrderDate")) = False Then
                        dRow("PRM_OrderDate") = dtFRomTable.Rows(i)("PRM_OrderDate")
                    Else
                        dRow("PRM_OrderDate") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_Commodity")) = False Then
                        dRow("PRD_Commodity") = dtFRomTable.Rows(i)("PRD_Commodity")
                    Else
                        dRow("PRD_Commodity") = ""
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_Rate")) = False Then
                        dRow("PRD_Rate") = dtFRomTable.Rows(i)("PRD_Rate")
                    Else
                        dRow("PRD_Rate") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_Quantity")) = False Then
                        dRow("PRD_Quantity") = dtFRomTable.Rows(i)("PRD_Quantity")
                    Else
                        dRow("PRD_Quantity") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_CST")) = False Then
                        dRow("PRD_CST") = dtFRomTable.Rows(i)("PRD_CST")
                    Else
                        dRow("PRD_CST") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_VAT")) = False Then
                        dRow("PRD_VAT") = dtFRomTable.Rows(i)("PRD_VAT")
                    Else
                        dRow("PRD_VAT") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_CSTAmount")) = False Then
                        dRow("PRD_CSTAmount") = Convert.ToDouble(dtFRomTable.Rows(i)("PRD_CSTAmount"))
                    Else
                        dRow("PRD_CSTAmount") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("PRD_RateAmount")) = False Then
                        dRow("PRD_RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dtFRomTable.Rows(i)("PRD_RateAmount") - dtFRomTable.Rows(i)("PRD_DiscountAmount"))))
                        subtotal = subtotal + dRow("PRD_RateAmount")
                    Else
                        dRow("PRD_RateAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_Discount")) = False Then
                        dRow("PRD_Discount") = dtFRomTable.Rows(i)("PRD_Discount")
                    Else
                        dRow("PRD_Discount") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("PRD_DiscountAmount")) = False Then
                        dRow("PRD_DiscountAmount") = dtFRomTable.Rows(i)("PRD_DiscountAmount")
                    Else
                        dRow("PRD_DiscountAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_TotalAmount")) = False Then
                        dRow("PRD_TotalAmount") = dtFRomTable.Rows(i)("PRD_TotalAmount")
                    Else
                        dRow("PRD_TotalAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_VATAmount")) = False Then
                        dRow("PRD_VATAmount") = dtFRomTable.Rows(i)("PRD_VATAmount")
                    Else
                        dRow("PRD_VATAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_Unit")) = False Then
                        dRow("PRD_Unit") = dtFRomTable.Rows(i)("PRD_Unit")
                    Else
                        dRow("PRD_Unit") = 0
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
                    If IsDBNull(dtFRomTable.Rows(i)("PRD_Excise")) = False Then
                        dRow("PRD_Excise") = dtFRomTable.Rows(i)("PRD_Excise")
                    Else
                        dRow("PRD_Excise") = 0
                    End If
                    If IsDBNull(dtFRomTable.Rows(i)("PRD_ExciseAmount")) = False Then
                        dRow("PRD_ExciseAmount") = dtFRomTable.Rows(i)("PRD_ExciseAmount")
                    Else
                        dRow("PRD_ExciseAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_Frieght")) = False Then
                        dRow("PRD_Frieght") = dtFRomTable.Rows(i)("PRD_Frieght")
                    Else
                        dRow("PRD_Frieght") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRD_FrieghtAmount")) = False Then
                        dRow("PRD_FrieghtAmount") = dtFRomTable.Rows(i)("PRD_FrieghtAmount")
                    Else
                        dRow("PRD_FrieghtAmount") = 0
                    End If

                    If IsDBNull(dtFRomTable.Rows(i)("PRM_Remarks")) = False Then
                        dRow("PRM_Remarks") = dtFRomTable.Rows(i)("PRM_Remarks")
                    Else
                        dRow("PRM_Remarks") = 0
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function SavePurchaseReturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dRequiredDate As Date, ByVal objPO As clsPurchaseReturn)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iMax As Integer = 0
        Try
            sSql = "" : sSql = "Select * from Purchase_Return_Details where PRD_MasterID = " & objPO.iPRD_MasterID & " and PRD_Commodity = " & objPO.iPRD_Commodity & " and "
            sSql = sSql & "PRD_DescriptionID = " & objPO.iPRD_DescriptionID & " and PRD_HistoryID =" & objPO.iPRD_HistoryID & " and PRD_CompID = " & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count = True Then
                sSql = "" : sSql = "Update Purchase_Return_Details set PRD_Unit = " & objPO.iPRD_Unit & ",PRD_Rate='" & objPO.sPRD_Rate & "',PRD_RateAmount = '" & objPO.sPRD_RateAmount & "',PRD_Quantity='" & objPO.sPRD_Quantity & "',"
                sSql = sSql & "PRD_Discount = '" & objPO.sPRD_Discount & "',PRD_DiscountAmount='" & objPO.sPRD_DiscountAmount & "',PRD_Excise='" & objPO.sPRD_Excise & "',"
                sSql = sSql & "PRD_ExciseAmount = '" & objPO.sPRD_ExciseAmount & "',PRD_VAT = '" & objPO.sPRD_VAT & "',PRD_VATAmount='" & objPO.sPRD_VATAmount & "',"
                sSql = sSql & "PRD_CST='" & objPO.sPRD_CST & "',PRD_CSTAmount='" & objPO.sPRD_CSTAmount & "',"
                sSql = sSql & "PRD_TotalAmount='" & objPO.sPRD_TotalAmount & "',PRD_Status='W',PRD_Frieght='" & objPO.sPRD_Frieght & "',PRD_FrieghtAmount='" & objPO.sPRD_FrieghtAmount & "' where PRD_MasterID = " & objPO.iPRD_MasterID & " and "
                sSql = sSql & "PRD_Commodity = " & objPO.iPRD_Commodity & " and PRD_DescriptionID = " & objPO.iPRD_DescriptionID & " and "
                sSql = sSql & "PRD_HistoryID =" & objPO.iPRD_HistoryID & " and PRD_CompID = " & iCompID & ""
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
            Else
                iMax = objFasAllgnrl.GetMaxID(sNameSpace, iCompID, "Purchase_Return_Details", "PRD_ID", "PRD_CompID")
                sSql = "" : sSql = "Insert into Purchase_Return_Details(PRD_ID,PRD_MasterID,PRD_Commodity,"
                sSql = sSql & "PRD_DescriptionID,PRD_HistoryID,PRD_Unit,PRD_Rate,PRD_RateAmount,"
                sSql = sSql & "PRD_Quantity,PRD_Discount,PRD_DiscountAmount,PRD_Excise,"
                sSql = sSql & "PRD_ExciseAmount,PRD_VAT,PRD_VATAmount,PRD_CST,"
                sSql = sSql & "PRD_CSTAmount,PRD_TotalAmount,PRD_CompID,PRD_Status,PRD_Frieght,PRD_FrieghtAmount)"
                sSql = sSql & "Values(" & iMax & "," & objPO.iPRD_MasterID & "," & objPO.iPRD_Commodity & ","
                sSql = sSql & "" & objPO.iPRD_DescriptionID & "," & objPO.iPRD_HistoryID & "," & objPO.iPRD_Unit & ",'" & objPO.sPRD_Rate & "','" & objPO.sPRD_RateAmount & "',"
                sSql = sSql & "'" & objPO.sPRD_Quantity & "','" & objPO.sPRD_Discount & "','" & objPO.PRD_DiscountAmount & "','" & objPO.sPRD_Excise & "',"
                sSql = sSql & "'" & objPO.sPRD_ExciseAmount & "','" & objPO.sPRD_VAT & "','" & objPO.sPRD_VATAmount & "','" & objPO.sPRD_CST & "',"
                sSql = sSql & "'" & objPO.sPRD_CSTAmount & "','" & objPO.sPRD_TotalAmount & "'," & iCompID & ",'W','" & objPO.sPRD_Frieght & "','" & objPO.sPRD_FrieghtAmount & "')"
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPurchasereturnDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
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
            dt.Columns.Add("ExciseDuty")
            dt.Columns.Add("ExciseAmt")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")
            dt.Columns.Add("TotalAmount")
            sSql = "Select * from Purchase_Return_Details where PRD_MasterID =" & iMasterID & " and PRD_CompID=" & iCompID & " and  PRD_Status='W' order by PRD_ID"
            ds = objDb.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("ID") = ds.Tables(0).Rows(i)("PRD_ID")
                    dRow("CommodityID") = ds.Tables(0).Rows(i)("PRD_Commodity")
                    dRow("DescriptionID") = ds.Tables(0).Rows(i)("PRD_DescriptionID")
                    dRow("HistoryID") = ds.Tables(0).Rows(i)("PRD_HistoryID")
                    dRow("UnitsID") = ds.Tables(0).Rows(i)("PRD_Unit")
                    iSlNo = iSlNo + 1
                    dRow("SLNO") = iSlNo
                    dRow("Goods") = objDb.SQLExecuteScalar(sNameSpace, "Select Inv_Code from Inventory_Master where Inv_ID='" & ds.Tables(0).Rows(i)("PRD_DescriptionID") & "' and Inv_compid=" & iCompID & "")
                    dRow("Units") = objDb.SQLExecuteScalar(sNameSpace, "Select Mas_Desc from acc_General_master where Mas_ID='" & ds.Tables(0).Rows(i)("PRD_Unit") & "' and Mas_compid=" & iCompID & "")
                    dRow("Rate") = ds.Tables(0).Rows(i)("PRD_Rate")
                    If IsDBNull(ds.Tables(0).Rows(i)("PrD_Quantity")) = False Then
                        dRow("Quantity") = Math.Round(ds.Tables(0).Rows(i)("PRD_Quantity"), 2)
                    End If
                    dRow("RateAmount") = ds.Tables(0).Rows(i)("PRD_RateAmount")
                    dRow("Discount") = ds.Tables(0).Rows(i)("PRD_Discount")
                    dRow("DiscountAmt") = ds.Tables(0).Rows(i)("PRD_DiscountAmount")
                    dRow("ExciseDuty") = ds.Tables(0).Rows(i)("PRD_Excise")
                    dRow("ExciseAmt") = ds.Tables(0).Rows(i)("PRD_ExciseAmount")
                    dRow("VAT") = ds.Tables(0).Rows(i)("PRD_VAT")
                    dRow("VATAmt") = ds.Tables(0).Rows(i)("PRD_VATAmount")
                    dRow("CST") = ds.Tables(0).Rows(i)("PRD_CST")
                    dRow("CSTAmount") = ds.Tables(0).Rows(i)("PRD_CSTAmount")
                    dRow("TotalAmount") = ds.Tables(0).Rows(i)("PRD_TotalAmount")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetOrderDtae(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderNo As Integer) As Date
        Dim sSql As String = ""
        Try
            sSql = "Select POM_OrderDate From Purchase_Order_Master Where POM_ID=" & iOrderNo & " And POM_CompID=" & iCompID & " And POM_YearID=" & iYearID & ""
            GetOrderDtae = objDb.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iSupplierID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_Name From CustomerSupplierMaster Where CSM_ID=" & iSupplierID & " And CSM_CompID=" & iCompID & " and CSM_Delflag='A' "
            GetSupplierCode = objDb.SQLGetDescription(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Shared Function SavePurchaseReturn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dOrderDate As Date, ByVal objPO As clsPurchaseReturn) As Integer
    '    Dim sSql As String = ""
    '    Dim dr As OleDb.OleDbDataReader
    '    Dim iMax As Integer = 0
    '    Try


    '        sSql = "" : sSql = "Select * from Purchase_Return_Master where PRM_ReturnOrderCode = '" & objPO.PRM_ReturnOrderCode & "' and PRM_CompID =" & iCompID & " and PRM_YearID =" & objPO.PRM_YearID & ""
    '        dr = DBHelper.DBLDataReader(sNameSpace, sSql)
    '        If dr.HasRows = True Then
    '            dr.Read()
    '            sSql = "" : sSql = "Update Purchase_Return_Master set PRM_SupplierID = " & objPO.PRM_SupplierID & ",PRM_SupplierCode=" & objPO.PRM_SupplierCode & ",PRM_ModeOfReturn=" & objPO.PRM_ModeOfReturn & ",PRM_Narration = " & objPO.PRM_Narration & " "
    '            sSql = sSql & "Where PRM_OrderNo = '" & objPO.PRM_OrderNo & "' and PRM_CompID =" & iCompID & " and PRM_YearID=" & objPO.PRM_YearID & ""
    '            DBHelper.ExecuteNoNQuery(sNameSpace, sSql)
    '            Return dr("PRM_ID")
    '        Else
    '            iMax = clsTRACeGeneral.GetMaxID(sNameSpace, iCompID, "Purchase_Return_Master", "PRM_ID", "PRM_CompID")
    '            sSql = "" : sSql = "Insert into Purchase_Return_Master(PRM_ID,PRM_ReturnOrderCode,PRM_OrderNo,PRM_OrderDate,"
    '            sSql = sSql & "PRM_ReferenceNo,PRM_ReturnDate,PRM_SupplierID,PRM_SupplierCode,"
    '            sSql = sSql & "PRM_ModeOfReturn,PRM_Narration,PRM_Status,PRM_YearID,PRM_CompID)Values(" & iMax & ",'" & objPO.PRM_ReturnOrderCode & "','" & objPO.PRM_OrderNo & "', " & clsTRACeGeneral.FormatDtForRDBMS(objPO.PRM_OrderDate, "I") & ","
    '            sSql = sSql & "'" & objPO.PRM_ReferenceNo & "'," & clsTRACeGeneral.FormatDtForRDBMS(objPO.dPRM_ReturnDate, "I") & "," & objPO.PRM_SupplierID & ","
    '            sSql = sSql & "'" & objPO.PRM_SupplierCode & "','" & objPO.PRM_ModeOfReturn & "','" & objPO.PRM_Narration & "','w'," & objPO.PRM_YearID & "," & objPO.PRM_CompID & ")"
    '            DBHelper.ExecuteNoNQuery(sNameSpace, sSql)
    '            Return iMax
    '        End If
    '        dr.Close()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    'Public Shared Function SavePurchaseReturnMaster(ByVal sNameSpace As String, ByVal objPR As clsPurchaseReturn) As Array
    '    Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
    '    Dim iParamCount As Integer
    '    Dim Arr(1) As String
    '    Try
    '        iParamCount = 0
    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_ID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRM_ID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_ReturnOrderCode", OleDb.OleDbType.VarChar, 100)
    '        ObjParam(iParamCount).Value = objPR.sPRM_ReturnOrderCode
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_OrderNo", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRM_OrderNo
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_OrderDate", OleDb.OleDbType.Date)
    '        ObjParam(iParamCount).Value = objPR.dPRM_OrderDate
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_ReferenceNo", OleDb.OleDbType.VarChar, 100)
    '        ObjParam(iParamCount).Value = objPR.sPRM_ReferenceNo
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_ReturnDate", OleDb.OleDbType.Date)
    '        ObjParam(iParamCount).Value = objPR.dPRM_ReturnDate
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_SupplierID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRM_SupplierID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_SupplierCode", OleDb.OleDbType.VarChar, 100)
    '        ObjParam(iParamCount).Value = objPR.sPRM_SupplierCode
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_ModeOfReturn", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRM_ModeOfReturn
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_Narration", OleDb.OleDbType.VarChar, 5000)
    '        ObjParam(iParamCount).Value = objPR.sPRM_Narration
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_Status", OleDb.OleDbType.VarChar, 1)
    '        ObjParam(iParamCount).Value = objPR.sPRM_Status
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_YearID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRM_YearID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_CompID", OleDb.OleDbType.Integer, 15)
    '        ObjParam(iParamCount).Value = objPR.iPRM_CompID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_CreatedBy", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRM_CreatedBy
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRM_CreatedOn", OleDb.OleDbType.Date)
    '        ObjParam(iParamCount).Value = objPR.dPRM_CreatedOn
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        Arr(0) = "@iUpdateOrSave"
    '        Arr(1) = "@iOper"

    '        Arr = DBHelper.ExecStoredProcFrInsRetARR(sNameSpace, "spPurchase_Return_Master", 1, Arr, ObjParam)
    '        Return Arr
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Shared Function SavePurchaseReturnDetails(ByVal sNameSpace As String, ByVal objPR As clsPurchaseReturn) As Array
    '    Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(21) {}
    '    Dim iParamCount As Integer
    '    Dim Arr(1) As String
    '    Try
    '        iParamCount = 0
    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_ID ", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRD_ID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_MasterID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRD_MasterID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_CommodityID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRD_CommodityID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_DescriptionID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRD_DescriptionID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_HistoryID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRD_HistoryID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_PurchaseQnty", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRD_PurchaseQnty
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_ReturnQnty", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRD_ReturnQnty
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_Rate", OleDb.OleDbType.Double, 4)
    '        ObjParam(iParamCount).Value = objPR.dPRD_Rate
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_UnitOfMeasurement", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRD_UnitOfMeasurement
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_Discount", OleDb.OleDbType.Double, 4)
    '        ObjParam(iParamCount).Value = objPR.PRD_Discount
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_DiscountAmount", OleDb.OleDbType.VarChar, 100)
    '        ObjParam(iParamCount).Value = objPR.PRD_DiscountAmount
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_VAT", OleDb.OleDbType.Double, 4)
    '        ObjParam(iParamCount).Value = objPR.PRD_VAT
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_VATAmount", OleDb.OleDbType.VarChar, 100)
    '        ObjParam(iParamCount).Value = objPR.PRD_VATAmount
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_CST", OleDb.OleDbType.Double, 4)
    '        ObjParam(iParamCount).Value = objPR.PRD_CST
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_CSTAmount", OleDb.OleDbType.VarChar, 100)
    '        ObjParam(iParamCount).Value = objPR.PRD_CSTAmount
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_Excise", OleDb.OleDbType.Double, 4)
    '        ObjParam(iParamCount).Value = objPR.PRD_Excise
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_ExciseAmount", OleDb.OleDbType.VarChar, 100)
    '        ObjParam(iParamCount).Value = objPR.PRD_ExciseAmount
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_TotalAmount", OleDb.OleDbType.Double, 4)
    '        ObjParam(iParamCount).Value = objPR.dPRD_TotalAmount
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_CompID", OleDb.OleDbType.Integer, 15)
    '        ObjParam(iParamCount).Value = objPR.iPRD_CompID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@PRD_YearID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objPR.iPRD_YearID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        Arr(0) = "@iUpdateOrSave"
    '        Arr(1) = "@iOper"

    '        Arr = DBHelper.ExecStoredProcFrInsRetARR(sNameSpace, "spPurchase_Return_Details", 1, Arr, ObjParam)
    '        Return Arr
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetMasterDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * From Purchase_Return_Master Where PRM_ID=" & iOrderID & " And PRM_CompID=" & iCompID & " and PRM_YearID = " & iYearID & ""
            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComodityID(ByVal sNameSpace As String, ByVal ItemID As Integer) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select Inv_Parent from inventory_master where Inv_ID='" & ItemID & "'"
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPicess(ByVal sNameSpace As String, ByVal ItemID As Integer, ByVal ICompID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = " Select  INVH_PerPieces From Inventory_master_History Where InvH_ID=" & ItemID & " and INVH_CompID=" & ICompID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetUnitID(ByVal sNameSpace As String, ByVal historyID As Integer, ByVal ICompID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = " Select InvH_Unit From Inventory_master_History Where InvH_ID=" & historyID & " and INVH_CompID=" & ICompID & ""
            Return objDb.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadStockRateQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer, ByVal GInID As Integer, ByVal OrderId As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtNew As New DataTable
        Dim dRow As DataRow
        Try
            dtNew.Columns.Add("SL_PurchaseQty")
            dtNew.Columns.Add("PurchaseRate")
            dtNew.Columns.Add("SL_HistoryID")
            sSql = "select SL_PurchaseQty,PurchaseRate,SL_HistoryID from stock_ledger where SL_OrderID = " & OrderId & " and SL_CompID =" & iCompID & " and SL_GINID=" & GInID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtNew.NewRow
                dRow("SL_PurchaseQty") = dt.Rows(i)("SL_PurchaseQty")
                dRow("PurchaseRate") = dt.Rows(i)("PurchaseRate")
                dRow("SL_HistoryID") = dt.Rows(i)("SL_HistoryID")
                dtNew.Rows.Add(dRow)
            Next
            Return dtNew
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SalesOrderDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderNo As Integer) As DataTable
        Dim dt, dtTab As New DataTable
        Dim txtorderqty As String = "", sSql As String = ""
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("POM_Supplier")
            dtTab.Columns.Add("POM_OrderDate")
            dtTab.Columns.Add("POM_ModeOfShipping")
            dtTab.Columns.Add("POM_PaymentTerms")
            dtTab.Columns.Add("POM_MPayment")
            dtTab.Columns.Add("POM_DSchdule")
            sSql = "" : sSql = "Select * From Purchase_Order_Master Where POM_ID=" & OrderNo & " And POM_CompID=" & iCompID & " and POM_YearID = " & iYearID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("POM_OrderDate")) = False Then
                    dr("POM_OrderDate") = dt.Rows(i)("POM_OrderDate")
                Else
                    dr("POM_OrderDate") = ""
                End If
                dr("POM_PaymentTerms") = dt.Rows(i)("POM_PaymentTerms")
                dr("POM_MPayment") = dt.Rows(i)("POM_MPayment")
                dr("POM_DSchdule") = dt.Rows(i)("POM_DSchdule")
                dr("POM_Supplier") = dt.Rows(i)("POM_Supplier")   ' DBHelper.GetDescription(sNameSpace, "Select CSM_Name From CustomerSupplierMaster Where CSM_ID =" & dt.Rows(i)("POM_Supplier") & " and CSM_CompID =" & iCompID & "")
                dr("POM_ModeOfShipping") = dt.Rows(i)("POM_ModeOfShipping")  ' DBHelper.GetDescription(sNameSpace, "Select Mas_desc From acc_general_master Where Mas_id =" & dt.Rows(i)("POM_ModeOfShipping") & " and Mas_CompID = " & iCompID & " and Mas_master=13")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSalesStockRateQty(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDescriptionID As Integer, ByVal GInID As Integer, ByVal OrderId As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable, dtNew As New DataTable
        Dim dRow As DataRow
        Try
            dtNew.Columns.Add("SDD_Quantity")
            dtNew.Columns.Add("SDD_RateAmount")
            dtNew.Columns.Add("SDD_HistoryID")
            sSql = "select SDD_Quantity,SDD_RateAmount,SDD_HistoryID from Sales_Dispatch_Details where SDD_MasterID in(select SDM_ID from Sales_Dispatch_Master where SDM_OrderID=" & OrderId & " ) and SDD_CompID =" & iCompID & " and SDD_DescID=" & iDescriptionID & " "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtNew.NewRow
                dRow("SDD_Quantity") = dt.Rows(i)("SDD_Quantity")
                dRow("SDD_RateAmount") = dt.Rows(i)("SDD_RateAmount")
                dRow("SDD_HistoryID") = dt.Rows(i)("SDD_HistoryID")
                dtNew.Rows.Add(dRow)
            Next
            Return dtNew
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
            dtTab.Columns.Add("PurchaseQty")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("ReturnQty")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("VAT")
            dtTab.Columns.Add("VATAmount")
            dtTab.Columns.Add("ExciseDuty")
            dtTab.Columns.Add("ExciseDutyAmount")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("CSTAmount")
            dtTab.Columns.Add("Amount")

            If iMasterID > 0 Then
                sSql = "Select * From Purchase_Return_Details Where PRD_MasterID=" & iMasterID & " And PRD_CompiD=" & iCompID & " Order By PRD_ID "
            End If

            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("PRD_CommodityID")
                    dRow("DescID") = dt.Rows(i)("PRD_DescriptionID")
                    dRow("HistoryID") = dt.Rows(i)("PRD_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("PRD_UnitOfMeasurement")
                    dRow("SlNo") = i + 1
                    dRow("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & dt.Rows(i)("PRD_DescriptionID") & " And INV_Parent=" & dt.Rows(i)("PRD_CommodityID") & " and Inv_CompID = " & iCompID & "")
                    dRow("UnitOfMeassurement") = objDb.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("PRD_UnitOfMeasurement") & " and Mas_CompID =" & iCompID & "")
                    dRow("PurchaseQty") = dt.Rows(i)("PRD_PurchaseQnty")
                    dRow("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PRD_Rate")))
                    dRow("ReturnQty") = dt.Rows(i)("PRD_ReturnQnty")

                    dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PRD_Discount")))
                    If IsDBNull(dt.Rows(i)("PRD_DiscountAmount")) = False Then
                        dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PRD_DiscountAmount")))
                    End If

                    dRow("VAT") = dt.Rows(i)("PRD_VAT")
                    dRow("VATAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PRD_VATAmount")))

                    If dt.Rows(i)("PRD_Excise") > 0 Then
                        dRow("ExciseDuty") = dt.Rows(i)("PRD_Excise")
                    Else
                        dRow("ExciseDuty") = 0
                    End If
                    dRow("ExciseDutyAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PRD_ExciseAmount")))

                    dRow("CST") = dt.Rows(i)("PRD_CST")
                    dRow("CSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PRD_CSTAmount")))
                    dRow("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("PRD_TotalAmount")))

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
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
            sMaximumID = objDb.SQLGetDescription(sNameSpace, "Select Top 1 PRM_ID From Purchase_Return_Master Order By PRM_ID Desc")
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

            If sDate.Length = 1 Then
                sSDate = "0" & "" & sDate & ""
            Else
                sSDate = sDate
            End If
            sStr = "PR" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPurchaseReturnID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = sSql & "Select PRM_ReturnOrderCode,PRM_OrderNo,CONVERT(VARCHAR(10),PRM_OrderDate,103)As PRM_OrderDate,PRM_ReferenceNo,CONVERT(VARCHAR(10),PRM_ReturnDate,103)As PRM_ReturnDate,PRM_SupplierID,PRM_ModeOfReturn,b.POM_OrderNo,c.PRD_CommodityID,c.PRD_DescriptionID,c.PRD_PurchaseQnty,c.PRD_ReturnQnty,c.PRD_Rate,c.PRD_UnitOfMeasurement,c.PRD_Totalamount,"
            sSql = sSql & " c.PRD_Discount,c.PRD_DiscountAmount,c.PRD_VAT,c.PRD_VATAmount,c.PRD_CST,c.PRD_CSTAmount,c.PRD_Excise,c.PRD_ExciseAmount,"
            sSql = sSql & " d.INV_Code, d.INV_Description, e.Mas_Desc, f.CUST_CODE, f.CUST_COMM_ADDRESS, f.CUST_COMM_TEL, f.CUST_EMAIL, g.Cmp_Value, h.CSM_Name, h.CSM_Address, h.CSM_MobileNo, h.CSM_EmailID, i.CST_Value,j.INV_Description As Commodity "
            sSql = sSql & " From Purchase_Return_Master"
            sSql = sSql & " Join Purchase_Order_Master b On PRM_OrderNo= b.POM_ID "
            sSql = sSql & " Join Purchase_Return_Details c On PRM_ID=c.PRD_MasterID "
            sSql = sSql & " Join Inventory_master d on c.PRD_DescriptionID=d.INV_ID "
            sSql = sSql & " Join Acc_General_Master e On c.PRD_UnitOfMeasurement=e.Mas_ID "
            sSql = sSql & " Join MST_Customer_Master f ON f.CUST_ID = c.PRD_CompID "
            sSql = sSql & " Join Company_Accounting_Template g ON f.CUST_ID = g.Cmp_ID And g.Cmp_Desc='TIN' "
            sSql = sSql & " Join CustomerSupplierMaster h On PRM_SupplierID=h.CSM_ID "
            sSql = sSql & " Left Join Customer_Supplier_Template i ON h.CSM_ID = i.CST_SupplierID And i.CST_Description='TIN' "
            sSql = sSql & " Join Inventory_master j on c.PRD_CommodityID=j.INV_ID Where PRM_ID=" & iPurchaseReturnID & " And PRM_CompID=" & iCompID & " And PRM_YearID=" & iYearID & " "

            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GoodsReturnAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPurchaseReturnID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtFRomTable As New DataTable
        Dim dRow As DataRow
        Dim subtotal As Double = 0
        Try
            dt.Columns.Add("PRM_ReturnNo")
            dt.Columns.Add("PRM_OrderDate")
            dt.Columns.Add("PRM_Supplier")
            dt.Columns.Add("PRD_Commodity")
            dt.Columns.Add("PRD_Rate")
            dt.Columns.Add("PRD_Quantity")
            dt.Columns.Add("PRD_CST")
            dt.Columns.Add("PRD_VAT")
            dt.Columns.Add("PRD_CSTAmount")
            dt.Columns.Add("PRD_RateAmount")
            dt.Columns.Add("PRD_Discount")
            dt.Columns.Add("PRD_DiscountAmount")
            dt.Columns.Add("PRD_TotalAmount")
            dt.Columns.Add("PRD_VATAmount")
            dt.Columns.Add("PRD_Unit")
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
            dt.Columns.Add("PRD_Excise")
            dt.Columns.Add("PRD_ExciseAmount")
            dt.Columns.Add("PRD_Frieght")
            dt.Columns.Add("PRD_FrieghtAmount")
            dt.Columns.Add("PRM_Remarks")
            sSql = "Select PRM_ReturnNo, Convert(VARCHAR(10), PRM_OrderDate, 103)As PRM_OrderDate, PRM_Supplier, b.PRD_Commodity, b.PRD_Rate, b.PRD_Quantity,PRM_Remarks, "
            sSql = sSql & "b.PRD_CST, Convert(money, b.PRD_CSTAmount) As PRD_CSTAmount, Convert(money, b.PRD_RateAmount) As PRD_RateAmount,"
            sSql = sSql & "Convert(money, b.PRD_Discount)As PRD_Discount,Convert(money,b.PRD_DiscountAmount) As PRD_DiscountAmount,"
            sSql = sSql & "Convert(money,b.PRD_TotalAmount)As PRD_TotalAmount,"
            sSql = sSql & "Convert(money,b.PRD_VAT)As PRD_VAT,Convert(money,b.PRD_Excise)As PRD_Excise,PRD_Frieght,PRD_FrieghtAmount,PRD_ExciseAmount,Convert(money,b.PRD_ExciseAmount)As PRD_ExciseAmount,Convert(money,b.PRD_VATAmount)As PRD_VATAmount,b.PRD_Unit, "
            sSql = sSql & "c.INV_Code,c.INV_Description,d.CSM_Name,d.CSM_Address,d.CSM_MobileNo,d.CSM_EmailID,e.Mas_Desc,m.CUST_CODE,m.CUST_Name,m.CUST_COMM_ADDRESS,m.CUST_EMAIL,m.CUST_COMM_TEL,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate "
            sSql = sSql & "From Purchase_Return_Master "
            sSql = sSql & "join Purchase_Return_Details b On PRM_ID=" & iPurchaseReturnID & " And PRM_ID=b.PRD_MasterID And b.PRD_Status <> 'D' "
            sSql = sSql & "Join Inventory_master_history InvH on  PRD_HistoryID=InvH.InvH_ID "
            sSql = sSql & "Join Inventory_master c on  PRD_DescriptionID=c.INV_ID "
            sSql = sSql & "Join CustomerSupplierMaster d On PRM_Supplier=d.CSM_ID "
            sSql = sSql & "Join Acc_General_master e on b.PRD_Unit=e.Mas_ID "
            sSql = sSql & "Join MST_CUSTOMER_MASTER m on b.PRD_CompID=m.CUST_ID "
            dtFRomTable = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtFRomTable.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtFRomTable.Rows(i)("PRM_ReturnNo")) = False Then
                    dRow("PRM_ReturnNo") = dtFRomTable.Rows(i)("PRM_ReturnNo")
                Else
                    dRow("PRM_ReturnNo") = ""
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRM_OrderDate")) = False Then
                    dRow("PRM_OrderDate") = dtFRomTable.Rows(i)("PRM_OrderDate")
                Else
                    dRow("PRM_OrderDate") = ""
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_Commodity")) = False Then
                    dRow("PRD_Commodity") = dtFRomTable.Rows(i)("PRD_Commodity")
                Else
                    dRow("PRD_Commodity") = ""
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_Rate")) = False Then
                    dRow("PRD_Rate") = dtFRomTable.Rows(i)("PRD_Rate")
                Else
                    dRow("PRD_Rate") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_Quantity")) = False Then
                    dRow("PRD_Quantity") = dtFRomTable.Rows(i)("PRD_Quantity")
                Else
                    dRow("PRD_Quantity") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_CST")) = False Then
                    dRow("PRD_CST") = dtFRomTable.Rows(i)("PRD_CST")
                Else
                    dRow("PRD_CST") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_VAT")) = False Then
                    dRow("PRD_VAT") = dtFRomTable.Rows(i)("PRD_VAT")
                Else
                    dRow("PRD_VAT") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_CSTAmount")) = False Then
                    dRow("PRD_CSTAmount") = Convert.ToDouble(dtFRomTable.Rows(i)("PRD_CSTAmount"))
                Else
                    dRow("PRD_CSTAmount") = 0
                End If
                If IsDBNull(dtFRomTable.Rows(i)("PRD_RateAmount")) = False Then
                    dRow("PRD_RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dtFRomTable.Rows(i)("PRD_RateAmount") - dtFRomTable.Rows(i)("PRD_DiscountAmount"))))
                    subtotal = subtotal + dRow("PRD_RateAmount")
                Else
                    dRow("PRD_RateAmount") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_Discount")) = False Then
                    dRow("PRD_Discount") = dtFRomTable.Rows(i)("PRD_Discount")
                Else
                    dRow("PRD_Discount") = 0
                End If
                If IsDBNull(dtFRomTable.Rows(i)("PRD_DiscountAmount")) = False Then
                    dRow("PRD_DiscountAmount") = dtFRomTable.Rows(i)("PRD_DiscountAmount")
                Else
                    dRow("PRD_DiscountAmount") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_TotalAmount")) = False Then
                    dRow("PRD_TotalAmount") = dtFRomTable.Rows(i)("PRD_TotalAmount")
                Else
                    dRow("PRD_TotalAmount") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_VATAmount")) = False Then
                    dRow("PRD_VATAmount") = dtFRomTable.Rows(i)("PRD_VATAmount")
                Else
                    dRow("PRD_VATAmount") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_Unit")) = False Then
                    dRow("PRD_Unit") = dtFRomTable.Rows(i)("PRD_Unit")
                Else
                    dRow("PRD_Unit") = 0
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
                If IsDBNull(dtFRomTable.Rows(i)("PRD_Excise")) = False Then
                    dRow("PRD_Excise") = dtFRomTable.Rows(i)("PRD_Excise")
                Else
                    dRow("PRD_Excise") = 0
                End If
                If IsDBNull(dtFRomTable.Rows(i)("PRD_ExciseAmount")) = False Then
                    dRow("PRD_ExciseAmount") = dtFRomTable.Rows(i)("PRD_ExciseAmount")
                Else
                    dRow("PRD_ExciseAmount") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_Frieght")) = False Then
                    dRow("PRD_Frieght") = dtFRomTable.Rows(i)("PRD_Frieght")
                Else
                    dRow("PRD_Frieght") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRD_FrieghtAmount")) = False Then
                    dRow("PRD_FrieghtAmount") = dtFRomTable.Rows(i)("PRD_FrieghtAmount")
                Else
                    dRow("PRD_FrieghtAmount") = 0
                End If

                If IsDBNull(dtFRomTable.Rows(i)("PRM_Remarks")) = False Then
                    dRow("PRM_Remarks") = dtFRomTable.Rows(i)("PRM_Remarks")
                Else
                    dRow("PRM_Remarks") = 0
                End If
                dt.Rows.Add(dRow)
            Next

            Return dt
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
                bCheckO = objDb.SQLCheckForRecord(sNameSpace, "Select * From Purchase_Return_Master where PRM_ReturnNo ='" & sSearch & "' And PRM_CompID=" & iCompID & " and PRM_YearID =" & iYearID & "")
                If bCheckO = True Then
                    sSql = "Select PRM_ID,PRM_ReturnNo From Purchase_Return_Master where PRM_ReturnNo ='" & sSearch & "' And PRM_CompID=" & iCompID & " and PRM_YearID =" & iYearID & ""
                    dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                End If
            Else
                sSql = "Select PRM_ID,PRM_ReturnNo From Purchase_Return_Master where PRM_CompID=" & iCompID & " and PRM_YearID = " & iYearID & " Order By PRM_ID Desc"
                dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
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


            'lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))        
            If (num > 1000) Then

                If ((Convert.ToString(num / 1000)).Contains(".")) Then
                    thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
                    num = num - (thousands * 1000)
                Else
                    thousands = num / 1000
                    num = num - (thousands * 1000)
                End If
            End If
            'thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))        

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

            'crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
            'num = num - (crore * 10000000)


            If ((Convert.ToString(num / 100000)).Contains(".")) Then
                lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                num = num - (hundreds * 100000)
            Else
                lakhs = num / 100000
                num = num - (hundreds * 100000)
            End If

            'lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))

            'num = num - (lakhs * 100000)

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

    Public Function GetCompanyMasterTemplete(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Company_Accounting_Template where CMP_ID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVendorTemplete(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "select * from Customer_Supplier_Template where CST_SupplierID in(select POM_Supplier from purchase_ORder_Master where POM_ID =" & iOrderID & ")"
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInwardNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As Integer) As DataTable
        Dim sSql As String = ""
        Try

            sSql = "Select PGM_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_DocumentRefNo In"
            sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1  And PV_OrderNo=" & iTransactionID & ")  "
            sSql = sSql & " And PGM_OrderID=" & iTransactionID & " And "
            sSql = sSql & "PGM_CompID=1 and PGM_YearID =" & iYearID & ""
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function BindDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal OrderID As Integer) As DataTable
        Dim sSql As String = ""
        Try

            sSql = "select SDM_ID,SDM_Code from Sales_Dispatch_Master where SDM_CompID=" & iCompID & " and SDM_OrderID=" & OrderID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindOrderGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasterID As Integer) As DataTable
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
            dtTab.Columns.Add("PurchaseQty")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("ReturnQty")
            dtTab.Columns.Add("Discount")
            dtTab.Columns.Add("DiscountAmount")
            dtTab.Columns.Add("VAT")
            dtTab.Columns.Add("VATAmount")
            dtTab.Columns.Add("ExciseDuty")
            dtTab.Columns.Add("ExciseDutyAmount")
            dtTab.Columns.Add("CST")
            dtTab.Columns.Add("CSTAmount")
            dtTab.Columns.Add("Amount")

            If iMasterID > 0 Then
                sSql = "Select * From Purchase_Order_Details Where POD_MasterID=" & iMasterID & " And POD_CompiD=" & iCompID & "  Order By POD_ID "
            End If

            dt = objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("CommodityID") = dt.Rows(i)("POD_Commodity")
                    dRow("DescID") = dt.Rows(i)("POD_DescriptionID")
                    dRow("HistoryID") = dt.Rows(i)("POD_HistoryID")
                    dRow("UnitID") = dt.Rows(i)("POD_Unit")
                    dRow("SlNo") = i + 1
                    dRow("Goods") = objDb.SQLGetDescription(sNameSpace, "Select INV_Code From Inventory_Master Where INV_ID=" & dt.Rows(i)("POD_DescriptionID") & " And INV_Parent=" & dt.Rows(i)("POD_Commodity") & " and Inv_CompID = " & iCompID & "")
                    dRow("UnitOfMeassurement") = objDb.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master Where Mas_ID=" & dt.Rows(i)("POD_Unit") & " and Mas_CompID =" & iCompID & "")
                    dRow("PurchaseQty") = dt.Rows(i)("POD_Quantity")
                    dRow("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("POD_Rate")))
                    dRow("ReturnQty") = ""

                    dRow("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("POD_Discount")))
                    If IsDBNull(dt.Rows(i)("POD_DiscountAmount")) = False And dt.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("POD_DiscountAmount")))
                    Else
                        dRow("DiscountAmount") = 0
                    End If

                    dRow("VAT") = dt.Rows(i)("POD_VAT")
                    dRow("VATAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("POD_VATAmount")))

                    If dt.Rows(i)("POD_Excise") > 0 Then
                        dRow("ExciseDuty") = dt.Rows(i)("POD_Excise")
                    Else
                        dRow("ExciseDuty") = 0
                    End If
                    dRow("ExciseDutyAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("POD_ExciseAmount")))

                    dRow("CST") = dt.Rows(i)("POD_CST")
                    dRow("CSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt.Rows(i)("POD_CSTAmount")))

                    dRow("Amount") = ""

                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
