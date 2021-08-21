Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsItemWiseSalesInvoice
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

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
    Public Function GetData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvice As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "select Distinct(SDD_ID),SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,g.SPO_ID,g.SPO_OrderCode,g.SPO_OrderDate,g.SPO_BuyerOrderNo,g.SPO_BuyerOrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP, SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_GSTRate,SDD_GSTAmount,SDD_TotalAmount,f.INV_Description Commodity,c.SAM_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,c.SAM_GrandDiscount As TradeDiscount,i.MAS_Desc As ModeOfDispatch, j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_PIN,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN,q.BM_Name,q.BM_Address,q.BM_MobileNo,q.BM_PinCode,q.BM_EmailID,r.Buyer_Value As BVAT,s.Buyer_Value As BTAX,t.Buyer_Value As BPAN,u.Buyer_Value As BTAN,v.Buyer_Value As BTIN,w.Buyer_Value As BCIN,z.PGD_manufactureDate,z.PGD_ExpireDate  
            '        from Sales_Dispatch_Details a 
            '        join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID 
            '        join Sales_Allocate_Master c on b.SDM_OrderID=c.SAM_OrderNo And c.SAM_ID=b.SDM_AllocateID 
            '        join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
            '        join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id 
            '        Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
            '        join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
            '        join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
            '        join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
            '        join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
            '        Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
            '        Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
            '        Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
            '        Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
            '        Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
            '        Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
            '        join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
            '        Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
            '        Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
            '        Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
            '        Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
            '        Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
            '        Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
            '        Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
            '        Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
            '        Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iInvice & " And b.SDM_ID=" & iDispatchID & " order by b.SDM_OrderID,a.SDD_HistoryID"

            'Working but items repeating for 0 inventory history id bcz it is entered by user directly in PO & SO'
            'sSql = "select Distinct(SDD_ID),SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,g.SPO_ID,g.SPO_OrderCode,g.SPO_OrderDate,g.SPO_BuyerOrderNo,g.SPO_BuyerOrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP, SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_GSTRate,SDD_GSTAmount,SDD_TotalAmount,SDD_ChargesPerItem,SDD_Amount,f.INV_Description Commodity,c.SAM_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,c.SAM_GrandDiscount As TradeDiscount,i.MAS_Desc As ModeOfDispatch, j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_PIN,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN,q.BM_Name,q.BM_Address,q.BM_MobileNo,q.BM_PinCode,q.BM_EmailID,r.Buyer_Value As BVAT,s.Buyer_Value As BTAX,t.Buyer_Value As BPAN,u.Buyer_Value As BTAN,v.Buyer_Value As BTIN,w.Buyer_Value As BCIN,z.PGD_manufactureDate,z.PGD_ExpireDate,ab.GST_CHST  
            '        from Sales_Dispatch_Details a 
            '        join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID 
            '        join Sales_Allocate_Master c on b.SDM_OrderID=c.SAM_OrderNo And c.SAM_ID=b.SDM_AllocateID 
            '        join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
            '        join Inventory_Master_History d on a.SDD_DescID=d.InvH_INV_Id 
            '        Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
            '        join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
            '        join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
            '        join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
            '        join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
            '        Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
            '        Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
            '        Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
            '        Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
            '        Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
            '        Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
            '        join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
            '        Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
            '        Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
            '        Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
            '        Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
            '        Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
            '        Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
            '        Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
            '        Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
            '        Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
            '        Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iInvice & " And b.SDM_ID=" & iDispatchID & " order by b.SDM_OrderID,a.SDD_HistoryID"
            'Working but items repeating for 0 inventory history id bcz it is entered by user directly in PO & SO'

            sSql = "select Distinct(SDD_ID),SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,g.SPO_ID,g.SPO_OrderCode,g.SPO_OrderDate,g.SPO_BuyerOrderNo,g.SPO_BuyerOrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID, SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_GSTRate,SDD_GSTAmount,SDD_TotalAmount,SDD_ChargesPerItem,SDD_Amount,f.INV_Description Commodity,c.SAM_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,c.SAM_GrandDiscount As TradeDiscount,i.MAS_Desc As ModeOfDispatch, j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_PIN,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN,q.BM_Name,q.BM_Address,q.BM_MobileNo,q.BM_PinCode,q.BM_EmailID,r.Buyer_Value As BVAT,s.Buyer_Value As BTAX,t.Buyer_Value As BPAN,u.Buyer_Value As BTAN,v.Buyer_Value As BTIN,w.Buyer_Value As BCIN,z.PGD_manufactureDate,z.PGD_ExpireDate,ab.GST_CHST,
                    b.SDM_DeliveryFrom,b.SDM_DeliveryFromGSTNRegNo,b.SDM_DeliveryAddress,b.SDM_DeliveryGSTNRegNo  
                    from Sales_Dispatch_Details a 
                    join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID 
                    join Sales_Allocate_Master c on b.SDM_OrderID=c.SAM_OrderNo And c.SAM_ID=b.SDM_AllocateID 
                    join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
                    join Inventory_Master_History d on a.SDD_DescID=d.InvH_INV_Id 
                    Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
                    join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
                    join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
                    join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
                    join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
                    Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
                    join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
                    Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
                    Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
                    Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
                    Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
                    Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
                    Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
                    Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
                    Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
                    Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
                    Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iInvice & " And b.SDM_ID=" & iDispatchID & " order by b.SDM_OrderID,a.SDD_HistoryID"

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDataOral(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvice As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "select Distinct(SDD_ID)As SDD_ID,SDD_MasterID,b.SDM_ID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_OrderID,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,c.SPO_ID,c.SPO_OrderCode,c.SPO_OrderDate,c.SPO_BuyerOrderNo,c.SPO_BuyerOrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,
            'SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP,
            'SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_TotalAmount,f.INV_Description Commodity,c.SPO_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,c.SPO_GrandDiscount As TradeDiscount,g.MAS_Desc As ModeOfDispatch,
            'i.CUST_NAME,i.CUST_Comm_Address,i.CUST_Comm_Tel,i.CUST_Email,i.CUST_PIN,j.Cmp_Value As CVAT,k.Cmp_Value As CTAX,l.Cmp_Value As CPAN,m.Cmp_Value As CTAN,n.Cmp_Value As CTIN,o.Cmp_Value As CCIN,p.BM_Name,p.BM_Address,p.BM_PinCode,p.BM_MobileNo,p.BM_EmailID,q.Buyer_Value As BVAT,r.Buyer_Value As BTAX,s.Buyer_Value As BPAN,t.Buyer_Value As BTAN,u.Buyer_Value As BTIN,v.Buyer_Value As BCIN,z.PGD_manufactureDate,z.PGD_ExpireDate
            'from Sales_Dispatch_Details a
            'join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID
            'join Sales_Proforma_Order c on b.SDM_OrderID=c.SPO_ID
            'join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id
            'join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
            'join Inventory_Master e on a.SDD_DescID=e.Inv_ID 
            'join Acc_General_master h on a.SDD_UnitID=h.Mas_ID 
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
            'Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
            'Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
            'Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iInvice & " And b.SDM_ID=" & iDispatchID & " order by b.SDM_OrderID,a.SDD_HistoryID"

            sSql = "select Distinct(SDD_ID)As SDD_ID,SDD_MasterID,b.SDM_ID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_OrderID,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,c.SPO_ID,c.SPO_OrderCode,c.SPO_OrderDate,c.SPO_BuyerOrderNo,c.SPO_BuyerOrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,
            SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP,
            SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_GSTRate,SDD_GSTAmount,SDD_TotalAmount,SDD_ChargesPerItem,SDD_Amount,f.INV_Description Commodity,c.SPO_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,c.SPO_GrandDiscount As TradeDiscount,g.MAS_Desc As ModeOfDispatch,
            i.CUST_NAME,i.CUST_Comm_Address,i.CUST_Comm_Tel,i.CUST_Email,i.CUST_PIN,j.Cmp_Value As CVAT,k.Cmp_Value As CTAX,l.Cmp_Value As CPAN,m.Cmp_Value As CTAN,n.Cmp_Value As CTIN,o.Cmp_Value As CCIN,p.BM_Name,p.BM_Address,p.BM_PinCode,p.BM_MobileNo,p.BM_EmailID,q.Buyer_Value As BVAT,r.Buyer_Value As BTAX,s.Buyer_Value As BPAN,t.Buyer_Value As BTAN,u.Buyer_Value As BTIN,v.Buyer_Value As BCIN,z.PGD_manufactureDate,z.PGD_ExpireDate,ab.GST_CHST
            from Sales_Dispatch_Details a
            join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID
            join Sales_Proforma_Order c on b.SDM_OrderID=c.SPO_ID
            join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id
            join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
            join Inventory_Master e on a.SDD_DescID=e.Inv_ID 
            join Acc_General_master h on a.SDD_UnitID=h.Mas_ID 
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
            Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
            Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
            Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
            Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iInvice & " And b.SDM_ID=" & iDispatchID & " order by b.SDM_OrderID,a.SDD_HistoryID"

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadOralDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iInvice As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("CName")
            dt.Columns.Add("CAddress")
            dt.Columns.Add("CTel")
            dt.Columns.Add("CEmail")
            dt.Columns.Add("CTIN")

            dt.Columns.Add("BName")
            dt.Columns.Add("BAddress")
            dt.Columns.Add("BTel")
            dt.Columns.Add("BEmail")
            dt.Columns.Add("BTIN")

            dt.Columns.Add("OrderNo")
            dt.Columns.Add("OrderDate")
            dt.Columns.Add("DispatchNo")
            dt.Columns.Add("DispatchDate")
            dt.Columns.Add("DispatchedThrough")

            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Unit")
            dt.Columns.Add("Qty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("RateAmount")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("Amount")

            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmount")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")
            dt.Columns.Add("Excise")
            dt.Columns.Add("ExciseAmount")
            dt.Columns.Add("GrandDiscount")
            dt.Columns.Add("GrandDiscountAmt")
            dt.Columns.Add("Shipping")

            sSql = "select SDD_ID,SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_ShippingRate,g.SPO_ID,g.SPO_OrderCode,g.SPO_OrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Description,e.INV_Description,"
            sSql = sSql & "SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP,"
            sSql = sSql & " SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_TotalAmount,f.INV_Description Commodity,c.SAM_GrandDiscountAmt,h.Mas_Desc As Unit,c.SAM_GrandDiscount,i.MAS_Desc As ModeOfDispatch,"
            sSql = sSql & " j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_Email,k.Cmp_Value,l.BM_Name,l.BM_Address,l.BM_MobileNo,l.BM_EmailID,m.Buyer_Value "
            sSql = sSql & " from Sales_Dispatch_Details a"
            sSql = sSql & " join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID"
            sSql = sSql & " join Sales_Allocate_Master c on b.SDM_OrderID=c.SAM_OrderNo"
            sSql = sSql & " join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID"
            sSql = sSql & " join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id"
            sSql = sSql & " Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID  "
            sSql = sSql & " join Inventory_Master e on a.SDD_DescID=e.Inv_ID "
            sSql = sSql & " join Acc_General_master h on a.SDD_UnitID=h.Mas_ID "
            sSql = sSql & " join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID "
            sSql = sSql & " join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID"
            sSql = sSql & " Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And Cmp_Desc='TIN'"
            sSql = sSql & " join Sales_Buyers_Masters l on b.SDM_SupplierID=l.BM_ID "
            sSql = sSql & " Left Join Sales_Buyer_Accounting_Template m On l.BM_ID=m.Buyer_ID And m.Buyer_Desc='TIN' where SDD_CompID=" & iCompID & " And c.SAM_OrderNo=" & iInvice & " order by b.SDM_OrderID,a.SDD_HistoryID"

            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()

                    dRow("CName") = dt1.Rows(i)("CUST_NAME")
                    dRow("CAddress") = dt1.Rows(i)("CUST_Comm_Address")
                    dRow("CTel") = dt1.Rows(i)("CUST_Comm_Tel")
                    dRow("CEmail") = dt1.Rows(i)("CUST_Email")
                    dRow("CTIN") = dt1.Rows(i)("Cmp_Value")

                    dRow("BName") = dt1.Rows(i)("BM_Name")
                    dRow("BAddress") = dt1.Rows(i)("BM_Address")
                    dRow("BTel") = dt1.Rows(i)("BM_MobileNo")
                    dRow("BEmail") = dt1.Rows(i)("BM_EmailID")
                    dRow("BTIN") = dt1.Rows(i)("Buyer_Value")

                    dRow("OrderNo") = dt1.Rows(i)("SPO_OrderCode")
                    dRow("OrderDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_OrderDate"), "D")
                    dRow("DispatchNo") = dt1.Rows(i)("SDM_Code")
                    dRow("DispatchDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SDM_DispatchDate"), "D")
                    dRow("DispatchedThrough") = dt1.Rows(i)("ModeOfDispatch")

                    dRow("SlNo") = i + 1
                    dRow("Commodity") = dt1.Rows(i)("Commodity")
                    dRow("Description") = dt1.Rows(i)("INV_Description")
                    dRow("Unit") = dt1.Rows(i)("Unit")
                    dRow("Qty") = dt1.Rows(i)("SDD_Quantity")
                    dRow("Rate") = dt1.Rows(i)("SDD_Rate")
                    dRow("RateAmount") = dt1.Rows(i)("SDD_RateAmount")
                    dRow("Discount") = dt1.Rows(i)("SDD_Discount")
                    dRow("DiscountAmt") = dt1.Rows(i)("SDD_DiscountAmount")
                    dRow("Amount") = dt1.Rows(i)("SDD_TotalAmount")

                    dRow("VAT") = dt1.Rows(i)("SDD_VAT")
                    dRow("VATAmount") = dt1.Rows(i)("SDD_VATAmount")
                    dRow("CST") = dt1.Rows(i)("SDD_CST")
                    dRow("CSTAmount") = dt1.Rows(i)("SDD_CSTAmount")
                    dRow("Excise") = dt1.Rows(i)("SDD_Excise")
                    dRow("ExciseAmount") = dt1.Rows(i)("SDD_ExciseAmount")
                    dRow("GrandDiscount") = dt1.Rows(i)("SAM_GrandDiscount")
                    dRow("GrandDiscountAmt") = dt1.Rows(i)("SAM_GrandDiscountAmt")
                    dRow("Shipping") = dt1.Rows(i)("SDM_ShippingRate")

                    dt.Rows.Add(dRow)
                Next
            End If

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
    Public Function GetVATBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow

        Dim dTradeDisAmt As Double
        Try

            dt1.Columns.Add("VAT")
            dt1.Columns.Add("Amount")
            dt1.Columns.Add("VATAmount")

            sSql = "Select Distinct(SDD_VAT) From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    'sVatDesc = dt.Rows(i)("SDD_VAT")
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SDD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(i)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_VATAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(i)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")

                    dRow("VAT") = sVatDesc
                    dRow("Amount") = sAmountTot
                    dRow("VATAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            ElseIf dt.Rows.Count = 1 Then
                Dim sVATAmtFinal As String = ""
                dRow = dt1.NewRow()
                'sVatDesc = dt.Rows(0)("SDD_VAT")
                sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(0)("SDD_VAT") & " And MAS_CompID=" & iCompID & " ")
                dTradeDisAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDM_GrandDiscountAmt) From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " ")
                sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(0)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_VATAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(0)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                sVATAmtFinal = (((sAmountTot - dTradeDisAmt)) * sVatDesc) / 100

                dRow("VAT") = sVatDesc
                dRow("Amount") = sAmountTot
                dRow("VATAmount") = sVATAmtFinal

                dt1.Rows.Add(dRow)
                sVatDesc = "" : sAmountTot = "" : sVATAmt = "" : sVATAmtFinal = "" : dTradeDisAmt = 0
            End If

            Return dt1
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
    Public Function GetCSTBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim sValue As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim dTradeDisAmt As Double
        Try

            dt1.Columns.Add("CST")
            dt1.Columns.Add("CAmount")
            dt1.Columns.Add("CSTAmount")

            sSql = "Select Distinct(SDD_CST) From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 1 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SDD_CST") & " And MAS_CompID=" & iCompID & " ")
                    'dt.Rows(i)("SDD_CST")
                    If dt.Rows(i)("SDD_CST") > 0 Then
                        sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(i)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                    Else
                        sAmountTot = 0
                    End If
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_CSTAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(i)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")

                    dRow("CST") = sVatDesc
                    dRow("CAmount") = sAmountTot
                    dRow("CSTAmount") = sVATAmt

                    dt1.Rows.Add(dRow)
                    sVatDesc = "" : sAmountTot = "" : sVATAmt = ""
                Next
            ElseIf dt.Rows.Count = 1 Then
                Dim sVATAmtFinal As String = ""
                dRow = dt1.NewRow()
                sVatDesc = objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(0)("SDD_CST") & " And MAS_CompID=" & iCompID & " ")
                'dt.Rows(0)("SDD_CST")
                dTradeDisAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDM_GrandDiscountAmt) From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & " ")
                If dt.Rows(0)("SDD_CST") > 0 Then
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(0)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                Else
                    sAmountTot = 0
                End If
                sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_CSTAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(0)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                sVATAmtFinal = (((sAmountTot - dTradeDisAmt)) * sVatDesc) / 100

                dRow("CST") = sVatDesc
                dRow("CAmount") = sAmountTot
                dRow("CSTAmount") = sVATAmtFinal

                dt1.Rows.Add(dRow)
                sVatDesc = "" : sAmountTot = "" : sVATAmt = "" : sVATAmtFinal = "" : dTradeDisAmt = 0
            End If

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetVATBifercationOral(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Try

            dt1.Columns.Add("VAT")
            dt1.Columns.Add("Amount")
            dt1.Columns.Add("VATAmount")

            sSql = "Select Distinct(SDD_VAT) From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = dt.Rows(i)("SDD_VAT")
                    'objDBL.SQLGetDescription(sNameSpace, "Select Mas_Desc From Acc_General_Master where Mas_ID=" & dt.Rows(i)("SDD_VAT") & " And MAS_CompID=" & iCompID & " ")
                    sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(i)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_VATAmount) From Sales_Dispatch_Details where SDD_VAT=" & dt.Rows(i)("SDD_VAT") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")

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
    Public Function GetCSTBifercationOral(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim sVatDesc, sVATAmt, sAmountTot As String
        Dim sStr As String = ""

        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Try

            dt1.Columns.Add("CST")
            dt1.Columns.Add("CAmount")
            dt1.Columns.Add("CSTAmount")

            sSql = "Select Distinct(SDD_CST) From Sales_Dispatch_Details where SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt1.NewRow()
                    sVatDesc = dt.Rows(i)("SDD_CST")
                    If dt.Rows(i)("SDD_CST") > 0 Then
                        sAmountTot = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_TotalAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(i)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")
                    Else
                        sAmountTot = 0
                    End If
                    sVATAmt = objDBL.SQLGetDescription(sNameSpace, "Select Sum(SDD_CSTAmount) From Sales_Dispatch_Details where SDD_CST=" & dt.Rows(i)("SDD_CST") & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master Where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & " And SDM_YearID=" & iYearID & ") ")

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
    Public Function GetCharges(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select C_ChargeType As ChargeType,C_ChargeAmount As ChargeAmount From Charges_Master Where C_DispatchID=" & iDispatchID & " And C_OrderID=" & iOrderID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetChargedAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Sum(C_ChargeAmount) As ChargeAmount From Charges_Master Where C_DispatchID=" & iDispatchID & " And C_OrderID=" & iOrderID & " And C_CompID=" & iCompID & " And C_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("ChargeAmount")) = False Then
                    GetChargedAmount = objDBL.SQLGetDescription(sNameSpace, sSql)
                Else
                    GetChargedAmount = 0
                End If
            End If
            Return GetChargedAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDispatchVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Distinct(SDD_GSTRate) From Sales_Dispatch_Details Where SDD_CompID=" & iCompID & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & ") "
            GetDispatchVAT = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetDispatchVAT
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDispatchCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Distinct(SDD_CST) From Sales_Dispatch_Details Where SDD_CompID=" & iCompID & " And SDD_MasterID in (Select SDM_ID From Sales_Dispatch_Master where SDM_ID=" & iDispatchID & " And SDM_OrderID=" & iOrderID & " And SDM_CompID=" & iCompID & ") "
            GetDispatchCST = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetDispatchCST
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetVAT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iVatID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_Desc From Acc_General_Master Where MAS_Master=14 And MAS_ID=" & iVatID & " And MAS_CompID=" & iCompID & ""
            GetVAT = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetVAT
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCST(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCstID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select MAS_Desc From Acc_General_Master Where MAS_ID=" & iCstID & " And MAS_CompID=" & iCompID & ""
            GetCST = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetCST
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGSTBifercation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dr As DataRow

        Dim dBasicAmt, dSGSTAmount, dCGSTAmount, dIGSTAmount, dGSTAmount As Double
        Dim dSGST, dCGST, dIGST, dGST As Double
        Try
            dt1.Columns.Add("SlNo")
            dt1.Columns.Add("BasicAmount")
            dt1.Columns.Add("SGST")
            dt1.Columns.Add("SGSTAmount")
            dt1.Columns.Add("CGST")
            dt1.Columns.Add("CGSTAmount")
            dt1.Columns.Add("IGST")
            dt1.Columns.Add("IGSTAmount")
            dt1.Columns.Add("GSTRate")
            dt1.Columns.Add("GSTAmount")

            sSql = "Select SDD_GSTRate,SDD_GSTAmount,SDD_TotalAmount ,SDD_SGST ,SDD_SGSTAmount ,SDD_CGST ,SDD_CGSTAmount ,SDD_IGST ,SDD_IGSTAmount  From Sales_Dispatch_Details Where SDD_MasterID=" & iDispatchID & " And SDD_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dt1.NewRow
                    dr("SlNo") = i + 1
                    dr("BasicAmount") = dt.Rows(i)("SDD_TotalAmount")
                    dBasicAmt = dBasicAmt + dt.Rows(i)("SDD_TotalAmount")

                    dr("SGST") = dt.Rows(i)("SDD_SGST")
                    dSGST = dSGST + dt.Rows(i)("SDD_SGST")

                    dr("SGSTAmount") = dt.Rows(i)("SDD_SGSTAmount")
                    dSGSTAmount = dSGSTAmount + dt.Rows(i)("SDD_SGSTAmount")

                    dr("CGST") = dt.Rows(i)("SDD_CGST")
                    dCGST = dCGST + dt.Rows(i)("SDD_CGST")

                    dr("CGSTAmount") = dt.Rows(i)("SDD_CGSTAmount")
                    dCGSTAmount = dCGSTAmount + dt.Rows(i)("SDD_CGSTAmount")

                    dr("IGST") = dt.Rows(i)("SDD_IGST")
                    dIGST = dIGST + dt.Rows(i)("SDD_IGST")

                    dr("IGSTAmount") = dt.Rows(i)("SDD_IGSTAmount")
                    dIGSTAmount = dIGSTAmount + dt.Rows(i)("SDD_IGSTAmount")

                    dr("GSTRate") = dt.Rows(i)("SDD_GSTRate")
                    dGST = dGST + dt.Rows(i)("SDD_GSTRate")

                    dr("GSTAmount") = dt.Rows(i)("SDD_GSTAmount")
                    dGSTAmount = dGSTAmount + dt.Rows(i)("SDD_GSTAmount")

                    dt1.Rows.Add(dr)
                Next
            End If

            dr = dt1.NewRow
            dr("SlNo") = "TOTAL"
            dr("BasicAmount") = dBasicAmt
            dr("SGST") = dSGST
            dr("SGSTAmount") = dSGSTAmount
            dr("CGST") = dCGST
            dr("CGSTAmount") = dCGSTAmount
            dr("IGST") = dIGST
            dr("IGSTAmount") = dIGSTAmount
            dr("GSTRate") = dGST
            dr("GSTAmount") = dGSTAmount
            dt1.Rows.Add(dr)

            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetURD(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim dt, dt1, dtDetails, dtdata As New DataTable
        Dim dRow As DataRow
        Dim dQty, dBasicAmt, dSGSTAmt, dCGSTAmt As Double
        Try

            sSql = "" : sSql = "select Distinct(SDD_ID),SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,g.SPO_ID,g.SPO_OrderCode,g.SPO_OrderDate,g.SPO_BuyerOrderNo,g.SPO_BuyerOrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP, SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_GSTRate,SDD_GSTAmount,SDD_TotalAmount,SDD_ChargesPerItem,SDD_Amount,SDD_SGSTAmount,SDD_CGSTAmount,f.INV_Description Commodity,c.SAM_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,c.SAM_GrandDiscount As TradeDiscount,i.MAS_Desc As ModeOfDispatch, j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_PIN,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN,q.BM_Name,q.BM_Address,q.BM_MobileNo,q.BM_PinCode,q.BM_EmailID,r.Buyer_Value As BVAT,s.Buyer_Value As BTAX,t.Buyer_Value As BPAN,u.Buyer_Value As BTAN,v.Buyer_Value As BTIN,w.Buyer_Value As BCIN,z.PGD_manufactureDate,z.PGD_ExpireDate,ab.GST_GSTRate,ab.GST_CHST  
                    from Sales_Dispatch_Details a 
                    join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID 
                    join Sales_Allocate_Master c on b.SDM_OrderID=c.SAM_OrderNo And c.SAM_ID=b.SDM_AllocateID 
                    join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
                    join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id 
                    Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
                    join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
                    join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
                    join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
                    join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
                    Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
                    join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
                    Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
                    Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
                    Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
                    Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
                    Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
                    Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
                    Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
                    Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
                    Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
                    Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iOrderID & " And b.SDM_ID=" & iDispatchID & " order by b.SDM_OrderID,a.SDD_HistoryID"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            sSql = "" : sSql = "select Distinct(ab.GST_GSTRate)                    
                    from Sales_Dispatch_Details a 
                    join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID 
                    join Sales_Allocate_Master c on b.SDM_OrderID=c.SAM_OrderNo And c.SAM_ID=b.SDM_AllocateID 
                    join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
                    join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id 
                    Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
                    join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
                    join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
                    join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
                    join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
                    Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
                    join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
                    Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
                    Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
                    Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
                    Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
                    Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
                    Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
                    Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
                    Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
                    Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
                    Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iOrderID & " And b.SDM_ID=" & iDispatchID & "  "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)


            dt1.Columns.Add("GSTRate")
            dt1.Columns.Add("Qty")
            dt1.Columns.Add("BasicAmount")
            dt1.Columns.Add("TaxAmount")
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

                            dQty = dQty + dtdata.Rows(j)("SDD_Quantity")
                            dRow("Qty") = dQty

                            dBasicAmt = dBasicAmt + dtdata.Rows(j)("SDD_Amount")
                            dRow("BasicAmount") = dBasicAmt

                            dSGSTAmt = dSGSTAmt + dtdata.Rows(j)("SDD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dtdata.Rows(j)("SDD_CGSTAmount")
                            dRow("TaxAmount") = dSGSTAmt + dCGSTAmt


                            dRow("Total") = Convert.ToDouble(dRow("BasicAmount")) + Convert.ToDouble(dRow("TaxAmount"))

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
    Public Function GetHSN(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim dt, dt1, dtDetails, dtdata As New DataTable
        Dim dRow As DataRow
        Dim dQty, dBasicAmt, dSGSTAmt, dCGSTAmt As Double
        Try

            sSql = "" : sSql = "select Distinct(SDD_ID),SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,g.SPO_ID,g.SPO_OrderCode,g.SPO_OrderDate,g.SPO_BuyerOrderNo,g.SPO_BuyerOrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP, SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_GSTRate,SDD_GSTAmount,SDD_TotalAmount,SDD_ChargesPerItem,SDD_Amount,SDD_SGSTAmount,SDD_CGSTAmount,f.INV_Description Commodity,c.SAM_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,c.SAM_GrandDiscount As TradeDiscount,i.MAS_Desc As ModeOfDispatch, j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_PIN,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN,q.BM_Name,q.BM_Address,q.BM_MobileNo,q.BM_PinCode,q.BM_EmailID,r.Buyer_Value As BVAT,s.Buyer_Value As BTAX,t.Buyer_Value As BPAN,u.Buyer_Value As BTAN,v.Buyer_Value As BTIN,w.Buyer_Value As BCIN,z.PGD_manufactureDate,z.PGD_ExpireDate,ab.GST_GSTRate,ab.GST_CHST  
                    from Sales_Dispatch_Details a 
                    join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID 
                    join Sales_Allocate_Master c on b.SDM_OrderID=c.SAM_OrderNo And c.SAM_ID=b.SDM_AllocateID 
                    join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
                    join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id 
                    Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
                    join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
                    join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
                    join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
                    join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
                    Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
                    join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
                    Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
                    Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
                    Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
                    Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
                    Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
                    Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
                    Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
                    Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
                    Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
                    Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iOrderID & " And b.SDM_ID=" & iDispatchID & " order by b.SDM_OrderID,a.SDD_HistoryID"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            sSql = "" : sSql = "select Distinct(ab.GST_CHST)                    
                    from Sales_Dispatch_Details a 
                    join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID 
                    join Sales_Allocate_Master c on b.SDM_OrderID=c.SAM_OrderNo And c.SAM_ID=b.SDM_AllocateID 
                    join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
                    join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id 
                    Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
                    join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
                    join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
                    join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
                    join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
                    Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
                    join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
                    Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
                    Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
                    Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
                    Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
                    Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
                    Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
                    Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
                    Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
                    Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
                    Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iOrderID & " And b.SDM_ID=" & iDispatchID & "  "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)


            dt1.Columns.Add("HSN")
            dt1.Columns.Add("Qty")
            dt1.Columns.Add("BasicAmount")
            dt1.Columns.Add("TaxAmount")
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

                            dQty = dQty + dtdata.Rows(j)("SDD_Quantity")
                            dRow("Qty") = dQty

                            dBasicAmt = dBasicAmt + dtdata.Rows(j)("SDD_Amount")
                            dRow("BasicAmount") = dBasicAmt

                            dSGSTAmt = dSGSTAmt + dtdata.Rows(j)("SDD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dtdata.Rows(j)("SDD_CGSTAmount")
                            dRow("TaxAmount") = dSGSTAmt + dCGSTAmt

                            dRow("Total") = Convert.ToDouble(dRow("BasicAmount")) + Convert.ToDouble(dRow("TaxAmount"))

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



    Public Function GetOralURD(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim dt, dt1, dtDetails, dtdata As New DataTable
        Dim dRow As DataRow
        Dim dQty, dBasicAmt, dSGSTAmt, dCGSTAmt As Double
        Try

            sSql = "" : sSql = "select Distinct(SDD_ID),SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,g.SPO_ID,g.SPO_OrderCode,g.SPO_OrderDate,g.SPO_BuyerOrderNo,g.SPO_BuyerOrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP, SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_GSTRate,SDD_GSTAmount,SDD_TotalAmount,SDD_ChargesPerItem,SDD_Amount,SDD_SGSTAmount,SDD_CGSTAmount,f.INV_Description Commodity,g.SPO_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,g.SPO_GrandDiscount As TradeDiscount,i.MAS_Desc As ModeOfDispatch, j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_PIN,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN,q.BM_Name,q.BM_Address,q.BM_MobileNo,q.BM_PinCode,q.BM_EmailID,r.Buyer_Value As BVAT,s.Buyer_Value As BTAX,t.Buyer_Value As BPAN,u.Buyer_Value As BTAN,v.Buyer_Value As BTIN,w.Buyer_Value As BCIN,z.PGD_manufactureDate,z.PGD_ExpireDate,ab.GST_GSTRate,ab.GST_CHST  
                    from Sales_Dispatch_Details a 
                    join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID                     
                    join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
                    join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id 
                    Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
                    join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
                    join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
                    join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
                    join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
                    Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
                    join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
                    Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
                    Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
                    Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
                    Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
                    Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
                    Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
                    Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
                    Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
                    Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
                    Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iOrderID & " And b.SDM_ID=" & iDispatchID & " order by b.SDM_OrderID,a.SDD_HistoryID"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            sSql = "" : sSql = "select Distinct(ab.GST_GSTRate)                    
                    from Sales_Dispatch_Details a 
                    join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID                      
                    join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
                    join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id 
                    Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
                    join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
                    join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
                    join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
                    join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
                    Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
                    join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
                    Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
                    Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
                    Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
                    Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
                    Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
                    Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
                    Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
                    Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
                    Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
                    Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iOrderID & " And b.SDM_ID=" & iDispatchID & "  "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)


            dt1.Columns.Add("GSTRate")
            dt1.Columns.Add("Qty")
            dt1.Columns.Add("BasicAmount")
            dt1.Columns.Add("TaxAmount")
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

                            dQty = dQty + dtdata.Rows(j)("SDD_Quantity")
                            dRow("Qty") = dQty

                            dBasicAmt = dBasicAmt + dtdata.Rows(j)("SDD_Amount")
                            dRow("BasicAmount") = dBasicAmt

                            dSGSTAmt = dSGSTAmt + dtdata.Rows(j)("SDD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dtdata.Rows(j)("SDD_CGSTAmount")
                            dRow("TaxAmount") = dSGSTAmt + dCGSTAmt


                            dRow("Total") = Convert.ToDouble(dRow("BasicAmount")) + Convert.ToDouble(dRow("TaxAmount"))

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
    Public Function GetOralHSN(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iOrderID As Integer, ByVal iDispatchID As Integer) As DataTable
        Dim sSql As String
        Dim dt, dt1, dtDetails, dtdata As New DataTable
        Dim dRow As DataRow
        Dim dQty, dBasicAmt, dSGSTAmt, dCGSTAmt As Double
        Try

            sSql = "" : sSql = "select Distinct(SDD_ID),SDD_MasterID,b.SDM_ID,b.SDM_OrderID,b.SDM_Code,b.SDM_DispatchDate,b.SDM_ShippingRate,b.SDM_DispatchRefNo,b.SDM_ESugamNo,b.SDM_Remarks,g.SPO_ID,g.SPO_OrderCode,g.SPO_OrderDate,g.SPO_BuyerOrderNo,g.SPO_BuyerOrderDate,SDD_HistoryID,e.Inv_Color,e.Inv_Size,e.INV_Code,e.INV_Description,SDD_CommodityID,SDD_DescID,SDD_HistoryID,SDD_Rate,SDD_Quantity,SDD_RateAmount,SDD_CompID,d.INVH_MRP, SDD_Discount,SDD_DiscountAmount,SDD_VAT,SDD_VATAmount,SDD_Excise,SDD_ExciseAmount,SDD_CST,SDD_CSTAmount,SDD_GSTRate,SDD_GSTAmount,SDD_TotalAmount,SDD_ChargesPerItem,SDD_Amount,SDD_SGSTAmount,SDD_CGSTAmount,f.INV_Description Commodity,g.SPO_GrandDiscountAmt As TradeDisAmt,h.Mas_Desc As Unit,g.SPO_GrandDiscount As TradeDiscount,i.MAS_Desc As ModeOfDispatch, j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_PIN,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN,q.BM_Name,q.BM_Address,q.BM_MobileNo,q.BM_PinCode,q.BM_EmailID,r.Buyer_Value As BVAT,s.Buyer_Value As BTAX,t.Buyer_Value As BPAN,u.Buyer_Value As BTAN,v.Buyer_Value As BTIN,w.Buyer_Value As BCIN,z.PGD_manufactureDate,z.PGD_ExpireDate,ab.GST_GSTRate,ab.GST_CHST  
                    from Sales_Dispatch_Details a 
                    join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID                    
                    join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
                    join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id 
                    Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
                    join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
                    join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
                    join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
                    join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
                    Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
                    join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
                    Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
                    Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
                    Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
                    Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
                    Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
                    Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
                    Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
                    Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
                    Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
                    Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iOrderID & " And b.SDM_ID=" & iDispatchID & " order by b.SDM_OrderID,a.SDD_HistoryID"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            sSql = "" : sSql = "select Distinct(ab.GST_CHST)                    
                    from Sales_Dispatch_Details a 
                    join Sales_Dispatch_Master b on a.SDD_MasterID=b.SDM_ID                     
                    join Sales_Proforma_Order g on b.SDM_OrderID=g.SPO_ID 
                    join Inventory_Master_History d on a.SDD_HistoryID=d.InvH_Id 
                    Join Inventory_Master f on a.SDD_CommodityID=f.Inv_ID   
                    join Inventory_Master e on a.SDD_DescID=e.Inv_ID  
                    join Acc_General_master h on a.SDD_UnitID=h.Mas_ID  
                    join Acc_General_master i on b.SDM_ModeOfShipping=i.Mas_ID  
                    join MST_Customer_Master j on b.SDM_CompID=j.CUST_ID 
                    Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT' 
                    Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX' 
                    Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN' 
                    Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN' 
                    Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN' 
                    Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' 
                    join Sales_Buyers_Masters q on b.SDM_SupplierID=q.BM_ID  
                    Left Join Sales_Buyer_Accounting_Template r On q.BM_ID=r.Buyer_ID And r.Buyer_Desc='VAT' 
                    Left Join Sales_Buyer_Accounting_Template s On q.BM_ID=s.Buyer_ID And s.Buyer_Desc='TAX' 
                    Left Join Sales_Buyer_Accounting_Template t On q.BM_ID=t.Buyer_ID And t.Buyer_Desc='PAN' 
                    Left Join Sales_Buyer_Accounting_Template u On q.BM_ID=u.Buyer_ID And u.Buyer_Desc='TAN' 
                    Left Join Sales_Buyer_Accounting_Template v On q.BM_ID=v.Buyer_ID And v.Buyer_Desc='TIN' 
                    Left Join Sales_Buyer_Accounting_Template w On q.BM_ID=w.Buyer_ID And w.Buyer_Desc='CIN' 
                    Left Join Stock_Ledger_SalesDetails x On b.SDM_OrderID=x.SLS_SaleOrderID And b.SDM_ID=x.SLS_DispatchID And a.SDD_HistoryID=x.SLS_HistoryID
                    Left Join Stock_Ledger y On y.SL_ID = x.SLS_MasterID
                    Left Join Purchase_GIN_Details z On y.SL_GINID=z.PGD_MasterID And y.SL_HistoryID=z.PGD_HistoryID And y.SL_Commodity=z.PGD_CommodityID And y.SL_ItemID=z.PGD_DescriptionID  
                    Left Join GST_Rates ab On ab.GST_ID = a.SDD_GST_ID  where b.SDM_CompID=" & iCompID & " And b.SDM_OrderID=" & iOrderID & " And b.SDM_ID=" & iDispatchID & "  "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)


            dt1.Columns.Add("HSN")
            dt1.Columns.Add("Qty")
            dt1.Columns.Add("BasicAmount")
            dt1.Columns.Add("TaxAmount")
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

                            dQty = dQty + dtdata.Rows(j)("SDD_Quantity")
                            dRow("Qty") = dQty

                            dBasicAmt = dBasicAmt + dtdata.Rows(j)("SDD_Amount")
                            dRow("BasicAmount") = dBasicAmt

                            dSGSTAmt = dSGSTAmt + dtdata.Rows(j)("SDD_SGSTAmount")
                            dCGSTAmt = dCGSTAmt + dtdata.Rows(j)("SDD_CGSTAmount")
                            dRow("TaxAmount") = dSGSTAmt + dCGSTAmt

                            dRow("Total") = Convert.ToDouble(dRow("BasicAmount")) + Convert.ToDouble(dRow("TaxAmount"))

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
