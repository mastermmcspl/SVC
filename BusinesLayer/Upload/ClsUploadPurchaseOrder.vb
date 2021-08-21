Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsUploadPurchaseOrder
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objDb As New DBHelper
    Dim objFas As New clsFASGeneral
    Dim objGnrl As New clsGeneralFunctions
    '  Dim poUpload As New ClsPurchaseOrderUpload
    Public Shared Function RemoveQuote(ByVal sString As String) As String
        Try
            RemoveQuote = Trim(Replace(sString, "'", "`"))
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCode(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As String
        Dim sSql As String = ""
        Dim sCode As String = ""
        Try
            sCode = objDBL.SQLGetDescription(sNameSpace, "Select BM_Code From Sales_Buyers_Masters Where BM_ID=" & iParty & " And BM_CompID=" & iCompID & " ")
            Return sCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " and CSM_Delflag='A' order by CSM_Name"
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPurchaseItems(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try

            dtTab.Columns.Add("ItemCode")
            dtTab.Columns.Add("Goods")
            dtTab.Columns.Add("UnitOfMeassurement")
            dtTab.Columns.Add("Rate")
            dtTab.Columns.Add("Quantity")
            'dtTab.Columns.Add("VAT")
            sSql = "" : sSql = "Select a.INV_Code,a.INV_Description,b.INVH_PreDeterminedPrice As Rate,b.INVH_VAT,d.Mas_Desc
                                    From Inventory_Master a 
                                    join Inventory_Master_History b On b.INVH_INV_ID=a.INV_ID   
                                    Join Acc_General_Master d On d.Mas_ID=b.INVH_Unit
                                    where a.INV_CompiD=" & iCompID & " Order By INV_ID"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("ItemCode") = dt.Rows(i)("INV_Code")
                    dRow("Goods") = dt.Rows(i)("INV_Description")
                    dRow("UnitOfMeassurement") = dt.Rows(i)("Mas_Desc")
                    dRow("Rate") = dt.Rows(i)("Rate")
                    dRow("Quantity") = ""
                    'dRow("VAT") = dt.Rows(i)("INVH_VAT")
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal dtUpload As DataTable, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iSplrID As Integer)
        Dim iMasterID As Integer = 0
        Dim dOrderDate As Date
        Dim dRequiredDate As Date
        Dim objPO As New clsPurchaseOrder
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0
        Dim dDiscountAmount As Double = 0.0
        Dim dVATAmount As Double = 0.0
        Dim dCSTAmount As Double = 0.0
        Dim dExciseAmount As Double = 0.0
        Dim dNetAmount As Double = 0.0
        Try
            dOrderDate = System.DateTime.Today
            objPO.sPOM_OrderNo = objPO.GeneratePurchaseOrderCode(sNameSpace, iCompID)
            objPO.iPOM_Supplier = iSplrID
            objPO.iPOM_ModeOfShipping = 0
            objPO.iPOM_CreatedBy = iUserID
            objPO.iPOM_YearID = iYearID
            objPO.iPOM_Paymentterms = 0
            objPO.iPOM_MethodofPayment = 0
            objPO.iPOM_DSchdule = 0
            objPO.iPOM_SaleType = 1
            objPO.iPOM_iCSTCtgry = 0
            objPO.sPOM_Status = "W"
            objPO.sOralOrPO = "P"
            objPO.sPOM_ESugam = ""
            objPO.sPOM_DEliveryChlnNo = ""
            objPO.sPOM_InvoiceRef = ""
            objPO.sPOD_IPAddress = sIPAddress
            sSql = "" : sSql = "Select * from Purchase_Order_Master where POM_OrderNo = '" & objPO.sPOM_OrderNo & "' and POM_CompID =" & iCompID & " and POM_YearID =" & objPO.iPOM_YearID & ""
            dr = objDb.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sSql = "" : sSql = "Update Purchase_Order_Master set POM_Supplier = " & objPO.iPOM_Supplier & ",POM_MPayment=" & objPO.iPOM_MethodofPayment & ",POM_PaymentTerms=" & objPO.iPOM_Paymentterms & ",POM_ModeOfShipping = " & objPO.iPOM_ModeOfShipping & ",POM_DSchdule=" & objPO.iPOM_DSchdule & " "
                sSql = sSql & "Where POM_OrderNo = '" & objPO.sPOM_OrderNo & "' and POM_CompID =" & iCompID & " and POM_YearID=" & objPO.iPOM_YearID & ""
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Return dr("POM_ID")
            Else
                iMax = objGenFun.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Master", "POM_ID", "POM_CompID")
                sSql = "" : sSql = "Insert into Purchase_Order_Master(POM_ID,POM_OrderDate,POM_OrderNo,POM_Supplier,"
                sSql = sSql & "POM_Status,POM_CreatedBy,POM_CreatedOn,"
                sSql = sSql & "POM_YearID,POM_CompID,POM_ModeOfShipping,POM_ESugamNo,POM_InvoiceRef,POM_OralStatus,POM_CstCategory,POM_TypeOfPurchase,POM_DcNo,POM_MPayment,POM_PaymentTerms,POM_DSchdule,POM_DelFlag,POM_IPAddress)Values(" & iMax & "," & objFas.FormatDtForRDBMS(dOrderDate, "I") & ",'" & objPO.sPOM_OrderNo & "'," & objPO.iPOM_Supplier & ","
                sSql = sSql & "'" & objPO.sPOM_Status & "'," & objPO.iPOM_CreatedBy & ",GetDate(),"
                sSql = sSql & "" & objPO.iPOM_YearID & "," & iCompID & "," & objPO.iPOM_ModeOfShipping & ",'" & objPO.sPOM_ESugam & "','" & objPO.sPOM_InvoiceRef & "','" & objPO.sOralOrPO & "'," & objPO.iPOM_iCSTCtgry & "," & objPO.iPOM_SaleType & ",'" & objPO.sPOM_Status & "','" & objPO.iPOM_MethodofPayment & "','" & objPO.iPOM_Paymentterms & "','" & objPO.iPOM_DSchdule & "','W','" & sIPAddress & "')"
                objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                iMasterID = iMax
            End If
            dr.Close()
            objPO.iPOD_MasterID = iMasterID
            For i = 0 To dtUpload.Rows.Count - 1
                objPO.iPOD_Commodity = objDb.SQLExecuteScalar(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_Code='" & dtUpload.Rows(i)(0).ToString() & "' And INV_CompID=" & iCompID & " ")

                If dtUpload.Rows(i)(1).ToString() = "" Then

                    objPO.iPOD_DescriptionID = 0
                Else

                    objPO.iPOD_DescriptionID = objDb.SQLExecuteScalar(sNameSpace, "Select INV_ID From Inventory_Master Where INV_Code='" & dtUpload.Rows(i)(0).ToString() & "' And INV_CompID=" & iCompID & " ")
                End If

                If dtUpload.Rows(i)(2).ToString() = "" Then
                    objPO.iPOD_Unit = 0
                Else
                    objPO.iPOD_Unit = CheckCityExistOrNot(sNameSpace, iCompID, dtUpload.Rows(i)(2).ToString(), 1)
                End If
                objPO.iPOD_HistoryID = objDb.SQLExecuteScalar(sNameSpace, "Select INVH_ID From Inventory_Master_History Where INVH_INV_ID=" & objPO.iPOD_DescriptionID & " And INVH_CompID=" & iCompID & " ")
                'Quantity
                If dtUpload.Rows(i)(4).ToString() = "" Then
                    objPO.sPOD_Quantity = 0
                Else
                    objPO.sPOD_Quantity = dtUpload.Rows(i)(4).ToString()
                End If
                'Rate
                If dtUpload.Rows(i)(3).ToString() = "" Then
                    objPO.sPOD_Rate = 0.0
                Else
                    objPO.sPOD_Rate = dtUpload.Rows(i)(3).ToString()
                    objPO.sPOD_RateAmount = objPO.sPOD_Rate * objPO.sPOD_Quantity
                End If
                dNetAmount = objPO.sPOD_Quantity * objPO.sPOD_Rate
                objPO.sPOD_Discount = 0.0
                objPO.sPOD_Discount = 0.0
                objPO.sPOD_DiscountAmount = 0.0
                ''VAT
                'If dtUpload.Rows(i)(5).ToString() = "" Then
                '    ' iVATID = 0
                '    objPO.sPOD_VAT = 0.0
                '    objPO.sPOD_VATAmount = 0.0
                'Else
                '    objPO.sPOD_VAT = dtUpload.Rows(i)(5).ToString()
                '    objPO.sPOD_VATAmount = (dNetAmount * Convert.ToDecimal(objPO.sPOD_VAT) / 100)
                'End If
                objPO.sPOD_VAT = 0.0
                objPO.sPOD_VATAmount = 0.0

                objPO.sPOD_CST = 0.0
                objPO.sPOD_CSTAmount = 0.0
                objPO.dPOD_RequiredDate = "01/01/1900"
                'Excise
                'If dtUpload.Rows(i)(7).ToString() = "" Then
                objPO.sPOD_Excise = 0.0
                objPO.sPOD_ExciseAmount = 0.0
                'Else
                objPO.POD_ReceivedQty = 0
                objPO.POD_Rejected = 0
                objPO.POD_Accepted = 0
                'objPO.sPOD_TotalAmount = dNetAmount + Convert.ToDecimal(objPO.sPOD_VATAmount) + Convert.ToDecimal(objPO.sPOD_ExciseAmount) + Convert.ToDecimal(objPO.sPOD_CSTAmount)
                objPO.sPOD_TotalAmount = dNetAmount
                Dim sStatus As String = ""
                sSql = "" : sSql = "Select * from Purchase_Order_Details where POD_MasterID = " & objPO.iPOD_MasterID & " and POD_Commodity = " & objPO.iPOD_Commodity & " and "
                sSql = sSql & "POD_DescriptionID = " & objPO.iPOD_DescriptionID & " and POD_HistoryID =" & objPO.iPOD_HistoryID & " and POD_CompID = " & iCompID & ""
                dr = objDb.SQLDataReader(sNameSpace, sSql)
                If dr.HasRows = True Then
                    sSql = "" : sSql = "Update Purchase_Order_Details set POD_Unit = " & objPO.iPOD_Unit & ",POD_Rate='" & objPO.sPOD_Rate & "',POD_RateAmount = '" & objPO.sPOD_RateAmount & "',POD_Quantity='" & objPO.sPOD_Quantity & "',"
                    sSql = sSql & "POD_Discount = '" & objPO.sPOD_Discount & "',POD_DiscountAmount='" & objPO.sPOD_DiscountAmount & "',POD_Excise='" & objPO.sPOD_Excise & "',"
                    sSql = sSql & "POD_ExciseAmount = '" & objPO.sPOD_ExciseAmount & "',POD_VAT = '" & objPO.sPOD_VAT & "',POD_VATAmount='" & objPO.sPOD_VATAmount & "',"
                    sSql = sSql & "POD_CST='" & objPO.sPOD_CST & "',POD_CSTAmount='" & objPO.sPOD_CSTAmount & "',POD_RequiredDate=" & objFas.FormatDtForRDBMS(objPO.dPOD_RequiredDate, "I") & ","
                    sSql = sSql & "POD_TotalAmount='" & objPO.sPOD_TotalAmount & "',POD_Status='W' where POD_MasterID = " & objPO.iPOD_MasterID & " and "
                    sSql = sSql & "POD_Commodity = " & objPO.iPOD_Commodity & " and POD_DescriptionID = " & objPO.iPOD_DescriptionID & " and "
                    sSql = sSql & "POD_HistoryID =" & objPO.iPOD_HistoryID & " and POD_CompID = " & iCompID & ""
                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                Else
                    iMax = objGenFun.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Details", "POD_ID", "POD_CompID")
                    sSql = "" : sSql = "Insert into Purchase_Order_Details(POD_ID,POD_MasterID,POD_Commodity,"
                    sSql = sSql & "POD_DescriptionID,POD_HistoryID,POD_Unit,POD_Rate,POD_RateAmount,"
                    sSql = sSql & "POD_Quantity,POD_Discount,POD_DiscountAmount,POD_Excise,"
                    sSql = sSql & "POD_ExciseAmount,POD_VAT,POD_VATAmount,POD_CST,"
                    sSql = sSql & "POD_CSTAmount,POD_RequiredDate,POD_TotalAmount,POD_CompID,POD_Status)"
                    sSql = sSql & "Values(" & iMax & "," & objPO.iPOD_MasterID & "," & objPO.iPOD_Commodity & ","
                    sSql = sSql & "" & objPO.iPOD_DescriptionID & "," & objPO.iPOD_HistoryID & "," & objPO.iPOD_Unit & ",'" & objPO.sPOD_Rate & "','" & objPO.sPOD_RateAmount & "',"
                    sSql = sSql & "'" & objPO.sPOD_Quantity & "','" & objPO.sPOD_Discount & "','" & objPO.sPOD_DiscountAmount & "','" & objPO.sPOD_Excise & "',"
                    sSql = sSql & "'" & objPO.sPOD_ExciseAmount & "','" & objPO.sPOD_VAT & "','" & objPO.sPOD_VATAmount & "','" & objPO.sPOD_CST & "',"
                    sSql = sSql & "'" & objPO.sPOD_CSTAmount & "'," & objFas.FormatDtForRDBMS(objPO.dPOD_RequiredDate, "I") & ",'" & objPO.sPOD_TotalAmount & "'," & iCompID & ",'W')"
                    objDb.SQLExecuteNonQuery(sNameSpace, sSql)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCityExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String, ByVal iMaster As Integer) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sDesc & "' and Mas_Master = " & iMaster & " And MAS_CompID=" & iCompID & " "
            dr = objDb.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckCityExistOrNot = dr("Mas_Id")
            Else
                CheckCityExistOrNot = 0
            End If
            dr.Close()
            Return CheckCityExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SavePurchaseOrderMasters(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal dtUpload As DataTable, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iSupplierID As Integer)
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iMax As Integer = 0

        Dim iMaxOrderID As Integer = 0
        Dim sOrderCode, sSupplierCode As String

        Dim iCommodityID, iItemID, iUnit, iHistoryID, iQuantity As Integer
        Dim dRate, dDiscount, dRateAmount As Double
        Dim dVAT, dCST, dExcise As Double

        Dim dDiscountAmount As Double = 0.0
        Dim dVATAmount As Double = 0.0
        Dim dCSTAmount As Double = 0.0
        Dim dExciseAmount As Double = 0.0
        Dim dNetAmount As Double = 0.0

        Dim sCode As String = ""
        Dim sArray As String()
        Dim sbret As String
        Dim dBasicPrice As Double

        Dim iVATID, iCSTID, iExciseID As Double
        Try

            sOrderCode = GenerateOrderCode(sNameSpace, iCompID)

            'PartyCode
            sSupplierCode = objDBL.SQLGetDescription(sNameSpace, "Select CSM_Code From CustomerSupplierMaster Where CSM_ID=" & iSupplierID & " And CSM_CompID=" & iCompID & " ")

            'sbret = sPartyCode
            'sArray = sbret.Split("-")
            'For i = 0 To sArray.Length - 1
            '    sCode = Trim(sArray(i))
            '    If sCode = "P" Then
            '        GoTo ItemCode
            '    ElseIf sCode = "C" Then
            '        GoTo ItemCode
            '    End If
            'Next

            '--- Master table ---'

            iMaxOrderID = objGenFun.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Master", "POM_ID", "POM_CompID")
            sSql = "" : sSql = "Insert into Purchase_Order_Master(POM_ID,POM_OrderDate,POM_OrderNo,POM_Supplier,POM_ModeOfShipping,"
            sSql = sSql & "POM_Status,POM_CreatedBy,POM_CreatedOn,POM_YearID,POM_CompID,POM_Operation,POM_IPADdress,POM_MPayment,POM_PaymentTerms,POM_DSchdule,POM_TypeOfPurchase,POM_CstCategory,POM_DelFlag,POM_OralStatus)"
            sSql = sSql & "Values(" & iMaxOrderID & ",GetDate(),'" & RemoveQuote(sOrderCode) & "'," & iSupplierID & ",0,"
            sSql = sSql & "'A'," & iUserID & ",GetDate()," & iYearID & "," & iCompID & ",'C','" & sIPAddress & "',0,0,0,0,0,'W','P')"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            '--- Master table ---'

            'iVATID = GetVATZeroID(sNameSpace, iCompID)
            iCSTID = 0
            iExciseID = 0

            For i = 0 To dtUpload.Rows.Count - 1
                '--= Detail table ---'
                iCommodityID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INV_Parent From Inventory_Master Where INV_Description='" & dtUpload.Rows(i)(1).ToString() & "' And INV_CompID=" & iCompID & " ")
                'Goods
                If dtUpload.Rows(i)(1).ToString() = "" Then
                    iItemID = 0
                Else
                    iItemID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INV_ID From Inventory_Master Where INV_Parent=" & iCommodityID & " And INV_Description='" & dtUpload.Rows(i)(1).ToString() & "' And INV_CompID=" & iCompID & " ")
                End If
                'UnitOfMeassurement
                If dtUpload.Rows(i)(2).ToString() = "" Then
                    iUnit = 0
                Else
                    iUnit = CheckCityExistOrNot(sNameSpace, iCompID, dtUpload.Rows(i)(2).ToString(), 1)
                End If
                iHistoryID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select INVH_ID From Inventory_Master_History Where INVH_PreDeterminedPrice=" & dtUpload.Rows(i)(3).ToString() & " And INVH_INV_ID=" & iItemID & " And INVH_CompID=" & iCompID & " ")

                'Rate
                If dtUpload.Rows(i)(3).ToString() = "" Then
                    dRate = 0.0
                Else
                    dRate = dtUpload.Rows(i)(3).ToString()
                End If

                'Quantity
                If dtUpload.Rows(i)(4).ToString() = "" Then
                    iQuantity = 0
                Else
                    iQuantity = dtUpload.Rows(i)(4).ToString()
                End If

                dRateAmount = iQuantity * dRate
                dNetAmount = iQuantity * dRate

                'iVATID = GetVAT(sNameSpace, iCompID, iItemID, iHistoryID)
                'dVAT = GetVAT(sNameSpace, iCompID, iItemID, iHistoryID)
                'dVATAmount = (dRateAmount * dVAT) / (100)
                'dNetAmount = dRateAmount + dVATAmount

                iVATID = 0
                dVAT = 0
                dVATAmount = 0

                If iQuantity > 0 Then
                    sSql = "" : sSql = "Select * from Purchase_Order_Details where POD_MasterID =" & iMaxOrderID & " and POD_Commodity=" & iCommodityID & " and POD_DescriptionID=" & iItemID & " and POD_CompID=" & iCompID & " "
                    dr = objDBL.SQLDataReader(sNameSpace, sSql)
                    If dr.HasRows = True Then
                        sSql = "" : sSql = "Update Purchase_Order_Details set POD_Quantity=" & iQuantity & ",POD_Discount=" & dDiscount & ",POD_VAT=" & iVATID & ",POD_CST=" & iCSTID & ",POD_Excise=" & iExciseID & ""
                        sSql = sSql & " where POD_MasterID =" & iMaxOrderID & " and POD_Commodity=" & iCommodityID & ",POD_DescriptionID=" & iItemID & " and POD_CompID=" & iCompID & " "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    Else
                        iMax = objGenFun.GetMaxID(sNameSpace, iCompID, "Purchase_Order_Details", "POD_ID", "POD_CompID")
                        sSql = "" : sSql = "Insert into Purchase_Order_Details(POD_ID,POD_MasterID,POD_Commodity,POD_DescriptionID,POD_HistoryID,POD_Unit,POD_Rate,POD_Quantity,POD_RateAmount,POD_Discount,POD_DiscountAmount,POD_Excise,POD_ExciseAmount,POD_VAT,POD_VATAmount,POD_CST,POD_CSTAmount,POD_TotalAmount,POD_CompID,POD_Status,POD_CreatedBy,POD_CreatedOn,POD_Operation,POD_IPAddress,POD_Frieght,POD_FrieghtAmount)"
                        sSql = sSql & "Values(" & iMax & "," & iMaxOrderID & "," & iCommodityID & "," & iItemID & "," & iHistoryID & "," & iUnit & "," & dRate & "," & iQuantity & "," & dRateAmount & "," & dDiscount & "," & dDiscountAmount & "," & iExciseID & "," & dExciseAmount & "," & iVATID & "," & dVATAmount & "," & iCSTID & "," & dCSTAmount & "," & dNetAmount & "," & iCompID & ",'W'," & iUserID & ",GetDate(),'C','" & sIPAddress & "',0,0) "
                        objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                    End If
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATZeroID(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc='0' And MAS_Master=14 And MAS_DelFlag='A' And MAS_CompID=" & iCompID & " "
            GetVATZeroID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetVATZeroID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTZeroID(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc='0' And MAS_Master=15 And MAS_DelFlag='A' And MAS_CompID=" & iCompID & " "
            GetCSTZeroID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetCSTZeroID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExciseZeroID(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc='0' And MAS_Master=16 And MAS_DelFlag='A' And MAS_CompID=" & iCompID & " "
            GetExciseZeroID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetExciseZeroID
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
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 POM_ID From Purchase_Order_Master Order By POM_ID Desc")
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
            sStr = "" & sYear & "" & "" & sMonthCode & "" & "" & sSDate & "" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckGoodsExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try

            sSql = "" : sSql = "Select * from Inventory_master where INV_Description ='" & sDesc & "' And INV_CompID=" & iCompID & " "
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckGoodsExistOrNot = dr("INV_Id")
            Else
                CheckGoodsExistOrNot = 0
            End If
            dr.Close()

            Return CheckGoodsExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_ID From Acc_General_Master Where MAS_Desc in(Select INVH_VAT From Inventory_Master_History Where INVH_ID=" & iHistoryID & " And INVH_INV_ID=" & iItemID & " And INVH_CompID=" & iCompID & ") And MAS_Master=14 And MAS_DelFlag='A' And MAS_CompID=" & iCompID & " "
            GetVATID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetVATID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iItemID As Integer, ByVal iHistoryID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select INVH_VAT From Inventory_Master_History Where INVH_ID=" & iHistoryID & " And INVH_INV_ID=" & iItemID & " And INVH_CompID=" & iCompID & " "
            GetVAT = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetVAT
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
