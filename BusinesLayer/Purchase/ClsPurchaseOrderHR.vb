
Imports System
Imports System.Data
Imports DatabaseLayer
Imports System.Data.OleDb
Public Class ClsPurchaseOrderHR
    Dim objDB As New DBHelper
    Dim objFasGnrl As New clsFASGeneral

    Public Function GetPurchaseORderHR(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPomID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sInvenotryCode As String = "", sColour As String = "", sStr As String = "", TotalinWord As String = ""
        Dim iQnt As Integer = 0
        Dim iQnt3 As Integer = 0, iQnt4 As Integer = 0, iQnt5 As Integer = 0, iQnt6 As Integer = 0
        Dim iQnt7 As Integer = 0, iQnt8 As Integer = 0, iQnt9 As Integer = 0, iQnt10 As Integer = 0
        Dim iQnt11 As Integer = 0, iQnt12 As Integer = 0, iQnt0 As Integer = 0
        Dim total As New Double
        Dim dSubTotal As Double = 0, dVatAmount As Double = 0, dCSTAmount As Double = 0, dDiscountAMt As Double = 0
        Try
            dt.Columns.Add("POM_OrderDate")
            dt.Columns.Add("POM_OrderNo")
            dt.Columns.Add("POD_Rate", System.Type.[GetType]("System.Double"))
            dt.Columns.Add("POD_RateAmount")
            dt.Columns.Add("POD_Quantity")
            dt.Columns.Add("POD_Discount")
            dt.Columns.Add("POD_DiscountAmount", System.Type.[GetType]("System.Double"))
            dt.Columns.Add("POD_Excise")
            dt.Columns.Add("POD_ExciseAMount")
            dt.Columns.Add("POD_VAT")
            dt.Columns.Add("POD_VATAMount", System.Type.[GetType]("System.Double"))
            dt.Columns.Add("POD_CST")
            dt.Columns.Add("POD_CSTAmount", System.Type.[GetType]("System.Double"))
            dt.Columns.Add("POD_RequiredDate")
            dt.Columns.Add("CSM_Name")
            dt.Columns.Add("CSM_Code")
            dt.Columns.Add("CSM_Address")
            dt.Columns.Add("CSM_EmailID")
            dt.Columns.Add("CSM_MobileNo")
            dt.Columns.Add("city")
            dt.Columns.Add("States")
            dt.Columns.Add("Inv_Code")
            dt.Columns.Add("Inv_Description")
            dt.Columns.Add("Inv_Color")
            dt.Columns.Add("Inv_Acode")
            dt.Columns.Add("Cust_Name")
            dt.Columns.Add("Cust_code")
            dt.Columns.Add("Cust_Comm_Address")
            dt.Columns.Add("Cust_Pincode")
            dt.Columns.Add("Cust_City")
            dt.Columns.Add("Cust_State")
            dt.Columns.Add("Inv_Size3")
            dt.Columns.Add("Inv_Size4")
            dt.Columns.Add("Inv_Size5")
            dt.Columns.Add("Inv_Size6")
            dt.Columns.Add("Inv_Size7")
            dt.Columns.Add("Inv_Size8")
            dt.Columns.Add("Inv_Size9")
            dt.Columns.Add("Inv_Size10")
            dt.Columns.Add("Inv_Size11")
            dt.Columns.Add("Inv_Size12")
            dt.Columns.Add("Inv_SizeO")
            dt.Columns.Add("SubTotalAmount", System.Type.[GetType]("System.Double"))
            dt.Columns.Add("TotalinWord")
            dt.Columns.Add("TotalQty")
            'sSql = "" : sSql = "Select A.POM_OrderDate, A.POM_OrderNo, A.POM_Supplier, A.POM_MOdeofShipping, "
            'sSql = sSql & "B.POD_Commodity, B.POD_DescriptionID, B.POD_HIstoryID, B.POD_Unit, B.POD_Rate, "
            'sSql = sSql & "B.POD_Quantity, Convert(money,B.POD_RateAmount) as POD_RateAmount, B.POD_Discount, B.POD_DiscountAmount, "
            'sSql = sSql & "B.POD_Excise, B.POD_ExciseAMount, B.POD_VAT, B.POD_VATAMount, "
            'sSql = sSql & "B.POD_CST, B.POD_CSTAmount, B.POD_RequiredDate, B.POD_TotalAmount,"
            'sSql = sSql & "C.CSM_Name, C.CSM_Code, C.CSM_ContactPerson, C.CSM_EmailID, C.CSM_MobileNo,"
            'sSql = sSql & "C.CSM_LandLineNo, C.CSM_FAX, C.CSM_Address, C.CSM_Pincode, C.CSM_City, C.CSM_State, "
            'sSql = sSql & "E.Inv_Code, E.Inv_Description, E.Inv_Size, E.Inv_Color, E.Inv_Acode, F.Mas_Desc, G.Mas_Desc, "
            'sSql = sSql & "H.Cust_Name, H.Cust_code, H.Cust_WebSite, H.Cust_Email, H.Cust_Comm_Address, "
            'sSql = sSql & "H.Cust_City, H.Cust_PIN, H.Cust_State, H.Cust_Country "
            'sSql = sSql & "from purchase_ORder_Master A join purchase_ORder_Details B On A.POM_ID = B.POD_MasterID And B.POD_MasterID = " & iPomID & " and b.POD_Status <> 'D'  "
            'sSql = sSql & "And A.POM_YearID = " & iYearID & " And A.POM_CompID = " & iCompID & " "
            'sSql = sSql & "join CustomerSupplierMaster C On A.POM_Supplier = C.CSM_ID "
            'sSql = sSql & "Join Inventory_Master E On B.POD_DescriptionID = E.Inv_ID "
            'sSql = sSql & "Join Acc_General_Master F On C.CSM_City = F.Mas_id "
            'sSql = sSql & "Join Acc_General_Master G On C.CSM_State = G.Mas_ID "
            'sSql = sSql & "join Mst_Customer_Master H On A.POM_CompID = H.Cust_ID "
            'sSql = sSql & "Order by E.Inv_ACode"

            sSql = "Select POM_OrderNo, Convert(VARCHAR(10), POM_OrderDate, 103)As POM_OrderDate, POM_Supplier, b.POD_Commodity, b.POD_Rate, b.POD_Quantity, "
            sSql = sSql & "b.POD_CST, Convert(money, b.POD_CSTAmount) As POD_CSTAmount, Convert(money, b.POD_RateAmount) As POD_RateAmount,"
            sSql = sSql & "Convert(money, b.POD_Discount)As POD_Discount,Convert(money,b.POD_DiscountAmount) As POD_DiscountAmount,"
            sSql = sSql & "Convert(money,b.POD_TotalAmount)As POD_TotalAmount,"
            sSql = sSql & "Convert(money,b.POD_VAT)As POD_VAT,Convert(money,b.POD_Excise)As POD_Excise,POD_Frieght,POD_FrieghtAmount,POD_ExciseAmount,Convert(money,b.POD_ExciseAmount)As POD_ExciseAmount,Convert(money,b.POD_VATAmount)As POD_VATAmount,b.POD_Unit, "
            sSql = sSql & "c.INV_Code,c.INV_Description,c.Inv_Acode,c.INV_Size,c.Inv_Color,d.CSM_Name,d.CSM_Address,d.CSM_MobileNo,d.CSM_City,d.CSM_State,d.CSM_PinCode,d.CSM_EmailID,e.Mas_Desc,m.CUST_CODE,m.Cust_City,m.Cust_State,m.CUST_Name,m.CUST_COMM_ADDRESS,m.Cust_Pin,m.CUST_EMAIL,m.CUST_COMM_TEL,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate "
            sSql = sSql & "From Purchase_Order_Master "
            sSql = sSql & "join Purchase_Order_Details b On POM_ID=" & iPomID & " And POM_ID=b.POD_MasterID And b.POD_Status <> 'D' "
            sSql = sSql & "Join Inventory_master_history InvH on  POD_HistoryID=InvH.InvH_ID "
            sSql = sSql & "Join Inventory_master c on  POD_DescriptionID=c.INV_ID "
            sSql = sSql & "Join CustomerSupplierMaster d On POM_Supplier=d.CSM_ID "
            sSql = sSql & "Join Acc_General_master e on b.POD_Unit=e.Mas_ID "
            sSql = sSql & "Join MST_CUSTOMER_MASTER m on b.POD_CompID=m.CUST_ID "
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1

                    If sInvenotryCode.Contains(dtDetails.Rows(i)("Inv_Description").ToString()) = False Then
                        GoTo AddNewITem
                    Else
                        GoTo 124
                    End If

AddNewITem:         dRow = dt.NewRow()

                    If sInvenotryCode.Contains(dtDetails.Rows(i)("Inv_Description").ToString()) = False Then
                        dRow("Inv_Acode") = dtDetails.Rows(i)("Inv_Acode").ToString()
                        sInvenotryCode = dtDetails.Rows(i)("Inv_Description").ToString()
                        iQnt = 0 : iQnt3 = 0 : iQnt4 = 0 : iQnt5 = 0 : iQnt6 = 0 : iQnt7 = 0
                        iQnt8 = 0 : iQnt9 = 0 : iQnt10 = 0 : iQnt11 = 0 : iQnt12 = 0 : iQnt0 = 0
                        dVatAmount = 0 : dCSTAmount = 0 : dDiscountAMt = 0

                        If IsDBNull(dtDetails.Rows(i)("Inv_Color").ToString()) = False Then
                            dRow("Inv_Color") = dtDetails.Rows(i)("Inv_Color").ToString()
                            sColour = dtDetails.Rows(i)("Inv_Color").ToString()
                        End If

                        dRow("Inv_Description") = objFasGnrl.SafeSQL(dtDetails.Rows(i)("Inv_Description").ToString())
                        dRow("POD_Rate") = dtDetails.Rows(i)("POD_Rate").ToString()
                    End If
124:
                    Try
                        dRow("POM_OrderNo") = objFasGnrl.SafeSQL(dtDetails.Rows(i)("POM_OrderNo").ToString())
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dtDetails.Rows(i)("POM_OrderDate").ToString()) = False Then
                            dRow("POM_OrderDate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate").ToString(), "D")
                        End If
                    Catch ex As Exception
                    End Try

                    If IsDBNull(dtDetails.Rows(i)("POM_OrderNo").ToString()) = False Then
                        dRow("POM_OrderNo") = dtDetails.Rows(i)("POM_OrderNo").ToString()
                    End If

                    If IsDBNull(dtDetails.Rows(i)("CSM_Name").ToString()) = False Then
                        dRow("CSM_Name") = dtDetails.Rows(i)("CSM_Name").ToString()
                    End If

                    If IsDBNull(dtDetails.Rows(i)("CSM_Address").ToString()) = False Then
                        Dim sAddress As String = dtDetails.Rows(i)("CSM_Address").ToString()
                        Dim sCity As String = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc from Acc_General_Master where Mas_id =" & dtDetails.Rows(i)("CSM_City").ToString() & "")
                        Dim sState As String = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc from Acc_General_Master where Mas_id =" & dtDetails.Rows(i)("CSM_State").ToString() & "")
                        dRow("CSM_Address") = sAddress & System.Environment.NewLine & sCity & " - " & dtDetails.Rows(i)("CSM_PinCode").ToString() & System.Environment.NewLine & sState
                    End If

                    If IsDBNull(dtDetails.Rows(i)("Cust_Name").ToString()) = False Then
                        dRow("Cust_Name") = dtDetails.Rows(i)("Cust_Name").ToString()
                    End If

                    If IsDBNull(dtDetails.Rows(i)("Cust_Comm_Address").ToString()) = False Then
                        Dim sAddress As String = dtDetails.Rows(i)("Cust_Comm_Address").ToString()
                        Dim sCity As String = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc from Acc_General_Master where Mas_id =" & dtDetails.Rows(i)("Cust_City").ToString() & "")
                        Dim sState As String = objDB.SQLGetDescription(sNameSpace, "Select Mas_Desc from Acc_General_Master where Mas_id =" & dtDetails.Rows(i)("Cust_State").ToString() & "")
                        dRow("Cust_Comm_Address") = sAddress & System.Environment.NewLine & sCity & " - " & dtDetails.Rows(i)("Cust_Pin").ToString() & System.Environment.NewLine & sState
                    End If
                    If IsDBNull(dtDetails.Rows(i)("INV_Size").ToString()) = False Then
                        Select Case dtDetails.Rows(i)("INV_Size").ToString()
                            Case 3
                                iQnt3 = iQnt3 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size3") = iQnt3
                                iQnt = iQnt + iQnt3
                            Case 34
                                iQnt3 = iQnt3 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size3") = iQnt3
                                iQnt = iQnt + iQnt3
                            Case 4
                                iQnt4 = iQnt4 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size4") = iQnt4
                                iQnt = iQnt + iQnt4
                            Case 36
                                iQnt4 = iQnt4 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size4") = iQnt4
                                iQnt = iQnt + iQnt4
                            Case 5
                                iQnt5 = iQnt5 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size5") = iQnt5
                                iQnt = iQnt + iQnt5

                            Case 37
                                iQnt5 = iQnt5 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size5") = iQnt5
                                iQnt = iQnt + iQnt5

                            Case 6
                                iQnt6 = iQnt6 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size6") = iQnt6
                                iQnt = iQnt + iQnt6

                            Case 39
                                iQnt6 = iQnt6 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size6") = iQnt6
                                iQnt = iQnt + iQnt6

                            Case 7
                                iQnt7 = iQnt7 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size7") = iQnt7
                                iQnt = iQnt + iQnt7

                            Case 40
                                iQnt7 = iQnt7 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size7") = iQnt7
                                iQnt = iQnt + iQnt7

                            Case 8
                                iQnt8 = iQnt8 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size8") = iQnt8
                                iQnt = iQnt + iQnt8

                            Case 41
                                iQnt8 = iQnt8 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size8") = iQnt8
                                iQnt = iQnt + iQnt8

                            Case 9
                                iQnt9 = iQnt9 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size9") = iQnt9
                                iQnt = iQnt + iQnt9

                            Case 42
                                iQnt9 = iQnt9 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size9") = iQnt9
                                iQnt = iQnt + iQnt9

                            Case 10
                                iQnt10 = iQnt10 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size10") = iQnt10
                                iQnt = iQnt + iQnt10

                            Case 43
                                iQnt10 = iQnt10 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size10") = iQnt10
                                iQnt = iQnt + iQnt10

                            Case 11
                                iQnt11 = iQnt11 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size11") = iQnt11
                                iQnt = iQnt + iQnt11

                            Case 44
                                iQnt11 = iQnt11 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size11") = iQnt11
                                iQnt = iQnt + iQnt11

                            Case 12
                                iQnt12 = iQnt12 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size12") = iQnt12
                                iQnt = iQnt + iQnt12

                            Case 45
                                iQnt12 = iQnt12 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_Size12") = iQnt12
                                iQnt = iQnt + iQnt12
                            Case 0
                                iQnt0 = iQnt0 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_SizeO") = iQnt0
                                iQnt = iQnt + iQnt0
                            Case ""
                                iQnt0 = iQnt0 + dtDetails.Rows(i)("POD_Quantity").ToString()
                                dRow("Inv_SizeO") = iQnt0
                                iQnt = iQnt + iQnt0
                        End Select
                    End If
                    Try
                        If sInvenotryCode.Contains(dtDetails.Rows(i)("Inv_Description").ToString()) = True Then
                            dRow("TotalQty") = iQnt
                            dRow("POD_RateAmount") = String.Format("{0:0.00}", Convert.ToDecimal(iQnt * dtDetails.Rows(i)("POD_Rate").ToString()))
                            dRow("SubTotalAmount") = dSubTotal + Convert.ToDecimal(dRow("POD_RateAmount"))
                            total = total + (Convert.ToDecimal(dRow("POD_RateAmount")))
                            If dtDetails.Rows(i)("POD_VATAMount").ToString() <> "" Then
                                dVatAmount = dVatAmount + dtDetails.Rows(i)("POD_VATAMount").ToString()
                                dRow("POD_VATAMount") = dVatAmount
                            Else
                                dVatAmount = dVatAmount + 0.00
                            End If
                            '  total = total + (dtDetails.Rows(i)("POD_VATAMount").ToString())
                            If dtDetails.Rows(i)("POD_CSTAmount").ToString() <> "" Then
                                dCSTAmount = dCSTAmount + dtDetails.Rows(i)("POD_CSTAmount").ToString()
                                dRow("POD_CSTAmount") = dCSTAmount
                            Else
                                dCSTAmount = dCSTAmount + 0.00
                            End If
                            total = total + (dCSTAmount + dtDetails.Rows(i)("POD_VATAMount").ToString())
                            If dtDetails.Rows(i)("POD_DiscountAmount").ToString() <> "" Then
                                dDiscountAMt = dDiscountAMt + dtDetails.Rows(i)("POD_DiscountAmount").ToString()
                                dRow("POD_DiscountAmount") = dDiscountAMt
                            Else
                                dDiscountAMt = dDiscountAMt + 0
                            End If
                            dRow("TotalinWord") = total 'NumberToWord(String.Format("{0:0.00}", total)) & " Only"
                            dt.Rows.Add(dRow)
                        End If
                    Catch ex As Exception
                    End Try
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCompanyMasterTemplete(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "select distinct a.Cmp_desc,a.Cmp_Value from Company_Accounting_Template a,print_settings p where (a.CMP_Desc='PAN' and p.PS_CPAN=1 and p.PS_Status='P') or (a.CMP_Desc='TAN' and p.PS_CTAN=1 and p.PS_Status='P')"
            sSql = sSql & " or (a.CMP_Desc ='VAT' and p.PS_CVAT=1 and p.PS_Status='P') or (a.CMP_Desc='TAX' and p.PS_CTAX=1 and p.PS_Status='P' and p.PS_Status='P') or (a.CMP_Desc='TIN' and p.PS_CTIN=1 and p.PS_Status='P') or (a.CMP_Desc='CIN' and p.PS_CTIN=1 and p.PS_Status='P') and p.PS_Status='P' "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SupplierDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal isupplier As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CST_ID,CST_SupplierID,CST_Description,CST_Value,CST_CompID,CST_Status from  Customer_Supplier_Template where  CST_SupplierID=" & isupplier & " And CST_CompID=" & iCompID & " "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SupplierMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select * from Purchase_Order_Master Join customerSupplierMaster b On POM_Supplier=b.CSM_ID where  POM_ID=" & iorder & " And POM_CompID=" & iCompID & " "
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
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
        Dim Total, TotalAmt, Totaltax As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtdiscount, gtdiscountAmt, GrandTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Try
            dt.Columns.Add("POM_OrderNo")
            dt.Columns.Add("POM_OrderDate")
            dt.Columns.Add("POM_Supplier")
            dt.Columns.Add("POD_Commodity")
            dt.Columns.Add("Colour")
            dt.Columns.Add("t0")
            dt.Columns.Add("t3")
            dt.Columns.Add("t4")
            dt.Columns.Add("t5")
            dt.Columns.Add("t6")
            dt.Columns.Add("t7")
            dt.Columns.Add("t8")
            dt.Columns.Add("t9")
            dt.Columns.Add("t10")
            dt.Columns.Add("t11")
            dt.Columns.Add("t12")
            dt.Columns.Add("TotalQty")
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
            sSql = sSql & "c.INV_Code,c.INV_Description,d.CSM_Name,d.CSM_Address,d.CSM_MobileNo,d.CSM_EmailID,e.Mas_Desc,m.CUST_CODE,m.CUST_COMM_ADDRESS,m.CUST_EMAIL,m.CUST_COMM_TEL,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate "
            sSql = sSql & "From Purchase_Order_Master "
            '  sSql = sSql & "join Purchase_Order_Details b On POM_ID=" & iPomID & " And POM_ID=b.POD_MasterID And b.POD_Status <> 'D' "
            sSql = sSql & "Join Inventory_master_history InvH on  POD_HistoryID=InvH.InvH_ID "
            sSql = sSql & "Join Inventory_master c on  POD_DescriptionID=c.INV_ID "
            sSql = sSql & "Join CustomerSupplierMaster d On POM_Supplier=d.CSM_ID "
            sSql = sSql & "Join Acc_General_master e on b.POD_Unit=e.Mas_ID "
            sSql = sSql & "Join MST_CUSTOMER_MASTER m on b.POD_CompID=m.CUST_ID "
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                TotalAmt = 0
                Totaltax = 0
                If IsDBNull(dtDetails.Rows(i)("PIA_Commodity")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                End If

                If IsDBNull(dtDetails.Rows(i)("PIA_DescriptionID")) = False Then
                    dRow("SlNo") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then


                        If ((dtDetails.Rows(i)("Inv_Size").ToString() = "0") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "")) Then
                            dRow("t0") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t0") = 0
                        End If

                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "3") Then
                            dRow("t3") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t3") = 0
                        End If

                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "4") Then
                            dRow("t4") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t4") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "5") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "39") Then
                            dRow("t5") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t5") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "6") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "40") Then
                            dRow("t6") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t6") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "7") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "41") Then
                            dRow("t7") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t7") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "8") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "42") Then
                            dRow("t8") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t8") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "9") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "43") Then
                            dRow("t9") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t9") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "10") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "44") Then
                            dRow("t10") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t10") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "11") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "45") Then
                            dRow("t11") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t11") = 0
                        End If
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                    dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                    gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                Else
                    dRow("TotalQty") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIA_MRP")) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("PIA_MRP")
                    gtMRP = gtMRP + dtDetails.Rows(i)("PIA_MRP")
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                    dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                    gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                Else
                    dRow("VAT") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_VATAmount")))
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If


                If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                    dRow("CST") = dtDetails.Rows(i)("POD_CST")
                    gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                Else
                    dRow("CST") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                    gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                Else
                    dRow("CSTAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                    gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                Else
                    dRow("Exise") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                Else
                    dRow("ExiseAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False Then
                    dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                    gtdiscount = gtdiscount + dRow("Discount")
                Else
                    dRow("Discount") = "0"
                End If



                If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                    dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                    gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                Else
                    dRow("DiscountAmt") = "0"
                End If


                TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(TotalAmt))
                GrandTotal = GrandTotal + TotalAmt

                dt.Rows.Add(dRow)
            Next

            dtDetails.Clear()

            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,PV_DocRefNo,
                                b.PIE_ID,b.PIE_OrderID,b.PIE_GINID,b.PIE_CommodityID,b.PIE_Description,b.PIE_HistoryID,b.PIE_UnitID,b.PIE_Rate,b.PIE_Quantity,b.PIE_RateAmount,
                                b.PIE_Discount,b.PIE_DiscountAmount,b.PIE_Excise,b.PIE_ExciseAmount,b.PIE_Vat,b.PIE_VatAmount,b.PIE_TotalAmount,b.PIE_AcceptQty,b.PIE_DocRef,
                            	c.Inv_Description,c.Inv_Color,c.Inv_Size,
                              	d.Inv_Description Commodity	
                                from Purchase_verification
	                            join Purchase_Invoice_Excess b on PV_DocRefNo=b.PIE_DocRef
	                            join Inventory_Master c on b.PIE_Description=c.Inv_ID
                            	join Inventory_Master d on b.PIE_CommodityID=d.Inv_ID
                            	join Purchase_GIN_Master e on PV_GinNo=e.PGM_ID 
                            	join Purchase_Order_Master f on PV_OrderNo =f.POM_ID"

            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo= " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIE_ID,b.PIE_Description"
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                Total = 0
                TotalAmt = 0
                Totaltax = 0
                If IsDBNull(dtDetails.Rows(i)("PIE_CommodityID")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Description")) = False Then
                    dRow("SlNo") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("PIE_AcceptQty")) = False Then


                        If ((dtDetails.Rows(i)("Inv_Size").ToString() = "0") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "")) Then
                            dRow("t0") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("t0") = 0
                        End If

                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "3") Then
                            dRow("t3") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If

                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "4") Then
                            dRow("t4") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "5") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "39") Then
                            dRow("t5") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "6") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "40") Then
                            dRow("t6") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "7") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "41") Then
                            dRow("t7") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "8") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "42") Then
                            dRow("t8") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "9") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "43") Then
                            dRow("t9") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "10") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "44") Then
                            dRow("t10") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                        If (dtDetails.Rows(i)("Inv_Size").ToString() = "11") Or (dtDetails.Rows(i)("Inv_Size").ToString() = "45") Then
                            dRow("t11") = dtDetails.Rows(i)("PIE_AcceptQty")
                        End If
                    End If
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_AcceptQty")) = False Then
                    dRow("TotalQty") = dtDetails.Rows(i)("PIE_AcceptQty")
                    gtQty = gtQty + dtDetails.Rows(i)("PIE_AcceptQty")
                Else
                    dRow("TotalQty") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Rate")) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("PIE_Rate")
                    gtMRP = gtMRP + dtDetails.Rows(i)("PIE_Rate")
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Vat")) = False Then
                    dRow("VAT") = dtDetails.Rows(i)("PIE_Vat")
                    gtVAT = gtVAT + dtDetails.Rows(i)("PIE_Vat")
                Else
                    dRow("VAT") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PIE_VatAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("PIE_VatAmount")))
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If


                'If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                '    dRow("CST") = dtDetails.Rows(i)("POD_CST")
                '    gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                'Else
                dRow("CST") = 0
                'End If

                'If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                '    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                '    gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                '    Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                'Else
                dRow("CSTAmt") = 0
                'End If

                If IsDBNull(dtDetails.Rows(i)("PIE_Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("PIE_Excise")
                    gtExise = gtExise + dtDetails.Rows(i)("PIE_Excise")
                Else
                    dRow("Exise") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("PIE_ExciseAmount")))
                    gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("PIE_ExciseAmount")
                    Totaltax = Totaltax + dtDetails.Rows(i)("PIE_ExciseAmount")
                Else
                    dRow("ExiseAmt") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_Discount")) = False And dtDetails.Rows(i)("PIE_Discount") <> "" Then
                    dRow("Discount") = dtDetails.Rows(i)("PIE_Discount")
                    gtdiscount = gtdiscount + dRow("Discount")
                Else
                    dRow("Discount") = "0"
                End If
                If IsDBNull(dtDetails.Rows(i)("PIE_DiscountAmount")) = False And dtDetails.Rows(i)("PIE_DiscountAmount") <> "" Then
                    dRow("DiscountAmt") = dtDetails.Rows(i)("PIE_DiscountAmount")
                    gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                Else
                    dRow("DiscountAmt") = "0"
                End If
                TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(TotalAmt))
                GrandTotal = GrandTotal + TotalAmt
                dt.Rows.Add(dRow)
            Next
            dr = dt.NewRow()
            dr("Commodity") = <b>Total</b>
            dr("Description") = <b>Total</b>
            dr("TotalQty") = gtQty
            dr("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(gtMRP))
            dr("VAT") = gtVAT
            dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
            dr("CST") = gtCST
            dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
            dr("Exise") = gtExise
            dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtExiseAmt))
            dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscount))
            dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscountAmt))
            dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandTotal))
            dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetVendorTemplete(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = " Select distinct a.CST_Description,a.CST_Value from Customer_Supplier_Template a,print_settings p where (a.CST_Description='PAN' and p.PS_BPAN=1) or "
            sSql = sSql & " (a.CST_Description ='TAN' and p.PS_BTAN=1 and p.PS_Status='P') or  (a.CST_Description='VAT' and p.PS_BVAT=1 and p.PS_Status='P')"
            sSql = sSql & " Or (a.CST_Description ='TAX' and p.PS_BTAX=1 and p.PS_Status='P') or (a.CST_Description='TIN' and p.PS_BTIN=1 and p.PS_Status='P') or (a.CST_Description='CIN' and p.PS_BTIN=1 and p.PS_Status='P') and p.PS_Status='P' and CST_SupplierID in(Select POM_Supplier from purchase_ORder_Master where POM_ID =" & iOrderID & ")"
            'sSql = "" : sSql = "Select * from Customer_Supplier_Template where CST_SupplierID In(Select POM_Supplier from purchase_ORder_Master where POM_ID =" & iOrderID & ")"
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
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
                    words += " And " + aftrdecimalWord

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
    Public Function GetAccessCode(ByVal sAccessName As String)
        Dim sSql As String
        Dim sAccessCode As String
        Try
            sSql = "Select SAD_CMS_AccessCode from Sad_CompanyMaster_Settings"
            sAccessCode = objDB.SQLExecuteScalar(sAccessName, sSql)
            Return sAccessCode
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridOtherDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("sID")
            dt.Columns.Add("Statutory Name")
            dt.Columns.Add("Statutory Value")

            sSql = "Select Cmp_PKID,Cmp_Desc,Cmp_Value,Cmp_Status,Cmp_ID from Company_Accounting_Template Where Cmp_ID=" & iCompID & " Order by Cmp_Desc"
            dr = objDB.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("sID") = dr("Cmp_PKID")
                    dRow("Statutory Name") = dr("Cmp_Desc")
                    dRow("Statutory Value") = dr("Cmp_Value")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCode As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from MST_Customer_Master where Cust_Code = '" & sCode & "' and Cust_Delflg <> 'D' and CUST_CompID=" & iCompID & ""
            dt = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Invoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer) As DataTable
        Dim sSql As String = ""
        Try
            If iorder <> 0 Then
                sSql = "" : sSql = "select PV_ID, PV_BillNo from Purchase_verification where PV_OrderNo=" & iorder & " and PV_CompID=" & iCompID & " Order By PV_ID "
            Else
                sSql = "" : sSql = "select PV_ID, PV_BillNo from Purchase_verification where PV_CompID=" & iCompID & " Order By PV_ID "
            End If

            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RemoveDublicateItemVerified(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("Description"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("Description"), String.Empty)
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

    Public Function Order(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_Status='A' And POM_CompID=" & iCompID & " Order By POM_ID desc"

            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function RemoveDublicateCST(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("CST"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("CST"), String.Empty)
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
    Public Function RemoveDublicateVAT(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("VAT"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("VAT"), String.Empty)
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

    Public Function loadDetailsPOVerified(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
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
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtExAmt, gtdiscount, gtdiscountAmt, GrandTotal, subTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("CSTAmtTotal")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("TotalExiseAmt")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("SubTotal")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalVat")
            dt.Columns.Add("UnitId")
            dt.Columns.Add("AltUnit")
            dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("INVH_Mdate")
            dt.Columns.Add("BatchNumber")
            dt.Columns.Add("POM_OrderDate")
            dt.Columns.Add("PGM_DocumentRefNo")
            dt.Columns.Add("PGM_InvoiceDate")
            dt.Columns.Add("gtdiscountAmt")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("CalculateAmount")

            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_ESugamNo,e.PGM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,"
            sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_AcceptedQnt,b.PIA_MRP,b.PIA_Status,"
            sSql = sSql & " b.PIA_CompID,b.PIA_Excess,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PGD_BatchNumber,h.PGD_ManufactureDate,h.PGD_ExpireDate,"
            sSql = sSql & " d.Inv_Description Commodity,"
            sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
            sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_GSTRate,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate"
            sSql = sSql & "  from Purchase_verification"
            sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            sSql = sSql & " join Inventory_Master_history InvH On b.PIA_HistoryID=InvH.InvH_ID"
            sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID "
            sSql = sSql & "  join Purchase_GIN_Details h On b.PIA_GINID=h.PGD_MasterID And b.PIA_OrderID=h.PGD_OrderID and PIA_HistoryID=h.PGD_HistoryID where b.PIA_CompID=" & iCompID & ""
            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo= " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    gtExAmt = 0
                    If IsDBNull(dtDetails.Rows(i)("PIA_Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PIA_DescriptionID")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                        gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_MRP")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("PIA_MRP")
                        gtMRP = gtMRP + dtDetails.Rows(i)("PIA_MRP")
                    Else
                        dRow("Rate") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = String.Format("{0:0.000}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) * dtDetails.Rows(i)("POD_Discount")) / 100))
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                    Else
                        dRow("VAT") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                        dRow("ExiseAmt") = String.Format("{0:0.000}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        gtExAmt = dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                        gtExAmt = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.000}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_VAT") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.000}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_CST") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        gtCSTAmt = gtCSTAmt + dRow("CSTAmt")
                        Totaltax = Totaltax + dRow("CSTAmt")
                    Else
                        dRow("CSTAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If

                    If (dtDetails.Rows(i)("PGM_InvoiceDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PGM_InvoiceDate")) = False Then
                            dRow("PGM_InvoiceDate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate").ToString(), "D")
                        Else
                            dRow("PGM_InvoiceDate") = ""
                        End If
                    Else
                        dRow("PGM_InvoiceDate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PGM_DocumentRefNo")) = False Then
                        dRow("PGM_DocumentRefNo") = dtDetails.Rows(i)("PGM_DocumentRefNo")
                    End If

                    If (dtDetails.Rows(i)("PGD_ManufactureDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PGD_ManufactureDate")) = False Then
                            dRow("INVH_Mdate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ManufactureDate").ToString(), "D")
                        Else
                            dRow("INVH_Mdate") = ""
                        End If
                    Else
                        dRow("INVH_Mdate") = ""
                    End If

                    If (dtDetails.Rows(i)("POM_OrderDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                            dRow("POM_OrderDate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate").ToString(), "D")
                        Else
                            dRow("POM_OrderDate") = ""
                        End If
                    Else
                        dRow("POM_OrderDate") = ""
                    End If
                    If (dtDetails.Rows(i)("PGD_ExpireDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PGD_ExpireDate")) = False Then
                            dRow("INVH_Edate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ExpireDate").ToString(), "D")
                        Else
                            dRow("INVH_Edate") = ""
                        End If
                    Else
                        dRow("INVH_Edate") = ""
                    End If

                    If (dRow("INVH_Edate").ToString() = "30/12/1899") Then
                        dRow("INVH_Edate") = ""
                    End If
                    If (dRow("INVH_Mdate").ToString() = "30/12/1899") Then
                        dRow("INVH_Mdate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
                        dRow("INVH_MRP") = dtDetails.Rows(i)("INVH_MRP")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PGD_BatchNumber")) = False Then
                        dRow("BatchNumber") = dtDetails.Rows(i)("PGD_BatchNumber")
                    End If

                    dRow("GSTRate") = dtDetails.Rows(i)("POD_GSTRate")
                    dRow("CalculateAmount") = String.Format("{0:0.000}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
                    dRow("GSTAmount") = String.Format("{0:0.000}", Convert.ToDecimal((dRow("CalculateAmount") * dRow("GSTRate")) / 100))

                    TotalAmt = Totaltax + (Convert.ToDecimal(dRow("CalculateAmount")) + Convert.ToDecimal(dRow("GSTAmount")))
                    dRow("TotalAmount") = String.Format("{0:0.000}", Convert.ToDecimal(Convert.ToDecimal(dRow("CalculateAmount")) + Convert.ToDecimal(dRow("GSTAmount"))))
                    GrandTotal = GrandTotal + TotalAmt

                    subTotal = subTotal + (Convert.ToDecimal(dRow("CalculateAmount")) + Convert.ToDecimal(dRow("GSTAmount")))
                    dRow("SubTotal") = String.Format("{0:0.000}", subTotal)
                    dRow("TotalVat") = String.Format("{0:0.000}", TotalVat)
                    dRow("CSTAmtTotal") = String.Format("{0:0.000}", gtCSTAmt)
                    dRow("GrandTotal") = String.Format("{0:0.000}", Convert.ToDecimal(subTotal + TotalVat + gtCSTAmt + gtExiseAmt))
                    dRow("gtdiscountAmt") = String.Format("{0:0.000}", gtdiscountAmt)
                    dRow("TotalExiseAmt") = String.Format("{0:0.000}", gtExiseAmt)

                    dt.Rows.Add(dRow)
                End If
            Next
            dtDetails.Clear()
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,PV_DocRefNo,
                                b.PIE_ID,b.PIE_OrderID,b.PIE_GINID,b.PIE_CommodityID,b.PIE_Description,b.PIE_HistoryID,b.PIE_UnitID,b.PIE_Rate,b.PIE_Quantity,b.PIE_RateAmount,
                                b.PIE_Discount,b.PIE_DiscountAmount,b.PIE_Excise,b.PIE_ExciseAmount,b.PIE_Vat,b.PIE_VatAmount,b.PIE_TotalAmount,b.PIE_AcceptQty,b.PIE_DocRef,
                            	c.Inv_Description,c.Inv_Color,c.Inv_Size,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,
                              	d.Inv_Description Commodity	
                                from Purchase_verification
	                            join Purchase_Invoice_Excess b on PV_DocRefNo=b.PIE_DocRef
                                join Inventory_Master_history InvH On b.PIE_HistoryID=InvH.InvH_ID
                                Join Inventory_Master c on b.PIE_Description=c.Inv_ID
                            	join Inventory_Master d on b.PIE_CommodityID=d.Inv_ID
                            	join Purchase_GIN_Master e on PV_GinNo=e.PGM_ID 
                            	join Purchase_Order_Master f on PV_OrderNo =f.POM_ID"
            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo= " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIE_ID,b.PIE_Description"
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    If IsDBNull(dtDetails.Rows(i)("Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Description")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> "") Then
                            dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                            gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("TotalQty") = 0
                        End If
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIE_Rate")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("POD_Rate")
                        gtMRP = gtMRP + dtDetails.Rows(i)("POD_Rate")
                    Else
                        dRow("Rate") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VATAmount")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VATAmount")
                    Else
                        dRow("VAT") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Vat")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_Vat")))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                        gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                    Else
                        dRow("CSTAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False And dtDetails.Rows(i)("POD_ExciseAmount") <> "" Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False And dtDetails.Rows(i)("POD_Discount") <> "" Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If
                    subTotal = subTotal + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    TotalAmt = Totaltax + ((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt"))
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = subTotal
                    dRow("TotalVat") = TotalVat
                    dRow("CSTAmtTotal") = gtCSTAmt
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal((subTotal + TotalVat + gtCSTAmt + gtExiseAmt) - gtdiscountAmt))
                    dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dt.Rows.Add(dRow)
                End If
            Next

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadDetailsBGST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer, ByVal iInvoice As Integer) As DataTable
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
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtExAmt, gtdiscount, gtdiscountAmt, GrandTotal, subTotal As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0
        Dim flag3 As Integer = 0
        Dim gtChargePerItem, gtGSTRate, gtGSTAmount, gtSGST, gtSGSTAmount, gtCGST, gtCGSTAmount, gtIGST, gtIGSTAmount As Double
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("ChargePerItem")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("CSTAmtTotal")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("TotalExiseAmt")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")

            dt.Columns.Add("Amount")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("GSTAmountTotal")
            dt.Columns.Add("SGST")
            dt.Columns.Add("SGSTAmount")
            dt.Columns.Add("SGSTAmountTotal")
            dt.Columns.Add("CGST")
            dt.Columns.Add("CGSTAmount")
            dt.Columns.Add("CGSTAmountTotal")
            dt.Columns.Add("IGST")
            dt.Columns.Add("IGSTAmount")
            dt.Columns.Add("IGSTAmountTotal")

            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("ItemCode")
            dt.Columns.Add("SubTotal")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalVat")
            dt.Columns.Add("UnitId")
            dt.Columns.Add("AltUnit")
            dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("INVH_Mdate")
            dt.Columns.Add("BatchNumber")
            dt.Columns.Add("POM_OrderDate")
            dt.Columns.Add("PGM_DocumentRefNo")
            dt.Columns.Add("PGM_InvoiceDate")
            dt.Columns.Add("gtdiscountAmt")
            dt.Columns.Add("CalculateAmount")
            dt.Columns.Add("HSNCode")

            'sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_ESugamNo,e.PGM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,"
            'sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_AcceptedQnt,b.PIA_MRP,b.PIA_Status,"
            'sSql = sSql & " b.PIA_CompID,b.PIA_Excess,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PGD_BatchNumber,h.PGD_ManufactureDate,h.PGD_ExpireDate,"
            'sSql = sSql & " d.Inv_Description Commodity,"
            'sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,"
            'sSql = sSql & " g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,g.POD_Frieght,g.POD_GSTRate,g.POD_GSTAmount,g.POD_SGST,g.POD_SGSTAmount,g.POD_CGST,g.POD_CGSTAmount,g.POD_IGST,g.POD_IGSTAmount,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate"
            'sSql = sSql & "  from Purchase_verification"
            'sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            'sSql = sSql & " join Inventory_Master_history InvH On b.PIA_HistoryID=InvH.InvH_ID"
            'sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            'sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            'sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            'sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            'sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID "
            'sSql = sSql & "  join Purchase_GIN_Details h On b.PIA_GINID=h.PGD_MasterID And b.PIA_OrderID=h.PGD_OrderID and PIA_HistoryID=h.PGD_HistoryID where b.PIA_CompID=" & iCompID & ""

            'sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,e.PGM_DocumentRefNo,e.PGM_ESugamNo,e.PGM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,PIM_PRegesterID,b.PID_MasterID,b.PID_CommodityID,b.PID_DescID,b.PID_HistoryID,b.PID_UnitID,b.PID_Quantity,b.PID_Rate,b.PID_ChargePerItem,b.PID_Discount,b.PID_DiscountAmount,b.PID_GSTRate,b.PID_GSTAmount,b.PID_SGST,b.PID_SGSTAmount,b.PID_CGST,b.PID_CGSTAmount,b.PID_IGST,b.PID_IGSTAmount,b.PID_Status,b.PID_CompID,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PGD_BatchNumber,h.PGD_ManufactureDate,h.PGD_ExpireDate, d.Inv_Description Commodity, g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount, g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate  "
            'sSql = sSql & " From Purchase_verification  "
            'sSql = sSql & " Join Purchase_Invoice_Master On PV_InvoiceID=PIM_ID "
            'sSql = sSql & " Join PI_Accepted_Details b On b.PID_MasterID=PIM_ID "
            'sSql = sSql & " Join Inventory_Master_history InvH On b.PID_HistoryID=InvH.InvH_ID "
            'sSql = sSql & " Join Inventory_Master c On b.PID_DescID=c.Inv_ID "
            'sSql = sSql & " Join Inventory_Master d On b.PID_CommodityID=d.Inv_ID "
            'sSql = sSql & " Left Join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            'sSql = sSql & " Join Purchase_Order_Master f On PV_OrderNo =f.POM_ID "
            'sSql = sSql & " Join Purchase_Order_Details g On  PID_HistoryID=g.POD_HistoryID And PIM_OrderID=g.POD_MAsterID "
            'sSql = sSql & " Left Join Purchase_GIN_Details h On PIM_PRegesterID=h.PGD_MasterID And PIM_OrderID=h.PGD_OrderID and b.PID_HistoryID=h.PGD_HistoryID "
            'sSql = sSql & " where b.PID_CompID=" & iCompID & " "

            '*** Commented Bcz not displaying for same item multiple times ***'
            'sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PRM_RegistryNo,e.PRM_DocumentRefNo,e.PRM_ESugamNo,e.PRM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,PIM_PRegesterID,b.PID_MasterID,b.PID_CommodityID,b.PID_DescID,b.PID_HistoryID,b.PID_UnitID,b.PID_Quantity,b.PID_Rate,b.PID_ChargePerItem,b.PID_Discount,b.PID_DiscountAmount,b.PID_GSTRate,b.PID_GSTAmount,b.PID_SGST,b.PID_SGSTAmount,b.PID_CGST,b.PID_CGSTAmount,b.PID_IGST,b.PID_IGSTAmount,b.PID_Status,b.PID_CompID,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PRD_BatchNumber,h.PRD_ManufactureDate,h.PRD_ExpireDate, d.Inv_Description Commodity, g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount, g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,i.GST_CHST   
            '                    From Purchase_verification   
            '                    Join Purchase_Invoice_Master On PV_InvoiceID=PIM_ID  
            '                    Join PI_Accepted_Details b On b.PID_MasterID=PIM_ID  
            '                    Join Inventory_Master_history InvH On b.PID_HistoryID=InvH.InvH_ID  
            '                    Join Inventory_Master c On b.PID_DescID=c.Inv_ID  
            '                    Join Inventory_Master d On b.PID_CommodityID=d.Inv_ID  
            '                    Join Purchase_Registry_Master e On PV_GinNo=e.PRM_ID  
            '                    Join Purchase_Order_Master f On PV_OrderNo =f.POM_ID  
            '                    Join Purchase_Order_Details g On  PID_HistoryID=g.POD_HistoryID And PIM_OrderID=g.POD_MAsterID  
            '                    Join Purchase_Registry_Details h On PIM_PRegesterID=h.PRD_MasterID And PIM_OrderID=h.PRD_OrderNo and b.PID_HistoryID=h.PRD_HistoryID 
            '                    Join GST_Rates i on i.GST_CommodityID=b.PID_CommodityID And i.GST_ItemID=b.PID_DescID And i.GST_ID=b.PID_GSTID "
            'sSql = sSql & " where b.PID_CompID=" & iCompID & " "
            '*** Commented Bcz not displaying for same item multiple times ***'

            sSql = "" : sSql = "select Distinct(b.PID_Quantity),ISNULL(PIM_PRegesterID,0) As PIM_PRegesterID,PV_ID,PV_OrderNo,PV_GinNo,PV_CompID,PV_Status,PV_BillNo,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,b.PID_MasterID,b.PID_CommodityID,b.PID_DescID,b.PID_HistoryID,b.PID_UnitID,b.PID_Rate,b.PID_ChargePerItem,b.PID_Discount,b.PID_DiscountAmount,b.PID_GSTRate,b.PID_GSTAmount,b.PID_SGST,b.PID_SGSTAmount,b.PID_CGST,b.PID_CGSTAmount,b.PID_IGST,b.PID_IGSTAmount,b.PID_Status,b.PID_CompID,c.Inv_Description,c.Inv_Color,c.Inv_Size,d.Inv_Description Commodity,e.PRM_RegistryNo,e.PRM_DocumentRefNo,e.PRM_ESugamNo,e.PRM_InvoiceDate,f.POM_OrderNo,f.POM_OrderDate,
                         g.POD_MasterID,g.POD_DescriptionID,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount,g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,h.PRD_BatchNumber,h.PRD_ManufactureDate,h.PRD_ExpireDate,i.GST_CHST    
                                From Purchase_verification   
                                Join Purchase_Invoice_Master On PV_InvoiceID=PIM_ID  
                                Join PI_Accepted_Details b On b.PID_MasterID=PIM_ID  
                                Join Inventory_Master_history InvH On b.PID_HistoryID=InvH.InvH_ID  
                                Join Inventory_Master c On b.PID_DescID=c.Inv_ID  
                                Join Inventory_Master d On b.PID_CommodityID=d.Inv_ID  
                                Join Purchase_Registry_Master e On PV_GinNo=e.PRM_ID  
                                Join Purchase_Order_Master f On PV_OrderNo =f.POM_ID  
                                Join Purchase_Order_Details g On  PID_HistoryID=g.POD_HistoryID And PIM_OrderID=g.POD_MAsterID  
                                Join Purchase_Registry_Details h On PIM_PRegesterID=h.PRD_MasterID And PIM_OrderID=h.PRD_OrderNo and b.PID_HistoryID=h.PRD_HistoryID 
                                Join GST_Rates i on i.GST_CommodityID=b.PID_CommodityID And i.GST_ItemID=b.PID_DescID And i.GST_ID=b.PID_GSTID "
            sSql = sSql & " where b.PID_CompID=" & iCompID & " "

            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo= " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            'sSql = sSql & " order by b.PID_ID,b.PID_DescID"
            dtDetails = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PID_Quantity") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    gtExAmt = 0
                    If IsDBNull(dtDetails.Rows(i)("PID_CommodityID")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_DescID")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("PID_Quantity")) = False Then
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PID_Quantity")) = False Then
                        dRow("TotalQty") = dtDetails.Rows(i)("PID_Quantity")
                        gtQty = gtQty + dtDetails.Rows(i)("PID_Quantity")
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PID_Rate")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("PID_Rate")
                        gtMRP = gtMRP + dtDetails.Rows(i)("PID_Rate")
                    Else
                        dRow("Rate") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_ChargePerItem")) = False Then
                        dRow("ChargePerItem") = dtDetails.Rows(i)("PID_ChargePerItem")
                        gtChargePerItem = gtChargePerItem + dRow("ChargePerItem")
                    Else
                        dRow("ChargePerItem") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_Discount")) = False Then
                        dRow("Discount") = dtDetails.Rows(i)("PID_Discount")
                        'objDB.SQLGetDescription(sNameSpace, "Select MAs_Desc From Acc_General_Master Where Mas_ID=" & dtDetails.Rows(i)("PID_Discount") & " And Mas_Master In(Select Mas_ID From Acc_Master_Type Where Mas_Type='Discount')")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_DiscountAmount")) = False Then
                        dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) * dRow("Discount")) / 100))
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If

                    '************'

                    If IsDBNull(dtDetails.Rows(i)("PID_GSTRate")) = False Then
                        dRow("GSTRate") = dtDetails.Rows(i)("PID_GSTRate")
                        gtGSTRate = gtGSTRate + dRow("GSTRate")
                    Else
                        dRow("GSTRate") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_GSTRate")) = False Then
                        dRow("GSTAmount") = dtDetails.Rows(i)("PID_GSTAmount")
                        gtGSTAmount = gtGSTAmount + dRow("GSTAmount")
                    Else
                        dRow("GSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_SGST")) = False Then
                        dRow("SGST") = dtDetails.Rows(i)("PID_SGST")
                        gtSGST = gtSGST + dRow("SGST")
                    Else
                        dRow("SGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_SGST")) = False Then
                        dRow("SGSTAmount") = dtDetails.Rows(i)("PID_SGSTAmount")
                        gtSGSTAmount = gtSGSTAmount + dRow("SGSTAmount")
                    Else
                        dRow("SGSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_CGST")) = False Then
                        dRow("CGST") = dtDetails.Rows(i)("PID_CGST")
                        gtCGST = gtCGST + dRow("CGST")
                    Else
                        dRow("CGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_CGST")) = False Then
                        dRow("CGSTAmount") = dtDetails.Rows(i)("PID_CGSTAmount")
                        gtCGSTAmount = gtCGSTAmount + dRow("CGSTAmount")
                    Else
                        dRow("CGSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_IGST")) = False Then
                        dRow("IGST") = dtDetails.Rows(i)("PID_IGST")
                        gtIGST = gtIGST + dRow("IGST")
                    Else
                        dRow("IGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_IGST")) = False Then
                        dRow("IGSTAmount") = dtDetails.Rows(i)("PID_IGSTAmount")
                        gtIGSTAmount = gtIGSTAmount + dRow("IGSTAmount")
                    Else
                        dRow("IGSTAmount") = "0"
                    End If

                    '************'


                    If IsDBNull(dtDetails.Rows(i)("POD_VAT")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VAT")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VAT")
                    Else
                        dRow("VAT") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        gtExAmt = dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                        gtExAmt = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_VAT") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(dtDetails.Rows(i)("POD_CST") * (((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")) + gtExAmt)) / 100))
                        gtCSTAmt = gtCSTAmt + dRow("CSTAmt")
                        Totaltax = Totaltax + dRow("CSTAmt")
                    Else
                        dRow("CSTAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PID_UnitID")) = False Then
                        'dRow("UnitId") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PID_HistoryID") & "')")
                        'dRow("AltUnit") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PID_HistoryID") & "')")
                        dRow("UnitId") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master where Mas_id ='" & dtDetails.Rows(i)("PID_UnitID") & "' ")
                        dRow("AltUnit") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master where Mas_id ='" & dtDetails.Rows(i)("PID_UnitID") & "' ")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If

                    If (dtDetails.Rows(i)("PRM_InvoiceDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PRM_InvoiceDate")) = False Then
                            dRow("PGM_InvoiceDate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PRM_InvoiceDate"), "D")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate"), "D")
                        Else
                            dRow("PGM_InvoiceDate") = ""
                        End If
                    Else
                        dRow("PGM_InvoiceDate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("PRM_DocumentRefNo")) = False Then
                        dRow("PGM_DocumentRefNo") = dtDetails.Rows(i)("PRM_DocumentRefNo")
                    End If

                    If (dtDetails.Rows(i)("PRD_ManufactureDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PRD_ManufactureDate")) = False Then
                            dRow("INVH_Mdate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PRD_ManufactureDate"), "D")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ManufactureDate"), "D")
                        Else
                            dRow("INVH_Mdate") = ""
                        End If
                    Else
                        dRow("INVH_Mdate") = ""
                    End If

                    If (dtDetails.Rows(i)("POM_OrderDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                            dRow("POM_OrderDate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
                        Else
                            dRow("POM_OrderDate") = ""
                        End If
                    Else
                        dRow("POM_OrderDate") = ""
                    End If
                    If (dtDetails.Rows(i)("PRD_ExpireDate").ToString() <> "") Then
                        If IsDBNull(dtDetails.Rows(i)("PRD_ExpireDate")) = False Then
                            dRow("INVH_Edate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PRD_ExpireDate"), "D")
                            'objGen.FormatDtForRDBMS(dtDetails.Rows(i)("PGD_ExpireDate"), "D")
                        Else
                            dRow("INVH_Edate") = ""
                        End If
                    Else
                        dRow("INVH_Edate") = ""
                    End If

                    If (dRow("INVH_Edate").ToString() = "30/12/1899") Then
                        dRow("INVH_Edate") = ""
                    End If
                    If (dRow("INVH_Mdate").ToString() = "30/12/1899") Then
                        dRow("INVH_Mdate") = ""
                    End If

                    If IsDBNull(dtDetails.Rows(i)("INVH_MRP")) = False Then
                        dRow("INVH_MRP") = dtDetails.Rows(i)("INVH_MRP")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PRD_BatchNumber")) = False Then
                        dRow("BatchNumber") = dtDetails.Rows(i)("PRD_BatchNumber")
                    End If

                    dRow("CalculateAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) + dRow("ChargePerItem")) - dRow("DiscountAmt")))
                    'String.Format("{0:0.000}", Convert.ToDecimal((dRow("Rate") * dRow("TotalQty")) - dRow("DiscountAmt")))

                    dRow("HSNCode") = dtDetails.Rows(i)("GST_CHST")

                    subTotal = subTotal + String.Format("{0:0.00}", Convert.ToDecimal((((dRow("Rate") * dRow("TotalQty")) + dRow("ChargePerItem")) - dRow("DiscountAmt")) + dRow("GSTAmount")))
                    TotalAmt = Totaltax + ((((dRow("Rate") * dRow("TotalQty")) + dRow("ChargePerItem")) - dRow("DiscountAmt")) + dRow("GSTAmount"))
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal((((dRow("Rate") * dRow("TotalQty")) + dRow("ChargePerItem")) - dRow("DiscountAmt")) + dRow("GSTAmount")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = String.Format("{0:0.00}", subTotal)
                    dRow("TotalVat") = String.Format("{0:0.00}", TotalVat)
                    dRow("CSTAmtTotal") = String.Format("{0:0.00}", gtCSTAmt)
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(subTotal))
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)

                    dRow("GSTAmountTotal") = String.Format("{0:0.00}", gtGSTAmount)
                    dRow("SGSTAmountTotal") = String.Format("{0:0.00}", gtSGSTAmount)
                    dRow("CGSTAmountTotal") = String.Format("{0:0.00}", gtCGSTAmount)
                    dRow("IGSTAmountTotal") = String.Format("{0:0.00}", gtIGSTAmount)

                    dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
                    dt.Rows.Add(dRow)
                End If
            Next
            dtDetails.Clear()
            sSql = "" : sSql = "select PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_BillNo,PV_DocRefNo,"
            sSql = sSql & "b.PIE_ID,b.PIE_OrderID,b.PIE_GINID,b.PIE_CommodityID,b.PIE_Description,b.PIE_HistoryID,b.PIE_UnitID,b.PIE_Rate,b.PIE_Quantity,b.PIE_RateAmount,"
            sSql = sSql & " b.PIE_Discount,b.PIE_DiscountAmount,b.PIE_Excise,b.PIE_ExciseAmount,b.PIE_Vat,b.PIE_VatAmount,b.PIE_TotalAmount,b.PIE_AcceptQty,b.PIE_DocRef,"
            sSql = sSql & "c.Inv_Description,c.Inv_Color,c.Inv_Size,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,"
            sSql = sSql & "d.Inv_Description Commodity	"
            sSql = sSql & " From Purchase_verification"
            sSql = sSql & " Join Purchase_Invoice_Excess b on PV_DocRefNo=b.PIE_DocRef"
            sSql = sSql & " Join Inventory_Master_history InvH On b.PIE_HistoryID=InvH.InvH_ID"
            sSql = sSql & "   Join Inventory_Master c on b.PIE_Description=c.Inv_ID"
            sSql = sSql & "  Join Inventory_Master d on b.PIE_CommodityID=d.Inv_ID"
            sSql = sSql & " Join Purchase_GIN_Master e on PV_GinNo=e.PGM_ID "
            sSql = sSql & " Join Purchase_Order_Master f on PV_OrderNo =f.POM_ID"

            If iorder <> 0 Then
                sSql = sSql & " And PV_OrderNo = " & iorder & " "
            End If
            If iInvoice <> 0 Then
                sSql = sSql & " And PV_ID= " & iInvoice & " "
            End If
            sSql = sSql & " order by b.PIE_ID,b.PIE_Description"
            dtDetails = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dtDetails.Rows.Count - 1
                If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> 0) Then
                    dRow = dt.NewRow()
                    Total = 0
                    TotalAmt = 0
                    Totaltax = 0
                    If IsDBNull(dtDetails.Rows(i)("Commodity")) = False Then
                        dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Description")) = False Then
                        dRow("SlNo") = i + 1
                        dRow("Description") = dtDetails.Rows(i)("Inv_Description")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_AcceptedQnt")) = False Then
                        If (dtDetails.Rows(i)("PIA_AcceptedQnt") <> "") Then
                            dRow("TotalQty") = dtDetails.Rows(i)("PIA_AcceptedQnt")
                            gtQty = gtQty + dtDetails.Rows(i)("PIA_AcceptedQnt")
                        Else
                            dRow("TotalQty") = 0
                        End If
                    Else
                        dRow("TotalQty") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIE_Rate")) = False Then
                        dRow("Rate") = dtDetails.Rows(i)("POD_Rate")
                        gtMRP = gtMRP + dtDetails.Rows(i)("POD_Rate")
                    Else
                        dRow("Rate") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_VATAmount")) = False Then
                        dRow("VAT") = dtDetails.Rows(i)("POD_VATAmount")
                        gtVAT = gtVAT + dtDetails.Rows(i)("POD_VATAmount")
                    Else
                        dRow("VAT") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Vat")) = False Then
                        dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_Vat")))
                        Totaltax = Totaltax + dRow("VATAmt")
                        gtVATAmt = gtVATAmt + dRow("VATAmt")
                        TotalVat = TotalVat + dRow("VATAmt")
                    Else
                        dRow("VATAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CST")) = False And dtDetails.Rows(i)("POD_CST") <> "" Then
                        dRow("CST") = dtDetails.Rows(i)("POD_CST")
                        gtCST = gtCST + dtDetails.Rows(i)("POD_CST")
                    Else
                        dRow("CST") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_CSTAmount")) = False Then
                        dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_CSTAmount")))
                        gtCSTAmt = gtCSTAmt + dtDetails.Rows(i)("POD_CSTAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_CSTAmount")
                    Else
                        dRow("CSTAmt") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_Excise")) = False Then
                        dRow("Exise") = dtDetails.Rows(i)("POD_Excise")
                        gtExise = gtExise + dtDetails.Rows(i)("POD_Excise")
                    Else
                        dRow("Exise") = 0
                    End If
                    If IsDBNull(dtDetails.Rows(i)("PIA_HistoryID")) = False Then
                        dRow("UnitId") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_Unit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                        dRow("AltUnit") = objDB.SQLGetDescription(sNameSpace, "select Mas_Desc from acc_general_master  where Mas_id in(select InvH_AlterUnit from inventory_master_history where invH_ID='" & dtDetails.Rows(i)("PIA_HistoryID") & "')")
                    Else
                        dRow("UnitId") = "0"
                        dRow("AltUnit") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_ExciseAmount")) = False And dtDetails.Rows(i)("POD_ExciseAmount") <> "" Then
                        dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("POD_ExciseAmount")))
                        gtExiseAmt = gtExiseAmt + dtDetails.Rows(i)("POD_ExciseAmount")
                        Totaltax = Totaltax + dtDetails.Rows(i)("POD_ExciseAmount")
                    Else
                        dRow("ExiseAmt") = 0
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_Frieght")) = False And dtDetails.Rows(i)("POD_Frieght") <> "" Then
                        dRow("ChargePerItem") = dtDetails.Rows(i)("POD_Frieght")
                        gtChargePerItem = gtChargePerItem + dRow("ChargePerItem")
                    Else
                        dRow("ChargePerItem") = "0"
                    End If


                    If IsDBNull(dtDetails.Rows(i)("POD_Discount")) = False And dtDetails.Rows(i)("POD_Discount") <> "" Then
                        dRow("Discount") = dtDetails.Rows(i)("POD_Discount")
                        gtdiscount = gtdiscount + dRow("Discount")
                    Else
                        dRow("Discount") = "0"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("POD_DiscountAmount")) = False And dtDetails.Rows(i)("POD_DiscountAmount") <> "" Then
                        dRow("DiscountAmt") = dtDetails.Rows(i)("POD_DiscountAmount")
                        gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    Else
                        dRow("DiscountAmt") = "0"
                    End If

                    '************'

                    If IsDBNull(dtDetails.Rows(i)("POD_GSTRate")) = False Then
                        dRow("GSTRate") = dtDetails.Rows(i)("POD_GSTRate")
                        gtGSTRate = gtGSTRate + dRow("GSTRate")
                    Else
                        dRow("GSTRate") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_GSTRate")) = False Then
                        dRow("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")) * dtDetails.Rows(i)("POD_GSTRate")) / 100))
                        gtGSTAmount = gtGSTAmount + dRow("GSTAmount")
                    Else
                        dRow("GSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_SGST")) = False Then
                        dRow("SGST") = dtDetails.Rows(i)("POD_SGST")
                        gtSGST = gtSGST + dRow("SGST")
                    Else
                        dRow("SGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_SGST")) = False Then
                        dRow("SGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")) * dtDetails.Rows(i)("POD_SGST")) / 100))
                        gtSGSTAmount = gtSGSTAmount + dRow("SGSTAmount")
                    Else
                        dRow("SGSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CGST")) = False Then
                        dRow("CGST") = dtDetails.Rows(i)("POD_CGST")
                        gtCGST = gtCGST + dRow("CGST")
                    Else
                        dRow("CGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_CGST")) = False Then
                        dRow("CGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")) * dtDetails.Rows(i)("POD_CGST")) / 100))
                        gtCGSTAmount = gtCGSTAmount + dRow("CGSTAmount")
                    Else
                        dRow("CGSTAmount") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_IGST")) = False Then
                        dRow("IGST") = dtDetails.Rows(i)("POD_IGST")
                        gtIGST = gtIGST + dRow("IGST")
                    Else
                        dRow("IGST") = "0"
                    End If

                    If IsDBNull(dtDetails.Rows(i)("POD_IGST")) = False Then
                        dRow("IGSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")) * dtDetails.Rows(i)("POD_IGST")) / 100))
                        gtIGSTAmount = gtIGSTAmount + dRow("IGSTAmount")
                    Else
                        dRow("IGSTAmount") = "0"
                    End If

                    '************'

                    subTotal = subTotal + (((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt"))
                    TotalAmt = Totaltax + (((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt"))
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((dRow("Rate") * dRow("TotalQty")) + dRow("Frieght")) - dRow("DiscountAmt")))
                    GrandTotal = GrandTotal + TotalAmt
                    dRow("SubTotal") = subTotal
                    dRow("TotalVat") = TotalVat
                    dRow("CSTAmtTotal") = gtCSTAmt
                    dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(subTotal + gtGSTAmount))
                    dRow("TotalExiseAmt") = String.Format("{0:0.00}", gtExiseAmt)
                    dRow("gtdiscountAmt") = String.Format("{0:0.00}", gtdiscountAmt)

                    dRow("GSTAmountTotal") = String.Format("{0:0.00}", gtGSTAmount)
                    dRow("SGSTAmountTotal") = String.Format("{0:0.00}", gtSGSTAmount)
                    dRow("CGSTAmountTotal") = String.Format("{0:0.00}", gtCGSTAmount)
                    dRow("IGSTAmountTotal") = String.Format("{0:0.00}", gtIGSTAmount)
                    dt.Rows.Add(dRow)
                End If
            Next

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetURD(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderNo As Integer, ByVal iVerificationID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dtDetails, dt1, dtdata As New DataTable
        Dim dRow As DataRow
        Dim dQty, dBasicAmt, dSGSTAmt, dCGSTAmt, dIGSTAmt As Double
        Try
            'g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,
            sSql = "" : sSql = "Select Distinct(b.PID_DescID),PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PRM_RegistryNo,e.PRM_DocumentRefNo,e.PRM_ESugamNo,e.PRM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,PIM_PRegesterID,b.PID_MasterID,b.PID_CommodityID,b.PID_HistoryID,b.PID_UnitID,b.PID_Quantity,b.PID_Rate,b.PID_ChargePerItem,b.PID_Discount,b.PID_DiscountAmount,b.PID_GSTID,b.PID_GSTRate,b.PID_GSTAmount,b.PID_SGST,b.PID_SGSTAmount,b.PID_CGST,b.PID_CGSTAmount,b.PID_IGST,b.PID_IGSTAmount,b.PID_Amount,b.PID_Status,b.PID_CompID,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PRD_BatchNumber,h.PRD_ManufactureDate,h.PRD_ExpireDate, d.Inv_Description Commodity, g.POD_MasterID,g.POD_HistoryID,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount, g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,i.GST_GSTRate      
                        From Purchase_verification   
                        Join Purchase_Invoice_Master On PV_InvoiceID=PIM_ID  
                        Join PI_Accepted_Details b On b.PID_MasterID=PIM_ID  
                        Join Inventory_Master_history InvH On b.PID_HistoryID=InvH.InvH_ID  
                        Join Inventory_Master c On b.PID_DescID=c.Inv_ID  
                        Join Inventory_Master d On b.PID_CommodityID=d.Inv_ID  
                        Join Purchase_Registry_Master e On PV_GinNo=e.PRM_ID  
                        Join Purchase_Order_Master f On PV_OrderNo =f.POM_ID  
                        Join Purchase_Order_Details g On  PID_HistoryID=g.POD_HistoryID And PIM_OrderID=g.POD_MAsterID  
                        Join Purchase_Registry_Details h On PIM_PRegesterID=h.PRD_MasterID And PIM_OrderID=h.PRD_OrderNo and b.PID_HistoryID=h.PRD_HistoryID 
                        Join GST_Rates i on i.GST_CommodityID=b.PID_CommodityID And i.GST_ItemID=b.PID_DescID And i.GST_ID=b.PID_GSTID
                        where b.PID_CompID=" & iCompID & " And PV_OrderNo=" & iOrderNo & " And PV_ID=" & iVerificationID & " "
            dtDetails = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)


            sSql = "" : sSql = "select Distinct(i.GST_GSTRate)
                    From Purchase_verification   
                    Join Purchase_Invoice_Master On PV_InvoiceID=PIM_ID  
                    Join PI_Accepted_Details b On b.PID_MasterID=PIM_ID  
                    Join Inventory_Master_history InvH On b.PID_HistoryID=InvH.InvH_ID  
                    Join Inventory_Master c On b.PID_DescID=c.Inv_ID  
                    Join Inventory_Master d On b.PID_CommodityID=d.Inv_ID  
                    Join Purchase_Registry_Master e On PV_GinNo=e.PRM_ID  
                    Join Purchase_Order_Master f On PV_OrderNo =f.POM_ID  
                    Join Purchase_Order_Details g On  PID_HistoryID=g.POD_HistoryID And PIM_OrderID=g.POD_MAsterID  
                    Join Purchase_Registry_Details h On PIM_PRegesterID=h.PRD_MasterID And PIM_OrderID=h.PRD_OrderNo and b.PID_HistoryID=h.PRD_HistoryID 
                    Join GST_Rates i on i.GST_CommodityID=b.PID_CommodityID And i.GST_ItemID=b.PID_DescID And i.GST_ID=b.PID_GSTID
                    where b.PID_CompID=" & iCompID & " And PV_OrderNo=" & iOrderNo & " And PV_ID=" & iVerificationID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            dt1.Columns.Add("GSTRate")
            dt1.Columns.Add("Qty")
            dt1.Columns.Add("BasicAmt")
            dt1.Columns.Add("TaxAmt")
            dt1.Columns.Add("Total")

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    dQty = 0 : dBasicAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0
                    Dim dview As New DataView(dtDetails)
                    dview = dtDetails.DefaultView
                    dview.RowFilter = "GST_GSTRate=" & dt.Rows(i)("GST_GSTRate") & ""
                    dtdata = dview.ToTable

                    If dtdata.Rows.Count > 0 Then
                        For j = 0 To dtdata.Rows.Count - 1

                            dRow = dt1.NewRow
                            dRow("GSTRate") = dtdata.Rows(j)("GST_GSTRate")

                            dQty = dQty + dtdata.Rows(j)("PID_Quantity")
                            dRow("Qty") = dQty

                            dBasicAmt = dBasicAmt + dtdata.Rows(j)("PID_Amount")
                            dRow("BasicAmt") = dBasicAmt

                            dSGSTAmt = dSGSTAmt + dtdata.Rows(j)("PID_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dtdata.Rows(j)("PID_CGSTAmount")

                            dIGSTAmt = dIGSTAmt + dtdata.Rows(j)("PID_IGSTAmount")

                            If dIGSTAmt > 0 Then
                                dRow("TaxAmt") = dIGSTAmt
                            Else
                                dRow("TaxAmt") = dSGSTAmt + dCGSTAmt
                            End If

                            dRow("Total") = Convert.ToDouble(dRow("BasicAmt")) + Convert.ToDouble(dRow("TaxAmt"))

                            If j = dtdata.Rows.Count - 1 Then
                                dt1.Rows.Add(dRow)
                            End If

                        Next
                    End If

                Next
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetHSN(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderNo As Integer, ByVal iVerificationID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dtDetails, dt1, dtdata As New DataTable
        Dim dRow As DataRow
        Dim dQty, dBasicAmt, dSGSTAmt, dCGSTAmt, dIGSTAmt As Double
        Try
            'g.POD_Rate,g.POD_Quantity,g.POD_RateAmount,
            sSql = "" : sSql = "Select Distinct(b.PID_DescID),PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PRM_RegistryNo,e.PRM_DocumentRefNo,e.PRM_ESugamNo,e.PRM_InvoiceDate,f.POM_OrderDate,PV_CompID,PV_Status,PV_BillNo,PIM_PRegesterID,b.PID_MasterID,b.PID_CommodityID,b.PID_HistoryID,b.PID_UnitID,b.PID_Quantity,b.PID_Rate,b.PID_ChargePerItem,b.PID_Discount,b.PID_DiscountAmount,b.PID_GSTID,b.PID_GSTRate,b.PID_GSTAmount,b.PID_SGST,b.PID_SGSTAmount,b.PID_CGST,b.PID_CGSTAmount,b.PID_IGST,b.PID_IGSTAmount,b.PID_Amount,b.PID_Status,b.PID_CompID,c.Inv_Description,c.Inv_Color,c.Inv_Size,h.PRD_BatchNumber,h.PRD_ManufactureDate,h.PRD_ExpireDate, d.Inv_Description Commodity, g.POD_MasterID,g.POD_HistoryID,g.POD_Discount,g.POD_DiscountAmount,g.POD_Excise,g.POD_ExciseAmount, g.POD_VAT,g.POD_VATAmount,g.POD_CST,g.POD_CSTAmount,g.POD_CompID,g.POD_Status,InvH.INVH_MRP,InvH.INVH_Mdate,InvH.INVH_Edate,i.GST_GSTRate,i.GST_CHST      
                        From Purchase_verification   
                        Join Purchase_Invoice_Master On PV_InvoiceID=PIM_ID  
                        Join PI_Accepted_Details b On b.PID_MasterID=PIM_ID  
                        Join Inventory_Master_history InvH On b.PID_HistoryID=InvH.InvH_ID  
                        Join Inventory_Master c On b.PID_DescID=c.Inv_ID  
                        Join Inventory_Master d On b.PID_CommodityID=d.Inv_ID  
                        Join Purchase_Registry_Master e On PV_GinNo=e.PRM_ID  
                        Join Purchase_Order_Master f On PV_OrderNo =f.POM_ID  
                        Join Purchase_Order_Details g On  PID_HistoryID=g.POD_HistoryID And PIM_OrderID=g.POD_MAsterID  
                        Join Purchase_Registry_Details h On PIM_PRegesterID=h.PRD_MasterID And PIM_OrderID=h.PRD_OrderNo and b.PID_HistoryID=h.PRD_HistoryID 
                        Join GST_Rates i on i.GST_CommodityID=b.PID_CommodityID And i.GST_ItemID=b.PID_DescID And i.GST_ID=b.PID_GSTID
                        where b.PID_CompID=" & iCompID & " And PV_OrderNo=" & iOrderNo & " And PV_ID=" & iVerificationID & " "
            dtDetails = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)


            sSql = "" : sSql = "select Distinct(i.GST_CHST)
                    From Purchase_verification   
                    Join Purchase_Invoice_Master On PV_InvoiceID=PIM_ID  
                    Join PI_Accepted_Details b On b.PID_MasterID=PIM_ID  
                    Join Inventory_Master_history InvH On b.PID_HistoryID=InvH.InvH_ID  
                    Join Inventory_Master c On b.PID_DescID=c.Inv_ID  
                    Join Inventory_Master d On b.PID_CommodityID=d.Inv_ID  
                    Join Purchase_Registry_Master e On PV_GinNo=e.PRM_ID  
                    Join Purchase_Order_Master f On PV_OrderNo =f.POM_ID  
                    Join Purchase_Order_Details g On  PID_HistoryID=g.POD_HistoryID And PIM_OrderID=g.POD_MAsterID  
                    Join Purchase_Registry_Details h On PIM_PRegesterID=h.PRD_MasterID And PIM_OrderID=h.PRD_OrderNo and b.PID_HistoryID=h.PRD_HistoryID 
                    Join GST_Rates i on i.GST_CommodityID=b.PID_CommodityID And i.GST_ItemID=b.PID_DescID And i.GST_ID=b.PID_GSTID
                    where b.PID_CompID=" & iCompID & " And PV_OrderNo=" & iOrderNo & " And PV_ID=" & iVerificationID & " "
            dt = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            dt1.Columns.Add("HSN")
            dt1.Columns.Add("Qty")
            dt1.Columns.Add("BasicAmt")
            dt1.Columns.Add("TaxAmt")
            dt1.Columns.Add("Total")

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    dQty = 0 : dBasicAmt = 0 : dSGSTAmt = 0 : dCGSTAmt = 0
                    Dim dview As New DataView(dtDetails)
                    dview = dtDetails.DefaultView
                    dview.RowFilter = "GST_CHST='" & dt.Rows(i)("GST_CHST") & "'"
                    dtdata = dview.ToTable

                    If dtdata.Rows.Count > 0 Then
                        For j = 0 To dtdata.Rows.Count - 1

                            dRow = dt1.NewRow
                            dRow("HSN") = dtdata.Rows(j)("GST_CHST")

                            dQty = dQty + dtdata.Rows(j)("PID_Quantity")
                            dRow("Qty") = dQty

                            dBasicAmt = dBasicAmt + dtdata.Rows(j)("PID_Amount")
                            dRow("BasicAmt") = dBasicAmt

                            dSGSTAmt = dSGSTAmt + dtdata.Rows(j)("PID_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dtdata.Rows(j)("PID_CGSTAmount")

                            dIGSTAmt = dIGSTAmt + dtdata.Rows(j)("PID_IGSTAmount")

                            If dIGSTAmt > 0 Then
                                dRow("TaxAmt") = dIGSTAmt
                            Else
                                dRow("TaxAmt") = dSGSTAmt + dCGSTAmt
                            End If

                            dRow("Total") = Convert.ToDouble(dRow("BasicAmt")) + Convert.ToDouble(dRow("TaxAmt"))

                            If j = dtdata.Rows.Count - 1 Then
                                dt1.Rows.Add(dRow)
                            End If

                        Next
                    End If

                Next
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
