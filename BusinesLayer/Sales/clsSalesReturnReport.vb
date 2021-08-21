Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsSalesReturnReport
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function GetVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVatID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "SElect Mas_Desc From Acc_General_Master Where Mas_ID=" & iVatID & " and mas_Master=14 and mas_compID=" & iCompID & ""
            GetVAT = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetVAT
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCstID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "SElect Mas_Desc From Acc_General_Master Where Mas_ID=" & iCstID & " and mas_Master=15 and mas_compID=" & iCompID & ""
            GetCST = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetCST
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOrderType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iInvice As Integer) As String
        Dim sStr As String = ""
        Dim sSql As String = ""
        Try
            sSql = "Select SPO_OrderType From Sales_ProForma_Order Where SPO_ID=" & iInvice & " And SPO_YearID=" & iYearID & " And SPO_CompID=" & iCompID & "  "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTermsConditions(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sStr As String = ""
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_Desc From Acc_General_Master Where MAS_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Terms & Conditions') And Mas_CompID=" & iCompID & " "
            sStr = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return sStr
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
    Public Function GetALLVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As String
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt As String
        Dim sStr As String = ""
        Try
            sSql = "Select Distinct(SPOD_VAT) From Sales_Proforma_Order_Details where SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SPOD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SPOD_VATAmount) From Sales_Proforma_Order_Details where SPOD_VAT=" & dt.Rows(i)("SPOD_VAT") & " And SPOD_SOID=" & iOrderID & " And SPOD_Status<>'C' And SPOD_CompID=" & iCompID & " And SPOD_YearID=" & iYearID & " ")
                    sValue = "VAT@" & sVatDesc & " - " & sVATAmt
                    sStr = sStr & "," & sValue
                Next
            End If
            If sStr.StartsWith(",") Then
                sStr = sStr.Remove(0, 1)
            End If
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDataOral(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iReturnID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try

            'sSql = "select Distinct(SRD_ID)As SRD_ID,z.SRD_MasterID,b.SDM_ID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_OrderID,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,c.SPO_ID,c.SPO_OrderCode,c.SPO_OrderDate,c.SPO_BuyerOrderNo,c.SPO_BuyerOrderDate,SRD_HistoryID As SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,
            'SRD_CommodityID As SDD_CommodityID,SRD_DescriptionID As SDD_DescID,SRD_HistoryID As SDD_HistoryID,SRD_Rate As SDD_Rate,SRD_ReturnQnty As SDD_Quantity,SRD_RateAmount As SDD_RateAmount,SRD_CompID As SDD_CompID,d.INVH_MRP,
            'SRD_Discount As SDD_Discount,SRD_DiscountAmount As SDD_DiscountAmount,SRD_VAT As SDD_VAT,SRD_VATAmount As SDD_VATAmount,SRD_Excise As SDD_Excise,SRD_ExciseAmount As SDD_ExciseAmount,SRD_CST As SDD_CST,SRD_CSTAmount As SDD_CSTAmount,SRD_TotalAmount As SDD_TotalAmount,f.INV_Description Commodity,c.SPO_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,c.SPO_GrandDiscount As TradeDiscount,g.MAS_Desc As ModeOfDispatch,
            'i.CUST_NAME,i.CUST_Comm_Address,i.CUST_Comm_Tel,i.CUST_Email,j.Cmp_Value As CVAT,k.Cmp_Value As CTAX,l.Cmp_Value As CPAN,m.Cmp_Value As CTAN,n.Cmp_Value As CTIN,o.Cmp_Value As CCIN,p.BM_Name,p.BM_Address,p.BM_MobileNo,p.BM_EmailID,q.Buyer_Value As BVAT,r.Buyer_Value As BTAX,s.Buyer_Value As BPAN,t.Buyer_Value As BTAN,u.Buyer_Value As BTIN,v.Buyer_Value As BCIN,x.PGD_manufactureDate,x.PGD_ExpireDate,y.SRM_ReturnOrderCode,y.SRM_ReturnDate,y.SRM_Narration
            'from  Sales_Return_Master y 
            'Join Sales_Return_Details z On z.SRD_MasterID=y.SRM_ID 
            'join Sales_Dispatch_Master b on b.SDM_ID=y.SRM_DispatchID And b.SDM_OrderID=y.SRM_OrderNo 

            'join Sales_Proforma_Order c on b.SDM_OrderID=c.SPO_ID
            'join Inventory_Master_History d on z.SRD_HistoryID=d.InvH_Id
            'join Inventory_Master f on z.SRD_CommodityID=f.Inv_ID   
            'join Inventory_Master e on z.SRD_DescriptionID=e.Inv_ID 
            'join Acc_General_master h on z.SRD_UnitOfMeasurement=h.Mas_ID 
            'join Acc_General_master g on b.SDM_ModeOfShipping=g.Mas_ID 
            'join MST_Customer_Master i on b.SDM_CompID=i.CUST_ID
            'Left Join Company_Accounting_Template j On i.CUST_ID=j.Cmp_ID And j.Cmp_Desc='VAT'
            'Left Join Company_Accounting_Template k On i.CUST_ID=k.Cmp_ID And k.Cmp_Desc='TAX'
            'Left Join Company_Accounting_Template l On i.CUST_ID=l.Cmp_ID And l.Cmp_Desc='PAN'
            'Left Join Company_Accounting_Template m On i.CUST_ID=m.Cmp_ID And m.Cmp_Desc='TAN'
            'Left Join Company_Accounting_Template n On i.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TIN'
            'Left Join Company_Accounting_Template o On i.CUST_ID=o.Cmp_ID And o.Cmp_Desc='CIN'

            'join Sales_Buyers_Masters p on b.SDM_SupplierID=p.BM_ID 
            'Left Join Sales_Buyer_Accounting_Template q On p.BM_ID=q.Buyer_ID And q.Buyer_Desc='VAT' 
            'Left Join Sales_Buyer_Accounting_Template r On p.BM_ID=r.Buyer_ID And r.Buyer_Desc='TAX' 
            'Left Join Sales_Buyer_Accounting_Template s On p.BM_ID=s.Buyer_ID And s.Buyer_Desc='PAN' 
            'Left Join Sales_Buyer_Accounting_Template t On p.BM_ID=t.Buyer_ID And t.Buyer_Desc='TAN' 
            'Left Join Sales_Buyer_Accounting_Template u On p.BM_ID=u.Buyer_ID And u.Buyer_Desc='TIN' 
            'Left Join Sales_Buyer_Accounting_Template v On p.BM_ID=v.Buyer_ID And v.Buyer_Desc='CIN'
            'Left Join Stock_Ledger w On c.SPO_ID=w.SL_SaleOrderID 
            'Left Join Purchase_GIN_Details x On w.SL_GINID=x.PGD_MasterID And z.SRD_CommodityID=x.PGD_CommodityID And z.SRD_DescriptionID=x.PGD_DescriptionID And z.SRD_HistoryID=x.PGD_HistoryID
            'where SRM_ID=" & iReturnID & " And SRM_CompID=" & iCompID & ""


            sSql = "select Distinct(SRD_ID)As SRD_ID,z.SRD_MasterID,b.SDM_ID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_OrderID,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,c.SPO_ID,c.SPO_OrderCode,c.SPO_OrderDate,c.SPO_BuyerOrderNo,c.SPO_BuyerOrderDate,SRD_HistoryID As SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,
            SRD_CommodityID As SDD_CommodityID,SRD_DescriptionID As SDD_DescID,SRD_HistoryID As SDD_HistoryID,SRD_Rate As SDD_Rate,SRD_ReturnQnty As SDD_Quantity,SRD_RateAmount As SDD_RateAmount,SRD_CompID As SDD_CompID,d.INVH_MRP,
            SRD_Discount As SDD_Discount,SRD_DiscountAmount As SDD_DiscountAmount,SRD_VAT As SDD_VAT,SRD_VATAmount As SDD_VATAmount,SRD_Excise As SDD_Excise,SRD_ExciseAmount As SDD_ExciseAmount,SRD_CST As SDD_CST,SRD_CSTAmount As SDD_CSTAmount,SRD_TotalAmount As SDD_TotalAmount,f.INV_Description Commodity,c.SPO_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,c.SPO_GrandDiscount As TradeDiscount,g.MAS_Desc As ModeOfDispatch,
            i.CUST_NAME,i.CUST_Comm_Address,i.CUST_Comm_Tel,i.CUST_Email,j.Cmp_Value As CVAT,k.Cmp_Value As CTAX,l.Cmp_Value As CPAN,m.Cmp_Value As CTAN,n.Cmp_Value As CTIN,o.Cmp_Value As CCIN,p.BM_Name,p.BM_Address,p.BM_MobileNo,p.BM_EmailID,q.Buyer_Value As BVAT,r.Buyer_Value As BTAX,s.Buyer_Value As BPAN,t.Buyer_Value As BTAN,u.Buyer_Value As BTIN,v.Buyer_Value As BCIN,x.PGD_manufactureDate,x.PGD_ExpireDate,y.SRM_ReturnOrderCode,y.SRM_ReturnDate,y.SRM_Narration
            from  Sales_Return_Master y 
            Join Sales_Return_Details z On z.SRD_MasterID=y.SRM_ID 
            join Sales_Dispatch_Master b on b.SDM_ID=y.SRM_DispatchID And b.SDM_OrderID=y.SRM_OrderNo 
 
            join Sales_Proforma_Order c on b.SDM_OrderID=c.SPO_ID
            join Inventory_Master_History d on z.SRD_HistoryID=d.InvH_Id
            join Inventory_Master f on z.SRD_CommodityID=f.Inv_ID   
            join Inventory_Master e on z.SRD_DescriptionID=e.Inv_ID 
            join Acc_General_master h on z.SRD_UnitOfMeasurement=h.Mas_ID 
            join Acc_General_master g on b.SDM_ModeOfShipping=g.Mas_ID 
            join MST_Customer_Master i on b.SDM_CompID=i.CUST_ID
            Left Join Company_Accounting_Template j On i.CUST_ID=j.Cmp_ID And j.Cmp_Desc='VAT'
            Left Join Company_Accounting_Template k On i.CUST_ID=k.Cmp_ID And k.Cmp_Desc='TAX'
            Left Join Company_Accounting_Template l On i.CUST_ID=l.Cmp_ID And l.Cmp_Desc='PAN'
            Left Join Company_Accounting_Template m On i.CUST_ID=m.Cmp_ID And m.Cmp_Desc='TAN'
            Left Join Company_Accounting_Template n On i.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TIN'
            Left Join Company_Accounting_Template o On i.CUST_ID=o.Cmp_ID And o.Cmp_Desc='CIN'

            join Sales_Buyers_Masters p on b.SDM_SupplierID=p.BM_ID 
            Left Join Sales_Buyer_Accounting_Template q On p.BM_ID=q.Buyer_ID And q.Buyer_Desc='VAT' 
            Left Join Sales_Buyer_Accounting_Template r On p.BM_ID=r.Buyer_ID And r.Buyer_Desc='TAX' 
            Left Join Sales_Buyer_Accounting_Template s On p.BM_ID=s.Buyer_ID And s.Buyer_Desc='PAN' 
            Left Join Sales_Buyer_Accounting_Template t On p.BM_ID=t.Buyer_ID And t.Buyer_Desc='TAN' 
            Left Join Sales_Buyer_Accounting_Template u On p.BM_ID=u.Buyer_ID And u.Buyer_Desc='TIN' 
            Left Join Sales_Buyer_Accounting_Template v On p.BM_ID=v.Buyer_ID And v.Buyer_Desc='CIN'
			Left Join Stock_Ledger_SalesDetails ab On ab.SLS_SaleOrderID=c.SPO_ID And z.SRD_CommodityID=ab.SLS_Commodity And z.SRD_DescriptionID=ab.SLS_ItemID And z.SRD_HistoryID=ab.SLS_HistoryID
            Left Join Stock_Ledger w On w.SL_ID=ab.SLS_MasterID And z.SRD_CommodityID=w.SL_Commodity And z.SRD_DescriptionID=w.SL_ItemID And z.SRD_HistoryID=w.SL_HistoryID
			Left Join Purchase_GIN_Master aa On aa.PGM_ID=w.SL_GINID
            Left Join Purchase_GIN_Details x On aa.PGM_ID=x.PGD_MasterID And z.SRD_CommodityID=x.PGD_CommodityID And z.SRD_DescriptionID=x.PGD_DescriptionID And z.SRD_HistoryID=x.PGD_HistoryID
            where SRM_ID=" & iReturnID & " And SRM_CompID=" & iCompID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function NumberToWord(ByVal num1 As String) As String
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

    Public Shared Function AfterDecimalfunction(ByVal num As Decimal) As String
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
    Public Function BindDispatchNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "Select SRM_ID,SRM_ReturnOrderCode from Sales_Return_Master where SRM_YearID =" & iYearID & " And SRM_CompID =" & iCompID & " order by SRM_ID Desc "
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVATBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Try

            dt1.Columns.Add("VAT")
            dt1.Columns.Add("Amount")
            dt1.Columns.Add("VATAmount")

            sSql = "Select Distinct(SRD_VAT) From Sales_Return_Details where SRD_MasterID=" & iOrderID & " And SRD_CompID=" & iCompID & " And SRD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SRD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SRD_TotalAmount) From Sales_Return_Details where SRD_VAT=" & dt.Rows(i)("SRD_VAT") & " And SRD_MasterID=" & iOrderID & " And SRD_CompID=" & iCompID & " And SRD_YearID=" & iYearID & " ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SRD_VATAmount) From Sales_Return_Details where SRD_VAT=" & dt.Rows(i)("SRD_VAT") & " And SRD_MasterID=" & iOrderID & " And SRD_CompID=" & iCompID & " And SRD_YearID=" & iYearID & " ")

                    dRow("VAT") = sVatDesc
                    dRow("Amount") = sAmountTot
                    dRow("VATAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCSTBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt As String
        Dim sAmountTot As String = ""
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Try

            dt1.Columns.Add("CST")
            dt1.Columns.Add("CAmount")
            dt1.Columns.Add("CSTAmount")

            sSql = "Select Distinct(SRD_CST) From Sales_Return_Details where SRD_MasterID=" & iOrderID & " And SRD_CompID=" & iCompID & " And SRD_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SRD_CST") & " And MAS_CompID=" & iCompID & " ")

                    If dt.Rows(i)("SRD_CST") > 0 Then
                        sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SRD_TotalAmount) From Sales_Return_Details where SRD_CST=" & dt.Rows(i)("SRD_CST") & " And SRD_MasterID=" & iOrderID & " And SRD_CompID=" & iCompID & " And SRD_YearID=" & iYearID & " ")
                    End If
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SRD_CSTAmount) From Sales_Return_Details where SRD_CST=" & dt.Rows(i)("SRD_CST") & " And SRD_MasterID=" & iOrderID & " And SRD_CompID=" & iCompID & " And SRD_YearID=" & iYearID & " ")

                    dRow("CST") = sVatDesc
                    dRow("CAmount") = sAmountTot
                    dRow("CSTAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
